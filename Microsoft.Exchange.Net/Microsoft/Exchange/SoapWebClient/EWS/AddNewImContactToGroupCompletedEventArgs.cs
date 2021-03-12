using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000533 RID: 1331
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class AddNewImContactToGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x00027CE6 File Offset: 0x00025EE6
		internal AddNewImContactToGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00027CF9 File Offset: 0x00025EF9
		public AddNewImContactToGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddNewImContactToGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400181B RID: 6171
		private object[] results;
	}
}
