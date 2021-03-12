using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000466 RID: 1126
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class RemoveImGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F8B RID: 8075 RVA: 0x0002B612 File Offset: 0x00029812
		internal RemoveImGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x0002B625 File Offset: 0x00029825
		public RemoveImGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveImGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D3 RID: 5075
		private object[] results;
	}
}
