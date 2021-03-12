using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200044A RID: 1098
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetNonIndexableItemDetailsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F37 RID: 7991 RVA: 0x0002B3E2 File Offset: 0x000295E2
		internal GetNonIndexableItemDetailsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0002B3F5 File Offset: 0x000295F5
		public GetNonIndexableItemDetailsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetNonIndexableItemDetailsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013C5 RID: 5061
		private object[] results;
	}
}
