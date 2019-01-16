using System;
using System.Globalization;

public class RegisterScriptMenu
{
    [DeclareMenu]
    public void MenuFunction()
    {
		//string menuText = getMenuText();
		Eplan.EplApi.Gui.ContextMenu oMenu =
		new Eplan.EplApi.Gui.ContextMenu();
		
		Eplan.EplApi.Gui.ContextMenuLocation oLocation =
		new Eplan.EplApi.Gui.ContextMenuLocation(
		    "GedEditGuiText",
		    "1002"
		    );
		
		Eplan.EplApi.Gui.ContextMenuLocation oLocation2 = 
		new Eplan.EplApi.Gui.ContextMenuLocation(
				"XDTDataDialog",
				"4006"
			);

		
		oMenu.AddMenuItem(
		oLocation,
		"Convert Case Text..",
		"ToUpper",
		true,
		false
		);
		
		oMenu.AddMenuItem(
		oLocation2,
		"Convert Case Text..",
		"ToUpper",
		true,
		false
		);

    }
    [DeclareAction("ToUpper")]
    public void ToUpper()
    {
    	
        string sSourceText = string.Empty;
		string sReturnText = string.Empty;
		string EplanCRLF = "¶";

		//Clear le clipboard
		System.Windows.Forms.Clipboard.Clear();

		//Copie le texte
		CommandLineInterpreter oCLI = new CommandLineInterpreter();
		oCLI.Execute("GfDlgMgrActionIGfWind /function:SelectAll"); // Alles markieren
		oCLI.Execute("GfDlgMgrActionIGfWind /function:Copy"); // Kopieren

		if (System.Windows.Forms.Clipboard.ContainsText())
		{
			sSourceText = System.Windows.Forms.Clipboard.GetText();
			if (sSourceText != string.Empty)
			{
				if (!IsAllUpper(sSourceText))
				{					
					sReturnText = sSourceText.ToUpper();
				}
				else
				{					
					/*if (IsAllLower(sSourceText))
					{
						sReturnText = UppercaseFirst(sSourceText.ToLower());
					}
					else
					{*/
						sReturnText = sSourceText.ToLower();
					//}
				}
				System.Windows.Forms.Clipboard.SetText(sReturnText);
				oCLI.Execute("GfDlgMgrActionIGfWind /function:SelectAll"); // Alles markieren
				oCLI.Execute("GfDlgMgrActionIGfWindDelete"); // Löschen
				oCLI.Execute("GfDlgMgrActionIGfWind /function:Paste"); // Einfügen
			}
		}
			
        return;
    }
	
	private bool IsAllUpper(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                return false;
        }
        return true;
    }

	private bool IsAllLower(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsLetter(input[i]) && !Char.IsLower(input[i]))
                return false;
        }
        return true;
    }
	
	private string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
        
}