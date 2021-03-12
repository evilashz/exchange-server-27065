using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000AD RID: 173
	internal static class SharedConfigurationTaskHelper
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0001A5B8 File Offset: 0x000187B8
		public static void Validate(Task task, SharedTenantConfigurationMode taskSharedTenantConfigurationMode, LazilyInitialized<SharedTenantConfigurationState> currentOrgState, string targetObject)
		{
			if (taskSharedTenantConfigurationMode == SharedTenantConfigurationMode.NotShared)
			{
				return;
			}
			if (SharedTenantConfigurationState.UnSupported == currentOrgState.Value || SharedTenantConfigurationState.NotShared == currentOrgState.Value)
			{
				return;
			}
			if (SharedTenantConfigurationMode.Static == taskSharedTenantConfigurationMode && (currentOrgState.Value & SharedTenantConfigurationState.Static) != SharedTenantConfigurationState.UnSupported)
			{
				task.WriteError(new InvalidOperationInDehydratedContextException(Strings.ErrorWriteOpOnDehydratedTenant), ExchangeErrorCategory.Context, targetObject);
			}
			if ((currentOrgState.Value & SharedTenantConfigurationState.Dehydrated) != SharedTenantConfigurationState.UnSupported)
			{
				task.WriteError(new InvalidOperationInDehydratedContextException(Strings.ErrorWriteOpOnDehydratedTenant), ExchangeErrorCategory.Context, targetObject);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001A628 File Offset: 0x00018828
		public static bool ShouldPrompt(Task task, SharedTenantConfigurationMode taskSharedTenantConfigurationMode, LazilyInitialized<SharedTenantConfigurationState> currentOrgState)
		{
			return taskSharedTenantConfigurationMode != SharedTenantConfigurationMode.NotShared && (currentOrgState.Value & SharedTenantConfigurationState.Shared) != SharedTenantConfigurationState.UnSupported;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001A63F File Offset: 0x0001883F
		internal static void VerifyIsNotTinyTenant(LazilyInitialized<SharedTenantConfigurationState> configurationState, Task.ErrorLoggerDelegate writeError)
		{
			if (null == configurationState)
			{
				throw new ArgumentNullException("configurationState");
			}
			if (writeError == null)
			{
				throw new ArgumentNullException("writeError");
			}
			if ((configurationState.Value & SharedTenantConfigurationState.Dehydrated) != SharedTenantConfigurationState.UnSupported)
			{
				SharedConfigurationTaskHelper.WriteTinyTenantError(writeError);
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001A67B File Offset: 0x0001887B
		internal static void VerifyIsNotTinyTenant(OrganizationId organizationId, Task.ErrorLoggerDelegate writeError)
		{
			if (null == organizationId)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (writeError == null)
			{
				throw new ArgumentNullException("writeError");
			}
			if (SharedConfiguration.GetSharedConfiguration(organizationId) != null)
			{
				SharedConfigurationTaskHelper.WriteTinyTenantError(writeError);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001A6AD File Offset: 0x000188AD
		private static void WriteTinyTenantError(Task.ErrorLoggerDelegate writeError)
		{
			writeError(new InvalidOperationInDehydratedContextException(Strings.ErrorWriteOpOnDehydratedTenant), ExchangeErrorCategory.Context, null);
		}
	}
}
