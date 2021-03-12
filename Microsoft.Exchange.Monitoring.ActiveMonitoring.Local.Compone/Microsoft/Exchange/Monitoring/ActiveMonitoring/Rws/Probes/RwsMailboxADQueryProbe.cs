using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes
{
	// Token: 0x02000443 RID: 1091
	public class RwsMailboxADQueryProbe : ProbeWorkItem
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x0009E198 File Offset: 0x0009C398
		private List<string> GetAvailableFEEndPointsByTenantName(string tenantName)
		{
			string arg = string.Empty;
			if (ExEnvironment.IsTest)
			{
				arg = string.Format("{0}dom.extest.microsoft.com", Environment.MachineName);
			}
			else
			{
				arg = "PRDMGT01.prod.exchangelabs.com";
			}
			SearchResultCollection searchResultCollection = null;
			using (DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("LDAP://{0}/RootDSE", arg)))
			{
				using (DirectoryEntry directoryEntry2 = new DirectoryEntry(string.Format("LDAP://{0}/{1}", arg, directoryEntry.Properties["configurationNamingContext"].Value)))
				{
					using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry2))
					{
						directorySearcher.PageSize = 1000;
						directorySearcher.PropertiesToLoad.Clear();
						directorySearcher.Filter = "(&(objectClass=msExchExchangeServer)(versionNumber>=1937801568)(msExchServerSite=*))";
						directorySearcher.PropertiesToLoad.Add("msexchcurrentserverroles");
						directorySearcher.PropertiesToLoad.Add("networkaddress");
						directorySearcher.PropertiesToLoad.Add("serialnumber");
						searchResultCollection = directorySearcher.FindAll();
					}
				}
			}
			List<string> list = new List<string>();
			foreach (object obj in searchResultCollection)
			{
				SearchResult searchResult = (SearchResult)obj;
				string text = string.Empty;
				object obj2 = searchResult.Properties["msexchcurrentserverroles"][0];
				foreach (object obj3 in searchResult.Properties["networkaddress"])
				{
					if (obj3.ToString().StartsWith("ncacn_ip_tcp"))
					{
						text = obj3.ToString().Substring(13);
						if (text.Contains("MF") && (int)obj2 == 16439)
						{
							list.Add(string.Format("http://{0}/PowerShell", text));
						}
					}
				}
			}
			string text2 = string.Empty;
			if (list != null && list.Count > 0 && list != null && list.Count > 0)
			{
				foreach (string text3 in list)
				{
					string value = string.Format("http://{0}", Environment.MachineName.ToLower().Substring(0, 3));
					if (text3.ToLower().StartsWith(value))
					{
						text2 = text3;
						break;
					}
				}
				if (string.IsNullOrEmpty(text2))
				{
					Random random = new Random();
					text2 = list[random.Next(0, list.Count - 1)];
				}
			}
			Uri uri = new Uri(text2);
			string shellUri = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";
			string text4 = string.Empty;
			if (RwsMailboxADQueryProbe.ManagementRPSSession == null)
			{
				Runspace runspace = RunspaceFactory.CreateRunspace(new WSManConnectionInfo(uri, shellUri, null)
				{
					AuthenticationMechanism = AuthenticationMechanism.Kerberos,
					OperationTimeout = 240000,
					OpenTimeout = 60000,
					SkipCNCheck = true,
					SkipCACheck = true,
					SkipRevocationCheck = true
				});
				runspace.Open();
				RwsMailboxADQueryProbe.ManagementRPSSession = runspace;
				base.Result.StateAttribute21 = string.Format("Management RPS Session created for tenant [{0}] ...", tenantName);
			}
			try
			{
				PowerShell powerShell = PowerShell.Create();
				powerShell.Runspace = RwsMailboxADQueryProbe.ManagementRPSSession;
				powerShell.AddCommand("Get-ManagementEndpoint");
				powerShell.AddParameter("DomainName", tenantName);
				Collection<PSObject> collection = powerShell.Invoke();
				foreach (PSObject psobject in collection)
				{
					text4 = psobject.Members["ResourceForest"].Value.ToString();
					if (!string.IsNullOrEmpty(text4))
					{
						break;
					}
				}
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute12 = ex.ToString();
			}
			return RwsMailboxADQueryProbe.GetAvailableFEEndPointsInForest(text4);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0009E5E4 File Offset: 0x0009C7E4
		private int GetMailboxCountForTenant(Runspace remoteRunspace, string tenantName)
		{
			if (remoteRunspace == null)
			{
				throw new ArgumentException("remoteRunspace could not be null");
			}
			int num = 0;
			try
			{
				PowerShell powerShell = PowerShell.Create();
				powerShell.Runspace = remoteRunspace;
				powerShell.AddCommand("Get-SyncMailbox");
				powerShell.AddParameter("Organization", tenantName);
				powerShell.AddParameter("ResultSize", 10000);
				Collection<PSObject> collection = powerShell.Invoke();
				num = collection.Count;
				foreach (PSObject psobject in collection)
				{
					string a = psobject.Members["RecipientTypeDetails"].Value.ToString();
					if (a == "DiscoveryMailbox")
					{
						num--;
					}
				}
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute14 = ex.ToString();
				throw ex;
			}
			return num;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0009E6D8 File Offset: 0x0009C8D8
		private X509Certificate2 FindCertificate(StoreLocation location, StoreName name, string findValue)
		{
			if (string.IsNullOrWhiteSpace(findValue))
			{
				throw new ArgumentException("Cannot be null or white-space characters.", "findValue");
			}
			X509Store x509Store = new X509Store(name, location);
			X509Certificate2 result;
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, findValue, true);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count != 1)
				{
					x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, findValue, true);
					if (x509Certificate2Collection == null || x509Certificate2Collection.Count != 1)
					{
						throw new Exception(string.Format("Unable to find a valid certificate by either thumbprint or subject DN '{0}', '{1}', '{2}'.", location, name, findValue));
					}
				}
				result = x509Certificate2Collection[0];
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0009E784 File Offset: 0x0009C984
		private int GetMailboxCountForTenant(string tenantName, string forest)
		{
			int num = 0;
			List<string> list = null;
			if (!string.IsNullOrEmpty(forest))
			{
				list = RwsMailboxADQueryProbe.GetAvailableFEEndPointsInForest(forest);
			}
			if (list == null || list.Count <= 0)
			{
				list = this.GetAvailableFEEndPointsByTenantName(tenantName);
			}
			string text = list[0].ToLower().Replace("http", "https");
			base.Result.StateAttribute11 = text;
			Uri uri = new Uri(text);
			string shellUri = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";
			X509Certificate2 x509Certificate = this.FindCertificate(StoreLocation.LocalMachine, StoreName.My, "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US");
			Runspace runspace = RunspaceFactory.CreateRunspace(new WSManConnectionInfo(uri, shellUri, x509Certificate.Thumbprint)
			{
				OperationTimeout = 240000,
				OpenTimeout = 60000,
				SkipCNCheck = true,
				SkipCACheck = true,
				SkipRevocationCheck = true
			});
			runspace.Open();
			bool flag = false;
			if (RwsMailboxADQueryProbe.RPSSessionPool == null)
			{
				RwsMailboxADQueryProbe.RPSSessionPool = new Dictionary<string, Runspace>();
			}
			if (!string.IsNullOrEmpty(forest) && !RwsMailboxADQueryProbe.RPSSessionPool.Keys.Contains(forest) && RwsMailboxADQueryProbe.RPSSessionPool.Keys.Count < 5)
			{
				RwsMailboxADQueryProbe.RPSSessionPool[forest] = runspace;
				flag = true;
			}
			try
			{
				PowerShell powerShell = PowerShell.Create();
				powerShell.Runspace = runspace;
				powerShell.AddCommand("Get-SyncMailbox");
				powerShell.AddParameter("Organization", tenantName);
				powerShell.AddParameter("ResultSize", 10000);
				Collection<PSObject> collection = powerShell.Invoke();
				num = collection.Count;
				foreach (PSObject psobject in collection)
				{
					string a = psobject.Members["RecipientTypeDetails"].Value.ToString();
					if (a == "DiscoveryMailbox")
					{
						num--;
					}
				}
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute11 = ex.ToString();
				throw ex;
			}
			finally
			{
				if (!flag)
				{
					runspace.Close();
					runspace.Dispose();
				}
			}
			return num;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0009E9A4 File Offset: 0x0009CBA4
		private static List<string> GetAvailableFEEndPointsInForest(string forest)
		{
			if (string.IsNullOrEmpty(forest))
			{
				throw new ArgumentException("forest cannot be null");
			}
			List<string> list = new List<string>();
			string text = forest;
			Environment.MachineName.Substring(0, 3).ToUpper();
			if (forest.ToUpper().Contains("EURPR01A00"))
			{
				text = "eurprd01.prod.exchangelabs.com";
			}
			else if (forest.ToUpper().Contains("NAMPR01A00"))
			{
				text = "prod.exchangelabs.com";
			}
			else if (forest.ToUpper().Contains("APCPR01A00"))
			{
				text = "apcprd01.prod.exchangelabs.com";
			}
			else if (forest.Contains("A0"))
			{
				int num = forest.IndexOf("PR");
				text = text.Substring(0, num + 2) + "D" + forest.Substring(num + 2, 2) + forest.Substring(forest.IndexOf("A0") + 4);
			}
			SearchResultCollection searchResultCollection = null;
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("LDAP://{0}/RootDSE", text)))
				{
					using (new DirectoryEntry("LDAP://" + directoryEntry.Properties["configurationNamingContext"].Value))
					{
						using (DirectoryEntry directoryEntry3 = new DirectoryEntry(string.Format("LDAP://{0}/{1}", text, directoryEntry.Properties["configurationNamingContext"].Value)))
						{
							using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry3))
							{
								directorySearcher.PageSize = 1000;
								directorySearcher.PropertiesToLoad.Clear();
								directorySearcher.Filter = "(&(objectClass=msExchExchangeServer)(versionNumber>=1937801568)(msExchServerSite=*))";
								directorySearcher.PropertiesToLoad.Add("msexchcurrentserverroles");
								directorySearcher.PropertiesToLoad.Add("networkaddress");
								directorySearcher.PropertiesToLoad.Add("serialnumber");
								searchResultCollection = directorySearcher.FindAll();
							}
						}
					}
				}
			}
			catch
			{
				return list;
			}
			foreach (object obj in searchResultCollection)
			{
				SearchResult searchResult = (SearchResult)obj;
				string text2 = string.Empty;
				object obj2 = searchResult.Properties["msexchcurrentserverroles"][0];
				foreach (object obj3 in searchResult.Properties["networkaddress"])
				{
					if (obj3.ToString().StartsWith("ncacn_ip_tcp"))
					{
						text2 = obj3.ToString().Substring(13);
						if (text2.Contains("CA") && (int)obj2 == 16385)
						{
							list.Add(string.Format("http://{0}/PowerShell", text2));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0009ED2C File Offset: 0x0009CF2C
		private static void DumpADQueryObjectToFile(RwsMailboxADQueryProbe.ADQueryFileObject adQueryResult, string outputFilePath)
		{
			if (string.IsNullOrEmpty(outputFilePath))
			{
				throw new ArgumentException("outputFilePath cannot be null");
			}
			if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
			}
			string value = string.Format(CultureInfo.InvariantCulture, "\"{0}\",\"{1}\",\"{2}\",\"{3}\"", new object[]
			{
				adQueryResult.TargetDate,
				adQueryResult.TenantName,
				adQueryResult.ADQueryCount,
				adQueryResult.ADQueryTimeUtc
			});
			using (StreamWriter streamWriter = new StreamWriter(outputFilePath, true))
			{
				streamWriter.WriteLine(value);
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0009EE18 File Offset: 0x0009D018
		private static void MoveADQueryFileToUploadFolder(string sourcePath, string targetPath)
		{
			if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(targetPath))
			{
				throw new ArgumentException("sourcePath and targetPath cannot be null");
			}
			if (!Directory.Exists(sourcePath))
			{
				return;
			}
			List<FileInfo> list = (from f in Directory.GetFiles(sourcePath)
			select new FileInfo(f) into f
			where f.LastAccessTimeUtc >= DateTime.UtcNow.AddHours(-6.0)
			select f).ToList<FileInfo>();
			foreach (FileInfo fileInfo in list)
			{
				string text = Path.Combine(sourcePath, fileInfo.Name);
				string text2 = Path.Combine(targetPath, fileInfo.Name);
				if (File.Exists(text2))
				{
					File.Delete(text);
				}
				else
				{
					if (!Directory.Exists(targetPath))
					{
						Directory.CreateDirectory(targetPath);
					}
					File.Copy(text, text2);
					File.Delete(text);
				}
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0009EF1C File Offset: 0x0009D11C
		private int NeedPerformADQuery(DateTime targetDate)
		{
			int result = 2;
			string account = base.Definition.Account;
			string arg = RwsCryptographyHelper.Decrypt(base.Definition.AccountPassword);
			string connectionString = string.Format(base.Definition.Endpoint, account, arg);
			string commandText = "\r\n                            SELECT [TargetDate]\r\n                                  ,[SuspiciousTenantCount]\r\n                                  ,[TotalTenantCount]\r\n                                  ,[Threshold]\r\n                              FROM [CFRDQ_Accuracy_MailboxUsage_SuspiciousTenantStatistics]\r\n                              WHERE [TargetDate] >= @StartTime AND [TargetDate] <= @EndTime;\r\n                            ";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@StartTime", targetDate.AddDays(-2.0).ToShortDateString());
			SqlParameter item2 = new SqlParameter("@EndTime", targetDate.ToShortDateString());
			list.Add(item);
			list.Add(item2);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = commandText;
					sqlCommand.CommandType = CommandType.Text;
					sqlConnection.Open();
					RwsMailboxADQueryProbe.needToQueryTargetDateList = new List<DateTime>();
					if (list != null)
					{
						foreach (SqlParameter value in list)
						{
							sqlCommand.Parameters.Add(value);
						}
					}
					bool flag = false;
					bool flag2 = false;
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (sqlDataReader.Read())
						{
							DateTime dateTime = sqlDataReader.IsDBNull(0) ? DateTime.MinValue : sqlDataReader.GetDateTime(0);
							int num = sqlDataReader.IsDBNull(1) ? -1 : sqlDataReader.GetInt32(1);
							int num2 = sqlDataReader.IsDBNull(3) ? 0 : sqlDataReader.GetInt32(3);
							flag2 = true;
							if (num <= num2)
							{
								if (dateTime != DateTime.MinValue && !RwsMailboxADQueryProbe.needToQueryTargetDateList.Contains(dateTime))
								{
									RwsMailboxADQueryProbe.needToQueryTargetDateList.Add(dateTime);
								}
								flag = true;
							}
						}
						if (flag2)
						{
							result = (flag ? 1 : 0);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0009F160 File Offset: 0x0009D360
		private void MarkSuspiciousTenantHandleAsFailed(DateTime targetDate, string tenantName)
		{
			string account = base.Definition.Account;
			string arg = RwsCryptographyHelper.Decrypt(base.Definition.AccountPassword);
			string connectionString = string.Format(base.Definition.Endpoint, account, arg);
			string cmdText = "\r\n                        UPDATE [CFRDQ_Accuracy_MailboxUsage_SuspiciousTenantListLog]\r\n                           SET [Status] = 2\r\n                              ,[ProcessEndTimeUTC] = GETUTCDATE()\r\n                              ,[ProcessOwner] = @ProcessOwner\r\n                         WHERE [TargetDate] = @TargetDate AND [TenantName] = @TenantName;\r\n                            ";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@ProcessOwner", Environment.MachineName);
			SqlParameter item2 = new SqlParameter("@TargetDate", targetDate.ToShortDateString());
			SqlParameter item3 = new SqlParameter("@TenantName", tenantName);
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
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
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0009F29C File Offset: 0x0009D49C
		private void MarkSuspiciousTenantHandleAsDone(DateTime targetDate, string tenantName)
		{
			string account = base.Definition.Account;
			string arg = RwsCryptographyHelper.Decrypt(base.Definition.AccountPassword);
			string connectionString = string.Format(base.Definition.Endpoint, account, arg);
			string cmdText = "\r\n                        UPDATE [CFRDQ_Accuracy_MailboxUsage_SuspiciousTenantListLog]\r\n                           SET [Status] = 1\r\n                              ,[ProcessEndTimeUTC] = GETUTCDATE()\r\n                              ,[ProcessOwner] = @ProcessOwner\r\n                         WHERE [TargetDate] = @TargetDate AND [TenantName] = @TenantName;\r\n                            ";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@ProcessOwner", Environment.MachineName);
			SqlParameter item2 = new SqlParameter("@TargetDate", targetDate.ToShortDateString());
			SqlParameter item3 = new SqlParameter("@TenantName", tenantName);
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
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
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0009F3D8 File Offset: 0x0009D5D8
		private List<RwsMailboxADQueryProbe.SuspicousTenant> GetSuspicousTenantListToHandle(DateTime targetDate)
		{
			List<RwsMailboxADQueryProbe.SuspicousTenant> list = new List<RwsMailboxADQueryProbe.SuspicousTenant>();
			string account = base.Definition.Account;
			string arg = RwsCryptographyHelper.Decrypt(base.Definition.AccountPassword);
			string connectionString = string.Format(base.Definition.Endpoint, account, arg);
			string empty = string.Empty;
			if (RwsMailboxADQueryProbe.needToQueryTargetDateList == null || RwsMailboxADQueryProbe.needToQueryTargetDateList.Count <= 0)
			{
				return list;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DateTime dateTime in RwsMailboxADQueryProbe.needToQueryTargetDateList)
			{
				stringBuilder.Append(string.Format("'{0}',", dateTime.ToShortDateString()));
			}
			string format = "declare @SuspiciousTenantListLog table(\r\n\t                                    TargetDate datetime null,\r\n\t                                    TenantGuid nvarchar(128) null,\r\n\t                                    TenantName nvarchar(128) null,\r\n\t                                    Forest nvarchar(128) null,\r\n\t                                    [Status] int null,\r\n\t                                    [ProcessStartTimeUTC] datetime null,\r\n\t                                    [ProcessEndTimeUTC] datetime null,\r\n\t                                    [ProcessOwner] nvarchar(100),\r\n                                        [MailboxCountToday] int\r\n                                    );\r\n\r\n                                    insert into [CFRDQ_Accuracy_MailboxUsage_SuspiciousTenantListLog]\r\n                                    OUTPUT inserted.[TargetDate],inserted.[TenantGuid],inserted.[TenantName],inserted.[Forest],inserted.[Status],inserted.[ProcessStartTimeUTC],inserted.[ProcessEndTimeUTC],inserted.[ProcessOwner],inserted.[MailboxCountToday]\r\n                                    into @SuspiciousTenantListLog\r\n                                    SELECT TOP 150 [TargetDate]\r\n                                            ,[TenantGuid]\r\n                                            ,[TenantName]\r\n                                            ,[Forest]\r\n\t                                        , 0 AS [Status]\r\n\t                                        ,GETUTCDATE() AS [ProcessStartTimeUTC]\r\n\t                                        ,null AS [ProcessEndTimeUTC]\r\n\t                                        ,'{0}' AS [ProcessOwner]\r\n                                            ,TOTALMAILBOXCOUNTTODAY AS [MailboxCountToday]\r\n                                    FROM [dbo].[CFRDQ_Accuracy_MailboxUsage_SuspiciousTenantList] T1 with (readpast, updlock)\r\n                                    WHERE [TargetDate] IN ({1}) AND [TenantName] NOT IN \r\n                                    (\r\n                                    SELECT [TenantName] FROM \r\n                                    [dbo].[CFRDQ_Accuracy_MailboxUsage_SuspiciousTenantListLog] T2\r\n                                    WHERE [TargetDate] IN ({1}) AND T1.[TargetDate] = T2.[TargetDate] AND (([Status] = 2 AND DATEADD(minute, 240, [ProcessStartTimeUTC]) > GETUTCDATE()) OR [Status] = 1 OR ([Status] = 0 AND DATEADD(minute, 10, [ProcessStartTimeUTC]) > GETUTCDATE()))\r\n                                    )\r\n                                    ORDER BY [TargetDate] DESC, [Forest] DESC, [TOTALMAILBOXCOUNTTODAY] ASC;\r\n\r\n                                    select *\r\n                                    from @SuspiciousTenantListLog;\r\n                                    ";
			string commandText = string.Format(format, Environment.MachineName, stringBuilder.ToString().Trim(new char[]
			{
				','
			}));
			List<SqlParameter> list2 = new List<SqlParameter>();
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
							list.Add(new RwsMailboxADQueryProbe.SuspicousTenant
							{
								TargetDate = (sqlDataReader.IsDBNull(0) ? SqlDateTime.MinValue.Value : sqlDataReader.GetDateTime(0)),
								TenantGuid = (sqlDataReader.IsDBNull(1) ? string.Empty : sqlDataReader.GetString(1)),
								TenantName = (sqlDataReader.IsDBNull(2) ? string.Empty : sqlDataReader.GetString(2)),
								Forest = (sqlDataReader.IsDBNull(3) ? string.Empty : sqlDataReader.GetString(3)),
								Status = (sqlDataReader.IsDBNull(4) ? 3 : sqlDataReader.GetInt32(4)),
								ProcessStartTimeUTC = (sqlDataReader.IsDBNull(5) ? SqlDateTime.MinValue.Value : sqlDataReader.GetDateTime(5)),
								ProcessEndTimeUTC = (sqlDataReader.IsDBNull(6) ? SqlDateTime.MinValue.Value : sqlDataReader.GetDateTime(6)),
								ProcessOwner = (sqlDataReader.IsDBNull(7) ? string.Empty : sqlDataReader.GetString(7)),
								MailboxCountToday = (sqlDataReader.IsDBNull(8) ? -1 : sqlDataReader.GetInt32(8))
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0009F940 File Offset: 0x0009DB40
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DateTime utcNow = DateTime.UtcNow;
			string outputFileFullPath = Path.Combine("D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot_Temp", string.Format(CultureInfo.InvariantCulture, "ADQuerySnapshot_{0}.log", new object[]
			{
				utcNow.Ticks
			}));
			RwsMailboxADQueryProbe.MoveADQueryFileToUploadFolder("D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot_Temp", "D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot");
			int num = 0;
			try
			{
				num = this.NeedPerformADQuery(utcNow);
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute13 = ex.ToString();
				return;
			}
			if (num == 0)
			{
				base.Result.StateAttribute13 = string.Format("{0}, Suspicious Tenant List count exceed threshold, bypass AD query ...", DateTime.UtcNow);
			}
			else
			{
				if (num == 2)
				{
					base.Result.StateAttribute13 = string.Format("{0}, Suspicious Tenant List count and threshold not found, bypass AD query ...", DateTime.UtcNow);
					return;
				}
				List<RwsMailboxADQueryProbe.SuspicousTenant> suspicousTenantListToHandle = this.GetSuspicousTenantListToHandle(utcNow);
				if (suspicousTenantListToHandle == null || suspicousTenantListToHandle.Count <= 0)
				{
					base.Result.StateAttribute13 = string.Format("{0}, No suspicious tenant received, bypass the check ...", DateTime.UtcNow);
					return;
				}
				base.Result.StateAttribute17 = (double)suspicousTenantListToHandle.Count;
				try
				{
					using (List<RwsMailboxADQueryProbe.SuspicousTenant>.Enumerator enumerator = suspicousTenantListToHandle.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							RwsMailboxADQueryProbe.<>c__DisplayClass7 CS$<>8__locals2 = new RwsMailboxADQueryProbe.<>c__DisplayClass7();
							CS$<>8__locals2.tenant = enumerator.Current;
							RwsMailboxADQueryProbe.ADQueryFileObject adQueryResult = default(RwsMailboxADQueryProbe.ADQueryFileObject);
							adQueryResult.TargetDate = CS$<>8__locals2.tenant.TargetDate;
							adQueryResult.TenantName = CS$<>8__locals2.tenant.TenantName;
							adQueryResult.ADQueryTimeUtc = DateTime.UtcNow;
							bool blockActionComplete = false;
							base.Result.StateAttribute16 = 1.0;
							Task.Factory.StartNew(delegate()
							{
								bool flag = true;
								if (CS$<>8__locals2.tenant.MailboxCountToday > 10000)
								{
									adQueryResult.ADQueryCount = -2;
								}
								else
								{
									try
									{
										bool flag2 = false;
										if (RwsMailboxADQueryProbe.RPSSessionPool != null && RwsMailboxADQueryProbe.RPSSessionPool.Keys.Contains(CS$<>8__locals2.tenant.Forest))
										{
											try
											{
												adQueryResult.ADQueryCount = this.GetMailboxCountForTenant(RwsMailboxADQueryProbe.RPSSessionPool[CS$<>8__locals2.tenant.Forest], CS$<>8__locals2.tenant.TenantName);
												flag2 = true;
											}
											catch
											{
												flag2 = false;
											}
										}
										if (!flag2)
										{
											adQueryResult.ADQueryCount = this.GetMailboxCountForTenant(CS$<>8__locals2.tenant.TenantName, CS$<>8__locals2.tenant.Forest);
										}
									}
									catch (Exception ex2)
									{
										flag = false;
										adQueryResult.ADQueryCount = -1;
										this.Result.StateAttribute22 = ex2.ToString();
									}
								}
								if (flag)
								{
									ProbeResult result = this.Result;
									result.StateAttribute5 += string.Format("[{0}]", CS$<>8__locals2.tenant.TenantName);
									this.MarkSuspiciousTenantHandleAsDone(CS$<>8__locals2.tenant.TargetDate, CS$<>8__locals2.tenant.TenantName);
									RwsMailboxADQueryProbe.DumpADQueryObjectToFile(adQueryResult, outputFileFullPath);
								}
								else
								{
									this.MarkSuspiciousTenantHandleAsFailed(CS$<>8__locals2.tenant.TargetDate, CS$<>8__locals2.tenant.TenantName);
								}
								blockActionComplete = true;
							}, cancellationToken);
							while (!blockActionComplete)
							{
								Thread.Sleep(200);
								cancellationToken.ThrowIfCancellationRequested();
							}
						}
					}
				}
				finally
				{
					int num2 = 0;
					if (RwsMailboxADQueryProbe.RPSSessionPool != null)
					{
						foreach (string key in RwsMailboxADQueryProbe.RPSSessionPool.Keys)
						{
							RwsMailboxADQueryProbe.RPSSessionPool[key].Close();
							num2++;
						}
						RwsMailboxADQueryProbe.RPSSessionPool.Clear();
					}
					if (RwsMailboxADQueryProbe.ManagementRPSSession != null)
					{
						RwsMailboxADQueryProbe.ManagementRPSSession.Close();
						RwsMailboxADQueryProbe.ManagementRPSSession.Dispose();
						RwsMailboxADQueryProbe.ManagementRPSSession = null;
					}
					base.Result.StateAttribute15 = string.Format("Release session pool succeed (session number = {0})...", num2);
				}
				RwsMailboxADQueryProbe.MoveADQueryFileToUploadFolder("D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot_Temp", "D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot");
				base.Result.StateAttribute13 = "Succeed";
				return;
			}
		}

		// Token: 0x040012E2 RID: 4834
		private const string ADQueryOutputFilePath = "D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot_Temp";

		// Token: 0x040012E3 RID: 4835
		private const string ADQueryUploadFilePath = "D:\\Datamining\\Uploader\\CFRDataAccuracy\\TenantMailboxCountADSnapshot";

		// Token: 0x040012E4 RID: 4836
		private static Dictionary<string, Runspace> RPSSessionPool;

		// Token: 0x040012E5 RID: 4837
		private static Runspace ManagementRPSSession;

		// Token: 0x040012E6 RID: 4838
		private static List<DateTime> needToQueryTargetDateList;

		// Token: 0x02000444 RID: 1092
		public enum ProcessStatus
		{
			// Token: 0x040012EA RID: 4842
			Processing,
			// Token: 0x040012EB RID: 4843
			Processed,
			// Token: 0x040012EC RID: 4844
			Failed,
			// Token: 0x040012ED RID: 4845
			Unknown
		}

		// Token: 0x02000445 RID: 1093
		internal struct SuspicousTenant
		{
			// Token: 0x040012EE RID: 4846
			internal DateTime TargetDate;

			// Token: 0x040012EF RID: 4847
			internal string TenantGuid;

			// Token: 0x040012F0 RID: 4848
			internal string TenantName;

			// Token: 0x040012F1 RID: 4849
			internal string Forest;

			// Token: 0x040012F2 RID: 4850
			internal int Status;

			// Token: 0x040012F3 RID: 4851
			internal DateTime ProcessStartTimeUTC;

			// Token: 0x040012F4 RID: 4852
			internal DateTime ProcessEndTimeUTC;

			// Token: 0x040012F5 RID: 4853
			internal string ProcessOwner;

			// Token: 0x040012F6 RID: 4854
			internal int MailboxCountToday;
		}

		// Token: 0x02000446 RID: 1094
		internal struct ADQueryFileObject
		{
			// Token: 0x040012F7 RID: 4855
			internal DateTime TargetDate;

			// Token: 0x040012F8 RID: 4856
			internal string TenantName;

			// Token: 0x040012F9 RID: 4857
			internal int ADQueryCount;

			// Token: 0x040012FA RID: 4858
			internal DateTime ADQueryTimeUtc;
		}
	}
}
