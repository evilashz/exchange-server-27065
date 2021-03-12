using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.MailboxOperators;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000009 RID: 9
	internal static class TransportItemHandlerAdaptor
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003104 File Offset: 0x00001304
		internal static IRetrieverItem WrapTransportItem(EmailMessage item)
		{
			return new TransportItemHandlerAdaptor.TransportItem(item, true);
		}

		// Token: 0x0200000A RID: 10
		private class TransportItem : IRetrieverItem, IDisposable
		{
			// Token: 0x06000030 RID: 48 RVA: 0x0000310D File Offset: 0x0000130D
			internal TransportItem(EmailMessage item, bool shouldDispose)
			{
				this.item = item;
				this.shouldDispose = shouldDispose;
				if (this.ShouldPromoteToXsoItem())
				{
					this.PromoteItemToXso();
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00003131 File Offset: 0x00001331
			public IRetrieverAttachmentCollection AttachmentCollection
			{
				get
				{
					if (this.promotedItem != null)
					{
						return XSOItemHandlerAdaptor.WrapXSOItem(this.promotedItem, false).AttachmentCollection;
					}
					return new TransportItemHandlerAdaptor.TransportAttachmentCollection(this.item.Attachments);
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000032 RID: 50 RVA: 0x0000315D File Offset: 0x0000135D
			public IEnumerable<IRetrieverPropertyDefinition> EmbeddedMessageProperties
			{
				get
				{
					return TransportItemHandlerAdaptor.PropertyDefinitionSchema.Instance.AllProperties;
				}
			}

			// Token: 0x06000033 RID: 51 RVA: 0x0000316C File Offset: 0x0000136C
			public object GetValue(IRetrieverPropertyDefinition propertyDefinition)
			{
				object result = null;
				if (propertyDefinition == TransportItemHandlerAdaptor.PropertyDefinitionSchema.Instance.Body)
				{
					using (MessageBody messageBody = MessageBody.Create(this.item.Body))
					{
						if (messageBody != null)
						{
							result = messageBody.ToString();
						}
						return result;
					}
				}
				if (propertyDefinition == TransportItemHandlerAdaptor.PropertyDefinitionSchema.Instance.Subject)
				{
					result = this.item.Subject;
				}
				return result;
			}

			// Token: 0x06000034 RID: 52 RVA: 0x000031DC File Offset: 0x000013DC
			public void Dispose()
			{
				if (this.shouldDispose && this.item != null)
				{
					this.item.Dispose();
				}
				if (this.promotedItem != null)
				{
					this.promotedItem.Dispose();
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x0000320C File Offset: 0x0000140C
			private bool ShouldPromoteToXsoItem()
			{
				if (!TransportItemHandlerAdaptor.TransportItem.config.UseMdmFlow || TransportItemHandlerAdaptor.TransportItem.config.SkipMdmGeneration)
				{
					return false;
				}
				foreach (Attachment attachment in this.item.Attachments)
				{
					if (attachment.EmbeddedMessage != null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00003288 File Offset: 0x00001488
			private void PromoteItemToXso()
			{
				this.promotedItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
				ItemConversion.ConvertAnyMimeToItem(this.promotedItem, this.item.MimeDocument, TransportItemHandlerAdaptor.TransportItem.inboundConversionOptions);
			}

			// Token: 0x04000027 RID: 39
			private static readonly InboundConversionOptions inboundConversionOptions = ConvertUtils.GetInboundConversionOptions();

			// Token: 0x04000028 RID: 40
			private static readonly SearchConfig config = new FlightingSearchConfig();

			// Token: 0x04000029 RID: 41
			private readonly EmailMessage item;

			// Token: 0x0400002A RID: 42
			private readonly bool shouldDispose;

			// Token: 0x0400002B RID: 43
			private MessageItem promotedItem;
		}

		// Token: 0x0200000B RID: 11
		private class TransportAttachmentCollection : IRetrieverAttachmentCollection, IEnumerable<IRetrieverAttachmentHandle>, IEnumerable
		{
			// Token: 0x06000038 RID: 56 RVA: 0x000032CB File Offset: 0x000014CB
			internal TransportAttachmentCollection(AttachmentCollection attachmentCollection)
			{
				this.attachmentCollection = attachmentCollection;
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000039 RID: 57 RVA: 0x000032DA File Offset: 0x000014DA
			public int Count
			{
				get
				{
					return this.attachmentCollection.Count;
				}
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000032E7 File Offset: 0x000014E7
			public IEnumerator<IRetrieverAttachmentHandle> GetEnumerator()
			{
				return new TransportItemHandlerAdaptor.TransportAttachmentCollection.Enumerator(this.attachmentCollection.Count);
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000032F9 File Offset: 0x000014F9
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00003304 File Offset: 0x00001504
			public IRetrieverAttachment Open(IRetrieverAttachmentHandle handle)
			{
				TransportItemHandlerAdaptor.TransportAttachmentHandle transportAttachmentHandle = (TransportItemHandlerAdaptor.TransportAttachmentHandle)handle;
				Attachment attachment = this.attachmentCollection[transportAttachmentHandle.AttachNumber];
				if (attachment.EmbeddedMessage != null)
				{
					return new TransportItemHandlerAdaptor.TransportItemAttachment(attachment);
				}
				return new TransportItemHandlerAdaptor.TransportStreamAttachment(attachment);
			}

			// Token: 0x0400002C RID: 44
			private readonly AttachmentCollection attachmentCollection;

			// Token: 0x0200000C RID: 12
			private class Enumerator : IEnumerator<IRetrieverAttachmentHandle>, IDisposable, IEnumerator
			{
				// Token: 0x0600003D RID: 61 RVA: 0x0000333F File Offset: 0x0000153F
				internal Enumerator(int numberOfAttachments)
				{
					this.numberOfAttachments = numberOfAttachments;
					this.Reset();
				}

				// Token: 0x1700000D RID: 13
				// (get) Token: 0x0600003E RID: 62 RVA: 0x00003354 File Offset: 0x00001554
				public IRetrieverAttachmentHandle Current
				{
					get
					{
						if (this.currentAttachmentNumber == -1)
						{
							throw new InvalidOperationException("Current cannot be called before MoveNext");
						}
						if (this.currentAttachmentNumber == this.numberOfAttachments)
						{
							throw new InvalidOperationException("Current cannot be called at the end of enumeration");
						}
						if (this.currentAttachmentHandle == null)
						{
							this.currentAttachmentHandle = new TransportItemHandlerAdaptor.TransportAttachmentHandle(this.currentAttachmentNumber);
						}
						return this.currentAttachmentHandle;
					}
				}

				// Token: 0x1700000E RID: 14
				// (get) Token: 0x0600003F RID: 63 RVA: 0x000033AD File Offset: 0x000015AD
				object IEnumerator.Current
				{
					get
					{
						return ((IEnumerator<IRetrieverAttachmentHandle>)this).Current;
					}
				}

				// Token: 0x06000040 RID: 64 RVA: 0x000033B5 File Offset: 0x000015B5
				public void Dispose()
				{
					this.Reset();
				}

				// Token: 0x06000041 RID: 65 RVA: 0x000033BD File Offset: 0x000015BD
				public bool MoveNext()
				{
					this.currentAttachmentHandle = null;
					if (this.currentAttachmentNumber == this.numberOfAttachments)
					{
						return false;
					}
					this.currentAttachmentNumber++;
					return this.currentAttachmentNumber != this.numberOfAttachments;
				}

				// Token: 0x06000042 RID: 66 RVA: 0x000033F5 File Offset: 0x000015F5
				public void Reset()
				{
					this.currentAttachmentHandle = null;
					this.currentAttachmentNumber = -1;
				}

				// Token: 0x0400002D RID: 45
				private readonly int numberOfAttachments;

				// Token: 0x0400002E RID: 46
				private TransportItemHandlerAdaptor.TransportAttachmentHandle currentAttachmentHandle;

				// Token: 0x0400002F RID: 47
				private int currentAttachmentNumber;
			}
		}

		// Token: 0x0200000D RID: 13
		private class TransportAttachmentHandle : IRetrieverAttachmentHandle
		{
			// Token: 0x06000043 RID: 67 RVA: 0x00003405 File Offset: 0x00001605
			internal TransportAttachmentHandle(int handle)
			{
				this.handle = handle;
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000044 RID: 68 RVA: 0x00003414 File Offset: 0x00001614
			public int AttachNumber
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x04000030 RID: 48
			private readonly int handle;
		}

		// Token: 0x0200000E RID: 14
		private class TransportAttachment : IRetrieverAttachment, IDisposable
		{
			// Token: 0x06000045 RID: 69 RVA: 0x0000341C File Offset: 0x0000161C
			internal TransportAttachment(Attachment attachment)
			{
				this.attachment = attachment;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000046 RID: 70 RVA: 0x0000342B File Offset: 0x0000162B
			public string FileName
			{
				get
				{
					return this.attachment.FileName;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00003438 File Offset: 0x00001638
			public bool IsInline
			{
				get
				{
					return this.attachment.AttachmentType == AttachmentType.Inline;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000048 RID: 72 RVA: 0x00003448 File Offset: 0x00001648
			public bool IsImage
			{
				get
				{
					return this.attachment.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00003460 File Offset: 0x00001660
			public bool IsSupportedAttachMethod
			{
				get
				{
					return !this.attachment.IsOleAttachment;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600004A RID: 74 RVA: 0x00003470 File Offset: 0x00001670
			public long Size
			{
				get
				{
					Stream stream;
					if (this.attachment.TryGetContentReadStream(out stream))
					{
						using (stream)
						{
							return stream.Length;
						}
					}
					return 0L;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600004B RID: 75 RVA: 0x000034B8 File Offset: 0x000016B8
			protected Attachment Attachment
			{
				get
				{
					return this.attachment;
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x000034C0 File Offset: 0x000016C0
			public void Dispose()
			{
			}

			// Token: 0x04000031 RID: 49
			internal const string ImageContentType = "image/";

			// Token: 0x04000032 RID: 50
			private readonly Attachment attachment;
		}

		// Token: 0x0200000F RID: 15
		private class TransportItemAttachment : TransportItemHandlerAdaptor.TransportAttachment, IRetrieverItemAttachment, IRetrieverAttachment, IDisposable
		{
			// Token: 0x0600004D RID: 77 RVA: 0x000034C2 File Offset: 0x000016C2
			internal TransportItemAttachment(Attachment attachment) : base(attachment)
			{
			}

			// Token: 0x0600004E RID: 78 RVA: 0x000034CB File Offset: 0x000016CB
			public IRetrieverItem GetItem()
			{
				return new TransportItemHandlerAdaptor.TransportItem(base.Attachment.EmbeddedMessage, false);
			}
		}

		// Token: 0x02000010 RID: 16
		private class TransportStreamAttachment : TransportItemHandlerAdaptor.TransportAttachment, IRetrieverStreamAttachment, IRetrieverAttachment, IDisposable
		{
			// Token: 0x0600004F RID: 79 RVA: 0x000034DE File Offset: 0x000016DE
			internal TransportStreamAttachment(Attachment attachment) : base(attachment)
			{
			}

			// Token: 0x06000050 RID: 80 RVA: 0x000034E8 File Offset: 0x000016E8
			public Stream TryGetContentStream()
			{
				Stream result;
				base.Attachment.TryGetContentReadStream(out result);
				return result;
			}
		}

		// Token: 0x02000011 RID: 17
		private class PropertyDefinition : IRetrieverPropertyDefinition
		{
			// Token: 0x06000051 RID: 81 RVA: 0x00003504 File Offset: 0x00001704
			internal PropertyDefinition(string name, PropertyDefinitionAttribute attributes)
			{
				this.Name = name;
				this.Attributes = attributes;
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000052 RID: 82 RVA: 0x0000351A File Offset: 0x0000171A
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00003522 File Offset: 0x00001722
			public string Name { get; private set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000054 RID: 84 RVA: 0x0000352B File Offset: 0x0000172B
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00003533 File Offset: 0x00001733
			public PropertyDefinitionAttribute Attributes { get; private set; }
		}

		// Token: 0x02000012 RID: 18
		private class PropertyDefinitionSchema
		{
			// Token: 0x06000056 RID: 86 RVA: 0x0000353C File Offset: 0x0000173C
			private PropertyDefinitionSchema()
			{
				this.AllProperties = new TransportItemHandlerAdaptor.PropertyDefinition[]
				{
					this.Body,
					this.Subject
				};
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00003591 File Offset: 0x00001791
			// (set) Token: 0x06000058 RID: 88 RVA: 0x00003599 File Offset: 0x00001799
			internal TransportItemHandlerAdaptor.PropertyDefinition[] AllProperties { get; private set; }

			// Token: 0x04000035 RID: 53
			internal static readonly TransportItemHandlerAdaptor.PropertyDefinitionSchema Instance = new TransportItemHandlerAdaptor.PropertyDefinitionSchema();

			// Token: 0x04000036 RID: 54
			internal readonly TransportItemHandlerAdaptor.PropertyDefinition Body = new TransportItemHandlerAdaptor.PropertyDefinition("Body", PropertyDefinitionAttribute.PartOfAttachmentAnnotation | PropertyDefinitionAttribute.SkipValueTracing);

			// Token: 0x04000037 RID: 55
			internal readonly TransportItemHandlerAdaptor.PropertyDefinition Subject = new TransportItemHandlerAdaptor.PropertyDefinition("Subject", PropertyDefinitionAttribute.PartOfAttachmentAnnotation);
		}
	}
}
