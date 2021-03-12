using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004D3 RID: 1235
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class ArchiveItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001066 RID: 4198 RVA: 0x00027566 File Offset: 0x00025766
		internal ArchiveItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00027579 File Offset: 0x00025779
		public ArchiveItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ArchiveItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017EB RID: 6123
		private object[] results;
	}
}
