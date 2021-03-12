using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200017C RID: 380
	internal class OriginalDeliveryFolderInfo
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x00026CE4 File Offset: 0x00024EE4
		public OriginalDeliveryFolderInfo(OriginalDeliveryFolderInfo.DeliveryFolderType folderType, int folderHash)
		{
			this.folderType = folderType;
			this.folderHash = folderHash;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00026CFA File Offset: 0x00024EFA
		public int FolderHash
		{
			get
			{
				return this.folderHash;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00026D02 File Offset: 0x00024F02
		public OriginalDeliveryFolderInfo.DeliveryFolderType FolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00026D0A File Offset: 0x00024F0A
		public bool IsDeliveryFolderInbox
		{
			get
			{
				return this.folderType == OriginalDeliveryFolderInfo.DeliveryFolderType.Inbox;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00026D15 File Offset: 0x00024F15
		public bool IsDeliveryFolderClutter
		{
			get
			{
				return this.folderType == OriginalDeliveryFolderInfo.DeliveryFolderType.Clutter;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00026D20 File Offset: 0x00024F20
		public bool IsDeliveryFolderDeletedItems
		{
			get
			{
				return this.folderType == OriginalDeliveryFolderInfo.DeliveryFolderType.DeletedItems;
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00026D2C File Offset: 0x00024F2C
		public byte[] Serialize()
		{
			byte[] result = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(1);
				binaryWriter.Write(0);
				binaryWriter.Write((byte)this.folderType);
				binaryWriter.Write(this.folderHash);
				binaryWriter.Flush();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00026D98 File Offset: 0x00024F98
		public static OriginalDeliveryFolderInfo Deserialize(byte[] folderInfoBytes)
		{
			ArgumentValidator.ThrowIfNull("folderInfoBytes", folderInfoBytes);
			OriginalDeliveryFolderInfo result = null;
			if (folderInfoBytes.Length == 9)
			{
				using (MemoryStream memoryStream = new MemoryStream(folderInfoBytes, false))
				{
					BinaryReader binaryReader = new BinaryReader(memoryStream);
					short num = binaryReader.ReadInt16();
					if (num > 1)
					{
						return null;
					}
					binaryReader.ReadInt16();
					byte b = binaryReader.ReadByte();
					int num2 = binaryReader.ReadInt32();
					result = new OriginalDeliveryFolderInfo((OriginalDeliveryFolderInfo.DeliveryFolderType)b, num2);
				}
				return result;
			}
			return result;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00026E1C File Offset: 0x0002501C
		public static bool IsDeliveryFolderInfoBytesDenoteInbox(byte[] deliveryFolderInfoBytes)
		{
			bool result = false;
			if (deliveryFolderInfoBytes != null)
			{
				OriginalDeliveryFolderInfo originalDeliveryFolderInfo = OriginalDeliveryFolderInfo.Deserialize(deliveryFolderInfoBytes);
				if (originalDeliveryFolderInfo != null && originalDeliveryFolderInfo.IsDeliveryFolderInbox)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00026E44 File Offset: 0x00025044
		public override string ToString()
		{
			switch (this.folderType)
			{
			case OriginalDeliveryFolderInfo.DeliveryFolderType.Inbox:
				return "Inbox";
			case OriginalDeliveryFolderInfo.DeliveryFolderType.DeletedItems:
				return "DeletedItems";
			case OriginalDeliveryFolderInfo.DeliveryFolderType.Clutter:
				return "Clutter";
			}
			return this.FolderHash.ToString();
		}

		// Token: 0x0400077F RID: 1919
		public const string InboxString = "Inbox";

		// Token: 0x04000780 RID: 1920
		public const string ClutterString = "Clutter";

		// Token: 0x04000781 RID: 1921
		public const string DeletedItemsString = "DeletedItems";

		// Token: 0x04000782 RID: 1922
		private const short SerializationVersion = 1;

		// Token: 0x04000783 RID: 1923
		private const short RequiredNumberOfBytes = 9;

		// Token: 0x04000784 RID: 1924
		private readonly OriginalDeliveryFolderInfo.DeliveryFolderType folderType;

		// Token: 0x04000785 RID: 1925
		private readonly int folderHash;

		// Token: 0x0200017D RID: 381
		internal enum DeliveryFolderType : byte
		{
			// Token: 0x04000787 RID: 1927
			Inbox = 1,
			// Token: 0x04000788 RID: 1928
			DeletedItems,
			// Token: 0x04000789 RID: 1929
			Other,
			// Token: 0x0400078A RID: 1930
			Clutter
		}
	}
}
