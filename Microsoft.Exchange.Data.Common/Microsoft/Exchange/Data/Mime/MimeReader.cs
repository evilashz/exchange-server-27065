using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200006A RID: 106
	public class MimeReader : IDisposable
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00015F3C File Offset: 0x0001413C
		public MimeReader(Stream mime) : this(mime, true, DecodingOptions.Default, MimeLimits.Unlimited, false, true)
		{
			if (mime == null)
			{
				throw new ArgumentNullException("mime");
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00015F60 File Offset: 0x00014160
		public MimeReader(Stream mime, bool inferMime, DecodingOptions decodingOptions, MimeLimits mimeLimits) : this(mime, inferMime, decodingOptions, mimeLimits, false, true)
		{
			if (mime == null)
			{
				throw new ArgumentNullException("mime");
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00015F7D File Offset: 0x0001417D
		internal MimeReader(Stream mime, bool inferMime, DecodingOptions decodingOptions, MimeLimits mimeLimits, bool parseEmbeddedMessages, bool parseInline) : this(mime, inferMime, decodingOptions, mimeLimits, parseEmbeddedMessages, parseInline, true)
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00015F90 File Offset: 0x00014190
		internal MimeReader(Stream mime, bool inferMime, DecodingOptions decodingOptions, MimeLimits mimeLimits, bool parseEmbeddedMessages, bool parseInline, bool expectBinaryContent)
		{
			this.FixBadMimeBoundary = true;
			this.state = MimeReaderState.Start;
			this.partCount = 1;
			this.eofMeansEndOfFile = true;
			base..ctor();
			if (mime != null && !mime.CanRead)
			{
				throw new ArgumentException(Strings.StreamMustAllowRead, "mime");
			}
			this.mimeStream = mime;
			this.parser = new MimeParser(true, parseInline, expectBinaryContent);
			this.data = new byte[5120];
			this.inferMime = inferMime;
			this.decodingOptions = decodingOptions;
			this.limits = mimeLimits;
			this.parseEmbeddedMessages = parseEmbeddedMessages;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00016020 File Offset: 0x00014220
		private MimeReader(MimeReader parent)
		{
			this.FixBadMimeBoundary = true;
			this.state = MimeReaderState.Start;
			this.partCount = 1;
			this.eofMeansEndOfFile = true;
			base..ctor();
			this.parentReader = parent;
			this.parentReader.childReader = this;
			this.mimeStream = parent.mimeStream;
			this.parser = parent.parser;
			this.data = parent.data;
			this.dataOffset = parent.dataOffset;
			this.dataCount = parent.dataCount;
			this.dataEOF = parent.dataEOF;
			this.outerContentStream = parent.outerContentStream;
			this.outerContentDepth = -1;
			this.inferMime = parent.inferMime;
			this.limits = parent.limits;
			this.decodingOptions = parent.decodingOptions;
			this.partCount = parent.partCount;
			this.headerCount = parent.headerCount;
			this.cumulativeHeaderBytes = parent.cumulativeHeaderBytes;
			this.embeddedDepth = parent.embeddedDepth + 1;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00016114 File Offset: 0x00014314
		internal MimeReader(IMimeHandlerInternal handler, bool inferMime, DecodingOptions decodingOptions, MimeLimits mimeLimits, bool parseInline)
		{
			this.FixBadMimeBoundary = true;
			this.state = MimeReaderState.Start;
			this.partCount = 1;
			this.eofMeansEndOfFile = true;
			base..ctor();
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.handler = handler;
			this.parser = new MimeParser(true, parseInline, true);
			this.data = new byte[5120];
			this.inferMime = inferMime;
			this.decodingOptions = decodingOptions;
			this.limits = mimeLimits;
			this.parseEmbeddedMessages = true;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00016194 File Offset: 0x00014394
		public MimeLimits MimeLimits
		{
			get
			{
				this.AssertGoodToUse(false, false);
				return this.limits;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000161A4 File Offset: 0x000143A4
		public DecodingOptions HeaderDecodingOptions
		{
			get
			{
				return this.decodingOptions;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000161AC File Offset: 0x000143AC
		public MimeComplianceStatus ComplianceStatus
		{
			get
			{
				this.AssertGoodToUse(false, false);
				return this.parser.ComplianceStatus;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000161C1 File Offset: 0x000143C1
		public long StreamOffset
		{
			get
			{
				this.AssertGoodToUse(false, true);
				return (long)this.parser.Position;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000161D7 File Offset: 0x000143D7
		internal int Depth
		{
			get
			{
				this.AssertGoodToUse(false, true);
				return this.depth;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000161E7 File Offset: 0x000143E7
		public int PartDepth
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (this.depth != 0)
				{
					return this.parser.PartDepth + 1;
				}
				return 0;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00016208 File Offset: 0x00014408
		public int EmbeddedDepth
		{
			get
			{
				this.AssertGoodToUse(false, true);
				return this.embeddedDepth;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00016218 File Offset: 0x00014418
		internal MimeReaderState ReaderState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00016220 File Offset: 0x00014420
		internal bool DataExhausted
		{
			get
			{
				return this.dataExhausted;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00016228 File Offset: 0x00014428
		internal bool EndOfFile
		{
			get
			{
				return this.state == MimeReaderState.End;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00016237 File Offset: 0x00014437
		public MimeHeaderReader HeaderReader
		{
			get
			{
				this.AssertGoodToUse(true, true);
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete | MimeReaderState.EndOfHeaders | MimeReaderState.InlineStart))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return new MimeHeaderReader(this);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00016264 File Offset: 0x00014464
		internal HeaderId HeaderId
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.currentHeaderId;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001628E File Offset: 0x0001448E
		internal string HeaderName
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.currentHeaderName;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000162B8 File Offset: 0x000144B8
		public bool IsMultipart
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (this.state == MimeReaderState.InlineStart)
				{
					return false;
				}
				if (this.state != MimeReaderState.EndOfHeaders)
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.currentPartMajorType == MajorContentType.Multipart && this.parser.IsMime;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00016308 File Offset: 0x00014508
		public bool IsEmbeddedMessage
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (this.state == MimeReaderState.InlineStart)
				{
					return false;
				}
				if (this.state != MimeReaderState.EndOfHeaders)
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.currentPartMajorType == MajorContentType.MessageRfc822 && this.parser.IsMime;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00016357 File Offset: 0x00014557
		public string ContentType
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (this.state == MimeReaderState.InlineStart)
				{
					return "application/octet-stream";
				}
				if (this.state != MimeReaderState.EndOfHeaders)
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.currentPartContentType;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001638F File Offset: 0x0001458F
		internal ContentTransferEncoding ContentTransferEncoding
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (this.state != MimeReaderState.EndOfHeaders && this.state != MimeReaderState.InlineStart)
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.currentPartContentTransferEncoding;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000163C1 File Offset: 0x000145C1
		public bool IsInline
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (MimeReader.StateIsOneOf(this.state, MimeReaderState.Start | MimeReaderState.End))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return MimeReader.StateIsOneOf(this.state, MimeReaderState.InlineStart | MimeReaderState.InlineBody | MimeReaderState.InlineEnd);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000163F8 File Offset: 0x000145F8
		public string InlineFileName
		{
			get
			{
				this.AssertGoodToUse(false, true);
				if (!this.IsInline)
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				return this.inlineFileName.ToString();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00016426 File Offset: 0x00014626
		internal LineTerminationState LineTerminationState
		{
			get
			{
				return this.currentLineTerminationState;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001642E File Offset: 0x0001462E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016440 File Offset: 0x00014640
		protected virtual void Dispose(bool disposing)
		{
			if (this.parser == null)
			{
				return;
			}
			if (disposing)
			{
				if (this.childReader != null)
				{
					throw new InvalidOperationException(Strings.EmbeddedMessageReaderNeedsToBeClosedFirst);
				}
				if (this.parentReader == null)
				{
					if (this.mimeStream != null)
					{
						this.mimeStream.Dispose();
					}
				}
				else
				{
					this.parentReader.partCount = this.partCount;
					this.parentReader.headerCount = this.headerCount;
					this.parentReader.cumulativeHeaderBytes = this.cumulativeHeaderBytes;
					this.parentReader.dataOffset = this.dataOffset;
					this.parentReader.dataCount = this.dataCount;
					this.parentReader.dataEOF = this.dataEOF;
					this.parentReader.currentToken = this.currentToken;
					this.parentReader.cleanupDepth = this.depth + this.cleanupDepth;
					this.parentReader.state = MimeReaderState.EmbeddedEnd;
					this.parentReader.childReader = null;
					this.parentReader = null;
				}
				if (this.contentStream != null)
				{
					this.contentStream.Dispose();
				}
				if (this.outerContentStream != null)
				{
					int num = this.outerContentDepth;
				}
			}
			this.state = MimeReaderState.End;
			this.mimeStream = null;
			this.handler = null;
			this.contentStream = null;
			this.outerContentStream = null;
			this.data = null;
			this.parser = null;
			this.currentHeader = null;
			this.currentChild = null;
			this.currentGrandChild = null;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000165B2 File Offset: 0x000147B2
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000165BA File Offset: 0x000147BA
		internal void DisconnectInputStream()
		{
			this.mimeStream = null;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000165C3 File Offset: 0x000147C3
		public bool ReadNextPart()
		{
			this.AssertGoodToUse(true, true);
			this.TrySkipToNextPartBoundary(false);
			return MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000165E8 File Offset: 0x000147E8
		public bool ReadFirstChildPart()
		{
			this.AssertGoodToUse(true, true);
			if (this.state == MimeReaderState.InlineStart)
			{
				return false;
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.Start | MimeReaderState.EndOfHeaders))
			{
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				do
				{
					this.TryReachNextState();
				}
				while (this.state != MimeReaderState.EndOfHeaders);
			}
			if (this.state == MimeReaderState.EndOfHeaders && !this.IsMultipart && (!this.IsEmbeddedMessage || !this.parseEmbeddedMessages))
			{
				return false;
			}
			this.TrySkipToNextPartBoundary(true);
			return this.state == MimeReaderState.PartStart;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001667C File Offset: 0x0001487C
		public bool ReadNextSiblingPart()
		{
			this.AssertGoodToUse(true, true);
			if (this.state == MimeReaderState.End)
			{
				return false;
			}
			if (MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete | MimeReaderState.EndOfHeaders))
			{
				this.createHeader = false;
				while (this.state != MimeReaderState.EndOfHeaders)
				{
					this.TryReachNextState();
				}
				this.parser.SetContentType(MajorContentType.Other, default(MimeString));
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.Start | MimeReaderState.PartEnd | MimeReaderState.InlineEnd))
			{
				int num = this.depth;
				do
				{
					this.TrySkipToNextPartBoundary(true);
				}
				while (this.depth > num || !MimeReader.StateIsOneOf(this.state, MimeReaderState.PartEnd | MimeReaderState.InlineEnd));
			}
			this.TrySkipToNextPartBoundary(true);
			return MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00016732 File Offset: 0x00014932
		public void EnableReadingUnparsedHeaders()
		{
			this.enableReadingOuterContent = true;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001673C File Offset: 0x0001493C
		public void ReadHeaders()
		{
			this.AssertGoodToUse(true, true);
			if (MimeReader.StateIsOneOf(this.state, MimeReaderState.EndOfHeaders | MimeReaderState.InlineStart))
			{
				return;
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			this.createHeader = false;
			do
			{
				this.TryReachNextState();
			}
			while (this.state != MimeReaderState.EndOfHeaders);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00016798 File Offset: 0x00014998
		internal bool ReadNextHeader()
		{
			this.AssertGoodToUse(true, true);
			if (MimeReader.StateIsOneOf(this.state, MimeReaderState.EndOfHeaders | MimeReaderState.InlineStart))
			{
				return false;
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			do
			{
				this.TryReachNextState();
			}
			while (!MimeReader.StateIsOneOf(this.state, MimeReaderState.HeaderStart | MimeReaderState.EndOfHeaders));
			return this.state == MimeReaderState.HeaderStart;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000167FA File Offset: 0x000149FA
		internal Header ReadHeaderObject()
		{
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			if (this.state == MimeReaderState.HeaderStart)
			{
				this.TryCompleteCurrentHeader(true);
			}
			return this.currentHeader;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001682D File Offset: 0x00014A2D
		internal bool TryCompleteCurrentHeader(bool createHeader)
		{
			this.createHeader = (this.createHeader || createHeader);
			if (!this.TryReachNextState())
			{
				return false;
			}
			this.currentHeaderConsumed = false;
			this.currentChildConsumed = false;
			this.currentChild = null;
			this.currentGrandChild = null;
			return true;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00016868 File Offset: 0x00014A68
		internal Header CurrentHeaderObject
		{
			get
			{
				return this.currentHeader;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00016870 File Offset: 0x00014A70
		internal void Reset(Stream stream)
		{
			this.mimeStream = stream;
			this.parser.Reset();
			this.state = MimeReaderState.Start;
			this.depth = 0;
			this.cleanupDepth = 0;
			this.embeddedDepth = 0;
			this.dataExhausted = false;
			this.dataEOF = false;
			this.dataOffset = 0;
			this.dataCount = 0;
			this.currentToken = default(MimeToken);
			this.currentHeader = null;
			this.currentChild = null;
			this.currentGrandChild = null;
			this.decoder = null;
			this.readRawData = false;
			this.contentStream = null;
			this.enableReadingOuterContent = false;
			this.outerContentStream = null;
			this.outerContentDepth = 0;
			this.childReader = null;
			this.parentReader = null;
			this.skipPart = false;
			this.skipHeaders = false;
			this.skipHeader = false;
			this.partCount = 0;
			this.headerCount = 0;
			this.cumulativeHeaderBytes = 0;
			this.currentTextHeaderBytes = 0;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016951 File Offset: 0x00014B51
		internal void DangerousSetFixBadMimeBoundary(bool value)
		{
			this.FixBadMimeBoundary = value;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001695C File Offset: 0x00014B5C
		internal HeaderList ReadHeaderList()
		{
			this.AssertGoodToUse(true, true);
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			HeaderList headerList = new HeaderList(null);
			if (this.state == MimeReaderState.InlineStart)
			{
				return headerList;
			}
			do
			{
				this.TryReachNextState();
				if (this.state == MimeReaderState.HeaderStart)
				{
					this.createHeader = true;
				}
				else if (this.state == MimeReaderState.HeaderComplete && this.currentHeader != null)
				{
					headerList.InternalAppendChild(this.currentHeader);
				}
			}
			while (this.state != MimeReaderState.EndOfHeaders);
			return headerList;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000169E8 File Offset: 0x00014BE8
		internal bool ReadNextDescendant(bool topLevel)
		{
			this.AssertGoodToUse(true, true);
			if (this.state != MimeReaderState.HeaderComplete)
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			if (topLevel)
			{
				if (this.currentHeaderConsumed)
				{
					return false;
				}
				if (this.currentChild == null)
				{
					this.currentChild = this.currentHeader.FirstChild;
				}
				else
				{
					this.currentGrandChild = null;
					this.currentChild = this.currentChild.NextSibling;
				}
				if (this.currentChild == null)
				{
					this.currentHeaderConsumed = true;
				}
				this.currentChildConsumed = false;
				this.currentGrandChild = null;
				return this.currentChild != null;
			}
			else
			{
				if (this.currentChild == null)
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				if (this.currentChildConsumed)
				{
					return false;
				}
				if (this.currentGrandChild == null)
				{
					this.currentGrandChild = this.currentChild.FirstChild;
				}
				else
				{
					this.currentGrandChild = this.currentGrandChild.NextSibling;
				}
				if (this.currentGrandChild == null)
				{
					this.currentChildConsumed = true;
				}
				return this.currentGrandChild != null;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016AE0 File Offset: 0x00014CE0
		internal bool IsCurrentChildValid(bool topLevel)
		{
			if (!topLevel)
			{
				return this.currentGrandChild != null;
			}
			return this.currentChild != null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00016B00 File Offset: 0x00014D00
		public void CopyOuterContentTo(Stream stream)
		{
			this.AssertGoodToUse(false, true);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(Strings.StreamMustSupportWriting, "stream");
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			if (this.outerContentStream != null)
			{
				throw new NotSupportedException(Strings.OnlyOneOuterContentPushStreamAllowed);
			}
			this.outerContentStream = stream;
			this.outerContentDepth = this.depth;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00016B80 File Offset: 0x00014D80
		public int ReadRawContent(byte[] buffer, int offset, int count)
		{
			this.AssertGoodToUse(true, true);
			MimeCommon.CheckBufferArguments(buffer, offset, count);
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartBody | MimeReaderState.InlineBody))
			{
				if (MimeReader.StateIsOneOf(this.state, MimeReaderState.PartEnd | MimeReaderState.InlineEnd))
				{
					return 0;
				}
				this.TryInitializeReadContent(false);
			}
			if (this.contentStream != null)
			{
				throw new NotSupportedException(Strings.CannotReadContentWhileStreamIsActive);
			}
			if (!this.readRawData)
			{
				throw new NotSupportedException(Strings.CannotMixReadRawContentAndReadContent);
			}
			int result;
			this.ReadPartData(buffer, offset, count, out result);
			return result;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00016C00 File Offset: 0x00014E00
		public int ReadContent(byte[] buffer, int offset, int count)
		{
			this.AssertGoodToUse(true, true);
			MimeCommon.CheckBufferArguments(buffer, offset, count);
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartBody | MimeReaderState.InlineBody))
			{
				if (MimeReader.StateIsOneOf(this.state, MimeReaderState.PartEnd | MimeReaderState.InlineEnd))
				{
					return 0;
				}
				if (!this.TryInitializeReadContent(true))
				{
					throw new MimeException(Strings.CannotDecodeContentStream);
				}
			}
			if (this.contentStream != null)
			{
				throw new NotSupportedException(Strings.CannotReadContentWhileStreamIsActive);
			}
			if (this.readRawData)
			{
				throw new NotSupportedException(Strings.CannotMixReadRawContentAndReadContent);
			}
			int result;
			bool flag = this.ReadPartData(buffer, offset, count, out result);
			return result;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00016C8C File Offset: 0x00014E8C
		public Stream GetContentReadStream()
		{
			Stream result;
			if (!this.TryGetContentReadStream(out result))
			{
				throw new MimeException(Strings.CannotDecodeContentStream);
			}
			return result;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00016CB0 File Offset: 0x00014EB0
		public bool TryGetContentReadStream(out Stream result)
		{
			this.AssertGoodToUse(true, true);
			if (!this.TryInitializeReadContent(true))
			{
				result = null;
				return false;
			}
			this.contentStream = new MimeReader.ContentReadStream(this);
			result = this.contentStream;
			return true;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016CEA File Offset: 0x00014EEA
		public Stream GetRawContentReadStream()
		{
			this.AssertGoodToUse(true, true);
			this.TryInitializeReadContent(false);
			this.contentStream = new MimeReader.ContentReadStream(this);
			return this.contentStream;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00016D10 File Offset: 0x00014F10
		private bool TryInitializeReadContent(bool decode)
		{
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.EndOfHeaders | MimeReaderState.InlineStart))
			{
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				if (this.state == MimeReaderState.PartStart && this.enableReadingOuterContent)
				{
					this.parser.SetStreamMode();
					this.state = MimeReaderState.PartBody;
					this.decoder = null;
					this.readRawData = !decode;
					return true;
				}
				while (this.TryReachNextState())
				{
					if (this.state == MimeReaderState.EndOfHeaders)
					{
						goto IL_7A;
					}
				}
				return false;
			}
			IL_7A:
			MimeReaderState mimeReaderState = this.state;
			if (this.state == MimeReaderState.EndOfHeaders)
			{
				this.parser.SetContentType(MajorContentType.Other, default(MimeString));
				mimeReaderState = MimeReaderState.PartBody;
			}
			else
			{
				mimeReaderState = MimeReaderState.InlineBody;
			}
			if (decode)
			{
				ContentTransferEncoding transferEncoding = this.parser.TransferEncoding;
				if (transferEncoding == ContentTransferEncoding.SevenBit || transferEncoding == ContentTransferEncoding.EightBit || transferEncoding == ContentTransferEncoding.Binary)
				{
					this.decoder = null;
				}
				else
				{
					this.decoder = MimePart.CreateDecoder(transferEncoding);
					if (this.decoder == null)
					{
						return false;
					}
				}
				this.readRawData = false;
			}
			else
			{
				this.decoder = null;
				this.readRawData = true;
			}
			this.state = mimeReaderState;
			return true;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00016E24 File Offset: 0x00015024
		public MimeReader GetEmbeddedMessageReader()
		{
			this.AssertGoodToUse(true, true);
			if (this.state != MimeReaderState.EndOfHeaders)
			{
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderComplete))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				do
				{
					this.TryReachNextState();
				}
				while (this.state != MimeReaderState.EndOfHeaders);
			}
			if (this.currentPartMajorType != MajorContentType.MessageRfc822 || !this.parser.IsMime)
			{
				throw new InvalidOperationException(Strings.CurrentPartIsNotEmbeddedMessage);
			}
			this.parser.SetContentType(MajorContentType.MessageRfc822, default(MimeString));
			this.TryReachNextState();
			this.childReader = new MimeReader(this);
			return this.childReader;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00016EBF File Offset: 0x000150BF
		public void ResetComplianceStatus()
		{
			this.parser.ComplianceStatus = MimeComplianceStatus.Compliant;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00016ECD File Offset: 0x000150CD
		private int TrimEndOfLine(int offset, int length)
		{
			if (length >= 1 && this.data[offset + length - 1] == 10)
			{
				length--;
				while (length >= 1 && this.data[offset + length - 1] == 13)
				{
					length--;
				}
			}
			return length;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00016F04 File Offset: 0x00015104
		private void ParseAndCheckSize()
		{
			this.currentToken = this.parser.Parse(this.data, this.dataOffset, this.dataOffset + this.dataCount, this.dataEOF);
			if (this.parser.Position > this.MimeLimits.MaxSize)
			{
				throw new MimeException(Strings.InputStreamTooLong(this.parser.Position, this.MimeLimits.MaxSize));
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00016F7C File Offset: 0x0001517C
		private void CheckHeaderBytesLimits()
		{
			this.cumulativeHeaderBytes += (int)this.currentToken.Length;
			if (this.cumulativeHeaderBytes > this.MimeLimits.MaxHeaderBytes)
			{
				throw new MimeException(Strings.TooManyHeaderBytes(this.cumulativeHeaderBytes, this.MimeLimits.MaxHeaderBytes));
			}
			if (this.currentToken.Id == MimeTokenId.Header)
			{
				this.currentTextHeaderBytes = 0;
			}
			this.currentTextHeaderBytes += (int)this.currentToken.Length;
			Type left = Header.TypeFromHeaderId(this.currentHeaderId);
			if ((left == typeof(TextHeader) || left == typeof(AsciiTextHeader)) && this.currentTextHeaderBytes > this.MimeLimits.MaxTextValueBytesPerValue)
			{
				throw new MimeException(Strings.TooManyTextValueBytes(this.currentTextHeaderBytes, this.MimeLimits.MaxTextValueBytesPerValue));
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001705C File Offset: 0x0001525C
		private void CheckPartsLimit()
		{
			if (++this.partCount > this.MimeLimits.MaxParts)
			{
				throw new MimeException(Strings.TooManyParts(this.partCount, this.MimeLimits.MaxParts));
			}
			if (this.PartDepth > this.MimeLimits.MaxPartDepth)
			{
				throw new MimeException(Strings.PartNestingTooDeep(this.PartDepth, this.MimeLimits.MaxPartDepth));
			}
			if (this.embeddedDepth > this.MimeLimits.MaxEmbeddedDepth)
			{
				throw new MimeException(Strings.EmbeddedNestingTooDeep(this.embeddedDepth, this.MimeLimits.MaxEmbeddedDepth));
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00017101 File Offset: 0x00015301
		internal bool GroupInProgress
		{
			get
			{
				return this.currentChild != null && this.currentChild is MimeGroup;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001711C File Offset: 0x0001531C
		internal string ReadRecipientEmail(bool topLevel)
		{
			string result = null;
			MimeRecipient mimeRecipient;
			if (topLevel)
			{
				mimeRecipient = (this.currentChild as MimeRecipient);
				if (mimeRecipient == null)
				{
					throw new NotSupportedException(Strings.CurrentAddressIsGroupAndCannotHaveEmail);
				}
			}
			else
			{
				mimeRecipient = (this.currentGrandChild as MimeRecipient);
			}
			if (mimeRecipient != null)
			{
				result = mimeRecipient.Email;
			}
			return result;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00017160 File Offset: 0x00015360
		internal bool TryReadDisplayName(bool topLevel, DecodingOptions decodingOptions, out DecodingResults decodingResults, out string displayName)
		{
			AddressItem addressItem;
			if (topLevel)
			{
				addressItem = (this.currentChild as AddressItem);
			}
			else
			{
				addressItem = (this.currentGrandChild as AddressItem);
			}
			return addressItem.TryGetDisplayName(decodingOptions, out decodingResults, out displayName);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00017194 File Offset: 0x00015394
		internal string ReadParameterName()
		{
			MimeParameter mimeParameter = this.currentChild as MimeParameter;
			return mimeParameter.Name;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000171B4 File Offset: 0x000153B4
		internal bool TryReadParameterValue(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string value)
		{
			MimeParameter mimeParameter = this.currentChild as MimeParameter;
			return mimeParameter.TryGetValue(decodingOptions, out decodingResults, out value);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000171D8 File Offset: 0x000153D8
		internal int AddMoreData(byte[] buffer, int offset, int length, bool endOfFile)
		{
			this.CompactDataBuffer();
			int num;
			if (length != 0)
			{
				num = Math.Min(length, this.data.Length - (this.dataOffset + this.dataCount) - 2);
				Buffer.BlockCopy(buffer, offset, this.data, this.dataOffset + this.dataCount, num);
				length -= num;
				this.dataCount += num;
			}
			else
			{
				num = 0;
			}
			this.dataEOF = (length == 0 && endOfFile);
			return num;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00017250 File Offset: 0x00015450
		private bool ReadMoreData()
		{
			this.CompactDataBuffer();
			int num = this.dataOffset + this.dataCount;
			int num2 = this.mimeStream.Read(this.data, num, this.data.Length - num - 2);
			if (num2 != 0)
			{
				this.dataCount += num2;
				return true;
			}
			if (this.eofMeansEndOfFile)
			{
				this.dataEOF = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000172B8 File Offset: 0x000154B8
		private void CompactDataBuffer()
		{
			if (this.dataCount == 0)
			{
				this.dataOffset = 0;
				return;
			}
			if (this.data.Length - this.dataOffset + this.dataCount < this.data.Length / 2)
			{
				Buffer.BlockCopy(this.data, this.dataOffset, this.data, 0, this.dataCount);
				this.dataOffset = 0;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001731C File Offset: 0x0001551C
		internal void Write(byte[] buffer, int offset, int length)
		{
			if (this.dataEOF)
			{
				throw new InvalidOperationException(Strings.CannotWriteAfterFlush);
			}
			for (;;)
			{
				if (this.currentToken.Id == MimeTokenId.None && this.state != MimeReaderState.Start)
				{
					this.ParseAndCheckSize();
					if (this.currentToken.Id == MimeTokenId.None)
					{
						if (length == 0)
						{
							break;
						}
						int num = this.AddMoreData(buffer, offset, length, false);
						offset += num;
						length -= num;
						continue;
					}
				}
				this.HandleTokenInPushMode();
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00017388 File Offset: 0x00015588
		internal void Flush()
		{
			if (this.dataEOF)
			{
				return;
			}
			this.dataEOF = true;
			do
			{
				if (this.currentToken.Id == MimeTokenId.None && this.state != MimeReaderState.Start)
				{
					this.ParseAndCheckSize();
				}
				this.HandleTokenInPushMode();
			}
			while (this.state != MimeReaderState.End);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000173D4 File Offset: 0x000155D4
		private void HandleTokenInPushMode()
		{
			if (MimeReader.StateIsOneOf(this.state, MimeReaderState.PartBody | MimeReaderState.InlineBody) && !this.skipPart && (this.currentToken.Id == MimeTokenId.PartData || (this.state == MimeReaderState.InlineBody && (this.currentToken.Id == MimeTokenId.InlineStart || this.currentToken.Id == MimeTokenId.InlineEnd))))
			{
				this.handler.PartContent(this.data, this.dataOffset, (int)this.currentToken.Length);
			}
			if (this.RunStateMachine())
			{
				MimeReaderState mimeReaderState = this.state;
				if (mimeReaderState > MimeReaderState.PartBody)
				{
					if (mimeReaderState <= MimeReaderState.InlineStart)
					{
						if (mimeReaderState != MimeReaderState.PartEnd)
						{
							if (mimeReaderState != MimeReaderState.InlineStart)
							{
								return;
							}
							goto IL_10F;
						}
					}
					else
					{
						if (mimeReaderState == MimeReaderState.InlineBody)
						{
							return;
						}
						if (mimeReaderState != MimeReaderState.InlineEnd)
						{
							if (mimeReaderState != MimeReaderState.End)
							{
								return;
							}
							this.handler.EndOfFile();
							return;
						}
					}
					this.handler.PartEnd();
					return;
				}
				if (mimeReaderState <= MimeReaderState.HeaderComplete)
				{
					switch (mimeReaderState)
					{
					case MimeReaderState.PartStart:
						break;
					case MimeReaderState.Start | MimeReaderState.PartStart:
						return;
					case MimeReaderState.HeaderStart:
					{
						if (this.skipHeaders)
						{
							this.skipHeader = true;
							return;
						}
						HeaderParseOptionInternal headerParseOptionInternal;
						this.handler.HeaderStart(this.currentHeaderId, this.currentHeaderName, out headerParseOptionInternal);
						this.skipHeader = (headerParseOptionInternal == HeaderParseOptionInternal.Skip);
						if (!this.skipHeader)
						{
							this.createHeader = true;
							return;
						}
						return;
					}
					default:
						if (mimeReaderState != MimeReaderState.HeaderComplete)
						{
							return;
						}
						if (this.currentHeader != null && !this.skipHeader)
						{
							this.handler.Header(this.currentHeader);
							return;
						}
						return;
					}
				}
				else
				{
					if (mimeReaderState == MimeReaderState.EndOfHeaders)
					{
						goto IL_275;
					}
					if (mimeReaderState != MimeReaderState.PartBody)
					{
						return;
					}
					return;
				}
				IL_10F:
				this.skipPart = false;
				this.skipHeaders = false;
				PartParseOptionInternal partParseOptionInternal;
				Stream stream;
				this.handler.PartStart(this.state == MimeReaderState.InlineStart, (this.state == MimeReaderState.InlineStart) ? this.InlineFileName : null, out partParseOptionInternal, out stream);
				if (stream != null)
				{
					if (this.outerContentStream != null)
					{
						throw new NotSupportedException(Strings.MimeHandlerErrorMoreThanOneOuterContentPushStream);
					}
					this.outerContentStream = stream;
					this.outerContentDepth = this.depth;
				}
				if (partParseOptionInternal == PartParseOptionInternal.Skip)
				{
					this.skipPart = true;
					this.parser.SetStreamMode();
					this.state = ((this.state == MimeReaderState.InlineStart) ? MimeReaderState.InlineBody : MimeReaderState.PartBody);
				}
				else if (partParseOptionInternal == PartParseOptionInternal.ParseSkipHeaders)
				{
					this.skipHeaders = true;
				}
				else if (partParseOptionInternal == PartParseOptionInternal.ParseRawOuterContent)
				{
					this.parser.SetStreamMode();
					this.state = ((this.state == MimeReaderState.InlineStart) ? MimeReaderState.InlineBody : MimeReaderState.PartBody);
				}
				if (this.skipPart || this.state != MimeReaderState.InlineStart)
				{
					return;
				}
				IL_275:
				PartContentParseOptionInternal partContentParseOptionInternal;
				this.handler.EndOfHeaders(this.parser.IsMime ? this.currentPartContentType : ((this.state == MimeReaderState.InlineStart) ? "application/octet-stream" : "text/plain"), this.parser.TransferEncoding, out partContentParseOptionInternal);
				if (partContentParseOptionInternal == PartContentParseOptionInternal.Skip)
				{
					if (this.state != MimeReaderState.InlineStart)
					{
						this.parser.SetContentType(MajorContentType.Other, default(MimeString));
					}
					this.state = ((this.state == MimeReaderState.InlineStart) ? MimeReaderState.InlineBody : MimeReaderState.PartBody);
					this.skipPart = true;
					return;
				}
				if (partContentParseOptionInternal == PartContentParseOptionInternal.ParseRawContent)
				{
					if (this.state != MimeReaderState.InlineStart)
					{
						this.parser.SetContentType(MajorContentType.Other, default(MimeString));
					}
					this.state = ((this.state == MimeReaderState.InlineStart) ? MimeReaderState.InlineBody : MimeReaderState.PartBody);
					return;
				}
				if (partContentParseOptionInternal == PartContentParseOptionInternal.ParseEmbeddedMessage)
				{
					if (this.currentPartMajorType != MajorContentType.MessageRfc822)
					{
						throw new NotSupportedException(Strings.MimeHandlerErrorNotEmbeddedMessage);
					}
				}
				else
				{
					if (this.currentPartMajorType == MajorContentType.MessageRfc822)
					{
						this.parser.SetContentType(MajorContentType.Other, default(MimeString));
					}
					if (this.currentPartMajorType != MajorContentType.Multipart || !this.parser.IsMime)
					{
						this.state = ((this.state == MimeReaderState.InlineStart) ? MimeReaderState.InlineBody : MimeReaderState.PartBody);
						return;
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000177B8 File Offset: 0x000159B8
		private bool TrySkipToNextPartBoundary(bool stopAtPartEnd)
		{
			while (this.state != MimeReaderState.End)
			{
				if (!this.TryReachNextState())
				{
					return false;
				}
				if (MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart) || (stopAtPartEnd && MimeReader.StateIsOneOf(this.state, MimeReaderState.PartEnd | MimeReaderState.InlineEnd)))
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00017808 File Offset: 0x00015A08
		internal bool TryReachNextState()
		{
			while (this.state != MimeReaderState.End)
			{
				if (this.currentToken.Id == MimeTokenId.None)
				{
					if (this.state == MimeReaderState.Start)
					{
						this.RunStateMachine();
						break;
					}
					this.ParseAndCheckSize();
					if (this.currentToken.Id == MimeTokenId.None)
					{
						if (this.mimeStream == null || !this.ReadMoreData())
						{
							this.dataExhausted = true;
							return false;
						}
						continue;
					}
				}
				if (this.RunStateMachine())
				{
					break;
				}
			}
			this.dataExhausted = false;
			return true;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00017880 File Offset: 0x00015A80
		private bool RunStateMachine()
		{
			MimeReaderState mimeReaderState = this.state;
			if (mimeReaderState <= MimeReaderState.PartBody)
			{
				if (mimeReaderState <= MimeReaderState.HeaderComplete)
				{
					switch (mimeReaderState)
					{
					case MimeReaderState.Start:
						this.depth++;
						this.StartPart();
						this.state = MimeReaderState.PartStart;
						return true;
					case MimeReaderState.PartStart:
						break;
					case MimeReaderState.Start | MimeReaderState.PartStart:
						goto IL_57D;
					case MimeReaderState.HeaderStart:
						this.CreateHeader();
						this.ContinueHeader();
						if (this.parser.IsHeaderComplete)
						{
							this.EndHeader();
							this.ConsumeCurrentToken();
							this.state = MimeReaderState.HeaderComplete;
							return true;
						}
						this.ConsumeCurrentToken();
						this.state = MimeReaderState.HeaderIncomplete;
						return false;
					default:
						if (mimeReaderState != MimeReaderState.HeaderIncomplete)
						{
							if (mimeReaderState != MimeReaderState.HeaderComplete)
							{
								goto IL_57D;
							}
						}
						else
						{
							if (this.currentToken.Id != MimeTokenId.HeaderContinuation)
							{
								this.EndHeader();
								this.state = MimeReaderState.HeaderComplete;
								return true;
							}
							this.ContinueHeader();
							this.ConsumeCurrentToken();
							if (this.parser.IsHeaderComplete)
							{
								this.EndHeader();
								this.state = MimeReaderState.HeaderComplete;
								return true;
							}
							return false;
						}
						break;
					}
					if (this.currentToken.Id == MimeTokenId.Header)
					{
						this.StartHeader();
						this.state = MimeReaderState.HeaderStart;
						return true;
					}
					this.ConsumeCurrentToken();
					this.state = MimeReaderState.EndOfHeaders;
					return true;
				}
				else
				{
					if (mimeReaderState != MimeReaderState.EndOfHeaders)
					{
						if (mimeReaderState != MimeReaderState.PartPrologue)
						{
							if (mimeReaderState != MimeReaderState.PartBody)
							{
								goto IL_57D;
							}
							if (this.currentToken.Id == MimeTokenId.PartData)
							{
								this.ConsumeCurrentToken();
								return false;
							}
							this.EndPart();
							this.state = MimeReaderState.PartEnd;
							return true;
						}
					}
					else if (this.currentToken.Id == MimeTokenId.EmbeddedStart)
					{
						this.ConsumeCurrentToken();
						if (this.parseEmbeddedMessages)
						{
							this.depth++;
							this.embeddedDepth++;
							this.StartPart();
							this.state = MimeReaderState.PartStart;
							return true;
						}
						this.state = MimeReaderState.Embedded;
						return true;
					}
					else
					{
						if (this.currentToken.Id == MimeTokenId.NestedStart)
						{
							this.depth++;
							this.StartPart();
							this.ConsumeCurrentToken();
							this.state = MimeReaderState.PartStart;
							return true;
						}
						if (this.currentToken.Id != MimeTokenId.PartData)
						{
							this.EndPart();
							this.state = MimeReaderState.PartEnd;
							return true;
						}
						if (this.parser.ContentType != MajorContentType.Multipart)
						{
							this.state = MimeReaderState.PartBody;
							return true;
						}
						this.state = MimeReaderState.PartPrologue;
					}
					if (this.currentToken.Id == MimeTokenId.NestedStart)
					{
						this.depth++;
						this.StartPart();
						this.ConsumeCurrentToken();
						this.state = MimeReaderState.PartStart;
						return true;
					}
					if (this.currentToken.Id == MimeTokenId.PartData)
					{
						this.ConsumeCurrentToken();
						return false;
					}
					this.EndPart();
					this.state = MimeReaderState.PartEnd;
					return true;
				}
			}
			else if (mimeReaderState <= MimeReaderState.InlineStart)
			{
				if (mimeReaderState != MimeReaderState.PartEpilogue)
				{
					if (mimeReaderState != MimeReaderState.PartEnd)
					{
						if (mimeReaderState == MimeReaderState.InlineStart)
						{
							this.state = MimeReaderState.InlineBody;
							return true;
						}
					}
					else
					{
						if (this.currentToken.Id == MimeTokenId.NestedNext)
						{
							this.StartPart();
							this.ConsumeCurrentToken();
							this.state = MimeReaderState.PartStart;
							return true;
						}
						if (this.currentToken.Id == MimeTokenId.NestedEnd)
						{
							this.ConsumeCurrentToken();
							this.depth--;
							this.state = MimeReaderState.PartEpilogue;
							return false;
						}
						if (this.currentToken.Id == MimeTokenId.InlineStart)
						{
							this.StartPart();
							this.ParseInlineFileName();
							this.state = MimeReaderState.InlineStart;
							return true;
						}
						if (this.currentToken.Id == MimeTokenId.EmbeddedEnd && this.parseEmbeddedMessages)
						{
							this.ConsumeCurrentToken();
							this.depth--;
							this.embeddedDepth--;
							this.EndPart();
							return true;
						}
						this.depth--;
						this.state = MimeReaderState.End;
						return true;
					}
				}
				else
				{
					if (this.currentToken.Id == MimeTokenId.PartData)
					{
						this.ConsumeCurrentToken();
						return false;
					}
					this.EndPart();
					this.state = MimeReaderState.PartEnd;
					return true;
				}
			}
			else
			{
				if (mimeReaderState <= MimeReaderState.InlineEnd)
				{
					if (mimeReaderState != MimeReaderState.InlineBody)
					{
						if (mimeReaderState != MimeReaderState.InlineEnd)
						{
							goto IL_57D;
						}
						this.depth--;
						this.state = MimeReaderState.InlineJunk;
					}
					else
					{
						if (this.currentToken.Id == MimeTokenId.InlineEnd)
						{
							this.ConsumeCurrentToken();
							this.EndPart();
							this.state = MimeReaderState.InlineEnd;
							return true;
						}
						this.ConsumeCurrentToken();
						return false;
					}
				}
				else if (mimeReaderState != MimeReaderState.InlineJunk)
				{
					if (mimeReaderState != MimeReaderState.EmbeddedEnd)
					{
						goto IL_57D;
					}
					if (this.currentToken.Id == MimeTokenId.EmbeddedEnd)
					{
						if (this.cleanupDepth == 0)
						{
							this.ConsumeCurrentToken();
							this.EndPart();
							this.state = MimeReaderState.PartEnd;
							return true;
						}
						this.cleanupDepth--;
					}
					else if (this.currentToken.Id == MimeTokenId.InlineStart || this.currentToken.Id == MimeTokenId.NestedStart)
					{
						this.cleanupDepth++;
					}
					else if (this.currentToken.Id == MimeTokenId.InlineEnd || this.currentToken.Id == MimeTokenId.NestedEnd)
					{
						this.cleanupDepth--;
					}
					this.ConsumeCurrentToken();
					return false;
				}
				if (this.currentToken.Id == MimeTokenId.PartData)
				{
					this.ConsumeCurrentToken();
					return false;
				}
				if (this.currentToken.Id == MimeTokenId.InlineStart)
				{
					this.depth++;
					this.StartPart();
					this.ParseInlineFileName();
					this.state = MimeReaderState.InlineStart;
					return true;
				}
				if (this.currentToken.Id == MimeTokenId.EmbeddedEnd && this.parseEmbeddedMessages)
				{
					this.ConsumeCurrentToken();
					this.EndPart();
					this.state = MimeReaderState.PartEnd;
					return true;
				}
				this.state = MimeReaderState.End;
				return true;
			}
			IL_57D:
			throw new InvalidOperationException();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00017E14 File Offset: 0x00016014
		private void ConsumeCurrentToken()
		{
			if (this.currentToken.Length != 0)
			{
				if (this.outerContentStream != null)
				{
					this.outerContentStream.Write(this.data, this.dataOffset, (int)this.currentToken.Length);
				}
				this.currentLineTerminationState = MimeCommon.AdvanceLineTerminationState(this.currentLineTerminationState, this.data, this.dataOffset, (int)this.currentToken.Length);
				this.parser.ReportConsumedData((int)this.currentToken.Length);
				this.dataOffset += (int)this.currentToken.Length;
				this.dataCount -= (int)this.currentToken.Length;
				this.currentToken.Length = 0;
			}
			this.currentToken.Id = MimeTokenId.None;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00017EE4 File Offset: 0x000160E4
		private void StartPart()
		{
			this.CheckPartsLimit();
			this.currentPartMajorType = MajorContentType.Other;
			this.currentPartContentType = null;
			this.currentPartContentTransferEncoding = ContentTransferEncoding.SevenBit;
			this.enableReadingOuterContent = false;
			this.currentHeader = null;
			this.createHeader = false;
			this.inlineFileName = default(MimeString);
			this.decoder = null;
			this.contentStream = null;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00017F3B File Offset: 0x0001613B
		private void EndPart()
		{
			if (this.outerContentStream != null && this.depth == this.outerContentDepth)
			{
				this.outerContentStream.Flush();
				this.outerContentStream = null;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00017F68 File Offset: 0x00016168
		private void ParseInlineFileName()
		{
			this.currentPartContentType = "application/octet-stream";
			this.currentPartContentTransferEncoding = this.parser.InlineFormat;
			if (this.parser.InlineFormat == ContentTransferEncoding.UUEncode)
			{
				int num = this.dataOffset + 6;
				while (num < this.dataOffset + (int)this.currentToken.Length && this.data[num] >= 48 && this.data[num] <= 55)
				{
					num++;
				}
				num += MimeScan.SkipLwsp(this.data, num, this.dataOffset + (int)this.currentToken.Length - num);
				int count = this.TrimEndOfLine(num, this.dataOffset + (int)this.currentToken.Length - num);
				this.inlineFileName = new MimeString(this.data, num, count);
				return;
			}
			this.inlineFileName = default(MimeString);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00018040 File Offset: 0x00016240
		private void StartHeader()
		{
			if (++this.headerCount > this.MimeLimits.MaxHeaders)
			{
				throw new MimeException(Strings.TooManyHeaders(this.headerCount, this.MimeLimits.MaxHeaders));
			}
			this.currentHeaderId = ((this.parser.HeaderNameLength == 0) ? HeaderId.Unknown : Header.GetHeaderId(this.data, this.dataOffset, this.parser.HeaderNameLength));
			this.currentHeaderName = ((this.parser.HeaderNameLength == 0) ? null : ByteString.BytesToString(this.data, this.dataOffset, this.parser.HeaderNameLength, false));
			this.currentHeader = null;
			bool flag = false;
			if (this.currentHeaderId == HeaderId.ContentType || this.currentHeaderId == HeaderId.ContentTransferEncoding || this.currentHeaderId == HeaderId.MimeVersion)
			{
				flag = true;
			}
			else if (this.MimeLimits.MaxAddressItemsPerHeader < 2147483647 || this.MimeLimits.MaxParametersPerHeader < 2147483647 || this.MimeLimits.MaxTextValueBytesPerValue < 2147483647)
			{
				Type left = Header.TypeFromHeaderId(this.currentHeaderId);
				if ((left == typeof(AddressHeader) && (this.MimeLimits.MaxParametersPerHeader < 2147483647 || this.MimeLimits.MaxAddressItemsPerHeader < 2147483647)) || ((left == typeof(ContentTypeHeader) || left == typeof(ContentDispositionHeader)) && (this.MimeLimits.MaxParametersPerHeader < 2147483647 || this.MimeLimits.MaxAddressItemsPerHeader < 2147483647)))
				{
					flag = true;
				}
			}
			this.createHeader = flag;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000181E8 File Offset: 0x000163E8
		private void CreateHeader()
		{
			if (this.createHeader && this.parser.HeaderNameLength != 0)
			{
				if (this.currentHeaderId != HeaderId.Unknown)
				{
					this.currentHeader = Header.Create(this.currentHeaderName, this.currentHeaderId);
				}
				else
				{
					this.currentHeader = Header.CreateGeneralHeader(this.currentHeaderName);
				}
			}
			else
			{
				this.currentHeader = null;
			}
			this.currentHeaderEmpty = true;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001824C File Offset: 0x0001644C
		private void ContinueHeader()
		{
			this.CheckHeaderBytesLimits();
			if (this.currentHeader != null)
			{
				int num = this.dataOffset + this.parser.HeaderDataOffset;
				int num2 = (int)this.currentToken.Length - this.parser.HeaderDataOffset;
				num2 = this.TrimEndOfLine(num, num2);
				if (this.currentHeaderEmpty)
				{
					int num3 = MimeScan.SkipLwsp(this.data, num, num2);
					num += num3;
					num2 -= num3;
				}
				if (num2 != 0)
				{
					this.currentHeaderEmpty = false;
					this.currentHeader.AppendLine(MimeString.CopyData(this.data, num, num2), false);
				}
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000182DC File Offset: 0x000164DC
		private void EndHeader()
		{
			if (this.currentHeader != null)
			{
				if (this.currentHeader is ComplexHeader)
				{
					if (this.MimeLimits.MaxParametersPerHeader < 2147483647 || this.MimeLimits.MaxTextValueBytesPerValue < 2147483647)
					{
						this.currentHeader.CheckChildrenLimit(this.MimeLimits.MaxParametersPerHeader, this.MimeLimits.MaxTextValueBytesPerValue);
					}
				}
				else if (this.currentHeader is AddressHeader && (this.MimeLimits.MaxAddressItemsPerHeader < 2147483647 || this.MimeLimits.MaxTextValueBytesPerValue < 2147483647))
				{
					this.currentHeader.CheckChildrenLimit(this.MimeLimits.MaxAddressItemsPerHeader, this.MimeLimits.MaxTextValueBytesPerValue);
				}
				if (this.currentHeader.HeaderId == HeaderId.MimeVersion && this.PartDepth == 1)
				{
					this.parser.SetMIME();
					return;
				}
				if (this.currentHeader.HeaderId == HeaderId.ContentTransferEncoding)
				{
					if (this.inferMime && this.PartDepth == 1)
					{
						this.parser.SetMIME();
					}
					ContentTransferEncoding encodingType = MimePart.GetEncodingType(this.currentHeader.FirstRawToken);
					this.parser.SetTransferEncoding(encodingType);
					this.currentPartContentTransferEncoding = encodingType;
					return;
				}
				if (this.currentHeader.HeaderId == HeaderId.ContentType)
				{
					MajorContentType majorContentType = MajorContentType.Other;
					string text = null;
					MimeString boundaryValue = default(MimeString);
					ContentTypeHeader contentTypeHeader = this.currentHeader as ContentTypeHeader;
					if (contentTypeHeader != null)
					{
						if (contentTypeHeader.IsMultipart)
						{
							MimeParameter mimeParameter = contentTypeHeader["boundary"];
							if (mimeParameter != null)
							{
								byte[] rawValue = mimeParameter.RawValue;
								int num = 0;
								if (rawValue != null && (num = rawValue.Length) != 0)
								{
									while (num != 0 && MimeScan.IsLWSP(rawValue[num - 1]))
									{
										num--;
									}
									if (num != 0 && num <= 994)
									{
										if (this.FixBadMimeBoundary)
										{
											int num2 = 0;
											if (num == rawValue.Length && num <= 70)
											{
												while (num2 < num && MimeScan.IsBChar(rawValue[num2]))
												{
													num2++;
												}
											}
											if (num2 != num)
											{
												this.parser.ComplianceStatus |= MimeComplianceStatus.InvalidBoundary;
												mimeParameter.RawValue = ContentTypeHeader.CreateBoundary();
											}
										}
										majorContentType = MajorContentType.Multipart;
										boundaryValue = new MimeString(rawValue, 0, num);
									}
								}
								if (rawValue == null || num == 0 || num > 994)
								{
									this.parser.ComplianceStatus |= MimeComplianceStatus.InvalidBoundary;
									contentTypeHeader.Value = "text/plain";
								}
							}
							else
							{
								this.parser.ComplianceStatus |= MimeComplianceStatus.MissingBoundaryParameter;
								contentTypeHeader.Value = "text/plain";
							}
						}
						else if (contentTypeHeader.IsEmbeddedMessage)
						{
							majorContentType = MajorContentType.MessageRfc822;
						}
						else if (contentTypeHeader.IsAnyMessage)
						{
							majorContentType = MajorContentType.Message;
						}
						text = contentTypeHeader.Value;
					}
					if (this.inferMime && this.PartDepth == 1)
					{
						this.parser.SetMIME();
					}
					this.currentPartMajorType = majorContentType;
					this.currentPartContentType = text;
					if (majorContentType == MajorContentType.Multipart || this.parseEmbeddedMessages)
					{
						this.parser.SetContentType(majorContentType, boundaryValue);
						return;
					}
					this.parser.SetContentType(MajorContentType.Other, default(MimeString));
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000185DC File Offset: 0x000167DC
		internal bool ReadPartData(byte[] buffer, int offset, int count, out int readCount)
		{
			readCount = 0;
			this.dataExhausted = false;
			bool flag = true;
			while (count != 0)
			{
				if (this.currentToken.Id == MimeTokenId.None)
				{
					this.ParseAndCheckSize();
					if (this.currentToken.Id == MimeTokenId.None)
					{
						if (this.mimeStream == null || !this.ReadMoreData())
						{
							this.dataExhausted = true;
							return false;
						}
						continue;
					}
				}
				int num;
				if ((this.currentToken.Id != MimeTokenId.PartData && this.currentToken.Id != MimeTokenId.InlineStart && this.currentToken.Id != MimeTokenId.InlineEnd) || (this.state == MimeReaderState.PartBody && this.currentToken.Id == MimeTokenId.InlineStart))
				{
					if (this.decoder != null)
					{
						int num2;
						this.decoder.Convert(this.data, 0, 0, buffer, offset, count, true, out num, out num2, out flag);
						count -= num2;
						offset += num2;
						readCount += num2;
						if (!flag)
						{
							break;
						}
					}
					this.EndPart();
					this.state = MimeReaderState.PartEnd;
					break;
				}
				if (this.decoder != null)
				{
					int num2;
					this.decoder.Convert(this.data, this.dataOffset, (int)this.currentToken.Length, buffer, offset, count, this.currentToken.Id == MimeTokenId.InlineEnd, out num, out num2, out flag);
					count -= num2;
					offset += num2;
					readCount += num2;
				}
				else
				{
					int num2 = num = Math.Min(count, (int)this.currentToken.Length);
					if (num2 != 0)
					{
						if (buffer != null)
						{
							Buffer.BlockCopy(this.data, this.dataOffset, buffer, offset, num2);
							count -= num2;
							offset += num2;
						}
						readCount += num2;
					}
				}
				if (num != 0)
				{
					if (this.outerContentStream != null)
					{
						this.outerContentStream.Write(this.data, this.dataOffset, num);
					}
					this.currentLineTerminationState = MimeCommon.AdvanceLineTerminationState(this.currentLineTerminationState, this.data, this.dataOffset, num);
					this.parser.ReportConsumedData(num);
					this.dataOffset += num;
					this.dataCount -= num;
					this.currentToken.Length = this.currentToken.Length - (short)num;
				}
				if (this.currentToken.Length != 0)
				{
					break;
				}
				if (this.currentToken.Id == MimeTokenId.InlineEnd)
				{
					if (flag)
					{
						this.EndPart();
						this.currentToken.Id = MimeTokenId.None;
						this.state = MimeReaderState.InlineEnd;
						break;
					}
					break;
				}
				else
				{
					this.currentToken.Id = MimeTokenId.None;
				}
			}
			return true;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00018832 File Offset: 0x00016A32
		internal static bool StateIsOneOf(MimeReaderState state, MimeReaderState set)
		{
			return (state & set) != (MimeReaderState)0;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00018840 File Offset: 0x00016A40
		internal void AssertGoodToUse(bool pullModeOnly, bool noEmbeddedReader)
		{
			if (this.parser == null)
			{
				throw new ObjectDisposedException("MimeReader");
			}
			if (pullModeOnly && this.mimeStream == null)
			{
				throw new ObjectDisposedException("MimeReader");
			}
			if (noEmbeddedReader && this.childReader != null)
			{
				throw new InvalidOperationException(Strings.EmbeddedMessageReaderNeedsToBeClosedFirst);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001888C File Offset: 0x00016A8C
		internal void SetEofMeansEndOfFile(bool eofMeansEndOfFile)
		{
			this.eofMeansEndOfFile = eofMeansEndOfFile;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00018895 File Offset: 0x00016A95
		internal bool TryReadNextPart()
		{
			this.AssertGoodToUse(false, true);
			return this.TrySkipToNextPartBoundary(false) && MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000188BC File Offset: 0x00016ABC
		internal bool TryReadFirstChildPart()
		{
			this.AssertGoodToUse(false, true);
			if (this.state == MimeReaderState.InlineStart)
			{
				return false;
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.Start | MimeReaderState.EndOfHeaders | MimeReaderState.PartPrologue))
			{
				if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete))
				{
					throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
				}
				while (this.TryReachNextState())
				{
					if (this.state == MimeReaderState.EndOfHeaders)
					{
						goto IL_54;
					}
				}
				return false;
			}
			IL_54:
			return (this.state != MimeReaderState.EndOfHeaders || this.IsMultipart || (this.IsEmbeddedMessage && this.parseEmbeddedMessages)) && this.TrySkipToNextPartBoundary(true) && this.state == MimeReaderState.PartStart;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00018958 File Offset: 0x00016B58
		internal bool TryReadNextSiblingPart()
		{
			this.AssertGoodToUse(false, true);
			if (this.state == MimeReaderState.End)
			{
				return false;
			}
			if (MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete | MimeReaderState.EndOfHeaders))
			{
				this.createHeader = false;
				while (this.state != MimeReaderState.EndOfHeaders)
				{
					if (!this.TryReachNextState())
					{
						return false;
					}
				}
				this.parser.SetContentType(MajorContentType.Other, default(MimeString));
			}
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.Start | MimeReaderState.PartEnd | MimeReaderState.InlineEnd))
			{
				int num = this.depth;
				while (this.TrySkipToNextPartBoundary(true))
				{
					if (this.depth <= num && MimeReader.StateIsOneOf(this.state, MimeReaderState.PartEnd | MimeReaderState.InlineEnd))
					{
						goto IL_97;
					}
				}
				return false;
			}
			IL_97:
			return this.TrySkipToNextPartBoundary(true) && MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00018A18 File Offset: 0x00016C18
		internal HeaderList TryReadHeaderList()
		{
			this.AssertGoodToUse(false, true);
			if (!MimeReader.StateIsOneOf(this.state, MimeReaderState.PartStart | MimeReaderState.InlineStart) && (!MimeReader.StateIsOneOf(this.state, MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete) || this.headerList == null))
			{
				throw new InvalidOperationException(Strings.OperationNotValidInThisReaderState);
			}
			if (this.state == MimeReaderState.InlineStart)
			{
				return new HeaderList(null);
			}
			HeaderList headerList;
			if (this.headerList == null)
			{
				headerList = new HeaderList(null);
			}
			else
			{
				headerList = this.headerList;
				this.headerList = null;
			}
			while (this.TryReachNextState())
			{
				if (this.state == MimeReaderState.HeaderStart)
				{
					this.createHeader = true;
				}
				else if (this.state == MimeReaderState.HeaderComplete && this.currentHeader != null)
				{
					headerList.InternalAppendChild(this.currentHeader);
				}
				if (this.state == MimeReaderState.EndOfHeaders)
				{
					return headerList;
				}
			}
			this.headerList = headerList;
			return null;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00018AE1 File Offset: 0x00016CE1
		internal Stream TryGetRawContentReadStream()
		{
			this.AssertGoodToUse(false, true);
			if (!this.TryInitializeReadContent(false))
			{
				return null;
			}
			this.contentStream = new MimeReader.ContentReadStream(this);
			return this.contentStream;
		}

		// Token: 0x040002F9 RID: 761
		private const int DataBufferSize = 5120;

		// Token: 0x040002FA RID: 762
		private bool FixBadMimeBoundary;

		// Token: 0x040002FB RID: 763
		private Stream mimeStream;

		// Token: 0x040002FC RID: 764
		private IMimeHandlerInternal handler;

		// Token: 0x040002FD RID: 765
		private bool inferMime;

		// Token: 0x040002FE RID: 766
		private bool parseEmbeddedMessages;

		// Token: 0x040002FF RID: 767
		private DecodingOptions decodingOptions;

		// Token: 0x04000300 RID: 768
		private MimeLimits limits;

		// Token: 0x04000301 RID: 769
		private MimeParser parser;

		// Token: 0x04000302 RID: 770
		private MimeReaderState state;

		// Token: 0x04000303 RID: 771
		private int depth;

		// Token: 0x04000304 RID: 772
		private int cleanupDepth;

		// Token: 0x04000305 RID: 773
		private int embeddedDepth;

		// Token: 0x04000306 RID: 774
		private bool dataExhausted;

		// Token: 0x04000307 RID: 775
		private bool dataEOF;

		// Token: 0x04000308 RID: 776
		private byte[] data;

		// Token: 0x04000309 RID: 777
		private int dataOffset;

		// Token: 0x0400030A RID: 778
		private int dataCount;

		// Token: 0x0400030B RID: 779
		private MimeToken currentToken;

		// Token: 0x0400030C RID: 780
		private HeaderId currentHeaderId;

		// Token: 0x0400030D RID: 781
		private string currentHeaderName;

		// Token: 0x0400030E RID: 782
		private bool createHeader;

		// Token: 0x0400030F RID: 783
		private Header currentHeader;

		// Token: 0x04000310 RID: 784
		private bool currentHeaderEmpty;

		// Token: 0x04000311 RID: 785
		private bool currentHeaderConsumed;

		// Token: 0x04000312 RID: 786
		private bool currentChildConsumed;

		// Token: 0x04000313 RID: 787
		private MimeNode currentChild;

		// Token: 0x04000314 RID: 788
		private MimeNode currentGrandChild;

		// Token: 0x04000315 RID: 789
		private MajorContentType currentPartMajorType;

		// Token: 0x04000316 RID: 790
		private string currentPartContentType;

		// Token: 0x04000317 RID: 791
		private ContentTransferEncoding currentPartContentTransferEncoding;

		// Token: 0x04000318 RID: 792
		private LineTerminationState currentLineTerminationState;

		// Token: 0x04000319 RID: 793
		private MimeString inlineFileName;

		// Token: 0x0400031A RID: 794
		private ByteEncoder decoder;

		// Token: 0x0400031B RID: 795
		private bool readRawData;

		// Token: 0x0400031C RID: 796
		private Stream contentStream;

		// Token: 0x0400031D RID: 797
		private bool enableReadingOuterContent;

		// Token: 0x0400031E RID: 798
		private Stream outerContentStream;

		// Token: 0x0400031F RID: 799
		private int outerContentDepth;

		// Token: 0x04000320 RID: 800
		private MimeReader childReader;

		// Token: 0x04000321 RID: 801
		private MimeReader parentReader;

		// Token: 0x04000322 RID: 802
		private int partCount;

		// Token: 0x04000323 RID: 803
		private int headerCount;

		// Token: 0x04000324 RID: 804
		private int cumulativeHeaderBytes;

		// Token: 0x04000325 RID: 805
		private int currentTextHeaderBytes;

		// Token: 0x04000326 RID: 806
		private bool skipPart;

		// Token: 0x04000327 RID: 807
		private bool skipHeaders;

		// Token: 0x04000328 RID: 808
		private bool skipHeader;

		// Token: 0x04000329 RID: 809
		private HeaderList headerList;

		// Token: 0x0400032A RID: 810
		private bool eofMeansEndOfFile;

		// Token: 0x0200006B RID: 107
		private class ContentReadStream : Stream
		{
			// Token: 0x06000426 RID: 1062 RVA: 0x00018B08 File Offset: 0x00016D08
			public ContentReadStream(MimeReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x00018B17 File Offset: 0x00016D17
			public override bool CanRead
			{
				get
				{
					return this.reader != null;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000428 RID: 1064 RVA: 0x00018B25 File Offset: 0x00016D25
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x00018B28 File Offset: 0x00016D28
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x00018B2B File Offset: 0x00016D2B
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00018B32 File Offset: 0x00016D32
			// (set) Token: 0x0600042C RID: 1068 RVA: 0x00018B39 File Offset: 0x00016D39
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

			// Token: 0x0600042D RID: 1069 RVA: 0x00018B40 File Offset: 0x00016D40
			public override int Read(byte[] buffer, int offset, int count)
			{
				MimeCommon.CheckBufferArguments(buffer, offset, count);
				if (this.reader.contentStream != this)
				{
					throw new NotSupportedException(Strings.StreamNoLongerValid);
				}
				if (MimeReader.StateIsOneOf(this.reader.state, MimeReaderState.PartBody | MimeReaderState.InlineBody))
				{
					int result;
					this.reader.ReadPartData(buffer, offset, count, out result);
					return result;
				}
				if (MimeReader.StateIsOneOf(this.reader.state, MimeReaderState.PartEnd | MimeReaderState.InlineEnd))
				{
					return 0;
				}
				throw new NotSupportedException(Strings.StreamNoLongerValid);
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00018BBB File Offset: 0x00016DBB
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x00018BC2 File Offset: 0x00016DC2
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x00018BC9 File Offset: 0x00016DC9
			public override void Flush()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x00018BD0 File Offset: 0x00016DD0
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x00018BD7 File Offset: 0x00016DD7
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
			}

			// Token: 0x0400032B RID: 811
			private MimeReader reader;
		}
	}
}
