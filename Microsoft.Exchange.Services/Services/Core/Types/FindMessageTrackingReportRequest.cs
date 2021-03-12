using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000423 RID: 1059
	[XmlType("FindMessageTrackingReportRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class FindMessageTrackingReportRequest : BaseRequest, IMessageTrackingRequestLogInformation
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x000A0794 File Offset: 0x0009E994
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new FindMessageTrackingReport(callContext, this);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000A079D File Offset: 0x0009E99D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000A07A0 File Offset: 0x0009E9A0
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000A07A4 File Offset: 0x0009E9A4
		public void AddRequestDataForLogging(List<KeyValuePair<string, object>> requestData)
		{
			requestData.Add(new KeyValuePair<string, object>("MessageId", this.MessageId ?? string.Empty));
			requestData.Add(new KeyValuePair<string, object>("Domain", this.Domain ?? string.Empty));
			string value = (this.Sender == null || this.Sender.EmailAddress == null) ? string.Empty : this.Sender.EmailAddress;
			requestData.Add(new KeyValuePair<string, object>("Sender", value));
			requestData.Add(new KeyValuePair<string, object>("ServerHint", this.ServerHint ?? string.Empty));
		}

		// Token: 0x040013B4 RID: 5044
		public string Scope;

		// Token: 0x040013B5 RID: 5045
		public string Domain;

		// Token: 0x040013B6 RID: 5046
		public EmailAddressWrapper Sender;

		// Token: 0x040013B7 RID: 5047
		public EmailAddressWrapper Recipient;

		// Token: 0x040013B8 RID: 5048
		public string Subject;

		// Token: 0x040013B9 RID: 5049
		public DateTime StartDateTime;

		// Token: 0x040013BA RID: 5050
		[XmlIgnore]
		public bool StartDateTimeSpecified;

		// Token: 0x040013BB RID: 5051
		public DateTime EndDateTime;

		// Token: 0x040013BC RID: 5052
		[XmlIgnore]
		public bool EndDateTimeSpecified;

		// Token: 0x040013BD RID: 5053
		public string MessageId;

		// Token: 0x040013BE RID: 5054
		public EmailAddressWrapper FederatedDeliveryMailbox;

		// Token: 0x040013BF RID: 5055
		public string DiagnosticsLevel;

		// Token: 0x040013C0 RID: 5056
		public string ServerHint;

		// Token: 0x040013C1 RID: 5057
		[XmlArrayItem("TrackingPropertyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
