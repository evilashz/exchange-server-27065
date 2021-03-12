using System;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000AF RID: 175
	public abstract class ResourceBase : DataSourceService
	{
		// Token: 0x06001C45 RID: 7237 RVA: 0x00058673 File Offset: 0x00056873
		public PowerShellResults<O> GetObject<O>(Identity identity) where O : ResourceConfigurationBase
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<O>("Get-CalendarProcessing", identity);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00058688 File Offset: 0x00056888
		public PowerShellResults<O> SetObject<O, U>(Identity identity, U properties) where O : ResourceConfigurationBase where U : SetResourceConfigurationBase
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<O> result;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				result = base.SetObject<O, U>("Set-CalendarProcessing", identity, properties);
			}
			return result;
		}

		// Token: 0x04001B94 RID: 7060
		internal const string GetCmdlet = "Get-CalendarProcessing";

		// Token: 0x04001B95 RID: 7061
		internal const string SetCmdlet = "Set-CalendarProcessing";

		// Token: 0x04001B96 RID: 7062
		internal const string Resource = "Resource+";

		// Token: 0x04001B97 RID: 7063
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001B98 RID: 7064
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001B99 RID: 7065
		internal const string GetObjectRole = "Resource+Get-CalendarProcessing?Identity@R:Self";

		// Token: 0x04001B9A RID: 7066
		internal const string SetObjectRole = "Resource+Get-CalendarProcessing?Identity@R:Self+Set-CalendarProcessing?Identity@W:Self";
	}
}
