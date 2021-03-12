using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003FE RID: 1022
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class AddDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x0002ADF2 File Offset: 0x00028FF2
		internal AddDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0002AE05 File Offset: 0x00029005
		public AddDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400139F RID: 5023
		private object[] results;
	}
}
