using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000569 RID: 1385
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CreateUMCallDataRecordCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001228 RID: 4648 RVA: 0x0002811E File Offset: 0x0002631E
		internal CreateUMCallDataRecordCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00028131 File Offset: 0x00026331
		public CreateUMCallDataRecordResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateUMCallDataRecordResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001836 RID: 6198
		private object[] results;
	}
}
