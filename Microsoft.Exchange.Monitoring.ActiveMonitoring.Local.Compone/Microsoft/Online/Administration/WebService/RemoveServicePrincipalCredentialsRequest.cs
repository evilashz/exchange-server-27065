using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002CB RID: 715
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "RemoveServicePrincipalCredentialsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RemoveServicePrincipalCredentialsRequest : Request
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0008AABD File Offset: 0x00088CBD
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0008AAC5 File Offset: 0x00088CC5
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

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0008AACE File Offset: 0x00088CCE
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0008AAD6 File Offset: 0x00088CD6
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

		// Token: 0x04000F23 RID: 3875
		private Guid[] KeyIdsField;

		// Token: 0x04000F24 RID: 3876
		private Guid ObjectIdField;
	}
}
