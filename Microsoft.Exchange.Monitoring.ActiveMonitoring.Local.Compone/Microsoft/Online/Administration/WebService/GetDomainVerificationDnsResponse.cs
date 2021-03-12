using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000349 RID: 841
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetDomainVerificationDnsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class GetDomainVerificationDnsResponse : Response
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0008BBA0 File Offset: 0x00089DA0
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0008BBA8 File Offset: 0x00089DA8
		[DataMember]
		public DomainDnsRecord ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x04000FE6 RID: 4070
		private DomainDnsRecord ReturnValueField;
	}
}
