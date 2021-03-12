using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003B4 RID: 948
	[DebuggerStepThrough]
	[DataContract(Name = "ServicePrincipalExtended", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ServicePrincipalExtended : ServicePrincipal
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0008C363 File Offset: 0x0008A563
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0008C36B File Offset: 0x0008A56B
		[DataMember]
		public ServicePrincipalCredential[] Credentials
		{
			get
			{
				return this.CredentialsField;
			}
			set
			{
				this.CredentialsField = value;
			}
		}

		// Token: 0x0400102C RID: 4140
		private ServicePrincipalCredential[] CredentialsField;
	}
}
