using Cosmos.HAL;
using Cosmos.HAL.BlockDevice;
using Cosmos.HAL.Drivers.USB;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.FAT;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System_Plugs.System.IO;
using PerfexiOS.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Commands
{
    internal class FSMANGER
    {
        public  CommandManager Parent { get; set; }
        
        public Message msg { get; private set; } 

        internal FSMANGER(CommandManager parent)
        {
            msg = parent.msg;
        }




        
        // this function was taken from the sphere OS source code becuz I was too lazy to write it 
        private void CreateIfNotExists(string dir)
        {
            if (!Directory.Exists(dir))
            {

                Directory.CreateDirectory(dir);

            }
        }

        



        internal void List(string dir)
        {
            try
            {

                foreach (var item in Directory.GetDirectories(dir))
                {
                    
                    msg.send("DIR: "+item, 0);
                   


                }
                foreach (var item in Directory.GetFiles(dir))
                {
                    msg.send("FILE: "+item, 0);

                }
                
                

                
        
                    
            }
            catch(Exception e) 
            {
                msg.send("ERROR:"+e, 0);
            }

        }
        internal  void DELDIR(string item,string dir)
        {
            try
            {
                foreach(var _item in Directory.GetDirectories(dir))
                {
                   
                    if(_item.StartsWith(item))
                    {
                        Directory.Delete(_item);

                        msg.send("Deleted" + _item, 1);   
                    }
                }
            }
            catch
            {
                msg.send("ERROR",1);
            }
        }

        internal void Newf(string path)
        {
            if(!Globals.FSinitalised)
            {
                msg.send("FILESYSTEM NOT INITALISED", 0);
                
            }
            else
            {
                try
                {
                    if(File.Exists(Parent.WorkingDirectory+path))
                    {
                        msg.send("This already exists ",0);
                    }
                    else
                    {
                        File.Create(Parent.WorkingDirectory+path);
                        
                        
                    }
                }
                catch(Exception e)
                {
                    msg.send("Failed to create file"+e, 0);
                }
            }
        }
       



        internal  void Dirn(string path,string hdir)
        {
            // Check if filesystem is initalised if not then print a line saying the filesystem cannot continue 
            if (!Globals.FSinitalised)
            {
                msg.send("ERROR:FILE SYSTEM IS NOT INITALISED", 0);
            }
            else
            {


                try
                {
                    if (Directory.Exists(hdir + path))
                    {
                        msg.send("This directory already exists", 0);
                    }
                    else { VFSManager.CreateDirectory(hdir +path); msg.send("FS:MSG:Directory "+path+" has been created! ", 0); }

                }
                catch (Exception e)
                {
                     msg.send("ERROR"+e,0);

                }

            }
        }
        internal  void FOLDOP(string path,string hdir)
        {
            if (!Globals.FSinitalised)
            {
                msg.send("ERROR:FILE SYSTEM IS NOT INITALISED", 0);
            }
            else
            {
                if (Directory.Exists($@"{Parent.currentdrive}{path}\"))
                {
                    Parent.WorkingDirectory = $@"{Parent.currentdrive}{path}";
                    msg.send("Sucessfully Moved to:"+path,0);
                    hdir = Directory.GetCurrentDirectory();
                }
                else if(path == "ROOT")
                {
                    Directory.SetCurrentDirectory($@"{Parent.currentdrive}:\");
                    hdir = Directory.GetCurrentDirectory();
                }

                else
                {
                    msg.send("This Directory dosen't exist use FS:DIRN to create it", 0);
                }
            }
        }
        internal  void FIDEL(string path)
        {
           
           
            if(File.Exists($@"{Parent.WorkingDirectory}{path}\"))
            {
                try
                {
                    File.Delete($@"{Parent.WorkingDirectory}{path}\");

                }
                catch(Exception e)
                {
                    msg.send("FAILED TO DELETE "+e.ToString(), 0);
                }
            }
            
        }
        internal void FOLDEL(string path)
        {
            
            if (File.Exists($@"{Parent.WorkingDirectory}{path}\"))
            {
                try
                {
                    Directory.Delete($@"{Parent.WorkingDirectory}{path}\");

                }
                catch (Exception e)
                {
                    msg.send("FAILED TO DELETE " + e.ToString(), 0);
                }
            }
            
        }
        internal  void DSKSIZE()
        {
            msg.send("YOUR DISK IS:"+Globals.drive.TotalSize+"BYTES    YOU HAVE:"+Globals.drive.AvailableFreeSpace + "BYTES FREE",0);
        }
        // diskpart 
        internal void ChangeDisk(int disk)
        {
            if (VFSManager.IsValidDriveId(@$"{disk}:\"))
            {
                Directory.SetCurrentDirectory($@"{disk}:\");
            }
            else
            {
                msg.send("This disk dosen't exist",0);

            }
                
        }
        internal void ListDisks()
        {
            var drives = VFSManager.GetDisks();
            int i = 0;

            msg.send("     CONNECTED DISKS", 0);
            msg.send("--------------------------", 0);

            foreach (var item in VFSManager.GetDisks())
            {
                var _type = item.Type.GetType;

                if (item.Partitions.Count > 0)
                {
                    for (int index = 0; i < item.Partitions.Count; i++)
                    {
                        
                        msg.send("Partition #: " + (index + 1),0);
          
                        msg.send("Block Size: " + item.Partitions[index].Host.BlockSize + " bytes",0);

                        msg.send("Block Partitions: " + item.Partitions[index].Host.BlockCount,0);
                      
                        msg.send("Size: " + item.Partitions[index].Host.BlockCount * item.Partitions[i].Host.BlockSize / 1024 / 1024 + " MB",0);
                    }
                }
                else
                {
                    msg.send("No partitons found on this disk",0);
                }
               












                msg.send("DISK:" + i, 0);


                
                
                msg.send("PARTITIONCOUNT:" + item.Partitions.Count, 0);

                item.DisplayInformation();



                switch(item.Type)
                {
                    case BlockDeviceType.RemovableCD:

                        msg.send("DEVICE:CDR", 0);

                        break;
                    case BlockDeviceType.HardDrive: msg.send("DEVICE:HDD", 0); break;

                    case BlockDeviceType.Removable: msg.send("DEVICE:EX-STORAGE", 0); break;
                }

                

                
                


                msg.send("RAWSIZE:" + item.Size + " BYTES", 0);
                






                msg.send("--------------------", 0);

                i++;
            }

        }
        internal  void NewPartition(int disk, int size)
        {

            var _disk = VFSManager.GetDisks()[disk];

            
            
            


            try
            {
                _disk.CreatePartition(size);
                msg.send(size + " Bytes Partion sucessfully created on DRIVE:" + disk,0);




            }
            catch (Exception e)
            {
                msg.send("Failed to create parition ERROR INFO:" + e,0);
            }

        }
        internal void DelPartition(int disk, int parition)
        {
            var _disk = VFSManager.GetDisks()[disk];

            try
            {
                _disk.DeletePartition(parition);

            }
            catch (Exception e)
            {
                msg.send("Failed to delete partition ERROR:" + e, 0);
            }






        }

        internal  void FormatDisk(int disk,int part, int quick)
        {
            var _disk = VFSManager.GetDisks()[disk];

            bool _quick = quick > 0;

            try
            {
                _disk.FormatPartition(part, "FAT32", _quick);
                
                CreateIfNotExists($@"{disk}:\");
                Directory.SetCurrentDirectory(@$"{disk}:\");
            }
            catch (Exception e) { msg.send("FAILED TO FORMAT THE DISK "+e,0); }

        }
        internal  void Mount(int disk,int part)
        {
            var _disk = VFSManager.GetDisks()[disk];
            _disk.MountPartition(part);
            Parent.currentdrive = disk;

           
        }
        internal void Check(int disk)
        {
            var _disk = VFSManager.GetDisks()[disk];

            msg.send("INFO FOR DISK:"+ disk,0);

            msg.send("PARTITIONCOUNT:" + _disk.Partitions.Count, 0);
            
            
            switch (_disk.Type)
            {
                case BlockDeviceType.RemovableCD:

                    msg.send("DEVICE:CDR",0);

                    break;
                case BlockDeviceType.HardDrive:msg.send("DEVICE:HDD",0); break;

                case BlockDeviceType.Removable:msg.send("DEVICE:EX-STORAGE",0); break;
            }


            _disk.DisplayInformation();



        }
       
        

    }
}
    




