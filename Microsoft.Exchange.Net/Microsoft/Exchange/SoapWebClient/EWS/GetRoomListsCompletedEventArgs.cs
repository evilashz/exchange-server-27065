using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000507 RID: 1287
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetRoomListsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x00027976 File Offset: 0x00025B76
		internal GetRoomListsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00027989 File Offset: 0x00025B89
		public GetRoomListsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetRoomListsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001805 RID: 6149
		private object[] results;
	}
}
