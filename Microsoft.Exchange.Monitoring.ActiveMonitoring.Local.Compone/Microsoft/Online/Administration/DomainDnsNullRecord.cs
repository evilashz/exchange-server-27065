using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F8 RID: 1016
	[DataContract(Name = "DomainDnsNullRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainDnsNullRecord : DomainDnsRecord
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0008D795 File Offset: 0x0008B995
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0008D79D File Offset: 0x0008B99D
		[DataMember]
		public DomainDnsRecordError Error
		{
			get
			{
				return this.ErrorField;
			}
			set
			{
				this.ErrorField = value;
			}
		}

		// Token: 0x040011A2 RID: 4514
		private DomainDnsRecordError ErrorField;
	}
}
