using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004D1 RID: 1233
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CopyItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x0002753E File Offset: 0x0002573E
		internal CopyItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00027551 File Offset: 0x00025751
		public CopyItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CopyItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017EA RID: 6122
		private object[] results;
	}
}
