using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000304 RID: 772
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListContactsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListContactsRequest : Request
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0008B3A0 File Offset: 0x000895A0
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0008B3A8 File Offset: 0x000895A8
		[DataMember]
		public ContactSearchDefinition ContactSearchDefinition
		{
			get
			{
				return this.ContactSearchDefinitionField;
			}
			set
			{
				this.ContactSearchDefinitionField = value;
			}
		}

		// Token: 0x04000F8E RID: 3982
		private ContactSearchDefinition ContactSearchDefinitionField;
	}
}
