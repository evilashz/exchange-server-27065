using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000314 RID: 788
	[DataContract(Name = "ListGroupsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListGroupsRequest : Request
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0008B596 File Offset: 0x00089796
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x0008B59E File Offset: 0x0008979E
		[DataMember]
		public GroupSearchDefinition GroupSearchDefinition
		{
			get
			{
				return this.GroupSearchDefinitionField;
			}
			set
			{
				this.GroupSearchDefinitionField = value;
			}
		}

		// Token: 0x04000FA4 RID: 4004
		private GroupSearchDefinition GroupSearchDefinitionField;
	}
}
