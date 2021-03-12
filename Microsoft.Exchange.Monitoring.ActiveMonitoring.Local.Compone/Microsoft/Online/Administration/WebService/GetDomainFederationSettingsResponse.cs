using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000346 RID: 838
	[DataContract(Name = "GetDomainFederationSettingsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetDomainFederationSettingsResponse : Response
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0008BB55 File Offset: 0x00089D55
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x0008BB5D File Offset: 0x00089D5D
		[DataMember]
		public DomainFederationSettings ReturnValue
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

		// Token: 0x04000FE3 RID: 4067
		private DomainFederationSettings ReturnValueField;
	}
}
