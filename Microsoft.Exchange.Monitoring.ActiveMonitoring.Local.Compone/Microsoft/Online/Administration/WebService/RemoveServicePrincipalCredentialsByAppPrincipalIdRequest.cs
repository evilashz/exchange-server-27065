using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002CD RID: 717
	[DataContract(Name = "RemoveServicePrincipalCredentialsByAppPrincipalIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class RemoveServicePrincipalCredentialsByAppPrincipalIdRequest : Request
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0008AB11 File Offset: 0x00088D11
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0008AB19 File Offset: 0x00088D19
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0008AB22 File Offset: 0x00088D22
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0008AB2A File Offset: 0x00088D2A
		[DataMember]
		public Guid[] KeyIds
		{
			get
			{
				return this.KeyIdsField;
			}
			set
			{
				this.KeyIdsField = value;
			}
		}

		// Token: 0x04000F27 RID: 3879
		private Guid AppPrincipalIdField;

		// Token: 0x04000F28 RID: 3880
		private Guid[] KeyIdsField;
	}
}
