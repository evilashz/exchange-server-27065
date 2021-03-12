using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003C6 RID: 966
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DAB RID: 7595 RVA: 0x0002A992 File Offset: 0x00028B92
		internal CreateFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0002A9A5 File Offset: 0x00028BA5
		public CreateFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001383 RID: 4995
		private object[] results;
	}
}
