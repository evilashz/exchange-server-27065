using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000AC RID: 172
	[DataContract(Name = "ProtocolConnection", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class ProtocolConnection
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00017EDF File Offset: 0x000160DF
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00017EE7 File Offset: 0x000160E7
		[DataMember(Name = "EncryptionMethod", IsRequired = true, Order = 3)]
		public string EncryptionMethod { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00017EF0 File Offset: 0x000160F0
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00017EF8 File Offset: 0x000160F8
		[DataMember(Name = "Hostname", IsRequired = true, Order = 1)]
		public string Hostname { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00017F01 File Offset: 0x00016101
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00017F09 File Offset: 0x00016109
		[DataMember(Name = "Port", IsRequired = true, Order = 2)]
		public int Port { get; set; }
	}
}
