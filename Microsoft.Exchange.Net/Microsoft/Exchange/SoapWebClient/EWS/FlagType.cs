using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000259 RID: 601
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FlagType
	{
		// Token: 0x04000F8D RID: 3981
		public FlagStatusType FlagStatus;

		// Token: 0x04000F8E RID: 3982
		public DateTime StartDate;

		// Token: 0x04000F8F RID: 3983
		[XmlIgnore]
		public bool StartDateSpecified;

		// Token: 0x04000F90 RID: 3984
		public DateTime DueDate;

		// Token: 0x04000F91 RID: 3985
		[XmlIgnore]
		public bool DueDateSpecified;

		// Token: 0x04000F92 RID: 3986
		public DateTime CompleteDate;

		// Token: 0x04000F93 RID: 3987
		[XmlIgnore]
		public bool CompleteDateSpecified;
	}
}
