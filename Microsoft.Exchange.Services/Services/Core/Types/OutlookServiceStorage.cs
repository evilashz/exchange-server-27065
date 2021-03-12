using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F65 RID: 3941
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OutlookServiceStorage : DisposableObject, IOutlookServiceStorage, IDisposable
	{
		// Token: 0x060063BC RID: 25532 RVA: 0x0013703C File Offset: 0x0013523C
		private OutlookServiceStorage(IMailboxSession mailboxSession, IFolder folder, string tenantId)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("folder", folder);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("tenantId", tenantId);
			this.mailboxSession = mailboxSession;
			this.folder = folder;
			this.TenantId = tenantId;
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060063BD RID: 25533 RVA: 0x0013707A File Offset: 0x0013527A
		// (set) Token: 0x060063BE RID: 25534 RVA: 0x00137082 File Offset: 0x00135282
		public string TenantId { get; private set; }

		// Token: 0x060063BF RID: 25535 RVA: 0x0013708B File Offset: 0x0013528B
		public static IOutlookServiceStorage Create(IMailboxSession mailboxSession)
		{
			return OutlookServiceStorage.Create(mailboxSession, XSOFactory.Default);
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x00137098 File Offset: 0x00135298
		public static IOutlookServiceStorage Create(IMailboxSession mailboxSession, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			IOutlookServiceStorage outlookServiceStorage = OutlookServiceStorage.Find(mailboxSession, xsoFactory);
			if (outlookServiceStorage != null)
			{
				return outlookServiceStorage;
			}
			if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string>(0L, "OutlookServiceStorage.Create: Creating a new Notification Subscription folder for user {0}.", (mailboxSession.MailboxOwner != null && mailboxSession.MailboxOwner.ObjectId != null) ? mailboxSession.MailboxOwner.ObjectId.ToDNString() : string.Empty);
			}
			string tenantId = OutlookServiceStorage.GetTenantId(mailboxSession);
			StoreObjectId folderId = mailboxSession.CreateDefaultFolder(DefaultFolderType.OutlookService);
			IFolder folder = xsoFactory.BindToFolder(mailboxSession, folderId);
			return new OutlookServiceStorage(mailboxSession, folder, tenantId);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x00137133 File Offset: 0x00135333
		public static IOutlookServiceStorage Find(IMailboxSession mailboxSession)
		{
			return OutlookServiceStorage.Find(mailboxSession, XSOFactory.Default);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x00137140 File Offset: 0x00135340
		public static IOutlookServiceStorage Find(IMailboxSession mailboxSession, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.OutlookService);
			if (defaultFolderId == null)
			{
				return null;
			}
			IFolder folder = xsoFactory.BindToFolder(mailboxSession, defaultFolderId);
			string tenantId = OutlookServiceStorage.GetTenantId(mailboxSession);
			return new OutlookServiceStorage(mailboxSession, folder, tenantId);
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0013718C File Offset: 0x0013538C
		private static string GetTenantId(IStoreSession session)
		{
			string result = string.Empty;
			byte[] valueOrDefault = session.Mailbox.GetValueOrDefault<byte[]>(MailboxSchema.PersistableTenantPartitionHint, null);
			if (valueOrDefault != null && valueOrDefault.Length > 0)
			{
				TenantPartitionHint tenantPartitionHint = TenantPartitionHint.FromPersistablePartitionHint(valueOrDefault);
				Guid externalDirectoryOrganizationId = tenantPartitionHint.GetExternalDirectoryOrganizationId();
				result = (Guid.Empty.Equals(externalDirectoryOrganizationId) ? string.Empty : externalDirectoryOrganizationId.ToString());
			}
			return result;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x001371F0 File Offset: 0x001353F0
		internal static OutlookServiceStorage GetOutlookServiceFolder(IMailboxSession mailboxSession, Folder folder)
		{
			string tenantId = OutlookServiceStorage.GetTenantId(mailboxSession);
			return new OutlookServiceStorage(mailboxSession, folder, tenantId);
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0013720C File Offset: 0x0013540C
		public IOutlookServiceSubscriptionStorage GetOutlookServiceSubscriptionStorage()
		{
			return OutlookServiceSubscriptionStorage.Create(this.mailboxSession, this.folder, this.TenantId);
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x00137225 File Offset: 0x00135425
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<OutlookServiceStorage>(this);
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0013722D File Offset: 0x0013542D
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x04003521 RID: 13601
		private IFolder folder;

		// Token: 0x04003522 RID: 13602
		private IMailboxSession mailboxSession;
	}
}
