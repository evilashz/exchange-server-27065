using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000549 RID: 1353
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class SetImGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x00027E9E File Offset: 0x0002609E
		internal SetImGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00027EB1 File Offset: 0x000260B1
		public SetImGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetImGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001826 RID: 6182
		private object[] results;
	}
}
