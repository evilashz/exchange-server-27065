using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000D3 RID: 211
	internal class DirectoryReader : IDisposable
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x0002DF54 File Offset: 0x0002C154
		public DirectoryReader(Stream inputStream, Encoding outerCharsetEncoding, ComplianceTracker complianceTracker)
		{
			this.inputStream = new UnfoldingStream(inputStream);
			this.outerCharsetEncoding = outerCharsetEncoding;
			this.currentCharsetEncoding = outerCharsetEncoding;
			this.currentCharsetDecoder = outerCharsetEncoding.GetDecoder();
			this.currentCharsetEncoder = outerCharsetEncoding.GetEncoder();
			this.dataBytes = new byte[256];
			this.dataChars = new char[256];
			this.complianceTracker = complianceTracker;
			this.SetFallback();
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002DFD9 File Offset: 0x0002C1D9
		public Encoding CurrentCharsetEncoding
		{
			get
			{
				this.CheckDisposed("CurrentEncoding::get");
				return this.currentCharsetEncoding;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002DFEC File Offset: 0x0002C1EC
		public bool ReadChar(out char result, out bool newLine)
		{
			this.CheckDisposed("ReadChar");
			result = '?';
			newLine = false;
			char? c = this.lastChar;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
			if (num != null)
			{
				result = this.lastChar.Value;
				this.lastChar = null;
			}
			else if (!this.ReadChar(out result))
			{
				return false;
			}
			if (!this.isDecoding)
			{
				if (result != '\r')
				{
					return true;
				}
				if (this.ReadChar(out result))
				{
					if (result == '\n')
					{
						newLine = true;
						return true;
					}
					this.lastChar = new char?(result);
				}
				result = '\r';
			}
			return true;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002E098 File Offset: 0x0002C298
		public void SwitchCharsetEncoding(Encoding newCharsetEncoding)
		{
			this.CheckDisposed("SwitchEncoding");
			char? c = this.lastChar;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
			if (num != null)
			{
				throw new InvalidOperationException();
			}
			if (newCharsetEncoding.WebName == this.CurrentCharsetEncoding.WebName)
			{
				return;
			}
			this.bottomByte += this.currentCharsetEncoder.GetByteCount(this.dataChars, 0, this.idxChar, true);
			if (this.bottomByte > this.topByte)
			{
				this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInPropertyValue, CalendarStrings.InvalidCharacterInPropertyValue);
				this.bottomByte = this.topByte;
			}
			this.idxByte = this.bottomByte;
			this.idxChar = 0;
			this.topChar = 0;
			this.currentCharsetEncoding = newCharsetEncoding;
			this.currentCharsetEncoder = newCharsetEncoding.GetEncoder();
			this.currentCharsetDecoder = newCharsetEncoding.GetDecoder();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002E18B File Offset: 0x0002C38B
		public void RestoreCharsetEncoding()
		{
			this.CheckDisposed("RestoreEncoding");
			this.SwitchCharsetEncoding(this.outerCharsetEncoding);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
		public void ApplyValueDecoder(ByteEncoder decoder)
		{
			this.CheckDisposed("ApplyValueDecoder");
			char? c = this.lastChar;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
			if (num != null)
			{
				throw new InvalidOperationException();
			}
			if (this.decoderStream != null)
			{
				throw new InvalidOperationException();
			}
			this.decoderStream = new EncoderStream(this.GetValueReadStream(null), decoder, EncoderStreamAccess.Read);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002E218 File Offset: 0x0002C418
		public Stream GetValueReadStream(DirectoryReader.OnValueEndFunc callback)
		{
			this.CheckDisposed("GetValueReadStream");
			char? c = this.lastChar;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
			if (num != null)
			{
				throw new InvalidOperationException();
			}
			this.bottomByte += this.currentCharsetEncoder.GetByteCount(this.dataChars, 0, this.idxChar, true);
			if (this.bottomByte > this.topByte)
			{
				this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInPropertyValue, CalendarStrings.InvalidCharacterInPropertyValue);
				this.bottomByte = this.topByte;
			}
			this.idxByte = this.bottomByte;
			this.idxChar = 0;
			this.topChar = 0;
			this.inputStream.Rewind(this.topByte - this.idxByte);
			this.topByte = 0;
			this.idxByte = 0;
			this.bottomByte = 0;
			return new DirectoryReader.AdapterStream(this.inputStream, callback);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002E30C File Offset: 0x0002C50C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002E31B File Offset: 0x0002C51B
		protected virtual void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("DirectoryReader", methodName);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002E334 File Offset: 0x0002C534
		private bool ReadChar(out char result)
		{
			result = '?';
			if (this.idxChar >= this.topChar)
			{
				this.idxChar = 0;
				this.topChar = 0;
				int num = 0;
				int num2 = 0;
				bool flag = false;
				if (this.idxByte < this.topByte)
				{
					this.currentCharsetDecoder.Convert(this.dataBytes, this.idxByte, this.topByte - this.idxByte, this.dataChars, 0, this.dataChars.Length, false, out num, out num2, out flag);
					this.topChar = num2;
					this.idxByte += num;
				}
				while (this.topChar == 0)
				{
					for (int i = 0; i < this.topByte - this.idxByte; i++)
					{
						this.dataBytes[i] = this.dataBytes[this.idxByte + i];
					}
					this.topByte -= this.idxByte;
					this.bottomByte = 0;
					this.idxByte = 0;
					int num3 = this.ReadInputStream(this.dataBytes, this.topByte, this.dataBytes.Length - this.topByte);
					this.topByte += num3;
					this.currentCharsetDecoder.Convert(this.dataBytes, 0, this.topByte, this.dataChars, 0, this.dataChars.Length, num3 == 0, out num, out num2, out flag);
					this.topChar = num2;
					this.idxByte += num;
					if (num3 == 0 && this.topChar == 0)
					{
						return false;
					}
				}
			}
			result = this.dataChars[this.idxChar++];
			if (this.swallowUTFByteOrderMark && (result == '￾' || result == '﻿'))
			{
				this.swallowUTFByteOrderMark = false;
				return this.ReadChar(out result);
			}
			this.swallowUTFByteOrderMark = false;
			return true;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002E500 File Offset: 0x0002C700
		private int ReadInputStream(byte[] buffer, int offset, int count)
		{
			this.isDecoding = false;
			if (this.decoderStream == null)
			{
				return this.inputStream.Read(buffer, offset, count);
			}
			int num = this.decoderStream.Read(buffer, offset, count);
			if (num > 0)
			{
				this.isDecoding = true;
				return num;
			}
			this.decoderStream.Dispose();
			this.decoderStream = null;
			buffer[offset] = 13;
			buffer[offset + 1] = 10;
			return 2;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002E568 File Offset: 0x0002C768
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					if (this.decoderStream != null)
					{
						this.decoderStream.Dispose();
						this.decoderStream = null;
					}
					if (this.inputStream != null)
					{
						this.inputStream.Dispose();
						this.inputStream = null;
					}
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002E5BB File Offset: 0x0002C7BB
		private void SetFallback()
		{
			this.currentCharsetDecoder.Fallback = new DecoderReplacementFallback("?");
			this.currentCharsetEncoder.Fallback = new EncoderReplacementFallback("?");
		}

		// Token: 0x04000713 RID: 1811
		private const int BufferSize = 256;

		// Token: 0x04000714 RID: 1812
		private const char ByteOrderMark1 = '￾';

		// Token: 0x04000715 RID: 1813
		private const char ByteOrderMark2 = '﻿';

		// Token: 0x04000716 RID: 1814
		private UnfoldingStream inputStream;

		// Token: 0x04000717 RID: 1815
		private Stream decoderStream;

		// Token: 0x04000718 RID: 1816
		private Encoding outerCharsetEncoding;

		// Token: 0x04000719 RID: 1817
		private Encoding currentCharsetEncoding;

		// Token: 0x0400071A RID: 1818
		private Encoder currentCharsetEncoder;

		// Token: 0x0400071B RID: 1819
		private Decoder currentCharsetDecoder;

		// Token: 0x0400071C RID: 1820
		private byte[] dataBytes;

		// Token: 0x0400071D RID: 1821
		private char[] dataChars;

		// Token: 0x0400071E RID: 1822
		private int bottomByte;

		// Token: 0x0400071F RID: 1823
		private int topByte;

		// Token: 0x04000720 RID: 1824
		private int idxByte;

		// Token: 0x04000721 RID: 1825
		private int topChar;

		// Token: 0x04000722 RID: 1826
		private int idxChar;

		// Token: 0x04000723 RID: 1827
		private bool isDisposed;

		// Token: 0x04000724 RID: 1828
		private bool swallowUTFByteOrderMark = true;

		// Token: 0x04000725 RID: 1829
		private bool isDecoding;

		// Token: 0x04000726 RID: 1830
		private char? lastChar = null;

		// Token: 0x04000727 RID: 1831
		private ComplianceTracker complianceTracker;

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x0600085B RID: 2139
		public delegate void OnValueEndFunc();

		// Token: 0x020000D5 RID: 213
		private class AdapterStream : Stream
		{
			// Token: 0x0600085E RID: 2142 RVA: 0x0002E5E7 File Offset: 0x0002C7E7
			public AdapterStream(UnfoldingStream inputStream, DirectoryReader.OnValueEndFunc callback)
			{
				this.inputStream = inputStream;
				this.callback = callback;
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x0600085F RID: 2143 RVA: 0x0002E614 File Offset: 0x0002C814
			public override bool CanRead
			{
				get
				{
					this.CheckDisposed("CanRead:get");
					return true;
				}
			}

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06000860 RID: 2144 RVA: 0x0002E622 File Offset: 0x0002C822
			public override bool CanWrite
			{
				get
				{
					this.CheckDisposed("CanWrite:get");
					return false;
				}
			}

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06000861 RID: 2145 RVA: 0x0002E630 File Offset: 0x0002C830
			public override bool CanSeek
			{
				get
				{
					this.CheckDisposed("CanSeek:get");
					return false;
				}
			}

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000862 RID: 2146 RVA: 0x0002E63E File Offset: 0x0002C83E
			public override long Length
			{
				get
				{
					this.CheckDisposed("Length:Get");
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002E650 File Offset: 0x0002C850
			// (set) Token: 0x06000864 RID: 2148 RVA: 0x0002E664 File Offset: 0x0002C864
			public override long Position
			{
				get
				{
					this.CheckDisposed("Position:get");
					return (long)this.position;
				}
				set
				{
					this.CheckDisposed("Position:set");
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x0002E678 File Offset: 0x0002C878
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.isClosed)
				{
					byte[] array = new byte[1024];
					while (this.Read(array, 0, array.Length) > 0)
					{
					}
				}
				this.isClosed = true;
				base.Dispose(disposing);
			}

			// Token: 0x06000866 RID: 2150 RVA: 0x0002E6B7 File Offset: 0x0002C8B7
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CheckDisposed("Write");
				throw new NotSupportedException();
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x0002E6CC File Offset: 0x0002C8CC
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.InternalRead(buffer, offset, count);
				if (num == 0 && this.callback != null)
				{
					this.callback();
					this.callback = null;
				}
				return num;
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x0002E701 File Offset: 0x0002C901
			public override void SetLength(long value)
			{
				this.CheckDisposed("SetLength");
				throw new NotSupportedException();
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x0002E713 File Offset: 0x0002C913
			public override long Seek(long offset, SeekOrigin origin)
			{
				this.CheckDisposed("Seek");
				throw new NotSupportedException();
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x0002E725 File Offset: 0x0002C925
			public override void Flush()
			{
				this.CheckDisposed("Flush");
				throw new NotSupportedException();
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x0002E738 File Offset: 0x0002C938
			private int InternalRead(byte[] buffer, int offset, int count)
			{
				this.CheckDisposed("Read");
				if (this.endIdx >= 0)
				{
					int num = Math.Min(count, this.endIdx - this.idx1);
					Array.Copy(this.tempBuffer, this.idx1, buffer, offset, num);
					this.idx1 += num;
					this.position += num;
					return num;
				}
				if (this.idx1 != 0)
				{
					for (int i = 0; i < this.idx2 - this.idx1; i++)
					{
						this.tempBuffer[i] = this.tempBuffer[this.idx1 + i];
					}
					this.idx2 -= this.idx1;
					this.idx1 = 0;
				}
				int num2 = this.inputStream.Read(this.tempBuffer, this.idx2, this.tempBuffer.Length - this.idx2);
				this.idx2 += num2;
				if (num2 == 0)
				{
					this.endIdx = ((this.idx2 >= 2 && this.tempBuffer[this.idx2 - 2] == 13 && this.tempBuffer[this.idx2 - 1] == 10) ? (this.idx2 - 2) : this.idx2);
					return this.InternalRead(buffer, offset, count);
				}
				while (this.idx1 < count && this.idx1 < this.idx2 - 1)
				{
					if (this.tempBuffer[this.idx1] == 13 && this.tempBuffer[this.idx1 + 1] == 10)
					{
						this.endIdx = this.idx1;
						this.inputStream.Rewind(this.idx2 - (this.idx1 + 2));
						break;
					}
					buffer[this.idx1] = this.tempBuffer[this.idx1];
					this.idx1++;
					this.position++;
				}
				if (this.idx1 != 0)
				{
					return this.idx1;
				}
				return this.InternalRead(buffer, offset, count);
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x0002E92B File Offset: 0x0002CB2B
			private void CheckDisposed(string methodName)
			{
				if (this.isClosed)
				{
					throw new ObjectDisposedException("AdapterStream", methodName);
				}
			}

			// Token: 0x04000728 RID: 1832
			private bool isClosed;

			// Token: 0x04000729 RID: 1833
			private UnfoldingStream inputStream;

			// Token: 0x0400072A RID: 1834
			private byte[] tempBuffer = new byte[256];

			// Token: 0x0400072B RID: 1835
			private int idx1;

			// Token: 0x0400072C RID: 1836
			private int idx2;

			// Token: 0x0400072D RID: 1837
			private DirectoryReader.OnValueEndFunc callback;

			// Token: 0x0400072E RID: 1838
			private int position;

			// Token: 0x0400072F RID: 1839
			private int endIdx = -1;
		}
	}
}
