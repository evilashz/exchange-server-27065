using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD2 RID: 2770
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RetentionPolicyTagDisplay : OptionsPropertyChangeTracker
	{
		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x0010827E File Offset: 0x0010647E
		// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x00108286 File Offset: 0x00106486
		[DataMember]
		public Identity Identity { get; set; }

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x0010828F File Offset: 0x0010648F
		// (set) Token: 0x06004ED8 RID: 20184 RVA: 0x00108297 File Offset: 0x00106497
		[DataMember]
		public Guid RetentionId { get; set; }

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x001082A0 File Offset: 0x001064A0
		// (set) Token: 0x06004EDA RID: 20186 RVA: 0x001082A8 File Offset: 0x001064A8
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x001082B1 File Offset: 0x001064B1
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x001082B9 File Offset: 0x001064B9
		[DataMember]
		public ElcFolderType Type { get; set; }

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x001082C2 File Offset: 0x001064C2
		// (set) Token: 0x06004EDE RID: 20190 RVA: 0x001082CA File Offset: 0x001064CA
		[DataMember]
		public RetentionActionType RetentionAction { get; set; }

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x001082D3 File Offset: 0x001064D3
		// (set) Token: 0x06004EE0 RID: 20192 RVA: 0x001082DB File Offset: 0x001064DB
		[DataMember]
		public bool RetentionEnabled { get; set; }

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x001082E4 File Offset: 0x001064E4
		// (set) Token: 0x06004EE2 RID: 20194 RVA: 0x001082EC File Offset: 0x001064EC
		[DataMember]
		public int? AgeLimitForRetentionDays { get; set; }

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x001082F5 File Offset: 0x001064F5
		// (set) Token: 0x06004EE4 RID: 20196 RVA: 0x001082FD File Offset: 0x001064FD
		[DataMember]
		public bool OptionalTag { get; set; }

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x00108306 File Offset: 0x00106506
		// (set) Token: 0x06004EE6 RID: 20198 RVA: 0x0010830E File Offset: 0x0010650E
		[DataMember]
		public string Description { get; set; }
	}
}
