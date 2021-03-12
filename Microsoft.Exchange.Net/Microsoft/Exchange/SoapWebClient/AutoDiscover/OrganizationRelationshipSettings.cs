using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200010F RID: 271
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class OrganizationRelationshipSettings
	{
		// Token: 0x04000595 RID: 1429
		public bool DeliveryReportEnabled;

		// Token: 0x04000596 RID: 1430
		[XmlArrayItem("Domain")]
		[XmlArray(IsNullable = true)]
		public string[] DomainNames;

		// Token: 0x04000597 RID: 1431
		public bool FreeBusyAccessEnabled;

		// Token: 0x04000598 RID: 1432
		[XmlElement(IsNullable = true)]
		public string FreeBusyAccessLevel;

		// Token: 0x04000599 RID: 1433
		public bool MailTipsAccessEnabled;

		// Token: 0x0400059A RID: 1434
		[XmlElement(IsNullable = true)]
		public string MailTipsAccessLevel;

		// Token: 0x0400059B RID: 1435
		public bool MailboxMoveEnabled;

		// Token: 0x0400059C RID: 1436
		[XmlElement(IsNullable = true)]
		public string Name;

		// Token: 0x0400059D RID: 1437
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string TargetApplicationUri;

		// Token: 0x0400059E RID: 1438
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string TargetAutodiscoverEpr;

		// Token: 0x0400059F RID: 1439
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string TargetSharingEpr;
	}
}
