using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A83 RID: 2691
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxRegionalConfigurationRequest : BaseJsonRequest
	{
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0010622A File Offset: 0x0010442A
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x00106232 File Offset: 0x00104432
		[DataMember]
		public bool VerifyDefaultFolderNameLanguage { get; set; }

		// Token: 0x06004C3A RID: 19514 RVA: 0x0010623B File Offset: 0x0010443B
		public override string ToString()
		{
			return string.Format("GetMailboxRegionalConfigurationRequest: VerifyDefaultFolderNameLanguage = {0}", this.VerifyDefaultFolderNameLanguage);
		}
	}
}
