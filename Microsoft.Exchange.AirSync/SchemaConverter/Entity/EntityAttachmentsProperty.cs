using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	internal class EntityAttachmentsProperty : EntityProperty, IAttachments12Property, IMultivaluedProperty<Attachment12Data>, IProperty, IEnumerable<Attachment12Data>, IEnumerable
	{
		// Token: 0x060011B6 RID: 4534 RVA: 0x00060C60 File Offset: 0x0005EE60
		public EntityAttachmentsProperty() : base(SchematizedObject<EventSchema>.SchemaInstance.AttachmentsProperty, PropertyType.ReadWrite, false)
		{
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00060C74 File Offset: 0x0005EE74
		public int Count
		{
			get
			{
				if (this.values == null)
				{
					return 0;
				}
				return this.values.Count;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00060C8B File Offset: 0x0005EE8B
		public override void Bind(IItem item)
		{
			base.Bind(item);
			this.values = base.Item.Attachments;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		public override void Unbind()
		{
			try
			{
				this.values = null;
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00060E3C File Offset: 0x0005F03C
		public IEnumerator<Attachment12Data> GetEnumerator()
		{
			if (this.values != null)
			{
				foreach (IAttachment attachment in this.values)
				{
					Attachment12Data attachment12Data = this.CreateAttachment14Data(attachment);
					if (attachment12Data != null)
					{
						yield return attachment12Data;
					}
				}
			}
			yield break;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00060E58 File Offset: 0x0005F058
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00060E60 File Offset: 0x0005F060
		public override void CopyFrom(IProperty srcProperty)
		{
			IAttachments12Property attachments12Property = srcProperty as IAttachments12Property;
			if (attachments12Property == null)
			{
				throw new UnexpectedTypeException("IAttachments12Property", srcProperty);
			}
			List<IAttachment> list = new List<IAttachment>(attachments12Property.Count);
			foreach (Attachment12Data attachment12Data in attachments12Property)
			{
				Attachment16Data attachment16Data = attachment12Data as Attachment16Data;
				if (attachment16Data == null)
				{
					throw new UnexpectedTypeException("Attachment16Data", attachment12Data);
				}
				IAttachment attachment = null;
				if (attachment16Data.ChangeType == AttachmentAction.Add)
				{
					byte method = attachment16Data.Method;
					if (method != 1)
					{
						if (method != 5)
						{
						}
						throw new ConversionException(string.Format("The attachment method \"{0}\" is not suported.", attachment16Data.Method));
					}
					attachment = new FileAttachment
					{
						ContentId = attachment16Data.ContentId,
						ContentLocation = attachment16Data.ContentLocation,
						Content = attachment16Data.Content
					};
					attachment.Name = attachment16Data.DisplayName;
					attachment.IsInline = attachment16Data.IsInline;
					attachment.ContentType = attachment16Data.ContentType;
					attachment.Id = attachment16Data.ClientId;
				}
				else if (attachment16Data.ChangeType == AttachmentAction.Delete)
				{
					if (!attachment16Data.FileReference.StartsWith(base.Item.Id + ":"))
					{
						throw new ConversionException(string.Format("The attachment \"{0}\" does not belog to the item \"{1}\".", attachment16Data.FileReference, base.Item.Id));
					}
					attachment = new EntityDeleteAttachment();
					attachment.Id = attachment16Data.FileReference.Substring(base.Item.Id.Length + 1);
				}
				list.Add(attachment);
			}
			base.Item.Attachments = list;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00061024 File Offset: 0x0005F224
		private Attachment14Data CreateAttachment14Data(IAttachment attachment)
		{
			Attachment14Data attachment14Data = new Attachment14Data();
			attachment14Data.DisplayName = attachment.Name;
			attachment14Data.EstimatedDataSize = attachment.Size;
			attachment14Data.IsInline = attachment.IsInline;
			attachment14Data.FileReference = HttpUtility.UrlEncode(base.Item.Id + ":" + attachment.Id);
			attachment14Data.Id = EntitySyncItemId.GetAttachmentId(attachment.Id);
			FileAttachment fileAttachment = attachment as FileAttachment;
			ItemAttachment itemAttachment = attachment as ItemAttachment;
			if (fileAttachment != null)
			{
				attachment14Data.Method = 1;
				attachment14Data.ContentId = fileAttachment.ContentId;
				attachment14Data.ContentLocation = fileAttachment.ContentLocation;
				if (fileAttachment.Content != null)
				{
					attachment14Data.EstimatedDataSize = (long)fileAttachment.Content.Length;
				}
			}
			else
			{
				if (itemAttachment == null)
				{
					throw new ConversionException(string.Format("Attachment type \"{0}\" is not supported.", attachment.GetType().FullName));
				}
				attachment14Data.Method = 5;
			}
			return attachment14Data;
		}

		// Token: 0x04000B42 RID: 2882
		private List<IAttachment> values;
	}
}
