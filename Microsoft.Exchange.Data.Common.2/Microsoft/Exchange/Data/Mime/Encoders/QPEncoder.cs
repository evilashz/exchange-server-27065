using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x0200009B RID: 155
	public class QPEncoder : ByteEncoder
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00023919 File Offset: 0x00021B19
		public QPEncoder()
		{
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002392D File Offset: 0x00021B2D
		public QPEncoder(bool ebcdicDictionary)
		{
			this.EbcdicDictionary = ebcdicDictionary;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00023948 File Offset: 0x00021B48
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x00023950 File Offset: 0x00021B50
		public bool EbcdicDictionary
		{
			get
			{
				return this.modeEBCDIC;
			}
			set
			{
				this.modeEBCDIC = value;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002395C File Offset: 0x00021B5C
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
			inputUsed = 0;
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
					outputUsed = outputIndex - outputUsed;
					completed = false;
					return;
				}
				this.encodedIndex = 0;
			}
			inputUsed = inputIndex;
			int num2 = inputSize + inputIndex;
			while (num2 != inputIndex || (flush && (this.state != QPEncoder.State.Normal || this.lineOffset != 0)))
			{
				if (this.IsState(QPEncoder.State.ForceSplit))
				{
					if (this.lineOffset + 1 > 76)
					{
						throw new Exception(EncodersStrings.QPEncoderNoSpaceForLineBreak);
					}
					this.encoded[this.encodedSize++] = 61;
					this.encoded[this.encodedSize++] = 13;
					this.encoded[this.encodedSize++] = 10;
					this.lineOffset++;
					this.lineOffset = 0;
					this.state &= ~QPEncoder.State.ForceSplit;
				}
				else if (num2 != inputIndex && input[inputIndex] == 10)
				{
					if (this.IsState(QPEncoder.State.LastCR))
					{
						if (this.IsState(QPEncoder.State.LastWSpCR))
						{
							if (this.lineOffset + 3 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = 61;
							this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[this.lastWSp >> 4];
							this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[(int)(this.lastWSp & 15)];
							this.lineOffset += 3;
							this.state &= ~QPEncoder.State.LastWSpCR;
						}
					}
					else
					{
						if (this.IsState(QPEncoder.State.LastWSp))
						{
							if (this.lineOffset + 1 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = this.lastWSp;
							this.lineOffset++;
							this.state &= ~QPEncoder.State.LastWSp;
						}
						if (this.lineOffset + 4 > 76)
						{
							this.state |= QPEncoder.State.ForceSplit;
							continue;
						}
						this.encoded[this.encodedSize++] = 61;
						this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[0];
						this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[10];
						this.encoded[this.encodedSize++] = 61;
						this.lineOffset += 4;
					}
					this.encoded[this.encodedSize++] = 13;
					this.encoded[this.encodedSize++] = 10;
					this.lineOffset = 0;
					this.state &= ~QPEncoder.State.LastCR;
					inputIndex++;
				}
				else
				{
					if (this.IsState(QPEncoder.State.LastWSp))
					{
						if (num2 == inputIndex || input[inputIndex] != 13)
						{
							if (this.lineOffset + 1 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = this.lastWSp;
							this.lineOffset++;
							this.state &= ~QPEncoder.State.LastWSp;
						}
					}
					else if (this.IsState(QPEncoder.State.LastCR))
					{
						if (this.IsState(QPEncoder.State.LastWSpCR))
						{
							if (this.lineOffset + 1 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = this.lastWSp;
							this.lineOffset++;
							this.state &= ~QPEncoder.State.LastWSpCR;
						}
						if (this.lineOffset + 3 + 1 > 76)
						{
							this.state |= QPEncoder.State.ForceSplit;
							continue;
						}
						this.encoded[this.encodedSize++] = 61;
						this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[0];
						this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[13];
						this.lineOffset += 3;
						this.state &= ~QPEncoder.State.LastCR;
					}
					if (num2 == inputIndex)
					{
						if (this.lineOffset != 0)
						{
							this.state |= QPEncoder.State.ForceSplit;
							continue;
						}
					}
					else
					{
						byte b = input[inputIndex];
						if (QPEncoder.IsSafe(b))
						{
							if (this.lineOffset + 1 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = b;
							this.lineOffset++;
						}
						else if (b == 13)
						{
							this.state |= QPEncoder.State.LastCR;
							if (QPEncoder.State.LastWSp == (this.state & QPEncoder.State.LastWSp))
							{
								this.state |= QPEncoder.State.LastWSpCR;
								this.state &= ~QPEncoder.State.LastWSp;
							}
						}
						else if (b == 32 || b == 9)
						{
							this.state |= QPEncoder.State.LastWSp;
							this.lastWSp = b;
						}
						else if (QPEncoder.IsQPEncode(b))
						{
							if (this.lineOffset + 3 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = 61;
							this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[b >> 4];
							this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[(int)(b & 15)];
							this.lineOffset += 3;
						}
						else if (this.modeEBCDIC)
						{
							if (this.lineOffset + 3 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = 61;
							this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[b >> 4];
							this.encoded[this.encodedSize++] = ByteEncoder.NibbleToHex[(int)(b & 15)];
							this.lineOffset += 3;
						}
						else
						{
							if (this.lineOffset + 1 + 1 > 76)
							{
								this.state |= QPEncoder.State.ForceSplit;
								continue;
							}
							this.encoded[this.encodedSize++] = b;
							this.lineOffset++;
						}
						inputIndex++;
					}
				}
				this.encodedIndex = 0;
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
				if (this.encodedSize != 0)
				{
					break;
				}
				this.encodedIndex = 0;
			}
			outputUsed = outputIndex - outputUsed;
			inputUsed = inputIndex - inputUsed;
			completed = (num2 == inputIndex && this.encodedSize == 0 && (!flush || QPEncoder.State.Normal == this.state));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000243CC File Offset: 0x000225CC
		public sealed override int GetMaxByteCount(int dataCount)
		{
			int num = dataCount * 3;
			return num + (num + 76) / 76 * 2;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000243EC File Offset: 0x000225EC
		public sealed override void Reset()
		{
			this.encodedSize = 0;
			this.lineOffset = 0;
			this.state = QPEncoder.State.Normal;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00024404 File Offset: 0x00022604
		public sealed override ByteEncoder Clone()
		{
			QPEncoder qpencoder = base.MemberwiseClone() as QPEncoder;
			qpencoder.encoded = (this.encoded.Clone() as byte[]);
			return qpencoder;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00024434 File Offset: 0x00022634
		private static bool IsQPEncode(byte bT)
		{
			return bT >= 128 || 0 != (byte)(ByteEncoder.Tables.CharacterTraits[(int)bT] & ByteEncoder.Tables.CharClasses.QPEncode);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00024450 File Offset: 0x00022650
		private static bool IsSafe(byte bT)
		{
			return bT < 128 && 0 == (byte)(ByteEncoder.Tables.CharacterTraits[(int)bT] & (ByteEncoder.Tables.CharClasses.QPEncode | ByteEncoder.Tables.CharClasses.QPUnsafe | ByteEncoder.Tables.CharClasses.QPWSp));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0002446A File Offset: 0x0002266A
		private bool IsState(QPEncoder.State state)
		{
			return state == (this.state & state);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00024477 File Offset: 0x00022677
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x040004AF RID: 1199
		private const int LineMaximum = 76;

		// Token: 0x040004B0 RID: 1200
		private int lineOffset;

		// Token: 0x040004B1 RID: 1201
		private byte[] encoded = new byte[7];

		// Token: 0x040004B2 RID: 1202
		private int encodedIndex;

		// Token: 0x040004B3 RID: 1203
		private int encodedSize;

		// Token: 0x040004B4 RID: 1204
		private byte lastWSp;

		// Token: 0x040004B5 RID: 1205
		private bool modeEBCDIC;

		// Token: 0x040004B6 RID: 1206
		private QPEncoder.State state;

		// Token: 0x0200009C RID: 156
		[Flags]
		private enum State
		{
			// Token: 0x040004B8 RID: 1208
			Normal = 0,
			// Token: 0x040004B9 RID: 1209
			ForceSplit = 1,
			// Token: 0x040004BA RID: 1210
			LastWSp = 2,
			// Token: 0x040004BB RID: 1211
			LastCR = 4,
			// Token: 0x040004BC RID: 1212
			LastWSpCR = 8
		}
	}
}
