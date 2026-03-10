using System.Drawing;
using System.Text;
using UIAutomationClient;

/*------------------------------------------------------------------------------------------
 Author: Roberto Renteria
 Date: 04/03/2026
 Description: Obtiene el texto de los btnMenu de Xpos(0), 
 Invoca el evento click del bntMenu(1) 
 usando RawViewWalk para los elementos que no son identificados IsControlElement = false
 ------------------------------------------------------------------------------------------*/
class AutomationFlow
{
    static void Main(string[] args)
    {
        //Set de Encoding UTF-8 para el output
        Console.OutputEncoding = Encoding.UTF8;

        //instancia UIAutomationClient
        CUIAutomation automation = new CUIAutomation();

        //int values for element propeties
        int Property_Name = 30005;
        int Property_AutomationId = 30011;
        int Property_BoundingRectangle = 30001;

        //int value for element patternId
        int PatternID_Invoke = 10000;

        //valores de propiedad
        string valueNameXpos = "Xpos";
        string valueAutomationId_btnMenu = "btnMenu";

        try
        {
            //variable para controlar el flujo segun la entrada
            string firstArg = args[0];

            //Obtener el elemento raíz (Escritorio)
            IUIAutomationElement desktop = automation.GetRootElement();

            //search condition for Xpos Window
            IUIAutomationCondition searchConditionWindow = automation.CreatePropertyCondition(Property_Name, valueNameXpos);

            //Obtener la ventana principal Xpos
            IUIAutomationElement mainWindow = desktop.FindFirst(TreeScope.TreeScope_Subtree, searchConditionWindow);
            mainWindow.SetFocus();

            //search condition to obtain all btnMenu
            IUIAutomationCondition seacrhButtonCondition = automation.CreatePropertyCondition(Property_AutomationId, valueAutomationId_btnMenu);

            //Obtener array con todos los botones del menu
            IUIAutomationElementArray buttonList = mainWindow.FindAll(TreeScope.TreeScope_Subtree, seacrhButtonCondition);

            //recibe args[0] y decide el flujo
            switch (firstArg)
            {
                case "0":
                    //Iteracion para recorrer la lista y extraer todos los lastchilds(Name) usando rawviewwalk
                    for (int i = 0; i < buttonList.Length; i++)
                    {
                        IUIAutomationElement individualButton = buttonList.GetElement(i);
                        string textBlock = automation.RawViewWalker.GetLastChildElement(individualButton).GetCurrentPropertyValue(Property_Name);
                        Console.WriteLine(textBlock);
                    }
                    break;
                case "1":
                    //valida se haya pasado el segundo valor
                    if (args.Length < 2)
                    {
                        Console.Error.WriteLine("Error: No se especifico el segundo valor");
                        break;
                    }

                    //Iteracion para recorrer la lista y extraer el lastchild usando rawviewwalk para hacer match del textBlock
                    //y dar click en el boton invocando su pattern nativo
                    for (int i = 0; i < buttonList.Length; i++)
                    {
                        IUIAutomationElement individualButton = buttonList.GetElement(i);
                        string textBlock = automation.RawViewWalker.GetLastChildElement(individualButton).GetCurrentPropertyValue(Property_Name);
                        if (textBlock.Equals(args[1]))
                        {
                            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                            Pen pen = new(Color.Red, 3);

                            var rect = individualButton.GetCurrentPropertyValue(Property_BoundingRectangle);

                            double left = rect[0];
                            double top = rect[1];
                            double right = rect[2];
                            double bottom = rect[3];

                            //realiza highlight al elemento
                            g.DrawRectangle(pen, (float)left ,(float)top , (float)right ,(float)bottom);
                            
                            //invoca el pattern nativo (click)
                            IUIAutomationInvokePattern pattern = individualButton.GetCurrentPattern(PatternID_Invoke);
                            pattern.Invoke();
                            break;
                        }
                    }
                    break;
                default:
                    Console.Error.WriteLine("Error: Solo acepta valores 0 y 1");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"A general error ocurred: {e.Message}");
        }
    }
}