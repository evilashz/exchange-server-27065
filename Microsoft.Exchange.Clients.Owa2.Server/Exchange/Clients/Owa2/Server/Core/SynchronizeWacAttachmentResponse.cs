using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200040B RID: 1035
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class SynchronizeWacAttachmentResponse
	{
		// Token: 0x06002251 RID: 8785 RVA: 0x0007EE38 File Offset: 0x0007D038
		public SynchronizeWacAttachmentResponse(SynchronizeWacAttachmentResult result)
		{
			this.result = result;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0007EE47 File Offset: 0x0007D047
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x0007EE4F File Offset: 0x0007D04F
		[DataMember(Order = 1)]
		public SynchronizeWacAttachmentResult Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x0400131F RID: 4895
		private SynchronizeWacAttachmentResult result;
	}
}
