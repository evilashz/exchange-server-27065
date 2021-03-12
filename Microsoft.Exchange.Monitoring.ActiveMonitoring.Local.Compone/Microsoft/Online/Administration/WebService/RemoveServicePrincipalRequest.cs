using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F8 RID: 760
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RemoveServicePrincipalRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RemoveServicePrincipalRequest : Request
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0008B252 File Offset: 0x00089452
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0008B25A File Offset: 0x0008945A
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

		// Token: 0x04000F80 RID: 3968
		private Guid ObjectIdField;
	}
}
