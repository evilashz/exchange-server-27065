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
	// Token: 0x02000C36 RID: 3126
	[Cmdlet("Remove", "ActiveSyncVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMobileSyncVirtualDirectory : RemoveExchangeVirtualDirectory<ADMobileVirtualDirectory>
	{
		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x06007663 RID: 30307 RVA: 0x001E34E3 File Offset: 0x001E16E3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMobileSyncVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x001E34F8 File Offset: 0x001E16F8
		protected override void DeleteFromMetabase()
		{
			if (string.IsNullOrEmpty(base.DataObject.MetabasePath))
			{
				string webSiteRoot = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebSiteRoot, this.Identity.Server, IisUtility.FindWebSiteRootPath(this.Identity.Name, this.Identity.Server), null);
				string virtualDirectoryName = "Microsoft-Server-ActiveSync";
				DeleteVirtualDirectory.DeleteFromMetabase(webSiteRoot, virtualDirectoryName, this.ChildVirtualDirectoryNames);
				return;
			}
			base.DeleteFromMetabase();
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x001E3560 File Offset: 0x001E1760
		protected override void PreDeleteFromMetabase()
		{
			if (!DirectoryEntry.Exists(base.DataObject.MetabasePath))
			{
				return;
			}
			try
			{
				MobileSyncVirtualDirectoryHelper.UninstallIsapiFilter(base.DataObject);
			}
			catch (Exception)
			{
				this.WriteWarning(Strings.ActiveSyncMetabaseIsapiUninstallFailure);
				throw;
			}
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x001E35AC File Offset: 0x001E17AC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.DataObject.ExchangeVersion.IsOlderThan(ADMobileVirtualDirectory.MinimumSupportedExchangeObjectVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorRemoveOlderVirtualDirectory(base.DataObject.Identity.ToString(), ADMobileVirtualDirectory.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
