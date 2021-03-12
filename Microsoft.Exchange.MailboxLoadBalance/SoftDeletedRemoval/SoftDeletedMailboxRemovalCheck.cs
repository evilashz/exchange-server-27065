using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x020000F9 RID: 249
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SoftDeletedMailboxRemovalCheck
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x00015129 File Offset: 0x00013329
		protected SoftDeletedMailboxRemovalCheck(SoftDeletedRemovalData data, IDirectoryProvider directory)
		{
			this.Data = data;
			this.targetDatabase = new Lazy<DirectoryDatabase>(new Func<DirectoryDatabase>(this.FetchTargetDatabase));
			this.directory = directory;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00015156 File Offset: 0x00013356
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0001515E File Offset: 0x0001335E
		private protected SoftDeletedRemovalData Data { protected get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00015167 File Offset: 0x00013367
		protected DirectoryDatabase TargetDatabase
		{
			get
			{
				return this.targetDatabase.Value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00015174 File Offset: 0x00013374
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0001517C File Offset: 0x0001337C
		private SoftDeletedMailboxRemovalCheck Next { get; set; }

		// Token: 0x06000786 RID: 1926 RVA: 0x00015188 File Offset: 0x00013388
		public SoftDeleteMailboxRemovalCheckRemoval GetRemovalResult()
		{
			return this.CheckRemoval() ?? this.CheckNext();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000151A7 File Offset: 0x000133A7
		public void SetNext(SoftDeletedMailboxRemovalCheck next)
		{
			this.Next = next;
		}

		// Token: 0x06000788 RID: 1928
		protected abstract SoftDeleteMailboxRemovalCheckRemoval CheckRemoval();

		// Token: 0x06000789 RID: 1929 RVA: 0x000151B0 File Offset: 0x000133B0
		private SoftDeleteMailboxRemovalCheckRemoval CheckNext()
		{
			if (this.Next == null)
			{
				return SoftDeleteMailboxRemovalCheckRemoval.AllowRemoval();
			}
			return this.Next.GetRemovalResult();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000151CB File Offset: 0x000133CB
		private DirectoryDatabase FetchTargetDatabase()
		{
			return (DirectoryDatabase)this.directory.GetDirectoryObject(this.Data.TargetDatabase);
		}

		// Token: 0x040002E4 RID: 740
		private readonly IDirectoryProvider directory;

		// Token: 0x040002E5 RID: 741
		private readonly Lazy<DirectoryDatabase> targetDatabase;
	}
}
