using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Monitor
{
	// Token: 0x02000080 RID: 128
	internal class PolicySyncFailureInformation
	{
		// Token: 0x06000349 RID: 841 RVA: 0x0000B721 File Offset: 0x00009921
		public PolicySyncFailureInformation(Guid policyId)
		{
			this.PolicyId = policyId;
			this.ObjectTypes = new HashSet<ConfigurationObjectType>();
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000B73B File Offset: 0x0000993B
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000B743 File Offset: 0x00009943
		public Guid PolicyId { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000B74C File Offset: 0x0000994C
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000B754 File Offset: 0x00009954
		public HashSet<ConfigurationObjectType> ObjectTypes { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000B75D File Offset: 0x0000995D
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000B765 File Offset: 0x00009965
		public Exception LastException { get; set; }

		// Token: 0x06000350 RID: 848 RVA: 0x0000B770 File Offset: 0x00009970
		public override string ToString()
		{
			string text = string.Format("PolicyId={0}", this.PolicyId);
			if (this.ObjectTypes.Any<ConfigurationObjectType>())
			{
				text += string.Format("\r\nFailures:{0}", string.Join<ConfigurationObjectType>(",", this.ObjectTypes));
			}
			return text;
		}
	}
}
