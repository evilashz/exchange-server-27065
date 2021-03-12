using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009F0 RID: 2544
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class FindServiceLocationsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600377E RID: 14206 RVA: 0x0008CD6B File Offset: 0x0008AF6B
		internal FindServiceLocationsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x0008CD7E File Offset: 0x0008AF7E
		public ServiceLocationResponse[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ServiceLocationResponse[])this.results[0];
			}
		}

		// Token: 0x04002F26 RID: 12070
		private object[] results;
	}
}
