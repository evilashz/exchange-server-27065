using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E1 RID: 1249
	[XmlType("FindMessageTrackingReportResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMessageTrackingReportResponseMessage : ResponseMessage
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x000A4ECC File Offset: 0x000A30CC
		public FindMessageTrackingReportResponseMessage()
		{
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000A4ED4 File Offset: 0x000A30D4
		public override ResponseType GetResponseType()
		{
			return ResponseType.FindMessageTrackingReportResponseMessage;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000A4ED8 File Offset: 0x000A30D8
		internal FindMessageTrackingReportResponseMessage(ServiceResultCode code, ServiceError error, FindMessageTrackingSearchResultType[] messageTrackingSearchResults, string[] diagnostics, string executedSearchScope, ArrayOfTrackingPropertiesType[] trackingErrors, TrackingPropertyType[] properties) : base(code, error)
		{
			this.MessageTrackingSearchResults = messageTrackingSearchResults;
			this.Diagnostics = diagnostics;
			this.ExecutedSearchScope = executedSearchScope;
			this.Errors = trackingErrors;
			this.Properties = properties;
		}

		// Token: 0x04001582 RID: 5506
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Diagnostics;

		// Token: 0x04001583 RID: 5507
		[XmlArrayItem("MessageTrackingSearchResult", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FindMessageTrackingSearchResultType[] MessageTrackingSearchResults;

		// Token: 0x04001584 RID: 5508
		public string ExecutedSearchScope;

		// Token: 0x04001585 RID: 5509
		[XmlArrayItem("ArrayOfTrackingPropertiesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ArrayOfTrackingPropertiesType[] Errors;

		// Token: 0x04001586 RID: 5510
		[XmlArrayItem("TrackingPropertyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
