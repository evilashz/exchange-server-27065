using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000003 RID: 3
	public struct ConversationMembersBlob
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002267 File Offset: 0x00000467
		public ConversationMembersBlob(byte[] folderId, byte[] messageId, int sortPosition)
		{
			this.folderId = folderId;
			this.messageId = messageId;
			this.sortPosition = sortPosition;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000227E File Offset: 0x0000047E
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002286 File Offset: 0x00000486
		public byte[] MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000228E File Offset: 0x0000048E
		public int SortPosition
		{
			get
			{
				return this.sortPosition;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002298 File Offset: 0x00000498
		internal static byte[] Serialize(IList<ConversationMembersBlob> arrayOfItems)
		{
			int count = arrayOfItems.Count;
			int num = 0;
			num += SerializedValue.SerializeInt32(1, null, 0);
			num += SerializedValue.SerializeInt32(count, null, 0);
			for (int i = 0; i < count; i++)
			{
				num += SerializedValue.SerializeBinary(arrayOfItems[i].folderId, null, 0);
				num += SerializedValue.SerializeBinary(arrayOfItems[i].messageId, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].sortPosition, null, 0);
			}
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += SerializedValue.SerializeInt32(1, array, num2);
			num2 += SerializedValue.SerializeInt32(count, array, num2);
			for (int j = 0; j < count; j++)
			{
				num2 += SerializedValue.SerializeBinary(arrayOfItems[j].folderId, array, num2);
				num2 += SerializedValue.SerializeBinary(arrayOfItems[j].messageId, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].sortPosition, array, num2);
			}
			return array;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002398 File Offset: 0x00000598
		internal static ConversationMembersBlob[] Deserialize(byte[] theBlob)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(theBlob, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Wrong version.");
			}
			int num3 = SerializedValue.ParseInt32(theBlob, ref num);
			if ((theBlob.Length - num) / 3 < num3)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			if (num3 < 0)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			ConversationMembersBlob[] array = new ConversationMembersBlob[num3];
			for (int i = 0; i < num3; i++)
			{
				byte[] array2 = SerializedValue.ParseBinary(theBlob, ref num);
				byte[] array3 = SerializedValue.ParseBinary(theBlob, ref num);
				int num4 = SerializedValue.ParseInt32(theBlob, ref num);
				array[i] = new ConversationMembersBlob(array2, array3, num4);
			}
			return array;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000243C File Offset: 0x0000063C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("FolderId:[");
			stringBuilder.AppendAsString(this.FolderId);
			stringBuilder.Append("] MessageId:[");
			stringBuilder.AppendAsString(this.MessageId);
			stringBuilder.Append("] SortPosition:[");
			stringBuilder.AppendAsString(this.SortPosition);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024AC File Offset: 0x000006AC
		public static string ConversationMembersBlobAsString(byte[] theBlob)
		{
			ConversationMembersBlob[] array = ConversationMembersBlob.Deserialize(theBlob);
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("ConversationMembersBlob:[");
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("[");
				stringBuilder.Append(array[i].ToString());
				stringBuilder.Append("]");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000007 RID: 7
		public const int BlobVersion = 1;

		// Token: 0x04000008 RID: 8
		private byte[] folderId;

		// Token: 0x04000009 RID: 9
		private byte[] messageId;

		// Token: 0x0400000A RID: 10
		private int sortPosition;
	}
}
