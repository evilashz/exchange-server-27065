using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000052 RID: 82
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetManagementCertificateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00006DF4 File Offset: 0x00004FF4
		internal GetManagementCertificateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00006E07 File Offset: 0x00005007
		public CertData Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CertData)this.results[0];
			}
		}

		// Token: 0x040000B6 RID: 182
		private object[] results;
	}
}
