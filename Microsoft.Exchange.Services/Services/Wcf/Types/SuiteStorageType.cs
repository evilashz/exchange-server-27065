using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B12 RID: 2834
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuiteStorageType
	{
		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06005077 RID: 20599 RVA: 0x00109B0F File Offset: 0x00107D0F
		// (set) Token: 0x06005078 RID: 20600 RVA: 0x00109B17 File Offset: 0x00107D17
		[DataMember]
		public SuiteStorageKeyType Key { get; set; }

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x00109B20 File Offset: 0x00107D20
		// (set) Token: 0x0600507A RID: 20602 RVA: 0x00109B28 File Offset: 0x00107D28
		[DataMember]
		public string Value { get; set; }

		// Token: 0x04002CED RID: 11501
		public const string ConfigurationName = "Suite.Storage";

		// Token: 0x04002CEE RID: 11502
		public const string UserMailboxStorage = "User";

		// Token: 0x04002CEF RID: 11503
		public const string OrgMailboxStorage = "Org";
	}
}
