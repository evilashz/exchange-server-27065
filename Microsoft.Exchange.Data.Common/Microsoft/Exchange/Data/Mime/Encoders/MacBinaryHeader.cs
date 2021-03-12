using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x02000098 RID: 152
	public class MacBinaryHeader
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0002310E File Offset: 0x0002130E
		public MacBinaryHeader()
		{
			this.version = 130;
			this.minimumVersion = 129;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002312C File Offset: 0x0002132C
		public MacBinaryHeader(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length != 128)
			{
				throw new ArgumentException(EncodersStrings.MacBinHeaderMustBe128Long, "bytes");
			}
			if (bytes[0] != 0 || bytes[74] != 0 || bytes[82] != 0)
			{
				throw new ByteEncoderException(EncodersStrings.MacBinInvalidData);
			}
			if (bytes[1] > 63)
			{
				throw new ByteEncoderException(EncodersStrings.MacBinInvalidData);
			}
			this.fileNameLength = (int)bytes[1];
			this.fileName = CTSGlobals.AsciiEncoding.GetString(bytes, 2, this.fileNameLength);
			this.fileType = BinHexUtils.UnmarshalInt32(bytes, 65);
			this.fileCreator = BinHexUtils.UnmarshalInt32(bytes, 69);
			this.finderFlags = (int)bytes[73];
			this.iconXOffset = (int)BinHexUtils.UnmarshalUInt16(bytes, 75);
			this.iconYOffset = (int)BinHexUtils.UnmarshalUInt16(bytes, 77);
			this.fileProtected = (1 == bytes[81]);
			this.dataForkLength = (long)BinHexUtils.UnmarshalInt32(bytes, 83);
			this.resourceForkLength = (long)BinHexUtils.UnmarshalInt32(bytes, 87);
			this.version = (int)bytes[122];
			this.minimumVersion = (int)bytes[123];
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00023237 File Offset: 0x00021437
		public int OldVersion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0002323A File Offset: 0x0002143A
		public int FileNameLength
		{
			get
			{
				return this.fileNameLength;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00023242 File Offset: 0x00021442
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0002324C File Offset: 0x0002144C
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(value);
				if (bytes.Length > 63)
				{
					throw new ArgumentException(EncodersStrings.MacBinFileNameTooLong, "value");
				}
				this.fileName = value;
				this.fileNameLength = bytes.Length;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0002328C File Offset: 0x0002148C
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00023294 File Offset: 0x00021494
		public int FileType
		{
			get
			{
				return this.fileType;
			}
			set
			{
				this.fileType = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0002329D File Offset: 0x0002149D
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x000232A5 File Offset: 0x000214A5
		public int FileCreator
		{
			get
			{
				return this.fileCreator;
			}
			set
			{
				this.fileCreator = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x000232AE File Offset: 0x000214AE
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x000232B6 File Offset: 0x000214B6
		public int FinderFlags
		{
			get
			{
				return this.finderFlags;
			}
			set
			{
				this.finderFlags = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x000232BF File Offset: 0x000214BF
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x000232C7 File Offset: 0x000214C7
		public int XIcon
		{
			get
			{
				return this.iconXOffset;
			}
			set
			{
				if (65535 < value)
				{
					throw new ArgumentOutOfRangeException("value", EncodersStrings.MacBinIconOffsetTooLarge(65535));
				}
				this.iconXOffset = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x000232ED File Offset: 0x000214ED
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x000232F5 File Offset: 0x000214F5
		public int YIcon
		{
			get
			{
				return this.iconYOffset;
			}
			set
			{
				if (65535 < value)
				{
					throw new ArgumentOutOfRangeException("value", EncodersStrings.MacBinIconOffsetTooLarge(65535));
				}
				this.iconYOffset = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0002331B File Offset: 0x0002151B
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00023323 File Offset: 0x00021523
		public int FileId
		{
			get
			{
				return this.fileID;
			}
			set
			{
				this.fileID = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0002332C File Offset: 0x0002152C
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00023334 File Offset: 0x00021534
		public bool Protected
		{
			get
			{
				return this.fileProtected;
			}
			set
			{
				this.fileProtected = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0002333D File Offset: 0x0002153D
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00023345 File Offset: 0x00021545
		public long DataForkLength
		{
			get
			{
				return this.dataForkLength;
			}
			set
			{
				this.dataForkLength = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0002334E File Offset: 0x0002154E
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00023356 File Offset: 0x00021556
		public long ResourceForkLength
		{
			get
			{
				return this.resourceForkLength;
			}
			set
			{
				this.resourceForkLength = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0002335F File Offset: 0x0002155F
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00023367 File Offset: 0x00021567
		public DateTime CreationDate
		{
			get
			{
				return this.creationDate;
			}
			set
			{
				this.creationDate = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00023370 File Offset: 0x00021570
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00023378 File Offset: 0x00021578
		public DateTime ModificationDate
		{
			get
			{
				return this.modificationDate;
			}
			set
			{
				this.modificationDate = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00023381 File Offset: 0x00021581
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00023389 File Offset: 0x00021589
		public int GetInfoLength
		{
			get
			{
				return this.commentLength;
			}
			set
			{
				this.commentLength = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00023392 File Offset: 0x00021592
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0002339A File Offset: 0x0002159A
		public int UnpackedSize
		{
			get
			{
				return this.unpackedSize;
			}
			set
			{
				this.unpackedSize = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000233A3 File Offset: 0x000215A3
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x000233AB File Offset: 0x000215AB
		public int SecondaryHeaderLength
		{
			get
			{
				return this.secondaryHeaderLength;
			}
			set
			{
				this.secondaryHeaderLength = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000233B4 File Offset: 0x000215B4
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x000233BC File Offset: 0x000215BC
		public int Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value != 0 && 129 != value && 130 != value)
				{
					throw new ArgumentOutOfRangeException("value", EncodersStrings.MacBinBadVersion);
				}
				this.version = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000233E8 File Offset: 0x000215E8
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x000233F0 File Offset: 0x000215F0
		public int MinimumVersion
		{
			get
			{
				return this.minimumVersion;
			}
			set
			{
				if (value != 0 && 129 != value && 130 != value)
				{
					throw new ArgumentOutOfRangeException("value", EncodersStrings.MacBinBadVersion);
				}
				this.minimumVersion = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0002341C File Offset: 0x0002161C
		public short CheckSum
		{
			get
			{
				this.GetBytes();
				return (short)this.headerCRC;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002342C File Offset: 0x0002162C
		internal byte[] GetBytes()
		{
			int num = 0;
			byte[] array = new byte[128];
			array[num++] = 0;
			array[num++] = (byte)this.fileNameLength;
			byte[] src = this.FileNameAsByteArray();
			Buffer.BlockCopy(src, 0, array, num, this.fileNameLength);
			num = 65;
			num += BinHexUtils.MarshalInt32(array, num, (long)this.fileType);
			num += BinHexUtils.MarshalInt32(array, num, (long)this.fileCreator);
			array[num++] = (byte)((65280 & this.finderFlags) >> 8);
			array[num++] = 0;
			num += BinHexUtils.MarshalUInt16(array, num, (ushort)this.iconXOffset);
			num += BinHexUtils.MarshalUInt16(array, num, (ushort)this.iconYOffset);
			num += BinHexUtils.MarshalUInt16(array, num, 0);
			array[num++] = (this.fileProtected ? 1 : 0);
			array[num++] = 0;
			num += BinHexUtils.MarshalInt32(array, num, this.dataForkLength);
			num += BinHexUtils.MarshalInt32(array, num, this.resourceForkLength);
			num += BinHexUtils.MarshalInt32(array, num, 0L);
			num += BinHexUtils.MarshalInt32(array, num, 0L);
			num += BinHexUtils.MarshalUInt16(array, num, (ushort)this.commentLength);
			array[num++] = (byte)(255 & this.finderFlags);
			num += 18;
			num += BinHexUtils.MarshalUInt16(array, num, (ushort)this.secondaryHeaderLength);
			array[num++] = (byte)this.version;
			array[num++] = (byte)this.minimumVersion;
			this.headerCRC = BinHexUtils.CalculateHeaderCrc(array, 124);
			num += BinHexUtils.MarshalUInt16(array, num, this.headerCRC);
			array[num++] = 0;
			array[num++] = 0;
			return array;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000235BC File Offset: 0x000217BC
		private byte[] FileNameAsByteArray()
		{
			return CTSGlobals.AsciiEncoding.GetBytes(this.fileName);
		}

		// Token: 0x0400048B RID: 1163
		private int fileNameLength;

		// Token: 0x0400048C RID: 1164
		private string fileName;

		// Token: 0x0400048D RID: 1165
		private int version;

		// Token: 0x0400048E RID: 1166
		private int fileType;

		// Token: 0x0400048F RID: 1167
		private int fileCreator;

		// Token: 0x04000490 RID: 1168
		private int finderFlags;

		// Token: 0x04000491 RID: 1169
		private long dataForkLength;

		// Token: 0x04000492 RID: 1170
		private long resourceForkLength;

		// Token: 0x04000493 RID: 1171
		private int minimumVersion;

		// Token: 0x04000494 RID: 1172
		private int secondaryHeaderLength;

		// Token: 0x04000495 RID: 1173
		private int unpackedSize;

		// Token: 0x04000496 RID: 1174
		private int commentLength;

		// Token: 0x04000497 RID: 1175
		private DateTime creationDate;

		// Token: 0x04000498 RID: 1176
		private DateTime modificationDate;

		// Token: 0x04000499 RID: 1177
		private bool fileProtected;

		// Token: 0x0400049A RID: 1178
		private int iconXOffset;

		// Token: 0x0400049B RID: 1179
		private int iconYOffset;

		// Token: 0x0400049C RID: 1180
		private int fileID;

		// Token: 0x0400049D RID: 1181
		private ushort headerCRC;

		// Token: 0x02000099 RID: 153
		[Flags]
		internal enum FinderFlagsFields
		{
			// Token: 0x0400049F RID: 1183
			OnDesk = 1,
			// Token: 0x040004A0 RID: 1184
			Color = 14,
			// Token: 0x040004A1 RID: 1185
			ColorReserved = 16,
			// Token: 0x040004A2 RID: 1186
			SwitchLaunch = 32,
			// Token: 0x040004A3 RID: 1187
			Shared = 64,
			// Token: 0x040004A4 RID: 1188
			NoInits = 128,
			// Token: 0x040004A5 RID: 1189
			Initialized = 256,
			// Token: 0x040004A6 RID: 1190
			Reserved = 512,
			// Token: 0x040004A7 RID: 1191
			CustomIcon = 1024,
			// Token: 0x040004A8 RID: 1192
			Stationary = 2048,
			// Token: 0x040004A9 RID: 1193
			NameLocked = 4096,
			// Token: 0x040004AA RID: 1194
			Bundle = 8192,
			// Token: 0x040004AB RID: 1195
			Invisible = 16384,
			// Token: 0x040004AC RID: 1196
			Alias = 32768
		}
	}
}
