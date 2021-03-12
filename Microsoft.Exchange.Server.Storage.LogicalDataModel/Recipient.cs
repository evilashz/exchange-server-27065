using System;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000B1 RID: 177
	public class Recipient : PrivatePropertyBag
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x00051452 File Offset: 0x0004F652
		internal Recipient(RecipientCollection parentCollection) : base(false)
		{
			this.parentCollection = parentCollection;
			PropertySchemaPopulation.InitializeRecipient(this.CurrentOperationContext, this);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0005146F File Offset: 0x0004F66F
		internal Recipient(RecipientCollection parentCollection, byte[] propertyBlob) : this(parentCollection)
		{
			base.LoadFromPropertyBlob(this.CurrentOperationContext, propertyBlob);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00051485 File Offset: 0x0004F685
		public override ObjectPropertySchema Schema
		{
			get
			{
				return this.parentCollection.RecipientSchema;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00051492 File Offset: 0x0004F692
		public override Context CurrentOperationContext
		{
			get
			{
				return this.parentCollection.ParentMessage.Mailbox.CurrentOperationContext;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000514A9 File Offset: 0x0004F6A9
		public override ReplidGuidMap ReplidGuidMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000514AC File Offset: 0x0004F6AC
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x000514D6 File Offset: 0x0004F6D6
		public DisplayType DisplayType
		{
			get
			{
				object propertyValue = this.GetPropertyValue(this.CurrentOperationContext, PropTag.Recipient.DisplayType);
				if (propertyValue == null)
				{
					return DisplayType.DT_MAILUSER;
				}
				return (DisplayType)((int)propertyValue);
			}
			set
			{
				this.SetProperty(this.CurrentOperationContext, PropTag.Recipient.DisplayType, (value == DisplayType.DT_MAILUSER) ? null : ((int)value));
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000514F8 File Offset: 0x0004F6F8
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00051522 File Offset: 0x0004F722
		public ObjectType ObjectType
		{
			get
			{
				object propertyValue = this.GetPropertyValue(this.CurrentOperationContext, PropTag.Recipient.ObjectType);
				if (propertyValue == null)
				{
					return ObjectType.MAPI_MAILUSER;
				}
				return (ObjectType)((int)propertyValue);
			}
			set
			{
				this.SetProperty(this.CurrentOperationContext, PropTag.Recipient.ObjectType, (value == ObjectType.MAPI_MAILUSER) ? null : ((int)value));
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00051543 File Offset: 0x0004F743
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0005155B File Offset: 0x0004F75B
		public string Name
		{
			get
			{
				return (string)this.GetPropertyValue(this.CurrentOperationContext, PropTag.Recipient.DisplayName);
			}
			set
			{
				this.SetProperty(this.CurrentOperationContext, PropTag.Recipient.DisplayName, value);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00051570 File Offset: 0x0004F770
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00051588 File Offset: 0x0004F788
		public string Email
		{
			get
			{
				return (string)this.GetPropertyValue(this.CurrentOperationContext, PropTag.Recipient.EmailAddress);
			}
			set
			{
				this.SetProperty(this.CurrentOperationContext, PropTag.Recipient.EmailAddress, value);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0005159D File Offset: 0x0004F79D
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x000515B5 File Offset: 0x0004F7B5
		public string LegacyDn
		{
			get
			{
				return (string)this.GetPropertyValue(this.CurrentOperationContext, PropTag.Recipient.UserDN);
			}
			set
			{
				this.SetProperty(this.CurrentOperationContext, PropTag.Recipient.UserDN, value);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000515CC File Offset: 0x0004F7CC
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x000515F6 File Offset: 0x0004F7F6
		public RecipientType RecipientType
		{
			get
			{
				object propertyValue = this.GetPropertyValue(this.CurrentOperationContext, PropTag.Recipient.RecipientType);
				if (propertyValue == null)
				{
					return RecipientType.To;
				}
				return (RecipientType)((int)propertyValue);
			}
			set
			{
				if (value != this.RecipientType)
				{
					this.SetProperty(this.CurrentOperationContext, PropTag.Recipient.RecipientType, (int)value);
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00051619 File Offset: 0x0004F819
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00051621 File Offset: 0x0004F821
		public int RowId
		{
			get
			{
				return this.rowId;
			}
			set
			{
				this.rowId = value;
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0005162A File Offset: 0x0004F82A
		public void Delete()
		{
			this.parentCollection.Remove(this);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00051638 File Offset: 0x0004F838
		public void ToBinary(Context context, out byte[] propertyBlob)
		{
			propertyBlob = base.SaveToPropertyBlob(context);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00051643 File Offset: 0x0004F843
		protected void OnDelete()
		{
			this.InvalidateMessageComputedProperty(this.RecipientType);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00051654 File Offset: 0x0004F854
		internal void InvalidateMessageComputedProperty(RecipientType type)
		{
			Message parentMessage = this.parentCollection.ParentMessage;
			switch (type)
			{
			case RecipientType.SubmittedTo:
			case RecipientType.SubmittedCc:
			case RecipientType.SubmittedBcc:
				break;
			default:
				switch (type)
				{
				case RecipientType.Orig:
					break;
				case RecipientType.To:
					parentMessage.MarkComputedPropertyAsInvalid(PropTag.Message.DisplayTo);
					return;
				case RecipientType.Cc:
					parentMessage.MarkComputedPropertyAsInvalid(PropTag.Message.DisplayCc);
					return;
				case RecipientType.Bcc:
					parentMessage.MarkComputedPropertyAsInvalid(PropTag.Message.DisplayBcc);
					break;
				default:
					if (type != RecipientType.P1)
					{
						return;
					}
					break;
				}
				break;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000516C9 File Offset: 0x0004F8C9
		protected override void OnDirty(Context context)
		{
			base.OnDirty(context);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000516D2 File Offset: 0x0004F8D2
		protected override void OnPropertyChanged(StorePropTag propTag, long deltaSize)
		{
			this.parentCollection.Changed = true;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000516E0 File Offset: 0x0004F8E0
		protected override StorePropTag MapPropTag(Context context, uint propertyTag)
		{
			return this.parentCollection.ParentMessage.Mailbox.GetStorePropTag(context, propertyTag, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);
		}

		// Token: 0x040004BE RID: 1214
		private RecipientCollection parentCollection;

		// Token: 0x040004BF RID: 1215
		private int rowId;
	}
}
