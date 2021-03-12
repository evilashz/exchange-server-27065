using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200030F RID: 783
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class QuarantinedDevices : DataSourceService, IQuarantinedDevices, IGetListService<QuarantinedDeviceFilter, QuarantinedDevice>
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x0008CD1F File Offset: 0x0008AF1F
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDevice?ResultSize&Filter&ActiveSync@R:Organization")]
		public PowerShellResults<QuarantinedDevice> GetList(QuarantinedDeviceFilter filter, SortOptions sort)
		{
			return base.GetList<QuarantinedDevice, QuarantinedDeviceFilter>("Get-MobileDevice", filter, sort);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x0008CD30 File Offset: 0x0008AF30
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDevice?Identity@R:Organization+Get-CASMailbox?Identity@R:Organization+Set-CASMailbox?Identity&ActiveSyncAllowedDeviceIDs@W:Organization")]
		public PowerShellResults AllowDevice(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PowerShellResults powerShellResults = new PowerShellResults();
			foreach (Identity identity in identities)
			{
				PowerShellResults<MobileDevice> @object = base.GetObject<MobileDevice>("Get-MobileDevice", identity);
				powerShellResults.MergeErrors<MobileDevice>(@object);
				if (@object.HasValue)
				{
					MobileDevice value = @object.Value;
					Identity identity2 = value.Id.Parent.Parent.ToIdentity();
					PowerShellResults<CASMailbox> object2 = base.GetObject<CASMailbox>("Get-CASMailbox", identity2);
					powerShellResults.MergeErrors<CASMailbox>(object2);
					if (object2.HasValue)
					{
						MultiValuedProperty<string> activeSyncAllowedDeviceIDs = object2.Value.ActiveSyncAllowedDeviceIDs;
						if (!activeSyncAllowedDeviceIDs.Contains(value.DeviceId))
						{
							activeSyncAllowedDeviceIDs.Add(value.DeviceId);
							PSCommand psCommand = new PSCommand().AddCommand("Set-CASMailbox").AddParameter("Identity", identity2).AddParameter("ActiveSyncAllowedDeviceIDs", activeSyncAllowedDeviceIDs);
							powerShellResults.MergeErrors(base.Invoke(psCommand));
						}
					}
				}
			}
			return powerShellResults;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x0008CE28 File Offset: 0x0008B028
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDevice?Identity@R:Organization+Get-CASMailbox?Identity@R:Organization+Set-CASMailbox?Identity&ActiveSyncBlockedDeviceIDs@W:Organization")]
		public PowerShellResults BlockDevice(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PowerShellResults powerShellResults = new PowerShellResults();
			foreach (Identity identity in identities)
			{
				PowerShellResults<MobileDevice> @object = base.GetObject<MobileDevice>("Get-MobileDevice", identity);
				powerShellResults.MergeErrors<MobileDevice>(@object);
				if (@object.HasValue)
				{
					MobileDevice value = @object.Value;
					Identity identity2 = value.Id.Parent.Parent.ToIdentity();
					PowerShellResults<CASMailbox> object2 = base.GetObject<CASMailbox>("Get-CASMailbox", identity2);
					powerShellResults.MergeErrors<CASMailbox>(object2);
					if (object2.HasValue)
					{
						MultiValuedProperty<string> activeSyncBlockedDeviceIDs = object2.Value.ActiveSyncBlockedDeviceIDs;
						if (!activeSyncBlockedDeviceIDs.Contains(value.DeviceId))
						{
							activeSyncBlockedDeviceIDs.Add(value.DeviceId);
							PSCommand psCommand = new PSCommand().AddCommand("Set-CASMailbox").AddParameter("Identity", identity2).AddParameter("ActiveSyncBlockedDeviceIDs", activeSyncBlockedDeviceIDs);
							powerShellResults.MergeErrors(base.Invoke(psCommand));
						}
					}
				}
			}
			return powerShellResults;
		}

		// Token: 0x04002299 RID: 8857
		internal const string GetCmdlet = "Get-MobileDevice";

		// Token: 0x0400229A RID: 8858
		internal const string GetMailboxCmdlet = "Get-CASMailbox";

		// Token: 0x0400229B RID: 8859
		internal const string SetMailboxCmdlet = "Set-CASMailbox";

		// Token: 0x0400229C RID: 8860
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400229D RID: 8861
		internal const string WriteScope = "@W:Organization";

		// Token: 0x0400229E RID: 8862
		private const string GetListRole = "Get-MobileDevice?ResultSize&Filter&ActiveSync@R:Organization";

		// Token: 0x0400229F RID: 8863
		internal const string AllowDeviceRole = "Get-MobileDevice?Identity@R:Organization+Get-CASMailbox?Identity@R:Organization+Set-CASMailbox?Identity&ActiveSyncAllowedDeviceIDs@W:Organization";

		// Token: 0x040022A0 RID: 8864
		internal const string BlockDeviceRole = "Get-MobileDevice?Identity@R:Organization+Get-CASMailbox?Identity@R:Organization+Set-CASMailbox?Identity&ActiveSyncBlockedDeviceIDs@W:Organization";
	}
}
