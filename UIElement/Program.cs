using UIAutomationClient;


//instancia UIAutomationClient
var automation = new CUIAutomation();


//Obtener el elemento raíz (Escritorio)
IUIAutomationElement desktop = automation.GetRootElement();
Console.WriteLine(desktop.GetCurrentPropertyValue(30005));

//search condition for Xpos Window
IUIAutomationCondition searchConditionWindow = automation.CreatePropertyCondition(30005,"Xpos");

//Obtener la ventana principal Xpos
IUIAutomationElement mainWindow = desktop.FindFirst(TreeScope.TreeScope_Subtree, searchConditionWindow);
Console.WriteLine(mainWindow.GetCurrentPropertyValue(30005));

//search condition to obtain all btnMenu
IUIAutomationCondition seacrhButtonCondition = automation.CreatePropertyCondition(30011, "btnMenu");

//Obtener array con todos los botones del menu
IUIAutomationElementArray buttonList = mainWindow.FindAll(TreeScope.TreeScope_Subtree, seacrhButtonCondition);

//Iteracion para recorrer la lista y extraer el lastchild usando rawviewwalk para hacer match del textBlock
//y dar click en el boton invocando su pattern nativo
for(int i = 0; i < buttonList.Length; i++)
{
   IUIAutomationElement individualButton = buttonList.GetElement(i);
   String textBlock = automation.RawViewWalker.GetLastChildElement(individualButton).GetCurrentPropertyValue(30005);
    if (textBlock.Equals("Depósito (corresponsalias)"))
    {
        IUIAutomationInvokePattern pattern = individualButton.GetCurrentPattern(10000);
        pattern.Invoke();
        continue;
    }
}

