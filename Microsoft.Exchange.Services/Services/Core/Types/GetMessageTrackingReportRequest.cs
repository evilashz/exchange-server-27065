using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200043F RID: 1087
	[XmlType("GetMessageTrackingReportRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetMessageTrackingReportRequest : BaseRequest, IMessageTrackingRequestLogInformation
	{
		// Token: 0x06001FE2 RID: 8162 RVA: 0x000A14BC File Offset: 0x0009F6BC
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetMessageTrackingReport(callContext, this);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x000A14C5 File Offset: 0x0009F6C5
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000A14C8 File Offset: 0x0009F6C8
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x000A14CC File Offset: 0x0009F6CC
		public void AddRequestDataForLogging(List<KeyValuePair<string, object>> requestData)
		{
			requestData.Add(new KeyValuePair<string, object>("MessageTrackingReportId", this.MessageTrackingReportId ?? string.Empty));
			requestData.Add(new KeyValuePair<string, object>("ReportTemplate", Names<Microsoft.Exchange.InfoWorker.Common.MessageTracking.ReportTemplate>.Map[(int)this.ReportTemplate]));
			string value = (this.RecipientFilter == null || this.RecipientFilter.EmailAddress == null) ? string.Empty : this.RecipientFilter.EmailAddress;
			requestData.Add(new KeyValuePair<string, object>("RecipientFilter", value));
			requestData.Add(new KeyValuePair<string, object>("Scope", this.Scope ?? string.Empty));
		}

		// Token: 0x04001416 RID: 5142
		public string Scope;

		// Token: 0x04001417 RID: 5143
		public MessageTrackingReportTemplate ReportTemplate;

		// Token: 0x04001418 RID: 5144
		public EmailAddressWrapper RecipientFilter;

		// Token: 0x04001419 RID: 5145
		public string MessageTrackingReportId;

		// Token: 0x0400141A RID: 5146
		public bool ReturnQueueEvents;

		// Token: 0x0400141B RID: 5147
		public string DiagnosticsLevel;

		// Token: 0x0400141C RID: 5148
		[XmlArrayItem("TrackingPropertyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
