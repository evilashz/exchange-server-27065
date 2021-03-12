using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000404 RID: 1028
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E65 RID: 7781 RVA: 0x0002AE6A File Offset: 0x0002906A
		internal CreateUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0002AE7D File Offset: 0x0002907D
		public CreateUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040013A2 RID: 5026
		private object[] results;
	}
}
