using System;

namespace IniParserLab3
{
    public static class Program
    {
        public static void Main()
        {
            IniParser parser = new IniParser("/Users/vitaly/RiderProjects/IniParserLab3/IniParserLab3/test.ini");
            
            var i = 0;
            parser.getData("LEGACY_XML", "ListenTcpPort", ref i);
            Console.WriteLine(i);
 
            Single s = 0;
            parser.getData("ADC_DEV", "BufferLenSeconds", ref s);
            Console.WriteLine(s);
 
            decimal d = 0;
            parser.getData("ncmd", "SampleRate", ref d);
            Console.WriteLine(d);

            var ss = "" ;
            parser.getData("common", "DiskCachePath", ref ss);
            Console.WriteLine(ss);
        }
    }
}
 