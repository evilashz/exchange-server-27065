using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000454 RID: 1108
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class AddNewTelUriContactToGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x0002B4AA File Offset: 0x000296AA
		internal AddNewTelUriContactToGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0002B4BD File Offset: 0x000296BD
		public AddNewTelUriContactToGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddNewTelUriContactToGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013CA RID: 5066
		private object[] results;
	}
}
