using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B3 RID: 179
	[DataContract(Name = "StringSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public sealed class StringSetting : UserSetting
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00018165 File Offset: 0x00016365
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001816D File Offset: 0x0001636D
		[DataMember]
		public string Value { get; set; }
	}
}
