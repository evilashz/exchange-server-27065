using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003D8 RID: 984
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x0002AAFA File Offset: 0x00028CFA
		internal GetEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0002AB0D File Offset: 0x00028D0D
		public GetEventsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetEventsResponseType)this.results[0];
			}
		}

		// Token: 0x0400138C RID: 5004
		private object[] results;
	}
}
