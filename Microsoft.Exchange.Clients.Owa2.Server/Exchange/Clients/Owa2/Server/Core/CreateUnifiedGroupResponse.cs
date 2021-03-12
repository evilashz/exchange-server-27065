using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D5 RID: 981
	[DataContract(Name = "CreateUnifiedGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateUnifiedGroupResponse : BaseJsonResponse
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x00077695 File Offset: 0x00075895
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x0007769D File Offset: 0x0007589D
		[DataMember(Name = "Persona", IsRequired = false)]
		public Persona Persona { get; set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000776A6 File Offset: 0x000758A6
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x000776AE File Offset: 0x000758AE
		[DataMember(Name = "Error", IsRequired = false)]
		public string Error { get; set; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000776B7 File Offset: 0x000758B7
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x000776BF File Offset: 0x000758BF
		[DataMember(Name = "ExternalDirectoryObjectId", IsRequired = false)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x000776C8 File Offset: 0x000758C8
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x000776D0 File Offset: 0x000758D0
		[DataMember(Name = "FailureState", IsRequired = false)]
		public CreateUnifiedGroupResponse.GroupProvisionFailureState FailureState { get; set; }

		// Token: 0x020003D6 RID: 982
		public enum GroupProvisionFailureState
		{
			// Token: 0x040011F0 RID: 4592
			NoError,
			// Token: 0x040011F1 RID: 4593
			FailedCreate,
			// Token: 0x040011F2 RID: 4594
			FailedMailboxProvision
		}
	}
}
