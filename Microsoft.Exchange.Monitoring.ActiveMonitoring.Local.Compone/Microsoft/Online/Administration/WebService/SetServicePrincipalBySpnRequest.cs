using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D0 RID: 720
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetServicePrincipalBySpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class SetServicePrincipalBySpnRequest : Request
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0008AB6D File Offset: 0x00088D6D
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x0008AB75 File Offset: 0x00088D75
		[DataMember]
		public ServicePrincipal ServicePrincipal
		{
			get
			{
				return this.ServicePrincipalField;
			}
			set
			{
				this.ServicePrincipalField = value;
			}
		}

		// Token: 0x04000F2B RID: 3883
		private ServicePrincipal ServicePrincipalField;
	}
}
