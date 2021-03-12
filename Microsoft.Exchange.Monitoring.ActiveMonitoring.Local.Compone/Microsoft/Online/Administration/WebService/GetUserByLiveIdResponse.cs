using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000332 RID: 818
	[DataContract(Name = "GetUserByLiveIdResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetUserByLiveIdResponse : Response
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0008B8B7 File Offset: 0x00089AB7
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x0008B8BF File Offset: 0x00089ABF
		[DataMember]
		public User ReturnValue
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

		// Token: 0x04000FC5 RID: 4037
		private User ReturnValueField;
	}
}
