using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

using OracleImport.Models;

namespace OracleImport
{
    class Program
    {
        private static Logger logger = LogManager.GetLogger("OracleImport");

        static void Main(string[] args)
        {
            LandingRepository repo = new LandingRepository();

            logger.Info("~~~~ Program Started ~~~~");

            List<Landing_Table> import_tables = repo.GetLandingTables();

            foreach(Landing_Table table in import_tables)
            {
                logger.Info("Importing {0}", table.Table_Name);
                Landing_Table_Log TableLog = repo.New_Log_Record(table.id);

                try
                {
                    String password = RijndaelSimple.Decrypt(table.Source_Pass, "PLanning and Audit", "Diplomatic Persistance", "SHA1", 1, "AGHYEFIVOPJNSFRU", 128);

                    if(repo.TableExists(table.Dest_Schema,table.Table_Name))
                    {
                        logger.Debug("Table {0}.{1} exists",table.Dest_Schema,table.Table_Name);
                    } else
                    {
                        logger.Debug("Table {0}.{1} does not exist", table.Dest_Schema, table.Table_Name);
                    }


                    TableLog.Start_Time = DateTime.Now;
                    TableLog.Success = 0;
                    repo.Update_Log(TableLog);

                } catch(Exception ex)
                {
                    TableLog.Notes = "Exception: "+ex.Message;
                    logger.Error(ex,"Error loading table "+table.Table_Name);
                } finally
                {
                    logger.Info("Finished {0}", table.Table_Name);
                    TableLog.End_Time = DateTime.Now;
                    repo.Update_Log(TableLog);
                }
            }


            logger.Info("~~~~ Program Finished ~~~~");


            //System.Console.ReadKey();
            LogManager.Flush();
            LogManager.Shutdown();
        }
    }
}
