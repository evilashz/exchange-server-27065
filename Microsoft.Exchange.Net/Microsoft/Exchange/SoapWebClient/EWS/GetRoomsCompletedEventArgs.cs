using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000509 RID: 1289
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetRoomsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x0002799E File Offset: 0x00025B9E
		internal GetRoomsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x000279B1 File Offset: 0x00025BB1
		public GetRoomsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetRoomsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001806 RID: 6150
		private object[] results;
	}
}
