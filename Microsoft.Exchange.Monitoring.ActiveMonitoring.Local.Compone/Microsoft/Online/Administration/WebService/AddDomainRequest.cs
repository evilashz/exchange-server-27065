using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000306 RID: 774
	[DataContract(Name = "AddDomainRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddDomainRequest : Request
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0008B3E3 File Offset: 0x000895E3
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0008B3EB File Offset: 0x000895EB
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

		// Token: 0x04000F91 RID: 3985
		private Domain DomainField;
	}
}
