using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004A9 RID: 1193
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CreateFolderPathCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002721E File Offset: 0x0002541E
		internal CreateFolderPathCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00027231 File Offset: 0x00025431
		public CreateFolderPathResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateFolderPathResponseType)this.results[0];
			}
		}

		// Token: 0x040017D6 RID: 6102
		private object[] results;
	}
}
