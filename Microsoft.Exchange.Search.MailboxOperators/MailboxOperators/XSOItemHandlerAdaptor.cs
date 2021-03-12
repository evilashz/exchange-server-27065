using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200001D RID: 29
	internal static class XSOItemHandlerAdaptor
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00009428 File Offset: 0x00007628
		internal static IRetrieverItem WrapXSOItem(Item item)
		{
			return XSOItemHandlerAdaptor.WrapXSOItem(item, SearchConfig.Instance.IRMMessageProcessingEnabled);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000943A File Offset: 0x0000763A
		internal static IRetrieverItem WrapXSOItem(Item item, bool irmMessageProcessingEnabled)
		{
			return new XSOItemHandlerAdaptor.XSOItem(item, irmMessageProcessingEnabled);
		}

		// Token: 0x0200001E RID: 30
		private class XSOItem : IRetrieverItem, IDisposable
		{
			// Token: 0x060001BD RID: 445 RVA: 0x00009443 File Offset: 0x00007643
			internal XSOItem(Item item, bool iRMMessageProcessingEnabled)
			{
				this.item = item;
				this.iRMMessageProcessingEnabled = iRMMessageProcessingEnabled;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001BE RID: 446 RVA: 0x0000945C File Offset: 0x0000765C
			public IRetrieverAttachmentCollection AttachmentCollection
			{
				get
				{
					if (this.attachmentCollection == null)
					{
						RightsManagedMessageItem rightsManagedMessageItem = this.item as RightsManagedMessageItem;
						XSOItemHandlerAdaptor.XSOAttachmentCollection xsoattachmentCollection;
						if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && this.iRMMessageProcessingEnabled)
						{
							xsoattachmentCollection = new XSOItemHandlerAdaptor.XSOAttachmentCollection(rightsManagedMessageItem.ProtectedAttachmentCollection, true, rightsManagedMessageItem);
						}
						else
						{
							xsoattachmentCollection = new XSOItemHandlerAdaptor.XSOAttachmentCollection(this.item.AttachmentCollection, this.iRMMessageProcessingEnabled);
						}
						this.attachmentCollection = xsoattachmentCollection;
					}
					return this.attachmentCollection;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060001BF RID: 447 RVA: 0x000094C4 File Offset: 0x000076C4
			public IEnumerable<IRetrieverPropertyDefinition> EmbeddedMessageProperties
			{
				get
				{
					return FastDocumentSchema.Instance.GetEmbeddedMessageProperties(this.item);
				}
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x000094D8 File Offset: 0x000076D8
			public object GetValue(IRetrieverPropertyDefinition propertyDefinition)
			{
				object result = null;
				try
				{
					result = ((FastPropertyDefinition)propertyDefinition).GetValue(this.item);
				}
				catch (PropertyErrorException)
				{
				}
				catch (NotInBagPropertyErrorException)
				{
				}
				return result;
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x00009520 File Offset: 0x00007720
			public void Dispose()
			{
				if (this.item != null)
				{
					this.item.Dispose();
				}
			}

			// Token: 0x04000153 RID: 339
			private readonly Item item;

			// Token: 0x04000154 RID: 340
			private readonly bool iRMMessageProcessingEnabled;

			// Token: 0x04000155 RID: 341
			private XSOItemHandlerAdaptor.XSOAttachmentCollection attachmentCollection;
		}

		// Token: 0x0200001F RID: 31
		private class XSOAttachmentCollection : IRetrieverAttachmentCollection, IEnumerable<IRetrieverAttachmentHandle>, IEnumerable
		{
			// Token: 0x060001C2 RID: 450 RVA: 0x00009535 File Offset: 0x00007735
			internal XSOAttachmentCollection(AttachmentCollection attachmentCollection, bool iRMMessageProcessingEnabled) : this(attachmentCollection, false, null)
			{
				this.iRMMessageProcessingEnabled = iRMMessageProcessingEnabled;
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x00009547 File Offset: 0x00007747
			internal XSOAttachmentCollection(AttachmentCollection attachmentCollection, bool isRightsManaged, RightsManagedMessageItem rmsMessage)
			{
				this.attachmentCollection = attachmentCollection;
				this.isRightsManaged = isRightsManaged;
				if (!isRightsManaged)
				{
					return;
				}
				if (rmsMessage != null)
				{
					this.rmsMessage = rmsMessage;
					return;
				}
				throw new InvalidOperationException("The rights managed message must be passed along to open the attachment collection.");
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009576 File Offset: 0x00007776
			public int Count
			{
				get
				{
					return this.attachmentCollection.GetAllHandles().Count;
				}
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00009588 File Offset: 0x00007788
			public IEnumerator<IRetrieverAttachmentHandle> GetEnumerator()
			{
				return new XSOItemHandlerAdaptor.XSOAttachmentCollection.Enumerator(this.attachmentCollection.GetAllHandles().GetEnumerator());
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x0000959F File Offset: 0x0000779F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x000095A8 File Offset: 0x000077A8
			public IRetrieverAttachment Open(IRetrieverAttachmentHandle handle)
			{
				XSOItemHandlerAdaptor.XSOAttachmentHandle xsoattachmentHandle = (XSOItemHandlerAdaptor.XSOAttachmentHandle)handle;
				IRetrieverAttachment result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					Attachment attachment = this.attachmentCollection.Open(xsoattachmentHandle.Handle);
					disposeGuard.Add<Attachment>(attachment);
					ItemAttachment itemAttachment = attachment as ItemAttachment;
					if (itemAttachment != null)
					{
						disposeGuard.Success();
						result = new XSOItemHandlerAdaptor.XSOItemAttachment(itemAttachment, this.iRMMessageProcessingEnabled);
					}
					else
					{
						if (this.isRightsManaged)
						{
							try
							{
								this.rmsMessage.UnprotectAttachment(attachment.Id);
							}
							catch (NullReferenceException)
							{
								return null;
							}
							catch (ObjectNotFoundException)
							{
								return null;
							}
							catch (ArgumentException)
							{
								return null;
							}
							catch (RightsManagementPermanentException)
							{
								return null;
							}
							catch (RightsManagementTransientException)
							{
								return null;
							}
						}
						disposeGuard.Success();
						result = new XSOItemHandlerAdaptor.XSOStreamAttachment((StreamAttachmentBase)attachment);
					}
				}
				return result;
			}

			// Token: 0x04000156 RID: 342
			private readonly AttachmentCollection attachmentCollection;

			// Token: 0x04000157 RID: 343
			private readonly bool isRightsManaged;

			// Token: 0x04000158 RID: 344
			private readonly RightsManagedMessageItem rmsMessage;

			// Token: 0x04000159 RID: 345
			private readonly bool iRMMessageProcessingEnabled;

			// Token: 0x02000020 RID: 32
			private class Enumerator : IEnumerator<IRetrieverAttachmentHandle>, IDisposable, IEnumerator
			{
				// Token: 0x060001C8 RID: 456 RVA: 0x000096AC File Offset: 0x000078AC
				internal Enumerator(IEnumerator<AttachmentHandle> enumerator)
				{
					this.enumerator = enumerator;
				}

				// Token: 0x1700007E RID: 126
				// (get) Token: 0x060001C9 RID: 457 RVA: 0x000096BB File Offset: 0x000078BB
				public IRetrieverAttachmentHandle Current
				{
					get
					{
						if (this.current == null)
						{
							this.current = new XSOItemHandlerAdaptor.XSOAttachmentHandle(this.enumerator.Current);
						}
						return this.current;
					}
				}

				// Token: 0x1700007F RID: 127
				// (get) Token: 0x060001CA RID: 458 RVA: 0x000096E1 File Offset: 0x000078E1
				object IEnumerator.Current
				{
					get
					{
						return ((IEnumerator<IRetrieverAttachmentHandle>)this).Current;
					}
				}

				// Token: 0x060001CB RID: 459 RVA: 0x000096E9 File Offset: 0x000078E9
				public void Dispose()
				{
					this.Reset();
				}

				// Token: 0x060001CC RID: 460 RVA: 0x000096F1 File Offset: 0x000078F1
				public bool MoveNext()
				{
					this.current = null;
					return this.enumerator.MoveNext();
				}

				// Token: 0x060001CD RID: 461 RVA: 0x00009705 File Offset: 0x00007905
				public void Reset()
				{
					this.current = null;
					this.enumerator.Reset();
				}

				// Token: 0x0400015A RID: 346
				private readonly IEnumerator<AttachmentHandle> enumerator;

				// Token: 0x0400015B RID: 347
				private XSOItemHandlerAdaptor.XSOAttachmentHandle current;
			}
		}

		// Token: 0x02000021 RID: 33
		private class XSOAttachmentHandle : IRetrieverAttachmentHandle
		{
			// Token: 0x060001CE RID: 462 RVA: 0x00009719 File Offset: 0x00007919
			internal XSOAttachmentHandle(AttachmentHandle handle)
			{
				this.handle = handle;
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060001CF RID: 463 RVA: 0x00009728 File Offset: 0x00007928
			public int AttachNumber
			{
				get
				{
					return this.handle.AttachNumber;
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00009735 File Offset: 0x00007935
			internal AttachmentHandle Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x0400015C RID: 348
			private readonly AttachmentHandle handle;
		}

		// Token: 0x02000022 RID: 34
		private class XSOAttachment : IRetrieverAttachment, IDisposable
		{
			// Token: 0x060001D1 RID: 465 RVA: 0x0000973D File Offset: 0x0000793D
			internal XSOAttachment(Attachment attachment)
			{
				this.attachment = attachment;
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000974C File Offset: 0x0000794C
			public string FileName
			{
				get
				{
					return this.attachment.FileName;
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060001D3 RID: 467 RVA: 0x00009759 File Offset: 0x00007959
			public bool IsInline
			{
				get
				{
					return this.attachment.IsInline;
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009766 File Offset: 0x00007966
			public bool IsImage
			{
				get
				{
					return !string.IsNullOrEmpty(this.attachment.ContentType) && this.attachment.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060001D5 RID: 469 RVA: 0x00009794 File Offset: 0x00007994
			public bool IsSupportedAttachMethod
			{
				get
				{
					object obj = this.attachment.TryGetProperty(AttachmentSchema.AttachMethod);
					if (obj is PropertyError)
					{
						return false;
					}
					AttachMethods attachMethods = (AttachMethods)obj;
					return attachMethods == AttachMethods.EmbeddedMessage || attachMethods == AttachMethods.ByValue;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060001D6 RID: 470 RVA: 0x000097CE File Offset: 0x000079CE
			public long Size
			{
				get
				{
					return this.attachment.Size;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060001D7 RID: 471 RVA: 0x000097DB File Offset: 0x000079DB
			protected Attachment Attachment
			{
				get
				{
					return this.attachment;
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001D8 RID: 472 RVA: 0x000097E3 File Offset: 0x000079E3
			protected List<Item> Placeholders
			{
				get
				{
					if (this.placeholders == null)
					{
						this.placeholders = new List<Item>();
					}
					return this.placeholders;
				}
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x00009800 File Offset: 0x00007A00
			public void Dispose()
			{
				if (this.attachment != null)
				{
					this.attachment.Dispose();
				}
				if (this.placeholders != null)
				{
					foreach (Item item in this.placeholders)
					{
						item.Dispose();
					}
				}
			}

			// Token: 0x0400015D RID: 349
			private readonly Attachment attachment;

			// Token: 0x0400015E RID: 350
			private List<Item> placeholders;
		}

		// Token: 0x02000023 RID: 35
		private class XSOItemAttachment : XSOItemHandlerAdaptor.XSOAttachment, IRetrieverItemAttachment, IRetrieverAttachment, IDisposable
		{
			// Token: 0x060001DA RID: 474 RVA: 0x00009870 File Offset: 0x00007A70
			internal XSOItemAttachment(ItemAttachment attachment, bool iRMMessageProcessingEnabled) : base(attachment)
			{
				this.iRMMessageProcessingEnabled = iRMMessageProcessingEnabled;
			}

			// Token: 0x060001DB RID: 475 RVA: 0x00009880 File Offset: 0x00007A80
			public IRetrieverItem GetItem()
			{
				bool flag = false;
				Item item = null;
				IRetrieverItem result;
				try
				{
					item = ((ItemAttachment)base.Attachment).GetItem();
					if (item == null)
					{
						result = null;
					}
					else
					{
						StoreSession session = item.Session;
						Item item2;
						switch (RightsManagement.ProcessSMIMEMessage(session, item, out item2))
						{
						case RightsManagementProcessingResult.IsSMIME:
							if (item2 != null)
							{
								base.Placeholders.Add(item);
								item = item2;
							}
							break;
						case RightsManagementProcessingResult.FailedTransient:
						case RightsManagementProcessingResult.FailedPermanent:
						case RightsManagementProcessingResult.Skipped:
							return null;
						}
						RightsManagementProcessingResult rightsManagementProcessingResult = RightsManagement.ProcessRightsManagedMessage(session, item);
						if (rightsManagementProcessingResult == RightsManagementProcessingResult.NotRightsManaged || rightsManagementProcessingResult == RightsManagementProcessingResult.Success)
						{
							flag = true;
							result = new XSOItemHandlerAdaptor.XSOItem(item, this.iRMMessageProcessingEnabled);
						}
						else
						{
							result = null;
						}
					}
				}
				finally
				{
					if (!flag && item != null)
					{
						item.Dispose();
					}
				}
				return result;
			}

			// Token: 0x0400015F RID: 351
			private readonly bool iRMMessageProcessingEnabled;
		}

		// Token: 0x02000024 RID: 36
		private class XSOStreamAttachment : XSOItemHandlerAdaptor.XSOAttachment, IRetrieverStreamAttachment, IRetrieverAttachment, IDisposable
		{
			// Token: 0x060001DC RID: 476 RVA: 0x0000993C File Offset: 0x00007B3C
			internal XSOStreamAttachment(StreamAttachmentBase attachment) : base(attachment)
			{
			}

			// Token: 0x060001DD RID: 477 RVA: 0x00009945 File Offset: 0x00007B45
			public Stream TryGetContentStream()
			{
				return ((StreamAttachmentBase)base.Attachment).TryGetContentStream(PropertyOpenMode.ReadOnly);
			}
		}
	}
}
