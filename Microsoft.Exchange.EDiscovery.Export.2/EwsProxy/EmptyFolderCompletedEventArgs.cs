using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003CC RID: 972
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class EmptyFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DBD RID: 7613 RVA: 0x0002AA0A File Offset: 0x00028C0A
		internal EmptyFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0002AA1D File Offset: 0x00028C1D
		public EmptyFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (EmptyFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001386 RID: 4998
		private object[] results;
	}
}
