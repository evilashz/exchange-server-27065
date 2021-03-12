using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B04 RID: 2820
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetConnectSubscriptionData : NewConnectSubscriptionData
	{
		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x00109318 File Offset: 0x00107518
		// (set) Token: 0x06005018 RID: 20504 RVA: 0x00109320 File Offset: 0x00107520
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

		// Token: 0x04002CCD RID: 11469
		private Identity identity;
	}
}
