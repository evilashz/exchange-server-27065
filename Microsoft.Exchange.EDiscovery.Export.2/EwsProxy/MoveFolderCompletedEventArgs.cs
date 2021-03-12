using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003D0 RID: 976
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class MoveFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DC9 RID: 7625 RVA: 0x0002AA5A File Offset: 0x00028C5A
		internal MoveFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0002AA6D File Offset: 0x00028C6D
		public MoveFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MoveFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001388 RID: 5000
		private object[] results;
	}
}
