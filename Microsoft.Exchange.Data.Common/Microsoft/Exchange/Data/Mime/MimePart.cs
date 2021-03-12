using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200005D RID: 93
	public class MimePart : MimeNode, IDisposable, IEnumerable<MimePart>, IEnumerable
	{
		// Token: 0x0600033B RID: 827 RVA: 0x000126F4 File Offset: 0x000108F4
		public MimePart()
		{
			this.accessToken = new MimePart.MimePartThreadAccessToken(this);
			this.headers = new HeaderList(this);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001272C File Offset: 0x0001092C
		public MimePart(string contentType) : this()
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.headers.InternalAppendChild(new ContentTypeHeader(contentType));
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00012788 File Offset: 0x00010988
		public MimePart(string contentType, string transferEncoding, Stream contentStream, CachingMode cachingMode) : this(contentType)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.SetContentStream(transferEncoding, contentStream, cachingMode);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000127D0 File Offset: 0x000109D0
		public MimePart(string contentType, ContentTransferEncoding transferEncoding, Stream contentStream, CachingMode cachingMode) : this(contentType)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.SetContentStream(transferEncoding, contentStream, cachingMode);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00012818 File Offset: 0x00010A18
		public HeaderList Headers
		{
			get
			{
				this.ThrowIfDisposed();
				HeaderList result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.headers;
				}
				return result;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0001285C File Offset: 0x00010A5C
		public string ContentType
		{
			get
			{
				this.ThrowIfDisposed();
				string result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					ContentTypeHeader contentTypeHeader = this.headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					if (contentTypeHeader != null)
					{
						result = contentTypeHeader.Value;
					}
					else
					{
						MimePart mimePart = base.Parent as MimePart;
						if (mimePart != null)
						{
							ContentTypeHeader contentTypeHeader2 = mimePart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
							if (contentTypeHeader2 != null && contentTypeHeader2.Value == "multipart/digest")
							{
								return "message/rfc822";
							}
						}
						result = "text/plain";
					}
				}
				return result;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000128FC File Offset: 0x00010AFC
		public bool IsMultipart
		{
			get
			{
				this.ThrowIfDisposed();
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					ContentTypeHeader contentTypeHeader = this.headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					result = (contentTypeHeader != null && contentTypeHeader.IsMultipart);
				}
				return result;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00012958 File Offset: 0x00010B58
		public bool IsEmbeddedMessage
		{
			get
			{
				this.ThrowIfDisposed();
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					ContentTypeHeader contentTypeHeader = this.headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					result = (contentTypeHeader != null && contentTypeHeader.IsEmbeddedMessage);
				}
				return result;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000129B4 File Offset: 0x00010BB4
		public bool RequiresSMTPUTF8
		{
			get
			{
				this.ThrowIfDisposed();
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					using (MimePart.SubtreeEnumerator enumerator = this.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages, true))
					{
						while (enumerator.MoveNext())
						{
							MimePart mimePart = enumerator.Current;
							foreach (Header header in mimePart.Headers)
							{
								if (header.RequiresSMTPUTF8)
								{
									return true;
								}
							}
						}
					}
					result = false;
				}
				return result;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00012A80 File Offset: 0x00010C80
		internal bool IsAnyMessage
		{
			get
			{
				this.ThrowIfDisposed();
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					ContentTypeHeader contentTypeHeader = this.headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					result = (contentTypeHeader != null && contentTypeHeader.IsAnyMessage);
				}
				return result;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00012ADC File Offset: 0x00010CDC
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00012B1C File Offset: 0x00010D1C
		internal int CacheMapStamp
		{
			get
			{
				int result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.cacheMapStamp;
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.cacheMapStamp = value;
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00012B58 File Offset: 0x00010D58
		public ContentTransferEncoding ContentTransferEncoding
		{
			get
			{
				this.ThrowIfDisposed();
				ContentTransferEncoding result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					Header header = this.headers.FindFirst(HeaderId.ContentTransferEncoding);
					if (header != null && header.FirstRawToken.Length != 0)
					{
						result = MimePart.GetEncodingType(header.FirstRawToken);
					}
					else
					{
						result = ContentTransferEncoding.SevenBit;
					}
				}
				return result;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00012BC8 File Offset: 0x00010DC8
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00012BD0 File Offset: 0x00010DD0
		internal ObjectThreadAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00012BD8 File Offset: 0x00010DD8
		internal ObjectThreadAccessToken ParentAccessToken
		{
			get
			{
				MimeNode mimeNode;
				MimeDocument mimeDocument = MimeNode.GetParentDocument(this, out mimeNode);
				if (mimeDocument == null)
				{
					return null;
				}
				return mimeDocument.AccessToken;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00012BFC File Offset: 0x00010DFC
		public MimePart.PartSubtree Subtree
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart.PartSubtree result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = new MimePart.PartSubtree(this);
				}
				return result;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00012C40 File Offset: 0x00010E40
		internal DataStorage Storage
		{
			get
			{
				DataStorage result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.dataStorage;
				}
				return result;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00012C80 File Offset: 0x00010E80
		internal long DataStart
		{
			get
			{
				long dataStart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					dataStart = this.storageInfo.DataStart;
				}
				return dataStart;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00012CC4 File Offset: 0x00010EC4
		internal long DataEnd
		{
			get
			{
				long dataEnd;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					dataEnd = this.storageInfo.DataEnd;
				}
				return dataEnd;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00012D08 File Offset: 0x00010F08
		internal long DataLength
		{
			get
			{
				long result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.dataStorage == null)
					{
						result = 0L;
					}
					else
					{
						if (!MimePart.IsEqualContentTransferEncoding(this.storageInfo.BodyCte, this.ContentTransferEncoding) || this.storageInfo.DataEnd == 9223372036854775807L)
						{
							using (Stream rawContentReadStream = this.GetRawContentReadStream())
							{
								if (rawContentReadStream.CanSeek)
								{
									return rawContentReadStream.Length;
								}
								byte[] array = new byte[4096];
								long num = 0L;
								int num2;
								while ((num2 = rawContentReadStream.Read(array, 0, array.Length)) != 0)
								{
									num += (long)num2;
								}
								return num;
							}
						}
						result = this.storageInfo.DataEnd - this.storageInfo.DataStart - this.storageInfo.BodyOffset;
					}
				}
				return result;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00012E00 File Offset: 0x00011000
		internal long BodyOffset
		{
			get
			{
				long bodyOffset;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bodyOffset = this.storageInfo.BodyOffset;
				}
				return bodyOffset;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00012E44 File Offset: 0x00011044
		internal ContentTransferEncoding BodyCte
		{
			get
			{
				ContentTransferEncoding bodyCte;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bodyCte = this.storageInfo.BodyCte;
				}
				return bodyCte;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00012E88 File Offset: 0x00011088
		internal LineTerminationState BodyLineTermination
		{
			get
			{
				LineTerminationState bodyLineTermination;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bodyLineTermination = this.storageInfo.BodyLineTermination;
				}
				return bodyLineTermination;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00012ECC File Offset: 0x000110CC
		internal byte[] Boundary
		{
			get
			{
				byte[] result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.boundary == null)
					{
						this.boundary = MimePart.GetBoundary(this.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader);
					}
					result = this.boundary;
				}
				return result;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00012F30 File Offset: 0x00011130
		internal bool IsSignedContent
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.Parent != null && this == base.Parent.FirstChild && this.dataStorage != null && 0L != this.storageInfo.BodyOffset && this.version == 0)
					{
						MimePart mimePart = base.Parent as MimePart;
						ContentTypeHeader contentTypeHeader = mimePart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
						result = (contentTypeHeader != null && contentTypeHeader.Value == "multipart/signed");
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00012FD4 File Offset: 0x000111D4
		internal bool IsProtectedContent
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.Parent != null && this.dataStorage != null && 0L != this.storageInfo.BodyOffset && this.version == 0)
					{
						MimePart mimePart = base.Parent as MimePart;
						Header header = mimePart.Headers.FindFirst("DKIM-Signature");
						result = (header != null);
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001305C File Offset: 0x0001125C
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00013064 File Offset: 0x00011264
		internal MimeDocument ParentDocument
		{
			get
			{
				return this.parentDocument;
			}
			set
			{
				base.ThrowIfReadOnly("MimePart.set_ParentDocument");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.parentDocument = value;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000130AC File Offset: 0x000112AC
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000130EC File Offset: 0x000112EC
		internal bool ContentDirty
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.contentDirty;
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.contentDirty != value)
					{
						base.ThrowIfReadOnly("MimePart.set_ContentDirty");
					}
					this.contentDirty = value;
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001313C File Offset: 0x0001133C
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00013194 File Offset: 0x00011394
		internal bool ContentPersisted
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = (this.contentPersisted && this.storageInfo.BodyCte == this.ContentTransferEncoding);
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.contentPersisted = value;
				}
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000131D0 File Offset: 0x000113D0
		public Stream GetRawContentReadStream()
		{
			this.ThrowIfDisposed();
			Stream rawContentReadStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				rawContentReadStream = this.GetRawContentReadStream(this.dataStorage, this.storageInfo);
			}
			return rawContentReadStream;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00013220 File Offset: 0x00011420
		internal Stream GetDeferredRawContentReadStream()
		{
			Stream rawContentReadStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (!base.IsReadOnly)
				{
					rawContentReadStream = this.GetRawContentReadStream();
				}
				else
				{
					DataStorage storage;
					MimePart.DataStorageInfo dataStorageInfo;
					lock (this.deferredStorageLock)
					{
						storage = this.deferredStorage;
						dataStorageInfo = this.deferredStorageInfo;
					}
					if (dataStorageInfo == null)
					{
						rawContentReadStream = this.GetRawContentReadStream();
					}
					else
					{
						rawContentReadStream = this.GetRawContentReadStream(storage, dataStorageInfo);
					}
				}
			}
			return rawContentReadStream;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000132B8 File Offset: 0x000114B8
		private Stream GetRawContentReadStream(DataStorage storage, MimePart.DataStorageInfo storageInfo)
		{
			this.ThrowIfDisposed();
			Stream result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Stream stream;
				if (!this.TryGetContentReadStream(storage, storageInfo, this.ContentTransferEncoding, out stream))
				{
					throw new MimeException(Strings.CannotDecodeContentStream);
				}
				result = stream;
			}
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00013314 File Offset: 0x00011514
		public Stream GetContentReadStream()
		{
			this.ThrowIfDisposed();
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				Stream stream;
				if (!this.TryGetContentReadStream(this.dataStorage, this.storageInfo, ContentTransferEncoding.Binary, out stream))
				{
					throw new MimeException(Strings.CannotDecodeContentStream);
				}
				result = stream;
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00013374 File Offset: 0x00011574
		public bool TryGetContentReadStream(out Stream result)
		{
			this.ThrowIfDisposed();
			bool result2;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result2 = this.TryGetContentReadStream(this.dataStorage, this.storageInfo, ContentTransferEncoding.Binary, out result);
			}
			return result2;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000133C8 File Offset: 0x000115C8
		private bool TryGetContentReadStream(DataStorage dataStorage, MimePart.DataStorageInfo storageInfo, ContentTransferEncoding desiredCte, out Stream result)
		{
			bool result2;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				result = null;
				ContentTransferEncoding contentTransferEncoding = storageInfo.BodyCte;
				if (contentTransferEncoding == ContentTransferEncoding.Unknown)
				{
					contentTransferEncoding = this.ContentTransferEncoding;
				}
				if (!MimePart.IsEqualContentTransferEncoding(desiredCte, contentTransferEncoding))
				{
					if (desiredCte == ContentTransferEncoding.Unknown)
					{
						return false;
					}
					if (contentTransferEncoding == ContentTransferEncoding.Unknown)
					{
						return false;
					}
					if (this.IsMultipart || this.IsAnyMessage)
					{
						contentTransferEncoding = ContentTransferEncoding.Binary;
						desiredCte = ContentTransferEncoding.Binary;
					}
				}
				bool flag = false;
				try
				{
					if (dataStorage == null)
					{
						if (this.IsMultipart && base.InternalLastChild != null)
						{
							return true;
						}
						if (!this.IsEmbeddedMessage || base.InternalLastChild == null)
						{
							result = DataStorage.NewEmptyReadStream();
							flag = true;
							return true;
						}
						TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
						try
						{
							using (Stream stream = temporaryDataStorage.OpenWriteStream(true))
							{
								base.FirstChild.WriteTo(stream);
							}
							result = temporaryDataStorage.OpenReadStream(0L, temporaryDataStorage.Length);
							goto IL_F2;
						}
						finally
						{
							temporaryDataStorage.Release();
						}
					}
					result = dataStorage.OpenReadStream(storageInfo.DataStart + storageInfo.BodyOffset, storageInfo.DataEnd);
					IL_F2:
					if (!MimePart.IsEqualContentTransferEncoding(desiredCte, contentTransferEncoding))
					{
						if (!MimePart.EncodingIsTransparent(contentTransferEncoding))
						{
							ByteEncoder encoder = MimePart.CreateDecoder(contentTransferEncoding);
							result = new EncoderStream(result, encoder, EncoderStreamAccess.Read, true);
						}
						if (!MimePart.EncodingIsTransparent(desiredCte))
						{
							ByteEncoder encoder2 = MimePart.CreateEncoder(result, desiredCte);
							result = new EncoderStream(result, encoder2, EncoderStreamAccess.Read, true);
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag && result != null)
					{
						result.Dispose();
						result = null;
					}
				}
				result2 = true;
			}
			return result2;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000135A4 File Offset: 0x000117A4
		public Stream GetRawContentWriteStream()
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.GetRawContentWriteStream");
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.IsMultipart)
				{
					throw new NotSupportedException(Strings.ModifyingRawContentOfMultipartNotSupported);
				}
				this.SetStorage(null, 0L, 0L);
				result = new MimePart.PartContentWriteStream(this, ContentTransferEncoding.Unknown);
			}
			return result;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013614 File Offset: 0x00011814
		public Stream GetContentWriteStream(ContentTransferEncoding transferEncoding)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.GetContentWriteStream");
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.IsMultipart)
				{
					throw new NotSupportedException(Strings.ModifyingRawContentOfMultipartNotSupported);
				}
				this.UpdateTransferEncoding(transferEncoding);
				this.SetStorage(null, 0L, 0L);
				result = new MimePart.PartContentWriteStream(this, ContentTransferEncoding.Binary);
			}
			return result;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00013688 File Offset: 0x00011888
		public Stream GetContentWriteStream(string transferEncoding)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.GetContentWriteStream");
			Stream contentWriteStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (transferEncoding == null)
				{
					throw new ArgumentNullException("transferEncoding");
				}
				contentWriteStream = this.GetContentWriteStream(MimePart.GetEncodingType(new MimeString(transferEncoding)));
			}
			return contentWriteStream;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000136F0 File Offset: 0x000118F0
		public void SetContentStream(string transferEncoding, Stream contentStream, CachingMode cachingMode)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.SetContentStream");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				ContentTransferEncoding contentTransferEncoding = ContentTransferEncoding.Unknown;
				if (transferEncoding != null)
				{
					contentTransferEncoding = MimePart.GetEncodingType(new MimeString(transferEncoding));
					if (contentTransferEncoding == ContentTransferEncoding.Unknown)
					{
						throw new ArgumentException("Transfer encoding is unknown or not supported", "transferEncoding");
					}
				}
				this.SetContentStream(contentTransferEncoding, contentStream, cachingMode);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00013764 File Offset: 0x00011964
		public void SetContentStream(ContentTransferEncoding transferEncoding, Stream contentStream, CachingMode cachingMode)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.SetContentStream");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (contentStream == null)
				{
					throw new ArgumentNullException("contentStream");
				}
				if (!contentStream.CanRead)
				{
					throw new ArgumentException(Strings.StreamMustSupportRead, "contentStream");
				}
				if (this.IsMultipart)
				{
					throw new NotSupportedException(Strings.ModifyingRawContentOfMultipartNotSupported);
				}
				DataStorage dataStorage = null;
				long dataStart = 0L;
				long dataEnd = long.MaxValue;
				if (transferEncoding != ContentTransferEncoding.Unknown)
				{
					this.UpdateTransferEncoding(transferEncoding);
					transferEncoding = ContentTransferEncoding.Binary;
				}
				switch (cachingMode)
				{
				case CachingMode.Copy:
				{
					TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
					dataStorage = temporaryDataStorage;
					using (Stream stream = temporaryDataStorage.OpenWriteStream(true))
					{
						byte[] array = null;
						dataEnd = DataStorage.CopyStreamToStream(contentStream, stream, long.MaxValue, ref array);
						goto IL_12B;
					}
					break;
				}
				case CachingMode.Source:
				case CachingMode.SourceTakeOwnership:
				{
					if (!contentStream.CanSeek)
					{
						throw new NotSupportedException(Strings.CachingModeSourceButStreamCannotSeek);
					}
					StreamOnDataStorage streamOnDataStorage = contentStream as StreamOnDataStorage;
					if (streamOnDataStorage == null)
					{
						dataStorage = new ReadableDataStorageOnStream(contentStream, cachingMode == CachingMode.SourceTakeOwnership);
						goto IL_12B;
					}
					dataStorage = streamOnDataStorage.Storage;
					dataStart = streamOnDataStorage.Start;
					dataEnd = streamOnDataStorage.End;
					dataStorage.AddRef();
					if (cachingMode == CachingMode.SourceTakeOwnership)
					{
						contentStream.Dispose();
						contentStream = null;
						goto IL_12B;
					}
					goto IL_12B;
				}
				}
				throw new ArgumentException("Invalid Caching Mode value", "cachingMode");
				IL_12B:
				this.SetStorage(dataStorage, dataStart, dataEnd, 0L, transferEncoding, LineTerminationState.Unknown);
				dataStorage.Release();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000138F4 File Offset: 0x00011AF4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00013900 File Offset: 0x00011B00
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				using (MimePart.SubtreeEnumerator enumerator = this.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages, false))
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.dataStorage != null)
						{
							enumerator.Current.dataStorage.Release();
							enumerator.Current.dataStorage = null;
						}
						if (enumerator.Current.headers != null)
						{
							enumerator.Current.headers.InternalDetachParent();
						}
						enumerator.Current.headers = null;
						enumerator.Current.boundary = null;
						enumerator.Current.parentDocument = null;
						if (enumerator.Current.deferredStorage != null)
						{
							enumerator.Current.deferredStorage.Release();
							enumerator.Current.deferredStorage = null;
							enumerator.Current.deferredStorageInfo = null;
						}
						enumerator.Current.isDisposed = true;
						GC.SuppressFinalize(enumerator.Current);
					}
					return;
				}
			}
			this.isDisposed = true;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00013A2C File Offset: 0x00011C2C
		public new MimeNode.Enumerator<MimePart> GetEnumerator()
		{
			this.ThrowIfDisposed();
			MimeNode.Enumerator<MimePart> result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = new MimeNode.Enumerator<MimePart>(this);
			}
			return result;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00013A70 File Offset: 0x00011C70
		IEnumerator<MimePart> IEnumerable<MimePart>.GetEnumerator()
		{
			this.ThrowIfDisposed();
			IEnumerator<MimePart> result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = new MimeNode.Enumerator<MimePart>(this);
			}
			return result;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00013AB8 File Offset: 0x00011CB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.ThrowIfDisposed();
			IEnumerator result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = new MimeNode.Enumerator<MimePart>(this);
			}
			return result;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00013B00 File Offset: 0x00011D00
		public sealed override MimeNode Clone()
		{
			MimeNode result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimePart mimePart = new MimePart();
				this.CopyTo(mimePart);
				result = mimePart;
			}
			return result;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00013B48 File Offset: 0x00011D48
		public sealed override void CopyTo(object destination)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (destination == null)
				{
					throw new ArgumentNullException("destination");
				}
				if (destination != this)
				{
					MimePart mimePart = destination as MimePart;
					if (mimePart == null)
					{
						throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
					}
					using (ThreadAccessGuard.EnterPublic(mimePart.accessToken))
					{
						byte[] array = null;
						TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
						using (Stream stream = temporaryDataStorage.OpenWriteStream(true))
						{
							this.CopyPartTo(false, mimePart, temporaryDataStorage, stream, 0L, ref array);
						}
						temporaryDataStorage.Release();
						mimePart.SetDirty();
					}
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00013C1C File Offset: 0x00011E1C
		public long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter)
		{
			this.ThrowIfDisposed();
			long result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				if (encodingOptions == null)
				{
					encodingOptions = base.GetDocumentEncodingOptions();
				}
				byte[] array = null;
				MimeStringLength mimeStringLength = new MimeStringLength(0);
				result = this.WriteTo(stream, encodingOptions, filter, ref mimeStringLength, ref array);
			}
			return result;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00013C90 File Offset: 0x00011E90
		internal bool IsProtectedHeader(string headerName)
		{
			this.ThrowIfDisposed();
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (!string.IsNullOrEmpty(headerName))
				{
					Header header = this.Headers.FindFirst("DKIM-Signature");
					if (header != null)
					{
						if (this.protectedHeaders == null)
						{
							this.PopulateProtectedHeaders();
						}
						foreach (string value in this.protectedHeaders)
						{
							if (headerName.Equals(value, StringComparison.OrdinalIgnoreCase))
							{
								return true;
							}
						}
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00013D44 File Offset: 0x00011F44
		private void PopulateProtectedHeaders()
		{
			this.protectedHeaders = new List<string>();
			this.protectedHeaders.Add("DKIM-Signature");
			Header header = this.Headers.FindFirst("DKIM-Signature");
			DecodingOptions headerDecodingOptions = base.GetHeaderDecodingOptions();
			while (header != null)
			{
				ValueParser valueParser = new ValueParser(header.Lines, headerDecodingOptions.AllowUTF8);
				MimeStringList mimeStringList = default(MimeStringList);
				bool handleISO = true;
				byte b;
				do
				{
					valueParser.ParseCFWS(false, ref mimeStringList, handleISO);
					b = valueParser.ParseGet();
					if (b != 59)
					{
						if (b == 0)
						{
							break;
						}
						valueParser.ParseUnget();
					}
					valueParser.ParseCFWS(false, ref mimeStringList, handleISO);
					MimeString mimeString = valueParser.ParseToken();
					if (mimeString.Length == 0 || mimeString.Length >= 128)
					{
						valueParser.ParseSkipToNextDelimiterByte(59);
					}
					else
					{
						valueParser.ParseCFWS(false, ref mimeStringList, handleISO);
						b = valueParser.ParseGet();
						if (b != 61)
						{
							if (b != 0)
							{
								valueParser.ParseUnget();
							}
						}
						else
						{
							valueParser.ParseCFWS(false, ref mimeStringList, handleISO);
							MimeStringList lines = default(MimeStringList);
							bool flag = false;
							valueParser.ParseParameterValue(ref lines, ref flag, handleISO);
							string text = Header.NormalizeString(mimeString.Data, mimeString.Offset, mimeString.Length, false);
							if (text.Equals("h", StringComparison.OrdinalIgnoreCase))
							{
								ValueParser valueParser2 = new ValueParser(lines, false);
								byte b2;
								do
								{
									valueParser2.ParseCFWS(false, ref mimeStringList, handleISO);
									b2 = valueParser2.ParseGet();
									if (b2 != 58)
									{
										if (b2 == 0)
										{
											break;
										}
										valueParser2.ParseUnget();
									}
									valueParser2.ParseCFWS(false, ref mimeStringList, handleISO);
									MimeString mimeString2 = valueParser2.ParseToken();
									if (mimeString2.Length == 0 || mimeString2.Length >= 128)
									{
										valueParser2.ParseSkipToNextDelimiterByte(58);
									}
									else
									{
										string item = Header.NormalizeString(mimeString2.Data, mimeString2.Offset, mimeString2.Length, false);
										this.protectedHeaders.Add(item);
									}
								}
								while (b2 != 0);
							}
						}
					}
				}
				while (b != 0);
				header = this.Headers.FindNext(header);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00013F44 File Offset: 0x00012144
		internal static byte[] GetBoundary(ContentTypeHeader contentType)
		{
			if (contentType == null || !contentType.IsMultipart)
			{
				return null;
			}
			MimeParameter mimeParameter = contentType["boundary"];
			if (mimeParameter == null)
			{
				mimeParameter = new MimeParameter("boundary");
				contentType.InternalAppendChild(mimeParameter);
				mimeParameter.RawValue = ContentTypeHeader.CreateBoundary();
			}
			byte[] rawValue = mimeParameter.RawValue;
			int num = (rawValue != null) ? rawValue.Length : 0;
			if (num == 0 || 70 < num)
			{
				mimeParameter.RawValue = ContentTypeHeader.CreateBoundary();
			}
			int num2 = MimeString.CRLF2Dashes.Length + rawValue.Length + MimeString.CrLf.Length;
			byte[] array = new byte[num2];
			int num3 = 0;
			Buffer.BlockCopy(MimeString.CRLF2Dashes, 0, array, num3, MimeString.CRLF2Dashes.Length);
			num3 = MimeString.CRLF2Dashes.Length;
			Buffer.BlockCopy(rawValue, 0, array, num3, rawValue.Length);
			num3 += rawValue.Length;
			Buffer.BlockCopy(MimeString.CrLf, 0, array, num3, MimeString.CrLf.Length);
			return array;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001401C File Offset: 0x0001221C
		internal static ContentTransferEncoding GetEncodingType(MimeString str)
		{
			if (str.Length != 0)
			{
				for (int i = 0; i < MimePart.EncodingMap.Length; i++)
				{
					if (str.CompareEqI(MimePart.EncodingMap[i].Name))
					{
						return MimePart.EncodingMap[i].Type;
					}
				}
			}
			return ContentTransferEncoding.Unknown;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00014070 File Offset: 0x00012270
		internal static byte[] GetEncodingName(ContentTransferEncoding encoding)
		{
			for (int i = 0; i < MimePart.EncodingMap.Length; i++)
			{
				if (MimePart.EncodingMap[i].Type == encoding)
				{
					return MimePart.EncodingMap[i].Name;
				}
			}
			return null;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000140B4 File Offset: 0x000122B4
		internal static bool IsEqualContentTransferEncoding(ContentTransferEncoding cte1, ContentTransferEncoding cte2)
		{
			return cte1 == cte2 || (MimePart.EncodingIsTransparent(cte1) && MimePart.EncodingIsTransparent(cte2));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000140CC File Offset: 0x000122CC
		internal static bool EncodingIsTransparent(ContentTransferEncoding cte)
		{
			return cte == ContentTransferEncoding.Binary || cte == ContentTransferEncoding.SevenBit || cte == ContentTransferEncoding.EightBit;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000140DC File Offset: 0x000122DC
		internal static ByteEncoder CreateEncoder(Stream stream, ContentTransferEncoding encoding)
		{
			switch (encoding)
			{
			case ContentTransferEncoding.QuotedPrintable:
				return new QPEncoder();
			case ContentTransferEncoding.Base64:
				return new Base64Encoder();
			case ContentTransferEncoding.UUEncode:
				return new UUEncoder();
			case ContentTransferEncoding.BinHex:
				return new BinHexEncoder(new MacBinaryHeader
				{
					DataForkLength = stream.Length,
					FileName = "binhex.dat"
				});
			default:
				return null;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001413C File Offset: 0x0001233C
		internal static ByteEncoder CreateDecoder(ContentTransferEncoding encoding)
		{
			switch (encoding)
			{
			case ContentTransferEncoding.QuotedPrintable:
				return new QPDecoder();
			case ContentTransferEncoding.Base64:
				return new Base64Decoder();
			case ContentTransferEncoding.UUEncode:
				return new UUDecoder();
			case ContentTransferEncoding.BinHex:
				return new BinHexDecoder(true);
			default:
				return null;
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00014180 File Offset: 0x00012380
		internal static long CopyStorageToStream(DataStorage srcStorage, long start, long end, LineTerminationState srcLineTermination, Stream destStream, ref byte[] scratchBuffer, ref LineTerminationState lineTermination)
		{
			long num = 0L;
			if (lineTermination != LineTerminationState.NotInteresting && srcLineTermination == LineTerminationState.Unknown)
			{
				if (scratchBuffer == null || scratchBuffer.Length < 16384)
				{
					scratchBuffer = new byte[16384];
				}
				using (Stream stream = srcStorage.OpenReadStream(start, end))
				{
					for (;;)
					{
						int num2 = stream.Read(scratchBuffer, 0, scratchBuffer.Length);
						if (num2 == 0)
						{
							break;
						}
						if (lineTermination != LineTerminationState.NotInteresting)
						{
							lineTermination = MimeCommon.AdvanceLineTerminationState(lineTermination, scratchBuffer, 0, num2);
						}
						destStream.Write(scratchBuffer, 0, num2);
						num += (long)num2;
					}
				}
				return num;
			}
			MimePart.CountingWriteStream countingWriteStream = null;
			if ((Stream.Null == destStream || ((countingWriteStream = (destStream as MimePart.CountingWriteStream)) != null && countingWriteStream.IsCountingOnly)) && end != 9223372036854775807L)
			{
				num = end - start;
				if (countingWriteStream != null)
				{
					countingWriteStream.Add(num);
				}
				if (lineTermination != LineTerminationState.NotInteresting)
				{
					lineTermination = srcLineTermination;
				}
				return num;
			}
			num = srcStorage.CopyContentToStream(start, end, destStream, ref scratchBuffer);
			if (lineTermination != LineTerminationState.NotInteresting)
			{
				lineTermination = srcLineTermination;
			}
			return num;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00014278 File Offset: 0x00012478
		internal override void SetDirty()
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.SetDirty");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.boundary = null;
				MimePart mimePart = this;
				bool flag = false;
				MimePart mimePart2;
				do
				{
					if (flag)
					{
						mimePart.SetStorageImpl(null, 0L, 0L);
						mimePart.ContentPersisted = false;
						mimePart.ContentDirty = true;
					}
					mimePart.IncrementVersion();
					flag = true;
					mimePart2 = mimePart;
					mimePart = (mimePart.Parent as MimePart);
				}
				while (mimePart != null);
				if (mimePart2.ParentDocument != null)
				{
					mimePart2.ParentDocument.IncrementVersion();
				}
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00014314 File Offset: 0x00012514
		internal override void ChildRemoved(MimeNode oldChild)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.ChildRemoved");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.dataStorage != null)
				{
					this.SetStorageImpl(null, 0L, 0L);
					this.ContentPersisted = false;
					this.ContentDirty = true;
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001437C File Offset: 0x0001257C
		internal override void RemoveAllUnparsed()
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.RemoveAllUnparsed");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.dataStorage != null && this.IsEmbeddedMessage)
				{
					this.SetStorageImpl(null, 0L, 0L);
					this.ContentPersisted = false;
					this.ContentDirty = true;
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000143EC File Offset: 0x000125EC
		internal override MimeNode ParseNextChild()
		{
			this.ThrowIfDisposed();
			MimeNode result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (base.InternalLastChild != null || this.dataStorage == null || !this.IsEmbeddedMessage)
				{
					result = null;
				}
				else
				{
					base.ThrowIfReadOnly("MimePart.ParseNextChild");
					MimeDocument mimeDocument = null;
					MimeDocument mimeDocument2;
					MimeNode mimeNode;
					base.GetMimeDocumentOrTreeRoot(out mimeDocument2, out mimeNode);
					try
					{
						if (mimeDocument2 == null)
						{
							mimeDocument = new MimeDocument();
							mimeDocument2 = mimeDocument;
						}
						mimeDocument2.BuildEmbeddedDom((MimePart)mimeNode);
						result = base.InternalLastChild;
					}
					finally
					{
						if (mimeDocument != null)
						{
							mimeDocument.Dispose();
							mimeDocument = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00014494 File Offset: 0x00012694
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimePart mimePart = newChild as MimePart;
				if (mimePart == null)
				{
					throw new ArgumentException(Strings.NewChildIsNotAPart);
				}
				MimeNode mimeNode = this;
				while (mimeNode != newChild)
				{
					mimeNode = mimeNode.Parent;
					if (mimeNode == null)
					{
						base.ThrowIfReadOnly("MimePart.ValidateNewChild");
						if (mimePart.parentDocument != null)
						{
							mimePart.parentDocument.RootPart = new MimePart();
							mimePart.parentDocument = null;
						}
						if (this.IsEmbeddedMessage && base.InternalLastChild != null)
						{
							base.InternalRemoveChild(base.InternalLastChild);
							refChild = null;
						}
						if (this.dataStorage != null)
						{
							this.SetStorageImpl(null, 0L, 0L);
							this.ContentPersisted = false;
							this.ContentDirty = true;
						}
						return refChild;
					}
				}
				throw new ArgumentException(Strings.ThisPartBelongsToSubtreeOfNewChild);
			}
			MimeNode result;
			return result;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00014570 File Offset: 0x00012770
		internal void CopyPartTo(bool signedOrProtectedContent, MimePart dstPart, TemporaryDataStorage dstStorage, Stream dstStream, long position, ref byte[] scratchBuffer)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				using (ThreadAccessGuard.EnterPublic(dstPart.accessToken))
				{
					long num = 0L;
					MimePart mimePart = null;
					using (MimePart.SubtreeEnumerator enumerator = this.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages | MimePart.SubtreeEnumerationOptions.RevisitParent, false))
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.FirstVisit)
							{
								if (enumerator.Depth != 0)
								{
									dstPart = new MimePart();
								}
								else if (signedOrProtectedContent)
								{
									DataStorage dataStorage = enumerator.Current.dataStorage;
									long dataStart = enumerator.Current.storageInfo.DataStart;
									num = position - dataStart;
									mimePart = dstPart;
									continue;
								}
								if (signedOrProtectedContent && enumerator.Current.dataStorage != null)
								{
									dstPart.SetStorageImpl(dstStorage, num + enumerator.Current.storageInfo.DataStart, num + enumerator.Current.storageInfo.DataEnd, enumerator.Current.storageInfo.BodyOffset, enumerator.Current.storageInfo.BodyCte, enumerator.Current.BodyLineTermination);
								}
								else if (enumerator.Current.IsSignedContent || enumerator.Current.IsProtectedContent)
								{
									long num2 = enumerator.Current.dataStorage.CopyContentToStream(enumerator.Current.storageInfo.DataStart, enumerator.Current.storageInfo.DataEnd, dstStream, ref scratchBuffer);
									dstPart.SetStorageImpl(dstStorage, position, position + num2, enumerator.Current.storageInfo.BodyOffset, enumerator.Current.storageInfo.BodyCte, enumerator.Current.storageInfo.BodyLineTermination);
									if (!enumerator.LastVisit)
									{
										enumerator.Current.CopyPartTo(true, dstPart, dstStorage, null, position, ref scratchBuffer);
										enumerator.SkipChildren();
									}
									position += num2;
								}
								else if (enumerator.Current.dataStorage == null || enumerator.Current.IsMultipart || (enumerator.Current.IsEmbeddedMessage && !enumerator.LastVisit))
								{
									dstPart.SetStorageImpl(null, 0L, 0L);
								}
								else
								{
									long num3 = enumerator.Current.dataStorage.CopyContentToStream(enumerator.Current.storageInfo.DataStart + enumerator.Current.storageInfo.BodyOffset, enumerator.Current.storageInfo.DataEnd, dstStream, ref scratchBuffer);
									dstPart.SetStorageImpl(dstStorage, position, position + num3, 0L, enumerator.Current.storageInfo.BodyCte, enumerator.Current.storageInfo.BodyLineTermination);
									position += num3;
								}
								dstPart.contentDirty = (enumerator.Depth == 0 && !signedOrProtectedContent);
								enumerator.Current.headers.CopyTo(dstPart.headers);
								if (!signedOrProtectedContent && !enumerator.Current.IsSignedContent && !enumerator.Current.IsProtectedContent)
								{
									ContentTypeHeader contentTypeHeader = dstPart.headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
									if (contentTypeHeader != null && contentTypeHeader.IsMultipart)
									{
										MimeParameter mimeParameter = contentTypeHeader["boundary"];
										if (mimeParameter != null)
										{
											mimeParameter.RawValue = ContentTypeHeader.CreateBoundary();
										}
									}
								}
								if (mimePart != null)
								{
									if (mimePart.IsEmbeddedMessage && mimePart.dataStorage != null && mimePart.InternalLastChild == null)
									{
										mimePart.InternalInsertAfter(dstPart, null);
									}
									else
									{
										mimePart.InternalAppendChild(dstPart);
									}
								}
								if (!enumerator.LastVisit)
								{
									mimePart = dstPart;
								}
							}
							else if (enumerator.LastVisit && mimePart != null)
							{
								mimePart = (mimePart.Parent as MimePart);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00014964 File Offset: 0x00012B64
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			this.ThrowIfDisposed();
			long result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				long num = 0L;
				MimePart.CountingWriteStream countingWriteStream = null;
				MimePart.CountingWriteStream countingWriteStream2 = null;
				long num2 = 0L;
				if (filter != null)
				{
					countingWriteStream = (stream as MimePart.CountingWriteStream);
					if (countingWriteStream == null)
					{
						countingWriteStream2 = new MimePart.CountingWriteStream(stream);
						countingWriteStream = countingWriteStream2;
						stream = countingWriteStream;
					}
					num2 = countingWriteStream.Count;
				}
				byte[] array = null;
				bool flag = true;
				LineTerminationState lineTerminationState = this.IsMultipart ? LineTerminationState.NotInteresting : LineTerminationState.CRLF;
				using (MimePart.SubtreeEnumerator enumerator = this.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages | MimePart.SubtreeEnumerationOptions.RevisitParent, false))
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.FirstVisit)
						{
							if (filter != null && filter.FilterPart(enumerator.Current, stream))
							{
								enumerator.SkipChildren();
							}
							else
							{
								if (array != null)
								{
									if (flag)
									{
										stream.Write(array, 0, array.Length);
										num += (long)array.Length;
									}
									else
									{
										stream.Write(array, 2, array.Length - 2);
										num += (long)(array.Length - 2);
										flag = true;
									}
									if (LineTerminationState.NotInteresting != lineTerminationState)
									{
										lineTerminationState = LineTerminationState.CRLF;
									}
								}
								if (enumerator.Current.IsSignedContent)
								{
									num += MimePart.CopyStorageToStream(enumerator.Current.dataStorage, enumerator.Current.storageInfo.DataStart, enumerator.Current.storageInfo.DataEnd, enumerator.Current.storageInfo.BodyLineTermination, stream, ref scratchBuffer, ref lineTerminationState);
									if (filter != null)
									{
										filter.ClosePart(enumerator.Current, stream);
									}
									enumerator.SkipChildren();
								}
								else
								{
									if (filter == null || !filter.FilterHeaderList(enumerator.Current.Headers, stream))
									{
										num += enumerator.Current.Headers.WriteTo(stream, encodingOptions, filter, ref currentLineLength, ref scratchBuffer);
										flag = true;
									}
									else
									{
										flag = false;
									}
									if (filter != null && filter.FilterPartBody(enumerator.Current, stream))
									{
										if (filter != null)
										{
											filter.ClosePart(enumerator.Current, stream);
										}
										enumerator.SkipChildren();
										flag = true;
									}
									else if (enumerator.Current.IsMultipart)
									{
										if (!enumerator.LastVisit)
										{
											array = enumerator.Current.Boundary;
										}
										else
										{
											if (flag)
											{
												stream.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
												num += (long)MimeString.CrLf.Length;
											}
											if (filter != null)
											{
												filter.ClosePart(enumerator.Current, stream);
											}
										}
									}
									else if (enumerator.Current.IsEmbeddedMessage && !enumerator.LastVisit)
									{
										if (flag)
										{
											stream.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
											num += (long)MimeString.CrLf.Length;
										}
										array = null;
									}
									else
									{
										if (flag)
										{
											stream.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
											num += (long)MimeString.CrLf.Length;
										}
										if (enumerator.Current.ContentTransferEncoding == enumerator.Current.storageInfo.BodyCte)
										{
											if (enumerator.Current.dataStorage != null)
											{
												num += MimePart.CopyStorageToStream(enumerator.Current.dataStorage, enumerator.Current.storageInfo.DataStart + enumerator.Current.storageInfo.BodyOffset, enumerator.Current.storageInfo.DataEnd, enumerator.Current.storageInfo.BodyLineTermination, stream, ref scratchBuffer, ref lineTerminationState);
											}
										}
										else
										{
											if (scratchBuffer == null || scratchBuffer.Length < 16384)
											{
												scratchBuffer = new byte[16384];
											}
											using (Stream rawContentReadStream = enumerator.Current.GetRawContentReadStream())
											{
												for (;;)
												{
													int num3 = rawContentReadStream.Read(scratchBuffer, 0, scratchBuffer.Length);
													if (num3 == 0)
													{
														break;
													}
													if (lineTerminationState != LineTerminationState.NotInteresting)
													{
														lineTerminationState = MimeCommon.AdvanceLineTerminationState(lineTerminationState, scratchBuffer, 0, num3);
													}
													stream.Write(scratchBuffer, 0, num3);
													num += (long)num3;
												}
											}
										}
										if (filter != null)
										{
											filter.ClosePart(enumerator.Current, stream);
										}
										if (!enumerator.LastVisit)
										{
											enumerator.SkipChildren();
										}
									}
								}
							}
						}
						else if (enumerator.LastVisit)
						{
							if (array != null)
							{
								stream.Write(array, 0, array.Length - 2);
								num += (long)(array.Length - 2);
								stream.Write(MimeString.TwoDashesCRLF, 0, MimeString.TwoDashesCRLF.Length);
								num += (long)MimeString.TwoDashesCRLF.Length;
								if (LineTerminationState.NotInteresting != lineTerminationState)
								{
									lineTerminationState = LineTerminationState.CRLF;
								}
							}
							array = ((enumerator.Current.Parent == null) ? null : ((MimePart)enumerator.Current.Parent).Boundary);
							if (filter != null)
							{
								filter.ClosePart(enumerator.Current, stream);
							}
						}
					}
				}
				if (!this.IsSignedContent)
				{
					switch (lineTerminationState)
					{
					case LineTerminationState.CR:
						stream.Write(MimeString.CrLf, 1, 1);
						num += 1L;
						break;
					case LineTerminationState.Other:
						stream.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
						num += (long)MimeString.CrLf.Length;
						break;
					}
				}
				if (countingWriteStream != null)
				{
					num = countingWriteStream.Count - num2;
					if (countingWriteStream2 != null)
					{
						countingWriteStream2.Dispose();
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00014E90 File Offset: 0x00013090
		internal void SetStorage(DataStorage storage, long dataStart, long dataEnd)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.SetStorageImpl(storage, dataStart, dataEnd);
				this.ContentPersisted = false;
				this.ContentDirty = true;
				this.SetDirty();
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00014EE4 File Offset: 0x000130E4
		internal void SetStorage(DataStorage storage, long dataStart, long dataEnd, long bodyOffset, ContentTransferEncoding bodyCte, LineTerminationState bodyLineTermination)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.SetStorageImpl(storage, dataStart, dataEnd, bodyOffset, bodyCte, bodyLineTermination);
				this.ContentPersisted = false;
				this.ContentDirty = true;
				this.SetDirty();
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00014F3C File Offset: 0x0001313C
		internal void SetStorageImpl(DataStorage storage, long dataStart, long dataEnd)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.SetStorageImpl(storage, dataStart, dataEnd, 0L, ContentTransferEncoding.Binary, LineTerminationState.Unknown);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00014F80 File Offset: 0x00013180
		internal void SetStorageImpl(DataStorage storage, long dataStart, long dataEnd, long bodyOffset, ContentTransferEncoding bodyCte, LineTerminationState bodyLineTermination)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.SetStorageImpl");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (storage != null)
				{
					storage.AddRef();
					storage.SetReadOnly(false);
				}
				if (this.dataStorage != null)
				{
					this.dataStorage.Release();
				}
				this.dataStorage = storage;
				this.storageInfo.DataStart = dataStart;
				this.storageInfo.DataEnd = dataEnd;
				this.storageInfo.BodyOffset = bodyOffset;
				this.storageInfo.BodyCte = bodyCte;
				this.storageInfo.BodyLineTermination = bodyLineTermination;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00015030 File Offset: 0x00013230
		internal void SetDeferredStorageImpl(DataStorage storage, long dataStart, long dataEnd)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.SetDeferredStorageImpl(storage, dataStart, dataEnd, 0L, ContentTransferEncoding.Binary, LineTerminationState.Unknown);
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00015074 File Offset: 0x00013274
		internal void SetDeferredStorageImpl(DataStorage storage, long dataStart, long dataEnd, long bodyOffset, ContentTransferEncoding bodyCte, LineTerminationState bodyLineTermination)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (base.IsReadOnly)
				{
					if (storage != null)
					{
						storage.AddRef();
					}
					MimePart.DataStorageInfo dataStorageInfo = new MimePart.DataStorageInfo();
					dataStorageInfo.DataStart = dataStart;
					dataStorageInfo.DataEnd = dataEnd;
					dataStorageInfo.BodyOffset = bodyOffset;
					dataStorageInfo.BodyCte = bodyCte;
					dataStorageInfo.BodyLineTermination = bodyLineTermination;
					lock (this.deferredStorageLock)
					{
						if (this.deferredStorage != null)
						{
							this.deferredStorage.Release();
						}
						this.deferredStorage = storage;
						this.deferredStorageInfo = dataStorageInfo;
						goto IL_9C;
					}
				}
				this.SetStorageImpl(storage, dataStart, dataEnd, bodyOffset, bodyCte, bodyLineTermination);
				IL_9C:;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00015148 File Offset: 0x00013348
		internal DataStorage DeferredStorage
		{
			get
			{
				DataStorage storage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.IsReadOnly)
					{
						lock (this.deferredStorageLock)
						{
							return (this.deferredStorageInfo != null) ? this.deferredStorage : this.dataStorage;
						}
					}
					storage = this.Storage;
				}
				return storage;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000151D0 File Offset: 0x000133D0
		internal long DeferredDataStart
		{
			get
			{
				long dataStart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.IsReadOnly)
					{
						lock (this.deferredStorageLock)
						{
							return (this.deferredStorageInfo != null) ? this.deferredStorageInfo.DataStart : this.storageInfo.DataStart;
						}
					}
					dataStart = this.DataStart;
				}
				return dataStart;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00015260 File Offset: 0x00013460
		internal long DeferredDataEnd
		{
			get
			{
				long dataEnd;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.IsReadOnly)
					{
						lock (this.deferredStorageLock)
						{
							return (this.deferredStorageInfo != null) ? this.deferredStorageInfo.DataEnd : this.storageInfo.DataEnd;
						}
					}
					dataEnd = this.DataEnd;
				}
				return dataEnd;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000389 RID: 905 RVA: 0x000152F0 File Offset: 0x000134F0
		internal long DeferredDataLength
		{
			get
			{
				long result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!base.IsReadOnly)
					{
						result = this.DataLength;
					}
					else
					{
						DataStorage dataStorage;
						MimePart.DataStorageInfo dataStorageInfo;
						lock (this.deferredStorageLock)
						{
							dataStorage = this.deferredStorage;
							dataStorageInfo = this.deferredStorageInfo;
						}
						if (dataStorageInfo == null)
						{
							result = this.DataLength;
						}
						else if (dataStorage == null)
						{
							result = 0L;
						}
						else
						{
							if (!MimePart.IsEqualContentTransferEncoding(dataStorageInfo.BodyCte, this.ContentTransferEncoding) || dataStorageInfo.DataEnd == 9223372036854775807L)
							{
								using (Stream rawContentReadStream = this.GetRawContentReadStream(dataStorage, dataStorageInfo))
								{
									if (rawContentReadStream.CanSeek)
									{
										return rawContentReadStream.Length;
									}
									byte[] array = new byte[4096];
									long num = 0L;
									int num2;
									while ((num2 = rawContentReadStream.Read(array, 0, array.Length)) != 0)
									{
										num += (long)num2;
									}
									return num;
								}
							}
							result = dataStorageInfo.DataEnd - dataStorageInfo.DataStart - dataStorageInfo.BodyOffset;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00015430 File Offset: 0x00013630
		internal long DeferredBodyOffset
		{
			get
			{
				long bodyOffset;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.IsReadOnly)
					{
						lock (this.deferredStorageLock)
						{
							return (this.deferredStorageInfo != null) ? this.deferredStorageInfo.BodyOffset : this.storageInfo.BodyOffset;
						}
					}
					bodyOffset = this.BodyOffset;
				}
				return bodyOffset;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000154C0 File Offset: 0x000136C0
		internal ContentTransferEncoding DeferredBodyCte
		{
			get
			{
				ContentTransferEncoding bodyCte;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.IsReadOnly)
					{
						lock (this.deferredStorageLock)
						{
							return (this.deferredStorageInfo != null) ? this.deferredStorageInfo.BodyCte : this.storageInfo.BodyCte;
						}
					}
					bodyCte = this.BodyCte;
				}
				return bodyCte;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00015550 File Offset: 0x00013750
		internal LineTerminationState DeferredBodyLineTermination
		{
			get
			{
				LineTerminationState bodyLineTermination;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (base.IsReadOnly)
					{
						lock (this.deferredStorageLock)
						{
							return (this.deferredStorageInfo != null) ? this.deferredStorageInfo.BodyLineTermination : this.storageInfo.BodyLineTermination;
						}
					}
					bodyLineTermination = this.BodyLineTermination;
				}
				return bodyLineTermination;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000155E0 File Offset: 0x000137E0
		internal void SetReadOnlyInternal(bool makeReadOnly)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (!makeReadOnly && this.deferredStorageInfo != null)
				{
					if (this.dataStorage != null)
					{
						this.dataStorage.Release();
					}
					this.dataStorage = this.deferredStorage;
					this.storageInfo = this.deferredStorageInfo;
					this.deferredStorage = null;
					this.deferredStorageInfo = null;
				}
				if (this.dataStorage != null)
				{
					this.Storage.SetReadOnly(makeReadOnly);
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00015670 File Offset: 0x00013870
		internal Charset FindMimeTreeCharset()
		{
			Charset result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				Charset charset = null;
				MimePart mimePart = this;
				while (!mimePart.IsEmbeddedMessage && mimePart.FirstChild != null)
				{
					mimePart = (MimePart)mimePart.FirstChild;
				}
				ComplexHeader complexHeader = mimePart.Headers.FindFirst(HeaderId.ContentType) as ComplexHeader;
				if (complexHeader != null)
				{
					MimeParameter mimeParameter = complexHeader["charset"];
					if (mimeParameter != null)
					{
						byte[] rawValue = mimeParameter.RawValue;
						if (rawValue != null)
						{
							string text = ByteString.BytesToString(rawValue, false);
							if (text != null && Charset.TryGetCharset(text, out charset) && charset.AsciiSupport < CodePageAsciiSupport.Fine)
							{
								charset = charset.Culture.MimeCharset;
							}
						}
					}
				}
				result = charset;
			}
			return result;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00015730 File Offset: 0x00013930
		private void IncrementVersion()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.version = ((int.MaxValue == this.version) ? 1 : (this.version + 1));
				this.protectedHeaders = null;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001578C File Offset: 0x0001398C
		internal void UpdateTransferEncoding(ContentTransferEncoding transferEncoding)
		{
			this.ThrowIfDisposed();
			base.ThrowIfReadOnly("MimePart.UpdateTransferEncoding");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				byte[] encodingName = MimePart.GetEncodingName(transferEncoding);
				if (encodingName == null)
				{
					throw new ArgumentException("Transfer encoding is unknown or not supported", "transferEncoding");
				}
				Header header = this.headers.FindFirst(HeaderId.ContentTransferEncoding);
				if (header == null)
				{
					header = Header.Create(HeaderId.ContentTransferEncoding);
					this.headers.InternalAppendChild(header);
				}
				else if (ContentTransferEncoding.SevenBit == transferEncoding)
				{
					header.RemoveFromParent();
					return;
				}
				header.RawValue = encodingName;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00015828 File Offset: 0x00013A28
		private void ThrowIfDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("MimePart");
			}
		}

		// Token: 0x040002BB RID: 699
		private static readonly MimePart.EncodingEntry[] EncodingMap = new MimePart.EncodingEntry[]
		{
			new MimePart.EncodingEntry(MimeString.Base64, ContentTransferEncoding.Base64),
			new MimePart.EncodingEntry(MimeString.QuotedPrintable, ContentTransferEncoding.QuotedPrintable),
			new MimePart.EncodingEntry(MimeString.Encoding7Bit, ContentTransferEncoding.SevenBit),
			new MimePart.EncodingEntry(MimeString.Encoding8Bit, ContentTransferEncoding.EightBit),
			new MimePart.EncodingEntry(MimeString.Binary, ContentTransferEncoding.Binary),
			new MimePart.EncodingEntry(MimeString.Uuencode, ContentTransferEncoding.UUEncode),
			new MimePart.EncodingEntry(MimeString.MacBinhex, ContentTransferEncoding.BinHex),
			new MimePart.EncodingEntry(MimeString.XUuencode, ContentTransferEncoding.UUEncode),
			new MimePart.EncodingEntry(MimeString.Uue, ContentTransferEncoding.UUEncode)
		};

		// Token: 0x040002BC RID: 700
		private MimePart.MimePartThreadAccessToken accessToken;

		// Token: 0x040002BD RID: 701
		private MimeDocument parentDocument;

		// Token: 0x040002BE RID: 702
		private HeaderList headers;

		// Token: 0x040002BF RID: 703
		private DataStorage dataStorage;

		// Token: 0x040002C0 RID: 704
		private DataStorage deferredStorage;

		// Token: 0x040002C1 RID: 705
		private MimePart.DataStorageInfo storageInfo = new MimePart.DataStorageInfo();

		// Token: 0x040002C2 RID: 706
		private MimePart.DataStorageInfo deferredStorageInfo;

		// Token: 0x040002C3 RID: 707
		private object deferredStorageLock = new object();

		// Token: 0x040002C4 RID: 708
		private bool isDisposed;

		// Token: 0x040002C5 RID: 709
		private bool contentDirty;

		// Token: 0x040002C6 RID: 710
		private bool contentPersisted;

		// Token: 0x040002C7 RID: 711
		private int version;

		// Token: 0x040002C8 RID: 712
		private List<string> protectedHeaders;

		// Token: 0x040002C9 RID: 713
		private int cacheMapStamp;

		// Token: 0x040002CA RID: 714
		private byte[] boundary;

		// Token: 0x0200005E RID: 94
		private class MimePartThreadAccessToken : ObjectThreadAccessToken
		{
			// Token: 0x06000393 RID: 915 RVA: 0x0001592A File Offset: 0x00013B2A
			internal MimePartThreadAccessToken(MimePart parent)
			{
			}
		}

		// Token: 0x0200005F RID: 95
		private class DataStorageInfo
		{
			// Token: 0x040002CB RID: 715
			public long DataStart;

			// Token: 0x040002CC RID: 716
			public long DataEnd;

			// Token: 0x040002CD RID: 717
			public long BodyOffset;

			// Token: 0x040002CE RID: 718
			public ContentTransferEncoding BodyCte;

			// Token: 0x040002CF RID: 719
			public LineTerminationState BodyLineTermination;
		}

		// Token: 0x02000060 RID: 96
		[Flags]
		public enum SubtreeEnumerationOptions : byte
		{
			// Token: 0x040002D1 RID: 721
			None = 0,
			// Token: 0x040002D2 RID: 722
			IncludeEmbeddedMessages = 1,
			// Token: 0x040002D3 RID: 723
			RevisitParent = 2
		}

		// Token: 0x02000061 RID: 97
		public struct PartSubtree : IEnumerable<MimePart>, IEnumerable
		{
			// Token: 0x06000395 RID: 917 RVA: 0x0001593A File Offset: 0x00013B3A
			internal PartSubtree(MimePart part)
			{
				this.part = part;
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00015943 File Offset: 0x00013B43
			public MimePart.SubtreeEnumerator GetEnumerator()
			{
				return new MimePart.SubtreeEnumerator(this.part, MimePart.SubtreeEnumerationOptions.None, true);
			}

			// Token: 0x06000397 RID: 919 RVA: 0x00015952 File Offset: 0x00013B52
			public MimePart.SubtreeEnumerator GetEnumerator(MimePart.SubtreeEnumerationOptions options)
			{
				return new MimePart.SubtreeEnumerator(this.part, options, true);
			}

			// Token: 0x06000398 RID: 920 RVA: 0x00015961 File Offset: 0x00013B61
			internal MimePart.SubtreeEnumerator GetEnumerator(MimePart.SubtreeEnumerationOptions options, bool includeUnparsed)
			{
				return new MimePart.SubtreeEnumerator(this.part, options, includeUnparsed);
			}

			// Token: 0x06000399 RID: 921 RVA: 0x00015970 File Offset: 0x00013B70
			IEnumerator<MimePart> IEnumerable<MimePart>.GetEnumerator()
			{
				return new MimePart.SubtreeEnumerator(this.part, MimePart.SubtreeEnumerationOptions.None, true);
			}

			// Token: 0x0600039A RID: 922 RVA: 0x00015984 File Offset: 0x00013B84
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new MimePart.SubtreeEnumerator(this.part, MimePart.SubtreeEnumerationOptions.None, true);
			}

			// Token: 0x040002D4 RID: 724
			private MimePart part;
		}

		// Token: 0x02000062 RID: 98
		public struct SubtreeEnumerator : IEnumerator<MimePart>, IDisposable, IEnumerator
		{
			// Token: 0x0600039B RID: 923 RVA: 0x00015998 File Offset: 0x00013B98
			internal SubtreeEnumerator(MimePart part, MimePart.SubtreeEnumerationOptions options, bool includeUnparsed)
			{
				this.options = options;
				this.includeUnparsed = includeUnparsed;
				this.root = part;
				this.current = null;
				this.currentDisposition = (MimePart.SubtreeEnumerator.EnumeratorDisposition)0;
				this.nextChild = part;
				this.depth = -1;
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x0600039C RID: 924 RVA: 0x000159CB File Offset: 0x00013BCB
			public MimePart Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x0600039D RID: 925 RVA: 0x000159D3 File Offset: 0x00013BD3
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600039E RID: 926 RVA: 0x000159DB File Offset: 0x00013BDB
			public bool FirstVisit
			{
				get
				{
					return 0 != (byte)(this.currentDisposition & MimePart.SubtreeEnumerator.EnumeratorDisposition.Begin);
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x0600039F RID: 927 RVA: 0x000159EC File Offset: 0x00013BEC
			public bool LastVisit
			{
				get
				{
					return 0 != (byte)(this.currentDisposition & MimePart.SubtreeEnumerator.EnumeratorDisposition.End);
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x000159FD File Offset: 0x00013BFD
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00015A08 File Offset: 0x00013C08
			public bool MoveNext()
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.root.AccessToken))
				{
					if (this.nextChild != null)
					{
						this.depth++;
						this.current = this.nextChild;
						if ((byte)(this.options & MimePart.SubtreeEnumerationOptions.IncludeEmbeddedMessages) == 0)
						{
							if (this.current.IsMultipart)
							{
								if (!this.includeUnparsed)
								{
									this.nextChild = ((this.current.InternalLastChild != null) ? ((MimePart)this.current.FirstChild) : null);
								}
								else
								{
									this.nextChild = (MimePart)this.current.FirstChild;
								}
							}
							else
							{
								this.nextChild = null;
							}
						}
						else if (!this.includeUnparsed)
						{
							this.nextChild = ((this.current.InternalLastChild != null) ? ((MimePart)this.current.FirstChild) : null);
						}
						else
						{
							this.nextChild = (MimePart)this.current.FirstChild;
						}
						this.currentDisposition = (MimePart.SubtreeEnumerator.EnumeratorDisposition)(1 | ((this.nextChild == null) ? 2 : 0));
						result = true;
					}
					else if (this.depth < 0)
					{
						result = false;
					}
					else
					{
						for (;;)
						{
							this.depth--;
							if (this.depth < 0)
							{
								break;
							}
							if (!this.includeUnparsed)
							{
								this.nextChild = (MimePart)this.current.InternalNextSibling;
							}
							else
							{
								this.nextChild = (MimePart)this.current.NextSibling;
							}
							this.current = (MimePart)this.current.Parent;
							this.currentDisposition = ((this.nextChild == null) ? MimePart.SubtreeEnumerator.EnumeratorDisposition.End : ((MimePart.SubtreeEnumerator.EnumeratorDisposition)0));
							if ((byte)(this.options & MimePart.SubtreeEnumerationOptions.RevisitParent) != 0 || this.nextChild != null)
							{
								goto IL_1B8;
							}
						}
						this.current = null;
						this.nextChild = null;
						this.currentDisposition = (MimePart.SubtreeEnumerator.EnumeratorDisposition)0;
						return false;
						IL_1B8:
						result = ((byte)(this.options & MimePart.SubtreeEnumerationOptions.RevisitParent) != 0 || this.MoveNext());
					}
				}
				return result;
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x00015C0C File Offset: 0x00013E0C
			public void SkipChildren()
			{
				if (this.nextChild != null)
				{
					this.nextChild = null;
					this.currentDisposition |= MimePart.SubtreeEnumerator.EnumeratorDisposition.End;
				}
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x00015C2C File Offset: 0x00013E2C
			void IEnumerator.Reset()
			{
				this.current = null;
				this.currentDisposition = (MimePart.SubtreeEnumerator.EnumeratorDisposition)0;
				this.nextChild = this.root;
				this.depth = -1;
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00015C4F File Offset: 0x00013E4F
			public void Dispose()
			{
				((IEnumerator)this).Reset();
				GC.SuppressFinalize(this);
			}

			// Token: 0x040002D5 RID: 725
			private MimePart.SubtreeEnumerationOptions options;

			// Token: 0x040002D6 RID: 726
			private bool includeUnparsed;

			// Token: 0x040002D7 RID: 727
			private MimePart.SubtreeEnumerator.EnumeratorDisposition currentDisposition;

			// Token: 0x040002D8 RID: 728
			private MimePart root;

			// Token: 0x040002D9 RID: 729
			private MimePart current;

			// Token: 0x040002DA RID: 730
			private MimePart nextChild;

			// Token: 0x040002DB RID: 731
			private int depth;

			// Token: 0x02000063 RID: 99
			[Flags]
			private enum EnumeratorDisposition : byte
			{
				// Token: 0x040002DD RID: 733
				Begin = 1,
				// Token: 0x040002DE RID: 734
				End = 2
			}
		}

		// Token: 0x02000064 RID: 100
		internal class CountingWriteStream : Stream
		{
			// Token: 0x060003A5 RID: 933 RVA: 0x00015C71 File Offset: 0x00013E71
			internal CountingWriteStream(Stream stream)
			{
				this.stream = stream;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x00015C80 File Offset: 0x00013E80
			public bool IsCountingOnly
			{
				get
				{
					return this.stream == Stream.Null;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x00015C8F File Offset: 0x00013E8F
			public long Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00015C97 File Offset: 0x00013E97
			public void Add(long value)
			{
				this.count += value;
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x00015CA7 File Offset: 0x00013EA7
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060003AA RID: 938 RVA: 0x00015CAA File Offset: 0x00013EAA
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060003AB RID: 939 RVA: 0x00015CAD File Offset: 0x00013EAD
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060003AC RID: 940 RVA: 0x00015CB0 File Offset: 0x00013EB0
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060003AD RID: 941 RVA: 0x00015CB7 File Offset: 0x00013EB7
			// (set) Token: 0x060003AE RID: 942 RVA: 0x00015CBE File Offset: 0x00013EBE
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

			// Token: 0x060003AF RID: 943 RVA: 0x00015CC5 File Offset: 0x00013EC5
			public override int Read(byte[] array, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00015CCC File Offset: 0x00013ECC
			public override void Write(byte[] array, int offset, int count)
			{
				this.count += (long)count;
				this.stream.Write(array, offset, count);
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00015CEB File Offset: 0x00013EEB
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x00015CF2 File Offset: 0x00013EF2
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x00015CFF File Offset: 0x00013EFF
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x00015D06 File Offset: 0x00013F06
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
			}

			// Token: 0x040002DF RID: 735
			private long count;

			// Token: 0x040002E0 RID: 736
			private Stream stream;
		}

		// Token: 0x02000067 RID: 103
		private class PartContentWriteStream : AppendStreamOnDataStorage
		{
			// Token: 0x060003CB RID: 971 RVA: 0x00015E76 File Offset: 0x00014076
			public PartContentWriteStream(MimePart part, ContentTransferEncoding cte) : base(new TemporaryDataStorage())
			{
				this.part = part;
				this.cte = cte;
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00015E94 File Offset: 0x00014094
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.part != null)
				{
					using (ThreadAccessGuard.EnterPublic(this.part.AccessToken))
					{
						ReadableDataStorage readableWritableStorage = base.ReadableWritableStorage;
						readableWritableStorage.AddRef();
						base.Dispose(disposing);
						if (!this.part.isDisposed)
						{
							this.part.SetStorage(readableWritableStorage, 0L, readableWritableStorage.Length, 0L, this.cte, LineTerminationState.Unknown);
						}
						readableWritableStorage.Release();
					}
					this.part = null;
					return;
				}
				base.Dispose(disposing);
			}

			// Token: 0x040002E3 RID: 739
			private MimePart part;

			// Token: 0x040002E4 RID: 740
			private ContentTransferEncoding cte;
		}

		// Token: 0x02000068 RID: 104
		private struct EncodingEntry
		{
			// Token: 0x060003CD RID: 973 RVA: 0x00015F2C File Offset: 0x0001412C
			internal EncodingEntry(byte[] name, ContentTransferEncoding type)
			{
				this.Name = name;
				this.Type = type;
			}

			// Token: 0x040002E5 RID: 741
			internal byte[] Name;

			// Token: 0x040002E6 RID: 742
			internal ContentTransferEncoding Type;
		}
	}
}
