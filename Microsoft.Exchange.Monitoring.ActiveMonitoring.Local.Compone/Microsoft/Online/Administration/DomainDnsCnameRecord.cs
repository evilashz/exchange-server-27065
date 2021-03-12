using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F9 RID: 1017
	[DataContract(Name = "DomainDnsCnameRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainDnsCnameRecord : DomainDnsRecord
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0008D7AE File Offset: 0x0008B9AE
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0008D7B6 File Offset: 0x0008B9B6
		[DataMember]
		public string CanonicalName
		{
			get
			{
				return this.CanonicalNameField;
			}
			set
			{
				this.CanonicalNameField = value;
			}
		}

		// Token: 0x040011A3 RID: 4515
		private string CanonicalNameField;
	}
}
