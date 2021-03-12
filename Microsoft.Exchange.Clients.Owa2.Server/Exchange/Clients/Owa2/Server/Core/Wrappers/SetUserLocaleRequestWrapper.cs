using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C2 RID: 706
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserLocaleRequestWrapper
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00054309 File Offset: 0x00052509
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x00054311 File Offset: 0x00052511
		[DataMember(Name = "userLocale")]
		public string UserLocale { get; set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005431A File Offset: 0x0005251A
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x00054322 File Offset: 0x00052522
		[DataMember(Name = "localizeFolders")]
		public bool LocalizeFolders { get; set; }
	}
}
