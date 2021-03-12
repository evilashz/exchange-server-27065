using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000044 RID: 68
	public struct FidMid : IEquatable<FidMid>, IComparable<FidMid>
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x0003C7E6 File Offset: 0x0003A9E6
		public FidMid(ExchangeId folderId, ExchangeId messageId)
		{
			this.folderId = folderId;
			this.messageId = messageId;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0003C7F6 File Offset: 0x0003A9F6
		public ExchangeId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0003C7FE File Offset: 0x0003A9FE
		public ExchangeId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0003C806 File Offset: 0x0003AA06
		public bool Equals(FidMid other)
		{
			return this.MessageId == other.MessageId && this.FolderId == other.FolderId;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0003C830 File Offset: 0x0003AA30
		public override bool Equals(object obj)
		{
			return obj is FidMid && ((FidMid)obj).Equals(this);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0003C85C File Offset: 0x0003AA5C
		public override int GetHashCode()
		{
			return this.folderId.GetHashCode() ^ this.messageId.GetHashCode();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0003C894 File Offset: 0x0003AA94
		public int CompareTo(FidMid other)
		{
			int num = this.FolderId.CompareTo(other.FolderId);
			if (num == 0)
			{
				num = this.MessageId.CompareTo(other.MessageId);
			}
			return num;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003C8D1 File Offset: 0x0003AAD1
		public override string ToString()
		{
			return string.Format("FidMid({0}:{1})", this.folderId, this.messageId);
		}

		// Token: 0x04000381 RID: 897
		private readonly ExchangeId folderId;

		// Token: 0x04000382 RID: 898
		private readonly ExchangeId messageId;
	}
}
