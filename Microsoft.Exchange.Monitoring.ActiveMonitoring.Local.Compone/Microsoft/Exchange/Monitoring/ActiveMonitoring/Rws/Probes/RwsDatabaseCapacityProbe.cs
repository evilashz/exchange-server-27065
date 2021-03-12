using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes
{
	// Token: 0x0200044E RID: 1102
	public class RwsDatabaseCapacityProbe : ProbeWorkItem
	{
		// Token: 0x06001C08 RID: 7176 RVA: 0x000A19BC File Offset: 0x0009FBBC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			List<RwsDatabaseCapacityProbe.DatabaseFileUsage> list = new List<RwsDatabaseCapacityProbe.DatabaseFileUsage>();
			List<RwsDatabaseCapacityProbe.DatabaseFileUsage> list2 = new List<RwsDatabaseCapacityProbe.DatabaseFileUsage>();
			List<RwsDatabaseCapacityProbe.TableUsage> list3 = new List<RwsDatabaseCapacityProbe.TableUsage>();
			this.GetUsageData(list, list2, list3);
			base.Result.StateAttribute11 = string.Format("{0} data files usage.", list.Count);
			base.Result.StateAttribute12 = string.Format("{0} log files usage.", list2.Count);
			base.Result.StateAttribute13 = string.Format("{0} table usage.", list3.Count);
			this.WriteUsageData(list, list2, list3);
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000A1A50 File Offset: 0x0009FC50
		private void GetUsageData(List<RwsDatabaseCapacityProbe.DatabaseFileUsage> dataFileUsageList, List<RwsDatabaseCapacityProbe.DatabaseFileUsage> logFileUsageList, List<RwsDatabaseCapacityProbe.TableUsage> tableUsageList)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(base.Definition.Endpoint);
			base.Result.StateAttribute14 = string.Format("Target instance = {0}", sqlConnectionStringBuilder.DataSource);
			base.Result.StateAttribute15 = string.Format("Target database = {0}", sqlConnectionStringBuilder.InitialCatalog);
			int num = 0;
			try
			{
				IL_49:
				using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ToString()))
				{
					sqlConnection.Open();
					using (SqlCommand sqlCommand = new SqlCommand("\r\n                DECLARE @sql VARCHAR(8000), @sql2 VARCHAR(8000)\r\n                SET @sql = 'SELECT DB_NAME() AS [Database], SUM(reserved_page_count) * 8.0 / 1024 as [Size (MB)] FROM sys.dm_db_partition_stats'\r\n                SET @sql2 = 'USE [' + @DatabaseName + '];' + @sql\r\n\r\n                DECLARE @database_usage TABLE(\r\n                    database_name SYSNAME,\r\n                    space_used FLOAT\r\n                );\r\n\r\n                INSERT INTO @database_usage EXEC (@sql2)\r\n\r\n                SELECT\r\n                    db.[name] AS [Database],\r\n                    dbf.[physical_name] AS [File Name],\r\n                    convert(FLOAT, dbf.[size] * 8 / 1024.0) AS [Current Size (MB)],\r\n                    convert(FLOAT, dbu.[space_used]) AS [UsedSizeInMB],\r\n                    CASE WHEN (dbf.[max_size] <> -1) THEN CAST(dbf.[max_size]/1024 * 8 AS VARCHAR) ELSE 'Unlimited' END AS [Max Size (MB)],\r\n                    CASE WHEN (dbf.[is_percent_growth] = 1) THEN 'By ' + CAST(dbf.[growth] AS VARCHAR) + '%' ELSE 'By ' + CAST(dbf.[growth] * 8 / 1024 AS VARCHAR) + 'MB' END AS [AutoGrowthSetting]\r\n                FROM\r\n                    sys.[databases] AS db\r\n                    LEFT OUTER JOIN sys.[master_files] AS dbf ON [db].[database_id] = [dbf].[database_id], @database_usage AS dbu\r\n                WHERE\r\n                    dbf.[type] = 0 AND\r\n                    db.[name] = @DatabaseName AND\r\n                    dbu.[database_name] = db.[name]\r\n                ORDER BY \r\n                    db.[name], dbf.[file_id]\r\n            ", sqlConnection))
					{
						sqlCommand.Parameters.Add("@DatabaseName", SqlDbType.NVarChar);
						sqlCommand.Parameters["@DatabaseName"].Value = sqlConnectionStringBuilder.InitialCatalog;
						using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
						{
							while (sqlDataReader.Read())
							{
								dataFileUsageList.Add(new RwsDatabaseCapacityProbe.DatabaseFileUsage
								{
									IsDataFile = true,
									Instance = sqlConnectionStringBuilder.DataSource,
									Database = sqlDataReader.GetString(0),
									FileName = sqlDataReader.GetString(1),
									CurrentSizeInMB = sqlDataReader.GetDouble(2),
									UsedSizeInMB = sqlDataReader.GetDouble(3),
									MaxSizeInMB = sqlDataReader.GetString(4),
									AutoGrowthSetting = sqlDataReader.GetString(5)
								});
							}
						}
					}
					using (SqlCommand sqlCommand2 = new SqlCommand("\r\n                DECLARE @tran_log_space_usage TABLE(\r\n                    database_name SYSNAME,\r\n                    log_size_mb FLOAT,\r\n                    log_space_used FLOAT,\r\n                    [status] INT\r\n                );\r\n\r\n                INSERT INTO @tran_log_space_usage EXEC('DBCC SQLPERF (LOGSPACE)') \r\n\r\n                SELECT\r\n                    db.[name] AS [Database],\r\n                    dbf.[physical_name] AS [File Name],\r\n                    convert(FLOAT, dbf.[size] * 8 / 1024.0) AS [Current Size (MB)],\r\n                    convert(FLOAT, tlog.[log_space_used] * dbf.[size] * 8 / (1024 * 100)) AS [Used Size (MB)],\r\n                    CASE WHEN (dbf.[max_size] <> -1) THEN CAST(dbf.[max_size]/1024 * 8 AS VARCHAR) ELSE 'Unlimited' END AS [Max Size (MB)],\r\n                    CASE WHEN (dbf.[is_percent_growth] = 1) THEN 'By ' + CAST(dbf.[growth] AS VARCHAR) + '%' ELSE 'By ' + CAST(dbf.[growth] * 8 / 1024 AS VARCHAR) + 'MB' END AS [AutoGrowthSetting]\r\n                FROM \r\n                    sys.[databases] AS db\r\n                    LEFT OUTER JOIN sys.[master_files] AS dbf ON [db].[database_id] = [dbf].[database_id], @tran_log_space_usage AS tlog\r\n                WHERE\r\n                    dbf.[type] = 1 AND\r\n                    db.[name] = @DatabaseName AND\r\n                    tlog.[database_name] = db.[name]\r\n                ORDER BY \r\n                    db.[name], dbf.[file_id]\r\n            ", sqlConnection))
					{
						sqlCommand2.Parameters.Add("@DatabaseName", SqlDbType.NVarChar);
						sqlCommand2.Parameters["@DatabaseName"].Value = sqlConnectionStringBuilder.InitialCatalog;
						using (SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader())
						{
							while (sqlDataReader2.Read())
							{
								logFileUsageList.Add(new RwsDatabaseCapacityProbe.DatabaseFileUsage
								{
									IsDataFile = false,
									Instance = sqlConnectionStringBuilder.DataSource,
									Database = sqlDataReader2.GetString(0),
									FileName = sqlDataReader2.GetString(1),
									CurrentSizeInMB = sqlDataReader2.GetDouble(2),
									UsedSizeInMB = sqlDataReader2.GetDouble(3),
									MaxSizeInMB = sqlDataReader2.GetString(4),
									AutoGrowthSetting = sqlDataReader2.GetString(5)
								});
							}
						}
					}
					using (SqlCommand sqlCommand3 = new SqlCommand("\r\n                SELECT\r\n                    sys.objects.name as [TableName], convert(FLOAT, sum(reserved_page_count) * 8.0 / 1024) as [Size (MB)]\r\n                FROM \r\n                    sys.dm_db_partition_stats, sys.objects \r\n                WHERE \r\n                    sys.dm_db_partition_stats.object_id = sys.objects.object_id AND\r\n                    sys.objects.type = 'U'\r\n                GROUP BY \r\n                    sys.objects.name\r\n            ", sqlConnection))
					{
						using (SqlDataReader sqlDataReader3 = sqlCommand3.ExecuteReader())
						{
							while (sqlDataReader3.Read())
							{
								tableUsageList.Add(new RwsDatabaseCapacityProbe.TableUsage
								{
									Instance = sqlConnectionStringBuilder.DataSource,
									Database = sqlConnectionStringBuilder.InitialCatalog,
									Table = sqlDataReader3.GetString(0),
									CurrentSizeInMB = sqlDataReader3.GetDouble(1)
								});
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("Get exception when query source database. Exception: {0}. ", ex.ToString()), null, "GetUsageData", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityProbe.cs", 146);
				if (num++ < base.Definition.MaxRetryAttempts)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("Doing {0} retries.", num), null, "GetUsageData", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityProbe.cs", 153);
					goto IL_49;
				}
				throw new Exception("Failed to query source database. We have retried 3 times.", ex);
			}
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000A21A0 File Offset: 0x000A03A0
		private void WriteUsageData(List<RwsDatabaseCapacityProbe.DatabaseFileUsage> dataFileUsageList, List<RwsDatabaseCapacityProbe.DatabaseFileUsage> logFileUsageList, List<RwsDatabaseCapacityProbe.TableUsage> tableUsageList)
		{
			if (ExEnvironment.IsTest)
			{
				return;
			}
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(base.Definition.SecondaryEndpoint);
			if (!sqlConnectionStringBuilder.IntegratedSecurity)
			{
				sqlConnectionStringBuilder.Password = RwsCryptographyHelper.Decrypt(sqlConnectionStringBuilder.Password);
			}
			int num = 0;
			for (;;)
			{
				using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ToString()))
				{
					connection.Open();
					SqlTransaction trans = connection.BeginTransaction();
					try
					{
						dataFileUsageList.ForEach(delegate(RwsDatabaseCapacityProbe.DatabaseFileUsage data)
						{
							using (SqlCommand sqlCommand = new SqlCommand("\r\n                INSERT INTO [dbo].[Database_DataFileUsage]\r\n                (\r\n                    [Instance],\r\n                    [Database],\r\n                    [FileName],\r\n                    [CurrentSizeInMB],\r\n                    [UsedSizeInMB],\r\n                    [MaxSizeInMB]\r\n                )\r\n                VALUES\r\n                (\r\n                    @Instance,\r\n                    @Database,\r\n                    @FileName,\r\n                    @CurrentSizeInMB,\r\n                    @UsedSizeInMB,\r\n                    @MaxSizeInMB\r\n                )\r\n            ", connection))
							{
								sqlCommand.Transaction = trans;
								sqlCommand.Parameters.Add(new SqlParameter("Instance", data.Instance));
								sqlCommand.Parameters.Add(new SqlParameter("Database", data.Database));
								sqlCommand.Parameters.Add(new SqlParameter("FileName", data.FileName));
								sqlCommand.Parameters.Add(new SqlParameter("CurrentSizeInMB", data.CurrentSizeInMB));
								sqlCommand.Parameters.Add(new SqlParameter("UsedSizeInMB", data.UsedSizeInMB));
								sqlCommand.Parameters.Add(new SqlParameter("MaxSizeInMB", data.MaxSizeInMB));
								sqlCommand.Parameters.Add(new SqlParameter("AutoGrowthSetting", data.AutoGrowthSetting));
								sqlCommand.ExecuteNonQuery();
							}
						});
						logFileUsageList.ForEach(delegate(RwsDatabaseCapacityProbe.DatabaseFileUsage log)
						{
							using (SqlCommand sqlCommand = new SqlCommand("\r\n                INSERT INTO [dbo].[Database_LogFileUsage]\r\n                (\r\n                    [Instance],\r\n                    [Database],\r\n                    [FileName],\r\n                    [CurrentSizeInMB],\r\n                    [UsedSizeInMB],\r\n                    [MaxSizeInMB]\r\n                )\r\n                VALUES\r\n                (\r\n                    @Instance,\r\n                    @Database,\r\n                    @FileName,\r\n                    @CurrentSizeInMB,\r\n                    @UsedSizeInMB,\r\n                    @MaxSizeInMB\r\n                )\r\n            ", connection))
							{
								sqlCommand.Transaction = trans;
								sqlCommand.Parameters.Add(new SqlParameter("Instance", log.Instance));
								sqlCommand.Parameters.Add(new SqlParameter("Database", log.Database));
								sqlCommand.Parameters.Add(new SqlParameter("FileName", log.FileName));
								sqlCommand.Parameters.Add(new SqlParameter("CurrentSizeInMB", log.CurrentSizeInMB));
								sqlCommand.Parameters.Add(new SqlParameter("UsedSizeInMB", log.UsedSizeInMB));
								sqlCommand.Parameters.Add(new SqlParameter("MaxSizeInMB", log.MaxSizeInMB));
								sqlCommand.Parameters.Add(new SqlParameter("AutoGrowthSetting", log.AutoGrowthSetting));
								sqlCommand.ExecuteNonQuery();
							}
						});
						tableUsageList.ForEach(delegate(RwsDatabaseCapacityProbe.TableUsage table)
						{
							using (SqlCommand sqlCommand = new SqlCommand("\r\n                INSERT INTO [dbo].[Database_TableUsage]\r\n                (\r\n                    [Instance],\r\n                    [Database],\r\n                    [Table],\r\n                    [CurrentSizeInMB]\r\n                )\r\n                VALUES\r\n                (\r\n                    @Instance,\r\n                    @Database,\r\n                    @Table,\r\n                    @CurrentSizeInMB\r\n                )\r\n            ", connection))
							{
								sqlCommand.Transaction = trans;
								sqlCommand.Parameters.Add(new SqlParameter("Instance", table.Instance));
								sqlCommand.Parameters.Add(new SqlParameter("Database", table.Database));
								sqlCommand.Parameters.Add(new SqlParameter("Table", table.Table));
								sqlCommand.Parameters.Add(new SqlParameter("CurrentSizeInMB", table.CurrentSizeInMB));
								sqlCommand.ExecuteNonQuery();
							}
						});
						trans.Commit();
					}
					catch (Exception ex)
					{
						trans.Rollback();
						if (num++ < base.Definition.MaxRetryAttempts)
						{
							WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("Failed to write results into target storage. Doing {0} retries. Exception: {1}.", num, ex.ToString()), null, "WriteUsageData", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityProbe.cs", 257);
							continue;
						}
						WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("Failed to write results into target storage. We have tried 3 times. Exception: {0}.", ex.ToString()), null, "WriteUsageData", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatabaseCapacityProbe.cs", 266);
						throw new Exception("Failed to write results into target storage. We have tried 3 times", ex);
					}
				}
				break;
			}
		}

		// Token: 0x0200044F RID: 1103
		internal struct DatabaseFileUsage
		{
			// Token: 0x04001323 RID: 4899
			internal bool IsDataFile;

			// Token: 0x04001324 RID: 4900
			internal string Instance;

			// Token: 0x04001325 RID: 4901
			internal string Database;

			// Token: 0x04001326 RID: 4902
			internal string FileName;

			// Token: 0x04001327 RID: 4903
			internal double CurrentSizeInMB;

			// Token: 0x04001328 RID: 4904
			internal double UsedSizeInMB;

			// Token: 0x04001329 RID: 4905
			internal string MaxSizeInMB;

			// Token: 0x0400132A RID: 4906
			internal string AutoGrowthSetting;
		}

		// Token: 0x02000450 RID: 1104
		internal struct TableUsage
		{
			// Token: 0x0400132B RID: 4907
			internal string Instance;

			// Token: 0x0400132C RID: 4908
			internal string Database;

			// Token: 0x0400132D RID: 4909
			internal string Table;

			// Token: 0x0400132E RID: 4910
			internal double CurrentSizeInMB;
		}
	}
}
