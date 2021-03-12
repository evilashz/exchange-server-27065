using System;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x0200008D RID: 141
	public class Base64Decoder : ByteEncoder
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0001FE96 File Offset: 0x0001E096
		public Base64Decoder()
		{
			this.decoded = new byte[3];
			this.encoded = new byte[4];
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
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
			int num = inputIndex + inputSize;
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
				if (this.decodedSize == 0)
				{
					this.decodedIndex = 0;
				}
			}
			while (this.decodedSize == 0 && (inputIndex != num || (flush && this.encodedSize != 0)))
			{
				while (inputIndex != num && 4 != this.encodedSize)
				{
					byte b = input[inputIndex++];
					if (b != 61 && !ByteEncoder.IsWhiteSpace(b))
					{
						b -= 32;
						if ((int)b < ByteEncoder.Tables.Base64ToByte.Length)
						{
							b = ByteEncoder.Tables.Base64ToByte[(int)b];
							if (b < 64)
							{
								this.encoded[this.encodedSize++] = b;
							}
						}
					}
				}
				if (4 == this.encodedSize && 3 <= outputSize)
				{
					output[outputIndex] = (byte)((int)this.encoded[0] << 2 | this.encoded[1] >> 4);
					output[outputIndex + 1] = (byte)((int)this.encoded[1] << 4 | this.encoded[2] >> 2);
					output[outputIndex + 2] = (byte)((int)this.encoded[2] << 6 | (int)this.encoded[3]);
					outputSize -= 3;
					outputIndex += 3;
					this.encodedSize = 0;
				}
				else
				{
					if (4 != this.encodedSize && (!flush || num != inputIndex))
					{
						break;
					}
					if (2 > this.encodedSize)
					{
						this.encodedSize = 0;
						break;
					}
					if (this.encodedSize > 1)
					{
						this.decoded[this.decodedSize++] = (byte)((int)this.encoded[0] << 2 | this.encoded[1] >> 4);
					}
					if (this.encodedSize > 2)
					{
						this.decoded[this.decodedSize++] = (byte)((int)this.encoded[1] << 4 | this.encoded[2] >> 2);
					}
					if (this.encodedSize > 3)
					{
						this.decoded[this.decodedSize++] = (byte)((int)this.encoded[2] << 6 | (int)this.encoded[3]);
					}
					this.encodedSize = 0;
					int num3 = Math.Min(outputSize, this.decodedSize);
					if ((num3 & 2) != 0)
					{
						output[outputIndex++] = this.decoded[this.decodedIndex++];
						output[outputIndex++] = this.decoded[this.decodedIndex++];
					}
					if ((num3 & 1) != 0)
					{
						output[outputIndex++] = this.decoded[this.decodedIndex++];
					}
					outputSize -= num3;
					this.decodedSize -= num3;
					if (this.decodedSize == 0)
					{
						this.decodedIndex = 0;
					}
				}
			}
			outputUsed = outputIndex - outputUsed;
			inputUsed = inputIndex - inputUsed;
			completed = (num == inputIndex && this.decodedSize == 0 && (!flush || 0 == this.encodedSize));
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000202B4 File Offset: 0x0001E4B4
		public sealed override int GetMaxByteCount(int dataCount)
		{
			return (dataCount + 4) / 4 * 3;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000202CC File Offset: 0x0001E4CC
		public sealed override void Reset()
		{
			this.decodedSize = 0;
			this.decodedIndex = 0;
			this.encodedSize = 0;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000202E4 File Offset: 0x0001E4E4
		public sealed override ByteEncoder Clone()
		{
			Base64Decoder base64Decoder = base.MemberwiseClone() as Base64Decoder;
			base64Decoder.decoded = (this.decoded.Clone() as byte[]);
			base64Decoder.encoded = (this.encoded.Clone() as byte[]);
			return base64Decoder;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002032A File Offset: 0x0001E52A
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000429 RID: 1065
		private const int EncodedBlockSize = 4;

		// Token: 0x0400042A RID: 1066
		private const int DecodedBlockSize = 3;

		// Token: 0x0400042B RID: 1067
		private byte[] decoded;

		// Token: 0x0400042C RID: 1068
		private int decodedSize;

		// Token: 0x0400042D RID: 1069
		private int decodedIndex;

		// Token: 0x0400042E RID: 1070
		private byte[] encoded;

		// Token: 0x0400042F RID: 1071
		private int encodedSize;
	}
}
