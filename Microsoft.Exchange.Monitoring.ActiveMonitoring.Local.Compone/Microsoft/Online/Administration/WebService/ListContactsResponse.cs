using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000343 RID: 835
	[DataContract(Name = "ListContactsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ListContactsResponse : Response
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0008BB0A File Offset: 0x00089D0A
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x0008BB12 File Offset: 0x00089D12
		[DataMember]
		public ListContactResults ReturnValue
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

		// Token: 0x04000FE0 RID: 4064
		private ListContactResults ReturnValueField;
	}
}
