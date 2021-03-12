using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200009E RID: 158
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetUserSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x0001F9FB File Offset: 0x0001DBFB
		internal GetUserSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001FA0E File Offset: 0x0001DC0E
		public GetUserSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserSettingsResponse)this.results[0];
			}
		}

		// Token: 0x04000357 RID: 855
		private object[] results;
	}
}
