using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC2 RID: 2754
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MessageClassification
	{
		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00107C15 File Offset: 0x00105E15
		// (set) Token: 0x06004E54 RID: 20052 RVA: 0x00107C1D File Offset: 0x00105E1D
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06004E55 RID: 20053 RVA: 0x00107C26 File Offset: 0x00105E26
		// (set) Token: 0x06004E56 RID: 20054 RVA: 0x00107C2E File Offset: 0x00105E2E
		[DataMember]
		public Guid Guid { get; set; }

		// Token: 0x06004E57 RID: 20055 RVA: 0x00107C37 File Offset: 0x00105E37
		public override string ToString()
		{
			return string.Format("Guid = {0}, DisplayName = {1}", this.Guid, this.DisplayName);
		}
	}
}
