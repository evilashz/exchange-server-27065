using System;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x0200008E RID: 142
	public class Base64Encoder : ByteEncoder
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00020333 File Offset: 0x0001E533
		public Base64Encoder() : this(76)
		{
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002033D File Offset: 0x0001E53D
		public Base64Encoder(int lineLength)
		{
			this.LineLength = lineLength;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00020358 File Offset: 0x0001E558
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00020360 File Offset: 0x0001E560
		public int LineLength
		{
			get
			{
				return this.lineMaximum;
			}
			set
			{
				if (value != 0 && value != 76)
				{
					throw new ArgumentOutOfRangeException("LineLength");
				}
				this.lineMaximum = value - value % 4;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00020380 File Offset: 0x0001E580
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
			if (this.encodedSize != 0)
			{
				int num = Math.Min(this.encodedSize, outputSize);
				if ((num & 4) != 0)
				{
					output[outputIndex++] = this.encoded[this.encodedIndex++];
					output[outputIndex++] = this.encoded[this.encodedIndex++];
					output[outputIndex++] = this.encoded[this.encodedIndex++];
					output[outputIndex++] = this.encoded[this.encodedIndex++];
				}
				if ((num & 2) != 0)
				{
					output[outputIndex++] = this.encoded[this.encodedIndex++];
					output[outputIndex++] = this.encoded[this.encodedIndex++];
				}
				if ((num & 1) != 0)
				{
					output[outputIndex++] = this.encoded[this.encodedIndex++];
				}
				outputSize -= num;
				this.encodedSize -= num;
				if (this.encodedSize != 0)
				{
					inputUsed = 0;
					outputUsed = num;
					completed = false;
					return;
				}
				this.encodedIndex = 0;
			}
			int num2 = inputIndex + inputSize;
			while (6 <= outputSize && 3 <= this.decodedSize + (num2 - inputIndex))
			{
				while (3 != this.decodedSize)
				{
					this.decoded = (this.decoded << 8 | (uint)input[inputIndex++]);
					this.decodedSize++;
				}
				output[outputIndex + 3] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
				this.decoded >>= 6;
				output[outputIndex + 2] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
				this.decoded >>= 6;
				output[outputIndex + 1] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
				this.decoded >>= 6;
				output[outputIndex] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
				outputIndex += 4;
				outputSize -= 4;
				this.decodedSize = 0;
				if (this.lineMaximum != 0)
				{
					this.lineOffset += 4;
					if (this.lineOffset == this.lineMaximum)
					{
						output[outputIndex++] = 13;
						output[outputIndex++] = 10;
						outputSize -= 2;
						this.lineOffset = 0;
						this.lineCount++;
					}
				}
			}
			if (outputSize != 0)
			{
				while (num2 != inputIndex && 3 != this.decodedSize)
				{
					this.decoded = (this.decoded << 8 | (uint)input[inputIndex++]);
					this.decodedSize++;
				}
				if (3 == this.decodedSize || (flush && (this.decodedSize != 0 || (this.lineMaximum != 0 && this.lineOffset != 0))))
				{
					this.encodedSize = 0;
					switch (this.decodedSize)
					{
					case 0:
						this.encodedSize = 0;
						break;
					case 1:
						this.encoded[3] = 61;
						this.encoded[2] = 61;
						this.decoded <<= 4;
						this.encoded[1] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.decoded >>= 6;
						this.encoded[0] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.encodedSize = 4;
						break;
					case 2:
						this.encoded[3] = 61;
						this.decoded <<= 2;
						this.encoded[2] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.decoded >>= 6;
						this.encoded[1] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.decoded >>= 6;
						this.encoded[0] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.encodedSize = 4;
						break;
					case 3:
						this.encoded[3] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.decoded >>= 6;
						this.encoded[2] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.decoded >>= 6;
						this.encoded[1] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.decoded >>= 6;
						this.encoded[0] = ByteEncoder.Tables.ByteToBase64[(int)((UIntPtr)(this.decoded & 63U))];
						this.encodedSize = 4;
						break;
					}
					this.decodedSize = 0;
					this.lineOffset += this.encodedSize;
					if (this.lineMaximum != 0 && (this.lineOffset == this.lineMaximum || (flush && num2 == inputIndex)))
					{
						this.encoded[this.encodedSize++] = 13;
						this.encoded[this.encodedSize++] = 10;
						this.lineOffset = 0;
						this.lineCount++;
					}
					if (this.encodedSize != 0)
					{
						int num3 = Math.Min(this.encodedSize, outputSize);
						if ((num3 & 4) != 0)
						{
							output[outputIndex++] = this.encoded[this.encodedIndex++];
							output[outputIndex++] = this.encoded[this.encodedIndex++];
							output[outputIndex++] = this.encoded[this.encodedIndex++];
							output[outputIndex++] = this.encoded[this.encodedIndex++];
						}
						if ((num3 & 2) != 0)
						{
							output[outputIndex++] = this.encoded[this.encodedIndex++];
							output[outputIndex++] = this.encoded[this.encodedIndex++];
						}
						if ((num3 & 1) != 0)
						{
							output[outputIndex++] = this.encoded[this.encodedIndex++];
						}
						outputSize -= num3;
						this.encodedSize -= num3;
						if (this.encodedSize == 0)
						{
							this.encodedIndex = 0;
						}
					}
				}
			}
			outputUsed = outputIndex - outputUsed;
			inputUsed = inputIndex - inputUsed;
			completed = (num2 == inputIndex && this.encodedSize == 0 && (!flush || 0 == this.decodedSize));
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00020ACC File Offset: 0x0001ECCC
		public sealed override int GetMaxByteCount(int dataCount)
		{
			int num = (dataCount + 3) / 3 * 4;
			int num2 = num;
			if (this.lineMaximum != 0)
			{
				int num3 = (num + this.lineMaximum) / this.lineMaximum;
				num2 += num3 * 2;
			}
			return num2;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00020B02 File Offset: 0x0001ED02
		public sealed override void Reset()
		{
			this.decodedSize = 0;
			this.encodedSize = 0;
			this.encodedIndex = 0;
			this.lineOffset = 0;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00020B20 File Offset: 0x0001ED20
		public sealed override ByteEncoder Clone()
		{
			Base64Encoder base64Encoder = base.MemberwiseClone() as Base64Encoder;
			base64Encoder.encoded = (this.encoded.Clone() as byte[]);
			return base64Encoder;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00020B50 File Offset: 0x0001ED50
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000430 RID: 1072
		private const int EncodedBlockSize = 4;

		// Token: 0x04000431 RID: 1073
		private const int DecodedBlockSize = 3;

		// Token: 0x04000432 RID: 1074
		private byte[] encoded = new byte[6];

		// Token: 0x04000433 RID: 1075
		private int encodedIndex;

		// Token: 0x04000434 RID: 1076
		private int encodedSize;

		// Token: 0x04000435 RID: 1077
		private uint decoded;

		// Token: 0x04000436 RID: 1078
		private int decodedSize;

		// Token: 0x04000437 RID: 1079
		private int lineMaximum;

		// Token: 0x04000438 RID: 1080
		private int lineOffset;

		// Token: 0x04000439 RID: 1081
		private int lineCount;
	}
}
