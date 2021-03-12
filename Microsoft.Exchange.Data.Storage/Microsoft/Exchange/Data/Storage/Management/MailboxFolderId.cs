using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A8F RID: 2703
	[Serializable]
	public class MailboxFolderId : XsoMailboxObjectId
	{
		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x060062FE RID: 25342 RVA: 0x001A1EFF File Offset: 0x001A00FF
		// (set) Token: 0x060062FF RID: 25343 RVA: 0x001A1F07 File Offset: 0x001A0107
		public MapiFolderPath MailboxFolderPath { get; private set; }

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06006300 RID: 25344 RVA: 0x001A1F10 File Offset: 0x001A0110
		// (set) Token: 0x06006301 RID: 25345 RVA: 0x001A1F18 File Offset: 0x001A0118
		internal StoreObjectId StoreObjectIdValue { get; private set; }

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06006302 RID: 25346 RVA: 0x001A1F21 File Offset: 0x001A0121
		public string StoreObjectId
		{
			get
			{
				if (string.IsNullOrEmpty(this.storeObjectId))
				{
					this.storeObjectId = ((this.StoreObjectIdValue == null) ? string.Empty : this.StoreObjectIdValue.ToString());
				}
				return this.storeObjectId;
			}
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x001A1F56 File Offset: 0x001A0156
		internal MailboxFolderId(ADObjectId mailboxOwnerId, StoreObjectId storeObjectId, MapiFolderPath folderPath) : base(mailboxOwnerId)
		{
			if (null == folderPath && storeObjectId == null)
			{
				throw new ArgumentException(ServerStrings.ErrorNoStoreObjectIdAndFolderPath);
			}
			this.StoreObjectIdValue = storeObjectId;
			this.MailboxFolderPath = folderPath;
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x001A1F8C File Offset: 0x001A018C
		public override byte[] GetBytes()
		{
			byte[] array = (this.StoreObjectIdValue == null) ? Array<byte>.Empty : this.StoreObjectIdValue.GetBytes();
			byte[] bytes = base.MailboxOwnerId.GetBytes();
			byte[] array2 = new byte[array.Length + bytes.Length + 2];
			int num = 0;
			array2[num++] = (byte)(bytes.Length & 255);
			array2[num++] = (byte)(bytes.Length >> 8 & 255);
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			return array2;
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x001A2014 File Offset: 0x001A0214
		public override int GetHashCode()
		{
			return base.MailboxOwnerId.GetHashCode() ^ ((this.StoreObjectIdValue == null) ? 0 : this.StoreObjectIdValue.GetHashCode());
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x001A2038 File Offset: 0x001A0238
		public override bool Equals(XsoMailboxObjectId other)
		{
			MailboxFolderId mailboxFolderId = other as MailboxFolderId;
			if (null == mailboxFolderId)
			{
				return false;
			}
			if (!ADObjectId.Equals(base.MailboxOwnerId, other.MailboxOwnerId))
			{
				return false;
			}
			bool flag = object.Equals(this.StoreObjectIdValue, mailboxFolderId.StoreObjectIdValue);
			if (flag && this.StoreObjectIdValue != null)
			{
				return true;
			}
			bool flag2 = object.Equals(this.MailboxFolderPath, mailboxFolderId.MailboxFolderPath);
			return (flag2 && null != this.MailboxFolderPath) || (flag2 && flag);
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x001A20B3 File Offset: 0x001A02B3
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", base.MailboxOwnerId, ':', (null == this.MailboxFolderPath) ? this.StoreObjectId : this.MailboxFolderPath.ToString());
		}

		// Token: 0x040037F8 RID: 14328
		public const char MailboxAndFolderSeperator = ':';

		// Token: 0x040037F9 RID: 14329
		private string storeObjectId;
	}
}
