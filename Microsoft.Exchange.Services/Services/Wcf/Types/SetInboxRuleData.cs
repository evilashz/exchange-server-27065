using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB9 RID: 2745
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetInboxRuleData : NewInboxRuleData
	{
		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004DDB RID: 19931 RVA: 0x00107598 File Offset: 0x00105798
		// (set) Token: 0x06004DDC RID: 19932 RVA: 0x001075A0 File Offset: 0x001057A0
		[DataMember]
		public Identity Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
				base.TrackPropertyChanged("Identity");
			}
		}

		// Token: 0x04002BCD RID: 11213
		private Identity identity;
	}
}
