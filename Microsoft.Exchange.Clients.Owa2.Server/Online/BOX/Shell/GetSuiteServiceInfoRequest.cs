using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000074 RID: 116
	[DebuggerStepThrough]
	[DataContract(Name = "GetSuiteServiceInfoRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetSuiteServiceInfoRequest : ShellServiceRequest
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000E0DF File Offset: 0x0000C2DF
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000E0E7 File Offset: 0x0000C2E7
		[DataMember]
		public string UrlOfRequestingPage
		{
			get
			{
				return this.UrlOfRequestingPageField;
			}
			set
			{
				this.UrlOfRequestingPageField = value;
			}
		}

		// Token: 0x0400020B RID: 523
		private string UrlOfRequestingPageField;
	}
}
