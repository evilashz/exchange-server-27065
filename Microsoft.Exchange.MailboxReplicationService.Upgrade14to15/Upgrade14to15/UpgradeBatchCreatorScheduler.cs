using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeBatchCreatorScheduler : CacheScheduler
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0000B988 File Offset: 0x00009B88
		public UpgradeBatchCreatorScheduler(AnchorContext context, IOrganizationOperation orgOperationProxyInstance, WaitHandle stopEvent) : base(context, stopEvent)
		{
			this.orgOperationProxy = orgOperationProxyInstance;
			if (orgOperationProxyInstance is OrgOperationProxy)
			{
				((OrgOperationProxy)this.orgOperationProxy).Context = context;
			}
			this.e14CountsUpdateInterval = base.Context.Config.GetConfig<TimeSpan>("E14CountUpdateInterval");
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000BA37 File Offset: 0x00009C37
		private string OrgBatchDirectoryPath
		{
			get
			{
				if (this.orgBatchDirectoryPath == null)
				{
					this.orgBatchDirectoryPath = Path.Combine(base.Context.Config.GetConfig<string>("BatchFileDirectoryPath"), this.organization.ExternalDirectoryOrganizationId);
					if (!Directory.Exists(this.orgBatchDirectoryPath))
					{
						Directory.CreateDirectory(this.orgBatchDirectoryPath);
					}
				}
				return this.orgBatchDirectoryPath;
			}
			set
			{
				this.orgBatchDirectoryPath = value;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000BA40 File Offset: 0x00009C40
		public override XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(parameters);
			if (this.organization != null)
			{
				diagnosticInfo.Add(new XElement("TenantId", this.organization.ExternalDirectoryOrganizationId));
				diagnosticInfo.Add(new XElement("Organization", this.organization.Name));
				diagnosticInfo.Add(new XElement("E14MbxCount", this.e14MbxCount));
				diagnosticInfo.Add(new XElement("crossOrgMoveRequestCount", this.crossOrgMoveRequestCount));
				diagnosticInfo.Add(new XElement("intraOrgMoveRequestCount", this.intraOrgMoveRequestCount));
				diagnosticInfo.Add(new XElement("highPriorityMoveRequestCount", this.highPriorityMoveRequestCount));
			}
			else
			{
				diagnosticInfo.Add(new XElement("CurrentStage", "None"));
			}
			return diagnosticInfo;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000BB40 File Offset: 0x00009D40
		internal AnchorJobProcessorResult InternalProcessEntry(ICacheEntry cacheEntryProxy)
		{
			try
			{
				if (cacheEntryProxy.OrgName.StartsWith("a830edad9050849EXOT", StringComparison.OrdinalIgnoreCase))
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Skipping interruption test tenant", new object[0]);
					return AnchorJobProcessorResult.Deleted;
				}
				this.organization = this.orgOperationProxy.GetOrganization(cacheEntryProxy.ExternalDirectoryOrganizationId);
				base.Context.Logger.Log(MigrationEventType.Information, "Current org state: UpgradeRequest='{0}', UpgradeStatus='{1}', UpgradeStage ='{2}'", new object[]
				{
					this.organization.UpgradeRequest,
					this.organization.UpgradeStatus,
					this.organization.UpgradeStage
				});
				if (this.organization.UpgradeStatus == UpgradeStatusTypes.Error)
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Organization UpgradeStatus is in Error and will not be processed.", new object[0]);
					return AnchorJobProcessorResult.Deleted;
				}
				if ((this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgrade || this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun) && (this.organization.UpgradeStatus == UpgradeStatusTypes.Complete || this.organization.UpgradeStatus == UpgradeStatusTypes.ForceComplete))
				{
					base.Context.Logger.Log(MigrationEventType.Information, "UpgradeStatus already Complete and no need to check for Pilot users. Nothing to do.", new object[0]);
					return AnchorJobProcessorResult.Deleted;
				}
				if (this.organization.UpgradeRequest == UpgradeRequestTypes.CancelPrestageUpgrade && this.organization.UpgradeStatus != UpgradeStatusTypes.Complete)
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Setting org to CancelPrestageUpgrade,Complete", new object[0]);
					this.orgOperationProxy.SetOrganization(this.organization, UpgradeStatusTypes.Complete, this.organization.UpgradeRequest, string.Empty, string.Empty, null, -1, -1);
					return AnchorJobProcessorResult.Deleted;
				}
				if (UpgradeBatchCreatorScheduler.e14MailboxQueryFilter == null)
				{
					UpgradeBatchCreatorScheduler.e14MailboxQueryFilter = cacheEntryProxy.ADSessionProxy.BuildE14MailboxQueryFilter();
					if (UpgradeBatchCreatorScheduler.e14MailboxQueryFilter == null)
					{
						throw new NoE14ServersFoundException();
					}
				}
				this.ProcessUpgradeRequest(cacheEntryProxy);
			}
			catch (AnchorServiceInstanceNotActiveException)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "This AnchorService instance is no longer Active. Processing stopped.", new object[0]);
				return AnchorJobProcessorResult.Deleted;
			}
			catch (Exception ex)
			{
				base.Context.Logger.Log(MigrationEventType.Error, "Processing of UpgradeRequest failed due to: {0}", new object[]
				{
					ex
				});
				if (this.organization != null)
				{
					UpgradeBatchCreatorProgressLog.Write(this.organization, ex.GetType().ToString(), ex.Message, null, null);
					try
					{
						this.orgOperationProxy.SetOrganization(this.organization, UpgradeStatusTypes.Warning, this.organization.UpgradeRequest, ex.Message, ex.ToString(), this.organization.UpgradeStage, -1, -1);
					}
					catch (Exception ex2)
					{
						base.Context.Logger.Log(MigrationEventType.Error, "Could not record error on organization due to: {0}", new object[]
						{
							ex2
						});
						UpgradeBatchCreatorProgressLog.Write(this.organization, ex2.GetType().ToString(), ex2.Message, null, null);
					}
				}
				if (!(ex is LocalizedException) && !(ex is IOException))
				{
					throw;
				}
			}
			finally
			{
				base.Context.Logger.Log(MigrationEventType.Information, "ProcessEntry completed.", new object[0]);
			}
			return AnchorJobProcessorResult.Waiting;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		protected override AnchorJobProcessorResult ProcessEntry(CacheEntryBase cacheEntry)
		{
			base.Context.Logger.Log(MigrationEventType.Information, "Entered ProcessEntry", new object[0]);
			return this.InternalProcessEntry(new CacheEntryProxy(cacheEntry));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		private static UpgradeStage MapMailboxTypeToUpgradeStage(UpgradeBatchCreatorScheduler.MailboxType mailboxType)
		{
			switch (mailboxType)
			{
			case UpgradeBatchCreatorScheduler.MailboxType.Regular:
			case UpgradeBatchCreatorScheduler.MailboxType.Discovery:
			case UpgradeBatchCreatorScheduler.MailboxType.RegularSoftDeleted:
				return UpgradeStage.MoveRegularUser;
			case UpgradeBatchCreatorScheduler.MailboxType.RegularPilot:
				return UpgradeStage.MoveRegularPilot;
			case UpgradeBatchCreatorScheduler.MailboxType.Arbitration:
				return UpgradeStage.MoveArbitration;
			case UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchive:
			case UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchiveSoftDeleted:
				return UpgradeStage.MoveCloudOnlyArchive;
			case UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchivePilot:
				return UpgradeStage.MoveCloudOnlyArchivePilot;
			default:
				throw new ArgumentException(string.Format("Not recognized mailbox type {0}", mailboxType));
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000BF54 File Offset: 0x0000A154
		private void ProcessUpgradeRequest(ICacheEntry cacheEntryProxy)
		{
			this.VerifyOrganizationStateBefore();
			this.OrgBatchDirectoryPath = null;
			if (!this.ShouldCreateUpgradeBatches())
			{
				return;
			}
			this.CreateUpgradeBatches(cacheEntryProxy);
			if (this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgrade && this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgradeDryRun)
			{
				this.CompletePilotUsers(cacheEntryProxy);
			}
			if (this.e14MbxCount <= 0 || this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "ProcessUpgradeRequest '{0}' Completed.", new object[]
				{
					this.organization.UpgradeRequest
				});
				if (this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgrade || this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun)
				{
					this.VerifyOrganizationStateAfter();
				}
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000C00C File Offset: 0x0000A20C
		private bool ShouldCreateUpgradeBatches()
		{
			int num = Directory.GetFiles(this.OrgBatchDirectoryPath, "*.csv").Length;
			if (num > 0)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "Batch directory '{0}' still has {1} {2} files remaining. Skipping new batch file creation.", new object[]
				{
					this.OrgBatchDirectoryPath,
					num,
					".csv"
				});
				return false;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(this.orgBatchDirectoryPath);
			FileInfo[] files = directoryInfo.GetFiles("*.inprogress*");
			DateTime dateTime = DateTime.UtcNow - base.Context.Config.GetConfig<TimeSpan>("DelayUntilCreateNewBatches");
			foreach (FileInfo fileInfo in files)
			{
				if (fileInfo.LastWriteTimeUtc > dateTime)
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Batch directory '{0}' still has at leat one .inprogress file newer than {1}. Skipping new batch file creation.", new object[]
					{
						this.OrgBatchDirectoryPath,
						dateTime
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000C110 File Offset: 0x0000A310
		private void CreateUpgradeBatches(ICacheEntry cacheEntryProxy)
		{
			bool flag = this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgrade && this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgradeDryRun;
			if (flag)
			{
				this.CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType.RegularPilot, cacheEntryProxy);
			}
			else
			{
				this.CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType.Arbitration, cacheEntryProxy);
				if (this.e14MbxCount > 0 && this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgradeDryRun)
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Still has {0} Arbitration mailboxes to move before it can move regular mailboxes.", new object[]
					{
						this.e14MbxCount
					});
					return;
				}
				this.CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType.Regular, cacheEntryProxy);
				this.CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType.RegularSoftDeleted, cacheEntryProxy);
				this.CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType.Discovery, cacheEntryProxy);
			}
			if (this.e14MbxCount > 0 && this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgradeDryRun)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "Still has {0} '{1}' mailboxes on E14.", new object[]
				{
					this.e14MbxCount,
					flag ? UpgradeBatchCreatorScheduler.MailboxType.RegularPilot : UpgradeBatchCreatorScheduler.MailboxType.Regular
				});
				return;
			}
			this.CreateFilteredUpgradeBatches(flag ? UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchivePilot : UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchive, cacheEntryProxy);
			if (!flag)
			{
				this.CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchiveSoftDeleted, cacheEntryProxy);
			}
			if (this.e14MbxCount > 0)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "Still has {0} '{1}' mailboxes to move.", new object[]
				{
					this.e14MbxCount,
					flag ? UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchivePilot : UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchive
				});
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000C264 File Offset: 0x0000A464
		private void CreateFilteredUpgradeBatches(UpgradeBatchCreatorScheduler.MailboxType mailboxType, ICacheEntry cacheEntryProxy)
		{
			UpgradeStage upgradeStage = UpgradeBatchCreatorScheduler.MapMailboxTypeToUpgradeStage(mailboxType);
			QueryFilter filterByBatchType = this.GetFilterByBatchType(mailboxType);
			base.Context.Logger.Log(MigrationEventType.Information, "CreateFilteredUpgradeBatches. upgrade stage is {0} mailboxType='{1}', filter='{2}'", new object[]
			{
				upgradeStage,
				mailboxType,
				filterByBatchType
			});
			ADObjectId rootId = (cacheEntryProxy is CacheEntryProxy && (mailboxType == UpgradeBatchCreatorScheduler.MailboxType.RegularSoftDeleted || mailboxType == UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchiveSoftDeleted)) ? ((CacheEntryProxy)cacheEntryProxy).CacheEntryBase.OrganizationId.OrganizationalUnit.GetChildId("OU", "Soft Deleted Objects") : null;
			IEnumerable<RecipientWrapper> enumerable = cacheEntryProxy.ADSessionProxy.FindPagedMiniRecipient(mailboxType, rootId, QueryScope.OneLevel, filterByBatchType, null, 0, UpgradeBatchCreatorScheduler.AddlMiniRecipientProps);
			if (mailboxType != UpgradeBatchCreatorScheduler.MailboxType.Discovery && mailboxType != UpgradeBatchCreatorScheduler.MailboxType.RegularSoftDeleted && mailboxType != UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchiveSoftDeleted)
			{
				this.e14MbxCount = 0;
				this.crossOrgMoveRequestCount = 0;
				this.highPriorityMoveRequestCount = 0;
				this.intraOrgMoveRequestCount = 0;
				this.upgradeInProgressMoveRequestCount = 0;
				this.upgradeNotInProgressMoveRequestCount = 0;
			}
			string text = null;
			StreamWriter streamWriter = null;
			int config = base.Context.Config.GetConfig<int>("MaxBatchSize");
			bool config2 = base.Context.Config.GetConfig<bool>("RemoveNonUpgradeMoveRequests");
			string config3 = base.Context.Config.GetConfig<string>((this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun) ? "DryRunBatchFilenamePrefix" : "UpgradeBatchFilenamePrefix");
			int num = config;
			int num2 = 0;
			try
			{
				foreach (RecipientWrapper recipientWrapper in enumerable)
				{
					if (mailboxType == UpgradeBatchCreatorScheduler.MailboxType.Regular && (recipientWrapper.RecipientTypeDetails.HasFlag(RecipientTypeDetails.MailboxPlan) || recipientWrapper.RecipientTypeDetails.HasFlag(RecipientTypeDetails.DiscoveryMailbox)))
					{
						base.Context.Logger.Log(MigrationEventType.Information, "Skipping '{0}' with RecipientTypeDetails '{1}'", new object[]
						{
							recipientWrapper.Id,
							recipientWrapper.RecipientTypeDetails
						});
					}
					else
					{
						this.e14MbxCount++;
						num2++;
						if (recipientWrapper.MoveStatus != RequestStatus.None)
						{
							base.Context.Logger.Log(MigrationEventType.Information, "Move Request for '{0}' already exists. BatchName='{1}' Status='{2}' RequestFlags='{3}'", new object[]
							{
								recipientWrapper.Id.ObjectGuid,
								recipientWrapper.MailboxMoveBatchName,
								recipientWrapper.MoveStatus,
								recipientWrapper.RequestFlags
							});
							bool flag = false;
							if (recipientWrapper.MailboxMoveBatchName.StartsWith(config3, StringComparison.OrdinalIgnoreCase))
							{
								if (recipientWrapper.MoveStatus != RequestStatus.InProgress && recipientWrapper.MoveStatus != RequestStatus.Queued)
								{
									this.upgradeInProgressMoveRequestCount++;
								}
								else
								{
									this.upgradeNotInProgressMoveRequestCount++;
								}
							}
							else
							{
								if (config2 && (recipientWrapper.MoveStatus == RequestStatus.Completed || recipientWrapper.MoveStatus == RequestStatus.CompletedWithWarning || (!recipientWrapper.RequestFlags.HasFlag(RequestFlags.CrossOrg) && !recipientWrapper.RequestFlags.HasFlag(RequestFlags.HighPriority))))
								{
									flag = this.orgOperationProxy.TryRemoveMoveRequest(recipientWrapper.Id.ObjectGuid.ToString());
								}
								if (!flag)
								{
									if (recipientWrapper.RequestFlags.HasFlag(RequestFlags.CrossOrg))
									{
										this.crossOrgMoveRequestCount++;
									}
									else if (recipientWrapper.RequestFlags.HasFlag(RequestFlags.HighPriority))
									{
										this.highPriorityMoveRequestCount++;
									}
									else
									{
										this.intraOrgMoveRequestCount++;
									}
								}
							}
							if (!flag)
							{
								continue;
							}
						}
						if (num == config)
						{
							if (streamWriter != null)
							{
								streamWriter.Close();
								streamWriter.Dispose();
								streamWriter = null;
								File.Move(text, Path.ChangeExtension(text, ".csv"));
								if (cacheEntryProxy is CacheEntryProxy && base.ShouldProcessEntry(((CacheEntryProxy)cacheEntryProxy).CacheEntryBase) != AnchorJobProcessorResult.Working)
								{
									throw new AnchorServiceInstanceNotActiveException();
								}
							}
							num = 0;
							string path = string.Format("{0}{1:MMdd}{2}{3}{4}{5}{6}{7}", new object[]
							{
								config3,
								DateTime.UtcNow,
								'_',
								this.organization.ExternalDirectoryOrganizationId,
								'_',
								mailboxType,
								UpgradeBatchCreatorScheduler.random.Next(),
								".tmp"
							});
							text = Path.Combine(this.OrgBatchDirectoryPath, path);
							base.Context.Logger.Log(MigrationEventType.Information, "Starting new batch file '{0}'.", new object[]
							{
								text
							});
							streamWriter = new StreamWriter(text, false);
							streamWriter.WriteLine("Guid,TenantPartitionHint,SourceDatabase,TargetDatabase,ArchiveOnly,Priority,CompletedRequestAgeLimit,Protect,PreventCompletion,BlockFinalization,WorkloadType");
						}
						string text2 = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", new object[]
						{
							recipientWrapper.Id.ObjectGuid,
							this.organization.ExternalDirectoryOrganizationId,
							string.Empty,
							string.Empty,
							mailboxType == UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchive || mailboxType == UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchivePilot,
							(mailboxType == UpgradeBatchCreatorScheduler.MailboxType.Arbitration || mailboxType == UpgradeBatchCreatorScheduler.MailboxType.Discovery) ? RequestPriority.High : RequestPriority.Low,
							0,
							true,
							this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun,
							this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun,
							RequestWorkloadType.TenantUpgrade
						});
						base.Context.Logger.Log(MigrationEventType.Information, "Adding Entry: {0}", new object[]
						{
							text2
						});
						streamWriter.WriteLine(text2);
						num++;
					}
				}
			}
			finally
			{
				if (num2 > 0 && this.organization.UpgradeRequest != UpgradeRequestTypes.TenantUpgradeDryRun)
				{
					this.LogUpgradeStageWithE14Counts(new UpgradeStage?(upgradeStage));
				}
				if (streamWriter != null)
				{
					streamWriter.Close();
					streamWriter.Dispose();
					File.Move(text, Path.ChangeExtension(text, ".csv"));
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000C898 File Offset: 0x0000AA98
		private QueryFilter GetFilterByBatchType(UpgradeBatchCreatorScheduler.MailboxType mailboxType)
		{
			QueryFilter queryFilter;
			switch (mailboxType)
			{
			case UpgradeBatchCreatorScheduler.MailboxType.Regular:
			case UpgradeBatchCreatorScheduler.MailboxType.RegularSoftDeleted:
				queryFilter = UpgradeBatchCreatorScheduler.e14MailboxQueryFilter;
				break;
			case UpgradeBatchCreatorScheduler.MailboxType.RegularPilot:
				queryFilter = new AndFilter(new QueryFilter[]
				{
					UpgradeBatchCreatorScheduler.e14MailboxQueryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.UpgradeRequest, UpgradeRequestTypes.PilotUpgrade)
				});
				break;
			case UpgradeBatchCreatorScheduler.MailboxType.Arbitration:
				queryFilter = new AndFilter(new QueryFilter[]
				{
					UpgradeBatchCreatorScheduler.e14MailboxQueryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.ArbitrationMailbox)
				});
				break;
			case UpgradeBatchCreatorScheduler.MailboxType.Discovery:
				queryFilter = new AndFilter(new QueryFilter[]
				{
					UpgradeBatchCreatorScheduler.e14MailboxQueryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.DiscoveryMailbox)
				});
				break;
			case UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchive:
			case UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchivePilot:
			case UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchiveSoftDeleted:
				queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailUser),
					new ExistsFilter(IADMailStorageSchema.ArchiveDatabaseRaw),
					new ComparisonFilter(ComparisonOperator.NotEqual, ADUserSchema.ArchiveRelease, MailboxRelease.E15.ToString())
				});
				if (mailboxType == UpgradeBatchCreatorScheduler.MailboxType.CloudOnlyArchivePilot)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.UpgradeRequest, UpgradeRequestTypes.PilotUpgrade)
					});
				}
				break;
			default:
				throw new UnsupportedBatchTypeException(mailboxType.ToString());
			}
			return queryFilter;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000CA10 File Offset: 0x0000AC10
		private void VerifyOrganizationStateBefore()
		{
			if (this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun)
			{
				if (!this.InBeforeStartOrgCmdletState())
				{
					throw new InvalidOrganizationStateException(this.organization.ExternalDirectoryOrganizationId, this.organization.ServicePlan, this.organization.AdminDisplayVersion, this.organization.IsUpgradingOrganization, this.organization.IsPilotingOrganization, this.organization.IsUpgradeOperationInProgress);
				}
			}
			else
			{
				if (this.InBeforeStartOrgCmdletState())
				{
					bool flag = this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgrade;
					string text = "Start-Organization" + (flag ? "Upgrade" : "Pilot");
					base.Context.Logger.Log(MigrationEventType.Information, "Need to run '{0}'", new object[]
					{
						text
					});
					this.LogUpgradeStageWithE14Counts(new UpgradeStage?((this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgrade) ? UpgradeStage.StartOrgUpgrade : UpgradeStage.StartPilotUpgrade));
					bool configOnly = flag && base.Context.Config.GetConfig<bool>("ConfigOnly");
					this.orgOperationProxy.InvokeOrganizationCmdlet(this.organization.ExternalDirectoryOrganizationId, text, configOnly);
					this.organization = this.orgOperationProxy.GetOrganization(this.organization.ExternalDirectoryOrganizationId);
				}
				if (this.InBeforeStartOrgCmdletState())
				{
					throw new InvalidOrganizationStateException(this.organization.ExternalDirectoryOrganizationId, this.organization.ServicePlan, this.organization.AdminDisplayVersion, this.organization.IsUpgradingOrganization, this.organization.IsPilotingOrganization, this.organization.IsUpgradeOperationInProgress);
				}
			}
			UpgradeStatusTypes upgradeStatusTypes = this.organization.UpgradeStatus;
			if (this.organization.UpgradeRequest == UpgradeRequestTypes.PrestageUpgrade)
			{
				upgradeStatusTypes = UpgradeStatusTypes.Complete;
			}
			else if (this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgrade || this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgradeDryRun)
			{
				upgradeStatusTypes = ((this.organization.UpgradeStatus == UpgradeStatusTypes.Warning) ? UpgradeStatusTypes.Warning : UpgradeStatusTypes.InProgress);
			}
			if (upgradeStatusTypes != this.organization.UpgradeStatus)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "Changing Status from '{0}' to '{1}'", new object[]
				{
					this.organization.UpgradeStatus,
					upgradeStatusTypes
				});
				this.orgOperationProxy.SetOrganization(this.organization, upgradeStatusTypes, this.organization.UpgradeRequest, string.Empty, string.Empty, this.organization.UpgradeStage, -1, -1);
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000CC64 File Offset: 0x0000AE64
		private bool InBeforeStartOrgCmdletState()
		{
			switch (this.organization.UpgradeRequest)
			{
			case UpgradeRequestTypes.None:
			case UpgradeRequestTypes.PrestageUpgrade:
			case UpgradeRequestTypes.CancelPrestageUpgrade:
				if (!this.organization.IsPilotingOrganization || this.organization.IsUpgradeOperationInProgress || this.organization.AdminDisplayVersion.ExchangeBuild.Major != ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major)
				{
					return true;
				}
				break;
			case UpgradeRequestTypes.TenantUpgrade:
				if (!this.organization.IsUpgradingOrganization || this.organization.IsUpgradeOperationInProgress || this.organization.AdminDisplayVersion.ExchangeBuild.Major != ExchangeObjectVersion.Exchange2012.ExchangeBuild.Major)
				{
					return true;
				}
				break;
			case UpgradeRequestTypes.TenantUpgradeDryRun:
				if (!this.organization.IsPilotingOrganization && !this.organization.IsUpgradingOrganization && !this.organization.IsUpgradeOperationInProgress && this.organization.AdminDisplayVersion.ExchangeBuild.Major == ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000CD78 File Offset: 0x0000AF78
		private void VerifyOrganizationStateAfter()
		{
			if (this.organization.UpgradeRequest == UpgradeRequestTypes.TenantUpgrade && this.BeforeCompleteOrganzationCmdletState())
			{
				base.Context.Logger.Log(MigrationEventType.Information, "Need to invoke Complete-OrgUpgrade", new object[0]);
				this.LogUpgradeStageWithE14Counts(new UpgradeStage?(UpgradeStage.CompleteOrgUpgrade));
				this.orgOperationProxy.InvokeOrganizationCmdlet(this.organization.ExternalDirectoryOrganizationId, "Complete-OrganizationUpgrade", false);
				this.organization = this.orgOperationProxy.GetOrganization(this.organization.ExternalDirectoryOrganizationId);
				if (this.BeforeCompleteOrganzationCmdletState())
				{
					throw new InvalidOrganizationStateException(this.organization.ExternalDirectoryOrganizationId, this.organization.ServicePlan, this.organization.AdminDisplayVersion, this.organization.IsUpgradingOrganization, this.organization.IsPilotingOrganization, this.organization.IsUpgradeOperationInProgress);
				}
			}
			if (this.organization.UpgradeStatus != UpgradeStatusTypes.Complete)
			{
				base.Context.Logger.Log(MigrationEventType.Information, "Setting UpgradeStatus to Complete.", new object[0]);
				this.orgOperationProxy.SetOrganization(this.organization, UpgradeStatusTypes.Complete, this.organization.UpgradeRequest, string.Empty, string.Empty, null, -1, -1);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		private bool BeforeCompleteOrganzationCmdletState()
		{
			return this.organization.IsUpgradingOrganization || this.organization.IsUpgradeOperationInProgress || this.organization.AdminDisplayVersion.ExchangeBuild.Major != ExchangeObjectVersion.Exchange2012.ExchangeBuild.Major;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000CF00 File Offset: 0x0000B100
		private void LogUpgradeStageWithE14Counts(UpgradeStage? upgradeStage)
		{
			if (this.organization.UpgradeStage == upgradeStage && this.organization.UpgradeLastE14CountsUpdateTime != null && DateTime.UtcNow - this.e14CountsUpdateInterval < this.organization.UpgradeLastE14CountsUpdateTime)
			{
				return;
			}
			int num = 0;
			if (upgradeStage != null)
			{
				num = this.crossOrgMoveRequestCount + this.intraOrgMoveRequestCount + this.highPriorityMoveRequestCount;
				if (num > 0 || this.upgradeNotInProgressMoveRequestCount > 0)
				{
					this.organization.UpgradeStatus = UpgradeStatusTypes.Warning;
					this.organization.UpgradeDetails = string.Empty;
					this.organization.UpgradeMessage = string.Format("Upgrade blocked by {0} Move Request(s). NotQueuedOrInProgress:{1} CrossOrg:{2} IntraOrg:{3} High Priority:{4}", new object[]
					{
						this.upgradeNotInProgressMoveRequestCount,
						num,
						this.crossOrgMoveRequestCount,
						this.intraOrgMoveRequestCount,
						this.highPriorityMoveRequestCount
					});
				}
				else
				{
					this.organization.UpgradeStatus = UpgradeStatusTypes.InProgress;
					this.organization.UpgradeMessage = string.Empty;
					this.organization.UpgradeDetails = string.Empty;
				}
			}
			this.orgOperationProxy.SetOrganization(this.organization, this.organization.UpgradeStatus, this.organization.UpgradeRequest, this.organization.UpgradeMessage, this.organization.UpgradeDetails, upgradeStage, this.e14MbxCount, num);
			UpgradeBatchCreatorProgressLog.Write(this.organization, string.Empty, string.Empty, new int?(this.e14MbxCount), new int?(num));
			UpgradeBatchCreatorProgressLog.FlushLog();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		private void CompletePilotUsers(ICacheEntry cacheEntryProxy)
		{
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.MailboxRelease, MailboxRelease.E15.ToString()),
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveRelease, MailboxRelease.E15.ToString())
			});
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.UpgradeRequest, UpgradeRequestTypes.PilotUpgrade),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.UpgradeStatus, UpgradeStatusTypes.Complete)
			});
			IEnumerable<RecipientWrapper> enumerable = cacheEntryProxy.ADSessionProxy.FindPilotUsersADRawEntry(null, QueryScope.SubTree, filter, null, 0, UpgradeBatchCreatorScheduler.AddlADRawEntryProperties);
			int num = 0;
			int num2 = 0;
			foreach (RecipientWrapper user in enumerable)
			{
				try
				{
					this.orgOperationProxy.SetUser(user, UpgradeStatusTypes.Complete, UpgradeRequestTypes.PilotUpgrade, string.Empty, string.Empty, null);
					num++;
				}
				catch (MigrationPermanentException)
				{
					num2++;
				}
			}
			base.Context.Logger.Log(MigrationEventType.Information, "CompletePilotUsers. {0} succeeded. {1} failed.", new object[]
			{
				num,
				num2
			});
		}

		// Token: 0x040002F9 RID: 761
		public const char BatchFilenamePrefixSeparator = '_';

		// Token: 0x040002FA RID: 762
		private const string TempFileExtension = ".tmp";

		// Token: 0x040002FB RID: 763
		private const string FinalFileExtension = ".csv";

		// Token: 0x040002FC RID: 764
		private const string InProgressFileExtension = ".inprogress";

		// Token: 0x040002FD RID: 765
		private const string CsvHeader = "Guid,TenantPartitionHint,SourceDatabase,TargetDatabase,ArchiveOnly,Priority,CompletedRequestAgeLimit,Protect,PreventCompletion,BlockFinalization,WorkloadType";

		// Token: 0x040002FE RID: 766
		private static readonly Random random = new Random();

		// Token: 0x040002FF RID: 767
		private static readonly ADPropertyDefinition[] AddlMiniRecipientProps = new ADPropertyDefinition[]
		{
			SharedPropertyDefinitions.MailboxMoveFlags,
			SharedPropertyDefinitions.MailboxMoveStatus,
			SharedPropertyDefinitions.MailboxMoveBatchName
		};

		// Token: 0x04000300 RID: 768
		private static readonly PropertyDefinition[] AddlADRawEntryProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id
		};

		// Token: 0x04000301 RID: 769
		private static QueryFilter e14MailboxQueryFilter = null;

		// Token: 0x04000302 RID: 770
		private readonly TimeSpan e14CountsUpdateInterval;

		// Token: 0x04000303 RID: 771
		private IOrganizationOperation orgOperationProxy;

		// Token: 0x04000304 RID: 772
		private TenantOrganizationPresentationObjectWrapper organization;

		// Token: 0x04000305 RID: 773
		private int e14MbxCount;

		// Token: 0x04000306 RID: 774
		private int upgradeInProgressMoveRequestCount;

		// Token: 0x04000307 RID: 775
		private int upgradeNotInProgressMoveRequestCount;

		// Token: 0x04000308 RID: 776
		private int crossOrgMoveRequestCount;

		// Token: 0x04000309 RID: 777
		private int intraOrgMoveRequestCount;

		// Token: 0x0400030A RID: 778
		private int highPriorityMoveRequestCount;

		// Token: 0x0400030B RID: 779
		private string orgBatchDirectoryPath;

		// Token: 0x020000C3 RID: 195
		public enum MailboxType
		{
			// Token: 0x0400030D RID: 781
			None,
			// Token: 0x0400030E RID: 782
			Regular,
			// Token: 0x0400030F RID: 783
			RegularPilot,
			// Token: 0x04000310 RID: 784
			Arbitration,
			// Token: 0x04000311 RID: 785
			Discovery,
			// Token: 0x04000312 RID: 786
			CloudOnlyArchive,
			// Token: 0x04000313 RID: 787
			CloudOnlyArchivePilot,
			// Token: 0x04000314 RID: 788
			RegularSoftDeleted,
			// Token: 0x04000315 RID: 789
			CloudOnlyArchiveSoftDeleted
		}
	}
}
