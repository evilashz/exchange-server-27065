using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200002B RID: 43
	internal class EhfRecipientTargetConnection : EhfTargetConnection
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000DF1D File Offset: 0x0000C11D
		public EhfRecipientTargetConnection(int localServerVersion, EhfTargetServerConfig config, EhfSynchronizationProvider provider, EdgeSyncLogSession logSession) : base(localServerVersion, config, provider.RecipientSyncInterval, logSession)
		{
			this.provider = provider;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000DF38 File Offset: 0x0000C138
		public EhfRecipientTargetConnection(int localServerVersion, EhfTargetServerConfig config, EdgeSyncLogSession logSession, EhfPerfCounterHandler perfCounterHandler, IAdminSyncService adminSyncservice, EhfADAdapter adapter, EnhancedTimeSpan syncInterval, EhfSynchronizationProvider provider) : base(localServerVersion, config, logSession, perfCounterHandler, null, null, adminSyncservice, adapter, syncInterval)
		{
			this.provider = provider;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000DF60 File Offset: 0x0000C160
		public EhfAdminAccountSynchronizer AdminAccountSynchronizer
		{
			get
			{
				return this.adminSync;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000DF68 File Offset: 0x0000C168
		public override bool SkipSyncCycle
		{
			get
			{
				return !base.Config.AdminSyncEnabled;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000DF78 File Offset: 0x0000C178
		public override void AbortSyncCycle(Exception cause)
		{
			if (this.adminSync != null)
			{
				this.adminSync.ClearBatches();
			}
			base.AbortSyncCycle(cause);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DF94 File Offset: 0x0000C194
		public override bool OnSynchronizing()
		{
			bool result = base.OnSynchronizing();
			if (this.adminSync != null)
			{
				throw new InvalidOperationException("Admin sync already exists");
			}
			this.provider.AdminSyncErrorTracker.PrepareForNewSyncCycle(this.SyncInterval);
			this.adminSync = new EhfAdminAccountSynchronizer(this, this.provider.AdminSyncErrorTracker);
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000DFE9 File Offset: 0x0000C1E9
		public override bool OnSynchronized()
		{
			if (this.adminSync != null && !this.adminSync.FlushBatches())
			{
				return false;
			}
			this.provider.AdminSyncErrorTracker.AbortSyncCycleIfRequired(this.adminSync, base.DiagSession);
			return true;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E020 File Offset: 0x0000C220
		public override SyncResult OnAddEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			SyncResult result = SyncResult.Added;
			EhfRecipientTargetConnection.AdminObjectType syncObjectType = this.GetSyncObjectType(entry, "Add");
			switch (syncObjectType)
			{
			case EhfRecipientTargetConnection.AdminObjectType.MailboxUser:
				this.adminSync.HandleWlidAddedEvent(entry);
				break;
			case EhfRecipientTargetConnection.AdminObjectType.Organization:
				this.adminSync.HandleOrganizationAddedEvent(entry);
				break;
			case EhfRecipientTargetConnection.AdminObjectType.UniversalSecurityGroup:
				this.adminSync.HandleGroupAddedEvent(entry);
				break;
			default:
				throw new InvalidOperationException("EhfRecipientTargetConnection.GetSyncObjectType() returned unexpected value " + syncObjectType);
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E094 File Offset: 0x0000C294
		public override SyncResult OnModifyEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			SyncResult result = SyncResult.Modified;
			EhfRecipientTargetConnection.AdminObjectType syncObjectType = this.GetSyncObjectType(entry, "Modify");
			switch (syncObjectType)
			{
			case EhfRecipientTargetConnection.AdminObjectType.MailboxUser:
				this.adminSync.HandleWlidChangedEvent(entry);
				break;
			case EhfRecipientTargetConnection.AdminObjectType.Organization:
				this.adminSync.HandleOrganizationChangedEvent(entry);
				break;
			case EhfRecipientTargetConnection.AdminObjectType.UniversalSecurityGroup:
				this.adminSync.HandleGroupChangedEvent(entry);
				break;
			default:
				throw new InvalidOperationException("EhfRecipientTargetConnection.GetSyncObjectType() returned unexpected value " + syncObjectType);
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E108 File Offset: 0x0000C308
		public override SyncResult OnDeleteEntry(ExSearchResultEntry entry)
		{
			EhfRecipientTargetConnection.AdminObjectType syncObjectType = this.GetSyncObjectType(entry, "Delete");
			SyncResult result;
			switch (syncObjectType)
			{
			case EhfRecipientTargetConnection.AdminObjectType.MailboxUser:
				this.adminSync.HandleWlidDeletedEvent(entry);
				result = SyncResult.Deleted;
				break;
			case EhfRecipientTargetConnection.AdminObjectType.Organization:
				this.adminSync.HandleOrganizationDeletedEvent(entry);
				result = SyncResult.Deleted;
				break;
			case EhfRecipientTargetConnection.AdminObjectType.UniversalSecurityGroup:
				this.adminSync.HandleGroupDeletedEvent(entry);
				result = SyncResult.Deleted;
				break;
			default:
				throw new InvalidOperationException("EhfRecipientTargetConnection.GetSyncObjectType() returned unexpected value " + syncObjectType);
			}
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E184 File Offset: 0x0000C384
		private EhfRecipientTargetConnection.AdminObjectType GetSyncObjectType(ExSearchResultEntry entry, string operation)
		{
			string objectClass = entry.ObjectClass;
			if (string.IsNullOrEmpty(objectClass))
			{
				base.DiagSession.LogAndTraceError("Entry <{0}> contains no objectClass attribute in operation {1}; cannot proceed", new object[]
				{
					entry.DistinguishedName,
					operation
				});
				throw new ArgumentException("Entry does not contain objectClass attribute", "entry");
			}
			string a;
			if ((a = objectClass) != null)
			{
				if (a == "group")
				{
					return EhfRecipientTargetConnection.AdminObjectType.UniversalSecurityGroup;
				}
				if (a == "user")
				{
					return EhfRecipientTargetConnection.AdminObjectType.MailboxUser;
				}
				if (a == "organizationalUnit")
				{
					return EhfRecipientTargetConnection.AdminObjectType.Organization;
				}
			}
			base.DiagSession.LogAndTraceError("Entry <{0}> contains unexpected objectClass value <{1}> in operation {2}; cannot proceed", new object[]
			{
				entry.DistinguishedName,
				objectClass,
				operation
			});
			throw new ArgumentException("Entry's objectClass is invalid: " + objectClass, "entry");
		}

		// Token: 0x040000AC RID: 172
		private EhfAdminAccountSynchronizer adminSync;

		// Token: 0x040000AD RID: 173
		private EhfSynchronizationProvider provider;

		// Token: 0x0200002C RID: 44
		private enum AdminObjectType
		{
			// Token: 0x040000AF RID: 175
			MailboxUser,
			// Token: 0x040000B0 RID: 176
			Organization,
			// Token: 0x040000B1 RID: 177
			UniversalSecurityGroup,
			// Token: 0x040000B2 RID: 178
			Unknown
		}
	}
}
