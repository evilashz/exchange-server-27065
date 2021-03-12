using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Win32;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000070 RID: 112
	internal sealed class IndexStatusStore : IIndexStatusStore
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000768C File Offset: 0x0000588C
		private IndexStatusStore()
		{
			this.config = new FlightingSearchConfig();
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("IndexStatusStore", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.IndexStatusStoreTracer, (long)this.GetHashCode());
			this.counterValueUpdateInterval = SearchConfig.Instance.FeederCounterValueUpdateInterval;
			this.indexStatusDictionary = this.LoadIndexStatusDictionary();
			if (this.indexStatusDictionary == IndexStatusStore.emptyStatusDictionary)
			{
				this.indexStatusDictionary = new Dictionary<Guid, IndexStatus>();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000770F File Offset: 0x0000590F
		public static IndexStatusStore Instance
		{
			[DebuggerStepThrough]
			get
			{
				return IndexStatusStore.instance;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007718 File Offset: 0x00005918
		public void UpdateIndexStatus(Guid databaseGuid, IndexStatusIndex indexStatusIndex, long value)
		{
			bool flag = false;
			IndexStatus indexStatus;
			lock (this.indexStatusDictionary)
			{
				if (!this.indexStatusDictionary.TryGetValue(databaseGuid, out indexStatus))
				{
					return;
				}
				indexStatus.UpdateValue(indexStatusIndex, value);
				if (ExDateTime.UtcNow - indexStatus.TimeStamp > this.counterValueUpdateInterval)
				{
					indexStatus.TimeStamp = ExDateTime.UtcNow;
					flag = true;
				}
			}
			if (flag)
			{
				this.StoreIndexStatus(databaseGuid, indexStatus);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000077A4 File Offset: 0x000059A4
		public IndexStatus SetIndexStatus(Guid databaseGuid, int mailboxToCrawl, VersionInfo version)
		{
			IndexStatus indexStatus = new IndexStatus(mailboxToCrawl, version);
			lock (this.indexStatusDictionary)
			{
				this.LogIndexStatusChange(indexStatus, databaseGuid);
				this.indexStatusDictionary[databaseGuid] = indexStatus;
			}
			this.StoreIndexStatus(databaseGuid, indexStatus);
			return indexStatus;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007804 File Offset: 0x00005A04
		public IndexStatus SetIndexStatus(Guid databaseGuid, ContentIndexStatusType indexingState, IndexStatusErrorCode errorCode, VersionInfo version, string seedingSource)
		{
			IndexStatus indexStatus = new IndexStatus(indexingState, errorCode, version, seedingSource);
			lock (this.indexStatusDictionary)
			{
				this.LogIndexStatusChange(indexStatus, databaseGuid);
				this.indexStatusDictionary[databaseGuid] = indexStatus;
			}
			this.StoreIndexStatus(databaseGuid, indexStatus);
			return indexStatus;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007868 File Offset: 0x00005A68
		public IndexStatus GetIndexStatus(Guid databaseGuid)
		{
			this.diagnosticsSession.TraceDebug<string>("Opening registry key: {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus");
			IndexStatus indexStatus;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus"))
			{
				if (registryKey == null)
				{
					this.diagnosticsSession.TraceError<string>("Registry key doesn't exist: {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus");
					throw new IndexStatusRegistryNotFoundException();
				}
				indexStatus = this.GetIndexStatus(registryKey, IndexStatusStore.GetIndexValueName(databaseGuid));
			}
			return indexStatus;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000078E4 File Offset: 0x00005AE4
		public IReadOnlyDictionary<Guid, IndexStatus> GetIndexStatusDictionary()
		{
			return this.LoadIndexStatusDictionary();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000078EC File Offset: 0x00005AEC
		internal static string GetIndexValueName(Guid databaseGuid)
		{
			return databaseGuid.ToString("B");
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000078FC File Offset: 0x00005AFC
		internal IndexStatus DeserializeIndexStatus(string serializationString)
		{
			if (string.IsNullOrEmpty(serializationString))
			{
				throw new IndexStatusInvalidException(serializationString);
			}
			string[] array = serializationString.Split(new char[]
			{
				','
			});
			if (array.Length != 8)
			{
				throw new IndexStatusInvalidException(serializationString);
			}
			ContentIndexStatusType contentIndexStatusType = (ContentIndexStatusType)this.ConvertTo<int>("IndexingState", array[0]);
			IndexStatusErrorCode errorCode = (IndexStatusErrorCode)this.ConvertTo<int>("ErrorCode", array[1]);
			VersionInfo version = VersionInfo.FromRaw(this.ConvertTo<long>("Version", array[2]));
			ExDateTime timeStamp = this.ConvertTo<ExDateTime>("TimeStamp", array[3]);
			IndexStatus indexStatus = new IndexStatus(contentIndexStatusType, errorCode, version, null, 0, timeStamp);
			ContentIndexStatusType contentIndexStatusType2 = contentIndexStatusType;
			switch (contentIndexStatusType2)
			{
			case ContentIndexStatusType.Crawling:
				break;
			case ContentIndexStatusType.Failed:
				goto IL_C2;
			case ContentIndexStatusType.Seeding:
				indexStatus.SeedingSource = array[5];
				goto IL_C2;
			default:
				if (contentIndexStatusType2 != ContentIndexStatusType.HealthyAndUpgrading)
				{
					goto IL_C2;
				}
				break;
			}
			indexStatus.MailboxesToCrawl = this.ConvertTo<int>("MailboxesToCrawl", array[4]);
			IL_C2:
			indexStatus.AgeOfLastNotificationProcessed = this.ConvertTo<long>("AgeOfLastNotificationProcessed", array[6]);
			indexStatus.RetriableItemsCount = this.ConvertTo<long>("RetriableItemsCount", array[7]);
			return indexStatus;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000079F8 File Offset: 0x00005BF8
		internal string SerializeIndexStatus(IndexStatus indexStatus)
		{
			return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}", new object[]
			{
				',',
				(int)indexStatus.IndexingState,
				(int)indexStatus.ErrorCode,
				indexStatus.Version.RawValue,
				indexStatus.TimeStamp.ToString("u"),
				indexStatus.MailboxesToCrawl,
				indexStatus.SeedingSource,
				indexStatus.AgeOfLastNotificationProcessed,
				indexStatus.RetriableItemsCount
			});
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007AA0 File Offset: 0x00005CA0
		private Dictionary<Guid, IndexStatus> LoadIndexStatusDictionary()
		{
			Dictionary<Guid, IndexStatus> result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus"))
			{
				if (registryKey == null)
				{
					result = IndexStatusStore.emptyStatusDictionary;
				}
				else
				{
					string[] valueNames = registryKey.GetValueNames();
					if (valueNames.Length == 0)
					{
						result = IndexStatusStore.emptyStatusDictionary;
					}
					else
					{
						Dictionary<Guid, IndexStatus> dictionary = new Dictionary<Guid, IndexStatus>(valueNames.Length);
						foreach (string text in valueNames)
						{
							Guid key;
							if (Guid.TryParse(text, out key))
							{
								try
								{
									IndexStatus indexStatus = this.GetIndexStatus(registryKey, text);
									dictionary.Add(key, indexStatus);
								}
								catch (IndexStatusException)
								{
								}
							}
						}
						result = dictionary;
					}
				}
			}
			return result;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007B54 File Offset: 0x00005D54
		private void StoreIndexStatus(Guid databaseGuid, IndexStatus indexStatus)
		{
			this.AddAccessRulesIfNecessary();
			this.diagnosticsSession.TraceDebug<Guid, IndexStatus>("Setting index status for mdb {0}: {1}", databaseGuid, indexStatus);
			string indexValueName = IndexStatusStore.GetIndexValueName(databaseGuid);
			if (ExEnvironment.IsTest)
			{
				indexStatus = this.ContentIndexStatusFaultInjection(indexStatus, indexValueName);
			}
			this.diagnosticsSession.TraceDebug<string>("Opening or creating registry key: {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus");
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus", RegistryKeyPermissionCheck.ReadWriteSubTree, this.registrySecurity))
			{
				string text = this.SerializeIndexStatus(indexStatus);
				this.diagnosticsSession.TraceDebug<string, string, string>("Setting value for {0} under RegistryKey {1}: {2}", indexValueName, registryKey.Name, text);
				registryKey.SetValue(indexValueName, text, RegistryValueKind.String);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007C04 File Offset: 0x00005E04
		private IndexStatus GetIndexStatus(RegistryKey registryKey, string valueName)
		{
			object value = registryKey.GetValue(valueName);
			this.diagnosticsSession.TraceDebug<string, string, object>("Getting value for {0} under RegistryKey {1}: {2}", valueName, registryKey.Name, value ?? "(null)");
			if (value == null)
			{
				throw new IndexStatusNotFoundException(valueName);
			}
			IndexStatus indexStatus = this.DeserializeIndexStatus(value as string);
			this.diagnosticsSession.TraceDebug<string, IndexStatus>("Read index status for mdb {0}: {1}", valueName, indexStatus);
			if (this.config.EnableIndexStatusTimestampVerification)
			{
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow - indexStatus.TimeStamp.UniversalTime >= this.config.IndexStatusInvalidateInterval)
				{
					this.diagnosticsSession.TraceDebug<string, TimeSpan>("Index status for mdb {0} is older than {1}. Will set to Unknown with IndexStatusTimestampTooOld", valueName, this.config.IndexStatusInvalidateInterval);
					indexStatus.IndexingState = ContentIndexStatusType.Unknown;
					indexStatus.ErrorCode = IndexStatusErrorCode.IndexStatusTimestampTooOld;
				}
			}
			return indexStatus;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007CC8 File Offset: 0x00005EC8
		private void LogIndexStatusChange(IndexStatus indexStatus, Guid databaseGuid)
		{
			IndexStatus indexStatus2;
			this.indexStatusDictionary.TryGetValue(databaseGuid, out indexStatus2);
			if (indexStatus2 == null || indexStatus2.IndexingState != indexStatus.IndexingState)
			{
				this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "IndexStatus change for mdb:{0} - Changed from [{1}] to [{2}]", new object[]
				{
					databaseGuid,
					indexStatus2,
					indexStatus
				});
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007D20 File Offset: 0x00005F20
		private IndexStatus ContentIndexStatusFaultInjection(IndexStatus indexStatus, string valueName)
		{
			IndexStatus result = indexStatus;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\SystemParameters"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("IndexStatusFault");
						this.diagnosticsSession.TraceDebug<string, string, object>("Getting value for {0} under RegistryKeyFault {1}: {2}", valueName, registryKey.Name, value ?? "(null)");
						if (value != null)
						{
							result = new IndexStatus((ContentIndexStatusType)Enum.Parse(typeof(ContentIndexStatusType), (string)value), indexStatus.ErrorCode, indexStatus.Version);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceDebug("Exception when setting index status with fault injection " + ex.ToString(), new object[0]);
			}
			return result;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007DF0 File Offset: 0x00005FF0
		private void AddAccessRulesIfNecessary()
		{
			if (Interlocked.CompareExchange<RegistrySecurity>(ref this.registrySecurity, null, null) == null)
			{
				lock (this.locker)
				{
					if (this.registrySecurity == null)
					{
						RegistrySecurity registrySecurity = new RegistrySecurity();
						this.diagnosticsSession.TraceDebug("Adding access rules", new object[0]);
						using (WindowsIdentity current = WindowsIdentity.GetCurrent())
						{
							SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, current.User.AccountDomainSid);
							SecurityIdentifier identity2 = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, current.User.AccountDomainSid);
							RegistryAccessRule rule = new RegistryAccessRule(identity, RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
							registrySecurity.AddAccessRule(rule);
							RegistryAccessRule rule2 = new RegistryAccessRule(identity2, RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
							registrySecurity.AddAccessRule(rule2);
							this.diagnosticsSession.TraceDebug("Added access rules", new object[0]);
							Thread.MemoryBarrier();
							this.registrySecurity = registrySecurity;
						}
					}
				}
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007F00 File Offset: 0x00006100
		private T ConvertTo<T>(string properyName, string stringValue)
		{
			long num2;
			if (typeof(T) == typeof(ExDateTime))
			{
				ExDateTime exDateTime;
				if (ExDateTime.TryParse(stringValue, out exDateTime))
				{
					return (T)((object)exDateTime.ToUtc());
				}
			}
			else if (typeof(T) == typeof(int))
			{
				int num;
				if (int.TryParse(stringValue, out num))
				{
					return (T)((object)num);
				}
			}
			else if (typeof(T) == typeof(long) && long.TryParse(stringValue, out num2))
			{
				return (T)((object)num2);
			}
			throw new IndexStatusInvalidPropertyException(properyName, stringValue);
		}

		// Token: 0x0400011F RID: 287
		internal const string IndexStatusKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\IndexStatus";

		// Token: 0x04000120 RID: 288
		internal const string IndexStatusKeyNameFault = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Search\\SystemParameters";

		// Token: 0x04000121 RID: 289
		private const char IndexStatusDelimiter = ',';

		// Token: 0x04000122 RID: 290
		private static readonly IndexStatusStore instance = new IndexStatusStore();

		// Token: 0x04000123 RID: 291
		private static readonly Dictionary<Guid, IndexStatus> emptyStatusDictionary = new Dictionary<Guid, IndexStatus>();

		// Token: 0x04000124 RID: 292
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000125 RID: 293
		private readonly object locker = new object();

		// Token: 0x04000126 RID: 294
		private readonly TimeSpan counterValueUpdateInterval;

		// Token: 0x04000127 RID: 295
		private readonly ISearchServiceConfig config;

		// Token: 0x04000128 RID: 296
		private RegistrySecurity registrySecurity;

		// Token: 0x04000129 RID: 297
		private Dictionary<Guid, IndexStatus> indexStatusDictionary;
	}
}
