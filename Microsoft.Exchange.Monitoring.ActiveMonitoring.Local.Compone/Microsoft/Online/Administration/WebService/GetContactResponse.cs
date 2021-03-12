using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000342 RID: 834
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetContactResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetContactResponse : Response
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0008BAF1 File Offset: 0x00089CF1
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x0008BAF9 File Offset: 0x00089CF9
		[DataMember]
		public Contact ReturnValue
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

		// Token: 0x04000FDF RID: 4063
		private Contact ReturnValueField;
	}
}
