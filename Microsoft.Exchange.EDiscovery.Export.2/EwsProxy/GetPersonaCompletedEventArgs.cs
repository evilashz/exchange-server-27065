using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000436 RID: 1078
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetPersonaCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EFB RID: 7931 RVA: 0x0002B252 File Offset: 0x00029452
		internal GetPersonaCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0002B265 File Offset: 0x00029465
		public GetPersonaResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetPersonaResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013BB RID: 5051
		private object[] results;
	}
}
