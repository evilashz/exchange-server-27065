using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C12 RID: 3090
	[Cmdlet("Get", "OwaVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetOwaVirtualDirectory : GetExchangeVirtualDirectory<ADOwaVirtualDirectory>
	{
		// Token: 0x0600749E RID: 29854 RVA: 0x001DBFD0 File Offset: 0x001DA1D0
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADOwaVirtualDirectory adowaVirtualDirectory = (ADOwaVirtualDirectory)dataObject;
			try
			{
				WebAppVirtualDirectoryHelper.UpdateFromMetabase(adowaVirtualDirectory);
				if (adowaVirtualDirectory.GzipLevel != GzipLevel.Off && !adowaVirtualDirectory.IsExchange2007OrLater && this.IsMailboxRoleInstalled(adowaVirtualDirectory))
				{
					this.WriteWarning(Strings.OwaGzipEnabledOnLegacyVirtualDirectoryWhenMailboxRoleInstalledWarning(adowaVirtualDirectory.Id.Name));
				}
			}
			catch (Exception ex)
			{
				TaskLogger.Trace("Exception occurred: {0}", new object[]
				{
					ex.Message
				});
				this.WriteError(new LocalizedException(Strings.OwaMetabaseGetPropertiesFailure), ErrorCategory.InvalidOperation, null, false);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x001DC07C File Offset: 0x001DA27C
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}

		// Token: 0x060074A0 RID: 29856 RVA: 0x001DC07F File Offset: 0x001DA27F
		protected override LocalizedString GetMissingMetabaseEntryWarning(ExchangeVirtualDirectory vdir)
		{
			return Strings.OwaAdOrphanFound(vdir.Id.Name);
		}

		// Token: 0x060074A1 RID: 29857 RVA: 0x001DC094 File Offset: 0x001DA294
		private bool IsMailboxRoleInstalled(ADOwaVirtualDirectory dataObject)
		{
			Server server = (Server)base.GetDataObject<Server>(new ServerIdParameter(dataObject.Server), base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(dataObject.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(dataObject.Server.ToString())));
			return server.IsMailboxServer;
		}
	}
}
