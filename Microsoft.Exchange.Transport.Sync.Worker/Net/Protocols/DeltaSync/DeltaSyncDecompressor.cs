using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000066 RID: 102
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncDecompressor
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x00016D38 File Offset: 0x00014F38
		internal static bool TryDeCompress(Stream compressedData, Stream decompressedData)
		{
			DeltaSyncDecompressor.SclFileHeader sclFileHeader = default(DeltaSyncDecompressor.SclFileHeader);
			DeltaSyncDecompressor.SclBlockHeader sclBlockHeader = default(DeltaSyncDecompressor.SclBlockHeader);
			byte[] array = null;
			byte[] array2 = null;
			int num = 0;
			using (BinaryReader binaryReader = new BinaryReader(compressedData))
			{
				if (!sclFileHeader.TryLoad(binaryReader))
				{
					return false;
				}
				while (sclBlockHeader.TryLoad(binaryReader))
				{
					if (!DeltaSyncDecompressor.TryLoadBlock(binaryReader, (int)sclBlockHeader.CompSize, out array))
					{
						return false;
					}
					if (sclBlockHeader.CompSize >= sclBlockHeader.OrigSize)
					{
						array2 = array;
						num = array2.Length;
					}
					else
					{
						num = (int)sclBlockHeader.OrigSize;
						if (array2 == null || array2.Length < num)
						{
							array2 = new byte[num];
						}
						int num2 = DeltaSyncDecompressor.EcDecompressEx(array, array.Length, array2, ref num);
						if (num2 != 0)
						{
							return false;
						}
						if (sclBlockHeader.CRC32 != 0 && sclBlockHeader.CRC32 != DeltaSyncDecompressor.XpressCrc32(array2, num, 0))
						{
							return false;
						}
					}
					if (num != (int)sclBlockHeader.OrigSize)
					{
						return false;
					}
					decompressedData.Write(array2, 0, num);
					decompressedData.Flush();
				}
				if (decompressedData.Length != (long)((ulong)sclFileHeader.OrigSize))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00016E68 File Offset: 0x00015068
		private static bool TryLoadBlock(BinaryReader reader, int size, out byte[] buffer)
		{
			buffer = null;
			try
			{
				buffer = reader.ReadBytes(size);
				if (buffer == null || buffer.Length != size)
				{
					return false;
				}
			}
			catch (EndOfStreamException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060004C7 RID: 1223
		[DllImport("huffman_xpress.dll")]
		private static extern int EcDecompressEx([MarshalAs(UnmanagedType.LPArray)] byte[] pbComp, int cbComp, [MarshalAs(UnmanagedType.LPArray)] byte[] pbOrig, ref int cbOrig);

		// Token: 0x060004C8 RID: 1224
		[DllImport("huffman_xpress.dll")]
		private static extern int XpressCrc32([MarshalAs(UnmanagedType.LPArray)] byte[] pbInput, int cbInput, int crc);

		// Token: 0x04000282 RID: 642
		private const int NoError = 0;

		// Token: 0x04000283 RID: 643
		private const int MinSclFileHeaderSize = 36;

		// Token: 0x04000284 RID: 644
		private const int MinSclBlockHeaderSize = 20;

		// Token: 0x04000285 RID: 645
		private const int GuidByteSize = 16;

		// Token: 0x04000286 RID: 646
		private const int PuidByteSize = 8;

		// Token: 0x04000287 RID: 647
		private static readonly char[] NoCompressionAlgoId = new char[]
		{
			'U',
			'N'
		};

		// Token: 0x04000288 RID: 648
		private static readonly char[] XpressHuffmanCompressionAlgoId = new char[]
		{
			'H',
			'U'
		};

		// Token: 0x04000289 RID: 649
		private static readonly char[] SupportedCompressionVersion = new char[]
		{
			'0',
			'1'
		};

		// Token: 0x0400028A RID: 650
		private static readonly char[] BlockSignature = new char[]
		{
			'S',
			'C',
			'B',
			'H'
		};

		// Token: 0x02000067 RID: 103
		private struct SclFileHeader
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x00016F28 File Offset: 0x00015128
			internal char[] Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x00016F30 File Offset: 0x00015130
			internal char[] Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060004CD RID: 1229 RVA: 0x00016F38 File Offset: 0x00015138
			internal uint FileHeaderSize
			{
				get
				{
					return this.fileHeaderSize;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060004CE RID: 1230 RVA: 0x00016F40 File Offset: 0x00015140
			internal Guid GuidMsgId
			{
				get
				{
					return this.guidMsgId;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016F48 File Offset: 0x00015148
			internal byte[] PuidUserId
			{
				get
				{
					return this.puidUserId;
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00016F50 File Offset: 0x00015150
			internal uint OrigSize
			{
				get
				{
					return this.origSize;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00016F58 File Offset: 0x00015158
			internal byte[] RemainingBytes
			{
				get
				{
					return this.remainingBytes;
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00016F60 File Offset: 0x00015160
			private bool IsNotCompressed
			{
				get
				{
					return this.id != null && this.id.Length == 2 && this.id[0] == DeltaSyncDecompressor.NoCompressionAlgoId[0] && this.id[1] == DeltaSyncDecompressor.NoCompressionAlgoId[0];
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016F9B File Offset: 0x0001519B
			private bool IsXpressHuffmanCompressed
			{
				get
				{
					return this.id != null && this.id.Length == 2 && this.id[0] == DeltaSyncDecompressor.XpressHuffmanCompressionAlgoId[0] && this.id[1] == DeltaSyncDecompressor.XpressHuffmanCompressionAlgoId[1];
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00016FD6 File Offset: 0x000151D6
			private bool IsSupportedVersion
			{
				get
				{
					return this.version != null && this.version.Length == 2 && this.version[0] == DeltaSyncDecompressor.SupportedCompressionVersion[0] && this.version[1] == DeltaSyncDecompressor.SupportedCompressionVersion[1];
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00017011 File Offset: 0x00015211
			private bool IsSupportedId
			{
				get
				{
					return this.IsNotCompressed || this.IsXpressHuffmanCompressed;
				}
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00017024 File Offset: 0x00015224
			internal bool TryLoad(BinaryReader reader)
			{
				try
				{
					this.id = reader.ReadChars(2);
					if (!this.IsSupportedId)
					{
						return false;
					}
					this.version = reader.ReadChars(2);
					if (!this.IsSupportedVersion)
					{
						return false;
					}
					this.fileHeaderSize = reader.ReadUInt32();
					if (this.fileHeaderSize < 36U)
					{
						return false;
					}
					byte[] array = reader.ReadBytes(16);
					if (array == null || array.Length != 16)
					{
						return false;
					}
					this.guidMsgId = new Guid(array);
					this.puidUserId = reader.ReadBytes(8);
					if (this.puidUserId == null || this.puidUserId.Length != 8)
					{
						return false;
					}
					this.origSize = reader.ReadUInt32();
					if (this.origSize <= 0U)
					{
						return false;
					}
					this.remainingBytes = reader.ReadBytes((int)(this.fileHeaderSize - 36U));
					if (this.remainingBytes == null || (long)this.remainingBytes.Length != (long)((ulong)(this.fileHeaderSize - 36U)))
					{
						return false;
					}
				}
				catch (EndOfStreamException)
				{
					return false;
				}
				return true;
			}

			// Token: 0x0400028B RID: 651
			private char[] id;

			// Token: 0x0400028C RID: 652
			private char[] version;

			// Token: 0x0400028D RID: 653
			private uint fileHeaderSize;

			// Token: 0x0400028E RID: 654
			private Guid guidMsgId;

			// Token: 0x0400028F RID: 655
			private byte[] puidUserId;

			// Token: 0x04000290 RID: 656
			private uint origSize;

			// Token: 0x04000291 RID: 657
			private byte[] remainingBytes;
		}

		// Token: 0x02000068 RID: 104
		private struct SclBlockHeader
		{
			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001713C File Offset: 0x0001533C
			internal char[] Signature
			{
				get
				{
					return this.signature;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00017144 File Offset: 0x00015344
			internal uint BlockHeaderSize
			{
				get
				{
					return this.blockHeaderSize;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001714C File Offset: 0x0001534C
			internal uint OrigSize
			{
				get
				{
					return this.origSize;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060004DA RID: 1242 RVA: 0x00017154 File Offset: 0x00015354
			internal int CRC32
			{
				get
				{
					return this.crc32;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001715C File Offset: 0x0001535C
			internal uint CompSize
			{
				get
				{
					return this.compSize;
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00017164 File Offset: 0x00015364
			internal byte[] RemainingBytes
			{
				get
				{
					return this.remainingBytes;
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001716C File Offset: 0x0001536C
			private bool IsValidSignature
			{
				get
				{
					return this.signature != null && this.signature.Length == 4 && (this.signature[0] == DeltaSyncDecompressor.BlockSignature[0] && this.signature[1] == DeltaSyncDecompressor.BlockSignature[1] && this.signature[2] == DeltaSyncDecompressor.BlockSignature[2]) && this.signature[3] == DeltaSyncDecompressor.BlockSignature[3];
				}
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x000171D4 File Offset: 0x000153D4
			internal bool TryLoad(BinaryReader reader)
			{
				try
				{
					this.signature = reader.ReadChars(4);
					if (!this.IsValidSignature)
					{
						return false;
					}
					this.blockHeaderSize = reader.ReadUInt32();
					if (this.blockHeaderSize < 20U)
					{
						return false;
					}
					this.origSize = reader.ReadUInt32();
					if (this.origSize <= 0U)
					{
						return false;
					}
					this.crc32 = reader.ReadInt32();
					this.compSize = reader.ReadUInt32();
					if (this.compSize <= 0U)
					{
						return false;
					}
					this.remainingBytes = reader.ReadBytes((int)(this.blockHeaderSize - 20U));
					if (this.remainingBytes == null || (long)this.remainingBytes.Length != (long)((ulong)(this.blockHeaderSize - 20U)))
					{
						return false;
					}
				}
				catch (EndOfStreamException)
				{
					return false;
				}
				return true;
			}

			// Token: 0x04000292 RID: 658
			private char[] signature;

			// Token: 0x04000293 RID: 659
			private uint blockHeaderSize;

			// Token: 0x04000294 RID: 660
			private uint origSize;

			// Token: 0x04000295 RID: 661
			private int crc32;

			// Token: 0x04000296 RID: 662
			private uint compSize;

			// Token: 0x04000297 RID: 663
			private byte[] remainingBytes;
		}
	}
}
