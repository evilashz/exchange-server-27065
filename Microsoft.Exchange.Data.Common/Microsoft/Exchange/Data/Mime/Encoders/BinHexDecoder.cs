using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x0200008F RID: 143
	public class BinHexDecoder : ByteEncoder
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x00020B59 File Offset: 0x0001ED59
		public BinHexDecoder()
		{
			this.dataForkOnly = true;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00020B98 File Offset: 0x0001ED98
		public BinHexDecoder(bool dataForkOnly)
		{
			this.DataForkOnly = dataForkOnly;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00020BD7 File Offset: 0x0001EDD7
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00020BDF File Offset: 0x0001EDDF
		public bool DataForkOnly
		{
			get
			{
				return this.dataForkOnly;
			}
			set
			{
				this.dataForkOnly = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00020BE8 File Offset: 0x0001EDE8
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
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00020C04 File Offset: 0x0001EE04
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
			for (;;)
			{
				if (this.extraSize != 0)
				{
					int num = Math.Min(this.extraSize, outputSize);
					Buffer.BlockCopy(this.extra, this.extraIndex, output, outputIndex, num);
					outputSize -= num;
					outputIndex += num;
					this.extraSize -= num;
					if (this.extraSize != 0)
					{
						this.extraIndex += num;
						goto IL_2F0;
					}
					this.extraIndex = 0;
				}
				if (this.repeatCount != 0)
				{
					switch (this.state)
					{
					case BinHexUtils.State.Data:
					case BinHexUtils.State.Resource:
						this.OutputChunk(output, ref outputIndex, ref outputSize);
						break;
					case BinHexUtils.State.DataCRC:
						goto IL_123;
					default:
						goto IL_123;
					}
					IL_13B:
					if (this.bytesNeeded == 0)
					{
						this.TransitionState();
					}
					if (outputSize == 0)
					{
						goto IL_2F0;
					}
					continue;
					IL_123:
					this.OutputChunk(this.scratch, ref this.scratchIndex, ref this.scratchSize);
					goto IL_13B;
				}
				if (this.lineReady)
				{
					byte b = this.encoded[this.encodedIndex++];
					this.encodedSize--;
					if (this.encodedSize == 0)
					{
						this.lineReady = false;
					}
					if (ByteEncoder.IsWhiteSpace(b))
					{
						continue;
					}
					if (this.state == BinHexUtils.State.Started)
					{
						if (58 == b)
						{
							this.state = BinHexUtils.State.HdrFileSize;
							this.bytesNeeded = 1;
							this.scratchSize = this.scratch.Length;
							this.scratchIndex = 0;
							this.runningCRC = 0;
							continue;
						}
						break;
					}
					else
					{
						b -= 32;
						byte b2 = ((int)b >= BinHexDecoder.Dictionary.Length) ? 127 : BinHexDecoder.Dictionary[(int)b];
						if (b2 == 127)
						{
							goto Block_20;
						}
						if (this.accumCount == 0)
						{
							this.accum = (int)b2;
							this.accumCount++;
							continue;
						}
						this.accum = (this.accum << 6 | (int)b2);
						b2 = (byte)(this.accum >> BinHexDecoder.ShiftTabe[this.accumCount] & 255);
						this.accumCount++;
						this.accumCount %= Marshal.SizeOf(this.accum);
						if (this.repeatCheck)
						{
							this.repeatCheck = false;
							if (b2 == 0)
							{
								this.decodedByte = 144;
								this.repeatCount = 1;
								continue;
							}
							this.repeatCount = (int)(b2 - 1);
							continue;
						}
						else
						{
							if (144 == b2)
							{
								this.repeatCheck = true;
								continue;
							}
							this.decodedByte = b2;
							this.repeatCount = 1;
							continue;
						}
					}
				}
				IL_2F0:
				if (this.lineReady)
				{
					goto IL_316;
				}
				this.lineReady = this.ReadLine(input, ref inputIndex, ref inputSize, flush);
				if (!this.lineReady)
				{
					goto IL_316;
				}
			}
			throw new ByteEncoderException(EncodersStrings.BinHexDecoderFirstNonWhitespaceMustBeColon);
			Block_20:
			throw new ByteEncoderException(EncodersStrings.BinHexDecoderFoundInvalidCharacter);
			IL_316:
			outputUsed = outputIndex - outputUsed;
			inputUsed = inputIndex - inputUsed;
			completed = (inputSize == 0 && this.extraSize == 0 && this.repeatCount == 0 && !this.lineReady);
			if (flush && completed)
			{
				if (this.state != BinHexUtils.State.Ending)
				{
					throw new ByteEncoderException(EncodersStrings.BinHexDecoderDataCorrupt);
				}
				this.Reset();
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00020F7E File Offset: 0x0001F17E
		public sealed override int GetMaxByteCount(int dataCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00020F85 File Offset: 0x0001F185
		public sealed override void Reset()
		{
			this.state = BinHexUtils.State.Starting;
			this.lineReady = false;
			this.encodedSize = 0;
			this.repeatCount = 0;
			this.extraIndex = 0;
			this.extraSize = 0;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		public sealed override ByteEncoder Clone()
		{
			BinHexDecoder binHexDecoder = base.MemberwiseClone() as BinHexDecoder;
			binHexDecoder.encoded = (this.encoded.Clone() as byte[]);
			binHexDecoder.scratch = (this.scratch.Clone() as byte[]);
			binHexDecoder.extra = (this.extra.Clone() as byte[]);
			binHexDecoder.header = ((this.header != null) ? this.header.Clone() : null);
			return binHexDecoder;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002102C File Offset: 0x0001F22C
		private void OutputChunk(byte[] bytes, ref int index, ref int size)
		{
			int num = Math.Min(this.bytesNeeded, this.repeatCount);
			num = Math.Min(size, num);
			if (bytes != null)
			{
				for (int i = 0; i < num; i++)
				{
					bytes[index++] = this.decodedByte;
				}
				size -= num;
			}
			this.runningCRC = BinHexUtils.CalculateCrc(this.decodedByte, num, this.runningCRC);
			this.bytesNeeded -= num;
			this.repeatCount -= num;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000210B0 File Offset: 0x0001F2B0
		private bool ReadLine(byte[] input, ref int inputIndex, ref int inputSize, bool flush)
		{
			if (this.state == BinHexUtils.State.Ending)
			{
				inputIndex += inputSize;
				inputSize = 0;
				return false;
			}
			bool result = false;
			while (inputSize != 0 || (flush && this.encodedSize != 0))
			{
				byte b = 10;
				if (inputSize != 0)
				{
					inputSize--;
					b = input[inputIndex++];
				}
				if (b != 10)
				{
					if (this.encodedSize >= this.encoded.Length)
					{
						throw new ByteEncoderException(EncodersStrings.BinHexDecoderLineTooLong);
					}
					this.encoded[this.encodedSize++] = b;
				}
				else if (this.state == BinHexUtils.State.Starting)
				{
					uint num = 0U;
					while ((ulong)num < (ulong)((long)this.encodedSize))
					{
						if (!ByteEncoder.IsWhiteSpace(this.encoded[(int)((UIntPtr)num)]))
						{
							if ((ulong)((uint)((long)this.encodedSize - (long)((ulong)num))) >= (ulong)((long)BinHexDecoder.BinHexPrologue.Length) && ByteEncoder.BeginsWithNI(this.encoded, (int)num, BinHexDecoder.BinHexPrologue, BinHexDecoder.BinHexPrologue.Length))
							{
								this.state = BinHexUtils.State.Started;
								this.repeatCheck = false;
								this.accumCount = 0;
								this.accum = 0;
								this.header = null;
								break;
							}
							throw new ByteEncoderException(EncodersStrings.BinHexDecoderLineCorrupt);
						}
						else
						{
							num += 1U;
						}
					}
					this.encodedSize = 0;
				}
				else
				{
					while (this.encodedSize != 0 && (this.encoded[this.encodedSize - 1] == 13 || this.encoded[this.encodedSize - 1] == 32 || this.encoded[this.encodedSize - 1] == 9))
					{
						this.encodedSize--;
					}
					if (this.encodedSize != 0)
					{
						result = true;
						this.encodedIndex = 0;
						break;
					}
					if (this.state == BinHexUtils.State.Started)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00021250 File Offset: 0x0001F450
		private void TransitionState()
		{
			switch (this.state)
			{
			case BinHexUtils.State.HdrFileSize:
				if (this.scratch[0] > 63)
				{
					throw new ByteEncoderException(EncodersStrings.BinHexDecoderFileNameTooLong);
				}
				this.state = BinHexUtils.State.Header;
				this.bytesNeeded = (int)(this.scratch[0] + 21);
				return;
			case BinHexUtils.State.Header:
				this.header = new BinHexHeader(this.scratch);
				if (!this.dataForkOnly)
				{
					byte[] bytes = this.header.GetBytes();
					Buffer.BlockCopy(bytes, 0, this.extra, 0, bytes.Length);
					this.extraIndex = 0;
					this.extraSize = bytes.Length;
				}
				if (0L != this.header.DataForkLength)
				{
					this.state = BinHexUtils.State.Data;
					this.bytesNeeded = (int)this.header.DataForkLength;
					this.runningCRC = 0;
					return;
				}
				this.checksum = this.runningCRC;
				this.checksum = BinHexUtils.CalculateCrc(this.checksum);
				this.state = BinHexUtils.State.DataCRC;
				this.bytesNeeded = 2;
				this.scratchSize = this.scratch.Length;
				this.scratchIndex = 0;
				return;
			case BinHexUtils.State.Data:
				this.checksum = this.runningCRC;
				this.checksum = BinHexUtils.CalculateCrc(this.checksum);
				this.state = BinHexUtils.State.DataCRC;
				this.bytesNeeded = 2;
				this.scratchSize = this.scratch.Length;
				this.scratchIndex = 0;
				return;
			case BinHexUtils.State.DataCRC:
			{
				if ((this.checksum & 65280) >> 8 != (int)this.scratch[0] || (this.checksum & 255) != (ushort)this.scratch[1])
				{
					throw new ByteEncoderException(EncodersStrings.BinHexDecoderBadCrc);
				}
				if (this.dataForkOnly)
				{
					this.state = BinHexUtils.State.Ending;
					this.repeatCount = 0;
					this.encodedSize = 0;
					this.lineReady = false;
					return;
				}
				int num = (this.header.DataForkLength % 128L != 0L) ? ((int)(128L - this.header.DataForkLength % 128L)) : 0;
				if (num != 0)
				{
					Array.Clear(this.extra, 0, num);
					this.extraSize = num;
				}
				if (this.header.ResourceForkLength > 0L)
				{
					this.state = BinHexUtils.State.Resource;
					this.bytesNeeded = (int)this.header.ResourceForkLength;
					this.runningCRC = 0;
					return;
				}
				this.checksum = 0;
				this.checksum = BinHexUtils.CalculateCrc(this.checksum);
				this.state = BinHexUtils.State.ResourceCRC;
				this.bytesNeeded = 2;
				this.scratchSize = this.scratch.Length;
				this.scratchIndex = 0;
				return;
			}
			case BinHexUtils.State.Resource:
				this.checksum = this.runningCRC;
				this.checksum = BinHexUtils.CalculateCrc(this.checksum);
				this.state = BinHexUtils.State.ResourceCRC;
				this.bytesNeeded = 2;
				this.scratchSize = this.scratch.Length;
				this.scratchIndex = 0;
				return;
			case BinHexUtils.State.ResourceCRC:
			{
				if ((this.checksum & 65280) >> 8 != (int)this.scratch[0] || (this.checksum & 255) != (ushort)this.scratch[1])
				{
					throw new ByteEncoderException(EncodersStrings.BinHexDecoderBadResourceForkCrc);
				}
				int num = (this.header.ResourceForkLength % 128L != 0L) ? ((int)(128L - this.header.ResourceForkLength % 128L)) : 0;
				if (num != 0)
				{
					Array.Clear(this.extra, 0, num);
					this.extraSize = num;
				}
				this.state = BinHexUtils.State.Ending;
				this.repeatCount = 0;
				this.encodedSize = 0;
				this.lineReady = false;
				return;
			}
			}
			throw new Exception(EncodersStrings.BinHexDecoderInternalError);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000215BF File Offset: 0x0001F7BF
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00021620 File Offset: 0x0001F820
		// Note: this type is marked as 'beforefieldinit'.
		static BinHexDecoder()
		{
			int[] array = new int[4];
			array[1] = 4;
			array[2] = 2;
			BinHexDecoder.ShiftTabe = array;
			BinHexDecoder.BinHexPrologue = CTSGlobals.AsciiEncoding.GetBytes("(This file must be converted with BinHex");
			BinHexDecoder.Dictionary = new byte[]
			{
				127,
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				127,
				127,
				13,
				14,
				15,
				16,
				17,
				18,
				19,
				127,
				20,
				21,
				22,
				127,
				127,
				127,
				127,
				127,
				22,
				23,
				24,
				25,
				26,
				27,
				28,
				29,
				30,
				31,
				32,
				33,
				34,
				35,
				36,
				127,
				37,
				38,
				39,
				40,
				41,
				42,
				43,
				127,
				44,
				45,
				46,
				47,
				127,
				127,
				127,
				127,
				48,
				49,
				50,
				51,
				52,
				53,
				54,
				127,
				55,
				56,
				57,
				58,
				59,
				60,
				127,
				127,
				61,
				62,
				63,
				127,
				127,
				127,
				127,
				127
			};
		}

		// Token: 0x0400043A RID: 1082
		private static readonly int[] ShiftTabe;

		// Token: 0x0400043B RID: 1083
		private static readonly byte[] BinHexPrologue;

		// Token: 0x0400043C RID: 1084
		private static readonly byte[] Dictionary;

		// Token: 0x0400043D RID: 1085
		private BinHexUtils.State state;

		// Token: 0x0400043E RID: 1086
		private BinHexHeader header;

		// Token: 0x0400043F RID: 1087
		private byte[] encoded = new byte[128];

		// Token: 0x04000440 RID: 1088
		private int encodedIndex;

		// Token: 0x04000441 RID: 1089
		private int encodedSize;

		// Token: 0x04000442 RID: 1090
		private bool lineReady;

		// Token: 0x04000443 RID: 1091
		private bool repeatCheck;

		// Token: 0x04000444 RID: 1092
		private byte decodedByte;

		// Token: 0x04000445 RID: 1093
		private int repeatCount;

		// Token: 0x04000446 RID: 1094
		private int accumCount;

		// Token: 0x04000447 RID: 1095
		private int accum;

		// Token: 0x04000448 RID: 1096
		private byte[] extra = new byte[128];

		// Token: 0x04000449 RID: 1097
		private int extraSize;

		// Token: 0x0400044A RID: 1098
		private int extraIndex;

		// Token: 0x0400044B RID: 1099
		private int bytesNeeded;

		// Token: 0x0400044C RID: 1100
		private ushort runningCRC;

		// Token: 0x0400044D RID: 1101
		private ushort checksum;

		// Token: 0x0400044E RID: 1102
		private byte[] scratch = new byte[128];

		// Token: 0x0400044F RID: 1103
		private int scratchIndex;

		// Token: 0x04000450 RID: 1104
		private int scratchSize;

		// Token: 0x04000451 RID: 1105
		private bool dataForkOnly;
	}
}
