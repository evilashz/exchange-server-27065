using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000045 RID: 69
	public class MimeDocument : IDisposable
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000DC56 File Offset: 0x0000BE56
		public MimeDocument() : this(DecodingOptions.Default, MimeLimits.Default)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public MimeDocument(DecodingOptions headerDecodingOptions, MimeLimits mimeLimits)
		{
			if (mimeLimits == null)
			{
				throw new ArgumentNullException("mimeLimits");
			}
			this.decodingOptions = headerDecodingOptions;
			this.limits = mimeLimits;
			this.accessToken = new MimeDocument.MimeDocumentThreadAccessToken(this);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000DCCC File Offset: 0x0000BECC
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public MimeDocument.EndOfHeadersCallback EndOfHeaders
		{
			get
			{
				this.ThrowIfDisposed();
				MimeDocument.EndOfHeadersCallback result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.eohCallback;
				}
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.eohCallback = value;
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000DD54 File Offset: 0x0000BF54
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000DD98 File Offset: 0x0000BF98
		public MimePart RootPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.root;
				}
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (value.Parent != null)
					{
						throw new ArgumentException(Strings.RootPartCantHaveAParent);
					}
					this.ThrowIfReadOnly("MimeDocument.set_RootPart");
					if (this.reader != null)
					{
						throw new InvalidOperationException("Cannot set a new document root part while document loading is not complete");
					}
					this.lastPart = null;
					this.contentStart = 0L;
					this.complianceStatus = MimeComplianceStatus.Compliant;
					this.stopLoading = false;
					if (this.root != null)
					{
						this.root.ParentDocument = null;
					}
					this.root = value;
					this.root.ParentDocument = this;
					this.parsedSize = 0L;
					this.IncrementVersion();
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000DE64 File Offset: 0x0000C064
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public DecodingOptions HeaderDecodingOptions
		{
			get
			{
				this.ThrowIfDisposed();
				DecodingOptions result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.decodingOptions;
				}
				return result;
			}
			internal set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("MimeDocument.set_HeaderDecodingOptions");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.decodingOptions = value;
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		public MimeLimits MimeLimits
		{
			get
			{
				this.ThrowIfDisposed();
				MimeLimits result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.limits;
				}
				return result;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000DF3C File Offset: 0x0000C13C
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000DF80 File Offset: 0x0000C180
		public MimeComplianceMode ComplianceMode
		{
			get
			{
				this.ThrowIfDisposed();
				MimeComplianceMode result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.complianceMode;
				}
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("MimeDocument.set_ComplianceMode");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.complianceMode = value;
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		public MimeComplianceStatus ComplianceStatus
		{
			get
			{
				this.ThrowIfDisposed();
				MimeComplianceStatus result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.complianceStatus;
				}
				return result;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000E014 File Offset: 0x0000C214
		public bool RequiresSMTPUTF8
		{
			get
			{
				this.ThrowIfDisposed();
				bool requiresSMTPUTF;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					requiresSMTPUTF = this.root.RequiresSMTPUTF8;
				}
				return requiresSMTPUTF;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000E05C File Offset: 0x0000C25C
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000E064 File Offset: 0x0000C264
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000E06B File Offset: 0x0000C26B
		internal static bool FixMimeForTestUseOnly
		{
			get
			{
				return MimeDocument.fixMime;
			}
			set
			{
				MimeDocument.fixMime = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E073 File Offset: 0x0000C273
		internal ObjectThreadAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000E07B File Offset: 0x0000C27B
		internal bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000E084 File Offset: 0x0000C284
		internal DecodingOptions EffectiveHeaderDecodingOptions
		{
			get
			{
				this.ThrowIfDisposed();
				DecodingOptions result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					DecodingOptions decodingOptions = this.decodingOptions;
					Charset charset = this.GetMimeTreeCharset();
					if (charset != null)
					{
						decodingOptions.Charset = charset;
					}
					result = decodingOptions;
				}
				return result;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		internal EncodingOptions EncodingOptions
		{
			get
			{
				this.ThrowIfDisposed();
				EncodingOptions result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.encodingOptions == null)
					{
						this.encodingOptions = new EncodingOptions(this.GetMimeTreeCharset());
					}
					result = this.encodingOptions;
				}
				return result;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000E138 File Offset: 0x0000C338
		internal bool CreateValidateStorage
		{
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.createValidateStorage = value;
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000E174 File Offset: 0x0000C374
		internal long Position
		{
			get
			{
				long streamOffset;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					streamOffset = this.reader.StreamOffset;
				}
				return streamOffset;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		internal long ParsedSize
		{
			get
			{
				long result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.parsedSize;
				}
				return result;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		private bool CreateDomObjects
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPrivate(this.accessToken))
				{
					result = (this.loadEmbeddedMessages || this.lastPart == null || !this.lastPart.IsEmbeddedMessage);
				}
				return result;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E250 File Offset: 0x0000C450
		public MimePart Load(Stream stream, CachingMode cachingMode)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("MimeDocument.Load");
			MimePart rootPart;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				if (this.root != null)
				{
					throw new InvalidOperationException(Strings.CannotLoadIntoNonEmptyDocument);
				}
				if (this.reader != null)
				{
					throw new InvalidOperationException("Cannot load document again while previous load is not complete");
				}
				switch (cachingMode)
				{
				case CachingMode.Copy:
				{
					this.InitializePushMode(true);
					bool discard = true;
					try
					{
						byte[] array = new byte[4096];
						while (!this.stopLoading)
						{
							int num = stream.Read(array, 0, array.Length);
							if (num == 0)
							{
								break;
							}
							this.Write(array, 0, num);
						}
						discard = false;
						goto IL_17B;
					}
					finally
					{
						this.Flush(discard);
					}
					break;
				}
				case CachingMode.Source:
				case CachingMode.SourceTakeOwnership:
					if (this.createValidateStorage)
					{
						if (!stream.CanSeek)
						{
							throw new NotSupportedException(Strings.CachingModeSourceButStreamCannotSeek);
						}
						stream.Position = 0L;
						this.backingStorage = new ReadableDataStorageOnStream(stream, cachingMode == CachingMode.SourceTakeOwnership);
					}
					this.reader = new MimeReader(stream, true, this.decodingOptions, this.limits, true, true, this.expectBinaryContent);
					this.reader.DangerousSetFixBadMimeBoundary(this.dangerousFixBadMimeBoundary);
					try
					{
						this.BuildDom(null, 0, 0, true);
					}
					finally
					{
						this.reader.DisconnectInputStream();
					}
					this.parsedSize = this.reader.StreamOffset;
					this.reader.Dispose();
					this.reader = null;
					if (this.backingStorage != null)
					{
						this.backingStorage.Release();
						this.backingStorage = null;
						goto IL_17B;
					}
					goto IL_17B;
				}
				throw new ArgumentException("Invalid Caching Mode value", "cachingMode");
				IL_17B:
				rootPart = this.RootPart;
			}
			return rootPart;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E43C File Offset: 0x0000C63C
		public Stream GetLoadStream()
		{
			return this.GetLoadStream(true);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E448 File Offset: 0x0000C648
		public MimeDocument Clone()
		{
			this.ThrowIfDisposed();
			MimeDocument result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.reader != null)
				{
					throw new NotSupportedException(Strings.DocumentCloneNotSupportedInThisState);
				}
				MimeDocument mimeDocument = (MimeDocument)base.MemberwiseClone();
				if (this.root != null)
				{
					mimeDocument.root = (MimePart)this.root.Clone();
					mimeDocument.root.ParentDocument = mimeDocument;
				}
				mimeDocument.contentPositionStack = null;
				mimeDocument.lastPart = null;
				result = mimeDocument;
			}
			return result;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public long WriteTo(Stream stream)
		{
			this.ThrowIfDisposed();
			long result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (Stream.Null != stream || this.cachedSizeVersion != this.version)
				{
					this.cachedSizeVersion = this.version;
					this.cachedSize = ((this.root == null) ? 0L : this.root.WriteTo(stream, this.EncodingOptions, null));
				}
				result = this.cachedSize;
			}
			return result;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E56C File Offset: 0x0000C76C
		public long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter)
		{
			this.ThrowIfDisposed();
			long result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.root != null)
				{
					if (encodingOptions == null)
					{
						encodingOptions = this.EncodingOptions;
					}
					result = this.root.WriteTo(stream, encodingOptions, filter);
				}
				else
				{
					result = 0L;
				}
			}
			return result;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E5DF File Offset: 0x0000C7DF
		internal void DangerousSetFixBadMimeBoundary(bool value)
		{
			if (this.reader != null)
			{
				throw new InvalidOperationException("Cannot change FixBadMimeBoundary flag while previous load is not complete");
			}
			this.dangerousFixBadMimeBoundary = value;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		internal Stream GetLoadStream(bool expectBinaryContent)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("MimeDocument.GetLoadStream");
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.root != null)
				{
					throw new InvalidOperationException(Strings.CannotLoadIntoNonEmptyDocument);
				}
				if (this.reader != null)
				{
					throw new InvalidOperationException(Strings.CannotGetLoadStreamMoreThanOnce);
				}
				this.expectBinaryContent = expectBinaryContent;
				this.InitializePushMode(this.expectBinaryContent);
				result = new MimeDocument.PushStream(this);
			}
			return result;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E684 File Offset: 0x0000C884
		internal void SetReadOnly(bool makeReadOnly)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (makeReadOnly != this.isReadOnly)
				{
					if (makeReadOnly)
					{
						this.CompleteParse();
					}
					this.SetReadOnlyInternal(makeReadOnly);
				}
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		internal void CompleteParse()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.BuildDomAndCompleteParse(this.RootPart);
				EncodingOptions encodingOptions = this.EncodingOptions;
				this.GetMimeTreeCharset();
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E72C File Offset: 0x0000C92C
		internal void SetReadOnlyInternal(bool makeReadOnly)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.isReadOnly = makeReadOnly;
				using (MimePart.SubtreeEnumerator enumerator = this.root.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages, false))
				{
					while (enumerator.MoveNext())
					{
						MimePart mimePart = enumerator.Current;
						mimePart.SetReadOnlyInternal(makeReadOnly);
					}
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
		internal void IncrementVersion()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.version = ((int.MaxValue == this.version) ? 1 : (this.version + 1));
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E808 File Offset: 0x0000CA08
		internal void BuildEmbeddedDom(MimePart part)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("MimeDocument.BuildEmbeddedDom");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.reader == null && !this.stopLoading)
				{
					MimePart mimePart = this.root;
					bool flag = this.loadEmbeddedMessages;
					MimeDocument.EndOfHeadersCallback endOfHeadersCallback = this.eohCallback;
					this.eohCallback = null;
					this.loadEmbeddedMessages = true;
					this.reader = new MimeReader(null, true, this.decodingOptions, this.limits, true, true, this.expectBinaryContent);
					this.reader.DangerousSetFixBadMimeBoundary(this.dangerousFixBadMimeBoundary);
					try
					{
						using (MimePart.SubtreeEnumerator enumerator = part.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages, true))
						{
							while (enumerator.MoveNext())
							{
								MimePart mimePart2 = enumerator.Current;
								if (mimePart2.InternalLastChild == null && mimePart2.Storage != null && mimePart2.IsEmbeddedMessage)
								{
									this.ParseOnePart(mimePart2);
									this.root.ParentDocument = null;
									mimePart2.InternalInsertAfter(this.root, null);
								}
							}
						}
					}
					finally
					{
						this.reader.DisconnectInputStream();
						this.reader.Dispose();
						this.reader = null;
						if (this.backingStorage != null)
						{
							this.backingStorage.Release();
							this.backingStorage = null;
						}
						this.backingStorageOffset = 0L;
						this.root = mimePart;
						this.loadEmbeddedMessages = flag;
						this.eohCallback = endOfHeadersCallback;
					}
				}
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		internal void BuildDomAndCompleteParse(MimePart rootPart)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.reader != null)
				{
					throw new InvalidOperationException("do not call BuildDomAndCompleteParse() before Load is complete");
				}
				if (this.stopLoading)
				{
					throw new InvalidOperationException("do not call BuildDomAndCompleteParse() after canceling Load");
				}
				if (rootPart.InternalLastChild == null && rootPart.Storage == null)
				{
					this.ParseAllHeaders(rootPart);
				}
				else
				{
					MimePart mimePart = this.root;
					bool flag = this.loadEmbeddedMessages;
					bool flag2 = this.parseCompletely;
					MimeDocument.EndOfHeadersCallback endOfHeadersCallback = this.eohCallback;
					this.eohCallback = null;
					this.loadEmbeddedMessages = true;
					this.parseCompletely = true;
					this.reader = new MimeReader(null, true, this.decodingOptions, this.limits, true, true, this.expectBinaryContent);
					this.reader.DangerousSetFixBadMimeBoundary(this.dangerousFixBadMimeBoundary);
					try
					{
						Stack<MimePart> stack = new Stack<MimePart>(5);
						stack.Push(rootPart);
						while (stack.Count > 0)
						{
							MimePart mimePart2 = stack.Pop();
							do
							{
								MimeNode internalLastChild = mimePart2.InternalLastChild;
								this.ParseAllHeaders(mimePart2);
								MimePart mimePart3 = mimePart2.FirstChild as MimePart;
								if (mimePart3 != null)
								{
									stack.Push(mimePart3);
								}
								mimePart2 = (mimePart2.NextSibling as MimePart);
							}
							while (mimePart2 != null);
						}
					}
					finally
					{
						this.reader.DisconnectInputStream();
						this.reader.Dispose();
						this.reader = null;
						if (this.backingStorage != null)
						{
							this.backingStorage.Release();
							this.backingStorage = null;
						}
						this.backingStorageOffset = 0L;
						this.root = mimePart;
						this.loadEmbeddedMessages = flag;
						this.parseCompletely = flag2;
						this.eohCallback = endOfHeadersCallback;
					}
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000EB88 File Offset: 0x0000CD88
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				if (this.backingStorageWriteStream != null)
				{
					this.backingStorageWriteStream.Dispose();
					this.backingStorageWriteStream = null;
				}
				if (this.backingStorage != null)
				{
					this.backingStorage.Release();
					this.backingStorage = null;
				}
				if (this.reader != null)
				{
					this.reader.Dispose();
					this.reader = null;
				}
				if (this.root != null)
				{
					this.root.Dispose();
					this.root = null;
				}
			}
			this.isDisposed = true;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000EC10 File Offset: 0x0000CE10
		private static bool IsContentBinary(Stream stream, int bytesToExamine, int thresholdPercentage)
		{
			int i = 0;
			int num = 0;
			byte[] array = new byte[bytesToExamine];
			while (i < array.Length)
			{
				int num2 = stream.Read(array, i, array.Length - i);
				if (num2 == 0)
				{
					break;
				}
				i += num2;
			}
			if (i < 1)
			{
				return false;
			}
			for (int j = 0; j < i; j++)
			{
				if ((array[j] & 128) != 0)
				{
					num++;
				}
			}
			int num3 = num * 100 / i;
			return num3 >= thresholdPercentage;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		private Charset GetMimeTreeCharset()
		{
			this.ThrowIfDisposed();
			Charset result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.searchMimeTreeCharset)
				{
					this.searchMimeTreeCharset = false;
					if (this.root != null)
					{
						this.mimeTreeCharset = this.root.FindMimeTreeCharset();
					}
				}
				result = this.mimeTreeCharset;
			}
			return result;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		private void ParseOnePart(MimePart nextPart)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.root = null;
				try
				{
					this.backingStorage = nextPart.Storage;
					this.backingStorage.AddRef();
					this.backingStorageOffset = nextPart.DataStart + nextPart.BodyOffset;
					Stream rawContentReadStream;
					Stream stream = rawContentReadStream = nextPart.GetRawContentReadStream();
					try
					{
						this.reader.Reset(stream);
						this.BuildDom(null, 0, 0, true, nextPart.IsEmbeddedMessage);
					}
					finally
					{
						if (rawContentReadStream != null)
						{
							((IDisposable)rawContentReadStream).Dispose();
						}
					}
				}
				finally
				{
					this.backingStorage.Release();
					this.backingStorage = null;
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		private void Flush(bool discard)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("MimeDocument.Flush");
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.reader != null)
				{
					if (!discard)
					{
						this.backingStorageWriteStream.Flush();
						if (!this.stopLoading)
						{
							this.BuildDom(null, 0, 0, true);
						}
					}
					this.parsedSize = this.reader.StreamOffset;
					this.reader.Dispose();
					this.reader = null;
					this.backingStorageWriteStream.Dispose();
					this.backingStorageWriteStream = null;
					this.backingStorage.Release();
					this.backingStorage = null;
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000EE64 File Offset: 0x0000D064
		private void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("MimeDocument.Write");
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.reader == null)
				{
					throw new InvalidOperationException(Strings.CannotWriteAfterFlush);
				}
				if (count != 0)
				{
					this.backingStorageWriteStream.Write(buffer, offset, count);
					if (!this.stopLoading)
					{
						this.BuildDom(buffer, offset, count, false);
					}
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		private void InitializePushMode(bool expectBinaryContent)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
				this.backingStorage = temporaryDataStorage;
				this.backingStorageWriteStream = temporaryDataStorage.OpenWriteStream(true);
				this.reader = new MimeReader(null, true, this.decodingOptions, this.limits, true, true, expectBinaryContent);
				this.reader.DangerousSetFixBadMimeBoundary(this.dangerousFixBadMimeBoundary);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000EF60 File Offset: 0x0000D160
		private void BuildDom(byte[] buffer, int offset, int length, bool eof)
		{
			this.BuildDom(buffer, offset, length, eof, false);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000EF70 File Offset: 0x0000D170
		private void BuildDom(byte[] buffer, int offset, int length, bool eof, bool parseHeaders)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				while (!this.reader.EndOfFile && (!this.reader.DataExhausted || length != 0 || eof) && !this.stopLoading)
				{
					if (this.reader.DataExhausted)
					{
						int num = this.reader.AddMoreData(buffer, offset, length, eof);
						offset += num;
						length -= num;
					}
					bool flag = this.reader.TryReachNextState();
					if (flag)
					{
						MimeComplianceStatus mimeComplianceStatus = this.reader.ComplianceStatus;
						MimeReaderState readerState = this.reader.ReaderState;
						if (readerState <= MimeReaderState.PartBody)
						{
							if (readerState <= MimeReaderState.HeaderComplete)
							{
								switch (readerState)
								{
								case MimeReaderState.PartStart:
									this.StartPart(false);
									goto IL_191;
								case MimeReaderState.Start | MimeReaderState.PartStart:
									goto IL_186;
								case MimeReaderState.HeaderStart:
									if (!this.reader.TryCompleteCurrentHeader(this.embeddedMessagePartDepth == 0 || this.CreateDomObjects))
									{
										goto IL_191;
									}
									break;
								default:
									if (readerState != MimeReaderState.HeaderComplete)
									{
										goto IL_186;
									}
									break;
								}
								if (this.embeddedMessagePartDepth == 0 || this.CreateDomObjects)
								{
									Header currentHeaderObject = this.reader.CurrentHeaderObject;
									if (currentHeaderObject != null)
									{
										this.lastPart.Headers.InternalAppendChild(currentHeaderObject);
									}
								}
							}
							else if (readerState != MimeReaderState.EndOfHeaders)
							{
								if (readerState != MimeReaderState.PartBody)
								{
									goto IL_186;
								}
							}
							else
							{
								this.EndPartHeaders();
							}
						}
						else if (readerState <= MimeReaderState.InlineStart)
						{
							if (readerState != MimeReaderState.PartEnd)
							{
								if (readerState != MimeReaderState.InlineStart)
								{
									goto IL_186;
								}
								this.StartPart(true);
							}
							else
							{
								mimeComplianceStatus = this.CompletePart(false, parseHeaders);
							}
						}
						else if (readerState != MimeReaderState.InlineBody)
						{
							if (readerState != MimeReaderState.InlineEnd)
							{
								if (readerState != MimeReaderState.End)
								{
									goto IL_186;
								}
							}
							else
							{
								mimeComplianceStatus = this.CompletePart(true, parseHeaders);
							}
						}
						IL_191:
						if (this.reader.ReaderState == MimeReaderState.PartBody)
						{
							continue;
						}
						this.complianceStatus |= mimeComplianceStatus;
						if (this.ComplianceMode == MimeComplianceMode.Strict && this.ComplianceStatus != MimeComplianceStatus.Compliant)
						{
							throw new MimeException(Strings.StrictComplianceViolation);
						}
						continue;
						IL_186:
						throw new InvalidOperationException("unexpected reader state");
					}
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		private void StartPart(bool inline)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.CreateDomObjects)
				{
					MimePart mimePart = new MimePart();
					if (this.root == null)
					{
						this.root = mimePart;
						mimePart.ParentDocument = this;
					}
					else
					{
						if (inline)
						{
							Header header;
							Header header2;
							if (!this.root.IsMultipart)
							{
								MimePart mimePart2 = (this.lastPart == null) ? this.root : ((MimePart)this.lastPart.FirstChild);
								MimePart mimePart3 = new MimePart();
								header = mimePart2.Headers.FindFirst(HeaderId.ContentType);
								header2 = mimePart2.Headers.FindFirst(HeaderId.ContentTransferEncoding);
								if (header != null)
								{
									mimePart2.Headers.InternalRemoveChild(header);
									mimePart3.Headers.InternalAppendChild(header);
								}
								if (header2 != null)
								{
									mimePart2.Headers.InternalRemoveChild(header2);
									mimePart3.Headers.InternalAppendChild(header2);
								}
								Header header3 = new AsciiTextHeader("X-ConvertedToMime", HeaderId.Unknown);
								header3.RawValue = MimeString.ConvertedToMimeUU;
								mimePart2.Headers.InternalAppendChild(header3);
								DataStorage storage = mimePart2.Storage;
								mimePart3.SetStorageImpl(storage, mimePart2.DataStart + mimePart2.BodyOffset, mimePart2.DataEnd, 0L, mimePart2.BodyCte, mimePart2.BodyLineTermination);
								mimePart2.SetStorageImpl(null, 0L, 0L);
								header = new ContentTypeHeader("multipart/mixed");
								mimePart2.Headers.InternalInsertAfter(header, null);
								mimePart2.InternalAppendChild(mimePart3);
								this.lastPart = mimePart2;
								if (this.eohCallback != null)
								{
									bool flag;
									this.eohCallback(mimePart3, out flag);
									if (flag)
									{
										this.stopLoading = true;
									}
								}
							}
							string value = this.reader.InlineFileName;
							if (string.IsNullOrEmpty(value))
							{
								value = "unnamed.dat";
							}
							header2 = Header.Create(HeaderId.ContentTransferEncoding);
							header2.RawValue = MimeString.Uuencode;
							mimePart.Headers.InternalAppendChild(header2);
							Header header4 = new ContentDispositionHeader("attachment");
							MimeParameter newChild = new MimeParameter("filename", value);
							header4.InternalAppendChild(newChild);
							mimePart.Headers.InternalAppendChild(header4);
							header = new ContentTypeHeader("application/octet-stream");
							newChild = new MimeParameter("name", value);
							header.InternalAppendChild(newChild);
							mimePart.Headers.InternalAppendChild(header);
							if (this.eohCallback != null)
							{
								bool flag2;
								this.eohCallback(mimePart, out flag2);
								if (flag2)
								{
									this.stopLoading = true;
								}
							}
						}
						this.lastPart.InternalInsertAfter(mimePart, this.lastPart.InternalLastChild);
					}
					if (this.contentPositionStack == null)
					{
						this.contentPositionStack = new MimeDocument.ContentPositionEntry[4];
					}
					else if (this.contentPositionStack.Length == this.contentPositionStackTop)
					{
						MimeDocument.ContentPositionEntry[] destinationArray = new MimeDocument.ContentPositionEntry[this.contentPositionStack.Length * 2];
						Array.Copy(this.contentPositionStack, 0, destinationArray, 0, this.contentPositionStackTop);
						this.contentPositionStack = destinationArray;
					}
					this.contentPositionStack[this.contentPositionStackTop++] = new MimeDocument.ContentPositionEntry(this.contentStart, this.headersEnd, this.contentTransferEncoding);
					this.lastPart = mimePart;
					this.contentStart = (this.headersEnd = this.reader.StreamOffset);
					this.contentTransferEncoding = (inline ? this.reader.ContentTransferEncoding : ContentTransferEncoding.Unknown);
				}
				else if (this.embeddedMessagePartDepth == 0)
				{
					this.embeddedMessagePartDepth = this.reader.Depth;
				}
				this.complianceStatus |= this.reader.ComplianceStatus;
				this.reader.ResetComplianceStatus();
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F548 File Offset: 0x0000D748
		private void EndPartHeaders()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.complianceStatus |= this.reader.ComplianceStatus;
				this.reader.ResetComplianceStatus();
				if (this.embeddedMessagePartDepth == 0 || this.CreateDomObjects)
				{
					this.headersEnd = this.reader.StreamOffset;
					this.contentTransferEncoding = this.reader.ContentTransferEncoding;
					if (MimeDocument.fixMime)
					{
						if ((this.lastPart == this.root || ((MimePart)this.lastPart.Parent).IsEmbeddedMessage) && this.lastPart.Headers.FindFirst(HeaderId.MimeVersion) == null)
						{
							Header header = Header.Create(HeaderId.MimeVersion);
							header.RawValue = MimeString.Version1;
							this.lastPart.Headers.InternalAppendChild(header);
						}
						if (this.lastPart.Headers.FindFirst(HeaderId.ContentType) == null)
						{
							bool flag = false;
							MimePart mimePart = this.lastPart.Parent as MimePart;
							if (mimePart != null && mimePart.Headers.FindFirst(HeaderId.ContentType).Value == "multipart/digest")
							{
								flag = true;
							}
							ContentTypeHeader newChild = new ContentTypeHeader(flag ? "message/rfc822" : "text/plain");
							this.lastPart.Headers.InternalAppendChild(newChild);
						}
					}
					if (this.parseCompletely)
					{
						this.ParseAllHeaders(this.lastPart);
					}
					if (this.eohCallback != null)
					{
						bool flag2;
						this.eohCallback(this.lastPart, out flag2);
						if (flag2)
						{
							this.stopLoading = true;
						}
					}
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		private void ParseAllHeaders(MimePart part)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				foreach (Header header in part.Headers)
				{
					header.ForceParse();
				}
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F770 File Offset: 0x0000D970
		private MimeComplianceStatus CompletePart(bool inline, bool parseHeaders)
		{
			MimeComplianceStatus result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimeComplianceStatus mimeComplianceStatus = this.reader.ComplianceStatus;
				if (this.embeddedMessagePartDepth == 0 || this.CreateDomObjects)
				{
					if (this.createValidateStorage)
					{
						if (parseHeaders)
						{
							this.ParseAllHeaders(this.lastPart);
						}
						this.lastPart.SetStorageImpl(this.backingStorage, this.contentStart + this.backingStorageOffset, this.reader.StreamOffset + this.backingStorageOffset, this.headersEnd - this.contentStart, this.contentTransferEncoding, this.reader.LineTerminationState);
						MimeComplianceStatus mimeComplianceStatus2 = MimeComplianceStatus.InvalidWrapping | MimeComplianceStatus.BareLinefeedInBody | MimeComplianceStatus.UnexpectedBinaryContent;
						if (MimeDocument.fixMime && (mimeComplianceStatus2 & mimeComplianceStatus) != MimeComplianceStatus.Compliant && this.FixPartContent())
						{
							mimeComplianceStatus &= ~mimeComplianceStatus2;
						}
					}
					ContentTypeHeader contentTypeHeader = this.lastPart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					if (contentTypeHeader != null && contentTypeHeader.IsMultipart && !this.lastPart.HasChildren)
					{
						contentTypeHeader.RawValue = MimeString.TextPlain;
					}
					this.lastPart = (this.lastPart.Parent as MimePart);
					this.contentPositionStackTop--;
					this.contentStart = this.contentPositionStack[this.contentPositionStackTop].ContentStart;
					this.headersEnd = this.contentPositionStack[this.contentPositionStackTop].HeadersEnd;
					this.contentTransferEncoding = this.contentPositionStack[this.contentPositionStackTop].ContentTransferEncoding;
				}
				if (0 < this.embeddedMessagePartDepth && this.embeddedMessagePartDepth == this.reader.Depth)
				{
					this.embeddedMessagePartDepth = 0;
				}
				this.reader.ResetComplianceStatus();
				result = mimeComplianceStatus;
			}
			return result;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000F93C File Offset: 0x0000DB3C
		private bool FixPartContent()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				ContentTypeHeader contentTypeHeader = this.lastPart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
				if (contentTypeHeader.IsMultipart || contentTypeHeader.MediaType == "message")
				{
					result = false;
				}
				else if (this.lastPart.BodyCte == ContentTransferEncoding.Unknown)
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.lastPart;
					for (;;)
					{
						MimePart mimePart2 = mimePart.Parent as MimePart;
						if (mimePart2 != null)
						{
							if (mimePart == mimePart2.FirstChild)
							{
								ContentTypeHeader contentTypeHeader2 = mimePart2.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
								if (contentTypeHeader2 != null && contentTypeHeader2.Value == "multipart/signed")
								{
									break;
								}
							}
							Header header = mimePart2.Headers.FindFirst("DKIM-Signature");
							if (header != null)
							{
								goto Block_9;
							}
						}
						mimePart = mimePart2;
						if (mimePart == null)
						{
							goto Block_10;
						}
					}
					return false;
					Block_9:
					return false;
					Block_10:
					Header header2 = this.lastPart.Headers.FindFirst(HeaderId.ContentTransferEncoding);
					if (header2 == null)
					{
						header2 = Header.Create(HeaderId.ContentTransferEncoding);
						this.lastPart.Headers.InternalAppendChild(header2);
						header2.RawValue = MimeString.QuotedPrintable;
						result = true;
					}
					else
					{
						ContentTransferEncoding encodingType = MimePart.GetEncodingType(header2.FirstRawToken);
						switch (encodingType)
						{
						case ContentTransferEncoding.Unknown:
						case ContentTransferEncoding.SevenBit:
						case ContentTransferEncoding.EightBit:
							header2.RawValue = MimeString.QuotedPrintable;
							return true;
						case ContentTransferEncoding.QuotedPrintable:
							this.ForceReencoding(encodingType);
							return true;
						case ContentTransferEncoding.Base64:
						{
							bool flag = false;
							if ((this.reader.ComplianceStatus & MimeComplianceStatus.UnexpectedBinaryContent) != MimeComplianceStatus.Compliant)
							{
								long num = this.lastPart.DataStart + this.lastPart.BodyOffset;
								long num2 = Math.Min(this.lastPart.DataEnd - num, 1000L);
								long end = num + num2;
								if (num2 > 10L)
								{
									using (Stream stream = this.lastPart.Storage.OpenReadStream(num, end))
									{
										flag = MimeDocument.IsContentBinary(stream, (int)num2, 10);
									}
								}
							}
							if (flag)
							{
								this.RepairBrokenExchangeMime(encodingType);
							}
							else
							{
								this.ForceReencoding(encodingType);
							}
							return true;
						}
						}
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FB98 File Offset: 0x0000DD98
		private void RepairBrokenExchangeMime(ContentTransferEncoding encoding)
		{
			MimeDocument.EncodingDataStorage encodingDataStorage = new MimeDocument.EncodingDataStorage(this.lastPart.Storage, this.lastPart.DataStart + this.lastPart.BodyOffset, this.lastPart.DataEnd, encoding);
			this.lastPart.SetStorageImpl(encodingDataStorage, 0L, long.MaxValue, 0L, encoding, LineTerminationState.CRLF);
			encodingDataStorage.Release();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		private void ForceReencoding(ContentTransferEncoding encoding)
		{
			MimeDocument.DecodingDataStorage decodingDataStorage = new MimeDocument.DecodingDataStorage(this.lastPart.Storage, this.lastPart.DataStart + this.lastPart.BodyOffset, this.lastPart.DataEnd, encoding);
			this.lastPart.SetStorageImpl(decodingDataStorage, 0L, long.MaxValue, 0L, ContentTransferEncoding.Binary, LineTerminationState.CRLF);
			decodingDataStorage.Release();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000FC5E File Offset: 0x0000DE5E
		private void ThrowIfDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("MimeDocument");
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FC73 File Offset: 0x0000DE73
		private void ThrowIfReadOnly(string method)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyMimeException(method);
			}
		}

		// Token: 0x04000230 RID: 560
		private static bool fixMime = true;

		// Token: 0x04000231 RID: 561
		private bool dangerousFixBadMimeBoundary = true;

		// Token: 0x04000232 RID: 562
		private MimeDocument.MimeDocumentThreadAccessToken accessToken;

		// Token: 0x04000233 RID: 563
		private DataStorage backingStorage;

		// Token: 0x04000234 RID: 564
		private long backingStorageOffset;

		// Token: 0x04000235 RID: 565
		private Stream backingStorageWriteStream;

		// Token: 0x04000236 RID: 566
		private MimePart root;

		// Token: 0x04000237 RID: 567
		private MimeComplianceMode complianceMode;

		// Token: 0x04000238 RID: 568
		private MimeComplianceStatus complianceStatus;

		// Token: 0x04000239 RID: 569
		private DecodingOptions decodingOptions = DecodingOptions.Default;

		// Token: 0x0400023A RID: 570
		private EncodingOptions encodingOptions;

		// Token: 0x0400023B RID: 571
		private Charset mimeTreeCharset;

		// Token: 0x0400023C RID: 572
		private bool searchMimeTreeCharset = true;

		// Token: 0x0400023D RID: 573
		private MimeReader reader;

		// Token: 0x0400023E RID: 574
		private MimeLimits limits;

		// Token: 0x0400023F RID: 575
		private long contentStart;

		// Token: 0x04000240 RID: 576
		private long headersEnd;

		// Token: 0x04000241 RID: 577
		private ContentTransferEncoding contentTransferEncoding;

		// Token: 0x04000242 RID: 578
		private MimeDocument.ContentPositionEntry[] contentPositionStack;

		// Token: 0x04000243 RID: 579
		private int contentPositionStackTop;

		// Token: 0x04000244 RID: 580
		private MimePart lastPart;

		// Token: 0x04000245 RID: 581
		private long parsedSize;

		// Token: 0x04000246 RID: 582
		private MimeDocument.EndOfHeadersCallback eohCallback;

		// Token: 0x04000247 RID: 583
		private int version;

		// Token: 0x04000248 RID: 584
		private bool createValidateStorage = true;

		// Token: 0x04000249 RID: 585
		private bool stopLoading;

		// Token: 0x0400024A RID: 586
		private bool loadEmbeddedMessages;

		// Token: 0x0400024B RID: 587
		private bool isDisposed;

		// Token: 0x0400024C RID: 588
		private bool isReadOnly;

		// Token: 0x0400024D RID: 589
		private bool parseCompletely;

		// Token: 0x0400024E RID: 590
		private int embeddedMessagePartDepth;

		// Token: 0x0400024F RID: 591
		private long cachedSize;

		// Token: 0x04000250 RID: 592
		private int cachedSizeVersion = -1;

		// Token: 0x04000251 RID: 593
		private bool expectBinaryContent;

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x060002A9 RID: 681
		public delegate void EndOfHeadersCallback(MimePart part, out bool stopLoading);

		// Token: 0x02000047 RID: 71
		private struct ContentPositionEntry
		{
			// Token: 0x060002AC RID: 684 RVA: 0x0000FC8C File Offset: 0x0000DE8C
			public ContentPositionEntry(long contentStart, long headersEnd, ContentTransferEncoding contentTransferEncoding)
			{
				this.ContentStart = contentStart;
				this.HeadersEnd = headersEnd;
				this.ContentTransferEncoding = contentTransferEncoding;
			}

			// Token: 0x04000252 RID: 594
			public long ContentStart;

			// Token: 0x04000253 RID: 595
			public long HeadersEnd;

			// Token: 0x04000254 RID: 596
			public ContentTransferEncoding ContentTransferEncoding;
		}

		// Token: 0x02000049 RID: 73
		private class MimeDocumentThreadAccessToken : ObjectThreadAccessToken
		{
			// Token: 0x060002AE RID: 686 RVA: 0x0000FCAB File Offset: 0x0000DEAB
			internal MimeDocumentThreadAccessToken(MimeDocument parent)
			{
			}
		}

		// Token: 0x0200004A RID: 74
		private class PushStream : Stream
		{
			// Token: 0x060002AF RID: 687 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
			public PushStream(MimeDocument document)
			{
				this.document = document;
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000FCC2 File Offset: 0x0000DEC2
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000FCC5 File Offset: 0x0000DEC5
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
			public override bool CanWrite
			{
				get
				{
					return this.document != null;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000FCD6 File Offset: 0x0000DED6
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000FCDD File Offset: 0x0000DEDD
			// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
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

			// Token: 0x060002B6 RID: 694 RVA: 0x0000FCEC File Offset: 0x0000DEEC
			public override void Flush()
			{
				if (this.document == null)
				{
					throw new ObjectDisposedException("stream");
				}
				using (ThreadAccessGuard.EnterPublic(this.document.AccessToken))
				{
					if (!this.badState)
					{
						if (this.document.stopLoading)
						{
							throw new InvalidOperationException(Strings.LoadingStopped);
						}
						this.badState = true;
						this.document.Flush(false);
						this.badState = false;
					}
				}
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x0000FD78 File Offset: 0x0000DF78
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0000FD7F File Offset: 0x0000DF7F
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000FD86 File Offset: 0x0000DF86
			public override void SetLength(long length)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000FD90 File Offset: 0x0000DF90
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.document == null)
				{
					throw new ObjectDisposedException("stream");
				}
				using (ThreadAccessGuard.EnterPublic(this.document.AccessToken))
				{
					if (!this.badState)
					{
						if (this.document.stopLoading)
						{
							throw new InvalidOperationException(Strings.LoadingStopped);
						}
						this.badState = true;
						this.document.Write(buffer, offset, count);
						this.badState = false;
					}
				}
			}

			// Token: 0x060002BB RID: 699 RVA: 0x0000FE1C File Offset: 0x0000E01C
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.document != null)
				{
					using (ThreadAccessGuard.EnterPublic(this.document.AccessToken))
					{
						this.document.Flush(this.badState);
						this.document = null;
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000255 RID: 597
			private MimeDocument document;

			// Token: 0x04000256 RID: 598
			private bool badState;
		}

		// Token: 0x0200004D RID: 77
		private class CodingDataStorage : DataStorage
		{
			// Token: 0x060002CB RID: 715 RVA: 0x00010029 File Offset: 0x0000E229
			public CodingDataStorage(DataStorage storage, long start, long end, ContentTransferEncoding cte, bool encode)
			{
				storage.AddRef();
				this.storage = storage;
				this.start = start;
				this.end = end;
				this.cte = cte;
				this.encode = encode;
			}

			// Token: 0x060002CC RID: 716 RVA: 0x0001005C File Offset: 0x0000E25C
			public override Stream OpenReadStream(long start, long end)
			{
				base.ThrowIfDisposed();
				start = this.start + start;
				end = ((end != long.MaxValue) ? (this.start + end) : this.end);
				ByteEncoder byteEncoder = this.encode ? MimeDocument.EncodingDataStorage.CreateEncoder(this.cte) : MimePart.CreateDecoder(this.cte);
				if (byteEncoder == null)
				{
					return this.storage.OpenReadStream(start, end);
				}
				return new EncoderStream(this.storage.OpenReadStream(start, end), byteEncoder, EncoderStreamAccess.Read, true);
			}

			// Token: 0x060002CD RID: 717 RVA: 0x000100DE File Offset: 0x0000E2DE
			protected override void Dispose(bool disposing)
			{
				if (disposing && !base.IsDisposed && this.storage != null)
				{
					this.storage.Release();
					this.storage = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x0400025B RID: 603
			private DataStorage storage;

			// Token: 0x0400025C RID: 604
			private long start;

			// Token: 0x0400025D RID: 605
			private long end;

			// Token: 0x0400025E RID: 606
			private ContentTransferEncoding cte;

			// Token: 0x0400025F RID: 607
			private bool encode;
		}

		// Token: 0x0200004E RID: 78
		private class EncodingDataStorage : MimeDocument.CodingDataStorage
		{
			// Token: 0x060002CE RID: 718 RVA: 0x0001010C File Offset: 0x0000E30C
			public EncodingDataStorage(DataStorage storage, long start, long end, ContentTransferEncoding cte) : base(storage, start, end, cte, true)
			{
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0001011C File Offset: 0x0000E31C
			internal static ByteEncoder CreateEncoder(ContentTransferEncoding encoding)
			{
				switch (encoding)
				{
				case ContentTransferEncoding.QuotedPrintable:
					return new QPEncoder();
				case ContentTransferEncoding.Base64:
					return new Base64Encoder();
				case ContentTransferEncoding.UUEncode:
					return new UUEncoder();
				default:
					return null;
				}
			}
		}

		// Token: 0x0200004F RID: 79
		private class DecodingDataStorage : MimeDocument.CodingDataStorage
		{
			// Token: 0x060002D0 RID: 720 RVA: 0x00010154 File Offset: 0x0000E354
			public DecodingDataStorage(DataStorage storage, long start, long end, ContentTransferEncoding cte) : base(storage, start, end, cte, false)
			{
			}
		}
	}
}
