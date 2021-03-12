using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000547 RID: 1351
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class RemoveImGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011C2 RID: 4546 RVA: 0x00027E76 File Offset: 0x00026076
		internal RemoveImGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00027E89 File Offset: 0x00026089
		public RemoveImGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveImGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001825 RID: 6181
		private object[] results;
	}
}
