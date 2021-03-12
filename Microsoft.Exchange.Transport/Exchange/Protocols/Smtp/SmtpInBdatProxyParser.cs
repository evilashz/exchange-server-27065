using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000412 RID: 1042
	internal class SmtpInBdatProxyParser : SmtpInParser, IDisposable
	{
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000BF4D4 File Offset: 0x000BD6D4
		public SmtpInBdatProxyParser(MimeDocument headersDocument)
		{
			if (headersDocument == null)
			{
				throw new ArgumentNullException("headersDocument");
			}
			this.headersDocument = headersDocument;
			this.headersDocument.DangerousSetFixBadMimeBoundary(false);
			if (this.headersDocument.RootPart == null)
			{
				this.headersDocument.RootPart = new MimePart();
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000BF549 File Offset: 0x000BD749
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x000BF551 File Offset: 0x000BD751
		public SmtpInDataProxyParser.EndOfHeadersCallback EndOfHeaders
		{
			get
			{
				return this.endOfHeaders;
			}
			set
			{
				this.endOfHeaders = value;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000BF55A File Offset: 0x000BD75A
		public HeaderList ParsedHeaders
		{
			get
			{
				return this.parsedHeaders;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000BF562 File Offset: 0x000BD762
		public ParserState State
		{
			get
			{
				return this.parserState;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000BF56A File Offset: 0x000BD76A
		// (set) Token: 0x06002FCE RID: 12238 RVA: 0x000BF572 File Offset: 0x000BD772
		public long ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
			set
			{
				if (value < 0L)
				{
					throw new InvalidOperationException("chunk size cannot be negative");
				}
				this.chunkSize = value;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000BF58B File Offset: 0x000BD78B
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x000BF593 File Offset: 0x000BD793
		public bool IsLastChunk
		{
			get
			{
				return this.isLastChunk;
			}
			set
			{
				this.isLastChunk = value;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000BF59C File Offset: 0x000BD79C
		public override bool IsEodSeen
		{
			get
			{
				return this.bytesRead >= this.ChunkSize;
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000BF5AF File Offset: 0x000BD7AF
		public void StartDiscardingMessage()
		{
			base.IsDiscardingData = true;
			if (this.endOfHeaders != null)
			{
				this.endOfHeaders = null;
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000BF5C7 File Offset: 0x000BD7C7
		public void ResetForNewBdatCommand(long chunkSize, bool discardingData, bool isLastChunk, SmtpInDataProxyParser.EndOfHeadersCallback endOfHeadersCallback, ExceptionFilter exceptionFilter)
		{
			this.chunkSize = chunkSize;
			base.IsDiscardingData = discardingData;
			this.isLastChunk = isLastChunk;
			this.EndOfHeaders = endOfHeadersCallback;
			base.ExceptionFilter = exceptionFilter;
			this.bytesRead = 0L;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000BF5F8 File Offset: 0x000BD7F8
		public override void Reset()
		{
			base.Reset();
			this.parserState = ParserState.LF1;
			this.isLastChunk = false;
			this.chunkSize = 0L;
			this.parserState = ParserState.LF1;
			if (this.preEohDataStream != null)
			{
				this.preEohDataStream.Dispose();
			}
			if (this.postEohDataStream != null)
			{
				this.postEohDataStream.Dispose();
			}
			this.preEohDataStream = new MemoryStream();
			this.postEohDataStream = new MemoryStream();
			this.accumulatePostEohData = true;
			this.endOfHeaders = null;
			this.parsedHeaders = null;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000BF67C File Offset: 0x000BD87C
		public override bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed)
		{
			if (numBytes < 0)
			{
				throw new LocalizedException(Strings.SmtpReceiveParserNegativeBytes);
			}
			long val = this.chunkSize - this.bytesRead;
			numBytesConsumed = (int)Math.Min(val, (long)numBytes);
			int num = offset;
			int num2 = offset + numBytesConsumed;
			int num3 = -1;
			bool flag = false;
			while (num < num2 && base.EohPos == -1L)
			{
				switch (this.parserState)
				{
				case ParserState.NONE:
				{
					int num4 = Array.IndexOf<byte>(data, 13, num, num2 - num);
					if (num4 >= 0)
					{
						this.parserState = ParserState.CR1;
						num = num4 + 1;
						continue;
					}
					num = num2;
					continue;
				}
				case ParserState.CR1:
					if (data[num] == 10)
					{
						this.parserState = ParserState.LF1;
					}
					else if (data[num] == 13)
					{
						this.parserState = ParserState.CR1;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					num++;
					continue;
				case ParserState.LF1:
					if (data[num] == 13)
					{
						if (base.EohPos != -1L)
						{
							this.parserState = ParserState.CR1;
						}
						else
						{
							this.parserState = ParserState.EOHCR2;
						}
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					num++;
					continue;
				case ParserState.EOHCR2:
					if (data[num] == 10)
					{
						base.EohPos = base.TotalBytesRead + (long)num - (long)offset - 1L;
						num3 = num + 1;
						this.parserState = ParserState.LF1;
						flag = true;
					}
					else if (data[num] == 13)
					{
						this.parserState = ParserState.CR1;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					num++;
					continue;
				}
				throw new InvalidOperationException("SmtpInBdatParser got into an unknown state");
			}
			this.bytesRead += (long)numBytesConsumed;
			if (base.EohPos == -1L && this.isLastChunk && this.bytesRead >= this.chunkSize)
			{
				base.EohPos = base.TotalBytesRead + (long)num - (long)offset;
				num3 = num;
			}
			base.TotalBytesRead += (long)numBytesConsumed;
			bool result = this.bytesRead >= this.chunkSize;
			if (!base.IsDiscardingData)
			{
				base.TotalBytesWritten += (long)numBytesConsumed;
				int num5;
				if (base.EohPos != -1L && this.parsedHeaders == null)
				{
					num5 = num3 - offset;
					if (num5 > 0)
					{
						this.preEohDataStream.Write(data, offset, num5);
					}
					offset = num3;
					try
					{
						this.parsedHeaders = SmtpInDataProxyParser.GetHeadersFromStream(this.preEohDataStream, this.headersDocument);
						if (flag)
						{
							this.postEohDataStream.Write(SmtpInParser.EodSequence, 3, 2);
						}
						if (this.parsedHeaders != null)
						{
							this.endOfHeaders(this.ParsedHeaders);
						}
					}
					catch (IOException e)
					{
						base.FilterException(e);
						base.IsDiscardingData = true;
						return result;
					}
					catch (ExchangeDataException e2)
					{
						base.FilterException(e2);
						base.IsDiscardingData = true;
						return result;
					}
				}
				num5 = num2 - offset;
				if (num5 > 0)
				{
					if (this.parsedHeaders == null)
					{
						this.preEohDataStream.Write(data, offset, num5);
					}
					else if (this.accumulatePostEohData)
					{
						this.postEohDataStream.Write(data, offset, num5);
					}
				}
			}
			return result;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000BF970 File Offset: 0x000BDB70
		public byte[] GetAccumulatedBytesForProxying()
		{
			if (base.EohPos == -1L)
			{
				throw new InvalidOperationException("Cannot get bytes to proxy before End Of Headers");
			}
			this.accumulatePostEohData = false;
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.parsedHeaders.WriteTo(memoryStream);
				byte[] array = new byte[memoryStream.Length + this.postEohDataStream.Length];
				SmtpInDataProxyParser.CopyMemoryStreamToByteArray(memoryStream, array, 0L, memoryStream.Length);
				SmtpInDataProxyParser.CopyMemoryStreamToByteArray(this.postEohDataStream, array, memoryStream.Length, this.postEohDataStream.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000BFA10 File Offset: 0x000BDC10
		public void Dispose()
		{
			if (this.preEohDataStream != null)
			{
				this.preEohDataStream.Dispose();
				this.preEohDataStream = null;
			}
			if (this.postEohDataStream != null)
			{
				this.postEohDataStream.Dispose();
				this.postEohDataStream = null;
			}
		}

		// Token: 0x04001783 RID: 6019
		private ParserState parserState = ParserState.LF1;

		// Token: 0x04001784 RID: 6020
		private MemoryStream preEohDataStream = new MemoryStream();

		// Token: 0x04001785 RID: 6021
		private MemoryStream postEohDataStream = new MemoryStream();

		// Token: 0x04001786 RID: 6022
		private HeaderList parsedHeaders;

		// Token: 0x04001787 RID: 6023
		private MimeDocument headersDocument;

		// Token: 0x04001788 RID: 6024
		private SmtpInDataProxyParser.EndOfHeadersCallback endOfHeaders;

		// Token: 0x04001789 RID: 6025
		private bool accumulatePostEohData = true;

		// Token: 0x0400178A RID: 6026
		private bool isLastChunk;

		// Token: 0x0400178B RID: 6027
		private long chunkSize;

		// Token: 0x0400178C RID: 6028
		private long bytesRead;
	}
}
