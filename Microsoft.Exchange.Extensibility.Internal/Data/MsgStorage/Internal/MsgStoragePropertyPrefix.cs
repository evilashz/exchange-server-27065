using System;
using System.IO;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000AB RID: 171
	internal class MsgStoragePropertyPrefix
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00018042 File Offset: 0x00016242
		internal MsgStoragePropertyPrefix(MsgSubStorageType substorageType)
		{
			this.substorageType = substorageType;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00018051 File Offset: 0x00016251
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00018059 File Offset: 0x00016259
		internal int AttachmentCount
		{
			get
			{
				return this.attachmentCount;
			}
			set
			{
				this.attachmentCount = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00018062 File Offset: 0x00016262
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001806A File Offset: 0x0001626A
		internal int RecipientCount
		{
			get
			{
				return this.recipientCount;
			}
			set
			{
				this.recipientCount = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00018073 File Offset: 0x00016273
		internal int Size
		{
			get
			{
				return MsgStoragePropertyPrefix.SubstorageStreamPrefixSizes[(int)this.substorageType];
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00018081 File Offset: 0x00016281
		internal int Read(byte[] streamData)
		{
			if (this.substorageType == MsgSubStorageType.TopLevelMessage || this.substorageType == MsgSubStorageType.AttachedMessage)
			{
				this.AttachmentCount = BitConverter.ToInt32(streamData, 20);
				this.RecipientCount = BitConverter.ToInt32(streamData, 16);
			}
			return this.Size;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000180B8 File Offset: 0x000162B8
		internal int Write(BinaryWriter writer)
		{
			writer.Write(MsgStoragePropertyPrefix.Padding, 0, 8);
			if (this.substorageType == MsgSubStorageType.Attachment || this.substorageType == MsgSubStorageType.Recipient)
			{
				return this.Size;
			}
			writer.Write(this.RecipientCount);
			writer.Write(this.AttachmentCount);
			writer.Write(this.RecipientCount);
			writer.Write(this.AttachmentCount);
			if (this.substorageType == MsgSubStorageType.AttachedMessage)
			{
				return this.Size;
			}
			writer.Write(MsgStoragePropertyPrefix.Padding, 0, 8);
			return this.Size;
		}

		// Token: 0x04000579 RID: 1401
		private const int RecipientCountOffset = 16;

		// Token: 0x0400057A RID: 1402
		private const int AttachmentCountOffset = 20;

		// Token: 0x0400057B RID: 1403
		internal static readonly int[] SubstorageStreamPrefixSizes = new int[]
		{
			32,
			24,
			8,
			8
		};

		// Token: 0x0400057C RID: 1404
		internal static readonly byte[] Padding = new byte[8];

		// Token: 0x0400057D RID: 1405
		private int attachmentCount;

		// Token: 0x0400057E RID: 1406
		private int recipientCount;

		// Token: 0x0400057F RID: 1407
		private MsgSubStorageType substorageType;
	}
}
