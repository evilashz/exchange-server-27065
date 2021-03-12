using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002CF RID: 719
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetServicePrincipalByAppPrincipalIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class SetServicePrincipalByAppPrincipalIdRequest : Request
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0008AB54 File Offset: 0x00088D54
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x0008AB5C File Offset: 0x00088D5C
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

		// Token: 0x04000F2A RID: 3882
		private ServicePrincipal ServicePrincipalField;
	}
}
