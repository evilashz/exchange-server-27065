using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000612 RID: 1554
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PostItem")]
	[Serializable]
	public class PostItemType : ItemType, IRelatedItemInfo
	{
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000B67C1 File Offset: 0x000B49C1
		// (set) Token: 0x060030AE RID: 12462 RVA: 0x000B67D3 File Offset: 0x000B49D3
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] ConversationIndex
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<byte[]>(PostItemSchema.ConversationIndex);
			}
			set
			{
				base.PropertyBag[PostItemSchema.ConversationIndex] = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000B67E8 File Offset: 0x000B49E8
		// (set) Token: 0x060030B0 RID: 12464 RVA: 0x000B6807 File Offset: 0x000B4A07
		[XmlIgnore]
		[DataMember(Name = "ConversationIndex", EmitDefaultValue = false, Order = 1)]
		public string ConversationIndexString
		{
			get
			{
				byte[] conversationIndex = this.ConversationIndex;
				if (conversationIndex == null)
				{
					return null;
				}
				return Convert.ToBase64String(conversationIndex);
			}
			set
			{
				this.ConversationIndex = ((value != null) ? Convert.FromBase64String(value) : null);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x000B681B File Offset: 0x000B4A1B
		// (set) Token: 0x060030B2 RID: 12466 RVA: 0x000B682D File Offset: 0x000B4A2D
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string ConversationTopic
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PostItemSchema.ConversationTopic);
			}
			set
			{
				base.PropertyBag[PostItemSchema.ConversationTopic] = value;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x000B6840 File Offset: 0x000B4A40
		// (set) Token: 0x060030B4 RID: 12468 RVA: 0x000B6852 File Offset: 0x000B4A52
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public SingleRecipientType From
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(PostItemSchema.From);
			}
			set
			{
				base.PropertyBag[PostItemSchema.From] = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000B6865 File Offset: 0x000B4A65
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x000B6877 File Offset: 0x000B4A77
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string InternetMessageId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PostItemSchema.InternetMessageId);
			}
			set
			{
				base.PropertyBag[PostItemSchema.InternetMessageId] = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000B688A File Offset: 0x000B4A8A
		// (set) Token: 0x060030B8 RID: 12472 RVA: 0x000B68A1 File Offset: 0x000B4AA1
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public bool? IsRead
		{
			get
			{
				return new bool?(base.PropertyBag.GetValueOrDefault<bool>(PostItemSchema.IsRead));
			}
			set
			{
				base.PropertyBag[PostItemSchema.IsRead] = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000B68B9 File Offset: 0x000B4AB9
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x000B68C6 File Offset: 0x000B4AC6
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsReadSpecified
		{
			get
			{
				return base.IsSet(PostItemSchema.IsRead);
			}
			set
			{
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000B68C8 File Offset: 0x000B4AC8
		// (set) Token: 0x060030BC RID: 12476 RVA: 0x000B68DA File Offset: 0x000B4ADA
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string PostedTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PostItemSchema.PostedTime);
			}
			set
			{
				base.PropertyBag[PostItemSchema.PostedTime] = value;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000B68ED File Offset: 0x000B4AED
		// (set) Token: 0x060030BE RID: 12478 RVA: 0x000B68FA File Offset: 0x000B4AFA
		[IgnoreDataMember]
		[XmlIgnore]
		public bool PostedTimeSpecified
		{
			get
			{
				return base.IsSet(PostItemSchema.PostedTime);
			}
			set
			{
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000B68FC File Offset: 0x000B4AFC
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x000B690E File Offset: 0x000B4B0E
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string References
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PostItemSchema.References);
			}
			set
			{
				base.PropertyBag[PostItemSchema.References] = value;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000B6921 File Offset: 0x000B4B21
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x000B6933 File Offset: 0x000B4B33
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public SingleRecipientType Sender
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(PostItemSchema.Sender);
			}
			set
			{
				base.PropertyBag[PostItemSchema.Sender] = value;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000B6946 File Offset: 0x000B4B46
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.Post;
			}
		}
	}
}
