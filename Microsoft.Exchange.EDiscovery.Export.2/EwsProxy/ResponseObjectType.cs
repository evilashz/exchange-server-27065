using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000119 RID: 281
	[DebuggerStepThrough]
	[XmlInclude(typeof(AcceptItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[XmlInclude(typeof(RemoveItemType))]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(ReferenceItemResponseType))]
	[XmlInclude(typeof(AcceptSharingInvitationType))]
	[XmlInclude(typeof(SuppressReadReceiptType))]
	[XmlInclude(typeof(SmartResponseBaseType))]
	[XmlInclude(typeof(SmartResponseType))]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[XmlInclude(typeof(WellKnownResponseObjectType))]
	[XmlInclude(typeof(MeetingRegistrationResponseObjectType))]
	[XmlInclude(typeof(DeclineItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class ResponseObjectType : ResponseObjectCoreType
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00021C6A File Offset: 0x0001FE6A
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00021C72 File Offset: 0x0001FE72
		[XmlAttribute]
		public string ObjectName
		{
			get
			{
				return this.objectNameField;
			}
			set
			{
				this.objectNameField = value;
			}
		}

		// Token: 0x040008FD RID: 2301
		private string objectNameField;
	}
}
