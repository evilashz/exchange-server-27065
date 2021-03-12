using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A9C RID: 2716
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateInboxRuleRequestBase : BaseJsonRequest
	{
		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x0010681C File Offset: 0x00104A1C
		// (set) Token: 0x06004CC9 RID: 19657 RVA: 0x00106824 File Offset: 0x00104A24
		[DataMember]
		public bool AlwaysDeleteOutlookRulesBlob { get; set; }

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x0010682D File Offset: 0x00104A2D
		// (set) Token: 0x06004CCB RID: 19659 RVA: 0x00106835 File Offset: 0x00104A35
		[DataMember]
		public bool Force { get; set; }

		// Token: 0x06004CCC RID: 19660 RVA: 0x0010683E File Offset: 0x00104A3E
		public override string ToString()
		{
			return string.Format(base.GetType().Name + ": AlwaysDeleteOutlookRulesBlob = {0}, Force = {1}", this.AlwaysDeleteOutlookRulesBlob, this.Force);
		}
	}
}
