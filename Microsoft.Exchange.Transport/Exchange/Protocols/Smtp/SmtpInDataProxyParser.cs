using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000410 RID: 1040
	internal class SmtpInDataProxyParser : SmtpInParser, IDisposable
	{
		// Token: 0x06002FB7 RID: 12215 RVA: 0x000BEBF8 File Offset: 0x000BCDF8
		public SmtpInDataProxyParser(SmtpInDataProxyParser.EndOfHeadersCallback endOfHeaders, MimeDocument headersDocument)
		{
			if (endOfHeaders == null)
			{
				throw new ArgumentNullException("endOfHeaders");
			}
			if (headersDocument == null)
			{
				throw new ArgumentNullException("headersDocument");
			}
			this.endOfHeaders = endOfHeaders;
			this.headersDocument = headersDocument;
			this.headersDocument.DangerousSetFixBadMimeBoundary(false);
			if (this.headersDocument.RootPart == null)
			{
				this.headersDocument.RootPart = new MimePart();
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000BEC82 File Offset: 0x000BCE82
		// (set) Token: 0x06002FB9 RID: 12217 RVA: 0x000BEC8A File Offset: 0x000BCE8A
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

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000BEC93 File Offset: 0x000BCE93
		public HeaderList ParsedHeaders
		{
			get
			{
				return this.parsedHeaders;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000BEC9B File Offset: 0x000BCE9B
		public ParserState State
		{
			get
			{
				return this.parserState;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000BECA3 File Offset: 0x000BCEA3
		public override bool IsEodSeen
		{
			get
			{
				return this.parserState == ParserState.EOD;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000BECB0 File Offset: 0x000BCEB0
		public static HeaderList GetHeadersFromStream(Stream stream, MimeDocument headersDocument)
		{
			stream.Seek(0L, SeekOrigin.Begin);
			HeaderList result;
			using (MimeReader mimeReader = new MimeReader(stream))
			{
				mimeReader.DangerousSetFixBadMimeBoundary(false);
				headersDocument.DangerousSetFixBadMimeBoundary(false);
				if (!mimeReader.ReadNextPart())
				{
					result = null;
				}
				else
				{
					HeaderList headers = headersDocument.RootPart.Headers;
					MimeHeaderReader headerReader = mimeReader.HeaderReader;
					while (headerReader.ReadNextHeader())
					{
						Type left = Header.TypeFromHeaderId(headerReader.HeaderId);
						Header header;
						if (left == typeof(AddressHeader))
						{
							header = (Header.ReadFrom(headerReader) as AddressHeader);
						}
						else if (left == typeof(ReceivedHeader))
						{
							header = (Header.ReadFrom(headerReader) as ReceivedHeader);
						}
						else if (left == typeof(ContentTypeHeader))
						{
							header = (Header.ReadFrom(headerReader) as ContentTypeHeader);
						}
						else if (left == typeof(ContentDispositionHeader))
						{
							header = (Header.ReadFrom(headerReader) as ContentDispositionHeader);
						}
						else if (left == typeof(TextHeader))
						{
							header = (Header.ReadFrom(headerReader) as TextHeader);
						}
						else if (left == typeof(DateHeader))
						{
							header = (Header.ReadFrom(headerReader) as DateHeader);
						}
						else
						{
							header = Header.Create(headerReader.Name);
							header.Value = headerReader.Value;
						}
						headers.AppendChild(header);
					}
					result = headers;
				}
			}
			return result;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000BEE3C File Offset: 0x000BD03C
		public static void CopyMemoryStreamToByteArray(MemoryStream stream, byte[] destination, long offset, long count)
		{
			byte[] buffer = stream.GetBuffer();
			if (offset <= 2147483647L && count <= 2147483647L)
			{
				Buffer.BlockCopy(buffer, 0, destination, (int)offset, (int)count);
				return;
			}
			for (long num = 0L; num < count; num += 1L)
			{
				checked
				{
					destination[(int)((IntPtr)(unchecked(offset + num)))] = buffer[(int)((IntPtr)num)];
				}
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000BEE87 File Offset: 0x000BD087
		public void StartDiscardingMessage()
		{
			base.IsDiscardingData = true;
			if (this.endOfHeaders != null)
			{
				this.endOfHeaders = null;
			}
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000BEEA0 File Offset: 0x000BD0A0
		public override void Reset()
		{
			base.Reset();
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

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000BEF0C File Offset: 0x000BD10C
		public override bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed)
		{
			bool flag = false;
			int num = offset;
			int i = offset;
			int num2 = offset + numBytes;
			int num3 = 0;
			int num4 = -1;
			if (this.parserState > ParserState.EOHCR2)
			{
				throw new InvalidOperationException("SmtpInDataParser is in an unknown state");
			}
			if (this.parserState == ParserState.EOD)
			{
				throw new InvalidOperationException("SmtpInDataParser received data after EOD");
			}
			while (i < num2)
			{
				switch (this.parserState)
				{
				case ParserState.NONE:
				{
					int num5 = Array.IndexOf<byte>(data, 13, i, num2 - i);
					if (num5 >= 0)
					{
						this.parserState = ParserState.CR1;
						i = num5 + 1;
						continue;
					}
					i = num2;
					continue;
				}
				case ParserState.CR1:
					if (data[i] == 10)
					{
						this.parserState = ParserState.LF1;
					}
					else if (data[i] == 13)
					{
						this.parserState = ParserState.CR1;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				case ParserState.LF1:
					if (data[i] == 46)
					{
						this.parserState = ParserState.DOT;
					}
					else if (data[i] == 13)
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
					i++;
					continue;
				case ParserState.DOT:
					if (data[i] == 13)
					{
						this.parserState = ParserState.CR2;
					}
					else if (!base.IsDiscardingData && base.EohPos == -1L)
					{
						int num6 = i - offset - 1;
						if (num6 > 0)
						{
							this.preEohDataStream.Write(data, offset, num6);
							base.TotalBytesWritten += (long)num6;
						}
						this.parserState = ParserState.NONE;
						offset = i;
					}
					else
					{
						num3++;
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				case ParserState.CR2:
					if (data[i] == 10)
					{
						this.parserState = ParserState.EOD;
						flag = true;
						num2 = i;
						if (base.EohPos == -1L)
						{
							num4 = i;
							base.EohPos = base.TotalBytesRead + (long)i - (long)num - 2L;
						}
					}
					else if (!base.IsDiscardingData && base.EohPos == -1L)
					{
						int num7 = i - offset - 2;
						if (num7 > 0)
						{
							this.preEohDataStream.Write(data, offset, num7);
							base.TotalBytesWritten += (long)num7;
						}
						this.preEohDataStream.Write(SmtpInParser.EodSequence, 3, 1);
						base.TotalBytesWritten += 1L;
						if (data[i] == 13)
						{
							this.parserState = ParserState.CR1;
						}
						else
						{
							this.parserState = ParserState.NONE;
						}
						offset = i;
					}
					else
					{
						num3++;
						if (data[i] == 13)
						{
							this.parserState = ParserState.CR1;
						}
						else
						{
							this.parserState = ParserState.NONE;
						}
					}
					i++;
					continue;
				case ParserState.EOHCR2:
					if (data[i] == 10)
					{
						base.EohPos = base.TotalBytesRead + (long)i - (long)num - 1L;
						this.parserState = ParserState.LF1;
						num4 = i;
					}
					else if (data[i] == 13)
					{
						this.parserState = ParserState.CR1;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				}
				throw new InvalidOperationException("SmtpInDataParser got into an unknown state");
			}
			numBytesConsumed = i - num;
			base.TotalBytesRead += (long)numBytesConsumed;
			if (!base.IsDiscardingData)
			{
				int num8;
				if (base.EohPos != -1L && this.parsedHeaders == null)
				{
					num8 = num4 - offset + 1;
					this.preEohDataStream.Write(data, offset, num8);
					base.TotalBytesWritten += (long)num8;
					offset = num4 + 1;
					try
					{
						this.parsedHeaders = SmtpInDataProxyParser.GetHeadersFromStream(this.preEohDataStream, this.headersDocument);
						if (num4 == num2)
						{
							this.postEohDataStream.Write(SmtpInParser.EodSequence, 2, 3);
						}
						else
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
						return flag;
					}
					catch (ExchangeDataException e2)
					{
						base.FilterException(e2);
						base.IsDiscardingData = true;
						return flag;
					}
				}
				if (this.parserState == ParserState.DOT && base.EohPos == -1L)
				{
					i--;
				}
				num8 = i - offset;
				if (num8 > 0)
				{
					if (this.parsedHeaders == null)
					{
						this.preEohDataStream.Write(data, offset, num8);
						base.TotalBytesWritten += (long)num8;
					}
					else
					{
						base.TotalBytesWritten += (long)(num8 - num3);
						if (this.accumulatePostEohData)
						{
							this.postEohDataStream.Write(data, offset, num8);
						}
					}
				}
				if (flag)
				{
					if (num4 != -1 && numBytesConsumed == 2)
					{
						base.TotalBytesWritten -= 2L;
					}
					else
					{
						base.TotalBytesWritten -= 3L;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000BF37C File Offset: 0x000BD57C
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
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (DotStuffingStream dotStuffingStream = new DotStuffingStream(memoryStream))
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						int num = 4096;
						byte[] buffer = new byte[num];
						for (int i = dotStuffingStream.Read(buffer, 0, num); i > 0; i = dotStuffingStream.Read(buffer, 0, num))
						{
							memoryStream2.Write(buffer, 0, i);
						}
						long num2 = memoryStream2.Length - 3L;
						byte[] array = new byte[num2 + this.postEohDataStream.Length];
						SmtpInDataProxyParser.CopyMemoryStreamToByteArray(memoryStream2, array, 0L, num2);
						SmtpInDataProxyParser.CopyMemoryStreamToByteArray(this.postEohDataStream, array, num2, this.postEohDataStream.Length);
						result = array;
					}
				}
			}
			return result;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000BF49C File Offset: 0x000BD69C
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

		// Token: 0x0400177C RID: 6012
		private ParserState parserState = ParserState.LF1;

		// Token: 0x0400177D RID: 6013
		private MemoryStream preEohDataStream = new MemoryStream();

		// Token: 0x0400177E RID: 6014
		private MemoryStream postEohDataStream = new MemoryStream();

		// Token: 0x0400177F RID: 6015
		private HeaderList parsedHeaders;

		// Token: 0x04001780 RID: 6016
		private MimeDocument headersDocument;

		// Token: 0x04001781 RID: 6017
		private SmtpInDataProxyParser.EndOfHeadersCallback endOfHeaders;

		// Token: 0x04001782 RID: 6018
		private bool accumulatePostEohData = true;

		// Token: 0x02000411 RID: 1041
		// (Invoke) Token: 0x06002FC5 RID: 12229
		public delegate void EndOfHeadersCallback(HeaderList list);
	}
}
