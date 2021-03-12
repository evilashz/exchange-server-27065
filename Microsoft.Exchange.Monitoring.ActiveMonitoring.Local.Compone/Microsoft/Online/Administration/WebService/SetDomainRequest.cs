using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000309 RID: 777
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetDomainRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class SetDomainRequest : Request
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0008B43F File Offset: 0x0008963F
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0008B447 File Offset: 0x00089647
		[DataMember]
		public Domain Domain
		{
			get
			{
				return this.DomainField;
			}
			set
			{
				this.DomainField = value;
			}
		}

		// Token: 0x04000F95 RID: 3989
		private Domain DomainField;
	}
}
