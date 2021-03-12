using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000091 RID: 145
	[DataContract(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public sealed class User
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001787B File Offset: 0x00015A7B
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00017883 File Offset: 0x00015A83
		[DataMember(IsRequired = true)]
		public string Mailbox { get; set; }
	}
}
