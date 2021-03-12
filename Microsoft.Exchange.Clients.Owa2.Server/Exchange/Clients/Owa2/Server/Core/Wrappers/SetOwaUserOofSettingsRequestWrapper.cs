using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002BE RID: 702
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetOwaUserOofSettingsRequestWrapper
	{
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000542A5 File Offset: 0x000524A5
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x000542AD File Offset: 0x000524AD
		[DataMember(Name = "userOofSettings")]
		public UserOofSettingsType UserOofSettings { get; set; }
	}
}
