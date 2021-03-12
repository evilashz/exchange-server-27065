using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000118 RID: 280
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(SmartResponseBaseType))]
	[XmlInclude(typeof(SmartResponseType))]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[XmlInclude(typeof(WellKnownResponseObjectType))]
	[XmlInclude(typeof(SuppressReadReceiptType))]
	[XmlInclude(typeof(DeclineItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[XmlInclude(typeof(AcceptItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(MeetingRegistrationResponseObjectType))]
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[XmlInclude(typeof(RemoveItemType))]
	[XmlInclude(typeof(ResponseObjectType))]
	[XmlInclude(typeof(ReferenceItemResponseType))]
	[XmlInclude(typeof(AcceptSharingInvitationType))]
	[Serializable]
	public abstract class ResponseObjectCoreType : MessageType
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00021C51 File Offset: 0x0001FE51
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00021C59 File Offset: 0x0001FE59
		public ItemIdType ReferenceItemId
		{
			get
			{
				return this.referenceItemIdField;
			}
			set
			{
				this.referenceItemIdField = value;
			}
		}

		// Token: 0x040008FC RID: 2300
		private ItemIdType referenceItemIdField;
	}
}
