using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002EA RID: 746
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum CalendarPermissionLevelType
	{
		// Token: 0x0400129B RID: 4763
		None,
		// Token: 0x0400129C RID: 4764
		Owner,
		// Token: 0x0400129D RID: 4765
		PublishingEditor,
		// Token: 0x0400129E RID: 4766
		Editor,
		// Token: 0x0400129F RID: 4767
		PublishingAuthor,
		// Token: 0x040012A0 RID: 4768
		Author,
		// Token: 0x040012A1 RID: 4769
		NoneditingAuthor,
		// Token: 0x040012A2 RID: 4770
		Reviewer,
		// Token: 0x040012A3 RID: 4771
		Contributor,
		// Token: 0x040012A4 RID: 4772
		FreeBusyTimeOnly,
		// Token: 0x040012A5 RID: 4773
		FreeBusyTimeAndSubjectAndLocation,
		// Token: 0x040012A6 RID: 4774
		Custom
	}
}
