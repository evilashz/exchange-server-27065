using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000573 RID: 1395
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class ValidateUMPinCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x000281E6 File Offset: 0x000263E6
		internal ValidateUMPinCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x000281F9 File Offset: 0x000263F9
		public ValidateUMPinResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ValidateUMPinResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400183B RID: 6203
		private object[] results;
	}
}
