using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002C8 RID: 712
	[DataContract(Name = "GetServicePrincipalRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetServicePrincipalRequest : Request
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0008AA72 File Offset: 0x00088C72
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0008AA7A File Offset: 0x00088C7A
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

		// Token: 0x04000F20 RID: 3872
		private Guid ObjectIdField;
	}
}
