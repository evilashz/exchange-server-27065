using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000096 RID: 150
	[DataContract(Name = "AlternateMailboxCollectionSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class AlternateMailboxCollectionSetting : UserSetting
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001792B File Offset: 0x00015B2B
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00017933 File Offset: 0x00015B33
		[DataMember(Name = "AlternateMailboxes", IsRequired = true)]
		public AlternateMailboxCollection AlternateMailboxes { get; set; }
	}
}
