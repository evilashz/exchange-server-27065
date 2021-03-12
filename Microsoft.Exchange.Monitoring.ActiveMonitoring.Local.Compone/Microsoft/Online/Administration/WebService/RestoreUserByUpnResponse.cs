using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000337 RID: 823
	[DebuggerStepThrough]
	[DataContract(Name = "RestoreUserByUpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RestoreUserByUpnResponse : Response
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0008B934 File Offset: 0x00089B34
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x0008B93C File Offset: 0x00089B3C
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

		// Token: 0x04000FCA RID: 4042
		private User ReturnValueField;
	}
}
