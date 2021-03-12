using System;
using System.IO;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000002 RID: 2
	internal class Pop3Message : ProtocolMessage
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public Pop3Message(Pop3ResponseFactory factory, int index, object[] itemData) : base(index, (int)itemData[0], (int)itemData[1], (int)itemData[2])
		{
			this.factory = factory;
			if (factory.ExactRFC822SizeEnabled)
			{
				OutboundConversionOptions outboundConversionOptions = factory.GetOutboundConversionOptions();
				if (!(itemData[7] is PropertyError) && !(itemData[6] is PropertyError) && (long)itemData[6] == this.GetPopMIMEOptionsHash((long)outboundConversionOptions.InternetTextFormat))
				{
					this.Rfc822Size = (long)itemData[7];
				}
				else
				{
					this.Rfc822Size = -1L;
				}
			}
			else
			{
				this.Rfc822Size = (long)((int)itemData[2]);
			}
			if (!(itemData[5] is PropertyError))
			{
				this.needsLegacyUid = ((short)itemData[5] <= 6);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002184 File Offset: 0x00000384
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000218C File Offset: 0x0000038C
		public override bool IsNotRfc822Renderable
		{
			get
			{
				return this.notRfc822Renderable;
			}
			set
			{
				this.notRfc822Renderable = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002195 File Offset: 0x00000395
		public override StoreObjectId Uid
		{
			get
			{
				return this.factory.DataAccessView.GetStoreObjectId(base.Id);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021AD File Offset: 0x000003AD
		public override ResponseFactory Factory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021B5 File Offset: 0x000003B5
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021BD File Offset: 0x000003BD
		public bool IsRead
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021C6 File Offset: 0x000003C6
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021CE File Offset: 0x000003CE
		public long Rfc822Size
		{
			get
			{
				return this.rfc822Size;
			}
			set
			{
				this.rfc822Size = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021D7 File Offset: 0x000003D7
		public bool HasSize
		{
			get
			{
				return this.Rfc822Size >= 0L;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021E6 File Offset: 0x000003E6
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021EE File Offset: 0x000003EE
		public bool NeedsLegacyUid
		{
			get
			{
				return this.needsLegacyUid;
			}
			set
			{
				this.needsLegacyUid = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000221D File Offset: 0x0000041D
		public string LegacyUid
		{
			get
			{
				return this.legacyUid ?? base.Id.ToString();
			}
			set
			{
				this.legacyUid = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002226 File Offset: 0x00000426
		public static PropertyDefinition[] GetFastQueryViewProperties(ResponseFactory factory)
		{
			if (!factory.ExactRFC822SizeEnabled)
			{
				return Pop3Message.fastMessageProperties;
			}
			return Pop3Message.messageProperties;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000223B File Offset: 0x0000043B
		public static SortBy[] GetFastQueryViewSortBys(ResponseFactory factory)
		{
			if (!factory.ExactRFC822SizeEnabled)
			{
				return Pop3Message.fastSortBys;
			}
			return Pop3Message.sortBys;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002250 File Offset: 0x00000450
		public long GetSize()
		{
			if (this.Rfc822Size < 0L)
			{
				using (this.GetMimeStream(null))
				{
				}
			}
			return this.Rfc822Size;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002294 File Offset: 0x00000494
		public StreamWrapper GetHeaderMimeStream(Pop3RequestRetr request)
		{
			return this.GetMimeStream(request, true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000229E File Offset: 0x0000049E
		public Pop3MimeStream GetMimeStream(Pop3RequestRetr request)
		{
			return this.GetMimeStream(request, false) as Pop3MimeStream;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002768 File Offset: 0x00000968
		private StreamWrapper GetMimeStream(Pop3RequestRetr request, bool headerOnly)
		{
			Pop3Message.<>c__DisplayClass2 CS$<>8__locals1 = new Pop3Message.<>c__DisplayClass2();
			CS$<>8__locals1.request = request;
			CS$<>8__locals1.headerOnly = headerOnly;
			CS$<>8__locals1.<>4__this = this;
			StoreObjectId uid = this.Uid;
			if (this.IsNotRfc822Renderable || uid == null)
			{
				return null;
			}
			Pop3Message.<>c__DisplayClass6 CS$<>8__locals2 = new Pop3Message.<>c__DisplayClass6();
			CS$<>8__locals2.CS$<>8__locals3 = CS$<>8__locals1;
			CS$<>8__locals2.item = Item.Bind(this.Factory.Store, uid, StoreObjectSchema.ContentConversionProperties);
			StreamWrapper result;
			try
			{
				if (CS$<>8__locals2.item == null)
				{
					ProtocolBaseServices.SessionTracer.TraceError<StoreObjectId>(this.Factory.Session.SessionId, "Null item returned for ObjectId: {0}", uid);
					this.IsNotRfc822Renderable = true;
					result = null;
				}
				else
				{
					result = ProtocolMessage.ProcessMessageWithPoisonMessageHandling<StreamWrapper>(new FilterDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<GetMimeStream>b__0)), delegate
					{
						string subject = null;
						string date = null;
						object obj = CS$<>8__locals2.item.TryGetProperty(ItemSchema.Subject);
						if (!(obj is PropertyError) && CS$<>8__locals2.CS$<>8__locals3.request != null)
						{
							subject = (CS$<>8__locals2.CS$<>8__locals3.request.CurrentMessageSubject = (string)obj);
						}
						obj = CS$<>8__locals2.item.TryGetProperty(ItemSchema.SentTime);
						if (!(obj is PropertyError) && CS$<>8__locals2.CS$<>8__locals3.request != null)
						{
							date = (CS$<>8__locals2.CS$<>8__locals3.request.CurrentMessageSentTime = ((ExDateTime)obj).ToString());
						}
						if (CS$<>8__locals2.CS$<>8__locals3.<>4__this.IsNotRfc822Renderable)
						{
							return null;
						}
						StreamWrapper streamWrapper = null;
						bool flag = false;
						bool flag2 = !CS$<>8__locals2.CS$<>8__locals3.headerOnly && CS$<>8__locals2.CS$<>8__locals3.<>4__this.Rfc822Size < 0L && CS$<>8__locals2.CS$<>8__locals3.<>4__this.Factory.ExactRFC822SizeEnabled;
						try
						{
							if (CS$<>8__locals2.CS$<>8__locals3.headerOnly)
							{
								streamWrapper = new MimeHeaderStream();
							}
							else
							{
								streamWrapper = new Pop3MimeStream();
							}
							ProtocolBaseServices.SessionTracer.TraceDebug(CS$<>8__locals2.CS$<>8__locals3.<>4__this.Factory.Session.SessionId, "ProtocolMessage.LoadMimeStream is called");
							OutboundConversionOptions outboundConversionOptions = CS$<>8__locals2.CS$<>8__locals3.<>4__this.Factory.GetOutboundConversionOptions();
							try
							{
								using (ImapItemConverter imapItemConverter = new ImapItemConverter(CS$<>8__locals2.item, outboundConversionOptions))
								{
									if (CS$<>8__locals2.CS$<>8__locals3.headerOnly)
									{
										MimePartHeaders headers = imapItemConverter.GetHeaders();
										CS$<>8__locals2.CS$<>8__locals3.<>4__this.WriteHeaders(headers, streamWrapper);
									}
									else
									{
										imapItemConverter.GetBody(streamWrapper);
										if (CS$<>8__locals2.CS$<>8__locals3.<>4__this.Rfc822Size < 0L)
										{
											CS$<>8__locals2.CS$<>8__locals3.<>4__this.Rfc822Size = streamWrapper.Length;
										}
										if (CS$<>8__locals2.CS$<>8__locals3.<>4__this.Factory.Session.LrsSession != null)
										{
											CS$<>8__locals2.CS$<>8__locals3.<>4__this.Factory.Session.LrsSession.LogMessage(imapItemConverter, CS$<>8__locals2.CS$<>8__locals3.<>4__this.Rfc822Size);
										}
									}
								}
							}
							catch (NotSupportedException exception)
							{
								ProtocolMessage.HandleMimeConversionException(CS$<>8__locals2.item, CS$<>8__locals2.CS$<>8__locals3.<>4__this, subject, date, exception);
								return null;
							}
							catch (NotImplementedException exception2)
							{
								ProtocolMessage.HandleMimeConversionException(CS$<>8__locals2.item, CS$<>8__locals2.CS$<>8__locals3.<>4__this, subject, date, exception2);
								return null;
							}
							catch (InvalidOperationException exception3)
							{
								ProtocolMessage.HandleMimeConversionException(CS$<>8__locals2.item, CS$<>8__locals2.CS$<>8__locals3.<>4__this, subject, date, exception3);
								return null;
							}
							catch (ExchangeDataException exception4)
							{
								ProtocolMessage.HandleMimeConversionException(CS$<>8__locals2.item, CS$<>8__locals2.CS$<>8__locals3.<>4__this, subject, date, exception4);
								return null;
							}
							catch (StoragePermanentException exception5)
							{
								ProtocolMessage.HandleMimeConversionException(CS$<>8__locals2.item, CS$<>8__locals2.CS$<>8__locals3.<>4__this, subject, date, exception5);
								return null;
							}
							catch (StorageTransientException exception6)
							{
								ProtocolMessage.HandleMimeConversionException(CS$<>8__locals2.item, CS$<>8__locals2.CS$<>8__locals3.<>4__this, subject, date, exception6);
								return null;
							}
							streamWrapper.Seek(0L, SeekOrigin.Begin);
							if (flag2)
							{
								try
								{
									CS$<>8__locals2.item.OpenAsReadWrite();
									CS$<>8__locals2.item[Pop3Message.messageProperties[7]] = streamWrapper.Length;
									CS$<>8__locals2.item[Pop3Message.messageProperties[6]] = CS$<>8__locals2.CS$<>8__locals3.<>4__this.GetPopMIMEOptionsHash((long)outboundConversionOptions.InternetTextFormat);
									CS$<>8__locals2.item.Save(SaveMode.ResolveConflicts);
								}
								catch (LocalizedException arg)
								{
									ProtocolBaseServices.ServerTracer.TraceError<LocalizedException>(CS$<>8__locals2.CS$<>8__locals3.<>4__this.factory.Session.SessionId, "Issue saving back item's size! Error: {0}", arg);
								}
							}
							flag = true;
						}
						finally
						{
							if (!flag && streamWrapper != null)
							{
								streamWrapper.Close();
								streamWrapper = null;
							}
						}
						return streamWrapper;
					});
				}
			}
			finally
			{
				if (CS$<>8__locals2.item != null)
				{
					((IDisposable)CS$<>8__locals2.item).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000285C File Offset: 0x00000A5C
		private void WriteHeaders(MimePartHeaders headers, Stream stream)
		{
			MimeWriter mimeWriter = new MimeWriter(stream, false, new EncodingOptions(headers.Charset.Name, this.Factory.Store.Culture.Name, EncodingFlags.None));
			mimeWriter.StartPart();
			foreach (Header header in headers)
			{
				header.WriteTo(mimeWriter);
			}
			mimeWriter.EndPart();
			mimeWriter.Flush();
			stream.Flush();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028EC File Offset: 0x00000AEC
		private long GetPopMIMEOptionsHash(long conversionOptions)
		{
			return (conversionOptions << 32) + (long)base.Id;
		}

		// Token: 0x04000001 RID: 1
		private const int LastNonE12ServerVersion = 6;

		// Token: 0x04000002 RID: 2
		private static readonly PropertyDefinition[] fastMessageProperties = new PropertyDefinition[]
		{
			ItemSchema.ImapId,
			ItemSchema.DocumentId,
			ItemSchema.Size,
			MessageItemSchema.MimeConversionFailed,
			MessageItemSchema.MessageHidden,
			ItemSchema.OriginalSourceServerVersion
		};

		// Token: 0x04000003 RID: 3
		private static readonly PropertyDefinition[] messageProperties = new PropertyDefinition[]
		{
			Pop3Message.fastMessageProperties[0],
			Pop3Message.fastMessageProperties[1],
			Pop3Message.fastMessageProperties[2],
			Pop3Message.fastMessageProperties[3],
			Pop3Message.fastMessageProperties[4],
			Pop3Message.fastMessageProperties[5],
			ItemSchema.PopMIMEOptions,
			ItemSchema.PopMIMESize
		};

		// Token: 0x04000004 RID: 4
		private static readonly SortBy[] fastSortBys = new SortBy[]
		{
			new SortBy(ItemSchema.ImapId, SortOrder.Ascending),
			new SortBy(ItemSchema.Size, SortOrder.Ascending),
			new SortBy(ItemSchema.MessageStatus, SortOrder.Ascending),
			new SortBy(ItemSchema.OriginalSourceServerVersion, SortOrder.Ascending)
		};

		// Token: 0x04000005 RID: 5
		private static readonly SortBy[] sortBys = new SortBy[]
		{
			new SortBy(ItemSchema.ImapId, SortOrder.Ascending),
			new SortBy(ItemSchema.Size, SortOrder.Ascending),
			new SortBy(ItemSchema.MessageStatus, SortOrder.Ascending),
			new SortBy(ItemSchema.PopMIMEOptions, SortOrder.Ascending),
			new SortBy(ItemSchema.PopMIMESize, SortOrder.Ascending)
		};

		// Token: 0x04000006 RID: 6
		private static readonly PropertyDefinition[] PropertiesToChangeForSizeConversion = new PropertyDefinition[]
		{
			ItemSchema.PopMIMEOptions,
			ItemSchema.PopMIMESize
		};

		// Token: 0x04000007 RID: 7
		private Pop3ResponseFactory factory;

		// Token: 0x04000008 RID: 8
		private long rfc822Size;

		// Token: 0x04000009 RID: 9
		private bool notRfc822Renderable;

		// Token: 0x0400000A RID: 10
		private bool read;

		// Token: 0x0400000B RID: 11
		private bool needsLegacyUid;

		// Token: 0x0400000C RID: 12
		private string legacyUid;

		// Token: 0x02000003 RID: 3
		public struct PropertyIndex
		{
			// Token: 0x0400000D RID: 13
			public const int ImapId = 0;

			// Token: 0x0400000E RID: 14
			public const int DocumentId = 1;

			// Token: 0x0400000F RID: 15
			public const int Size = 2;

			// Token: 0x04000010 RID: 16
			public const int MimeConversionFailed = 3;

			// Token: 0x04000011 RID: 17
			public const int MessageHidden = 4;

			// Token: 0x04000012 RID: 18
			public const int OriginalSourceServerVersion = 5;

			// Token: 0x04000013 RID: 19
			public const int PopMIMEOptions = 6;

			// Token: 0x04000014 RID: 20
			public const int PopMIMESize = 7;
		}
	}
}
