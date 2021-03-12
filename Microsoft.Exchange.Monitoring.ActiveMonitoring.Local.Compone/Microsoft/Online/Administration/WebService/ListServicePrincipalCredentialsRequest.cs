using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D4 RID: 724
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListServicePrincipalCredentialsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListServicePrincipalCredentialsRequest : Request
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0008AC04 File Offset: 0x00088E04
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x0008AC0C File Offset: 0x00088E0C
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

		// Token: 0x04000F32 RID: 3890
		private Guid ObjectIdField;
	}
}
