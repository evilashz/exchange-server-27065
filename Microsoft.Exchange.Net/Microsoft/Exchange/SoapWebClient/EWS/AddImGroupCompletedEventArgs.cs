using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200053B RID: 1339
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class AddImGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600119E RID: 4510 RVA: 0x00027D86 File Offset: 0x00025F86
		internal AddImGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00027D99 File Offset: 0x00025F99
		public AddImGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddImGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400181F RID: 6175
		private object[] results;
	}
}
