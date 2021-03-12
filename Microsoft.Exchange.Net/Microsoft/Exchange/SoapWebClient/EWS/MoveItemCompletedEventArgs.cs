using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004CF RID: 1231
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class MoveItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x00027516 File Offset: 0x00025716
		internal MoveItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00027529 File Offset: 0x00025729
		public MoveItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MoveItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017E9 RID: 6121
		private object[] results;
	}
}
