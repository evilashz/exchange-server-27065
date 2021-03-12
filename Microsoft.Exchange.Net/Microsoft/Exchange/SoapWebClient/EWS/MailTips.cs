using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000329 RID: 809
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailTips
	{
		// Token: 0x04001356 RID: 4950
		public EmailAddressType RecipientAddress;

		// Token: 0x04001357 RID: 4951
		public MailTipTypes PendingMailTips;

		// Token: 0x04001358 RID: 4952
		public OutOfOfficeMailTip OutOfOffice;

		// Token: 0x04001359 RID: 4953
		public bool MailboxFull;

		// Token: 0x0400135A RID: 4954
		[XmlIgnore]
		public bool MailboxFullSpecified;

		// Token: 0x0400135B RID: 4955
		public string CustomMailTip;

		// Token: 0x0400135C RID: 4956
		public int TotalMemberCount;

		// Token: 0x0400135D RID: 4957
		[XmlIgnore]
		public bool TotalMemberCountSpecified;

		// Token: 0x0400135E RID: 4958
		public int ExternalMemberCount;

		// Token: 0x0400135F RID: 4959
		[XmlIgnore]
		public bool ExternalMemberCountSpecified;

		// Token: 0x04001360 RID: 4960
		public int MaxMessageSize;

		// Token: 0x04001361 RID: 4961
		[XmlIgnore]
		public bool MaxMessageSizeSpecified;

		// Token: 0x04001362 RID: 4962
		public bool DeliveryRestricted;

		// Token: 0x04001363 RID: 4963
		[XmlIgnore]
		public bool DeliveryRestrictedSpecified;

		// Token: 0x04001364 RID: 4964
		public bool IsModerated;

		// Token: 0x04001365 RID: 4965
		[XmlIgnore]
		public bool IsModeratedSpecified;

		// Token: 0x04001366 RID: 4966
		public bool InvalidRecipient;

		// Token: 0x04001367 RID: 4967
		[XmlIgnore]
		public bool InvalidRecipientSpecified;

		// Token: 0x04001368 RID: 4968
		public int Scope;

		// Token: 0x04001369 RID: 4969
		[XmlIgnore]
		public bool ScopeSpecified;
	}
}
