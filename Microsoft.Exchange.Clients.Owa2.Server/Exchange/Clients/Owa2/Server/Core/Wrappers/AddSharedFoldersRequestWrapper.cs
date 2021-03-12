using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000262 RID: 610
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddSharedFoldersRequestWrapper
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00053756 File Offset: 0x00051956
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0005375E File Offset: 0x0005195E
		[DataMember(Name = "displayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00053767 File Offset: 0x00051967
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0005376F File Offset: 0x0005196F
		[DataMember(Name = "primarySMTPAddress")]
		public string PrimarySMTPAddress { get; set; }
	}
}
