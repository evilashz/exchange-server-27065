using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200049B RID: 1179
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class FindFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x00027106 File Offset: 0x00025306
		internal FindFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00027119 File Offset: 0x00025319
		public FindFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017CF RID: 6095
		private object[] results;
	}
}
