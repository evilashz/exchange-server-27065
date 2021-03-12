using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x02000091 RID: 145
	internal class BinHexHeader
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0002244A File Offset: 0x0002064A
		public BinHexHeader()
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00022454 File Offset: 0x00020654
		public BinHexHeader(byte[] header)
		{
			if (header.Length < 23)
			{
				throw new ArgumentException(EncodersStrings.BinHexHeaderTooSmall, "header");
			}
			int num = (int)header[0];
			if (header.Length - 22 < num)
			{
				throw new ByteEncoderException(EncodersStrings.BinHexHeaderIncomplete);
			}
			if (63 < num || 1 > num)
			{
				throw new ByteEncoderException(EncodersStrings.BinHexHeaderInvalidNameLength);
			}
			int num2 = 2 + num;
			ushort num3 = BinHexUtils.CalculateHeaderCrc(header, num2 + 18);
			ushort num4 = BinHexUtils.UnmarshalUInt16(header, num2 + 18);
			if (num3 != num4)
			{
				throw new ByteEncoderException(EncodersStrings.BinHexHeaderInvalidCrc);
			}
			if (header[1 + num] != 0)
			{
				throw new ByteEncoderException(EncodersStrings.BinHexHeaderUnsupportedVersion);
			}
			this.fileNameLength = num;
			this.fileName = new byte[num];
			Buffer.BlockCopy(header, 1, this.fileName, 0, this.fileNameLength);
			this.version = (int)header[this.fileNameLength + 1];
			this.fileType = BinHexUtils.UnmarshalInt32(header, num2);
			this.fileCreator = BinHexUtils.UnmarshalInt32(header, num2 + 4);
			this.finderFlags = (int)BinHexUtils.UnmarshalUInt16(header, num2 + 8);
			this.dataForkLength = (long)BinHexUtils.UnmarshalInt32(header, num2 + 10);
			this.resourceForkLength = (long)BinHexUtils.UnmarshalInt32(header, num2 + 14);
			this.headerCRC = num4;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00022570 File Offset: 0x00020770
		public BinHexHeader(MacBinaryHeader header)
		{
			if (63 < header.FileNameLength || 1 > header.FileNameLength)
			{
				throw new ByteEncoderException(EncodersStrings.BinHexHeaderBadFileNameLength);
			}
			this.FileName = header.FileName;
			this.version = 0;
			this.fileType = header.FileType;
			this.fileCreator = header.FileCreator;
			this.finderFlags = 0;
			this.dataForkLength = header.DataForkLength;
			this.resourceForkLength = header.ResourceForkLength;
			this.GetBytes();
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000225F2 File Offset: 0x000207F2
		public int FileNameLength
		{
			get
			{
				return this.fileNameLength;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000225FA File Offset: 0x000207FA
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00022614 File Offset: 0x00020814
		public string FileName
		{
			get
			{
				return CTSGlobals.AsciiEncoding.GetString(this.fileName, 0, this.fileNameLength);
			}
			set
			{
				byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(value);
				if (63 < bytes.Length || 1 > bytes.Length)
				{
					throw new ByteEncoderException(EncodersStrings.BinHexHeaderBadFileNameLength);
				}
				this.fileName = bytes;
				this.fileNameLength = bytes.Length;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00022655 File Offset: 0x00020855
		public long DataForkLength
		{
			get
			{
				return this.dataForkLength;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0002265D File Offset: 0x0002085D
		public long ResourceForkLength
		{
			get
			{
				return this.resourceForkLength;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00022668 File Offset: 0x00020868
		public static implicit operator MacBinaryHeader(BinHexHeader rhs)
		{
			return new MacBinaryHeader
			{
				FileName = rhs.FileName,
				FileType = rhs.fileType,
				FileCreator = rhs.fileCreator,
				FinderFlags = rhs.finderFlags,
				DataForkLength = rhs.dataForkLength,
				ResourceForkLength = rhs.resourceForkLength
			};
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000226C4 File Offset: 0x000208C4
		public byte[] GetBytes()
		{
			int num = 0;
			int num2 = 1 + this.FileNameLength + 1 + 4 + 4 + 2 + 4 + 4 + 2;
			byte[] array = new byte[num2];
			array[num++] = (byte)this.fileNameLength;
			Buffer.BlockCopy(this.fileName, 0, array, num, this.fileNameLength);
			num += this.FileNameLength;
			array[num++] = (byte)this.version;
			num += BinHexUtils.MarshalInt32(array, num, (long)this.fileType);
			num += BinHexUtils.MarshalInt32(array, num, (long)this.fileCreator);
			num += BinHexUtils.MarshalUInt16(array, num, (ushort)this.finderFlags);
			num += BinHexUtils.MarshalInt32(array, num, this.dataForkLength);
			num += BinHexUtils.MarshalInt32(array, num, this.resourceForkLength);
			this.headerCRC = BinHexUtils.CalculateHeaderCrc(array, num);
			num += BinHexUtils.MarshalUInt16(array, num, this.headerCRC);
			return array;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002279C File Offset: 0x0002099C
		public BinHexHeader Clone()
		{
			BinHexHeader binHexHeader = base.MemberwiseClone() as BinHexHeader;
			binHexHeader.fileName = (this.fileName.Clone() as byte[]);
			return binHexHeader;
		}

		// Token: 0x04000467 RID: 1127
		private int fileNameLength;

		// Token: 0x04000468 RID: 1128
		private byte[] fileName;

		// Token: 0x04000469 RID: 1129
		private int version;

		// Token: 0x0400046A RID: 1130
		private int fileType;

		// Token: 0x0400046B RID: 1131
		private int fileCreator;

		// Token: 0x0400046C RID: 1132
		private int finderFlags;

		// Token: 0x0400046D RID: 1133
		private long dataForkLength;

		// Token: 0x0400046E RID: 1134
		private long resourceForkLength;

		// Token: 0x0400046F RID: 1135
		private ushort headerCRC;
	}
}
