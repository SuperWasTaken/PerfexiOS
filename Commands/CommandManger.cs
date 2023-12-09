
using System;

using System.Linq;

using Sys = Cosmos.System;


using System.IO;

using Mirage.UIKit;
using PerfexiOS.DE;


namespace PerfexiOS.Commands
{
    


    public class CommandManager 
    {
        


        public Message msg { get; init; }
        public string WorkingDirectory { get; set; }
        public int currentdrive { get; set; }

        private readonly FSMANGER FS;
 
        



        public CommandManager()
        {
            

            msg = new Message();
            
            FS = new FSMANGER(this);
            currentdrive = 0;
            WorkingDirectory = @"0:\";

        }

        public void Update()
        {
            var userinput = Console.ReadLine();
            parse(userinput);
        }


        // THe command manger now just parses everything and is not static anymore so it can be instanced 
        public void parse(string cmd) // I changed the command manager 
        {
            
            if (!cmd.Contains(':'))
            {
                msg.send(cmd + ": IS NOT A VALID SYNTAX", 0);
            }
            else
            {
                string[] commandParts = cmd.Split(':');
                string commandName = commandParts[1];
                string[] arguments = commandParts.Skip(2).ToArray();
                string commandtype = commandParts[0];

                try
                {

                    switch (commandtype)
                    {
                        case "FS": // All commands involing the file system are here 
                            switch (commandName)
                            {
                                case "LIST":
                                    FS.List(this.WorkingDirectory);
                                    break;
                                case "FOLDOP":
                                    FS.FOLDOP(arguments[0],this.WorkingDirectory);
                                    break;
                                case "FOLDEL":
                                    FS.DELDIR(arguments[0],this.WorkingDirectory);



                                    break;
                                case "FOLDCOP":

                                    break;
                                case "FIDEL":
                                    FS.FIDEL(arguments[0]);
                                    break;
                                case "FICOP":
                                    var file = arguments[0];

                                    msg.send("Comming soon use the File Explorer app to preform this action in the meantime", 0);
                                  
                                    break;

                                case "DIRN":
                                    FS.Dirn(arguments[0],WorkingDirectory);


                                    break;
                                case "NEWF":

                                    FS.Newf(arguments[0]);



                                    break;
                               

                                case "PRINTFI":
                                    msg.send(File.ReadAllText(WorkingDirectory + arguments[0]), 0);
                                    break;
                                case "DISKPART":
                                    switch (arguments[0])
                                    {
                                        case "LIST":

                                            FS.ListDisks();

                                            break;
                                        case "NEWPART":



                                            FS.NewPartition((int.Parse(arguments[1])), int.Parse(arguments[2]));






                                            break;
                                        case "DELPART":

                                            FS.DelPartition(int.Parse(arguments[1]), int.Parse(arguments[2]));


                                            break;
                                        case "FORMAT":






                                            FS.FormatDisk(int.Parse(arguments[1]), int.Parse(arguments[2]), int.Parse(arguments[3]));


                                            break;
                                        case "MOUNT":

                                            FS.Mount(int.Parse(arguments[1]), int.Parse(arguments[2]));



                                            break;
                                        case "DSKINFO":
                                            FS.Check(int.Parse(arguments[1]));




                                            break;
                                        case "CHANGEDISK":


                                            FS.ChangeDisk(int.Parse(arguments[1]));
                                            break;


                                    }

                                    break;

                                default:
                                    msg.send("ERROR INVALID COMMAND OR COMMAND TYPE", 0);
                                    break;



                            }


                            break;
                        case "SYS":
                            switch (commandName)
                            {
                                case "SHUTDOWN-COMPUTER-RIGHT-NOW!!!!!!":
                                    Sys.Power.Shutdown();
                                    break;
                                case "REBOOT-COMPUTER-RIGHT-NOW!!!!!!":
                                    Sys.Power.Reboot();

                                    break;

                                    
                                case "LAUNCH-APP":

                                    msg.send("This will Disable the CLI and load a terminal based app (Currently not supported yet \nand will only be supported in Terminal mode)", 0);


                                    break;
                                case "CREDIT":
                                    msg.send("CREDITS:", 0);
                                    msg.send("SUPER - CREATOR OF PERFEXI OS", 0);
                                    msg.send("GoldenretriverYT - TTF MODULE(Modified by Super)", 0);
                                    msg.send("Luma Technologies Sphere OS -Inspiration", 0);
                                    msg.send("napalmtorch - Purple Moon os and KBSTRING READER", 0);
                                    msg.send("Denis Bartashevich for MIV", 0);
                                    msg.send("All windows flash parodies ", 0);
                                    msg.send("Mirage OS desktop Envirment",0);





                                    break;




                            }


                            break;
                        case "TER":
                            switch (commandName)
                            {
                                case "PRINTLIN":
                                    msg.send(arguments[0], 0);

                                    break;
                                case "COLOR":
                                    switch (arguments[0])
                                    {
                                        case "GREEN":
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            break;
                                        case "WHITE":
                                            Console.ForegroundColor = ConsoleColor.White;
                                            break;
                                        case "BLUE":
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            break;
                                        case "CYAN":
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            break;
                                        case "YELLOW":
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            break;
                                        case "RED":
                                            Console.ForegroundColor= ConsoleColor.Red;
                                            break;
                                        case "DARK-BLUE":
                                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                                            break;
                                    }
                                    break;
                                case "CLEAR":
                                    Console.Clear();
                                    break;
                                case "BCOLOR":
                                    switch (arguments[0])
                                    {
                                        case "GREEN":
                                            Console.BackgroundColor = ConsoleColor.Green;
                                            break;
                                        case "WHITE":
                                            Console.BackgroundColor = ConsoleColor.White;
                                            break;
                                        case "BLUE":
                                            Console.BackgroundColor = ConsoleColor.Blue;
                                            break;
                                        case "CYAN":
                                            Console.BackgroundColor = ConsoleColor.Cyan;
                                            break;
                                        case "YELLOW":
                                            Console.BackgroundColor = ConsoleColor.Yellow;
                                            break;
                                        case "RED":
                                            Console.BackgroundColor= ConsoleColor.Red;
                                            break;
                                        case "DARK-BLUE":
                                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                                            break;
                                    }




                                    break;
                                case "PLEASE-SAY-MY-LINE!!!!!!!!":

                                    msg.send(arguments[0], 0);

                                    break;


                            }


                            break;








                        default:

                            msg.send("Syntax ERROR", 0);

                            break;
                    }









                }
                catch
                {
                    msg.send("SYNTAX ERROR!", 0);
                }




            }

        }
            
    }


}





