using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000324 RID: 804
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetRoleResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetRoleResponse : Response
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0008B759 File Offset: 0x00089959
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0008B761 File Offset: 0x00089961
		[DataMember]
		public Role ReturnValue
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

		// Token: 0x04000FB7 RID: 4023
		private Role ReturnValueField;
	}
}
