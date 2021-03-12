using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x02000090 RID: 144
	public class BinHexEncoder : ByteEncoder
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x0002166D File Offset: 0x0001F86D
		public BinHexEncoder()
		{
			this.state = BinHexUtils.State.Starting;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00021699 File Offset: 0x0001F899
		public BinHexEncoder(MacBinaryHeader header)
		{
			this.MacBinaryHeader = header;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000216C5 File Offset: 0x0001F8C5
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000216E0 File Offset: 0x0001F8E0
		public MacBinaryHeader MacBinaryHeader
		{
			get
			{
				if (this.header == null)
				{
					return new MacBinaryHeader();
				}
				return this.header;
			}
			set
			{
				if (value != null)
				{
					if (0L != value.ResourceForkLength)
					{
						throw new ArgumentException(EncodersStrings.BinHexEncoderDoesNotSupportResourceFork);
					}
					this.header = new BinHexHeader(value);
				}
				this.Reset();
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002170C File Offset: 0x0001F90C
		public sealed override void Convert(byte[] input, int inputIndex, int inputSize, byte[] output, int outputIndex, int outputSize, bool flush, out int inputUsed, out int outputUsed, out bool completed)
		{
			if (inputSize != 0)
			{
				if (input == null)
				{
					throw new ArgumentNullException("input");
				}
				if (inputIndex < 0 || inputIndex >= input.Length)
				{
					throw new ArgumentOutOfRangeException("inputIndex");
				}
				if (inputSize < 0 || inputSize > input.Length - inputIndex)
				{
					throw new ArgumentOutOfRangeException("inputSize");
				}
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (outputIndex < 0 || outputIndex >= output.Length)
			{
				throw new ArgumentOutOfRangeException("outputIndex");
			}
			if (outputSize < 1 || outputSize > output.Length - outputIndex)
			{
				throw new ArgumentOutOfRangeException("outputSize");
			}
			inputUsed = inputIndex;
			outputUsed = outputIndex;
			int num = 0;
			if (this.encodedSize != 0)
			{
				int num2 = Math.Min(this.encodedSize, outputSize);
				Buffer.BlockCopy(this.encoded, this.encodedIndex, output, outputIndex, num2);
				outputSize -= num2;
				outputIndex += num2;
				this.encodedSize -= num2;
				if (this.encodedSize != 0)
				{
					this.encodedIndex += num2;
					outputUsed = outputIndex - outputUsed;
					inputUsed = 0;
					completed = false;
					return;
				}
				this.encodedIndex = 0;
			}
			bool flag = true;
			while (flag)
			{
				int num3 = 0;
				flag = false;
				switch (this.state)
				{
				case BinHexUtils.State.Starting:
					Buffer.BlockCopy(BinHexEncoder.BinHexPrefix, 0, this.decoded, 0, BinHexEncoder.BinHexPrefix.Length);
					this.decodedIndex = 0;
					this.decodedSize = BinHexEncoder.BinHexPrefix.Length;
					this.state = BinHexUtils.State.Prefix;
					goto IL_182;
				case BinHexUtils.State.Prefix:
					goto IL_182;
				case BinHexUtils.State.Header:
					goto IL_284;
				case BinHexUtils.State.Data:
					num3 = Math.Min(this.decodedSize, inputSize);
					if (num3 != 0)
					{
						flag = true;
						this.CompressAndEncode(input, ref inputIndex, ref inputSize, ref this.decodedSize, ref num3, true, output, ref outputIndex, ref outputSize);
					}
					break;
				case BinHexUtils.State.DataCRC:
					if (this.macbinHeaderSize != 0)
					{
						BinHexEncoder.IgnoreChunkFromInput(ref this.macbinHeaderSize, ref inputIndex, ref inputSize);
						if (this.macbinHeaderSize != 0)
						{
							break;
						}
					}
					num3 = this.decodedSize;
					if (num3 != 0)
					{
						flag = true;
						this.CompressAndEncode(this.decoded, ref this.decodedIndex, ref this.decodedSize, ref num, ref num3, false, output, ref outputIndex, ref outputSize);
					}
					break;
				case BinHexUtils.State.Resource:
					num3 = Math.Min(this.decodedSize, inputSize);
					if (num3 != 0)
					{
						flag = true;
						this.CompressAndEncode(input, ref inputIndex, ref inputSize, ref this.decodedSize, ref num3, true, output, ref outputIndex, ref outputSize);
					}
					break;
				case BinHexUtils.State.ResourceCRC:
					if (this.macbinHeaderSize != 0)
					{
						BinHexEncoder.IgnoreChunkFromInput(ref this.macbinHeaderSize, ref inputIndex, ref inputSize);
						if (this.macbinHeaderSize != 0)
						{
							break;
						}
					}
					num3 = this.decodedSize;
					if (num3 != 0)
					{
						flag = true;
						this.CompressAndEncode(this.decoded, ref this.decodedIndex, ref this.decodedSize, ref num, ref num3, false, output, ref outputIndex, ref outputSize);
					}
					break;
				case BinHexUtils.State.Ending:
				{
					int num4 = Math.Min(this.decodedSize, outputSize);
					Buffer.BlockCopy(this.decoded, this.decodedIndex, output, outputIndex, num4);
					outputSize -= num4;
					outputIndex += num4;
					this.decodedSize -= num4;
					if (this.decodedSize == 0)
					{
						this.decodedIndex = 0;
						this.state = BinHexUtils.State.Ended;
						goto IL_4F5;
					}
					this.decodedIndex += num4;
					break;
				}
				case BinHexUtils.State.Ended:
					goto IL_4F5;
				}
				IL_50B:
				if (this.encodedSize == 0)
				{
					continue;
				}
				break;
				IL_182:
				int num5 = Math.Min(this.decodedSize, outputSize);
				Buffer.BlockCopy(this.decoded, this.decodedIndex, output, outputIndex, num5);
				outputSize -= num5;
				outputIndex += num5;
				this.decodedSize -= num5;
				if (this.decodedSize != 0)
				{
					this.decodedIndex += num5;
					goto IL_50B;
				}
				this.decodedIndex = 0;
				this.previousByte = 256;
				this.repeatCount = 0;
				this.accumCount = 0;
				this.lineOffset = 1;
				this.lines = 0;
				this.checksum = 0;
				if (this.header != null)
				{
					byte[] bytes = this.header.GetBytes();
					Buffer.BlockCopy(bytes, 0, this.decoded, 0, bytes.Length);
					this.decodedSize = bytes.Length;
					this.decodedIndex = 0;
					this.macbinHeaderSize = 0;
				}
				else
				{
					this.macbinHeader = new byte[128];
					this.macbinHeaderOffset = 0;
					this.macbinHeaderSize = this.macbinHeader.Length;
				}
				this.state = BinHexUtils.State.Header;
				IL_284:
				if (this.macbinHeaderSize != 0)
				{
					int num6 = Math.Min(this.macbinHeaderSize, inputSize);
					Buffer.BlockCopy(input, inputIndex, this.macbinHeader, this.macbinHeaderOffset, num6);
					this.macbinHeaderOffset += num6;
					this.macbinHeaderSize -= num6;
					inputIndex += num6;
					inputSize -= num6;
					if (this.macbinHeaderSize != 0)
					{
						goto IL_50B;
					}
					MacBinaryHeader macBinaryHeader = new MacBinaryHeader(this.macbinHeader);
					this.macbinHeader = null;
					this.header = new BinHexHeader(macBinaryHeader);
					byte[] bytes2 = this.header.GetBytes();
					Buffer.BlockCopy(bytes2, 0, this.decoded, 0, bytes2.Length);
					this.decodedSize = bytes2.Length;
					this.decodedIndex = 0;
				}
				num3 = this.decodedSize;
				if (num3 != 0)
				{
					flag = true;
					this.CompressAndEncode(this.decoded, ref this.decodedIndex, ref this.decodedSize, ref num, ref num3, false, output, ref outputIndex, ref outputSize);
					goto IL_50B;
				}
				goto IL_50B;
				IL_4F5:
				this.macbinHeaderSize = inputSize;
				BinHexEncoder.IgnoreChunkFromInput(ref this.macbinHeaderSize, ref inputIndex, ref inputSize);
				goto IL_50B;
			}
			if (flush && inputSize == 0 && this.state != BinHexUtils.State.Ended && outputSize != 0)
			{
				throw new ByteEncoderException(EncodersStrings.BinHexEncoderDataCorruptCannotFinishEncoding);
			}
			outputUsed = outputIndex - outputUsed;
			inputUsed = inputIndex - inputUsed;
			completed = (inputSize == 0 && (!flush || this.state == BinHexUtils.State.Ended));
			if (flush && completed)
			{
				this.state = BinHexUtils.State.Starting;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00021C90 File Offset: 0x0001FE90
		public sealed override int GetMaxByteCount(int dataCount)
		{
			int num = (dataCount + 85 + 6) * 8 / 3 + 1;
			num += (num / 64 + 1) * 2;
			return num + BinHexEncoder.BinHexPrefix.Length + BinHexEncoder.BinHexSuffix.Length;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00021CC6 File Offset: 0x0001FEC6
		public sealed override void Reset()
		{
			this.state = BinHexUtils.State.Starting;
			this.encodedSize = 0;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00021CD8 File Offset: 0x0001FED8
		public sealed override ByteEncoder Clone()
		{
			BinHexEncoder binHexEncoder = base.MemberwiseClone() as BinHexEncoder;
			binHexEncoder.encoded = (this.encoded.Clone() as byte[]);
			binHexEncoder.decoded = (this.decoded.Clone() as byte[]);
			if (this.macbinHeader != null)
			{
				binHexEncoder.macbinHeader = (this.macbinHeader.Clone() as byte[]);
			}
			return binHexEncoder;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00021D3C File Offset: 0x0001FF3C
		private static void IgnoreChunkFromInput(ref int ignoreTotal, ref int index, ref int size)
		{
			int num = Math.Min(ignoreTotal, size);
			ignoreTotal -= num;
			size -= num;
			index += num;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00021D68 File Offset: 0x0001FF68
		private void CompressAndEncode(byte[] input, ref int inputOffset, ref int inputSize, ref int inputTotal, ref int inputMax, bool inputIsCRC, byte[] output, ref int outputOffset, ref int outputSize)
		{
			byte[] bytes = null;
			int index = 0;
			int num = 0;
			if (inputIsCRC)
			{
				bytes = input;
				index = inputOffset;
				num = inputMax;
			}
			int num5;
			for (;;)
			{
				int num2 = (int)input[inputOffset++];
				inputSize--;
				inputTotal--;
				inputMax--;
				if (num2 != this.previousByte || this.repeatCount >= 254)
				{
					goto IL_74;
				}
				this.repeatCount++;
				if (this.state >= BinHexUtils.State.ResourceCRC && inputMax == 0)
				{
					num2 = 257;
					goto IL_74;
				}
				IL_3D5:
				if (inputMax == 0)
				{
					goto IL_3DD;
				}
				continue;
				IL_74:
				byte[] array = new byte[5];
				int num3 = 0;
				if (this.repeatCount >= 2)
				{
					this.repeatCount++;
					array[num3++] = 144;
					array[num3++] = (byte)this.repeatCount;
					this.repeatCount = 0;
				}
				else if (this.repeatCount != 0)
				{
					array[num3++] = (byte)this.previousByte;
					if (this.previousByte == 144)
					{
						array[num3++] = 0;
					}
					this.repeatCount = 0;
				}
				if (num2 <= 255)
				{
					array[num3++] = (byte)num2;
					if (num2 == 144)
					{
						array[num3++] = 0;
					}
				}
				bool flag = false;
				if (this.state == BinHexUtils.State.ResourceCRC && inputMax == 0 && (this.accumCount + num3) % 3 != 0)
				{
					array[num3++] = 0;
					flag = true;
				}
				this.previousByte = num2;
				int num4 = 0;
				while (num3 != 0)
				{
					this.accumValue <<= 8;
					this.accumValue |= (int)array[num4];
					this.accumCount++;
					switch (this.accumCount)
					{
					case 1:
						this.encoded[this.encodedSize++] = BinHexEncoder.Table[this.accumValue >> 2 & 63];
						this.lineOffset++;
						break;
					case 2:
						this.encoded[this.encodedSize++] = BinHexEncoder.Table[this.accumValue >> 4 & 63];
						this.lineOffset++;
						break;
					case 3:
						this.encoded[this.encodedSize++] = BinHexEncoder.Table[this.accumValue >> 6 & 63];
						this.lineOffset++;
						if (!flag || 1 != num3)
						{
							if (this.lineOffset == 64)
							{
								this.encoded[this.encodedSize++] = 13;
								this.encoded[this.encodedSize++] = 10;
								this.lineOffset = 0;
								this.lines++;
							}
							this.encoded[this.encodedSize++] = BinHexEncoder.Table[this.accumValue & 63];
							this.lineOffset++;
						}
						this.accumCount = 0;
						break;
					}
					if (this.lineOffset == 64)
					{
						this.encoded[this.encodedSize++] = 13;
						this.encoded[this.encodedSize++] = 10;
						this.lineOffset = 0;
						this.lines++;
					}
					num3--;
					num4++;
				}
				num5 = Math.Min(this.encodedSize, outputSize);
				if (output != null)
				{
					Buffer.BlockCopy(this.encoded, this.encodedIndex, output, outputOffset, num5);
					outputSize -= num5;
				}
				outputOffset += num5;
				this.encodedSize -= num5;
				if (this.encodedSize != 0)
				{
					break;
				}
				this.encodedIndex = 0;
				goto IL_3D5;
			}
			this.encodedIndex += num5;
			IL_3DD:
			switch (this.state)
			{
			case BinHexUtils.State.Header:
				if (this.decodedSize != 0)
				{
					return;
				}
				this.checksum = 0;
				if (0L != this.header.DataForkLength)
				{
					this.decodedSize = (int)this.header.DataForkLength;
					this.state = BinHexUtils.State.Data;
					return;
				}
				this.macbinHeaderSize = 0;
				this.checksum = BinHexUtils.CalculateCrc(this.checksum);
				this.decodedSize = BinHexUtils.MarshalUInt16(this.decoded, 0, this.checksum);
				this.decodedIndex = 0;
				this.state = BinHexUtils.State.DataCRC;
				return;
			case BinHexUtils.State.Data:
				num -= inputMax;
				this.checksum = BinHexUtils.CalculateCrc(bytes, index, num, this.checksum);
				if (this.decodedSize == 0)
				{
					if (0L < this.header.ResourceForkLength)
					{
						this.macbinHeaderSize = ((this.header.DataForkLength % 128L != 0L) ? ((int)(128L - this.header.DataForkLength % 128L)) : 0);
					}
					else
					{
						this.macbinHeaderSize = 0;
					}
					this.checksum = BinHexUtils.CalculateCrc(this.checksum);
					this.decodedSize = BinHexUtils.MarshalUInt16(this.decoded, 0, this.checksum);
					this.decodedIndex = 0;
					this.state = BinHexUtils.State.DataCRC;
					return;
				}
				return;
			case BinHexUtils.State.DataCRC:
				if (this.decodedSize != 0)
				{
					return;
				}
				this.checksum = 0;
				if (0L != this.header.ResourceForkLength)
				{
					this.decodedSize = (int)this.header.ResourceForkLength;
					this.state = BinHexUtils.State.Resource;
					return;
				}
				this.macbinHeaderSize = 0;
				this.checksum = BinHexUtils.CalculateCrc(this.checksum);
				this.decodedSize = BinHexUtils.MarshalUInt16(this.decoded, 0, this.checksum);
				this.decodedIndex = 0;
				this.state = BinHexUtils.State.ResourceCRC;
				return;
			case BinHexUtils.State.Resource:
				num -= inputMax;
				this.checksum = BinHexUtils.CalculateCrc(bytes, index, num, this.checksum);
				if (this.decodedSize == 0)
				{
					this.macbinHeaderSize = ((this.header.ResourceForkLength % 128L != 0L) ? ((int)(128L - this.header.ResourceForkLength % 128L)) : 0);
					this.checksum = BinHexUtils.CalculateCrc(this.checksum);
					this.decodedSize = BinHexUtils.MarshalUInt16(this.decoded, 0, this.checksum);
					this.decodedIndex = 0;
					this.state = BinHexUtils.State.ResourceCRC;
					return;
				}
				return;
			case BinHexUtils.State.ResourceCRC:
				if (this.decodedSize == 0)
				{
					Buffer.BlockCopy(BinHexEncoder.BinHexSuffix, 0, this.decoded, 0, BinHexEncoder.BinHexSuffix.Length);
					this.decodedIndex = 0;
					this.decodedSize = BinHexEncoder.BinHexSuffix.Length;
					this.state = BinHexUtils.State.Ending;
					return;
				}
				return;
			}
			throw new Exception(EncodersStrings.BinHexEncoderInternalError);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00022403 File Offset: 0x00020603
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000452 RID: 1106
		private static readonly byte[] Table = CTSGlobals.AsciiEncoding.GetBytes("!\"#$%&'()*+,-012345689@ABCDEFGHIJKLMNPQRSTUVXYZ[`abcdefhijklmpqr");

		// Token: 0x04000453 RID: 1107
		private static readonly byte[] BinHexPrefix = CTSGlobals.AsciiEncoding.GetBytes("(This file must be converted with BinHex 4.0)\r\n\r\n:");

		// Token: 0x04000454 RID: 1108
		private static readonly byte[] BinHexSuffix = CTSGlobals.AsciiEncoding.GetBytes(":\r\n");

		// Token: 0x04000455 RID: 1109
		private BinHexUtils.State state;

		// Token: 0x04000456 RID: 1110
		private int encodedSize;

		// Token: 0x04000457 RID: 1111
		private int encodedIndex;

		// Token: 0x04000458 RID: 1112
		private byte[] encoded = new byte[9];

		// Token: 0x04000459 RID: 1113
		private int previousByte;

		// Token: 0x0400045A RID: 1114
		private int repeatCount;

		// Token: 0x0400045B RID: 1115
		private int accumCount;

		// Token: 0x0400045C RID: 1116
		private int accumValue;

		// Token: 0x0400045D RID: 1117
		private ushort checksum;

		// Token: 0x0400045E RID: 1118
		private int lineOffset;

		// Token: 0x0400045F RID: 1119
		private int lines;

		// Token: 0x04000460 RID: 1120
		private byte[] macbinHeader;

		// Token: 0x04000461 RID: 1121
		private int macbinHeaderSize;

		// Token: 0x04000462 RID: 1122
		private int macbinHeaderOffset;

		// Token: 0x04000463 RID: 1123
		private byte[] decoded = new byte[128];

		// Token: 0x04000464 RID: 1124
		private int decodedSize;

		// Token: 0x04000465 RID: 1125
		private int decodedIndex;

		// Token: 0x04000466 RID: 1126
		private BinHexHeader header;
	}
}
