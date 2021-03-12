using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000557 RID: 1367
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetUserPhotoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011F2 RID: 4594 RVA: 0x00027FB6 File Offset: 0x000261B6
		internal GetUserPhotoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00027FC9 File Offset: 0x000261C9
		public GetUserPhotoResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserPhotoResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400182D RID: 6189
		private object[] results;
	}
}
