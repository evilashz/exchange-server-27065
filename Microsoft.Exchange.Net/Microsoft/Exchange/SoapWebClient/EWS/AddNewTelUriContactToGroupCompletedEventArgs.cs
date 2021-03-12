using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000535 RID: 1333
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class AddNewTelUriContactToGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x00027D0E File Offset: 0x00025F0E
		internal AddNewTelUriContactToGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00027D21 File Offset: 0x00025F21
		public AddNewTelUriContactToGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddNewTelUriContactToGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400181C RID: 6172
		private object[] results;
	}
}
