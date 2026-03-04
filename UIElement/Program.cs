using UIAutomationClient;


class AutomationFlow
{
    static void Main(string[] args)
    {

        //instancia UIAutomationClient
        CUIAutomation automation = new CUIAutomation();

        //int values for element propeties
        int Property_Name = 30005;
        int Property_AutomationId = 30011;

        //int value for element patternId
        int PatternID_Invoke = 10000;

        //variable para controlar el flujo segun la entrada
        string firstArg = args[0];

        //Obtener el elemento raíz (Escritorio)
        IUIAutomationElement desktop = automation.GetRootElement();

        //search condition for Xpos Window
        IUIAutomationCondition searchConditionWindow = automation.CreatePropertyCondition(Property_Name, "Xpos");

        //Obtener la ventana principal Xpos
        IUIAutomationElement mainWindow = desktop.FindFirst(TreeScope.TreeScope_Subtree, searchConditionWindow);
      
        //search condition to obtain all btnMenu
        IUIAutomationCondition seacrhButtonCondition = automation.CreatePropertyCondition(Property_AutomationId, "btnMenu");

        //Obtener array con todos los botones del menu
        IUIAutomationElementArray buttonList = mainWindow.FindAll(TreeScope.TreeScope_Subtree, seacrhButtonCondition);

       
        switch (firstArg)
        {
            case "0":
                for (int i = 0; i < buttonList.Length; i++)
                {
                    IUIAutomationElement individualButton = buttonList.GetElement(i);
                    String textBlock = automation.RawViewWalker.GetLastChildElement(individualButton).GetCurrentPropertyValue(Property_Name);
                    Console.WriteLine(textBlock);
                }
                break;
            case "1":
                //Iteracion para recorrer la lista y extraer el lastchild usando rawviewwalk para hacer match del textBlock
                //y dar click en el boton invocando su pattern nativo
                for (int i = 0; i < buttonList.Length; i++)
                {
                    IUIAutomationElement individualButton = buttonList.GetElement(i);
                    String textBlock = automation.RawViewWalker.GetLastChildElement(individualButton).GetCurrentPropertyValue(Property_Name);
                    if (textBlock.Equals(args[1]))
                    {
                        IUIAutomationInvokePattern pattern = individualButton.GetCurrentPattern(PatternID_Invoke);
                        pattern.Invoke();
                        break;
                    }
                }
                break;
        }
    }
}

