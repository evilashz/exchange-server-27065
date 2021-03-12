using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200049E RID: 1182
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MobileDevices : DataSourceService, IMobileDevices, IGetListService<MobileDeviceFilter, MobileDeviceRow>, IGetObjectService<MobileDevice>, IGetObjectForListService<MobileDeviceRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06003AD3 RID: 15059 RVA: 0x000B2198 File Offset: 0x000B0398
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDeviceStatistics?Mailbox&ShowRecoveryPassword&ActiveSync@R:Organization+Get-CASMailbox?Identity@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDeviceStatistics?Mailbox&ShowRecoveryPassword&ActiveSync@R:Self+Get-CASMailbox?Identity@R:Self")]
		public PowerShellResults<MobileDeviceRow> GetList(MobileDeviceFilter filter, SortOptions sort)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Get-CASMailbox").AddParameter("ActiveSyncDebugLogging", new SwitchParameter(true));
			PowerShellResults<CASMailbox> @object = base.GetObject<CASMailbox>(psCommand, (filter == null) ? Identity.FromExecutingUserId() : filter.Mailbox);
			bool isLoggingRunning = false;
			if (@object.HasValue)
			{
				isLoggingRunning = @object.Value.ActiveSyncDebugLogging;
			}
			PowerShellResults<MobileDeviceRow> list = base.GetList<MobileDeviceRow, MobileDeviceFilter>("Get-MobileDeviceStatistics", filter, sort);
			if (@object.HasValue)
			{
				foreach (MobileDeviceRow mobileDeviceRow in list.Output)
				{
					mobileDeviceRow.IsLoggingRunning = isLoggingRunning;
				}
			}
			return list;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000B2239 File Offset: 0x000B0439
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDeviceStatistics?Identity@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDeviceStatistics?Identity@R:Self")]
		public PowerShellResults<MobileDevice> GetObject(Identity identity)
		{
			return base.GetObject<MobileDevice>("Get-MobileDeviceStatistics", identity);
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000B2248 File Offset: 0x000B0448
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDeviceStatistics?Identity&ShowRecoveryPassword@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDeviceStatistics?Identity&ShowRecoveryPassword@R:Self")]
		public PowerShellResults<MobileDeviceRow> GetObjectForList(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Get-MobileDeviceStatistics");
			pscommand.AddParameter("ShowRecoveryPassword", true);
			return base.GetObject<MobileDeviceRow>(pscommand, identity);
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000B2280 File Offset: 0x000B0480
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-CASMailbox?Identity&ActiveSyncDebugLogging@W:Self")]
		public PowerShellResults StartLogging(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Set-CASMailbox");
			pscommand.AddParameter("Identity", Identity.FromExecutingUserId());
			pscommand.AddParameter("ActiveSyncDebugLogging", true);
			return base.Invoke(pscommand);
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000B22C8 File Offset: 0x000B04C8
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-CASMailbox?Identity&ActiveSyncDebugLogging@W:Self+Get-MobileDeviceStatistics?Identity&GetMailboxLog@R:Self")]
		public PowerShellResults StopAndRetrieveLog(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Set-CASMailbox");
			pscommand.AddParameter("Identity", Identity.FromExecutingUserId());
			pscommand.AddParameter("ActiveSyncDebugLogging", false);
			PowerShellResults powerShellResults = base.Invoke(pscommand);
			PSCommand psCommand = new PSCommand().AddCommand("Get-MobileDeviceStatistics").AddParameter("GetMailboxLog", new SwitchParameter(true));
			return powerShellResults.MergeErrors(base.Invoke(psCommand, identities, parameters));
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000B2344 File Offset: 0x000B0544
		[PrincipalPermission(SecurityAction.Demand, Role = "Clear-MobileDevice?Identity&Cancel@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Clear-MobileDevice?Identity&Cancel@W:Self")]
		public PowerShellResults<MobileDeviceRow> BlockOrWipeDevice(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Clear-MobileDevice").AddParameter("Cancel", new SwitchParameter(false));
			return base.InvokeAndGetObject<MobileDeviceRow>(psCommand, identities, parameters);
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000B2380 File Offset: 0x000B0580
		[PrincipalPermission(SecurityAction.Demand, Role = "Clear-MobileDevice?Identity&Cancel@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Clear-MobileDevice?Identity&Cancel@W:Self")]
		public PowerShellResults<MobileDeviceRow> UnBlockOrCancelWipeDevice(Identity[] identities, BaseWebServiceParameters parameters)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Clear-MobileDevice").AddParameter("Cancel", new SwitchParameter(true));
			return base.InvokeAndGetObject<MobileDeviceRow>(psCommand, identities, parameters);
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000B23BB File Offset: 0x000B05BB
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-MobileDevice?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-MobileDevice?Identity@W:Self")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-MobileDevice", identities, parameters);
		}

		// Token: 0x04002726 RID: 10022
		internal const string GetCmdlet = "Get-MobileDeviceStatistics";

		// Token: 0x04002727 RID: 10023
		internal const string SetCmdlet = "Clear-MobileDevice";

		// Token: 0x04002728 RID: 10024
		internal const string RemoveCmdlet = "Remove-MobileDevice";

		// Token: 0x04002729 RID: 10025
		internal const string GetLoggingCmdlet = "Get-CASMailbox";

		// Token: 0x0400272A RID: 10026
		internal const string SetLoggingCmdlet = "Set-CASMailbox";

		// Token: 0x0400272B RID: 10027
		internal const string GetListRole_Self = "Get-MobileDeviceStatistics?Mailbox&ShowRecoveryPassword&ActiveSync@R:Self+Get-CASMailbox?Identity@R:Self";

		// Token: 0x0400272C RID: 10028
		internal const string GetListRole_Org = "Get-MobileDeviceStatistics?Mailbox&ShowRecoveryPassword&ActiveSync@R:Organization+Get-CASMailbox?Identity@R:Organization";

		// Token: 0x0400272D RID: 10029
		internal const string GetObjectRole_Self = "Get-MobileDeviceStatistics?Identity@R:Self";

		// Token: 0x0400272E RID: 10030
		internal const string GetObjectRole_Org = "Get-MobileDeviceStatistics?Identity@R:Organization";

		// Token: 0x0400272F RID: 10031
		internal const string GetObjectForListRole_Self = "Get-MobileDeviceStatistics?Identity&ShowRecoveryPassword@R:Self";

		// Token: 0x04002730 RID: 10032
		internal const string GetObjectForListRole_Org = "Get-MobileDeviceStatistics?Identity&ShowRecoveryPassword@R:Organization";

		// Token: 0x04002731 RID: 10033
		private const string StartLoggingRole = "Set-CASMailbox?Identity&ActiveSyncDebugLogging@W:Self";

		// Token: 0x04002732 RID: 10034
		private const string StopLoggingAndRetrieveRole = "Set-CASMailbox?Identity&ActiveSyncDebugLogging@W:Self+Get-MobileDeviceStatistics?Identity&GetMailboxLog@R:Self";

		// Token: 0x04002733 RID: 10035
		internal const string BlockOrWipeDeviceRole_Self = "Clear-MobileDevice?Identity&Cancel@W:Self";

		// Token: 0x04002734 RID: 10036
		internal const string BlockOrWipeDeviceRole_Org = "Clear-MobileDevice?Identity&Cancel@W:Organization";

		// Token: 0x04002735 RID: 10037
		internal const string RemoveObjectsRole_Self = "Remove-MobileDevice?Identity@W:Self";

		// Token: 0x04002736 RID: 10038
		internal const string RemoveObjectsRole_Org = "Remove-MobileDevice?Identity@W:Organization";
	}
}
