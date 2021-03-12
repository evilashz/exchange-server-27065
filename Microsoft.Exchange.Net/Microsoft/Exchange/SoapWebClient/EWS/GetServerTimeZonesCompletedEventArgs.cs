using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000499 RID: 1177
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetServerTimeZonesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FB8 RID: 4024 RVA: 0x000270DE File Offset: 0x000252DE
		internal GetServerTimeZonesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000270F1 File Offset: 0x000252F1
		public GetServerTimeZonesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetServerTimeZonesResponseType)this.results[0];
			}
		}

		// Token: 0x040017CE RID: 6094
		private object[] results;
	}
}
