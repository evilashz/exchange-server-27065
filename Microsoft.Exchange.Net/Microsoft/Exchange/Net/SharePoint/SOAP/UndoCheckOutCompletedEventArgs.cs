using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x02000957 RID: 2391
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UndoCheckOutCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060033F3 RID: 13299 RVA: 0x0007FA62 File Offset: 0x0007DC62
		internal UndoCheckOutCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x0007FA75 File Offset: 0x0007DC75
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x04002C3E RID: 11326
		private object[] results;
	}
}
