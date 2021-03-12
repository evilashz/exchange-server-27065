using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000808 RID: 2056
	[XmlType(TypeName = "RecipientTrackingEventType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RecipientTrackingEvent
	{
		// Token: 0x06003BFC RID: 15356 RVA: 0x000D4FF8 File Offset: 0x000D31F8
		internal bool TryGetInternalId(out long internalId)
		{
			internalId = 0L;
			return !string.IsNullOrEmpty(this.InternalId) && long.TryParse(this.InternalId, out internalId) && internalId >= 0L;
		}

		// Token: 0x0400212B RID: 8491
		public DateTime Date;

		// Token: 0x0400212C RID: 8492
		public EmailAddressWrapper Recipient;

		// Token: 0x0400212D RID: 8493
		public string DeliveryStatus;

		// Token: 0x0400212E RID: 8494
		public string EventDescription;

		// Token: 0x0400212F RID: 8495
		[XmlArrayItem("String", IsNullable = false)]
		public string[] EventData;

		// Token: 0x04002130 RID: 8496
		public string Server;

		// Token: 0x04002131 RID: 8497
		[XmlElement(DataType = "nonNegativeInteger")]
		public string InternalId;

		// Token: 0x04002132 RID: 8498
		public string UniquePathId;

		// Token: 0x04002133 RID: 8499
		public bool HiddenRecipient;

		// Token: 0x04002134 RID: 8500
		public bool BccRecipient;

		// Token: 0x04002135 RID: 8501
		public string RootAddress;

		// Token: 0x04002136 RID: 8502
		[XmlArrayItem("TrackingPropertyType", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
