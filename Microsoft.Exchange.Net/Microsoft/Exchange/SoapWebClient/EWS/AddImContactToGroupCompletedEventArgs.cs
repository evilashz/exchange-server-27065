using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000537 RID: 1335
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class AddImContactToGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x00027D36 File Offset: 0x00025F36
		internal AddImContactToGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00027D49 File Offset: 0x00025F49
		public AddImContactToGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddImContactToGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400181D RID: 6173
		private object[] results;
	}
}
