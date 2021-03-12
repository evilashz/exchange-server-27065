using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200046A RID: 1130
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class SetImListMigrationCompletedCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x0002B662 File Offset: 0x00029862
		internal SetImListMigrationCompletedCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0002B675 File Offset: 0x00029875
		public SetImListMigrationCompletedResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetImListMigrationCompletedResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D5 RID: 5077
		private object[] results;
	}
}
