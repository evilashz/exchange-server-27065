using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000348 RID: 840
	[DataContract(Name = "ListDomainsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ListDomainsResponse : Response
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0008BB87 File Offset: 0x00089D87
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0008BB8F File Offset: 0x00089D8F
		[DataMember]
		public Domain[] ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x04000FE5 RID: 4069
		private Domain[] ReturnValueField;
	}
}
