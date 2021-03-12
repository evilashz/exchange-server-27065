using System;
using System.Management.Automation.Runspaces;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200000A RID: 10
	internal class CmdletConfiguration
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003BD7 File Offset: 0x00001DD7
		internal static CmdletConfigurationEntry[] SupportCmdletConfigurationEntries
		{
			get
			{
				return CmdletConfiguration.supportCmdletConfigurationEntries;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003BDE File Offset: 0x00001DDE
		internal static FormatConfigurationEntry[] SupportFormatConfigurationEntries
		{
			get
			{
				return CmdletConfiguration.supportFormatConfigurationEntries;
			}
		}

		// Token: 0x04000041 RID: 65
		private static CmdletConfigurationEntry[] supportCmdletConfigurationEntries = new CmdletConfigurationEntry[]
		{
			new CmdletConfigurationEntry("Get-DatabaseEvent", typeof(GetDatabaseEvent), "Microsoft.Exchange.Support-Help.xml"),
			new CmdletConfigurationEntry("Get-DatabaseEventWatermark", typeof(GetDatabaseEventWatermark), "Microsoft.Exchange.Support-Help.xml"),
			new CmdletConfigurationEntry("Get-CalendarValidationResult", typeof(GetCalendarValidationResult), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-ExchangeDiagnosticInfo", typeof(GetExchangeDiagnosticInfo), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Test-Message", typeof(TestMessage), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-MailboxActivityLog", typeof(GetMailboxActivityLog), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Repair-Migration", typeof(RepairMigration), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-FolderRestriction", typeof(GetFolderRestriction), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-OABFile", typeof(GetOABFile), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-MailboxFileStore", typeof(GetMailboxFileStore), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Remove-MailboxFileStore", typeof(RemoveMailboxFileStore), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-WebDnsRecord", typeof(GetWebDnsRecord), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-GroupCapacity", typeof(GetGroupCapacity), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-GroupBlackout", typeof(GetGroupBlackout), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-Constraint", typeof(GetConstraint), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-SymphonyGroup", typeof(GetSymphonyGroup), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-TenantReadiness", typeof(GetTenantReadiness), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-UpgradeWorkItem", typeof(GetUpgradeWorkItem), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Set-Constraint", typeof(SetConstraint), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Set-GroupCapacity", typeof(SetGroupCapacity), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Set-GroupBlackout", typeof(SetGroupBlackout), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Set-SymphonyGroup", typeof(SetSymphonyGroup), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Set-TenantReadiness", typeof(SetTenantReadiness), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Set-UpgradeWorkItem", typeof(SetUpgradeWorkItem), "Microsoft.Exchange.Management-Help.xml"),
			new CmdletConfigurationEntry("Get-UnifiedGroup", typeof(GetUnifiedGroup), null),
			new CmdletConfigurationEntry("New-UnifiedGroup", typeof(NewUnifiedGroup), null),
			new CmdletConfigurationEntry("Remove-UnifiedGroup", typeof(RemoveUnifiedGroup), null),
			new CmdletConfigurationEntry("Set-UnifiedGroup", typeof(SetUnifiedGroup), null)
		};

		// Token: 0x04000042 RID: 66
		private static FormatConfigurationEntry[] supportFormatConfigurationEntries = new FormatConfigurationEntry[]
		{
			new FormatConfigurationEntry("Exchange.Support.format.ps1xml")
		};
	}
}
