using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x0200024E RID: 590
	internal class RtfOutput : IRestartable, IByteSource, IDisposable
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x000C0488 File Offset: 0x000BE688
		public RtfOutput(Stream destination, bool push, bool restartable)
		{
			if (!push)
			{
				this.pullSink = (destination as ConverterStream);
				this.pullSink.SetSource(this);
			}
			else
			{
				this.pushSink = destination;
				if (restartable && destination.CanSeek && destination.Position == destination.Length)
				{
					this.restartablePushSink = true;
					this.restartPosition = destination.Position;
				}
			}
			this.restartable = restartable;
			this.textBuffer = new char[512];
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000C0518 File Offset: 0x000BE718
		bool IRestartable.CanRestart()
		{
			return this.restartable;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000C0520 File Offset: 0x000BE720
		void IRestartable.Restart()
		{
			this.endOfFile = false;
			this.cache.Reset();
			this.textEnd = 0;
			this.textType = RtfOutput.TextType.Control;
			this.lastKeyword = false;
			if (this.pullSink == null && this.restartablePushSink)
			{
				this.pushSink.Position = this.restartPosition;
				this.pushSink.SetLength(this.restartPosition);
			}
			this.restartable = false;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000C058D File Offset: 0x000BE78D
		void IRestartable.DisableRestart()
		{
			this.restartable = false;
			this.FlushCached();
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000C059D File Offset: 0x000BE79D
		public void SetEncoding(Encoding encoding)
		{
			this.codePageMap.ChoseCodePage(encoding);
			this.encoder = encoding.GetEncoder();
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000C05B8 File Offset: 0x000BE7B8
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x000C05C0 File Offset: 0x000BE7C0
		public int RtfLineLength
		{
			get
			{
				return this.rtfLineLength;
			}
			set
			{
				this.rtfLineLength = value;
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000C05CC File Offset: 0x000BE7CC
		public void Flush()
		{
			if (this.endOfFile)
			{
				return;
			}
			this.restartable = false;
			this.FlushTextBuffer(true);
			this.CommitOutput();
			this.FlushCached();
			if (this.pullSink == null)
			{
				this.pushSink.Flush();
			}
			else if (this.cache.Length == 0)
			{
				this.pullSink.ReportEndOfFile();
			}
			this.endOfFile = true;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000C0634 File Offset: 0x000BE834
		private bool FlushCached()
		{
			if (this.restartable || this.cache.Length == 0)
			{
				return false;
			}
			if (this.pullSink == null)
			{
				while (this.cache.Length != 0)
				{
					byte[] buffer;
					int offset;
					int num;
					this.cache.GetData(out buffer, out offset, out num);
					this.pushSink.Write(buffer, offset, num);
					this.cache.ReportRead(num);
				}
			}
			else
			{
				byte[] buffer;
				int offset;
				int count;
				this.pullSink.GetOutputBuffer(out buffer, out offset, out count);
				int num = this.cache.Read(buffer, offset, count);
				this.pullSink.ReportOutput(num);
			}
			return true;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x000C06C9 File Offset: 0x000BE8C9
		public bool CanAcceptMoreOutput
		{
			get
			{
				return this.restartable || this.pullSink == null || this.cache.Length == 0;
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000C06EC File Offset: 0x000BE8EC
		public void WriteKeyword(string keyword, int value)
		{
			if (this.textType != RtfOutput.TextType.Control)
			{
				this.FlushTextBuffer(true);
				this.textType = RtfOutput.TextType.Control;
			}
			int num = 0;
			int num2 = value;
			do
			{
				num++;
			}
			while ((num2 /= 10) != 0);
			if (this.outputEnd - this.outputCurrent < keyword.Length + num + 1 + 2)
			{
				this.CommitOutput();
				this.GetOutputBuffer(keyword.Length + num + 1 + 2);
			}
			for (int i = 0; i < keyword.Length; i++)
			{
				this.outputBuffer[this.outputCurrent++] = (byte)keyword[i];
			}
			if (value < 0)
			{
				this.outputBuffer[this.outputCurrent++] = 45;
				value = -value;
			}
			int num3 = this.outputCurrent += num;
			do
			{
				this.outputBuffer[--num3] = (byte)(value % 10 + 48);
			}
			while ((value /= 10) != 0);
			this.rtfLineLength += keyword.Length + num + 1;
			if (this.rtfLineLength > 128)
			{
				this.outputBuffer[this.outputCurrent++] = 13;
				this.outputBuffer[this.outputCurrent++] = 10;
				this.rtfLineLength = 0;
				this.lastKeyword = false;
				return;
			}
			this.lastKeyword = true;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000C084C File Offset: 0x000BEA4C
		public void WriteControlText(string controlText, bool lastKeyword)
		{
			if (this.textType != RtfOutput.TextType.Control)
			{
				this.FlushTextBuffer(true);
				this.textType = RtfOutput.TextType.Control;
			}
			int i = 0;
			if (this.outputEnd - this.outputCurrent < controlText.Length)
			{
				while (this.outputCurrent != this.outputEnd)
				{
					this.outputBuffer[this.outputCurrent++] = (byte)controlText[i];
					i++;
				}
				this.CommitOutput();
				this.GetOutputBuffer(controlText.Length - i);
			}
			while (i < controlText.Length)
			{
				this.outputBuffer[this.outputCurrent++] = (byte)controlText[i];
				i++;
			}
			this.rtfLineLength += controlText.Length;
			if (this.rtfLineLength > 128 && controlText[controlText.Length - 1] != '\n' && this.outputEnd - this.outputCurrent >= 2 && lastKeyword)
			{
				this.outputBuffer[this.outputCurrent++] = 13;
				this.outputBuffer[this.outputCurrent++] = 10;
				this.rtfLineLength = 0;
				lastKeyword = false;
			}
			this.lastKeyword = lastKeyword;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000C098C File Offset: 0x000BEB8C
		public void WriteBinary(byte[] buffer, int offset, int count)
		{
			if (count != 0)
			{
				if (this.textType != RtfOutput.TextType.Control)
				{
					this.FlushTextBuffer(true);
					this.textType = RtfOutput.TextType.Control;
				}
				byte b = buffer[offset + count - 1];
				this.rtfLineLength = ((b == 10 || b == 13) ? 0 : (this.rtfLineLength + count));
				if (this.outputEnd - this.outputCurrent < count)
				{
					int num = this.outputEnd - this.outputCurrent;
					if (num != 0)
					{
						Buffer.BlockCopy(buffer, offset, this.outputBuffer, this.outputCurrent, num);
						this.outputCurrent += num;
						offset += num;
						count -= num;
					}
					this.CommitOutput();
					this.GetOutputBuffer(count);
				}
				Buffer.BlockCopy(buffer, offset, this.outputBuffer, this.outputCurrent, count);
				this.outputCurrent += count;
				this.lastKeyword = false;
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000C0A5B File Offset: 0x000BEC5B
		public void WriteText(string buffer)
		{
			this.WriteText(buffer, 0, buffer.Length);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000C0A6C File Offset: 0x000BEC6C
		public void WriteText(string buffer, int offset, int count)
		{
			if (this.textType != RtfOutput.TextType.Text || this.textEnd > this.textBuffer.Length - count)
			{
				if (this.textType == RtfOutput.TextType.Control)
				{
					if (this.lastKeyword)
					{
						this.WriteControlText(" ", false);
					}
				}
				else
				{
					this.FlushTextBuffer(this.textType != RtfOutput.TextType.Text);
				}
				this.textType = RtfOutput.TextType.Text;
			}
			this.rtfLineLength += count;
			while (count != 0)
			{
				int num = Math.Min(count, this.textBuffer.Length - this.textEnd);
				buffer.CopyTo(offset, this.textBuffer, this.textEnd, num);
				offset += num;
				count -= num;
				this.textEnd += num;
				if (count == 0)
				{
					return;
				}
				this.FlushTextBuffer(false);
			}
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000C0B2C File Offset: 0x000BED2C
		public void WriteText(char[] buffer, int offset, int count)
		{
			if (this.textType != RtfOutput.TextType.Text || this.textEnd > this.textBuffer.Length - count)
			{
				if (this.textType == RtfOutput.TextType.Control)
				{
					if (this.lastKeyword)
					{
						this.WriteControlText(" ", false);
					}
				}
				else
				{
					this.FlushTextBuffer(this.textType != RtfOutput.TextType.Text);
				}
				this.textType = RtfOutput.TextType.Text;
			}
			this.rtfLineLength += count;
			if (count <= 64)
			{
				Buffer.BlockCopy(buffer, offset * 2, this.textBuffer, this.textEnd * 2, count * 2);
				this.textEnd += count;
				return;
			}
			if (this.textEnd != 0)
			{
				this.FlushTextBuffer(false);
				if (this.textEnd != 0)
				{
					this.textBuffer[this.textEnd++] = buffer[offset++];
					count--;
					this.FlushTextBuffer(false);
				}
			}
			int num = this.EncodeText(buffer, offset, count, false);
			if (num != count)
			{
				this.textBuffer[0] = this.textBuffer[offset + num];
				this.textEnd = 1;
				return;
			}
			this.textEnd = 0;
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000C0C3C File Offset: 0x000BEE3C
		public void WriteEncapsulatedMarkupText(char[] buffer, int offset, int count)
		{
			if (this.textType != RtfOutput.TextType.MarkupText || this.textEnd > this.textBuffer.Length - count)
			{
				if (this.textType == RtfOutput.TextType.Control)
				{
					if (this.lastKeyword)
					{
						this.WriteControlText(" ", false);
					}
				}
				else
				{
					this.FlushTextBuffer(this.textType != RtfOutput.TextType.MarkupText);
				}
				this.textType = RtfOutput.TextType.MarkupText;
			}
			this.rtfLineLength += count;
			if (count <= 64)
			{
				Buffer.BlockCopy(buffer, offset * 2, this.textBuffer, this.textEnd * 2, count * 2);
				this.textEnd += count;
				return;
			}
			if (this.textEnd != 0)
			{
				this.FlushTextBuffer(false);
				if (this.textEnd != 0)
				{
					this.textBuffer[this.textEnd++] = buffer[offset++];
					count--;
					this.FlushTextBuffer(false);
				}
			}
			int num = this.EncodeText(buffer, offset, count, false);
			if (num != count)
			{
				this.textBuffer[0] = this.textBuffer[offset + num];
				this.textEnd = 1;
				return;
			}
			this.textEnd = 0;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000C0D4A File Offset: 0x000BEF4A
		public void WriteDoubleEscapedText(string buffer)
		{
			this.WriteDoubleEscapedText(buffer, 0, buffer.Length);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000C0D5C File Offset: 0x000BEF5C
		public void WriteDoubleEscapedText(string buffer, int offset, int count)
		{
			if (this.textType != RtfOutput.TextType.DoubleEscapedText || this.textEnd > this.textBuffer.Length - count)
			{
				if (this.textType == RtfOutput.TextType.Control)
				{
					if (this.lastKeyword)
					{
						this.WriteControlText(" ", false);
					}
				}
				else
				{
					this.FlushTextBuffer(this.textType != RtfOutput.TextType.Text);
				}
				this.textType = RtfOutput.TextType.DoubleEscapedText;
			}
			this.rtfLineLength += count;
			for (;;)
			{
				int num = Math.Min(count, this.textBuffer.Length - this.textEnd);
				buffer.CopyTo(offset, this.textBuffer, this.textEnd, num);
				offset += num;
				count -= num;
				this.textEnd += num;
				if (count == 0)
				{
					break;
				}
				this.FlushTextBuffer(false);
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000C0E18 File Offset: 0x000BF018
		private void FlushTextBuffer(bool flushEncoder)
		{
			if (this.textEnd != 0 || (flushEncoder && !this.encoderFlushed))
			{
				int num = this.EncodeText(this.textBuffer, 0, this.textEnd, flushEncoder);
				if (num != this.textEnd)
				{
					this.textBuffer[0] = this.textBuffer[num];
					this.textEnd = 1;
					return;
				}
				this.textEnd = 0;
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000C0E78 File Offset: 0x000BF078
		private int EncodeText(char[] buffer, int offset, int count, bool flushEncoder)
		{
			int num = offset;
			int num2 = offset + count;
			while (num != num2)
			{
				if (this.outputCurrent == this.outputEnd)
				{
					this.CommitOutput();
					this.GetOutputBuffer(16);
				}
				int num3 = num;
				char c = buffer[num3];
				if ((int)c < RtfSupport.UnsafeAsciiMap.Length)
				{
					if (RtfSupport.UnsafeAsciiMap[(int)c] != 0)
					{
						if (this.outputEnd - this.outputCurrent < 6)
						{
							this.CommitOutput();
							this.GetOutputBuffer(16);
						}
						if (c == '\\')
						{
							if (this.textType == RtfOutput.TextType.DoubleEscapedText)
							{
								this.outputBuffer[this.outputCurrent++] = 92;
								this.outputBuffer[this.outputCurrent++] = 92;
							}
							this.outputBuffer[this.outputCurrent++] = 92;
							this.outputBuffer[this.outputCurrent++] = 92;
						}
						else if (c == '{' || c == '}')
						{
							if (this.textType == RtfOutput.TextType.DoubleEscapedText)
							{
								this.outputBuffer[this.outputCurrent++] = 92;
							}
							this.outputBuffer[this.outputCurrent++] = 92;
							this.outputBuffer[this.outputCurrent++] = (byte)c;
						}
						else if (c == '"')
						{
							if (this.textType == RtfOutput.TextType.DoubleEscapedText)
							{
								this.outputBuffer[this.outputCurrent++] = 92;
								this.outputBuffer[this.outputCurrent++] = 92;
								this.outputBuffer[this.outputCurrent++] = 34;
							}
							else
							{
								this.outputBuffer[this.outputCurrent++] = 34;
							}
						}
						else if (c == '\u00a0')
						{
							this.outputBuffer[this.outputCurrent++] = 32;
						}
						else if (this.textType == RtfOutput.TextType.MarkupText && (c == '\r' || c == '\n'))
						{
							this.outputBuffer[this.outputCurrent++] = 92;
							this.outputBuffer[this.outputCurrent++] = 112;
							this.outputBuffer[this.outputCurrent++] = 97;
							this.outputBuffer[this.outputCurrent++] = 114;
							this.outputBuffer[this.outputCurrent++] = 13;
							this.outputBuffer[this.outputCurrent++] = 10;
							if (c == '\r' && num3 < num2 - 1 && buffer[num3 + 1] == '\n')
							{
								num3++;
							}
						}
						else
						{
							this.outputBuffer[this.outputCurrent++] = 92;
							this.outputBuffer[this.outputCurrent++] = 39;
							RtfSupport.Escape(c, this.outputBuffer, this.outputCurrent);
							this.outputCurrent += 2;
						}
						num3++;
					}
					else
					{
						this.outputBuffer[this.outputCurrent++] = (byte)c;
						num3++;
						while (num3 != num2 && this.outputCurrent != this.outputEnd && (int)(c = buffer[num3]) < RtfSupport.UnsafeAsciiMap.Length)
						{
							if (RtfSupport.UnsafeAsciiMap[(int)c] != 0)
							{
								break;
							}
							this.outputBuffer[this.outputCurrent++] = (byte)c;
							num3++;
						}
					}
				}
				else if (!this.codePageMap.IsUnsafeExtendedCharacter(c))
				{
					if (this.outputEnd - this.outputCurrent < 16)
					{
						this.CommitOutput();
						this.GetOutputBuffer(16);
					}
					do
					{
						num3++;
					}
					while (num3 != num2 && (int)(c = buffer[num3]) >= RtfSupport.UnsafeAsciiMap.Length && !this.codePageMap.IsUnsafeExtendedCharacter(c));
					if (this.encodeBuffer == null)
					{
						this.encodeBuffer = new byte[512];
					}
					bool flag = num3 != num2 || flushEncoder;
					for (;;)
					{
						int num4;
						int num5;
						bool flag2;
						this.encoder.Convert(buffer, num, num3 - num, this.encodeBuffer, 0, this.encodeBuffer.Length, flag, out num4, out num5, out flag2);
						this.encoderFlushed = (flag && flag2);
						for (int i = 0; i < num5; i++)
						{
							if (this.outputEnd - this.outputCurrent < 4)
							{
								this.CommitOutput();
								this.GetOutputBuffer(16);
							}
							this.outputBuffer[this.outputCurrent++] = 92;
							this.outputBuffer[this.outputCurrent++] = 39;
							RtfSupport.Escape((char)this.encodeBuffer[i], this.outputBuffer, this.outputCurrent);
							this.outputCurrent += 2;
						}
						num += num4;
						if (num == num3)
						{
							if (!flag)
							{
								break;
							}
							if (flag2)
							{
								break;
							}
						}
					}
				}
				else
				{
					if (this.outputEnd - this.outputCurrent < 8)
					{
						this.CommitOutput();
						this.GetOutputBuffer(16);
					}
					int num6 = (int)c;
					int num7 = (num6 < 10) ? 1 : ((num6 < 100) ? 2 : ((num6 < 1000) ? 3 : ((num6 < 10000) ? 4 : 5)));
					this.outputBuffer[this.outputCurrent++] = 92;
					this.outputBuffer[this.outputCurrent++] = 117;
					int num8 = this.outputCurrent + num7;
					while (num6 != 0)
					{
						int num9 = num6 % 10;
						this.outputBuffer[--num8] = (byte)(num9 + 48);
						num6 /= 10;
					}
					this.outputCurrent += num7;
					this.outputBuffer[this.outputCurrent++] = 63;
					num3++;
				}
				num = num3;
			}
			return count;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000C1494 File Offset: 0x000BF694
		private void GetOutputBuffer(int minLength)
		{
			if (this.restartable || this.pullSink == null || this.cache.Length != 0)
			{
				this.cache.GetBuffer(minLength, out this.outputBuffer, out this.outputStart);
				this.outputEnd = this.outputBuffer.Length;
				this.outputToCache = true;
			}
			else
			{
				int num;
				this.pullSink.GetOutputBuffer(out this.outputBuffer, out this.outputStart, out num);
				this.outputEnd = this.outputStart + num;
				if (num < minLength)
				{
					this.cache.GetBuffer(minLength, out this.outputBuffer, out this.outputStart);
					this.outputEnd = this.outputBuffer.Length;
					this.outputToCache = true;
				}
				else
				{
					this.outputToCache = false;
				}
			}
			this.outputCurrent = this.outputStart;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000C155C File Offset: 0x000BF75C
		private void CommitOutput()
		{
			if (this.outputCurrent != this.outputStart)
			{
				if (this.outputToCache)
				{
					int length = this.cache.Length;
					this.cache.Commit(this.outputCurrent - this.outputStart);
					if (length == 0)
					{
						if (this.pullSink == null)
						{
							if (this.restartable)
							{
								if (!this.restartablePushSink)
								{
									goto IL_EE;
								}
							}
							while (this.cache.Length != 0)
							{
								byte[] buffer;
								int offset;
								int num;
								this.cache.GetData(out buffer, out offset, out num);
								this.pushSink.Write(buffer, offset, num);
								this.cache.ReportRead(num);
							}
						}
						else if (!this.restartable)
						{
							byte[] buffer;
							int offset;
							int num;
							this.pullSink.GetOutputBuffer(out buffer, out offset, out num);
							if (num != 0)
							{
								num = this.cache.Read(buffer, offset, num);
								this.pullSink.ReportOutput(num);
							}
						}
					}
				}
				else
				{
					this.pullSink.ReportOutput(this.outputCurrent - this.outputStart);
				}
				IL_EE:
				this.outputStart = this.outputCurrent;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000C1664 File Offset: 0x000BF864
		bool IByteSource.GetOutputChunk(out byte[] chunkBuffer, out int chunkOffset, out int chunkCount)
		{
			if (this.outputCurrent != this.outputStart)
			{
				this.CommitOutput();
			}
			if (this.cache.Length == 0 || this.restartable)
			{
				chunkBuffer = null;
				chunkOffset = 0;
				chunkCount = 0;
				return false;
			}
			this.cache.GetData(out chunkBuffer, out chunkOffset, out chunkCount);
			return true;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000C16B4 File Offset: 0x000BF8B4
		void IByteSource.ReportOutput(int readCount)
		{
			this.cache.ReportRead(readCount);
			if (this.cache.Length == 0)
			{
				this.outputToCache = false;
				this.outputStart = (this.outputCurrent = this.outputEnd);
				if (this.endOfFile)
				{
					this.pullSink.ReportEndOfFile();
				}
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000C170C File Offset: 0x000BF90C
		void IDisposable.Dispose()
		{
			if (this.cache != null && this.cache is IDisposable)
			{
				((IDisposable)this.cache).Dispose();
			}
			this.cache = null;
			this.pushSink = null;
			this.pullSink = null;
			this.textBuffer = null;
			this.encodeBuffer = null;
			this.encoder = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001B87 RID: 7047
		private Stream pushSink;

		// Token: 0x04001B88 RID: 7048
		private ConverterStream pullSink;

		// Token: 0x04001B89 RID: 7049
		private bool endOfFile;

		// Token: 0x04001B8A RID: 7050
		private bool restartable;

		// Token: 0x04001B8B RID: 7051
		private bool restartablePushSink;

		// Token: 0x04001B8C RID: 7052
		private long restartPosition;

		// Token: 0x04001B8D RID: 7053
		private byte[] outputBuffer;

		// Token: 0x04001B8E RID: 7054
		private int outputStart;

		// Token: 0x04001B8F RID: 7055
		private int outputCurrent;

		// Token: 0x04001B90 RID: 7056
		private int outputEnd;

		// Token: 0x04001B91 RID: 7057
		private bool outputToCache;

		// Token: 0x04001B92 RID: 7058
		private Encoder encoder;

		// Token: 0x04001B93 RID: 7059
		private bool encoderFlushed;

		// Token: 0x04001B94 RID: 7060
		private CodePageMap codePageMap = new CodePageMap();

		// Token: 0x04001B95 RID: 7061
		private ByteCache cache = new ByteCache();

		// Token: 0x04001B96 RID: 7062
		private char[] textBuffer;

		// Token: 0x04001B97 RID: 7063
		private int textEnd;

		// Token: 0x04001B98 RID: 7064
		private RtfOutput.TextType textType;

		// Token: 0x04001B99 RID: 7065
		private bool lastKeyword;

		// Token: 0x04001B9A RID: 7066
		private byte[] encodeBuffer;

		// Token: 0x04001B9B RID: 7067
		private int rtfLineLength;

		// Token: 0x0200024F RID: 591
		private enum TextType
		{
			// Token: 0x04001B9D RID: 7069
			Control,
			// Token: 0x04001B9E RID: 7070
			Text,
			// Token: 0x04001B9F RID: 7071
			DoubleEscapedText,
			// Token: 0x04001BA0 RID: 7072
			MarkupText
		}
	}
}
