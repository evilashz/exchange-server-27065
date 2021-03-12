using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200056B RID: 1387
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUMCallDataRecordsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x00028146 File Offset: 0x00026346
		internal GetUMCallDataRecordsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00028159 File Offset: 0x00026359
		public GetUMCallDataRecordsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUMCallDataRecordsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001837 RID: 6199
		private object[] results;
	}
}
