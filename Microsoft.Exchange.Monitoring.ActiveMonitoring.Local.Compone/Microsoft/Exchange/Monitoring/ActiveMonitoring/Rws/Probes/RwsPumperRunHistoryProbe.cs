using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes
{
	// Token: 0x0200044A RID: 1098
	public class RwsPumperRunHistoryProbe : ProbeWorkItem
	{
		// Token: 0x06001BFA RID: 7162 RVA: 0x000A08CC File Offset: 0x0009EACC
		private List<RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness> GetInvalidCFRPumperCompletenessData()
		{
			string connectionString = string.Format("Data Source={0};Initial Catalog=cdm-tenantds;Integrated Security=SSPI;", base.Definition.SecondaryEndpoint);
			List<RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness> list = new List<RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness>();
			string commandText = "\r\n                            SELECT DatamartServerName AS TargetDBInstance,\r\n                                   DatamartName AS TargetDatabase,\r\n                                   TargetTable,\r\n\t                               ServerName AS PumperRunOnServerName,\r\n\t                               DataUnitTimeStamp AS CheckDate,\r\n\t                               SourceLineCount AS LineToBePumped,\r\n\t                               CompletedLineCount AS LineReallyPumped,\r\n                                   ServerName AS Uploader,\r\n                                   Type As LastRunType,\r\n                                   Status AS LastRunStatus\r\n                            FROM [dbo].[PumperJobHistory] T1\r\n                            WHERE [UpdateTime] = (SELECT MAX(UpdateTime) FROM [dbo].[PumperJobHistory] T2 WHERE T1.TargetTable = T2.TargetTable) AND \r\n\t                        (([DataUnitTimeStamp] >= @StartTime) OR [DataUnitTimeStamp] IS NULL)  AND  \r\n                            (([Type] = 0 AND [Status] = 0 AND SourceLineCount != CompletedLineCount) OR\r\n                             ([Type] = 0 AND [Status] = 1) OR\r\n                             ([Type] = 0 AND [Status] = 16) OR\r\n                             ([Type] = 0 AND [Status] = 19) OR\r\n                             ([Type] = 1 AND [Status] = 1) OR\r\n                             ([Type] = 1 AND [Status] = 16))\r\n                            ";
			List<SqlParameter> list2 = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@StartTime", DateTime.Today.AddDays(-8.0));
			list2.Add(item);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = commandText;
					sqlCommand.CommandType = CommandType.Text;
					sqlConnection.Open();
					if (list2 != null)
					{
						foreach (SqlParameter value in list2)
						{
							sqlCommand.Parameters.Add(value);
						}
					}
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (sqlDataReader.Read())
						{
							list.Add(new RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness
							{
								TargetDBInstance = (sqlDataReader.IsDBNull(0) ? string.Empty : sqlDataReader.GetString(0)),
								TargetDatabase = (sqlDataReader.IsDBNull(1) ? string.Empty : sqlDataReader.GetString(1)),
								TargetTable = (sqlDataReader.IsDBNull(2) ? string.Empty : sqlDataReader.GetString(2)),
								PumperRunOnServerName = (sqlDataReader.IsDBNull(3) ? string.Empty : sqlDataReader.GetString(3)),
								CheckDate = (sqlDataReader.IsDBNull(4) ? SqlDateTime.MinValue.Value : sqlDataReader.GetDateTime(4)),
								LineToBePumped = (sqlDataReader.IsDBNull(5) ? 0L : sqlDataReader.GetInt64(5)),
								LineReallyPumped = (sqlDataReader.IsDBNull(6) ? 0L : sqlDataReader.GetInt64(6)),
								Uploader = (sqlDataReader.IsDBNull(7) ? string.Empty : sqlDataReader.GetString(7)),
								LastRunType = (sqlDataReader.IsDBNull(8) ? -1 : sqlDataReader.GetInt32(8)),
								LastRunStatus = (sqlDataReader.IsDBNull(9) ? -1 : sqlDataReader.GetInt32(9))
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000A0B90 File Offset: 0x0009ED90
		private List<RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness> GetValidCFRPumperCompletenessData()
		{
			string connectionString = string.Format("Data Source={0};Initial Catalog=cdm-tenantds;Integrated Security=SSPI;", base.Definition.SecondaryEndpoint);
			List<RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness> list = new List<RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness>();
			string commandText = "\r\n                            SELECT ServerName AS TargetDBInstance,\r\n                                   'cdm-tenantds' AS TargetDatabase,\r\n                                   TableName AS TargetTable,\r\n\t                               '' AS PumperRunOnServerName,\r\n\t                               DataSetTimeStamp AS CheckDate,\r\n\t                               0 AS LineToBePumped,\r\n\t                               0 AS LineReallyPumped,\r\n                                   '' AS Uploader\r\n                            FROM [dbo].[CFRInstrumentDataSummary]\r\n                            WHERE [DataSetTimeStamp] >= @StartTime AND [DataReadyTimeStamp] IS NOT NULL \r\n                            ORDER BY [DataReadyTimeStamp] DESC\r\n                            ";
			List<SqlParameter> list2 = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@StartTime", DateTime.Today.AddDays(-8.0));
			list2.Add(item);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = commandText;
					sqlCommand.CommandType = CommandType.Text;
					sqlConnection.Open();
					if (list2 != null)
					{
						foreach (SqlParameter value in list2)
						{
							sqlCommand.Parameters.Add(value);
						}
					}
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (sqlDataReader.Read())
						{
							list.Add(new RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness
							{
								TargetDBInstance = (sqlDataReader.IsDBNull(0) ? string.Empty : sqlDataReader.GetString(0)),
								TargetDatabase = (sqlDataReader.IsDBNull(1) ? string.Empty : sqlDataReader.GetString(1)),
								TargetTable = (sqlDataReader.IsDBNull(2) ? string.Empty : sqlDataReader.GetString(2)),
								PumperRunOnServerName = (sqlDataReader.IsDBNull(3) ? string.Empty : sqlDataReader.GetString(3)),
								CheckDate = (sqlDataReader.IsDBNull(4) ? SqlDateTime.MinValue.Value : sqlDataReader.GetDateTime(4)),
								LineToBePumped = (long)(sqlDataReader.IsDBNull(5) ? 0 : sqlDataReader.GetInt32(5)),
								LineReallyPumped = (long)(sqlDataReader.IsDBNull(6) ? 0 : sqlDataReader.GetInt32(6)),
								Uploader = (sqlDataReader.IsDBNull(7) ? string.Empty : sqlDataReader.GetString(7))
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000A0E1C File Offset: 0x0009F01C
		private void PurgeAllInvalidCompletenessData(SqlConnection azureConnection, int hourToKeep, string uploader)
		{
			if (azureConnection == null || azureConnection.State != ConnectionState.Open)
			{
				return;
			}
			string cmdText = "\r\n                        DELETE FROM [Database_InvalidCFRPumperJobCompleteness] where [RowUpdateTime] <= @CutOffDateTime AND [Uploader] = @Uploader;\r\n                            ";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@CutOffDateTime", DateTime.UtcNow.AddHours((double)(-(double)hourToKeep)));
			SqlParameter item2 = new SqlParameter("@Uploader", uploader);
			list.Add(item);
			list.Add(item2);
			using (SqlCommand sqlCommand = new SqlCommand(cmdText, azureConnection))
			{
				if (list != null)
				{
					foreach (SqlParameter value in list)
					{
						sqlCommand.Parameters.Add(value);
					}
				}
				sqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000A0EF8 File Offset: 0x0009F0F8
		private void WriteInvalidCompletenessDataToDB(SqlConnection azureConnection, RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness compData)
		{
			if (azureConnection == null || azureConnection.State != ConnectionState.Open)
			{
				return;
			}
			string cmdText = "\r\n                        IF EXISTS (select * from [Database_InvalidCFRPumperJobCompleteness] where [TargetDBInstance] = @TargetDBInstance and [TargetDatabase] = @TargetDatabase and [TargetTable] = @TargetTable and [CheckDate] = @CheckDate)\r\n                        BEGIN\r\n                            update [Database_InvalidCFRPumperJobCompleteness] \r\n\t                        set [LineToBePumped] = @LineToBePumped,\r\n\t\t                        [LineReallyPumped] = @LineReallyPumped,\r\n\t\t                        [RowUpdateTime] = GETUTCDATE(),\r\n                                [Uploader] = @Uploader\r\n\t                        where [TargetDBInstance] = @TargetDBInstance and [TargetDatabase] = @TargetDatabase and [TargetTable] = @TargetTable and [CheckDate] = @CheckDate;\r\n                        END\r\n                        ELSE\r\n                        BEGIN\r\n                            INSERT INTO [dbo].[Database_InvalidCFRPumperJobCompleteness]\r\n                                   ([TargetDBInstance]\r\n                                   ,[TargetDatabase]\r\n                                   ,[TargetTable]\r\n                                   ,[CheckDate]\r\n                                   ,[LineToBePumped]\r\n                                   ,[LineReallyPumped]\r\n                                   ,[RowCreatedDate]\r\n                                   ,[RowUpdateTime]\r\n                                   ,[PumperRunOnServerName]\r\n                                   ,[Uploader]\r\n                                   ,[LastRunStatus]\r\n                                   ,[LastRunType])\r\n                             VALUES\r\n                                   (@TargetDBInstance\r\n                                   ,@TargetDatabase\r\n                                   ,@TargetTable\r\n                                   ,@CheckDate\r\n                                   ,@LineToBePumped\r\n                                   ,@LineReallyPumped\r\n                                   ,@RowCreatedDate\r\n                                   ,@RowUpdateTime\r\n                                   ,@PumperRunOnServerName\r\n                                   ,@Uploader\r\n                                   ,@LastRunStatus\r\n                                   ,@LastRunType)\r\n                        END\r\n                            ";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@TargetDBInstance", compData.TargetDBInstance);
			SqlParameter item2 = new SqlParameter("@TargetDatabase", compData.TargetDatabase);
			SqlParameter item3 = new SqlParameter("@TargetTable", compData.TargetTable);
			SqlParameter item4 = new SqlParameter("@CheckDate", compData.CheckDate.ToShortDateString());
			SqlParameter item5 = new SqlParameter("@LineToBePumped", compData.LineToBePumped);
			SqlParameter item6 = new SqlParameter("@LineReallyPumped", compData.LineReallyPumped);
			SqlParameter item7 = new SqlParameter("@RowCreatedDate", compData.RowCreatedDate);
			SqlParameter item8 = new SqlParameter("@RowUpdateTime", compData.RowUpdateTime);
			SqlParameter item9 = new SqlParameter("@PumperRunOnServerName", compData.PumperRunOnServerName);
			SqlParameter item10 = new SqlParameter("@Uploader", Environment.MachineName);
			SqlParameter item11 = new SqlParameter("@LastRunStatus", compData.LastRunStatus);
			SqlParameter item12 = new SqlParameter("@LastRunType", compData.LastRunType);
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			list.Add(item4);
			list.Add(item5);
			list.Add(item6);
			list.Add(item7);
			list.Add(item8);
			list.Add(item9);
			list.Add(item10);
			list.Add(item11);
			list.Add(item12);
			using (SqlCommand sqlCommand = new SqlCommand(cmdText, azureConnection))
			{
				if (list != null)
				{
					foreach (SqlParameter value in list)
					{
						sqlCommand.Parameters.Add(value);
					}
				}
				sqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000A10F8 File Offset: 0x0009F2F8
		private void WriteValidCompletenessDataToDB(SqlConnection azureConnection, RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness compData)
		{
			if (azureConnection == null || azureConnection.State != ConnectionState.Open)
			{
				return;
			}
			string cmdText = "\r\n                        IF EXISTS (select * from [Database_ValidCFRPumperJobCompleteness] where [TargetDBInstance] = @TargetDBInstance and [TargetDatabase] = @TargetDatabase and [TargetTable] = @TargetTable and [CheckDate] = @CheckDate)\r\n                        BEGIN\r\n                            UPDATE [Database_ValidCFRPumperJobCompleteness] \r\n\t                        SET [LineToBePumped] = @LineToBePumped,\r\n\t\t                        [LineReallyPumped] = @LineReallyPumped,\r\n\t\t                        [RowUpdateTime] = GETUTCDATE(),\r\n                                [Uploader] = @Uploader\r\n\t                        WHERE [TargetDBInstance] = @TargetDBInstance and [TargetDatabase] = @TargetDatabase and [TargetTable] = @TargetTable and [CheckDate] = @CheckDate;\r\n                        END\r\n                        ELSE\r\n                        BEGIN\r\n                            INSERT INTO [dbo].[Database_ValidCFRPumperJobCompleteness]\r\n                                   ([TargetDBInstance]\r\n                                   ,[TargetDatabase]\r\n                                   ,[TargetTable]\r\n                                   ,[CheckDate]\r\n                                   ,[LineToBePumped]\r\n                                   ,[LineReallyPumped]\r\n                                   ,[RowCreatedDate]\r\n                                   ,[RowUpdateTime]\r\n                                   ,[PumperRunOnServerName]\r\n                                   ,[Uploader])\r\n                             VALUES\r\n                                   (@TargetDBInstance\r\n                                   ,@TargetDatabase\r\n                                   ,@TargetTable\r\n                                   ,@CheckDate\r\n                                   ,@LineToBePumped\r\n                                   ,@LineReallyPumped\r\n                                   ,@RowCreatedDate\r\n                                   ,@RowUpdateTime\r\n                                   ,@PumperRunOnServerName\r\n                                   ,@Uploader)\r\n                        END\r\n                            ";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@TargetDBInstance", compData.TargetDBInstance);
			SqlParameter item2 = new SqlParameter("@TargetDatabase", compData.TargetDatabase);
			SqlParameter item3 = new SqlParameter("@TargetTable", compData.TargetTable);
			SqlParameter item4 = new SqlParameter("@CheckDate", compData.CheckDate.ToShortDateString());
			SqlParameter item5 = new SqlParameter("@LineToBePumped", compData.LineToBePumped);
			SqlParameter item6 = new SqlParameter("@LineReallyPumped", compData.LineReallyPumped);
			SqlParameter item7 = new SqlParameter("@RowCreatedDate", compData.RowCreatedDate);
			SqlParameter item8 = new SqlParameter("@RowUpdateTime", compData.RowUpdateTime);
			SqlParameter item9 = new SqlParameter("@PumperRunOnServerName", compData.PumperRunOnServerName);
			SqlParameter item10 = new SqlParameter("@Uploader", Environment.MachineName);
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			list.Add(item4);
			list.Add(item5);
			list.Add(item6);
			list.Add(item7);
			list.Add(item8);
			list.Add(item9);
			list.Add(item10);
			using (SqlCommand sqlCommand = new SqlCommand(cmdText, azureConnection))
			{
				if (list != null)
				{
					foreach (SqlParameter value in list)
					{
						sqlCommand.Parameters.Add(value);
					}
				}
				sqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000A12B8 File Offset: 0x0009F4B8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			List<RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness> validCFRPumperCompletenessData = this.GetValidCFRPumperCompletenessData();
			List<RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness> invalidCFRPumperCompletenessData = this.GetInvalidCFRPumperCompletenessData();
			base.Result.StateAttribute12 = string.Format("Valid = {0}, Invalid = {1}", validCFRPumperCompletenessData.Count, invalidCFRPumperCompletenessData.Count);
			base.Result.StateAttribute11 = "Query pumper job history succeed";
			string account = base.Definition.Account;
			string arg = RwsCryptographyHelper.Decrypt(base.Definition.AccountPassword);
			string connectionString = string.Format(base.Definition.Endpoint, account, arg);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				base.Result.StateAttribute11 = "SQL connection opened, begin to write data";
				foreach (RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness cfrvalidPumperJobCompleteness in validCFRPumperCompletenessData)
				{
					RwsPumperRunHistoryProbe.CFRValidPumperJobCompleteness compData = cfrvalidPumperJobCompleteness;
					compData.RowCreatedDate = (compData.RowUpdateTime = DateTime.UtcNow);
					this.WriteValidCompletenessDataToDB(sqlConnection, compData);
				}
				foreach (RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness cfrinvalidPumperJobCompleteness in invalidCFRPumperCompletenessData)
				{
					RwsPumperRunHistoryProbe.CFRInvalidPumperJobCompleteness compData2 = cfrinvalidPumperJobCompleteness;
					compData2.RowCreatedDate = (compData2.RowUpdateTime = DateTime.UtcNow);
					this.WriteInvalidCompletenessDataToDB(sqlConnection, compData2);
				}
				base.Result.StateAttribute11 = "Write data succeed, begin purge invalid data";
				this.PurgeAllInvalidCompletenessData(sqlConnection, 1, Environment.MachineName);
			}
			base.Result.StateAttribute11 = "Collection Succeed";
		}

		// Token: 0x0200044B RID: 1099
		internal struct CFRValidPumperJobCompleteness
		{
			// Token: 0x04001306 RID: 4870
			internal string TargetDBInstance;

			// Token: 0x04001307 RID: 4871
			internal string TargetDatabase;

			// Token: 0x04001308 RID: 4872
			internal string TargetTable;

			// Token: 0x04001309 RID: 4873
			internal DateTime CheckDate;

			// Token: 0x0400130A RID: 4874
			internal long LineToBePumped;

			// Token: 0x0400130B RID: 4875
			internal long LineReallyPumped;

			// Token: 0x0400130C RID: 4876
			internal DateTime RowCreatedDate;

			// Token: 0x0400130D RID: 4877
			internal DateTime RowUpdateTime;

			// Token: 0x0400130E RID: 4878
			internal string PumperRunOnServerName;

			// Token: 0x0400130F RID: 4879
			internal string Uploader;
		}

		// Token: 0x0200044C RID: 1100
		internal struct CFRInvalidPumperJobCompleteness
		{
			// Token: 0x04001310 RID: 4880
			internal string TargetDBInstance;

			// Token: 0x04001311 RID: 4881
			internal string TargetDatabase;

			// Token: 0x04001312 RID: 4882
			internal string TargetTable;

			// Token: 0x04001313 RID: 4883
			internal DateTime CheckDate;

			// Token: 0x04001314 RID: 4884
			internal long LineToBePumped;

			// Token: 0x04001315 RID: 4885
			internal long LineReallyPumped;

			// Token: 0x04001316 RID: 4886
			internal DateTime RowCreatedDate;

			// Token: 0x04001317 RID: 4887
			internal DateTime RowUpdateTime;

			// Token: 0x04001318 RID: 4888
			internal string PumperRunOnServerName;

			// Token: 0x04001319 RID: 4889
			internal string Uploader;

			// Token: 0x0400131A RID: 4890
			internal int LastRunStatus;

			// Token: 0x0400131B RID: 4891
			internal int LastRunType;
		}
	}
}
