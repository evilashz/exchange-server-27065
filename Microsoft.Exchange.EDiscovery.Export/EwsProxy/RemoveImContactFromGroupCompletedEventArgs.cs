using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000458 RID: 1112
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RemoveImContactFromGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F61 RID: 8033 RVA: 0x0002B4FA File Offset: 0x000296FA
		internal RemoveImContactFromGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0002B50D File Offset: 0x0002970D
		public RemoveImContactFromGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveImContactFromGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013CC RID: 5068
		private object[] results;
	}
}
