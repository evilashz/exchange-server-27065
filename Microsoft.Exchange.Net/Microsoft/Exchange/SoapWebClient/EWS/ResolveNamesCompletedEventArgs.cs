using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000495 RID: 1173
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ResolveNamesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x0002708E File Offset: 0x0002528E
		internal ResolveNamesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000270A1 File Offset: 0x000252A1
		public ResolveNamesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ResolveNamesResponseType)this.results[0];
			}
		}

		// Token: 0x040017CC RID: 6092
		private object[] results;
	}
}
