using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000279 RID: 633
	internal sealed class Tracker
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00054760 File Offset: 0x00052960
		public bool IsPending(Guid mailboxGuid, StoreId folderId)
		{
			string item = this.ConvertToKey(mailboxGuid, folderId);
			bool result;
			lock (this.locker)
			{
				result = this.pending.Contains(item);
			}
			return result;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000547B4 File Offset: 0x000529B4
		public bool Start(Guid mailboxGuid, StoreId folderId)
		{
			string item = this.ConvertToKey(mailboxGuid, folderId);
			lock (this.locker)
			{
				if (this.pending.Contains(item))
				{
					return false;
				}
				this.pending.Add(item);
			}
			return true;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0005481C File Offset: 0x00052A1C
		public void End(Guid mailboxGuid, StoreId folderId)
		{
			string item = this.ConvertToKey(mailboxGuid, folderId);
			lock (this.locker)
			{
				this.pending.Remove(item);
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0005486C File Offset: 0x00052A6C
		private string ConvertToKey(Guid mailboxGuid, StoreId folderId)
		{
			return mailboxGuid.ToString() + "/" + folderId.ToBase64String();
		}

		// Token: 0x04000BE2 RID: 3042
		private HashSet<string> pending = new HashSet<string>();

		// Token: 0x04000BE3 RID: 3043
		private object locker = new object();
	}
}
