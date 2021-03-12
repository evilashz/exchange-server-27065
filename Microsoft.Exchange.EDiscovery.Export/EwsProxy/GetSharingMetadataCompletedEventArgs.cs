using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200041C RID: 1052
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSharingMetadataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EAD RID: 7853 RVA: 0x0002B04A File Offset: 0x0002924A
		internal GetSharingMetadataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0002B05D File Offset: 0x0002925D
		public GetSharingMetadataResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetSharingMetadataResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013AE RID: 5038
		private object[] results;
	}
}
