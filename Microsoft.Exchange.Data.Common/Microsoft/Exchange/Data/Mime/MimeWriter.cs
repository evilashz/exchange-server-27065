using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200007A RID: 122
	public class MimeWriter : IDisposable
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001ABA6 File Offset: 0x00018DA6
		public MimeWriter(Stream data) : this(data, true, MimeCommon.DefaultEncodingOptions)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001ABB8 File Offset: 0x00018DB8
		public MimeWriter(Stream data, bool forceMime, EncodingOptions encodingOptions)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!data.CanWrite)
			{
				throw new ArgumentException("Stream must support writing", "data");
			}
			this.forceMime = forceMime;
			this.data = data;
			this.encodingOptions = encodingOptions;
			this.shimStream = new MimeWriter.WriterQueueStream(this);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001AC20 File Offset: 0x00018E20
		public int PartDepth
		{
			get
			{
				this.AssertOpen();
				return this.partDepth;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001AC2E File Offset: 0x00018E2E
		public int EmbeddedDepth
		{
			get
			{
				this.AssertOpen();
				return this.embeddedDepth;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public int StreamOffset
		{
			get
			{
				this.AssertOpen();
				return this.bytesWritten;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001AC4A File Offset: 0x00018E4A
		public EncodingOptions EncodingOptions
		{
			get
			{
				this.AssertOpen();
				return this.encodingOptions;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001AC58 File Offset: 0x00018E58
		public string PartBoundary
		{
			get
			{
				this.AssertOpen();
				string result = null;
				switch (this.state)
				{
				case MimeWriteState.StartPart:
				case MimeWriteState.Headers:
					if (this.currentPart.IsMultipart)
					{
						result = ByteString.BytesToString(this.currentPart.Boundary, false);
					}
					break;
				}
				return result;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001ACA6 File Offset: 0x00018EA6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		protected virtual void Dispose(bool disposing)
		{
			if (this.data == null)
			{
				return;
			}
			try
			{
				if (disposing)
				{
					while (this.partDepth != 0)
					{
						this.EndPart();
					}
					this.FlushWriteQueue();
					if (this.lineTermination != LineTerminationState.CRLF)
					{
						if (this.lineTermination == LineTerminationState.CR)
						{
							this.data.Write(MimeString.CrLf, 1, 1);
						}
						else
						{
							this.data.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
						}
						this.lineTermination = LineTerminationState.CRLF;
					}
				}
			}
			finally
			{
				if (disposing)
				{
					if (this.encodedPartContent != null)
					{
						this.encodedPartContent.Dispose();
					}
					if (this.partContent != null)
					{
						this.partContent.Dispose();
					}
					this.data.Dispose();
				}
				this.state = MimeWriteState.Complete;
				this.encodedPartContent = null;
				this.partContent = null;
				this.data = null;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001AD90 File Offset: 0x00018F90
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001AD98 File Offset: 0x00018F98
		public void WritePart(MimeReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.AssertOpen();
			if (!MimeReader.StateIsOneOf(reader.ReaderState, MimeReaderState.PartStart | MimeReaderState.InlineStart))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			this.StartPart();
			MimeHeaderReader headerReader = reader.HeaderReader;
			while (headerReader.ReadNextHeader())
			{
				this.WriteHeader(headerReader);
			}
			this.WriteContent(reader);
			this.EndPart();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001AE04 File Offset: 0x00019004
		public void WriteHeader(MimeHeaderReader reader)
		{
			this.AssertOpen();
			Header header = Header.ReadFrom(reader);
			header.WriteTo(this);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001AE28 File Offset: 0x00019028
		public void WriteAddress(MimeAddressReader reader)
		{
			this.AssertOpen();
			if (reader.IsGroup)
			{
				this.StartGroup(reader.DisplayName);
				MimeAddressReader groupRecipientReader = reader.GroupRecipientReader;
				while (groupRecipientReader.ReadNextAddress())
				{
					string displayName = groupRecipientReader.DisplayName;
					string email = groupRecipientReader.Email;
					if (displayName == null || email == null)
					{
						throw new ExchangeDataException(Strings.AddressReaderIsNotPositionedOnAddress);
					}
					this.WriteRecipient(displayName, email);
				}
				this.EndGroup();
				return;
			}
			this.WriteRecipient(reader.DisplayName, reader.Email);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001AEAA File Offset: 0x000190AA
		public void WriteParameter(MimeParameterReader reader)
		{
			this.AssertOpen();
			this.WriteParameter(reader.Name, reader.Value);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001AEC8 File Offset: 0x000190C8
		public void WriteContent(MimeReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			using (Stream rawContentReadStream = reader.GetRawContentReadStream())
			{
				if (rawContentReadStream != null)
				{
					using (Stream rawContentWriteStream = this.GetRawContentWriteStream())
					{
						DataStorage.CopyStreamToStream(rawContentReadStream, rawContentWriteStream, long.MaxValue, ref this.scratchBuffer);
					}
				}
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001AF58 File Offset: 0x00019158
		public void StartHeader(string name)
		{
			this.AssertOpen();
			this.WriteHeader(Header.Create(name));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001AF6C File Offset: 0x0001916C
		public void StartHeader(HeaderId headerId)
		{
			this.AssertOpen();
			this.WriteHeader(Header.Create(headerId));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001AF80 File Offset: 0x00019180
		public void WriteHeaderValue(string value)
		{
			this.AssertOpen();
			if (this.headerValueWritten)
			{
				throw new InvalidOperationException(Strings.CannotWriteHeaderValueMoreThanOnce);
			}
			if (this.lastHeader == null)
			{
				throw new InvalidOperationException(Strings.CannotWriteHeaderValueHere);
			}
			this.headerValueWritten = true;
			if (value != null)
			{
				if (!(this.lastHeader is TextHeader))
				{
					byte[] rawValue = ByteString.StringToBytes(value, this.encodingOptions.AllowUTF8);
					this.lastHeader.RawValue = rawValue;
					return;
				}
				this.lastHeader.Value = value;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001AFFC File Offset: 0x000191FC
		public void WriteHeaderValue(DateTime value)
		{
			this.AssertOpen();
			if (this.headerValueWritten)
			{
				throw new InvalidOperationException(Strings.CannotWriteHeaderValueMoreThanOnce);
			}
			if (this.lastHeader == null)
			{
				throw new InvalidOperationException(Strings.CannotWriteHeaderValueHere);
			}
			this.headerValueWritten = true;
			TimeSpan timeZoneOffset = TimeSpan.Zero;
			DateTime utcDateTime = value.ToUniversalTime();
			if (value.Kind != DateTimeKind.Utc)
			{
				timeZoneOffset = TimeZoneInfo.Local.GetUtcOffset(value);
			}
			Header.WriteName(this.shimStream, this.lastHeader.Name, ref this.scratchBuffer);
			MimeStringLength mimeStringLength = new MimeStringLength(0);
			DateHeader.WriteDateHeaderValue(this.shimStream, utcDateTime, timeZoneOffset, ref mimeStringLength);
			this.lastHeader = null;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001B0A4 File Offset: 0x000192A4
		public void WriteContent(byte[] buffer, int offset, int count)
		{
			MimeCommon.CheckBufferArguments(buffer, offset, count);
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			if (this.encodedPartContent != null)
			{
				this.encodedPartContent.Write(buffer, offset, count);
				return;
			}
			using (Stream contentWriteStream = this.GetContentWriteStream())
			{
				contentWriteStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001B118 File Offset: 0x00019318
		public void WriteContent(Stream sourceStream)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			Stream stream = this.encodedPartContent;
			Stream stream2 = null;
			try
			{
				if (stream == null)
				{
					stream2 = this.GetContentWriteStream();
					stream = stream2;
				}
				byte[] buffer = new byte[4096];
				int count;
				while (0 < (count = sourceStream.Read(buffer, 0, 4096)))
				{
					stream.Write(buffer, 0, count);
				}
			}
			finally
			{
				if (stream2 != null)
				{
					stream2.Dispose();
					stream2 = null;
				}
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001B1A8 File Offset: 0x000193A8
		public void WriteRawContent(byte[] buffer, int offset, int count)
		{
			MimeCommon.CheckBufferArguments(buffer, offset, count);
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			Stream rawContentWriteStream = this.partContent;
			if (rawContentWriteStream == null)
			{
				rawContentWriteStream = this.GetRawContentWriteStream();
			}
			rawContentWriteStream.Write(buffer, offset, count);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public void WriteRawContent(Stream sourceStream)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			Stream rawContentWriteStream = this.partContent;
			if (rawContentWriteStream == null)
			{
				rawContentWriteStream = this.GetRawContentWriteStream();
			}
			byte[] buffer = new byte[4096];
			int count;
			while (0 < (count = sourceStream.Read(buffer, 0, 4096)))
			{
				rawContentWriteStream.Write(buffer, 0, count);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001B260 File Offset: 0x00019460
		public void EndContent()
		{
			this.AssertOpen();
			if (this.encodedPartContent != null)
			{
				this.encodedPartContent.Dispose();
				this.encodedPartContent = null;
				this.contentWritten = true;
				if (this.partContent != null)
				{
					this.partContent.Dispose();
					this.partContent = null;
				}
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001B2AE File Offset: 0x000194AE
		public void Flush()
		{
			this.AssertOpen();
			if (this.state == MimeWriteState.Initial)
			{
				return;
			}
			this.FlushHeader();
			this.EndContent();
			this.FlushWriteQueue();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001B2D4 File Offset: 0x000194D4
		internal void WriteMimeNode(MimeNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			Header header = node as Header;
			if (header != null)
			{
				this.WriteHeader(header);
				this.FlushHeader();
				return;
			}
			MimePart mimePart = node as MimePart;
			if (mimePart != null)
			{
				this.StartPart();
				mimePart.WriteTo(this.shimStream, this.encodingOptions);
				this.EndPart();
				return;
			}
			HeaderList headerList = node as HeaderList;
			if (headerList != null)
			{
				foreach (Header header2 in headerList)
				{
					this.WriteHeader(header);
				}
				this.FlushHeader();
				return;
			}
			node = node.Clone();
			MimeRecipient mimeRecipient = node as MimeRecipient;
			if (mimeRecipient != null)
			{
				this.WriteRecipient(mimeRecipient);
				return;
			}
			MimeParameter mimeParameter = node as MimeParameter;
			if (mimeParameter != null)
			{
				this.WriteParameter(mimeParameter);
				return;
			}
			MimeGroup mimeGroup = node as MimeGroup;
			if (mimeGroup != null)
			{
				this.StartGroup(mimeGroup);
				this.EndGroup();
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001B3D0 File Offset: 0x000195D0
		public void WriteHeader(string name, string value)
		{
			this.AssertOpen();
			this.StartHeader(name);
			this.WriteHeaderValue(value);
			this.FlushHeader();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001B3EC File Offset: 0x000195EC
		public void WriteHeader(HeaderId headerId, string value)
		{
			this.AssertOpen();
			this.StartHeader(headerId);
			this.WriteHeaderValue(value);
			this.FlushHeader();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001B408 File Offset: 0x00019608
		public void WriteParameter(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.AssertOpen();
			this.WriteParameter(new MimeParameter(name, value));
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001B42B File Offset: 0x0001962B
		public void StartGroup(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.AssertOpen();
			this.StartGroup(new MimeGroup(name));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001B450 File Offset: 0x00019650
		public void EndGroup()
		{
			this.AssertOpen();
			MimeWriteState mimeWriteState = this.state;
			if (mimeWriteState != MimeWriteState.GroupRecipients)
			{
				throw new InvalidOperationException(Strings.CannotWriteGroupEndHere);
			}
			this.state = MimeWriteState.Recipients;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001B480 File Offset: 0x00019680
		public void WriteRecipient(string displayName, string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.AssertOpen();
			this.WriteRecipient(new MimeRecipient(displayName, address));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001B4A4 File Offset: 0x000196A4
		public MimeWriter GetEmbeddedMessageWriter()
		{
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			MimeWriteState mimeWriteState = this.state;
			switch (mimeWriteState)
			{
			case MimeWriteState.Initial:
			case MimeWriteState.Complete:
				break;
			default:
				switch (mimeWriteState)
				{
				case MimeWriteState.PartContent:
				case MimeWriteState.EndPart:
					break;
				default:
					return new MimeWriter(this.GetRawContentWriteStream())
					{
						embeddedDepth = this.embeddedDepth + 1
					};
				}
				break;
			}
			throw new InvalidOperationException(Strings.CannotWritePartContentNow);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001B518 File Offset: 0x00019718
		public Stream GetRawContentWriteStream()
		{
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			switch (this.state)
			{
			case MimeWriteState.Initial:
			case MimeWriteState.Complete:
			case MimeWriteState.EndPart:
				throw new InvalidOperationException(Strings.CannotWritePartContentNow);
			case MimeWriteState.StartPart:
			case MimeWriteState.Headers:
			case MimeWriteState.Parameters:
			case MimeWriteState.Recipients:
			case MimeWriteState.GroupRecipients:
				this.FlushHeader();
				if (!this.foundMimeVersion)
				{
					if (this.forceMime && this.partDepth == 1)
					{
						this.WriteMimeVersion();
					}
					else
					{
						this.currentPart.IsMultipart = false;
					}
				}
				if (MimeWriteState.StartPart != this.state)
				{
					this.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
				}
				break;
			case MimeWriteState.PartContent:
				return this.partContent;
			}
			if (this.currentPart.IsMultipart)
			{
				throw new InvalidOperationException(Strings.MultipartCannotContainContent);
			}
			this.state = MimeWriteState.PartContent;
			this.partContent = new MimeWriter.WriterContentStream(this);
			return this.partContent;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001B604 File Offset: 0x00019804
		public Stream GetContentWriteStream()
		{
			this.AssertOpen();
			if (this.contentWritten)
			{
				throw new InvalidOperationException(Strings.ContentAlreadyWritten);
			}
			if (this.partContent != null)
			{
				throw new InvalidOperationException(Strings.PartContentIsBeingWritten);
			}
			Stream rawContentWriteStream = this.GetRawContentWriteStream();
			if (this.contentTransferEncoding == ContentTransferEncoding.SevenBit || this.contentTransferEncoding == ContentTransferEncoding.EightBit || this.contentTransferEncoding == ContentTransferEncoding.Binary)
			{
				return rawContentWriteStream;
			}
			if (this.contentTransferEncoding == ContentTransferEncoding.BinHex)
			{
				throw new NotSupportedException(Strings.BinHexNotSupportedForThisMethod);
			}
			ByteEncoder byteEncoder = MimePart.CreateEncoder(null, this.contentTransferEncoding);
			if (byteEncoder == null)
			{
				throw new NotSupportedException(Strings.UnrecognizedTransferEncodingUsed);
			}
			this.encodedPartContent = new EncoderStream(rawContentWriteStream, byteEncoder, EncoderStreamAccess.Write);
			return new SuppressCloseStream(this.encodedPartContent);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001B6AC File Offset: 0x000198AC
		public void StartPart()
		{
			this.AssertOpen();
			MimeWriteState mimeWriteState = this.state;
			if (mimeWriteState == MimeWriteState.Complete || mimeWriteState == MimeWriteState.PartContent)
			{
				throw new InvalidOperationException(Strings.CannotStartPartHere);
			}
			if (this.partDepth != 0)
			{
				this.FlushHeader();
				if (!this.currentPart.IsMultipart)
				{
					throw new InvalidOperationException(Strings.NonMultiPartPartsCannotHaveChildren);
				}
				if (!this.foundMimeVersion && this.forceMime && this.partDepth == 1)
				{
					this.WriteMimeVersion();
				}
				this.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
				this.WriteBoundary(this.currentPart.Boundary, false);
			}
			MimeWriter.PartData partData = default(MimeWriter.PartData);
			this.PushPart(ref partData);
			this.state = MimeWriteState.StartPart;
			this.contentWritten = false;
			this.contentTransferEncoding = ContentTransferEncoding.SevenBit;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001B768 File Offset: 0x00019968
		public void EndPart()
		{
			this.AssertOpen();
			switch (this.state)
			{
			case MimeWriteState.Initial:
			case MimeWriteState.Complete:
				throw new InvalidOperationException(Strings.CannotEndPartHere);
			case MimeWriteState.StartPart:
			case MimeWriteState.Headers:
			case MimeWriteState.Parameters:
			case MimeWriteState.Recipients:
			case MimeWriteState.GroupRecipients:
				this.FlushHeader();
				if (!this.foundMimeVersion)
				{
					if (this.forceMime && this.partDepth == 1)
					{
						this.WriteMimeVersion();
					}
					else
					{
						this.currentPart.IsMultipart = false;
					}
				}
				this.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
				break;
			case MimeWriteState.PartContent:
				if (this.encodedPartContent != null)
				{
					this.encodedPartContent.Dispose();
					this.encodedPartContent = null;
				}
				if (this.partContent != null)
				{
					this.partContent.Dispose();
					this.partContent = null;
				}
				this.contentWritten = true;
				break;
			}
			this.state = MimeWriteState.EndPart;
			if (this.currentPart.IsMultipart)
			{
				this.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
				this.WriteBoundary(this.currentPart.Boundary, true);
			}
			this.PopPart();
			if (this.partDepth == 0)
			{
				this.state = MimeWriteState.Complete;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001B889 File Offset: 0x00019A89
		internal void Write(byte[] data, int offset, int count)
		{
			if (0 >= count)
			{
				return;
			}
			this.QueueWrite(data, offset, count);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001B89C File Offset: 0x00019A9C
		internal void QueueWrite(byte[] data, int offset, int count)
		{
			this.bytesWritten += count;
			this.lineTermination = MimeCommon.AdvanceLineTerminationState(this.lineTermination, data, offset, count);
			int num = (this.writeCount == 1) ? (this.currentWrite.Length - this.currentWrite.Count) : ((this.writeCount == 0) ? MimeWriter.QueuedWrite.QueuedWriteSize : 0);
			if (num >= count)
			{
				if (this.writeCount == 0)
				{
					MimeWriter.QueuedWrite queuedWrite = default(MimeWriter.QueuedWrite);
					this.PushWrite(ref queuedWrite);
				}
				this.currentWrite.Append(data, offset, count);
				return;
			}
			MimeWriter.QueuedWrite queuedWrite2 = default(MimeWriter.QueuedWrite);
			if (count < MimeWriter.QueuedWrite.QueuedWriteSize && this.writeCount > 0)
			{
				queuedWrite2 = this.currentWrite;
			}
			this.FlushWriteQueue();
			if (count < MimeWriter.QueuedWrite.QueuedWriteSize && queuedWrite2.Length > 0)
			{
				queuedWrite2.Reset();
				queuedWrite2.Append(data, offset, count);
				this.PushWrite(ref queuedWrite2);
				return;
			}
			this.data.Write(data, offset, count);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001B98C File Offset: 0x00019B8C
		private void WriteHeader(Header header)
		{
			MimeWriteState mimeWriteState = this.state;
			switch (mimeWriteState)
			{
			case MimeWriteState.Initial:
			case MimeWriteState.Complete:
				break;
			default:
				switch (mimeWriteState)
				{
				case MimeWriteState.PartContent:
				case MimeWriteState.EndPart:
					break;
				default:
					this.state = MimeWriteState.Headers;
					this.FlushHeader();
					this.lastHeader = header;
					return;
				}
				break;
			}
			throw new InvalidOperationException(Strings.CannotWriteHeadersHere);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		private void WriteParameter(MimeParameter parameter)
		{
			if (this.lastHeader == null || !(this.lastHeader is ComplexHeader))
			{
				throw new InvalidOperationException(Strings.CannotWriteParametersOnThisHeader);
			}
			switch (this.state)
			{
			case MimeWriteState.Complete:
			case MimeWriteState.StartPart:
			case MimeWriteState.Recipients:
			case MimeWriteState.PartContent:
			case MimeWriteState.EndPart:
				throw new InvalidOperationException(Strings.CannotWriteParametersHere);
			}
			this.state = MimeWriteState.Parameters;
			ContentTypeHeader contentTypeHeader = this.lastHeader as ContentTypeHeader;
			if (contentTypeHeader != null && contentTypeHeader.IsMultipart && parameter.Name == "boundary")
			{
				string value = parameter.Value;
				if (value.Length == 0)
				{
					throw new ArgumentException(Strings.CannotWriteEmptyOrNullBoundary);
				}
				this.currentPart.Boundary = ByteString.StringToBytes(value, false);
			}
			this.lastHeader.InternalAppendChild(parameter);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		private void WriteRecipient(MimeRecipient recipient)
		{
			if (this.lastHeader == null || !(this.lastHeader is AddressHeader))
			{
				throw new InvalidOperationException(Strings.CannotWriteRecipientsHere);
			}
			MimeNode lastChild;
			switch (this.state)
			{
			case MimeWriteState.Complete:
			case MimeWriteState.StartPart:
			case MimeWriteState.PartContent:
			case MimeWriteState.EndPart:
				throw new InvalidOperationException(Strings.CannotWriteRecipientsHere);
			case MimeWriteState.GroupRecipients:
				lastChild = this.lastHeader.LastChild;
				goto IL_78;
			}
			this.state = MimeWriteState.Recipients;
			lastChild = this.lastHeader;
			IL_78:
			lastChild.InternalAppendChild(recipient);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001BB40 File Offset: 0x00019D40
		private void StartGroup(MimeGroup group)
		{
			MimeWriteState mimeWriteState = this.state;
			switch (mimeWriteState)
			{
			case MimeWriteState.Complete:
			case MimeWriteState.StartPart:
				break;
			default:
				switch (mimeWriteState)
				{
				case MimeWriteState.PartContent:
				case MimeWriteState.EndPart:
					break;
				default:
					if (this.lastHeader == null || !(this.lastHeader is AddressHeader))
					{
						throw new InvalidOperationException(Strings.CannotWriteGroupStartHere);
					}
					this.state = MimeWriteState.GroupRecipients;
					this.lastHeader.InternalAppendChild(group);
					return;
				}
				break;
			}
			throw new InvalidOperationException(Strings.CannotWriteGroupStartHere);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001BBB8 File Offset: 0x00019DB8
		private void FlushHeader()
		{
			this.headerValueWritten = false;
			if (this.lastHeader != null)
			{
				if (this.lastHeader.HeaderId == HeaderId.MimeVersion && this.partDepth == 1)
				{
					this.foundMimeVersion = true;
				}
				else if (this.lastHeader.HeaderId == HeaderId.ContentTransferEncoding)
				{
					string value = this.lastHeader.Value;
					if (value != null)
					{
						this.contentTransferEncoding = MimePart.GetEncodingType(new MimeString(value));
					}
				}
				else if (this.lastHeader.HeaderId == HeaderId.ContentType)
				{
					ContentTypeHeader contentTypeHeader = this.lastHeader as ContentTypeHeader;
					if (contentTypeHeader.IsMultipart)
					{
						this.currentPart.IsMultipart = true;
						MimeParameter mimeParameter = contentTypeHeader["boundary"];
						this.currentPart.Boundary = mimeParameter.RawValue;
					}
					else
					{
						this.currentPart.IsMultipart = false;
					}
					this.currentPart.HasContentType = true;
				}
				this.lastHeader.WriteTo(this.shimStream, this.encodingOptions);
				this.lastHeader = null;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001BCB1 File Offset: 0x00019EB1
		private void WriteMimeVersion()
		{
			this.foundMimeVersion = true;
			this.QueueWrite(MimeString.MimeVersion, 0, MimeString.MimeVersion.Length);
			this.state = MimeWriteState.Headers;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		private void FlushWriteQueue()
		{
			if (this.writeCount != 0)
			{
				this.writeQueue[this.writeCount - 1] = this.currentWrite;
			}
			for (int i = 0; i < this.writeCount; i++)
			{
				this.data.Write(this.writeQueue[i].Data, this.writeQueue[i].Offset, this.writeQueue[i].Count);
				this.writeQueue[i] = default(MimeWriter.QueuedWrite);
			}
			this.writeCount = 0;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001BD70 File Offset: 0x00019F70
		private void WriteBoundary(byte[] boundary, bool final)
		{
			byte[] array;
			if (final)
			{
				array = MimeWriter.terminateBoundarySuffix;
			}
			else
			{
				array = MimeWriter.endBoundarySuffix;
			}
			this.Write(MimeWriter.boundaryPrefix, 0, MimeWriter.boundaryPrefix.Length);
			this.Write(boundary, 0, boundary.Length);
			this.Write(array, 0, array.Length);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		private void PushPart(ref MimeWriter.PartData part)
		{
			if (this.partStack == null)
			{
				this.partStack = new MimeWriter.PartData[8];
				this.partDepth = 0;
			}
			else if (this.partStack.Length == this.partDepth)
			{
				MimeWriter.PartData[] destinationArray = new MimeWriter.PartData[this.partStack.Length * 2];
				Array.Copy(this.partStack, 0, destinationArray, 0, this.partStack.Length);
				for (int i = 0; i < this.partDepth; i++)
				{
					this.partStack[i] = default(MimeWriter.PartData);
				}
				this.partStack = destinationArray;
			}
			if (this.partDepth != 0)
			{
				this.partStack[this.partDepth - 1] = this.currentPart;
			}
			this.partStack[this.partDepth++] = part;
			this.currentPart = part;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001BE9C File Offset: 0x0001A09C
		private void PopPart()
		{
			this.partDepth--;
			this.partStack[this.partDepth] = default(MimeWriter.PartData);
			this.currentPart = this.partStack[(this.partDepth > 0) ? (this.partDepth - 1) : 0];
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		private void AssertOpen()
		{
			if (this.data == null)
			{
				throw new ObjectDisposedException("MimeWriter");
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001BF10 File Offset: 0x0001A110
		private void PushWrite(ref MimeWriter.QueuedWrite write)
		{
			if (this.writeQueue == null)
			{
				this.writeQueue = new MimeWriter.QueuedWrite[16];
				this.writeCount = 0;
			}
			else if (this.writeQueue.Length == this.writeCount)
			{
				MimeWriter.QueuedWrite[] destinationArray = new MimeWriter.QueuedWrite[this.writeQueue.Length * 2];
				Array.Copy(this.writeQueue, 0, destinationArray, 0, this.writeQueue.Length);
				for (int i = 0; i < this.writeQueue.Length; i++)
				{
					this.writeQueue[i] = default(MimeWriter.QueuedWrite);
				}
				this.writeQueue = destinationArray;
			}
			if (this.writeCount != 0)
			{
				this.writeQueue[this.writeCount - 1] = this.currentWrite;
			}
			this.writeQueue[this.writeCount++] = write;
			this.currentWrite = write;
		}

		// Token: 0x0400038E RID: 910
		private static readonly byte[] terminateBoundarySuffix = new byte[]
		{
			45,
			45,
			13,
			10
		};

		// Token: 0x0400038F RID: 911
		private static readonly byte[] endBoundarySuffix = MimeString.CrLf;

		// Token: 0x04000390 RID: 912
		private static readonly byte[] boundaryPrefix = new byte[]
		{
			45,
			45
		};

		// Token: 0x04000391 RID: 913
		private MimeWriteState state;

		// Token: 0x04000392 RID: 914
		private Stream data;

		// Token: 0x04000393 RID: 915
		private Header lastHeader;

		// Token: 0x04000394 RID: 916
		private MimeWriter.WriterContentStream partContent;

		// Token: 0x04000395 RID: 917
		private Stream encodedPartContent;

		// Token: 0x04000396 RID: 918
		private ContentTransferEncoding contentTransferEncoding = ContentTransferEncoding.SevenBit;

		// Token: 0x04000397 RID: 919
		private MimeWriter.PartData[] partStack;

		// Token: 0x04000398 RID: 920
		private int partDepth;

		// Token: 0x04000399 RID: 921
		private int embeddedDepth;

		// Token: 0x0400039A RID: 922
		private MimeWriter.QueuedWrite[] writeQueue;

		// Token: 0x0400039B RID: 923
		private int writeCount;

		// Token: 0x0400039C RID: 924
		private EncodingOptions encodingOptions;

		// Token: 0x0400039D RID: 925
		private int bytesWritten;

		// Token: 0x0400039E RID: 926
		private bool forceMime = true;

		// Token: 0x0400039F RID: 927
		private bool headerValueWritten;

		// Token: 0x040003A0 RID: 928
		private bool contentWritten;

		// Token: 0x040003A1 RID: 929
		private MimeWriter.PartData currentPart;

		// Token: 0x040003A2 RID: 930
		private MimeWriter.QueuedWrite currentWrite;

		// Token: 0x040003A3 RID: 931
		private bool foundMimeVersion;

		// Token: 0x040003A4 RID: 932
		private MimeWriter.WriterQueueStream shimStream;

		// Token: 0x040003A5 RID: 933
		private byte[] scratchBuffer;

		// Token: 0x040003A6 RID: 934
		private LineTerminationState lineTermination;

		// Token: 0x0200007B RID: 123
		private struct PartData
		{
			// Token: 0x1700016D RID: 365
			// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001C040 File Offset: 0x0001A240
			// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001C048 File Offset: 0x0001A248
			public bool IsMultipart
			{
				get
				{
					return this.multipartPart;
				}
				set
				{
					this.multipartPart = value;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001C051 File Offset: 0x0001A251
			// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001C059 File Offset: 0x0001A259
			public bool HasContentType
			{
				get
				{
					return this.contentType;
				}
				set
				{
					this.contentType = value;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001C062 File Offset: 0x0001A262
			// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001C06A File Offset: 0x0001A26A
			public byte[] Boundary
			{
				get
				{
					return this.boundary;
				}
				set
				{
					this.boundary = value;
				}
			}

			// Token: 0x040003A7 RID: 935
			private byte[] boundary;

			// Token: 0x040003A8 RID: 936
			private bool contentType;

			// Token: 0x040003A9 RID: 937
			private bool multipartPart;
		}

		// Token: 0x0200007C RID: 124
		private struct QueuedWrite
		{
			// Token: 0x17000170 RID: 368
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001C073 File Offset: 0x0001A273
			public int Length
			{
				get
				{
					return this.data.Length;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001C07D File Offset: 0x0001A27D
			public byte[] Data
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001C085 File Offset: 0x0001A285
			public int Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001C08D File Offset: 0x0001A28D
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001C095 File Offset: 0x0001A295
			public bool Full
			{
				get
				{
					return this.data != null && this.Count == this.data.Length;
				}
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
			public void Reset()
			{
				this.count = 0;
				this.offset = 0;
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
			public int Append(byte[] buffer, int offset, int count)
			{
				if (this.Full)
				{
					return 0;
				}
				if (this.data == null)
				{
					this.data = new byte[MimeWriter.QueuedWrite.QueuedWriteSize];
				}
				int num = Math.Min(count, this.data.Length - this.Count);
				Buffer.BlockCopy(buffer, offset, this.data, this.Count, num);
				this.count += num;
				return num;
			}

			// Token: 0x040003AA RID: 938
			public static int QueuedWriteSize = 4096;

			// Token: 0x040003AB RID: 939
			private byte[] data;

			// Token: 0x040003AC RID: 940
			private int offset;

			// Token: 0x040003AD RID: 941
			private int count;
		}

		// Token: 0x0200007D RID: 125
		internal class WriterQueueStream : Stream
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x0001C138 File Offset: 0x0001A338
			public WriterQueueStream(MimeWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001C147 File Offset: 0x0001A347
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001C14A File Offset: 0x0001A34A
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001C14D File Offset: 0x0001A34D
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001C150 File Offset: 0x0001A350
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001C157 File Offset: 0x0001A357
			// (set) Token: 0x06000508 RID: 1288 RVA: 0x0001C15E File Offset: 0x0001A35E
			public override long Position
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x0001C165 File Offset: 0x0001A365
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0001C16C File Offset: 0x0001A36C
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (count > 0)
				{
					this.writer.QueueWrite(buffer, offset, count);
				}
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x0001C180 File Offset: 0x0001A380
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x0001C187 File Offset: 0x0001A387
			public override void Flush()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0001C18E File Offset: 0x0001A38E
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x0001C195 File Offset: 0x0001A395
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
			}

			// Token: 0x040003AE RID: 942
			private MimeWriter writer;
		}

		// Token: 0x0200007E RID: 126
		private class WriterContentStream : Stream
		{
			// Token: 0x0600050F RID: 1295 RVA: 0x0001C19E File Offset: 0x0001A39E
			public WriterContentStream(MimeWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001C1AD File Offset: 0x0001A3AD
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001C1B3 File Offset: 0x0001A3B3
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001C1B6 File Offset: 0x0001A3B6
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001C1BD File Offset: 0x0001A3BD
			// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
			public override long Position
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0001C1CB File Offset: 0x0001A3CB
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0001C1D2 File Offset: 0x0001A3D2
			public override void Write(byte[] buffer, int offset, int count)
			{
				MimeCommon.CheckBufferArguments(buffer, offset, count);
				if (this.writer.contentWritten)
				{
					throw new InvalidOperationException(Strings.ContentAlreadyWritten);
				}
				this.writer.Write(buffer, offset, count);
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x0001C202 File Offset: 0x0001A402
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0001C209 File Offset: 0x0001A409
			public override void Flush()
			{
				if (this.writer.contentWritten)
				{
					throw new InvalidOperationException(Strings.ContentAlreadyWritten);
				}
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x0001C223 File Offset: 0x0001A423
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x0001C22A File Offset: 0x0001A42A
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
			}

			// Token: 0x040003AF RID: 943
			private MimeWriter writer;
		}
	}
}
