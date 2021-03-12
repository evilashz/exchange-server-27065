using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000428 RID: 1064
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetRoomsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x0002B13A File Offset: 0x0002933A
		internal GetRoomsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0002B14D File Offset: 0x0002934D
		public GetRoomsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetRoomsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B4 RID: 5044
		private object[] results;
	}
}
