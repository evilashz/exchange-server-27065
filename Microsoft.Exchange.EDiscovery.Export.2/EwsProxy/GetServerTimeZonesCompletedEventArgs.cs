using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003B8 RID: 952
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetServerTimeZonesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D81 RID: 7553 RVA: 0x0002A87A File Offset: 0x00028A7A
		internal GetServerTimeZonesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x0002A88D File Offset: 0x00028A8D
		public GetServerTimeZonesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetServerTimeZonesResponseType)this.results[0];
			}
		}

		// Token: 0x0400137C RID: 4988
		private object[] results;
	}
}
