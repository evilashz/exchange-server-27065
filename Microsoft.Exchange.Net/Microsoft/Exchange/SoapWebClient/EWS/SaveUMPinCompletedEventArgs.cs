using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000575 RID: 1397
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class SaveUMPinCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x0002820E File Offset: 0x0002640E
		internal SaveUMPinCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00028221 File Offset: 0x00026421
		public SaveUMPinResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SaveUMPinResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400183C RID: 6204
		private object[] results;
	}
}
