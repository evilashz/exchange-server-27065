using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000310 RID: 784
	[DataContract(Name = "AddGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddGroupRequest : Request
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0008B532 File Offset: 0x00089732
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0008B53A File Offset: 0x0008973A
		[DataMember]
		public Group Group
		{
			get
			{
				return this.GroupField;
			}
			set
			{
				this.GroupField = value;
			}
		}

		// Token: 0x04000FA0 RID: 4000
		private Group GroupField;
	}
}
