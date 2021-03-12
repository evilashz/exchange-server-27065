using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200022E RID: 558
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NotificationType
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000263CC File Offset: 0x000245CC
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x000263D4 File Offset: 0x000245D4
		public string SubscriptionId
		{
			get
			{
				return this.subscriptionIdField;
			}
			set
			{
				this.subscriptionIdField = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x000263DD File Offset: 0x000245DD
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x000263E5 File Offset: 0x000245E5
		public string PreviousWatermark
		{
			get
			{
				return this.previousWatermarkField;
			}
			set
			{
				this.previousWatermarkField = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x000263EE File Offset: 0x000245EE
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x000263F6 File Offset: 0x000245F6
		public bool MoreEvents
		{
			get
			{
				return this.moreEventsField;
			}
			set
			{
				this.moreEventsField = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000263FF File Offset: 0x000245FF
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x00026407 File Offset: 0x00024607
		[XmlIgnore]
		public bool MoreEventsSpecified
		{
			get
			{
				return this.moreEventsFieldSpecified;
			}
			set
			{
				this.moreEventsFieldSpecified = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00026410 File Offset: 0x00024610
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x00026418 File Offset: 0x00024618
		[XmlElement("DeletedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("CreatedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("FreeBusyChangedEvent", typeof(BaseObjectChangedEventType))]
		[XmlElement("ModifiedEvent", typeof(ModifiedEventType))]
		[XmlElement("MovedEvent", typeof(MovedCopiedEventType))]
		[XmlElement("NewMailEvent", typeof(BaseObjectChangedEventType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("CopiedEvent", typeof(MovedCopiedEventType))]
		[XmlElement("StatusEvent", typeof(BaseNotificationEventType))]
		public BaseNotificationEventType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00026421 File Offset: 0x00024621
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00026429 File Offset: 0x00024629
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType[] ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x04000EB1 RID: 3761
		private string subscriptionIdField;

		// Token: 0x04000EB2 RID: 3762
		private string previousWatermarkField;

		// Token: 0x04000EB3 RID: 3763
		private bool moreEventsField;

		// Token: 0x04000EB4 RID: 3764
		private bool moreEventsFieldSpecified;

		// Token: 0x04000EB5 RID: 3765
		private BaseNotificationEventType[] itemsField;

		// Token: 0x04000EB6 RID: 3766
		private ItemsChoiceType[] itemsElementNameField;
	}
}
