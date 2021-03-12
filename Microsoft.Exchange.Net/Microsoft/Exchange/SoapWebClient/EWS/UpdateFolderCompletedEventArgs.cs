using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004AF RID: 1199
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UpdateFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x00027296 File Offset: 0x00025496
		internal UpdateFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x000272A9 File Offset: 0x000254A9
		public UpdateFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017D9 RID: 6105
		private object[] results;
	}
}
