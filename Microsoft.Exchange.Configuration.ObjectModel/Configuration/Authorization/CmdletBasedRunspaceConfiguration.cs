using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000223 RID: 547
	internal class CmdletBasedRunspaceConfiguration : ExchangeRunspaceConfiguration
	{
		// Token: 0x06001399 RID: 5017 RVA: 0x00045206 File Offset: 0x00043406
		protected CmdletBasedRunspaceConfiguration(string identityName) : base(new GenericIdentity(identityName), new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.Partial))
		{
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004521C File Offset: 0x0004341C
		public static CmdletBasedRunspaceConfiguration Create(MonadConnectionInfo connectionInfo, string identityName, IReportProgress reportProgress)
		{
			CmdletBasedRunspaceConfiguration.reportProgress = reportProgress;
			CmdletBasedRunspaceConfiguration.connectionInfo = connectionInfo;
			return new CmdletBasedRunspaceConfiguration(identityName);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00045230 File Offset: 0x00043430
		protected override SerializedAccessToken PopulateGroupMemberships(IIdentity identity)
		{
			return null;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00045233 File Offset: 0x00043433
		protected override ICollection<SecurityIdentifier> GetGroupAccountsSIDs(IIdentity logonIdentity)
		{
			if (logonIdentity is WindowsIdentity)
			{
				return base.GetGroupAccountsSIDs(logonIdentity);
			}
			return null;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x00045246 File Offset: 0x00043446
		internal SmtpAddress LogonUserLiveID
		{
			get
			{
				if (this.executingUser != null)
				{
					return (SmtpAddress)this.executingUser[ADRecipientSchema.WindowsLiveID];
				}
				return SmtpAddress.Empty;
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004526C File Offset: 0x0004346C
		protected override ADRawEntry LoadExecutingUser(IIdentity identity, IList<PropertyDefinition> properties)
		{
			MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), null, CmdletBasedRunspaceConfiguration.connectionInfo);
			CmdletBasedRunspaceConfiguration.reportProgress.ReportProgress(70, 100, Strings.LoadingLogonUser(base.IdentityName), Strings.LoadingLogonUserErrorText(base.IdentityName));
			ADRawEntry result;
			using (new OpenConnection(connection))
			{
				MonadCommand monadCommand = new MonadCommand("Get-LogonUser", connection);
				result = (ADRawEntry)monadCommand.Execute()[0];
			}
			return result;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000452FC File Offset: 0x000434FC
		protected override Result<ExchangeRoleAssignment>[] LoadRoleAssignments(IConfigurationSession session, ADRawEntry user, List<ADObjectId> existingRoleGroups)
		{
			MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), null, CmdletBasedRunspaceConfiguration.connectionInfo);
			object[] array;
			using (new OpenConnection(connection))
			{
				CmdletBasedRunspaceConfiguration.reportProgress.ReportProgress(75, 100, Strings.LoadingRoleAssignment(base.IdentityName), Strings.LoadingRoleAssignmentErrorText(base.IdentityName));
				MonadCommand monadCommand = new MonadCommand("Get-ManagementRoleAssignmentForLogonUser", connection);
				array = monadCommand.Execute();
			}
			Result<ExchangeRoleAssignment>[] array2 = new Result<ExchangeRoleAssignment>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new Result<ExchangeRoleAssignment>((ExchangeRoleAssignment)((ExchangeRoleAssignmentPresentation)array[i]).DataObject, null);
			}
			return array2;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000453C8 File Offset: 0x000435C8
		protected override Result<ExchangeRole>[] LoadRoles(IConfigurationSession session, List<ADObjectId> roleIds)
		{
			MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), null, CmdletBasedRunspaceConfiguration.connectionInfo);
			object[] roles;
			using (new OpenConnection(connection))
			{
				CmdletBasedRunspaceConfiguration.reportProgress.ReportProgress(90, 100, Strings.LoadingRole(base.IdentityName), Strings.LoadingRoleErrorText(base.IdentityName));
				MonadCommand monadCommand = new MonadCommand("Get-ManagementRoleForLogonUser", connection);
				roles = monadCommand.Execute();
			}
			Result<ExchangeRole>[] array = new Result<ExchangeRole>[roleIds.Count];
			for (int i = 0; i < roleIds.Count; i++)
			{
				array[i] = new Result<ExchangeRole>(this.FindRole(roles, roleIds[i]), null);
			}
			return array;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00045498 File Offset: 0x00043698
		protected override Result<ManagementScope>[] LoadScopes(IConfigurationSession session, ADObjectId[] scopeIds)
		{
			MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), null, CmdletBasedRunspaceConfiguration.connectionInfo);
			object[] scopes;
			using (new OpenConnection(connection))
			{
				this.scopeReported = true;
				CmdletBasedRunspaceConfiguration.reportProgress.ReportProgress(80, 100, Strings.LoadingScope(base.IdentityName), Strings.LoadingScopeErrorText(base.IdentityName));
				MonadCommand monadCommand = new MonadCommand("Get-ManagementScopeForLogonUser", connection);
				scopes = monadCommand.Execute(scopeIds);
			}
			Result<ManagementScope>[] array = new Result<ManagementScope>[scopeIds.Length];
			for (int i = 0; i < scopeIds.Length; i++)
			{
				array[i] = new Result<ManagementScope>(this.FindScope(scopes, scopeIds[i]), null);
			}
			return array;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00045564 File Offset: 0x00043764
		protected override ManagementScope[] LoadExclusiveScopes()
		{
			MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), null, CmdletBasedRunspaceConfiguration.connectionInfo);
			object[] array;
			using (new OpenConnection(connection))
			{
				if (!this.scopeReported)
				{
					CmdletBasedRunspaceConfiguration.reportProgress.ReportProgress(80, 100, Strings.LoadingScope(base.IdentityName), Strings.LoadingScopeErrorText(base.IdentityName));
				}
				MonadCommand monadCommand = new MonadCommand("Get-ExclusiveManagementScopeForLogonUser", connection);
				array = monadCommand.Execute();
			}
			ManagementScope[] array2 = new ManagementScope[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = (array[i] as ManagementScope);
			}
			return array2;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004563C File Offset: 0x0004383C
		private ExchangeRole FindRole(object[] roles, ADObjectId roleId)
		{
			return (from ExchangeRole c in roles
			where ADObjectId.Equals(roleId, c.Id)
			select c).First<ExchangeRole>();
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00045690 File Offset: 0x00043890
		private ManagementScope FindScope(object[] scopes, ADObjectId scopeId)
		{
			return (from ManagementScope c in scopes
			where ADObjectId.Equals(scopeId, c.Id)
			select c).FirstOrDefault<ManagementScope>();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000456C8 File Offset: 0x000438C8
		private void CheckCmdlet(string cmdletName, string[] parameters)
		{
			ScopeSet scopeSet = new ScopeSet(new ADScope(null, null), new ADScopeCollection[0], null, null);
			if (!this.IsCmdletAllowedInScope("Microsoft.Exchange.Management.PowerShell.E2010\\" + cmdletName, parameters, scopeSet))
			{
				throw new CommandNotFoundException(Strings.CommandNotFoundError(cmdletName));
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00045710 File Offset: 0x00043910
		protected override bool ApplyValidationRules
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000523 RID: 1315
		private const string PSSnapInName = "Microsoft.Exchange.Management.PowerShell.E2010";

		// Token: 0x04000524 RID: 1316
		private static MonadConnectionInfo connectionInfo;

		// Token: 0x04000525 RID: 1317
		private static IReportProgress reportProgress;

		// Token: 0x04000526 RID: 1318
		private bool scopeReported;
	}
}
