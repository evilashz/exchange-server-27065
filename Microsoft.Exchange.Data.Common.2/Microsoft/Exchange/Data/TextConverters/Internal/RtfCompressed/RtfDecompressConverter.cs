using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters.Internal.RtfCompressed
{
	// Token: 0x0200027D RID: 637
	internal class RtfDecompressConverter : RtfCompressCommon, IProducerConsumer, IDisposable
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x000CB818 File Offset: 0x000C9A18
		public RtfDecompressConverter(Stream input, bool push, Stream output, bool disableFastLoop, IResultsFeedback resultFeedback, int inputBufferSize, int outputBufferSize) : base(input, push, output, inputBufferSize, outputBufferSize)
		{
			this.resultFeedback = resultFeedback;
			this.disableFastLoop = disableFastLoop;
			this.window = new byte[4130];
			Buffer.BlockCopy(RtfCompressCommon.PreloadData, 0, this.window, 0, RtfCompressCommon.PreloadData.Length);
			this.windowCurrent = RtfCompressCommon.PreloadData.Length;
			Buffer.BlockCopy(RtfCompressCommon.PreloadData, 0, this.window, 4096, 34);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000CB890 File Offset: 0x000C9A90
		public void Run()
		{
			if (this.endOfFile)
			{
				return;
			}
			if (this.writeCurrent == this.writeEnd && !base.GetOutputSpace())
			{
				return;
			}
			if (this.readCurrent == this.readEnd)
			{
				if (!this.inputEndOfFile && !base.ReadMoreData())
				{
					return;
				}
				if (this.inputEndOfFile && (this.logicalEOS || this.numFlags == 0 || (this.flags & 1) == 0 || this.pointerRead < 2))
				{
					this.endOfFile = true;
					if (this.pullSink != null)
					{
						this.pullSink.ReportEndOfFile();
					}
					else
					{
						this.pushSink.Flush();
					}
					if (!this.uncompressed && !this.logicalEOS)
					{
						throw new TextConvertersException("data truncated");
					}
					return;
				}
				else
				{
					if (this.logicalEOS && this.readCurrent != this.readEnd)
					{
						this.readCurrent = this.readEnd;
						base.ReportRead();
						return;
					}
					if (!this.headerRead)
					{
						byte[] readBuffer;
						int num2;
						if (this.headerBuffer != null || this.readEnd - this.readCurrent < 16)
						{
							int num = Math.Min(16 - this.headerBufferCurrent, this.readEnd - this.readCurrent);
							if (this.headerBuffer == null)
							{
								this.headerBuffer = new byte[16];
							}
							Buffer.BlockCopy(this.readBuffer, this.readCurrent, this.headerBuffer, this.headerBufferCurrent, num);
							this.readCurrent += num;
							this.headerBufferCurrent += num;
							if (this.headerBufferCurrent != 16)
							{
								base.ReportRead();
								return;
							}
							readBuffer = this.headerBuffer;
							num2 = 0;
						}
						else
						{
							readBuffer = this.readBuffer;
							num2 = this.readCurrent;
							this.readCurrent += 16;
						}
						uint num3 = BitConverter.ToUInt32(readBuffer, num2 + 8);
						this.expectedCount = BitConverter.ToInt32(readBuffer, num2) - 12;
						if (num3 == 1095517517U)
						{
							this.uncompressed = true;
						}
						if ((num3 != 1095517517U && num3 != 1967544908U) || this.expectedCount > 2147483647 || this.expectedCount < 0)
						{
							this.readCurrent = this.readEnd;
							throw new TextConvertersException("invalid compressed RTF header");
						}
						if (this.resultFeedback != null)
						{
							this.resultFeedback.Set(ConfigParameter.RtfCompressionMode, this.uncompressed ? RtfCompressionMode.Uncompressed : RtfCompressionMode.Compressed);
						}
						this.expectedCrc = BitConverter.ToUInt32(readBuffer, num2 + 12);
						this.headerRead = true;
					}
				}
			}
			if (this.uncompressed)
			{
				int num4 = Math.Min(this.readEnd - this.readCurrent, this.writeEnd - this.writeCurrent);
				Buffer.BlockCopy(this.readBuffer, this.readCurrent, this.writeBuffer, this.writeCurrent, num4);
				this.readCurrent += num4;
				this.writeCurrent += num4;
				base.ReportRead();
				base.FlushOutput();
				return;
			}
			if (this.numFlags != 0 && (this.flags & 1) != 0 && this.pointerRead != 0)
			{
				if (this.pointerRead == 1)
				{
					int num5 = (int)this.readBuffer[this.readCurrent++];
					this.crc = CRC32.ComputeCRC(this.crc, (uint)num5);
					this.match = (this.match << 8 | num5);
					this.matchLength = (this.match & 15) + 2;
					this.match >>= 4;
					this.pointerRead++;
					this.expectedCount--;
				}
				if (this.match == (this.windowCurrent & 4095))
				{
					while (this.expectedCount > 0 && this.readCurrent != this.readEnd)
					{
						int num5 = (int)this.readBuffer[this.readCurrent++];
						this.crc = CRC32.ComputeCRC(this.crc, (uint)num5);
						this.expectedCount--;
					}
					this.readCurrent = this.readEnd;
					if (this.expectedCount <= 0)
					{
						this.logicalEOS = true;
						if (this.crc != this.expectedCrc)
						{
							throw new TextConvertersException("CRC does not match, data corrupted");
						}
						if (this.pullSource != null)
						{
							this.endOfFile = true;
							this.pullSink.ReportEndOfFile();
							return;
						}
						if (this.expectedCount < 0)
						{
							throw new TextConvertersException("expected count in header is smaller than actual, data corrupted");
						}
					}
					else if (this.inputEndOfFile)
					{
						this.endOfFile = true;
						throw new TextConvertersException("data truncated");
					}
				}
				else if (this.ExpandMatchWithOverflow())
				{
					this.numFlags--;
					this.flags >>= 1;
					this.pointerRead = 0;
				}
			}
			if (!this.disableFastLoop && this.readCurrent + 3 <= this.readEnd && this.writeCurrent + 17 <= this.writeEnd)
			{
				this.FastLoop();
			}
			if (this.pointerRead == 0)
			{
				this.SlowLoop();
			}
			base.ReportRead();
			if (this.pushSink == null || this.writeCurrent + 17 > this.writeEnd || this.logicalEOS)
			{
				base.FlushOutput();
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000CBD7D File Offset: 0x000C9F7D
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000CBD93 File Offset: 0x000C9F93
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000CBDA4 File Offset: 0x000C9FA4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.pullSource != null)
				{
					this.pullSource.Dispose();
				}
				if (this.pushSink != null)
				{
					this.pushSink.Dispose();
				}
			}
			this.pullSource = null;
			this.pushSource = null;
			this.pullSink = null;
			this.pushSink = null;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000CBDF8 File Offset: 0x000C9FF8
		private void FastLoop()
		{
			int num = this.numFlags;
			int num2 = this.flags;
			int readCurrent = this.readCurrent;
			byte[] readBuffer = this.readBuffer;
			uint seed = this.crc;
			this.readEnd -= 3;
			this.writeEnd -= 17;
			this.expectedCount += readCurrent - this.readStart;
			int num3;
			int num5;
			for (;;)
			{
				if (num == 0)
				{
					num2 = (int)readBuffer[readCurrent++];
					seed = CRC32.ComputeCRC(seed, (uint)num2);
					num = 8;
				}
				if ((num2 & 1) != 0)
				{
					num3 = (int)readBuffer[readCurrent++];
					seed = CRC32.ComputeCRC(seed, (uint)num3);
					int num4 = (int)readBuffer[readCurrent++];
					seed = CRC32.ComputeCRC(seed, (uint)num4);
					num3 = (num3 << 8 | num4);
					num5 = (num3 & 15) + 2;
					num3 >>= 4;
					if (num3 == (this.windowCurrent & 4095))
					{
						break;
					}
					this.ExpandMatch(num3, num5);
				}
				else
				{
					int num4 = (int)readBuffer[readCurrent++];
					seed = CRC32.ComputeCRC(seed, (uint)num4);
					this.AddRawByte(num4);
				}
				num--;
				num2 >>= 1;
				if (readCurrent > this.readEnd || this.writeCurrent > this.writeEnd)
				{
					goto IL_128;
				}
			}
			this.match = num3;
			this.matchLength = num5;
			this.pointerRead = 2;
			IL_128:
			this.crc = seed;
			this.readCurrent = readCurrent;
			this.readEnd += 3;
			this.writeEnd += 17;
			this.expectedCount -= this.readCurrent - this.readStart;
			this.numFlags = num;
			this.flags = num2;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000CBF84 File Offset: 0x000CA184
		private void SlowLoop()
		{
			this.expectedCount += this.readCurrent - this.readStart;
			while (this.readCurrent != this.readEnd && this.writeCurrent != this.writeEnd)
			{
				if (this.numFlags == 0)
				{
					this.flags = (int)this.readBuffer[this.readCurrent++];
					this.crc = CRC32.ComputeCRC(this.crc, (uint)this.flags);
					this.numFlags = 8;
				}
				if (this.readCurrent != this.readEnd)
				{
					if ((this.flags & 1) != 0)
					{
						if (this.readEnd - this.readCurrent < 2)
						{
							this.match = (int)this.readBuffer[this.readCurrent++];
							this.crc = CRC32.ComputeCRC(this.crc, (uint)this.match);
							this.pointerRead = 1;
							break;
						}
						this.match = (int)this.readBuffer[this.readCurrent++];
						this.crc = CRC32.ComputeCRC(this.crc, (uint)this.match);
						int num = (int)this.readBuffer[this.readCurrent++];
						this.crc = CRC32.ComputeCRC(this.crc, (uint)num);
						this.match = (this.match << 8 | num);
						this.matchLength = (this.match & 15) + 2;
						this.match >>= 4;
						if (this.match == (this.windowCurrent & 4095))
						{
							this.pointerRead = 2;
							break;
						}
						if (!this.ExpandMatchWithOverflow())
						{
							this.pointerRead = 2;
							break;
						}
					}
					else
					{
						int num = (int)this.readBuffer[this.readCurrent++];
						this.crc = CRC32.ComputeCRC(this.crc, (uint)num);
						this.AddRawByte(num);
					}
					this.numFlags--;
					this.flags >>= 1;
				}
			}
			this.expectedCount -= this.readCurrent - this.readStart;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000CC1B4 File Offset: 0x000CA3B4
		private void ExpandMatch(int match, int matchLength)
		{
			if (match < 17 && this.windowCurrent >= 4096 && match < this.windowCurrent % 4096)
			{
				match += 4096;
			}
			byte[] window = this.window;
			int windowCurrent = this.windowCurrent;
			byte[] writeBuffer = this.writeBuffer;
			int writeCurrent = this.writeCurrent;
			int num = matchLength;
			while (num-- != 0)
			{
				byte b = window[match++];
				writeBuffer[writeCurrent++] = b;
				window[windowCurrent++] = b;
			}
			this.windowCurrent = windowCurrent;
			this.writeCurrent = writeCurrent;
			if (windowCurrent > 4096)
			{
				this.ShadowCopy(matchLength);
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000CC250 File Offset: 0x000CA450
		private bool ExpandMatchWithOverflow()
		{
			int num = this.match;
			if (num < 17 && this.windowCurrent >= 4096 && num < this.windowCurrent % 4096)
			{
				num += 4096;
			}
			int num2 = Math.Min(this.matchLength, this.writeEnd - this.writeCurrent);
			this.matchLength -= num2;
			int num3 = num2;
			while (num3-- != 0)
			{
				this.writeBuffer[this.writeCurrent++] = (this.window[this.windowCurrent++] = this.window[num++]);
			}
			if (this.windowCurrent > 4096)
			{
				this.ShadowCopy(num2);
			}
			if (num >= 4096)
			{
				num -= 4096;
			}
			this.match = num;
			return this.matchLength == 0;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000CC338 File Offset: 0x000CA538
		private void ShadowCopy(int matchLength)
		{
			int num = Math.Min(matchLength, this.windowCurrent - 4096);
			int num2 = this.windowCurrent - num;
			int num3 = num2 - 4096;
			while (num-- != 0)
			{
				this.window[num3++] = this.window[num2++];
			}
			if (num3 >= 17)
			{
				this.windowCurrent = num3;
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000CC398 File Offset: 0x000CA598
		private void AddRawByte(int b)
		{
			this.writeBuffer[this.writeCurrent++] = (this.window[this.windowCurrent++] = (byte)b);
			if (this.windowCurrent > 4096)
			{
				this.window[this.windowCurrent - 1 - 4096] = (byte)b;
				if (this.windowCurrent - 4096 >= 17)
				{
					this.windowCurrent -= 4096;
				}
			}
		}

		// Token: 0x04001F03 RID: 7939
		private IResultsFeedback resultFeedback;

		// Token: 0x04001F04 RID: 7940
		private bool disableFastLoop;

		// Token: 0x04001F05 RID: 7941
		private bool uncompressed;

		// Token: 0x04001F06 RID: 7942
		private int expectedCount;

		// Token: 0x04001F07 RID: 7943
		private uint expectedCrc;

		// Token: 0x04001F08 RID: 7944
		private bool headerRead;

		// Token: 0x04001F09 RID: 7945
		private bool logicalEOS;

		// Token: 0x04001F0A RID: 7946
		private uint crc;

		// Token: 0x04001F0B RID: 7947
		private int pointerRead;

		// Token: 0x04001F0C RID: 7948
		private int match;

		// Token: 0x04001F0D RID: 7949
		private int matchLength;

		// Token: 0x04001F0E RID: 7950
		private int numFlags;

		// Token: 0x04001F0F RID: 7951
		private int flags;

		// Token: 0x04001F10 RID: 7952
		private byte[] headerBuffer;

		// Token: 0x04001F11 RID: 7953
		private int headerBufferCurrent;
	}
}
