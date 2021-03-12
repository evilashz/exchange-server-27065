using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003EA RID: 1002
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class UpdateItemInRecoverableItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x0002AC62 File Offset: 0x00028E62
		internal UpdateItemInRecoverableItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0002AC75 File Offset: 0x00028E75
		public UpdateItemInRecoverableItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateItemInRecoverableItemsResponseType)this.results[0];
			}
		}

		// Token: 0x04001395 RID: 5013
		private object[] results;
	}
}
