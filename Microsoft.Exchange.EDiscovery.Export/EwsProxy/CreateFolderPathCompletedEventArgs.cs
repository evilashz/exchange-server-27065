using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003C8 RID: 968
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateFolderPathCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DB1 RID: 7601 RVA: 0x0002A9BA File Offset: 0x00028BBA
		internal CreateFolderPathCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0002A9CD File Offset: 0x00028BCD
		public CreateFolderPathResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateFolderPathResponseType)this.results[0];
			}
		}

		// Token: 0x04001384 RID: 4996
		private object[] results;
	}
}
