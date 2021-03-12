using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TenantDataCollectorScheduler : CacheScheduler
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00006CAC File Offset: 0x00004EAC
		internal TenantDataCollectorScheduler(AnchorContext context, IOrganizationOperation orgOperationProxyInstance, ISymphonyProxy symphonyProxyInstance, WaitHandle stopEvent) : base(context, stopEvent)
		{
			this.ForestName = NativeHelpers.GetForestName().Split(new char[]
			{
				'.'
			})[0];
			this.orgOperationProxy = orgOperationProxyInstance;
			if (orgOperationProxyInstance is OrgOperationProxy)
			{
				((OrgOperationProxy)this.orgOperationProxy).Context = context;
			}
			this.symphonyProxy = symphonyProxyInstance;
			if (symphonyProxyInstance is SymphonyProxy)
			{
				((SymphonyProxy)this.symphonyProxy).WorkloadUri = new Uri(context.Config.GetConfig<Uri>("WebServiceUri"), "WorkloadService.svc");
				((SymphonyProxy)this.symphonyProxy).Cert = CertificateHelper.GetExchangeCertificate(context.Config.GetConfig<string>("CertificateSubject"));
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00006D5F File Offset: 0x00004F5F
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00006D67 File Offset: 0x00004F67
		private string ForestName { get; set; }

		// Token: 0x0600045E RID: 1118 RVA: 0x00006D70 File Offset: 0x00004F70
		public override XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return base.GetDiagnosticInfo(parameters);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00006D88 File Offset: 0x00004F88
		protected override AnchorJobProcessorResult ProcessEntry(CacheEntryBase cacheEntry)
		{
			Dictionary<string, TenantData> tenantDataDictionary = new Dictionary<string, TenantData>();
			Hashtable idsToNames = new Hashtable();
			try
			{
				this.AddOrgInformation(tenantDataDictionary, idsToNames);
				this.GetTenantSizeData(cacheEntry, tenantDataDictionary, idsToNames);
				this.UploadData(tenantDataDictionary);
				this.ValidateAndLog(tenantDataDictionary);
			}
			catch (Exception ex)
			{
				base.Context.Logger.Log(MigrationEventType.Error, "Unhandled exception occured, rethrowing to Anchor Service. {0}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex, true)
				});
				throw ex;
			}
			return AnchorJobProcessorResult.Working;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00006E04 File Offset: 0x00005004
		private void AddOrgInformation(Dictionary<string, TenantData> tenantDataDictionary, Hashtable idsToNames)
		{
			List<TenantOrganizationPresentationObjectWrapper> list = null;
			try
			{
				list = this.orgOperationProxy.GetAllOrganizations(base.Context.Config.GetConfig<bool>("CheckAllAccountPartitions"));
			}
			catch (MigrationPermanentException ex)
			{
				base.Context.Logger.Log(MigrationEventType.Error, "Get-Organization failed with error: {0}", new object[]
				{
					ex
				});
			}
			if (list == null)
			{
				base.Context.Logger.Log(MigrationEventType.Error, "Get-Organization returned null, skipping processing of org data", new object[0]);
				return;
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Get-Organization returned {0} tenants", new object[]
			{
				list.Count
			});
			int num = 0;
			foreach (TenantOrganizationPresentationObjectWrapper tenantOrganizationPresentationObjectWrapper in list)
			{
				if (tenantOrganizationPresentationObjectWrapper.AdminDisplayVersion.ExchangeBuild.Major == ExchangeObjectVersion.Exchange2012.ExchangeBuild.Major || (tenantOrganizationPresentationObjectWrapper.AdminDisplayVersion.ExchangeBuild.Major == ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major && tenantOrganizationPresentationObjectWrapper.AdminDisplayVersion.ExchangeBuild.Minor == 16))
				{
					if (!tenantDataDictionary.ContainsKey(tenantOrganizationPresentationObjectWrapper.Name))
					{
						tenantDataDictionary.Add(tenantOrganizationPresentationObjectWrapper.Name, new TenantData(tenantOrganizationPresentationObjectWrapper.Name));
					}
					else
					{
						base.Context.Logger.Log(MigrationEventType.Error, "Get-Organization returned multiple orgs with name {0}", new object[]
						{
							tenantOrganizationPresentationObjectWrapper.Name
						});
					}
					if (!string.IsNullOrWhiteSpace(tenantOrganizationPresentationObjectWrapper.ExternalDirectoryOrganizationId) && !tenantOrganizationPresentationObjectWrapper.ExternalDirectoryOrganizationId.Equals(TenantDataCollectorScheduler.emptyGuid) && !idsToNames.ContainsKey(tenantOrganizationPresentationObjectWrapper.ExternalDirectoryOrganizationId))
					{
						idsToNames.Add(tenantOrganizationPresentationObjectWrapper.ExternalDirectoryOrganizationId, tenantOrganizationPresentationObjectWrapper.Name);
					}
					else if (idsToNames.ContainsKey(tenantOrganizationPresentationObjectWrapper.ExternalDirectoryOrganizationId))
					{
						base.Context.Logger.Log(MigrationEventType.Error, "Get-Organization returned multiple orgs with ExternalDirectoryOrganizationId {0}", new object[]
						{
							tenantOrganizationPresentationObjectWrapper.ExternalDirectoryOrganizationId
						});
					}
					tenantDataDictionary[tenantOrganizationPresentationObjectWrapper.Name].UpdateFromTenant(tenantOrganizationPresentationObjectWrapper);
				}
				else
				{
					num++;
				}
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Found {0} non R6 or E15 tenants", new object[]
			{
				num
			});
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000070C4 File Offset: 0x000052C4
		private void GetTenantSizeData(CacheEntryBase cacheEntry, Dictionary<string, TenantData> tenantDataDictionary, Hashtable idsToNames)
		{
			base.Context.Logger.Log(MigrationEventType.Information, "Processing E14 Servers", new object[0]);
			this.CatchExceptions(delegate
			{
				this.GetVersionSizeData(cacheEntry, tenantDataDictionary, idsToNames, true);
			}, "Error Finding MailboxServers");
			base.Context.Logger.Log(MigrationEventType.Information, "Processing E15 Servers", new object[0]);
			this.CatchExceptions(delegate
			{
				this.GetVersionSizeData(cacheEntry, tenantDataDictionary, idsToNames, false);
			}, "Error Finding MailboxServers");
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000715C File Offset: 0x0000535C
		private void CatchExceptions(Action actionDelegate, string logString)
		{
			try
			{
				actionDelegate();
			}
			catch (Exception ex)
			{
				if (!(ex is MigrationTransientException) && !(ex is MigrationPermanentException))
				{
					throw;
				}
				base.Context.Logger.Log(MigrationEventType.Error, "{0}: {1}", new object[]
				{
					logString,
					ex
				});
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000724C File Offset: 0x0000544C
		private void GetVersionSizeData(CacheEntryBase cacheEntry, Dictionary<string, TenantData> tenantDataDictionary, Hashtable idsToNames, bool isE14)
		{
			Dictionary<string, FileInfo> files = new Dictionary<string, FileInfo>();
			HashSet<string> databaseNames = new HashSet<string>();
			List<Server> list;
			if (isE14)
			{
				list = CommonUtils.GetMailboxServer(((AnchorADProvider)cacheEntry.ADProvider).ConfigurationSession, Server.E14SP1MinVersion, Server.E15MinVersion).ToList<Server>();
			}
			else
			{
				list = CommonUtils.GetMailboxServer(((AnchorADProvider)cacheEntry.ADProvider).ConfigurationSession, Server.E15MinVersion).ToList<Server>();
			}
			if (list.Count == 0)
			{
				base.Context.Logger.Log(MigrationEventType.Error, "Couldn't find any MBX servers to collect data from", new object[0]);
				return;
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Found {0} servers", new object[]
			{
				list.Count
			});
			using (List<Server>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Server server = enumerator.Current;
					this.CatchExceptions(delegate
					{
						this.QuerySingleServer(server, files, databaseNames, isE14);
					}, string.Format("Error Querying server {0}", server.Name));
				}
			}
			files = this.ValidateDatabases(files, databaseNames, isE14);
			using (Dictionary<string, FileInfo>.ValueCollection.Enumerator enumerator2 = files.Values.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					FileInfo file = enumerator2.Current;
					this.CatchExceptions(delegate
					{
						this.ProcessFile(file, tenantDataDictionary, idsToNames, isE14);
					}, string.Format("Error opening and reading file {0}", file.FullName));
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00007464 File Offset: 0x00005664
		private void QuerySingleServer(Server server, Dictionary<string, FileInfo> files, HashSet<string> databaseNames, bool isE14)
		{
			try
			{
				foreach (ADObjectId adobjectId in server.Databases)
				{
					base.Context.Logger.Log(MigrationEventType.Verbose, "Found Database: {0} on Server: {1}", new object[]
					{
						adobjectId.Name,
						server.Name
					});
					databaseNames.Add(adobjectId.Name);
				}
			}
			catch (ADTransientException innerException)
			{
				throw new ErrorGettingDatabasesException(server.Name, innerException);
			}
			string text = Path.Combine("\\\\", server.Name, base.Context.Config.GetConfig<string>(isE14 ? "E14DataDirectory" : "E15DataDirectory"));
			base.Context.Logger.Log(MigrationEventType.Information, "Working on server {0}", new object[]
			{
				server.Name
			});
			base.Context.Logger.Log(MigrationEventType.Verbose, "Looking in Path {0} for files", new object[]
			{
				text
			});
			if (!Directory.Exists(text))
			{
				base.Context.Logger.Log(MigrationEventType.Error, "Directory {0} could not be found on server {1}", new object[]
				{
					text,
					server.Name
				});
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			FileInfo[] files2;
			try
			{
				files2 = directoryInfo.GetFiles("*.tenant", SearchOption.AllDirectories);
			}
			catch (IOException innerException2)
			{
				throw new ErrorGettingFilesException(directoryInfo.Name, innerException2);
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Found {0} Files on {1}", new object[]
			{
				files2.Length,
				server.Name
			});
			foreach (FileInfo fileInfo in files2)
			{
				string key = fileInfo.Name.Split(new char[]
				{
					'_'
				})[0];
				if (files.ContainsKey(key))
				{
					if (fileInfo.LastWriteTime > files[key].LastWriteTime)
					{
						base.Context.Logger.Log(MigrationEventType.Verbose, "Replacing {0} with newer version {1}", new object[]
						{
							files[key].FullName,
							fileInfo.FullName
						});
						files[key] = fileInfo;
					}
				}
				else
				{
					base.Context.Logger.Log(MigrationEventType.Verbose, "Adding {0} to list of filepaths", new object[]
					{
						fileInfo.FullName
					});
					files.Add(key, fileInfo);
				}
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000076FC File Offset: 0x000058FC
		private Dictionary<string, FileInfo> ValidateDatabases(Dictionary<string, FileInfo> files, HashSet<string> databaseNames, bool isE14)
		{
			base.Context.Logger.Log(MigrationEventType.Information, "Found {0} {1} Mailbox Databases", new object[]
			{
				databaseNames.Count,
				isE14 ? "E14" : "E15"
			});
			int num = 0;
			Dictionary<string, FileInfo> dictionary = new Dictionary<string, FileInfo>();
			foreach (KeyValuePair<string, FileInfo> keyValuePair in files)
			{
				if (databaseNames.Contains(keyValuePair.Key))
				{
					base.Context.Logger.Log(MigrationEventType.Verbose, "Database {0} found", new object[]
					{
						keyValuePair.Key
					});
					databaseNames.Remove(keyValuePair.Key);
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					base.Context.Logger.Log(MigrationEventType.Error, "No Database found for file {0}", new object[]
					{
						keyValuePair.Key
					});
					num++;
				}
			}
			foreach (string text in databaseNames)
			{
				base.Context.Logger.Log(MigrationEventType.Error, "No .tenant file found for database {0}", new object[]
				{
					text
				});
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Found {0} {1} files with a corresponding {1} database", new object[]
			{
				files.Count<KeyValuePair<string, FileInfo>>(),
				isE14 ? "E14" : "E15"
			});
			base.Context.Logger.Log(MigrationEventType.Information, "Found {0} {2} files with no database and {1} {2}databases with no corresponding file", new object[]
			{
				num,
				databaseNames.Count,
				isE14 ? "E14" : "E15"
			});
			return dictionary;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000790C File Offset: 0x00005B0C
		private void ProcessFile(FileInfo file, Dictionary<string, TenantData> tenantDataDictionary, Hashtable idsToNames, bool isE14)
		{
			base.Context.Logger.Log(MigrationEventType.Verbose, "Processing file {0}", new object[]
			{
				file.FullName
			});
			IEnumerable<CsvRow> enumerable;
			try
			{
				FileStream sourceStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
				enumerable = TenantDataCsvSchema.TenantDataCsvSchemaInstance.Read(sourceStream, null, false, false);
			}
			catch (IOException innerException)
			{
				throw new ErrorReadingFileException(file.FullName, innerException);
			}
			bool flag = true;
			int num = 0;
			int num2 = 0;
			foreach (CsvRow csvRow in enumerable)
			{
				if (flag)
				{
					flag = false;
				}
				else if (string.IsNullOrWhiteSpace(csvRow["TenantName"]))
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Found row with empty OrgName in .tenant file {0}", new object[]
					{
						file.FullName
					});
				}
				else
				{
					string text = csvRow["TenantName"];
					int num3;
					double num4;
					int num5;
					double num6;
					try
					{
						num3 = int.Parse(csvRow["PrimaryMBXNum"]);
						num4 = double.Parse(csvRow["PrimaryMBXSize"]);
						num5 = int.Parse(csvRow["ArchiveMBXNum"]);
						num6 = double.Parse(csvRow["ArchiveMBXSize"]);
					}
					catch (Exception ex)
					{
						if (ex is FormatException || ex is OverflowException)
						{
							base.Context.Logger.Log(MigrationEventType.Error, "Found corrupt org data, skipping processing for this org. Name: {0} PrimaryMBXCount: {1} PrimaryMBXSize: {2} ArchiveMBXCount: {3} ArchiveMBXSize:{4} Error: {5}", new object[]
							{
								text,
								csvRow["PrimaryMBXNum"],
								csvRow["PrimaryMBXSize"],
								csvRow["ArchiveMBXNum"],
								csvRow["ArchiveMBXSize"],
								ex
							});
							continue;
						}
						throw;
					}
					num++;
					base.Context.Logger.Log(MigrationEventType.Verbose, "Found org data. Organization: {0} PrimaryMBXCount: {1} PrimaryMBXSize: {2} ArchiveMBXCount: {3} ArchiveMBXSize:{4}", new object[]
					{
						text,
						num3,
						num4,
						num5,
						num6
					});
					if (isE14)
					{
						if (!tenantDataDictionary.ContainsKey(text))
						{
							base.Context.Logger.Log(MigrationEventType.Verbose, "Organization {0} was not found using get-organization", new object[]
							{
								text
							});
						}
						else
						{
							tenantDataDictionary[text].AddValues(num3, num4, num5, num6, true);
						}
					}
					else if (text.Equals(TenantDataCollectorScheduler.emptyGuid))
					{
						base.Context.Logger.Log(MigrationEventType.Verbose, "Cannot process orgs with the empty Guid as their id", new object[0]);
						num2++;
					}
					else if (!idsToNames.ContainsKey(text))
					{
						base.Context.Logger.Log(MigrationEventType.Verbose, "Organization {0} was not found using get-organization", new object[]
						{
							text
						});
					}
					else
					{
						tenantDataDictionary[(string)idsToNames[text]].AddValues(num3, num4, num5, num6, false);
					}
				}
			}
			if (num2 > 0)
			{
				base.Context.Logger.Log(MigrationEventType.Verbose, "{0} organizations had empty guids as their name", new object[]
				{
					num2
				});
			}
			base.Context.Logger.Log(MigrationEventType.Verbose, "Found data for {0} orgs in file {1}", new object[]
			{
				num,
				file.FullName
			});
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00007D14 File Offset: 0x00005F14
		private void UploadData(Dictionary<string, TenantData> tenantDataDictionary)
		{
			IEnumerable<TenantData> source = from p in tenantDataDictionary.Values
			where p.Version != null && p.Version.ExchangeBuild.Major == ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major && p.Version.ExchangeBuild.Minor >= 16
			select p;
			TenantData[] array = source.ToArray<TenantData>();
			if (array.Length > 0 && base.Context.Config.GetConfig<bool>("UploadToSymphony"))
			{
				CommonUtils.ProcessInBatches<TenantData>(array, 500, new Action<TenantData[]>(this.UploadDataBatch));
				return;
			}
			if (array.Length > 0)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "UploadToSymphony config is false so no data will be uploaded.", new object[0]);
				return;
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Found no E14 R6 data to upload", new object[0]);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00007DEC File Offset: 0x00005FEC
		private void UploadDataBatch(TenantData[] batch)
		{
			List<TenantReadiness> tenantReadinessList = new List<TenantReadiness>();
			foreach (TenantData tenantData in batch)
			{
				tenantData.TenantSize = this.GetTenantSize(tenantData);
				if (!tenantData.ShouldIgnore)
				{
					bool useDefaultCapacity = tenantData.TenantSize <= 0;
					string[] array;
					if (tenantData.UpgradeConstraintsDisabled != null && tenantData.UpgradeConstraintsDisabled.Value)
					{
						array = Array<string>.Empty;
					}
					else
					{
						array = tenantData.Constraints;
					}
					bool isReady = array == null || array.Length == 0;
					tenantReadinessList.Add(new TenantReadiness(array, this.ForestName, isReady, tenantData.TenantId, tenantData.TenantSize, useDefaultCapacity));
				}
				else
				{
					base.Context.Logger.Log(MigrationEventType.Verbose, "Ignoring tenant {0} with ExternalDirectoryOrganizationId: '{1}' TenantSize: '{2}' ProgramId: '{3}' OfferId: '{4}' ", new object[]
					{
						tenantData.TenantName,
						tenantData.TenantId.ToString(),
						tenantData.TenantSize,
						tenantData.ProgramId,
						tenantData.OfferId
					});
				}
			}
			base.Context.Logger.Log(MigrationEventType.Information, "Updating the TenantReadiness of {0} tenants", new object[]
			{
				tenantReadinessList.Count
			});
			if (tenantReadinessList.Count > 0)
			{
				this.CatchExceptions(delegate
				{
					this.symphonyProxy.UpdateTenantReadiness(tenantReadinessList.ToArray());
				}, "Error Updating Tenant Readiness info");
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00007F8C File Offset: 0x0000618C
		private int GetTenantSize(TenantData data)
		{
			if (data.UpgradeUnitsOverride != null)
			{
				return data.UpgradeUnitsOverride.Value;
			}
			double num = (double)(data.E14MbxData.PrimaryData.Count + data.E14MbxData.ArchiveData.Count);
			if (num > 0.0)
			{
				double num2 = (data.E14MbxData.PrimaryData.Size + data.E14MbxData.ArchiveData.Size) / (num * (double)base.Context.Config.GetConfig<int>("UpgradeUnitsConversionFactor"));
				if (num2 > 1.0)
				{
					num *= num2;
				}
				num = Math.Ceiling(num);
			}
			return (int)num;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000803C File Offset: 0x0000623C
		private void ValidateAndLog(Dictionary<string, TenantData> tenantDataDictionary)
		{
			base.Context.Logger.Log(MigrationEventType.Information, "Writing output files and validating mbx and tenant versions", new object[0]);
			string logFilePrefix = Path.ChangeExtension("TenantSizeLog", ".fail");
			using (TenantSizeLog tenantSizeLog = new TenantSizeLog("TenantSizeLog"))
			{
				using (TenantSizeLog tenantSizeLog2 = new TenantSizeLog(logFilePrefix))
				{
					foreach (TenantData tenantData in tenantDataDictionary.Values)
					{
						string text = null;
						if (base.Context.Config.GetConfig<bool>("ValidateMailboxVersions"))
						{
							try
							{
								tenantData.Validate();
							}
							catch (TooManyPilotMailboxesException)
							{
								text = "TooManyPilotMailboxes";
							}
							catch (InvalidE15MailboxesException)
							{
								text = "InvalidE15Mailboxes";
							}
							catch (InvalidE14MailboxesException)
							{
								text = "InvalidE14Mailboxes";
							}
						}
						tenantSizeLog.Write(tenantData, text);
						if (text != null)
						{
							tenantSizeLog2.Write(tenantData, text);
						}
					}
				}
			}
		}

		// Token: 0x040001F1 RID: 497
		private const string InvalidE15Mailboxes = "InvalidE15Mailboxes";

		// Token: 0x040001F2 RID: 498
		private const string TooManyPilotMailboxes = "TooManyPilotMailboxes";

		// Token: 0x040001F3 RID: 499
		private const string InvalidE14Mailboxes = "InvalidE14Mailboxes";

		// Token: 0x040001F4 RID: 500
		private static readonly string emptyGuid = Guid.Empty.ToString();

		// Token: 0x040001F5 RID: 501
		private IOrganizationOperation orgOperationProxy;

		// Token: 0x040001F6 RID: 502
		private ISymphonyProxy symphonyProxy;
	}
}
