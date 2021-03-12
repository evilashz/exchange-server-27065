using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F7 RID: 759
	[DataContract(Name = "AddServicePrincipalCredentialsByAppPrincipalIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddServicePrincipalCredentialsByAppPrincipalIdRequest : Request
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0008B228 File Offset: 0x00089428
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x0008B230 File Offset: 0x00089430
		[DataMember]
		public Guid AppPrincipalId
		{
			get
			{
				return this.AppPrincipalIdField;
			}
			set
			{
				this.AppPrincipalIdField = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0008B239 File Offset: 0x00089439
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x0008B241 File Offset: 0x00089441
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

		// Token: 0x04000F7E RID: 3966
		private Guid AppPrincipalIdField;

		// Token: 0x04000F7F RID: 3967
		private ServicePrincipalCredential[] CredentialsField;
	}
}
