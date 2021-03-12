using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000031 RID: 49
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class CreateMemberEncryptedExCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00006CC7 File Offset: 0x00004EC7
		internal CreateMemberEncryptedExCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006CDA File Offset: 0x00004EDA
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006CEF File Offset: 0x00004EEF
		public string slt
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040000AF RID: 175
		private object[] results;
	}
}
