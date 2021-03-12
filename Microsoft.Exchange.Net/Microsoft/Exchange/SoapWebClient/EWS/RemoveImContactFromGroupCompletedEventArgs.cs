using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000539 RID: 1337
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class RemoveImContactFromGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x00027D5E File Offset: 0x00025F5E
		internal RemoveImContactFromGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00027D71 File Offset: 0x00025F71
		public RemoveImContactFromGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveImContactFromGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400181E RID: 6174
		private object[] results;
	}
}
