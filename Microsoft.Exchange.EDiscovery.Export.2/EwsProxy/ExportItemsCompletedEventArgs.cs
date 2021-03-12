using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003C2 RID: 962
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class ExportItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D9F RID: 7583 RVA: 0x0002A942 File Offset: 0x00028B42
		internal ExportItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0002A955 File Offset: 0x00028B55
		public ExportItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExportItemsResponseType)this.results[0];
			}
		}

		// Token: 0x04001381 RID: 4993
		private object[] results;
	}
}
