using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000044 RID: 68
	internal class MRSIssueCache : ServiceIssueCache
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001727A File Offset: 0x0001547A
		public override bool ScanningIsEnabled
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<bool>("IssueCacheIsEnabled");
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00017286 File Offset: 0x00015486
		protected override TimeSpan FullScanFrequency
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("IssueCacheScanFrequency");
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00017292 File Offset: 0x00015492
		protected override int IssueLimit
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<int>("IssueCacheItemLimit");
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000172A0 File Offset: 0x000154A0
		protected override ICollection<ServiceIssue> RunFullIssueScan()
		{
			ICollection<ServiceIssue> collection = new List<ServiceIssue>();
			foreach (Guid mdbGuid in MapiUtils.GetDatabasesOnThisServer())
			{
				using (new DatabaseSettingsContext(mdbGuid, null).Activate())
				{
					try
					{
						DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, null, null, FindServerFlags.None);
						string databaseName = databaseInformation.DatabaseName;
						if (!databaseInformation.IsOnThisServer)
						{
							return null;
						}
						using (MapiStore systemMailbox = MapiUtils.GetSystemMailbox(mdbGuid, false))
						{
							using (MapiFolder requestJobsFolder = RequestJobXML.GetRequestJobsFolder(systemMailbox))
							{
								using (MapiTable contentsTable = requestJobsFolder.GetContentsTable(ContentsTableFlags.DeferredErrors))
								{
									if (contentsTable.GetRowCount() > 0)
									{
										RequestJobNamedPropertySet requestJobNamedPropertySet = RequestJobNamedPropertySet.Get(systemMailbox);
										contentsTable.SetColumns(requestJobNamedPropertySet.PropTags);
										Restriction restriction = Restriction.GT(requestJobNamedPropertySet.PropTags[23], ConfigBase<MRSConfigSchema>.GetConfig<int>("PoisonLimit"));
										List<MoveJob> allMoveJobs = SystemMailboxJobs.GetAllMoveJobs(restriction, null, contentsTable, mdbGuid, null);
										if (allMoveJobs != null)
										{
											foreach (MoveJob job in allMoveJobs)
											{
												collection.Add(new MRSPoisonedJobIssue(job));
											}
										}
									}
								}
							}
						}
					}
					catch (LocalizedException lastScanError)
					{
						base.LastScanError = lastScanError;
					}
				}
			}
			return collection;
		}
	}
}
