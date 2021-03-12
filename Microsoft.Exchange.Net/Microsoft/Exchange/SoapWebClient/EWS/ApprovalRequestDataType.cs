using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001FF RID: 511
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ApprovalRequestDataType
	{
		// Token: 0x04000D55 RID: 3413
		public bool IsUndecidedApprovalRequest;

		// Token: 0x04000D56 RID: 3414
		[XmlIgnore]
		public bool IsUndecidedApprovalRequestSpecified;

		// Token: 0x04000D57 RID: 3415
		public int ApprovalDecision;

		// Token: 0x04000D58 RID: 3416
		[XmlIgnore]
		public bool ApprovalDecisionSpecified;

		// Token: 0x04000D59 RID: 3417
		public string ApprovalDecisionMaker;

		// Token: 0x04000D5A RID: 3418
		public DateTime ApprovalDecisionTime;

		// Token: 0x04000D5B RID: 3419
		[XmlIgnore]
		public bool ApprovalDecisionTimeSpecified;
	}
}
