using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009F9 RID: 2553
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class AcquireTemplateInformationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060037B8 RID: 14264 RVA: 0x0008D191 File Offset: 0x0008B391
		internal AcquireTemplateInformationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x0008D1A4 File Offset: 0x0008B3A4
		public TemplateInformation Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (TemplateInformation)this.results[0];
			}
		}

		// Token: 0x04002F38 RID: 12088
		private object[] results;
	}
}
