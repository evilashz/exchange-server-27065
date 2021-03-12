using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000313 RID: 787
	[DebuggerStepThrough]
	[DataContract(Name = "GetGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetGroupRequest : Request
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0008B57D File Offset: 0x0008977D
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x0008B585 File Offset: 0x00089785
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

		// Token: 0x04000FA3 RID: 4003
		private Guid ObjectIdField;
	}
}
