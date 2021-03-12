using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000548 RID: 1352
	public class MailboxDatabaseInfo
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x000CB720 File Offset: 0x000C9920
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x000CB728 File Offset: 0x000C9928
		internal LiveIdAuthResult AuthenticationResult { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000CB731 File Offset: 0x000C9931
		// (set) Token: 0x0600214F RID: 8527 RVA: 0x000CB739 File Offset: 0x000C9939
		internal string AuthenticationError { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000CB742 File Offset: 0x000C9942
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x000CB74A File Offset: 0x000C994A
		internal string OriginatingServer { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x000CB753 File Offset: 0x000C9953
		// (set) Token: 0x06002153 RID: 8531 RVA: 0x000CB75B File Offset: 0x000C995B
		public string MailboxDatabaseName { get; set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000CB764 File Offset: 0x000C9964
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x000CB76C File Offset: 0x000C996C
		public Guid MailboxDatabaseGuid { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000CB775 File Offset: 0x000C9975
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x000CB77D File Offset: 0x000C997D
		public string MonitoringAccount { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000CB786 File Offset: 0x000C9986
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x000CB78E File Offset: 0x000C998E
		public string MonitoringAccountDisplayName { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x000CB797 File Offset: 0x000C9997
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x000CB79F File Offset: 0x000C999F
		public string MonitoringAccountDomain { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x000CB7A8 File Offset: 0x000C99A8
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x000CB7B0 File Offset: 0x000C99B0
		public SmtpAddress MonitoringAccountPrimarySmtpAddress { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x000CB7B9 File Offset: 0x000C99B9
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x000CB7D5 File Offset: 0x000C99D5
		public string MonitoringAccountPassword
		{
			get
			{
				if (this.AuthenticationResult != LiveIdAuthResult.Success)
				{
					throw new MailboxNotValidatedException(this.password);
				}
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x000CB7DE File Offset: 0x000C99DE
		// (set) Token: 0x06002161 RID: 8545 RVA: 0x000CB7E6 File Offset: 0x000C99E6
		public string MonitoringAccountSid { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000CB7EF File Offset: 0x000C99EF
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x000CB7F7 File Offset: 0x000C99F7
		public string MonitoringAccountPuid { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000CB800 File Offset: 0x000C9A00
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x000CB808 File Offset: 0x000C9A08
		public string MonitoringAccountPartitionId { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x000CB811 File Offset: 0x000C9A11
		// (set) Token: 0x06002167 RID: 8551 RVA: 0x000CB819 File Offset: 0x000C9A19
		public OrganizationId MonitoringAccountOrganizationId { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000CB822 File Offset: 0x000C9A22
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x000CB82A File Offset: 0x000C9A2A
		public string MonitoringAccountLegacyDN { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000CB833 File Offset: 0x000C9A33
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x000CB83B File Offset: 0x000C9A3B
		public Guid MonitoringAccountMailboxGuid { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000CB844 File Offset: 0x000C9A44
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x000CB84C File Offset: 0x000C9A4C
		public Guid MonitoringAccountMailboxArchiveGuid { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x000CB855 File Offset: 0x000C9A55
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x000CB85D File Offset: 0x000C9A5D
		public string MonitoringAccountUserPrincipalName { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000CB866 File Offset: 0x000C9A66
		// (set) Token: 0x06002171 RID: 8561 RVA: 0x000CB86E File Offset: 0x000C9A6E
		public string MonitoringAccountExchangeLoginName { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x000CB877 File Offset: 0x000C9A77
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x000CB87F File Offset: 0x000C9A7F
		public string MonitoringAccountWindowsLoginName { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x000CB888 File Offset: 0x000C9A88
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x000CB890 File Offset: 0x000C9A90
		public string MonitoringMailboxLegacyExchangeDN { get; set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000CB899 File Offset: 0x000C9A99
		// (set) Token: 0x06002177 RID: 8567 RVA: 0x000CB8A1 File Offset: 0x000C9AA1
		public Guid SystemMailboxGuid { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000CB8AA File Offset: 0x000C9AAA
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x000CB8B2 File Offset: 0x000C9AB2
		public string MonitoringAccountSipAddress { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000CB8BB File Offset: 0x000C9ABB
		// (set) Token: 0x0600217B RID: 8571 RVA: 0x000CB8C3 File Offset: 0x000C9AC3
		public string HostingServer { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000CB8CC File Offset: 0x000C9ACC
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x000CB8D4 File Offset: 0x000C9AD4
		public DateTime TimeVerified { get; set; }

		// Token: 0x0600217E RID: 8574 RVA: 0x000CB8E0 File Offset: 0x000C9AE0
		public MailboxDatabaseInfo()
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled || !LocalEndpointManager.IsDataCenter)
			{
				this.AuthenticationResult = LiveIdAuthResult.Success;
			}
			else
			{
				this.AuthenticationResult = LiveIdAuthResult.AuthFailure;
			}
			this.TimeVerified = DateTime.UtcNow;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000CB938 File Offset: 0x000C9B38
		public void WriteToRegistry(RegistryKey roleKey)
		{
			WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "Writing dbInfo for database {0}", this.MailboxDatabaseName ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 415);
			string mailboxDatabaseName = this.MailboxDatabaseName;
			WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "Creating registry subkey '{0}'", mailboxDatabaseName ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 419);
			using (RegistryKey registryKey = roleKey.CreateSubKey(mailboxDatabaseName, RegistryKeyPermissionCheck.ReadWriteSubTree))
			{
				if (!string.IsNullOrEmpty(this.MonitoringAccountUserPrincipalName))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "MonitoringAccountUserPrincipalName: {0}", this.MonitoringAccountUserPrincipalName ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 426);
					registryKey.SetValue("UserPrincipalName", this.MonitoringAccountUserPrincipalName, RegistryValueKind.String);
				}
				if (!string.IsNullOrEmpty(this.MonitoringAccountDisplayName))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "MonitoringAccountDisplayName: {0}", this.MonitoringAccountDisplayName ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 433);
					registryKey.SetValue("DisplayName", this.MonitoringAccountDisplayName, RegistryValueKind.String);
				}
				if (!string.IsNullOrEmpty(this.MonitoringAccount))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "MonitoringAccount: {0}", this.MonitoringAccount ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 441);
					registryKey.SetValue("MonitoringAccount", this.MonitoringAccount, RegistryValueKind.String);
				}
				if (!string.IsNullOrEmpty(this.MonitoringAccountDomain))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "MonitoringAccountDomain: {0}", this.MonitoringAccountDomain ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 449);
					registryKey.SetValue("MonitoringAccountDomain", this.MonitoringAccountDomain, RegistryValueKind.String);
				}
				if (!string.IsNullOrEmpty(this.OriginatingServer))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "OriginatingServer: {0}", this.OriginatingServer ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 456);
					registryKey.SetValue("DomainController", this.OriginatingServer, RegistryValueKind.String);
				}
				WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "AuthenticationResult: {0}", this.AuthenticationResult.ToString(), null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 461);
				registryKey.SetValue("AuthenticationResult", this.AuthenticationResult.ToString(), RegistryValueKind.String);
				if (!string.IsNullOrEmpty(this.AuthenticationError))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "AuthenticationError: {0}", this.AuthenticationError ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 467);
					registryKey.SetValue("AuthenticationError", this.AuthenticationError, RegistryValueKind.String);
				}
				if (!string.IsNullOrEmpty(this.HostingServer))
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "HostingServer: {0}", this.HostingServer ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 474);
					registryKey.SetValue("HostingServer", this.HostingServer, RegistryValueKind.String);
				}
				if (this.password != null && LocalEndpointManager.IsDataCenter)
				{
					WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "Password: {0}", this.password ?? "null", null, "WriteToRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 482);
					registryKey.SetValue("Password", this.password, RegistryValueKind.String);
				}
				registryKey.SetValue("TimeVerified", this.TimeVerified.ToString("u"), RegistryValueKind.String);
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000CBCE0 File Offset: 0x000C9EE0
		public void UpdateMailboxAuthenticationRegistryInfo(string rootRegistryPath, string roleRegistrySubkey)
		{
			WTFDiagnostics.TraceInformation<string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "Updating Mailbox Authentication info on registry for database {0}", this.MailboxDatabaseName ?? "null", null, "UpdateMailboxAuthenticationRegistryInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 498);
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(rootRegistryPath))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(roleRegistrySubkey))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey.OpenSubKey(this.MailboxDatabaseName))
							{
								if (registryKey3 != null)
								{
									try
									{
										registryKey3.SetValue("AuthenticationResult", this.AuthenticationResult.ToString(), RegistryValueKind.String);
										if (!string.IsNullOrEmpty(this.AuthenticationError))
										{
											registryKey3.SetValue("AuthenticationError", this.AuthenticationError, RegistryValueKind.String);
										}
										registryKey3.SetValue("TimeVerified", this.TimeVerified.ToString("u"), RegistryValueKind.String);
									}
									catch (Exception arg)
									{
										WTFDiagnostics.TraceError<Exception>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.CommonComponentsTracer, TracingContext.Default, "Error while updating registry. Caught exception: {0}", arg, null, "UpdateMailboxAuthenticationRegistryInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseInfo.cs", 528);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04001852 RID: 6226
		public const string TimeVerifiedName = "TimeVerified";

		// Token: 0x04001853 RID: 6227
		public const string UserPrincipalNameRegistryValue = "UserPrincipalName";

		// Token: 0x04001854 RID: 6228
		public const string AuthenticationResultRegistryValue = "AuthenticationResult";

		// Token: 0x04001855 RID: 6229
		public const string AuthenticationErrorRegistryValue = "AuthenticationError";

		// Token: 0x04001856 RID: 6230
		private const string DisplayNameRegistryValue = "DisplayName";

		// Token: 0x04001857 RID: 6231
		private const string MonitoringAccountRegistryValue = "MonitoringAccount";

		// Token: 0x04001858 RID: 6232
		private const string MonitoringAccountDomainRegistryValue = "MonitoringAccountDomain";

		// Token: 0x04001859 RID: 6233
		private const string DomainControllerRegistryValue = "DomainController";

		// Token: 0x0400185A RID: 6234
		private const string HostingServerRegistryValue = "HostingServer";

		// Token: 0x0400185B RID: 6235
		private const string PasswordRegistryValue = "Password";

		// Token: 0x0400185C RID: 6236
		private string password;
	}
}
