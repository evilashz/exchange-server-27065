using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F5 RID: 757
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "AddServicePrincipalCredentialsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class AddServicePrincipalCredentialsRequest : Request
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0008B1D4 File Offset: 0x000893D4
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0008B1DC File Offset: 0x000893DC
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0008B1E5 File Offset: 0x000893E5
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x0008B1ED File Offset: 0x000893ED
		[DataMember]
		public Guid ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x04000F7A RID: 3962
		private ServicePrincipalCredential[] CredentialsField;

		// Token: 0x04000F7B RID: 3963
		private Guid ObjectIdField;
	}
}
