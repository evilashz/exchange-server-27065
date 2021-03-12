using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200049D RID: 1181
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class FindItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002712E File Offset: 0x0002532E
		internal FindItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00027141 File Offset: 0x00025341
		public FindItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017D0 RID: 6096
		private object[] results;
	}
}
