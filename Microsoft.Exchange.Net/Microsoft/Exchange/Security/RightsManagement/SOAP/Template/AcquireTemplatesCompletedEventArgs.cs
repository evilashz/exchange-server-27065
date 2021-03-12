using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009FB RID: 2555
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class AcquireTemplatesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x0008D1B9 File Offset: 0x0008B3B9
		internal AcquireTemplatesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x0008D1CC File Offset: 0x0008B3CC
		public GuidTemplate[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GuidTemplate[])this.results[0];
			}
		}

		// Token: 0x04002F39 RID: 12089
		private object[] results;
	}
}
