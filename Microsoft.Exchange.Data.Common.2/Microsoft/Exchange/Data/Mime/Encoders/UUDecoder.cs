﻿using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x0200009D RID: 157
	public class UUDecoder : ByteEncoder
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000244A4 File Offset: 0x000226A4
		public string FileName
		{
			get
			{
				if (this.fileName != null)
				{
					return CTSGlobals.AsciiEncoding.GetString(this.fileName, 0, this.fileName.Length);
				}
				return string.Empty;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000244D0 File Offset: 0x000226D0
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
			if (this.state == UUDecoder.State.Starting)
			{
				this.lineReady = false;
				this.encodedSize = 0;
				this.encodedBytes = 0;
				this.decodedSize = 0;
				this.state = UUDecoder.State.Prologue;
			}
			int num = inputIndex + inputSize;
			inputUsed = inputIndex;
			outputUsed = outputIndex;
			if (this.decodedSize != 0)
			{
				int num2 = Math.Min(outputSize, this.decodedSize);
				if ((num2 & 2) != 0)
				{
					output[outputIndex++] = this.decoded[this.decodedIndex++];
					output[outputIndex++] = this.decoded[this.decodedIndex++];
				}
				if ((num2 & 1) != 0)
				{
					output[outputIndex++] = this.decoded[this.decodedIndex++];
				}
				outputSize -= num2;
				this.decodedSize -= num2;
				if (this.decodedSize != 0)
				{
					inputUsed = 0;
					outputUsed = num2;
					completed = false;
					return;
				}
				this.decodedIndex = 0;
			}
			byte[] array = new byte[4];
			do
			{
				if (this.lineReady)
				{
					int num3 = Math.Min(outputSize / 3, this.encodedBytes / 3);
					for (int i = 0; i < num3; i++)
					{
						array[0] = UUDecoder.UUDecode(this.encoded[this.encodedIndex]);
						array[1] = UUDecoder.UUDecode(this.encoded[this.encodedIndex + 1]);
						array[2] = UUDecoder.UUDecode(this.encoded[this.encodedIndex + 2]);
						array[3] = UUDecoder.UUDecode(this.encoded[this.encodedIndex + 3]);
						output[outputIndex] = (byte)((int)array[0] << 2 | array[1] >> 4);
						output[outputIndex + 1] = (byte)((int)array[1] << 4 | array[2] >> 2);
						output[outputIndex + 2] = (byte)((int)array[2] << 6 | (int)array[3]);
						this.encodedIndex += 4;
						outputIndex += 3;
					}
					this.encodedBytes -= 3 * num3;
					outputSize -= 3 * num3;
					this.decodedSize = 0;
					if (0 < outputSize && 0 < this.encodedBytes)
					{
						array[0] = UUDecoder.UUDecode(this.encoded[this.encodedIndex]);
						array[1] = UUDecoder.UUDecode(this.encoded[this.encodedIndex + 1]);
						array[2] = UUDecoder.UUDecode(this.encoded[this.encodedIndex + 2]);
						array[3] = UUDecoder.UUDecode(this.encoded[this.encodedIndex + 3]);
						this.decodedSize = Math.Min(this.encodedBytes, 3);
						this.decodedIndex = 0;
						this.decoded[0] = (byte)((int)array[0] << 2 | array[1] >> 4);
						this.decoded[1] = (byte)((int)array[1] << 4 | array[2] >> 2);
						this.decoded[2] = (byte)((int)array[2] << 6 | (int)array[3]);
						this.encodedBytes -= this.decodedSize;
						this.encodedIndex += 4;
						int num4 = Math.Min(outputSize, this.decodedSize);
						if ((num4 & 2) != 0)
						{
							output[outputIndex++] = this.decoded[this.decodedIndex++];
							output[outputIndex++] = this.decoded[this.decodedIndex++];
						}
						if ((num4 & 1) != 0)
						{
							output[outputIndex++] = this.decoded[this.decodedIndex++];
						}
						outputSize -= num4;
						this.decodedSize -= num4;
					}
					if (this.encodedBytes == 0)
					{
						this.encodedIndex = 0;
						this.encodedSize = 0;
						this.lineReady = false;
					}
					if (outputSize == 0)
					{
						break;
					}
				}
				while (num != inputIndex || (flush && this.encodedSize != 0))
				{
					byte b = (num != inputIndex) ? input[inputIndex++] : 10;
					if (10 != b)
					{
						if (this.encodedSize == this.encoded.Length)
						{
							int num5 = this.encoded.Length + 128;
							if (num5 > 4098)
							{
								goto IL_485;
							}
							byte[] dst = new byte[num5];
							Buffer.BlockCopy(this.encoded, 0, dst, 0, this.encoded.Length);
							this.encoded = dst;
						}
						this.encoded[this.encodedSize++] = b;
					}
					else
					{
						if (!this.UULineGood())
						{
							goto IL_4D0;
						}
						if (this.encodedBytes != 0)
						{
							this.lineReady = true;
							break;
						}
						this.encodedSize = 0;
					}
				}
			}
			while (this.lineReady);
			goto IL_4FC;
			IL_485:
			throw new ByteEncoderException(EncodersStrings.UUDecoderInvalidData);
			IL_4D0:
			throw new ByteEncoderException(EncodersStrings.UUDecoderInvalidDataBadLine);
			IL_4FC:
			inputUsed = inputIndex - inputUsed;
			outputUsed = outputIndex - outputUsed;
			completed = (num == inputIndex && (!flush || (this.decodedSize == 0 && !this.lineReady)));
			if (flush && completed)
			{
				this.state = UUDecoder.State.Starting;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00024A20 File Offset: 0x00022C20
		public sealed override int GetMaxByteCount(int dataCount)
		{
			int num = dataCount / 63;
			dataCount -= num * 3;
			return (dataCount + 4) / 4 * 3;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00024A44 File Offset: 0x00022C44
		public sealed override void Reset()
		{
			this.state = UUDecoder.State.Starting;
			this.fileName = null;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00024A54 File Offset: 0x00022C54
		public sealed override ByteEncoder Clone()
		{
			UUDecoder uudecoder = base.MemberwiseClone() as UUDecoder;
			uudecoder.decoded = (this.decoded.Clone() as byte[]);
			uudecoder.encoded = (this.encoded.Clone() as byte[]);
			if (this.fileName != null)
			{
				uudecoder.fileName = (this.fileName.Clone() as byte[]);
			}
			return uudecoder;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00024AB8 File Offset: 0x00022CB8
		private static byte UUDecode(byte c)
		{
			return (c == 96) ? 0 : (c - 32 & 63);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00024ACC File Offset: 0x00022CCC
		private bool UULineGood()
		{
			this.encodedBytes = 0;
			int num = this.encodedSize;
			while (this.encodedSize > 0 && ByteEncoder.IsWhiteSpace(this.encoded[this.encodedSize - 1]))
			{
				this.encodedSize--;
			}
			if (this.encodedSize == 0)
			{
				return true;
			}
			if (this.state == UUDecoder.State.Prologue)
			{
				if (ByteEncoder.BeginsWithNI(this.encoded, 0, UUDecoder.Prologue, this.encodedSize))
				{
					int num2 = this.encodedSize - 6;
					int num3 = 6;
					while (num2 != 0 && ByteEncoder.IsWhiteSpace(this.encoded[num3]))
					{
						num3++;
						num2--;
					}
					if (num2 == 0 || this.encoded[num3] < 48 || this.encoded[num3] > 55)
					{
						return true;
					}
					do
					{
						num3++;
						num2--;
					}
					while (num2 != 0 && this.encoded[num3] >= 48 && this.encoded[num3] <= 55);
					if (num2 == 0 || !ByteEncoder.IsWhiteSpace(this.encoded[num3]))
					{
						return true;
					}
					do
					{
						num3++;
						num2--;
					}
					while (num2 != 0 && ByteEncoder.IsWhiteSpace(this.encoded[num3]));
					if (num2 <= 128)
					{
						this.fileName = new byte[num2];
						ByteEncoder.BlockCopy(this.encoded, num3, this.fileName, 0, num2);
					}
					return true;
				}
			}
			else
			{
				if (this.state == UUDecoder.State.Ending)
				{
					return true;
				}
				if (ByteEncoder.BeginsWithNI(this.encoded, 0, UUDecoder.Prologue, this.encodedSize))
				{
					return true;
				}
			}
			if (true)
			{
				int num4 = (int)UUDecoder.UUDecode(this.encoded[0]);
				int num5 = 0;
				int num6 = num4 % 3;
				if (num6 != 0)
				{
					num6++;
					num5 = 4 - num6;
				}
				int num7 = 4 * (num4 / 3) + num6 + 1;
				if (this.encodedSize < num7)
				{
					if (num < num7)
					{
						goto IL_285;
					}
					this.encodedSize = num7;
				}
				if (num7 != this.encodedSize && num7 + num5 != this.encodedSize)
				{
					if (num7 + 1 == this.encodedSize || num7 + num5 + 1 == this.encodedSize)
					{
						this.encodedSize--;
					}
					else
					{
						if (num7 - 1 != this.encodedSize && num7 + num5 - 1 == this.encodedSize)
						{
							goto IL_285;
						}
						goto IL_285;
					}
				}
				this.encodedBytes = num4;
				if (num5 != 0 && num7 == this.encodedSize)
				{
					this.encoded[num7] = 32;
					if (num5 > 1)
					{
						this.encoded[num7 + 1] = 32;
					}
					this.encodedSize += num5;
				}
				this.encodedSize--;
				this.encodedIndex = 1;
				if (this.encodedBytes == 0)
				{
					this.state = UUDecoder.State.Ending;
				}
				else
				{
					this.state = UUDecoder.State.Data;
				}
				return true;
			}
			IL_285:
			if (ByteEncoder.BeginsWithNI(this.encoded, 0, UUDecoder.Epilogue, this.encodedSize))
			{
				this.state = UUDecoder.State.Ending;
				return true;
			}
			return false;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00024D81 File Offset: 0x00022F81
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x040004BD RID: 1213
		private const int EncodedBlockSize = 4;

		// Token: 0x040004BE RID: 1214
		private const int DecodedBlockSize = 3;

		// Token: 0x040004BF RID: 1215
		private const int NewlineBytes = 2;

		// Token: 0x040004C0 RID: 1216
		private const int BlockBytes = 128;

		// Token: 0x040004C1 RID: 1217
		private const int MaximumBytes = 4096;

		// Token: 0x040004C2 RID: 1218
		private static readonly byte[] Prologue = CTSGlobals.AsciiEncoding.GetBytes("begin ");

		// Token: 0x040004C3 RID: 1219
		private static readonly byte[] Epilogue = CTSGlobals.AsciiEncoding.GetBytes("end");

		// Token: 0x040004C4 RID: 1220
		private UUDecoder.State state;

		// Token: 0x040004C5 RID: 1221
		private bool lineReady;

		// Token: 0x040004C6 RID: 1222
		private int encodedBytes;

		// Token: 0x040004C7 RID: 1223
		private byte[] encoded = new byte[130];

		// Token: 0x040004C8 RID: 1224
		private int encodedSize;

		// Token: 0x040004C9 RID: 1225
		private int encodedIndex;

		// Token: 0x040004CA RID: 1226
		private byte[] decoded = new byte[3];

		// Token: 0x040004CB RID: 1227
		private int decodedIndex;

		// Token: 0x040004CC RID: 1228
		private int decodedSize;

		// Token: 0x040004CD RID: 1229
		private byte[] fileName;

		// Token: 0x0200009E RID: 158
		private enum State
		{
			// Token: 0x040004CF RID: 1231
			Starting,
			// Token: 0x040004D0 RID: 1232
			Prologue,
			// Token: 0x040004D1 RID: 1233
			Data,
			// Token: 0x040004D2 RID: 1234
			Ending
		}
	}
}
