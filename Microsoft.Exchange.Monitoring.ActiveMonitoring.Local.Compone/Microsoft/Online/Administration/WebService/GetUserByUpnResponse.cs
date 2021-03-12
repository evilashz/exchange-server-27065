using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000331 RID: 817
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetUserByUpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class GetUserByUpnResponse : Response
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0008B89E File Offset: 0x00089A9E
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0008B8A6 File Offset: 0x00089AA6
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

		// Token: 0x04000FC4 RID: 4036
		private User ReturnValueField;
	}
}
