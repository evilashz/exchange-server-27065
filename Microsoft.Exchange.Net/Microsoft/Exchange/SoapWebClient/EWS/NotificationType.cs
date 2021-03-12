using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200030F RID: 783
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class NotificationType
	{
		// Token: 0x04001303 RID: 4867
		public string SubscriptionId;

		// Token: 0x04001304 RID: 4868
		public string PreviousWatermark;

		// Token: 0x04001305 RID: 4869
		public bool MoreEvents;

		// Token: 0x04001306 RID: 4870
		[XmlIgnore]
		public bool MoreEventsSpecified;

		// Token: 0x04001307 RID: 4871
		[XmlElement("StatusEvent", typeof(BaseNotificationEventType))]
		[XmlElement("CopiedEvent", typeof(MovedCopiedEventType))]
		[XmlElement("DeletedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("MovedEvent", typeof(MovedCopiedEventType))]
		[XmlElement("FreeBusyChangedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("NewMailEvent", typeof(BaseObjectChangedEventType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("CreatedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("ModifiedEvent", typeof(ModifiedEventType))]
		public BaseNotificationEventType[] Items;

		// Token: 0x04001308 RID: 4872
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType[] ItemsElementName;
	}
}
