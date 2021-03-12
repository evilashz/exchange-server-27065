using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000502 RID: 1282
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetMessageTrackingReportResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetMessageTrackingReportResponseMessage : ResponseMessage
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x000A5661 File Offset: 0x000A3861
		public GetMessageTrackingReportResponseMessage()
		{
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000A5669 File Offset: 0x000A3869
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetMessageTrackingReportResponseMessage;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000A566D File Offset: 0x000A386D
		internal GetMessageTrackingReportResponseMessage(ServiceResultCode code, ServiceError error, MessageTrackingReport messageTrackingReport, string[] diagnostics, ArrayOfTrackingPropertiesType[] trackingErrors, TrackingPropertyType[] properties) : base(code, error)
		{
			this.MessageTrackingReport = messageTrackingReport;
			this.Diagnostics = diagnostics;
			this.Errors = trackingErrors;
			this.Properties = properties;
		}

		// Token: 0x040015A8 RID: 5544
		public MessageTrackingReport MessageTrackingReport;

		// Token: 0x040015A9 RID: 5545
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Diagnostics;

		// Token: 0x040015AA RID: 5546
		[XmlArrayItem("ArrayOfTrackingPropertiesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ArrayOfTrackingPropertiesType[] Errors;

		// Token: 0x040015AB RID: 5547
		[XmlArrayItem("TrackingPropertyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
