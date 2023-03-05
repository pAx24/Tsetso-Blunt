using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Threading;

namespace ScriptReaderNameSpace;

public class ScriptReader
{
	public string fileLocation { get; set; }
	private string fileContents;
	private string[] operatorsArray;
	private int oneI = 0, twoI = 0, threeI = 0, fourI = 0, fiveI = 0, sixI = 0, sevenI = 0, eightI = 0, nineI = 0, tenI = 0;
	private string oneS = "null", twoS = "null", threeS = "null", fourS = "null", fiveS = "null", sixS = "null", sevenS = "null", eightS = "null", nineS = "null", tenS = "null";
	private int line = 1;
	private int outTemp;
	private int secondReturn = 0;

	public void Execute()
	{
		int length = fileLocation.Length - 7;
		string fileType = "";
		for (int i = fileLocation.Length; i != length; length++)
			fileType += fileLocation[length].ToString();

		if (fileType != ".script")
			throw new FileIncorrectFormatException();

		try
		{
			fileContents = File.ReadAllText(fileLocation);
		}
		catch
		{
			throw new FileNotInLocationException();
		}

		operatorsArray = fileContents.Split("\n");

		foreach (string oper in operatorsArray)
		{
			string operOnly = oper[0].ToString() + oper[1].ToString() + oper[2].ToString();

			if (operOnly == "say")
				Say(oper);
			else if (operOnly == "inp")
				Inp(oper);
			else if (operOnly == "stp")
				Stp();
			else if (operOnly == "set")
				Set(oper);
			else if (operOnly == "add")
				Add(oper);
			else if (operOnly == "rid")
				Rid(oper);
			else if (operOnly == "mul")
				Mul(oper);
			else if (operOnly == "div")
				Div(oper);
			else if (operOnly == "pow")
				Pow(oper);
            else if (operOnly == "sqr")
                Sqr(oper);
            line++;
		}
	}

	public void PrintOutScript()
	{
		try
		{
			Console.WriteLine($"Showing script at {fileLocation}");
			fileContents = File.ReadAllText(fileLocation);
		}
		catch
		{
			throw new FileNotInLocationException();
		}

		Console.WriteLine(fileContents);
	}

	//-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- BASE OPERATORS -+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

	private void Say(string oper)
	{
		string contents = "";
		int index = 4;
		char piece = '0';
		while (true)
		{
			piece = oper[index];
			if (piece == ')')
				break;
			if (piece == '!')
			{
				index++;
				switch (Var(index, oper))
				{
					case "oneS": contents += oneS; break;
					case "twoS": contents += twoS; break;
					case "threeS": contents += threeS; break;
					case "fourS": contents += fourS; break;
					case "fiveS": contents += fiveS; break;
					case "sixS": contents += sixS; break;
					case "sevenS": contents += sevenS; break;
					case "eightS": contents += eightS; break;
					case "nineS": contents += nineS; break;
					case "tenS": contents += tenS; break;
					//-----------------------------------------
					case "oneI": contents += oneI; break;
					case "twoI": contents += twoI; break;
					case "threeI": contents += threeI; break;
					case "fourI": contents += fourI; break;
					case "fiveI": contents += fiveI; break;
					case "sixI": contents += sixI; break;
					case "sevenI": contents += sevenI; break;
					case "eightI": contents += eightI; break;
					case "nineI": contents += nineI; break;
					case "tenI": contents += tenI; break;
					default: throw new ImproperSyntaxException(line); break;
				}
				index = secondReturn;
			}
			else
				contents += piece.ToString();
			index++;
		}
		Console.WriteLine(contents);
	}

	private string Var(int index, string oper)
	{
		string variable = "";
		while (true)
		{
			if (oper[index] == 'S' || oper[index] == 'I')
			{
				variable += oper[index].ToString();
				break;
			}
			variable += oper[index].ToString();
			index++;
		}
		secondReturn = index;
		return variable;
	}
	
	private void Inp(string oper)
	{
		string contents = "";
		int index = 5;
		char piece = '0';
		
		if(!oper.Contains(")") || !oper.Contains("("))
			throw new ImproperSyntaxException(line);
		
		while (true)
		{
			piece = oper[index];
			if (piece == ')')
				break;
			contents += piece.ToString();
			index++;
		}

		Console.Write(">");
		if (contents[contents.Length - 1] == 'S')
			switch (contents)
			{
				case "oneS":   oneS = Console.ReadLine();   break;
				case "twoS":   twoS = Console.ReadLine();   break;
				case "threeS": threeS = Console.ReadLine(); break;
				case "fourS":  fourS = Console.ReadLine();  break;
				case "fiveS":  fiveS = Console.ReadLine();  break;
				case "sixS":   sixS = Console.ReadLine();   break;
				case "sevenS": sevenS = Console.ReadLine(); break;
				case "eightS": eightS = Console.ReadLine(); break;
				case "nineS":  nineS = Console.ReadLine();  break;
				case "tenS":   tenS = Console.ReadLine();   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else if (contents[contents.Length - 1] == 'I')
			switch (contents)
			{
				case "oneI":   oneI = int.Parse(Console.ReadLine());   break;
				case "twoI":   twoI = int.Parse(Console.ReadLine());   break;
				case "threeI": threeI = int.Parse(Console.ReadLine()); break;
				case "fourI":  fourI = int.Parse(Console.ReadLine());  break;
				case "fiveI":  fiveI = int.Parse(Console.ReadLine());  break;
				case "sixI":   sixI = int.Parse(Console.ReadLine());   break;
				case "sevenI": sevenI = int.Parse(Console.ReadLine()); break;
				case "eightI": eightI = int.Parse(Console.ReadLine()); break;
				case "nineI":  nineI = int.Parse(Console.ReadLine());  break;
				case "tenI":   tenI = int.Parse(Console.ReadLine());   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else
			throw new ImproperSyntaxException(line);
	}
	
	private void Stp()
	{
		Console.Write("Press any key to continue...");
		Console.ReadKey();
	}
	
	private void Set(string oper)
	{
		int index = 5;
		string varWhich = "";
		
		int intType = 0;
		string stringType = "";
		
		switch(Var(index, oper))
		{
			case "oneS":   varWhich = "oneS";   break;
			case "twoS":   varWhich = "twoS";   break;
			case "threeS": varWhich = "threeS"; break;
			case "fourS":  varWhich = "fourS";  break;
			case "fiveS":  varWhich = "fiveS";  break;
			case "sixS":   varWhich = "sixS";   break;
			case "sevenS": varWhich = "sevenS"; break;
			case "eightS": varWhich = "eightS"; break;
			case "nineS":  varWhich = "nineS";  break;
			case "tenS":   varWhich = "tenS";   break;
			//----------------------------------------
			case "oneI":   varWhich = "oneI";   break;
			case "twoI":   varWhich = "twoI";   break;
			case "threeI": varWhich = "threeI"; break;
			case "fourI":  varWhich = "fourI";  break;
			case "fiveI":  varWhich = "fiveI";  break;
			case "sixI":   varWhich = "sixI";   break;
			case "sevenI": varWhich = "sevenI"; break;
			case "eightI": varWhich = "eightI"; break;
			case "nineI":  varWhich = "nineI";  break;
			case "tenI":   varWhich = "tenI";   break;
			default: throw new ImproperSyntaxException(line); break;
		}
		
		index = secondReturn;
		index += 3;
		
		if (varWhich[varWhich.Length - 1] == 'S')
		{
			while (true)
			{
				if (oper[index] == ')')
				{	
					break;
				}
				stringType += oper[index].ToString();
				index++;
			}
			
			switch(varWhich)
			{
				case "oneS":   oneS = stringType;   break;
				case "twoS":   twoS = stringType;   break;
				case "threeS": threeS = stringType; break;
				case "fourS":  fourS = stringType;  break;
				case "fiveS":  fiveS = stringType;  break;
				case "sixS":   sixS = stringType;   break;
				case "sevenS": sevenS = stringType; break;
				case "eightS": eightS = stringType; break;
				case "nineS":  nineS = stringType;  break;
				case "tenS":   tenS = stringType;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		}
		else
		{	
			while(int.TryParse(oper[index].ToString(), out outTemp))
			{	
				intType = intType * 10 + int.Parse(oper[index].ToString());
				index++;
			}
			
			switch(varWhich)
			{
				case "oneI":   oneI = intType;   break;
				case "twoI":   twoI = intType;   break;
				case "threeI": threeI = intType; break;
				case "fourI":  fourI = intType;  break;
				case "fiveI":  fiveI = intType;  break;
				case "sixI":   sixI = intType;   break;
				case "sevenI": sevenI = intType; break;
				case "eightI": eightI = intType; break;
				case "nineI":  nineI = intType;  break;
				case "tenI":   tenI = intType;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		}
	}
	
	//-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- MATH OPERATORS -+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
	
	private void Add(string oper)
	{
		int index = 5;
		char piece = '0';
		int first = 0, second = 0;
		string varNum ="";
		
		switch(Var(index, oper))
		{	
			case "oneI":   first = oneI; varNum = "oneI";     break;
			case "twoI":   first = twoI; varNum = "twoI";     break;
			case "threeI": first = threeI; varNum = "threeI"; break;
			case "fourI":  first = fourI; varNum = "fourI";   break;
			case "fiveI":  first = fiveI; varNum = "fiveI";   break;
			case "sixI":   first = sixI; varNum = "sixI";     break;
			case "sevenI": first = sevenI; varNum = "sevenI"; break;
			case "eightI": first = eightI; varNum = "eightI"; break;
			case "nineI":  first = nineI; varNum = "nineI";   break;
			case "tenI":   first = tenI; varNum = "tenI";     break;
			default: throw new ImproperSyntaxException(line); break;
		}
		index = secondReturn + 1;
		
		index += 2;
		
		if(oper[index] == '!')
			switch(Var(++index, oper))
			{	
				case "oneI":   second = oneI;   break;
				case "twoI":   second = twoI;   break;
				case "threeI": second = threeI; break;
				case "fourI":  second = fourI;  break;
				case "fiveI":  second = fiveI;  break;
				case "sixI":   second = sixI;   break;
				case "sevenI": second = sevenI; break;
				case "eightI": second = eightI; break;
				case "nineI":  second = nineI;  break;
				case "tenI":   second = tenI;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else
			while (int.TryParse(oper[index].ToString(), out outTemp))
			{
				second += int.Parse(oper[index].ToString());
				index++;
			}
		
		switch (varNum)
		{
			case "oneI":   oneI = first + second;   break;
			case "twoI":   twoI = first + second;   break;
			case "threeI": threeI = first + second; break;
			case "fourI":  fourI = first + second;  break;
			case "fiveI":  fiveI = first + second;  break;
			case "sixI":   sixI = first + second;   break;
			case "sevenI": sevenI = first + second; break;
			case "eightI": eightI = first + second; break;
			case "nineI":  nineI = first + second;  break;
			case "tenI":   tenI = first + second;   break;
			default: throw new ImproperSyntaxException(line); break;
		}
	}
	
	private void Rid(string oper)
	{
		int index = 5;
		char piece = '0';
		int first = 0, second = 0;
		string varNum ="";
		
		switch(Var(index, oper))
		{	
			case "oneI":   first = oneI; varNum = "oneI";     break;
			case "twoI":   first = twoI; varNum = "twoI";     break;
			case "threeI": first = threeI; varNum = "threeI"; break;
			case "fourI":  first = fourI; varNum = "fourI";   break;
			case "fiveI":  first = fiveI; varNum = "fiveI";   break;
			case "sixI":   first = sixI; varNum = "sixI";     break;
			case "sevenI": first = sevenI; varNum = "sevenI"; break;
			case "eightI": first = eightI; varNum = "eightI"; break;
			case "nineI":  first = nineI; varNum = "nineI";   break;
			case "tenI":   first = tenI; varNum = "tenI";     break;
			default: throw new ImproperSyntaxException(line); break;
		}
		index = secondReturn + 1;
		
		index += 2;
		
		if(oper[index] == '!')
			switch(Var(++index, oper))
			{	
				case "oneI":   second = oneI;   break;
				case "twoI":   second = twoI;   break;
				case "threeI": second = threeI; break;
				case "fourI":  second = fourI;  break;
				case "fiveI":  second = fiveI;  break;
				case "sixI":   second = sixI;   break;
				case "sevenI": second = sevenI; break;
				case "eightI": second = eightI; break;
				case "nineI":  second = nineI;  break;
				case "tenI":   second = tenI;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else
			while (int.TryParse(oper[index].ToString(), out outTemp))
			{
				second += int.Parse(oper[index].ToString());
				index++;
			}
		
		switch (varNum)
		{
			case "oneI":   oneI = first - second;   break;
			case "twoI":   twoI = first - second;   break;
			case "threeI": threeI = first - second; break;
			case "fourI":  fourI = first - second;  break;
			case "fiveI":  fiveI = first - second;  break;
			case "sixI":   sixI = first - second;   break;
			case "sevenI": sevenI = first - second; break;
			case "eightI": eightI = first - second; break;
			case "nineI":  nineI = first - second;  break;
			case "tenI":   tenI = first - second;   break;
			default: throw new ImproperSyntaxException(line); break;
		}
	}
	
	private void Mul(string oper)
	{
		int index = 5;
		char piece = '0';
		int first = 0, second = 0;
		string varNum ="";
		
		switch(Var(index, oper))
		{	
			case "oneI":   first = oneI; varNum = "oneI";     break;
			case "twoI":   first = twoI; varNum = "twoI";     break;
			case "threeI": first = threeI; varNum = "threeI"; break;
			case "fourI":  first = fourI; varNum = "fourI";   break;
			case "fiveI":  first = fiveI; varNum = "fiveI";   break;
			case "sixI":   first = sixI; varNum = "sixI";     break;
			case "sevenI": first = sevenI; varNum = "sevenI"; break;
			case "eightI": first = eightI; varNum = "eightI"; break;
			case "nineI":  first = nineI; varNum = "nineI";   break;
			case "tenI":   first = tenI; varNum = "tenI";     break;
			default: throw new ImproperSyntaxException(line); break;
		}
		index = secondReturn + 1;
		
		index += 2;
		
		if(oper[index] == '!')
			switch(Var(++index, oper))
			{	
				case "oneI":   second = oneI;   break;
				case "twoI":   second = twoI;   break;
				case "threeI": second = threeI; break;
				case "fourI":  second = fourI;  break;
				case "fiveI":  second = fiveI;  break;
				case "sixI":   second = sixI;   break;
				case "sevenI": second = sevenI; break;
				case "eightI": second = eightI; break;
				case "nineI":  second = nineI;  break;
				case "tenI":   second = tenI;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else
			while (int.TryParse(oper[index].ToString(), out outTemp))
			{
				second += int.Parse(oper[index].ToString());
				index++;
			}
		
		switch (varNum)
		{
			case "oneI":   oneI = first * second;   break;
			case "twoI":   twoI = first * second;   break;
			case "threeI": threeI = first * second; break;
			case "fourI":  fourI = first * second;  break;
			case "fiveI":  fiveI = first * second;  break;
			case "sixI":   sixI = first * second;   break;
			case "sevenI": sevenI = first * second; break;
			case "eightI": eightI = first * second; break;
			case "nineI":  nineI = first * second;  break;
			case "tenI":   tenI = first * second;   break;
			default: throw new ImproperSyntaxException(line); break;
		}
	}
	
	private void Div(string oper)
	{
		int index = 5;
		char piece = '0';
		int first = 0, second = 0;
		string varNum ="";
		
		switch(Var(index, oper))
		{	
			case "oneI":   first = oneI; varNum = "oneI";     break;
			case "twoI":   first = twoI; varNum = "twoI";     break;
			case "threeI": first = threeI; varNum = "threeI"; break;
			case "fourI":  first = fourI; varNum = "fourI";   break;
			case "fiveI":  first = fiveI; varNum = "fiveI";   break;
			case "sixI":   first = sixI; varNum = "sixI";     break;
			case "sevenI": first = sevenI; varNum = "sevenI"; break;
			case "eightI": first = eightI; varNum = "eightI"; break;
			case "nineI":  first = nineI; varNum = "nineI";   break;
			case "tenI":   first = tenI; varNum = "tenI";     break;
			default: throw new ImproperSyntaxException(line); break;
		}
		index = secondReturn + 1;
			
		index += 2;
		
		if(oper[index] == '!')
			switch(Var(++index, oper))
			{	
				case "oneI":   second = oneI;   break;
				case "twoI":   second = twoI;   break;
				case "threeI": second = threeI; break;
				case "fourI":  second = fourI;  break;
				case "fiveI":  second = fiveI;  break;
				case "sixI":   second = sixI;   break;
				case "sevenI": second = sevenI; break;
				case "eightI": second = eightI; break;
				case "nineI":  second = nineI;  break;
				case "tenI":   second = tenI;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else
			while (int.TryParse(oper[index].ToString(), out outTemp))
			{
				second += int.Parse(oper[index].ToString());
				index++;
			}
		
		switch (varNum)
		{
			case "oneI":   oneI = first / second;   break;
			case "twoI":   twoI = first / second;   break;
			case "threeI": threeI = first / second; break;
			case "fourI":  fourI = first / second;  break;
			case "fiveI":  fiveI = first / second;  break;
			case "sixI":   sixI = first / second;   break;
			case "sevenI": sevenI = first / second; break;
			case "eightI": eightI = first / second; break;
			case "nineI":  nineI = first / second;  break;
			case "tenI":   tenI = first / second;   break;
			default: throw new ImproperSyntaxException(line); break;
		}
	}
	
	private void Pow(string oper)
	{
		int index = 5;
		char piece = '0';
		int first = 0, second = 0;
		string varNum ="";
		
		switch(Var(index, oper))
		{	
			case "oneI":   first = oneI; varNum = "oneI";     break;
			case "twoI":   first = twoI; varNum = "twoI";     break;
			case "threeI": first = threeI; varNum = "threeI"; break;
			case "fourI":  first = fourI; varNum = "fourI";   break;
			case "fiveI":  first = fiveI; varNum = "fiveI";   break;
			case "sixI":   first = sixI; varNum = "sixI";     break;
			case "sevenI": first = sevenI; varNum = "sevenI"; break;
			case "eightI": first = eightI; varNum = "eightI"; break;
			case "nineI":  first = nineI; varNum = "nineI";   break;
			case "tenI":   first = tenI; varNum = "tenI";     break;
			default: throw new ImproperSyntaxException(line); break;
		}
		index = secondReturn + 1;
		
		index += 2;
		
		if(oper[index] == '!')
			switch(Var(++index, oper))
			{	
				case "oneI":   second = oneI;   break;
				case "twoI":   second = twoI;   break;
				case "threeI": second = threeI; break;
				case "fourI":  second = fourI;  break;
				case "fiveI":  second = fiveI;  break;
				case "sixI":   second = sixI;   break;
				case "sevenI": second = sevenI; break;
				case "eightI": second = eightI; break;
				case "nineI":  second = nineI;  break;
				case "tenI":   second = tenI;   break;
				default: throw new ImproperSyntaxException(line); break;
			}
		else
			while (int.TryParse(oper[index].ToString(), out outTemp))
			{
				second += int.Parse(oper[index].ToString());
				index++;
			}
		
		switch (varNum)
		{
			case "oneI":   oneI = (int) Math.Pow(first, second);   break;
			case "twoI":   twoI = (int) Math.Pow(first, second);   break;
			case "threeI": threeI = (int) Math.Pow(first, second); break;
			case "fourI":  fourI = (int) Math.Pow(first, second);  break;
			case "fiveI":  fiveI = (int) Math.Pow(first, second);  break;
			case "sixI":   sixI = (int) Math.Pow(first, second);   break;
			case "sevenI": sevenI = (int) Math.Pow(first, second); break;
			case "eightI": eightI = (int) Math.Pow(first, second); break;
			case "nineI":  nineI = (int) Math.Pow(first, second);  break;
			case "tenI":   tenI = (int) Math.Pow(first, second);   break;
			default: throw new ImproperSyntaxException(line); break;
		}
	}

    private void Sqr(string oper)
    {
        int index = 5;
        char piece = '0';
        int first = 0, second = 0;
        string varNum = "";
		
        switch (Var(index, oper))
        {
            case "oneI": oneI = (int)Math.Sqrt(oneI); break;
            case "twoI": twoI = (int)Math.Sqrt(twoI); break;
			case "threeI": threeI = (int)Math.Sqrt(threeI); break;
            case "fourI": fourI = (int)Math.Sqrt(fourI); break;
            case "fiveI": fiveI = (int)Math.Sqrt(fiveI); break;
            case "sixI": sixI = (int)Math.Sqrt(sixI); break;
            case "sevenI": sevenI = (int)Math.Sqrt(sevenI); break;
            case "eightI": eightI = (int)Math.Sqrt(eightI); break;
            case "nineI": nineI = (int)Math.Sqrt(nineI); break;
            case "tenI": tenI = (int)Math.Sqrt(tenI); break;
            default: throw new ImproperSyntaxException(line); break;
        }
    }

    //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- OTHER OPERATORS -+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

}


//Exceptions.
[Serializable]
public class FileIncorrectFormatException : Exception
{
	public FileIncorrectFormatException() : base("The file is in an incorrect format.") { }
}

[Serializable]
public class FileNotInLocationException : Exception
{
	public FileNotInLocationException() : base("The file is not in the specified location.") { }
}

[Serializable]
public class ImproperSyntaxException : Exception
{
	public ImproperSyntaxException(int location) : base($"Improper syntax at line {location}.") { }
}