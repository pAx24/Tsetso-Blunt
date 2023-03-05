using System;
using System.Linq;
using System.Collections.Generic;
using ScriptReaderNameSpace;

namespace ScriptReaderTest;

public static class Program 
{         
    public static void Main()
    {
        ScriptReader sr = new ScriptReader();
		
		sr.fileLocation = "C:\\test.script";
		sr.Execute();
		sr.PrintOutScript();
    }
}
