using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200011E RID: 286
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ApprovalRequestDataType
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00021CF8 File Offset: 0x0001FEF8
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00021D00 File Offset: 0x0001FF00
		public bool IsUndecidedApprovalRequest
		{
			get
			{
				return this.isUndecidedApprovalRequestField;
			}
			set
			{
				this.isUndecidedApprovalRequestField = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00021D09 File Offset: 0x0001FF09
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00021D11 File Offset: 0x0001FF11
		[XmlIgnore]
		public bool IsUndecidedApprovalRequestSpecified
		{
			get
			{
				return this.isUndecidedApprovalRequestFieldSpecified;
			}
			set
			{
				this.isUndecidedApprovalRequestFieldSpecified = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00021D1A File Offset: 0x0001FF1A
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00021D22 File Offset: 0x0001FF22
		public int ApprovalDecision
		{
			get
			{
				return this.approvalDecisionField;
			}
			set
			{
				this.approvalDecisionField = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00021D2B File Offset: 0x0001FF2B
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00021D33 File Offset: 0x0001FF33
		[XmlIgnore]
		public bool ApprovalDecisionSpecified
		{
			get
			{
				return this.approvalDecisionFieldSpecified;
			}
			set
			{
				this.approvalDecisionFieldSpecified = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00021D3C File Offset: 0x0001FF3C
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00021D44 File Offset: 0x0001FF44
		public string ApprovalDecisionMaker
		{
			get
			{
				return this.approvalDecisionMakerField;
			}
			set
			{
				this.approvalDecisionMakerField = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00021D4D File Offset: 0x0001FF4D
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00021D55 File Offset: 0x0001FF55
		public DateTime ApprovalDecisionTime
		{
			get
			{
				return this.approvalDecisionTimeField;
			}
			set
			{
				this.approvalDecisionTimeField = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00021D5E File Offset: 0x0001FF5E
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00021D66 File Offset: 0x0001FF66
		[XmlIgnore]
		public bool ApprovalDecisionTimeSpecified
		{
			get
			{
				return this.approvalDecisionTimeFieldSpecified;
			}
			set
			{
				this.approvalDecisionTimeFieldSpecified = value;
			}
		}

		// Token: 0x04000903 RID: 2307
		private bool isUndecidedApprovalRequestField;

		// Token: 0x04000904 RID: 2308
		private bool isUndecidedApprovalRequestFieldSpecified;

		// Token: 0x04000905 RID: 2309
		private int approvalDecisionField;

		// Token: 0x04000906 RID: 2310
		private bool approvalDecisionFieldSpecified;

		// Token: 0x04000907 RID: 2311
		private string approvalDecisionMakerField;

		// Token: 0x04000908 RID: 2312
		private DateTime approvalDecisionTimeField;

		// Token: 0x04000909 RID: 2313
		private bool approvalDecisionTimeFieldSpecified;
	}
}
