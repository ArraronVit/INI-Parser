using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.Convert;


namespace IniParserLab3
{
     public class IniParser
    {
        private struct SectionPair
        {
            public string section;
            public string key;
        }
 
        private string path;
        private readonly Hashtable hashPairs = new Hashtable();
        public IniParser(string path)
        {
            StreamReader iniFile = null;
            string currentSection = null;
            string value = null;
 
            this.path = path;
            try
            { 
                if (File.Exists(path)) 
                {
                    iniFile = new StreamReader(path);
                    string inputString;
                    
                    while ((inputString = iniFile.ReadLine()) != null)
                    {
                        inputString = ignoreCommAndSpaces(inputString).Trim().ToUpper();

                        if (inputString == string.Empty)
                            continue;
                        if (inputString.StartsWith("[") && inputString.EndsWith("]"))
                            currentSection = inputString.Substring(1, inputString.Length - 2);
                        else
                        {
                            var keyPair = inputString.Split(new char[] { '=' }, 2);
 
                            SectionPair sectionPair;
                            sectionPair.section = currentSection;
                            sectionPair.key = keyPair[0];
 
                            if (keyPair.Length > 1)
                                value = keyPair[1];

                            if (!hashPairs.ContainsKey(sectionPair)) 
                                hashPairs.Add(sectionPair, value);
                        }
                    } 
                }
                else
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to open file " + path);
            }
        }
        public void getData<T>(string section, string name, ref T variable)
        {
            SectionPair sectionPair;

            sectionPair.section = section.ToUpper();
            sectionPair.key = name.ToUpper();
            try
            {
                if (hashPairs.ContainsKey(sectionPair))
                {
                    var value = (string)hashPairs[sectionPair];
                    variable = checkTypeVariable(variable, value);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
               
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Exception! Name  OR Section in hashtable not found.");
            }
        }
 
 
        private string ignoreCommAndSpaces(string str)
        {
            var foo = str.Replace(" ", string.Empty).Trim();
           
            var index = foo.IndexOf(';');
            if (index == 0)
                    foo = "";
            else if(index != -1)
                    foo = foo.Substring(0,index);
            return foo;
        }
        private T checkTypeVariable<T>(T variable, string val)
        {
            switch (Type.GetTypeCode(variable.GetType()))
            {
                case TypeCode.Int32:
                    variable = (T) (ToInt32(val) as object);
                    break;
                case TypeCode.String:
                    variable = (T) (val as object);
                    break;
                case TypeCode.Decimal:
                    variable = (T) (ToDecimal(val) as object);
                    break;
                case TypeCode.Single:
                    variable = (T) (ToSingle(val) as object);
                    break;
                default:
                    throw new Exception("Exception! TypeCode not found");
            }

            return variable;
        }
    }
}