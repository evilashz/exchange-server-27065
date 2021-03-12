using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	internal class EntityContentProperty : EntityProperty, IContent16Property, IContent14Property, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x000614E2 File Offset: 0x0005F6E2
		public EntityContentProperty() : base(SchematizedObject<EventSchema>.SchemaInstance.BodyProperty, PropertyType.ReadWrite, false)
		{
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000614F6 File Offset: 0x0005F6F6
		public EntityContentProperty(PropertyType type) : base(SchematizedObject<EventSchema>.SchemaInstance.BodyProperty, type, false)
		{
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0006150A File Offset: 0x0005F70A
		public bool IsOnSMIMEMessage
		{
			get
			{
				throw new NotImplementedException("IsOnSMIMEMessage");
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00061516 File Offset: 0x0005F716
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00061522 File Offset: 0x0005F722
		public Stream MIMEData
		{
			get
			{
				throw new NotImplementedException("get_MIMEData");
			}
			set
			{
				throw new NotImplementedException("set_MIMEData");
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0006152E File Offset: 0x0005F72E
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x0006153A File Offset: 0x0005F73A
		public long MIMESize
		{
			get
			{
				throw new NotImplementedException("get_MIMESize");
			}
			set
			{
				throw new NotImplementedException("set_MIMESize");
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00061546 File Offset: 0x0005F746
		public string Preview
		{
			get
			{
				if (base.Item == null)
				{
					return null;
				}
				return base.Item.Preview;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0006155D File Offset: 0x0005F75D
		public Stream Body
		{
			get
			{
				if (this.itemBody == null || this.itemBody.Content == null)
				{
					return null;
				}
				return new MemoryStream(Encoding.UTF8.GetBytes(this.itemBody.Content));
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00061590 File Offset: 0x0005F790
		public string BodyString
		{
			get
			{
				if (this.itemBody == null || this.itemBody.Content == null)
				{
					return null;
				}
				return this.itemBody.Content;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x000615B4 File Offset: 0x0005F7B4
		public long Size
		{
			get
			{
				if (this.itemBody == null || this.itemBody.Content == null)
				{
					return 0L;
				}
				return (long)this.itemBody.Content.Length;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x000615DF File Offset: 0x0005F7DF
		public bool IsIrmErrorMessage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000615E2 File Offset: 0x0005F7E2
		public override void Bind(IItem item)
		{
			base.Bind(item);
			this.itemBody = base.Item.Body;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000615FC File Offset: 0x0005F7FC
		public override void Unbind()
		{
			try
			{
				this.itemBody = null;
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0006162C File Offset: 0x0005F82C
		public Stream GetData(Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType type, long truncationSize, out long estimatedDataSize, out IEnumerable<AirSyncAttachmentInfo> attachments)
		{
			estimatedDataSize = this.Size;
			attachments = null;
			if (this.GetNativeType() == type)
			{
				return new MemoryStream(Encoding.UTF8.GetBytes(this.itemBody.Content.Substring(0, (int)((truncationSize > -1L) ? Math.Min(truncationSize, estimatedDataSize) : estimatedDataSize))));
			}
			throw new NotImplementedException(string.Format("EntityContentProperty.GetData of type {0} when the item has body type of {1}", type, this.GetNativeType()));
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000616A4 File Offset: 0x0005F8A4
		public Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType GetNativeType()
		{
			if (this.itemBody == null)
			{
				return Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType.None;
			}
			switch (this.itemBody.ContentType)
			{
			case Microsoft.Exchange.Entities.DataModel.Items.BodyType.Text:
				return Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType.PlainText;
			case Microsoft.Exchange.Entities.DataModel.Items.BodyType.Html:
				return Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType.Html;
			default:
				throw new NotImplementedException(string.Format("Unable to convert body content type {0}", this.itemBody.ContentType));
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000616FA File Offset: 0x0005F8FA
		public void PreProcessProperty()
		{
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000616FC File Offset: 0x0005F8FC
		public void PostProcessProperty()
		{
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00061700 File Offset: 0x0005F900
		public override void CopyFrom(IProperty srcProperty)
		{
			IContent16Property content16Property = srcProperty as IContent16Property;
			if (content16Property != null)
			{
				ItemBody itemBody = new ItemBody();
				switch (content16Property.GetNativeType())
				{
				case Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType.None:
					base.Item.Body = null;
					return;
				case Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType.PlainText:
					itemBody.ContentType = Microsoft.Exchange.Entities.DataModel.Items.BodyType.Text;
					break;
				case Microsoft.Exchange.AirSync.SchemaConverter.Common.BodyType.Html:
					itemBody.ContentType = Microsoft.Exchange.Entities.DataModel.Items.BodyType.Html;
					break;
				default:
					throw new NotImplementedException(string.Format("Unable to convert content type {0}", content16Property.GetNativeType()));
				}
				itemBody.Content = content16Property.BodyString;
				base.Item.Body = itemBody;
			}
		}

		// Token: 0x04000B44 RID: 2884
		private ItemBody itemBody;
	}
}
