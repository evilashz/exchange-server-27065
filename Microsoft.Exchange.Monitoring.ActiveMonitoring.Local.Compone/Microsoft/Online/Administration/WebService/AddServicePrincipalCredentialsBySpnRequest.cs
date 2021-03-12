using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F6 RID: 758
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "AddServicePrincipalCredentialsBySpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class AddServicePrincipalCredentialsBySpnRequest : Request
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0008B1FE File Offset: 0x000893FE
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x0008B206 File Offset: 0x00089406
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0008B20F File Offset: 0x0008940F
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x0008B217 File Offset: 0x00089417
		[DataMember]
		public string ServicePrincipalName
		{
			get
			{
				return this.ServicePrincipalNameField;
			}
			set
			{
				this.ServicePrincipalNameField = value;
			}
		}

		// Token: 0x04000F7C RID: 3964
		private ServicePrincipalCredential[] CredentialsField;

		// Token: 0x04000F7D RID: 3965
		private string ServicePrincipalNameField;
	}
}
