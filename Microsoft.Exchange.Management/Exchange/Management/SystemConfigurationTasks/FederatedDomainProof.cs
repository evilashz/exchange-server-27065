using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009DA RID: 2522
	[Serializable]
	public sealed class FederatedDomainProof
	{
		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x00179E16 File Offset: 0x00178016
		// (set) Token: 0x06005A37 RID: 23095 RVA: 0x00179E1E File Offset: 0x0017801E
		public SmtpDomain DomainName { get; internal set; }

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x06005A38 RID: 23096 RVA: 0x00179E27 File Offset: 0x00178027
		// (set) Token: 0x06005A39 RID: 23097 RVA: 0x00179E2F File Offset: 0x0017802F
		public string Name { get; internal set; }

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x06005A3A RID: 23098 RVA: 0x00179E38 File Offset: 0x00178038
		// (set) Token: 0x06005A3B RID: 23099 RVA: 0x00179E40 File Offset: 0x00178040
		public string Thumbprint { get; internal set; }

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x06005A3C RID: 23100 RVA: 0x00179E49 File Offset: 0x00178049
		// (set) Token: 0x06005A3D RID: 23101 RVA: 0x00179E51 File Offset: 0x00178051
		public string Proof { get; internal set; }

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x06005A3E RID: 23102 RVA: 0x00179E5A File Offset: 0x0017805A
		// (set) Token: 0x06005A3F RID: 23103 RVA: 0x00179E62 File Offset: 0x00178062
		public string DnsRecord { get; internal set; }

		// Token: 0x06005A40 RID: 23104 RVA: 0x00179E6B File Offset: 0x0017806B
		public FederatedDomainProof()
		{
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x00179E73 File Offset: 0x00178073
		public FederatedDomainProof(SmtpDomain domainName, string name, string thumbprint, string proof)
		{
			this.DomainName = domainName;
			this.Name = name;
			this.Thumbprint = thumbprint;
			this.Proof = proof;
			this.DnsRecord = domainName + " TXT IN " + proof;
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x00179EAB File Offset: 0x001780AB
		public override string ToString()
		{
			return this.DnsRecord;
		}
	}
}
