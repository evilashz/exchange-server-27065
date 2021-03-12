using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000798 RID: 1944
	internal class RemoteAppEntry
	{
		// Token: 0x060054F2 RID: 21746 RVA: 0x0012CA3B File Offset: 0x0012AC3B
		internal RemoteAppEntry(string appName, string appURI)
		{
			this._remoteAppName = appName;
			this._remoteAppURI = appURI;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x0012CA51 File Offset: 0x0012AC51
		internal string GetAppURI()
		{
			return this._remoteAppURI;
		}

		// Token: 0x040026CD RID: 9933
		private string _remoteAppName;

		// Token: 0x040026CE RID: 9934
		private string _remoteAppURI;
	}
}
