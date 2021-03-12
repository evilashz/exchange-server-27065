using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000948 RID: 2376
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderMailboxSynchronizerManager
	{
		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0016BDB0 File Offset: 0x00169FB0
		public static PublicFolderMailboxSynchronizerManager Instance
		{
			get
			{
				return PublicFolderMailboxSynchronizerManager.singleton;
			}
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x0016BDD4 File Offset: 0x00169FD4
		public PublicFolderMailboxSynchronizerReference GetPublicFolderMailboxSynchronizer(IExchangePrincipal publicFolderMailboxPrincipal, bool onlyRefCounting, bool forHierarchyAccess)
		{
			PublicFolderMailboxSynchronizerReference result;
			lock (this.lockObject)
			{
				PublicFolderMailboxSynchronizerManager.PublicFolderMailboxSynchronizerAndReferenceCount publicFolderMailboxSynchronizerAndReferenceCount;
				if (!this.publicFolderMailboxSynchronizers.TryGetValue(publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, out publicFolderMailboxSynchronizerAndReferenceCount))
				{
					PublicFolderMailboxSynchronizer publicFolderMailboxSynchronizer2 = new PublicFolderMailboxSynchronizer(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, publicFolderMailboxPrincipal.MailboxInfo.Location.ServerFqdn, onlyRefCounting);
					publicFolderMailboxSynchronizerAndReferenceCount = new PublicFolderMailboxSynchronizerManager.PublicFolderMailboxSynchronizerAndReferenceCount(publicFolderMailboxSynchronizer2);
					this.publicFolderMailboxSynchronizers[publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid] = publicFolderMailboxSynchronizerAndReferenceCount;
				}
				publicFolderMailboxSynchronizerAndReferenceCount.ReferenceCount++;
				if (forHierarchyAccess)
				{
					publicFolderMailboxSynchronizerAndReferenceCount.HierarchyAccessReferenceCount++;
				}
				result = new PublicFolderMailboxSynchronizerReference(publicFolderMailboxSynchronizerAndReferenceCount.PublicFolderMailboxSynchronizer, delegate(PublicFolderMailboxSynchronizer publicFolderMailboxSynchronizer)
				{
					this.OnReferenceDisposed(publicFolderMailboxSynchronizer, forHierarchyAccess);
				});
			}
			return result;
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x0016BED4 File Offset: 0x0016A0D4
		public int GetActiveReferenceCount(IExchangePrincipal publicFolderMailboxPrincipal)
		{
			return this.publicFolderMailboxSynchronizers[publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid].ReferenceCount;
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x0016BEF1 File Offset: 0x0016A0F1
		public int GetActiveHierarchyAccessReferenceCount(IExchangePrincipal publicFolderMailboxPrincipal)
		{
			return this.publicFolderMailboxSynchronizers[publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid].HierarchyAccessReferenceCount;
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0016BF0E File Offset: 0x0016A10E
		public ExDateTime GetAlertIssuedTime(IExchangePrincipal publicFolderMailboxPrincipal)
		{
			return this.publicFolderMailboxSynchronizers[publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid].AlertIssuedTime;
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0016BF2B File Offset: 0x0016A12B
		public void SetAlertIssuedTime(IExchangePrincipal publicFolderMailboxPrincipal, ExDateTime alertTime)
		{
			this.publicFolderMailboxSynchronizers[publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid].AlertIssuedTime = alertTime;
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0016BF4C File Offset: 0x0016A14C
		private void OnReferenceDisposed(PublicFolderMailboxSynchronizer publicFolderMailboxSynchronizer, bool forHierarchyAccess)
		{
			bool flag = false;
			lock (this.lockObject)
			{
				PublicFolderMailboxSynchronizerManager.PublicFolderMailboxSynchronizerAndReferenceCount publicFolderMailboxSynchronizerAndReferenceCount;
				if (!this.publicFolderMailboxSynchronizers.TryGetValue(publicFolderMailboxSynchronizer.MailboxGuid, out publicFolderMailboxSynchronizerAndReferenceCount) || publicFolderMailboxSynchronizerAndReferenceCount.ReferenceCount == 0)
				{
					throw new InvalidOperationException("The public folder mailbox synchronizer has already been removed. This should not happen. ReferenceCount = " + ((publicFolderMailboxSynchronizerAndReferenceCount == null) ? -1 : publicFolderMailboxSynchronizerAndReferenceCount.ReferenceCount));
				}
				publicFolderMailboxSynchronizerAndReferenceCount.ReferenceCount--;
				if (forHierarchyAccess)
				{
					publicFolderMailboxSynchronizerAndReferenceCount.HierarchyAccessReferenceCount--;
				}
				if (publicFolderMailboxSynchronizerAndReferenceCount.ReferenceCount == 0)
				{
					this.publicFolderMailboxSynchronizers.Remove(publicFolderMailboxSynchronizer.MailboxGuid);
					flag = true;
				}
			}
			if (flag)
			{
				publicFolderMailboxSynchronizer.Dispose();
			}
		}

		// Token: 0x0400303A RID: 12346
		private static PublicFolderMailboxSynchronizerManager singleton = new PublicFolderMailboxSynchronizerManager();

		// Token: 0x0400303B RID: 12347
		private Dictionary<Guid, PublicFolderMailboxSynchronizerManager.PublicFolderMailboxSynchronizerAndReferenceCount> publicFolderMailboxSynchronizers = new Dictionary<Guid, PublicFolderMailboxSynchronizerManager.PublicFolderMailboxSynchronizerAndReferenceCount>();

		// Token: 0x0400303C RID: 12348
		private object lockObject = new object();

		// Token: 0x02000949 RID: 2377
		private class PublicFolderMailboxSynchronizerAndReferenceCount
		{
			// Token: 0x0600587D RID: 22653 RVA: 0x0016C036 File Offset: 0x0016A236
			public PublicFolderMailboxSynchronizerAndReferenceCount(PublicFolderMailboxSynchronizer publicFolderMailboxSynchronizer)
			{
				this.PublicFolderMailboxSynchronizer = publicFolderMailboxSynchronizer;
			}

			// Token: 0x0400303D RID: 12349
			public readonly PublicFolderMailboxSynchronizer PublicFolderMailboxSynchronizer;

			// Token: 0x0400303E RID: 12350
			public int ReferenceCount;

			// Token: 0x0400303F RID: 12351
			public int HierarchyAccessReferenceCount;

			// Token: 0x04003040 RID: 12352
			public ExDateTime AlertIssuedTime = ExDateTime.MinValue;
		}
	}
}
