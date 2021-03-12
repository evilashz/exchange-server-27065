using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000426 RID: 1062
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetRoomListsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001ECB RID: 7883 RVA: 0x0002B112 File Offset: 0x00029312
		internal GetRoomListsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0002B125 File Offset: 0x00029325
		public GetRoomListsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetRoomListsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B3 RID: 5043
		private object[] results;
	}
}
