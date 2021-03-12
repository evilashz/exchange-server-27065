using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A7C RID: 2684
	[Serializable]
	public class MailboxStoreObjectId : XsoMailboxObjectId
	{
		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x0019F39A File Offset: 0x0019D59A
		internal StoreObjectId StoreObjectId
		{
			get
			{
				return this.storeObjectId;
			}
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x0019F3A2 File Offset: 0x0019D5A2
		internal MailboxStoreObjectId(ADObjectId mailboxOwnerId, StoreObjectId storeObjectId) : base(mailboxOwnerId)
		{
			this.storeObjectId = storeObjectId;
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x0019F3B4 File Offset: 0x0019D5B4
		public override byte[] GetBytes()
		{
			byte[] bytes = this.StoreObjectId.GetBytes();
			byte[] bytes2 = base.MailboxOwnerId.GetBytes();
			byte[] array = new byte[bytes.Length + bytes2.Length + 2];
			int num = 0;
			array[num++] = (byte)(bytes2.Length & 255);
			array[num++] = (byte)(bytes2.Length >> 8 & 255);
			Array.Copy(bytes2, 0, array, num, bytes2.Length);
			num += bytes2.Length;
			Array.Copy(bytes, 0, array, num, bytes.Length);
			return array;
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x0019F42D File Offset: 0x0019D62D
		public override int GetHashCode()
		{
			return base.MailboxOwnerId.GetHashCode() ^ this.StoreObjectId.GetHashCode();
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x0019F448 File Offset: 0x0019D648
		public override bool Equals(XsoMailboxObjectId other)
		{
			MailboxStoreObjectId mailboxStoreObjectId = other as MailboxStoreObjectId;
			return !(null == mailboxStoreObjectId) && ADObjectId.Equals(base.MailboxOwnerId, other.MailboxOwnerId) && object.Equals(this.storeObjectId, mailboxStoreObjectId.StoreObjectId);
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x0019F48D File Offset: 0x0019D68D
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", base.MailboxOwnerId, '\\', this.StoreObjectId.ToString());
		}

		// Token: 0x040037B7 RID: 14263
		public const char MailboxAndStoreObjectSeperator = '\\';

		// Token: 0x040037B8 RID: 14264
		private StoreObjectId storeObjectId;
	}
}
