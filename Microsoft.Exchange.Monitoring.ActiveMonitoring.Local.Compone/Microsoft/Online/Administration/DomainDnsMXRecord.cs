using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F6 RID: 1014
	[DataContract(Name = "DomainDnsMXRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainDnsMXRecord : DomainDnsRecord
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0008D752 File Offset: 0x0008B952
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x0008D75A File Offset: 0x0008B95A
		[DataMember]
		public string MailExchange
		{
			get
			{
				return this.MailExchangeField;
			}
			set
			{
				this.MailExchangeField = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0008D763 File Offset: 0x0008B963
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x0008D76B File Offset: 0x0008B96B
		[DataMember]
		public int? Preference
		{
			get
			{
				return this.PreferenceField;
			}
			set
			{
				this.PreferenceField = value;
			}
		}

		// Token: 0x0400119F RID: 4511
		private string MailExchangeField;

		// Token: 0x040011A0 RID: 4512
		private int? PreferenceField;
	}
}
