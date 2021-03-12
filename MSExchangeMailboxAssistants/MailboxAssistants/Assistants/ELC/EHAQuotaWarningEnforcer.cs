using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A9 RID: 169
	internal class EHAQuotaWarningEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x00031700 File Offset: 0x0002F900
		internal EHAQuotaWarningEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0003170A File Offset: 0x0002F90A
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by EHAQuotaWarningEnforcer.";
			}
			return this.toString;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00031744 File Offset: 0x0002F944
		protected override bool QueryIsEnabled()
		{
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: All the checks below make sense only for DC and not for enterprise.", this);
				return false;
			}
			if (!base.MailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages)
			{
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: Organization's ProcessEhaMigratedMessages settings is set to false. This mailbox will not be processed for migration messages", this);
				return false;
			}
			TransportConfigContainer tenantTransportConfig = EHAQuotaWarningEnforcer.GetTenantTransportConfig(base.MailboxDataForTags.ElcUserTagInformation.ADUser.OrganizationId, this);
			if (tenantTransportConfig != null && !tenantTransportConfig.LegacyArchiveJournalingEnabled)
			{
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: EHA Migration is already complete for this tenant, Skip Quota enforcer check.", this);
				return false;
			}
			if (base.MailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: This is archive mailbox. This mailbox will not be processed for migration messages", this);
				return false;
			}
			if (!this.IsMailboxInteresting())
			{
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: This is not one of the eha mailboxes : journal ndr or confirmation mailbox. This mailbox will be skipped.", this);
				return false;
			}
			return true;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00031834 File Offset: 0x0002FA34
		private bool IsMailboxInteresting()
		{
			if (this.MatchEhaConfigurationMailbox(EHAQuotaWarningEnforcer.MailboxType.EhaJournalNdrMailbox, base.MailboxDataForTags.ElcUserInformation.ADUser.EmailAddresses))
			{
				this.mailboxType = EHAQuotaWarningEnforcer.MailboxType.EhaJournalNdrMailbox;
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: This is journal ndr mailbox. It will be processed for quota warning alert.", this);
				return true;
			}
			if (this.MatchEhaConfigurationMailbox(EHAQuotaWarningEnforcer.MailboxType.EhaConfirmationMailbox, base.MailboxDataForTags.ElcUserInformation.ADUser.EmailAddresses))
			{
				this.mailboxType = EHAQuotaWarningEnforcer.MailboxType.EhaConfirmationMailbox;
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: This is confirmation mailbox. It will be processed for quota warning alert.", this);
				return true;
			}
			if (this.CheckForMigrationFolderAndSubFolderCounts())
			{
				this.mailboxType = EHAQuotaWarningEnforcer.MailboxType.EhaTenantMailbox;
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: This is eha mailbox we need to check if it is overquota.", this);
				return true;
			}
			return false;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000318E8 File Offset: 0x0002FAE8
		private bool MatchEhaConfigurationMailbox(EHAQuotaWarningEnforcer.MailboxType type, ProxyAddressCollection proxyAddresses)
		{
			string value = string.Empty;
			if (type == EHAQuotaWarningEnforcer.MailboxType.EhaConfirmationMailbox)
			{
				value = ElcGlobals.ConfirmationMailboxAlias;
			}
			if (type == EHAQuotaWarningEnforcer.MailboxType.EhaJournalNdrMailbox)
			{
				value = ElcGlobals.EhaMigrationMailboxName;
			}
			if (proxyAddresses != null && proxyAddresses.Count > 0 && !string.IsNullOrEmpty(value))
			{
				foreach (ProxyAddress proxyAddress in proxyAddresses)
				{
					if (proxyAddress != null && !string.IsNullOrEmpty(proxyAddress.AddressString) && proxyAddress.AddressString.ToLower().Contains(value))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0003198C File Offset: 0x0002FB8C
		private bool CheckForMigrationFolderAndSubFolderCounts()
		{
			MailboxSession mailboxSession = base.MailboxDataForTags.MailboxSession;
			using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.Configuration))
			{
				StoreId folderId = this.GetFolderId(base.MailboxDataForTags.MailboxSession, folder.Id, ElcGlobals.MigrationFolderName);
				if (folderId != null)
				{
					this.LogEhaMessageCount(folderId, mailboxSession);
					StoreId folderId2 = this.GetFolderId(base.MailboxDataForTags.MailboxSession, folderId, DefaultFolderType.Inbox.ToString());
					if (folderId2 != null && this.IfFolderHasItems(folderId2, mailboxSession))
					{
						return true;
					}
					StoreId folderId3 = this.GetFolderId(base.MailboxDataForTags.MailboxSession, folderId, DefaultFolderType.SentItems.ToString());
					if (folderId3 != null && this.IfFolderHasItems(folderId3, mailboxSession))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00031A5C File Offset: 0x0002FC5C
		private bool IfFolderHasItems(StoreId folderId, MailboxSession session)
		{
			bool result;
			using (Folder folder = Folder.Bind(session, folderId, new PropertyDefinition[]
			{
				FolderSchema.ItemCount
			}))
			{
				result = (folder.ItemCount > 0);
			}
			return result;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00031AA8 File Offset: 0x0002FCA8
		private static TransportConfigContainer GetTenantTransportConfig(OrganizationId orgId, EHAQuotaWarningEnforcer enforcer)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 349, "GetTenantTransportConfig", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\SysCleanupAssistant\\EHAQuotaWarningEnforcer.cs");
			PerTenantTransportSettings perTenantTransportSettings = new PerTenantTransportSettings(orgId);
			return perTenantTransportSettings.ReadTransportConfig(tenantOrTopologyConfigurationSession);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		private void LogEhaMessageCount(StoreId folderId, MailboxSession session)
		{
			using (Folder folder = Folder.Bind(session, folderId, new PropertyDefinition[]
			{
				FolderSchema.EhaMigrationMessageCount
			}))
			{
				object obj = folder.TryGetProperty(FolderSchema.EhaMigrationMessageCount);
				if (obj != null && obj is long)
				{
					base.MailboxDataForTags.StatisticsLogEntry.EhaMigrationMessageCount += (long)obj;
					EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, string, object>((long)this.GetHashCode(), "{0}: Total eha messages migrated so far {1} : {2}", this, session.DisplayName, obj);
				}
				else
				{
					StatisticsLogEntry statisticsLogEntry = base.MailboxDataForTags.StatisticsLogEntry;
					statisticsLogEntry.EhaMigrationMessageCount = statisticsLogEntry.EhaMigrationMessageCount;
					EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, string>((long)this.GetHashCode(), "{0}: Property EhaMigrationMessageCount not found on mailbox folder {1}", this, session.DisplayName);
				}
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		private StoreId GetFolderId(MailboxSession session, StoreId rootFolderId, string ChildFolderName)
		{
			StoreId result;
			using (Folder folder = Folder.Bind(session, rootFolderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, EHAQuotaWarningEnforcer.DataColumns))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, ChildFolderName);
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, string>((long)this.GetHashCode(), "{0}: Folder not found {1}", this, ChildFolderName);
							result = null;
						}
						else
						{
							StoreObjectId objectId = (rows[0][0] as VersionedId).ObjectId;
							string arg = rows[0][1] as string;
							EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, string>((long)this.GetHashCode(), "{0}: Found MigrationFolder , Display Name {1}", this, arg);
							result = objectId;
						}
					}
					else
					{
						EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, string>((long)this.GetHashCode(), "{0}: Folder not found {1}", this, ChildFolderName);
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		protected override void CollectItemsToExpire()
		{
			if (this.IsMailboxOverQuota())
			{
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_EhaMailboxQuotaWarning, null, new object[]
				{
					this.mailboxType.ToString() + " : " + base.MailboxDataForTags.MailboxSmtpAddress,
					base.MailboxDataForTags.ElcUserTagInformation.ADUser.OrganizationId,
					this.mailboxSize,
					this.mailboxQuota,
					base.MailboxDataForTags.MailboxSession.MailboxOwner
				});
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00031D44 File Offset: 0x0002FF44
		private bool IsMailboxOverQuota()
		{
			bool? useDatabaseQuotaDefaults = base.MailboxDataForTags.ElcUserInformation.ADUser.UseDatabaseQuotaDefaults;
			if (useDatabaseQuotaDefaults != null && !useDatabaseQuotaDefaults.Value)
			{
				this.mailboxQuota = base.MailboxDataForTags.ElcUserInformation.ADUser.IssueWarningQuota;
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. Using IssueWarningQuota limit from the mailbox object = {2}.", this, useDatabaseQuotaDefaults, this.mailboxQuota);
			}
			else
			{
				if (base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota == null)
				{
					EHAQuotaWarningEnforcer.Tracer.TraceError<EHAQuotaWarningEnforcer>((long)this.GetHashCode(), "{0}: We could not get IssueWarningQuota of this mailbox database. Skipping it.", this);
					return false;
				}
				this.mailboxQuota = base.SysCleanupSubAssistant.DatabaseConfig.DatabaseIssueWarningQuota.Value;
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. Mailbox.RecoverableItemsWarningQuota from database object = {2}.", this, useDatabaseQuotaDefaults, this.mailboxQuota);
			}
			if (this.mailboxQuota.IsUnlimited)
			{
				EHAQuotaWarningEnforcer.Tracer.TraceDebug<EHAQuotaWarningEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. Mailboxquota is unlimited. Skipping it.", this, useDatabaseQuotaDefaults, this.mailboxQuota);
				return false;
			}
			long? totalItemSize = base.MailboxDataForTags.MailboxSession.ReadMailboxCountsAndSizes().TotalItemSize;
			if (totalItemSize != null)
			{
				this.mailboxSize = totalItemSize.Value;
				double num = EHAQuotaWarningEnforcer.MailboxQuotaWarningPercentage * this.mailboxQuota.Value.ToBytes();
				return (double)this.mailboxSize > num;
			}
			return false;
		}

		// Token: 0x040004BE RID: 1214
		private static Unlimited<ByteQuantifiedSize> DefaultWarningQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(30UL));

		// Token: 0x040004BF RID: 1215
		private static readonly Trace Tracer = ExTraceGlobals.SupplementExpirationEnforcerTracer;

		// Token: 0x040004C0 RID: 1216
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040004C1 RID: 1217
		public static readonly double MailboxQuotaWarningPercentage = 0.75;

		// Token: 0x040004C2 RID: 1218
		private Unlimited<ByteQuantifiedSize> mailboxQuota;

		// Token: 0x040004C3 RID: 1219
		private long mailboxSize;

		// Token: 0x040004C4 RID: 1220
		private EHAQuotaWarningEnforcer.MailboxType mailboxType;

		// Token: 0x040004C5 RID: 1221
		private string toString;

		// Token: 0x040004C6 RID: 1222
		private static readonly PropertyDefinition[] DataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName
		};

		// Token: 0x020000AA RID: 170
		private enum MailboxType
		{
			// Token: 0x040004C8 RID: 1224
			EhaJournalNdrMailbox,
			// Token: 0x040004C9 RID: 1225
			EhaConfirmationMailbox,
			// Token: 0x040004CA RID: 1226
			EhaTenantMailbox
		}
	}
}
