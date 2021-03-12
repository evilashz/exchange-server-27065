using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000476 RID: 1142
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetUserPhotoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001FBB RID: 8123 RVA: 0x0002B752 File Offset: 0x00029952
		internal GetUserPhotoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0002B765 File Offset: 0x00029965
		public GetUserPhotoResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserPhotoResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013DB RID: 5083
		private object[] results;
	}
}
