using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000009 RID: 9
	public struct FolderHierarchyBlob
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00004F8B File Offset: 0x0000318B
		public FolderHierarchyBlob(int mailboxPartitionNumber, int mailboxNumber, byte[] parentFolderId, byte[] folderId, string displayName, int depth, int sortPosition)
		{
			this.mailboxPartitionNumber = mailboxPartitionNumber;
			this.mailboxNumber = mailboxNumber;
			this.parentFolderId = parentFolderId;
			this.folderId = folderId;
			this.displayName = displayName;
			this.depth = depth;
			this.sortPosition = sortPosition;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004FC2 File Offset: 0x000031C2
		public int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004FCA File Offset: 0x000031CA
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004FD2 File Offset: 0x000031D2
		public byte[] ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004FDA File Offset: 0x000031DA
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004FE2 File Offset: 0x000031E2
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004FEA File Offset: 0x000031EA
		public int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004FF2 File Offset: 0x000031F2
		public int SortPosition
		{
			get
			{
				return this.sortPosition;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004FFC File Offset: 0x000031FC
		internal static byte[] Serialize(IList<FolderHierarchyBlob> arrayOfItems)
		{
			int count = arrayOfItems.Count;
			int num = 0;
			num += SerializedValue.SerializeInt32(1, null, 0);
			num += SerializedValue.SerializeInt32(count, null, 0);
			for (int i = 0; i < count; i++)
			{
				num += SerializedValue.SerializeInt32(arrayOfItems[i].mailboxPartitionNumber, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].mailboxNumber, null, 0);
				num += SerializedValue.SerializeBinary(arrayOfItems[i].parentFolderId, null, 0);
				num += SerializedValue.SerializeBinary(arrayOfItems[i].folderId, null, 0);
				num += SerializedValue.SerializeString(arrayOfItems[i].displayName, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].depth, null, 0);
				num += SerializedValue.SerializeInt32(arrayOfItems[i].sortPosition, null, 0);
			}
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += SerializedValue.SerializeInt32(1, array, num2);
			num2 += SerializedValue.SerializeInt32(count, array, num2);
			for (int j = 0; j < count; j++)
			{
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].mailboxPartitionNumber, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].mailboxNumber, array, num2);
				num2 += SerializedValue.SerializeBinary(arrayOfItems[j].parentFolderId, array, num2);
				num2 += SerializedValue.SerializeBinary(arrayOfItems[j].folderId, array, num2);
				num2 += SerializedValue.SerializeString(arrayOfItems[j].displayName, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].depth, array, num2);
				num2 += SerializedValue.SerializeInt32(arrayOfItems[j].sortPosition, array, num2);
			}
			return array;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000051C8 File Offset: 0x000033C8
		internal static FolderHierarchyBlob[] Deserialize(byte[] theBlob)
		{
			int num = 0;
			int num2 = SerializedValue.ParseInt32(theBlob, ref num);
			if (num2 != 1)
			{
				throw new InvalidSerializedFormatException("Wrong version.");
			}
			int num3 = SerializedValue.ParseInt32(theBlob, ref num);
			if ((theBlob.Length - num) / 7 < num3)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			if (num3 < 0)
			{
				throw new InvalidSerializedFormatException("Invalid number of elements.");
			}
			FolderHierarchyBlob[] array = new FolderHierarchyBlob[num3];
			for (int i = 0; i < num3; i++)
			{
				int num4 = SerializedValue.ParseInt32(theBlob, ref num);
				int num5 = SerializedValue.ParseInt32(theBlob, ref num);
				byte[] array2 = SerializedValue.ParseBinary(theBlob, ref num);
				byte[] array3 = SerializedValue.ParseBinary(theBlob, ref num);
				string text = SerializedValue.ParseString(theBlob, ref num);
				int num6 = SerializedValue.ParseInt32(theBlob, ref num);
				int num7 = SerializedValue.ParseInt32(theBlob, ref num);
				array[i] = new FolderHierarchyBlob(num4, num5, array2, array3, text, num6, num7);
			}
			return array;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000529C File Offset: 0x0000349C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("MailboxPartitionNumber:[");
			stringBuilder.AppendAsString(this.MailboxPartitionNumber);
			stringBuilder.Append("] MailboxNumber:[");
			stringBuilder.AppendAsString(this.MailboxNumber);
			stringBuilder.Append("] ParentFolderId:[");
			stringBuilder.AppendAsString(this.ParentFolderId);
			stringBuilder.Append("] FolderId:[");
			stringBuilder.AppendAsString(this.FolderId);
			stringBuilder.Append("] DisplayName:[");
			stringBuilder.AppendAsString(this.DisplayName);
			stringBuilder.Append("] Depth:[");
			stringBuilder.AppendAsString(this.Depth);
			stringBuilder.Append("] SortPosition:[");
			stringBuilder.AppendAsString(this.SortPosition);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000536C File Offset: 0x0000356C
		public static string FolderHierarchyBlobAsString(byte[] theBlob)
		{
			FolderHierarchyBlob[] array = FolderHierarchyBlob.Deserialize(theBlob);
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("FolderHierarchyBlob:[");
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

		// Token: 0x0400008F RID: 143
		public const int BlobVersion = 1;

		// Token: 0x04000090 RID: 144
		private int mailboxPartitionNumber;

		// Token: 0x04000091 RID: 145
		private int mailboxNumber;

		// Token: 0x04000092 RID: 146
		private byte[] parentFolderId;

		// Token: 0x04000093 RID: 147
		private byte[] folderId;

		// Token: 0x04000094 RID: 148
		private string displayName;

		// Token: 0x04000095 RID: 149
		private int depth;

		// Token: 0x04000096 RID: 150
		private int sortPosition;
	}
}
