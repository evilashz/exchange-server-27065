using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200054B RID: 1355
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SetImListMigrationCompletedCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x00027EC6 File Offset: 0x000260C6
		internal SetImListMigrationCompletedCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00027ED9 File Offset: 0x000260D9
		public SetImListMigrationCompletedResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetImListMigrationCompletedResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001827 RID: 6183
		private object[] results;
	}
}
