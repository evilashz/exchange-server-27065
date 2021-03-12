using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C11 RID: 3089
	[Cmdlet("Get", "OabVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetOabVirtualDirectory : GetExchangeVirtualDirectory<ADOabVirtualDirectory>
	{
		// Token: 0x0600749A RID: 29850 RVA: 0x001DBF0C File Offset: 0x001DA10C
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			((ADOabVirtualDirectory)dataObject).OAuthAuthentication = ((ADOabVirtualDirectory)dataObject).InternalAuthenticationMethods.Contains(AuthenticationMethod.OAuth);
			((ADOabVirtualDirectory)dataObject).RequireSSL = IisUtility.SSLEnabled(dataObject.MetabasePath);
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(dataObject.MetabasePath))
			{
				((ADOabVirtualDirectory)dataObject).BasicAuthentication = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic);
				((ADOabVirtualDirectory)dataObject).WindowsAuthentication = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Ntlm);
			}
			dataObject.ResetChangeTracking();
			TaskLogger.LogExit();
		}

		// Token: 0x0600749B RID: 29851 RVA: 0x001DBFB0 File Offset: 0x001DA1B0
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x001DBFB3 File Offset: 0x001DA1B3
		protected override LocalizedString GetMissingMetabaseEntryWarning(ExchangeVirtualDirectory vdir)
		{
			return Strings.OabVirtualDirectoryAdOrphanFound(vdir.Id.Name);
		}
	}
}
