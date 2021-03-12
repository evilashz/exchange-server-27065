using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003C4 RID: 964
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ConvertIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DA5 RID: 7589 RVA: 0x0002A96A File Offset: 0x00028B6A
		internal ConvertIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0002A97D File Offset: 0x00028B7D
		public ConvertIdResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ConvertIdResponseType)this.results[0];
			}
		}

		// Token: 0x04001382 RID: 4994
		private object[] results;
	}
}
