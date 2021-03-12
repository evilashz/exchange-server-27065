using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004A3 RID: 1187
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class ExportItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x000271A6 File Offset: 0x000253A6
		internal ExportItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000271B9 File Offset: 0x000253B9
		public ExportItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExportItemsResponseType)this.results[0];
			}
		}

		// Token: 0x040017D3 RID: 6099
		private object[] results;
	}
}
