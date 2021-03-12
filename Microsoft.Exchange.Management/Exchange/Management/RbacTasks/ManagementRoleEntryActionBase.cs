using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200067D RID: 1661
	public abstract class ManagementRoleEntryActionBase : SystemConfigurationObjectActionTask<RoleEntryIdParameter, ExchangeRole>
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000F9BC8 File Offset: 0x000F7DC8
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000F9BEE File Offset: 0x000F7DEE
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000F9C06 File Offset: 0x000F7E06
		protected ExchangeRole ParentRole
		{
			get
			{
				return this.parentRole;
			}
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000F9C0E File Offset: 0x000F7E0E
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.parentRole = null;
		}

		// Token: 0x06003AC3 RID: 15043
		protected abstract void InternalApplyChangeAndValidate();

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000F9C1D File Offset: 0x000F7E1D
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000F9C3C File Offset: 0x000F7E3C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.WriteError));
			this.DataObject.CheckWritable();
			bool flag = false;
			if (this.DataObject.IsDeprecated)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotModifyDeprecatedRole(this.DataObject.ToString())), ErrorCategory.InvalidOperation, null);
			}
			if (this.DataObject.IsUnscopedTopLevel && this.IsTopLevelUnscopedRoleModificationAllowed())
			{
				flag = true;
			}
			else
			{
				this.parentRole = this.ConfigurationSession.Read<ExchangeRole>(this.DataObject.Id.Parent);
				if (this.parentRole != null && this.parentRole.IsDeprecated)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotModifyDeprecatedRole(this.parentRole.ToString())), ErrorCategory.InvalidOperation, null);
				}
				else if (this.parentRole == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotModifyPrecannedRole(this.DataObject.Id.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
				}
			}
			if (!base.CurrentTaskContext.CanBypassRBACScope && !RoleHelper.HasDelegatingHierarchicalRoleAssignmentWithoutScopeRestriction(base.ExecutingUserOrganizationId, base.ExchangeRunspaceConfig.RoleAssignments, flag ? this.DataObject.Id : this.ParentRole.Id))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorModifyRoleNeedHierarchicalParentRoleWithoutScopeRestriction(this.DataObject.Id.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
			}
			this.InternalApplyChangeAndValidate();
			IEnumerable<ExchangeRole> childRoles = this.ConfigurationSession.FindPaged<ExchangeRole>(this.DataObject.Id, QueryScope.OneLevel, null, null, 0);
			RoleHelper.ValidateChangeAgainstParentAndChildren(this.DataObject, flag ? this.DataObject : this.ParentRole, childRoles, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x06003AC6 RID: 15046
		protected abstract bool IsTopLevelUnscopedRoleModificationAllowed();

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000F9E20 File Offset: 0x000F8020
		protected void VerifyScriptEntry(string scriptName, string[] parameters, bool skipScriptExistenceCheck = false)
		{
			int num = scriptName.IndexOfAny(Path.GetInvalidFileNameChars());
			if (num >= 0)
			{
				base.WriteError(new ArgumentException(Strings.InvalidCharacterInScriptFileName(scriptName, scriptName[num].ToString())), ErrorCategory.InvalidData, this.DataObject.Id);
			}
			if (skipScriptExistenceCheck)
			{
				return;
			}
			string path = Path.Combine(ConfigurationContext.Setup.RemoteScriptPath, scriptName);
			if (!File.Exists(path))
			{
				path = Path.Combine(ConfigurationContext.Setup.TorusRemoteScriptPath, scriptName);
			}
			if (!File.Exists(path))
			{
				base.WriteError(new ArgumentException(Strings.ScriptDontExist(scriptName)), ErrorCategory.InvalidData, this.DataObject.Id);
			}
			try
			{
				CommandMetadata metadata = new CommandMetadata(path);
				this.VerifyParametersWithMetadata(metadata, parameters);
			}
			catch (CommandNotFoundException ex)
			{
				base.WriteError(new ArgumentException(Strings.CannotLoadScript(scriptName, ex.ErrorRecord.ErrorDetails.ToString())), ErrorCategory.InvalidData, this.DataObject.Id);
			}
			catch (ParseException ex2)
			{
				base.WriteError(new ArgumentException(Strings.ErrorParsingScript(scriptName, ex2.Message)), ErrorCategory.InvalidData, this.DataObject.Id);
			}
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000F9F4C File Offset: 0x000F814C
		private void VerifyParametersWithMetadata(CommandMetadata metadata, string[] parameters)
		{
			if (parameters != null)
			{
				foreach (string text in parameters)
				{
					if (!metadata.Parameters.ContainsKey(text))
					{
						base.WriteError(new ArgumentException(Strings.ParameterDoesNotExist(text)), ErrorCategory.InvalidData, this.DataObject.Id);
					}
				}
			}
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000F9FA0 File Offset: 0x000F81A0
		protected void VerifyCmdletEntry(string cmdletName, string snapinName, string[] parameters)
		{
			if (snapinName.StartsWith("Microsoft.Exchange.Management.PowerShell.E2010", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteError(new ArgumentException(Strings.ExchangeCmdletsNotAllowedInRole(RoleType.UnScoped.ToString())), ErrorCategory.InvalidArgument, this.DataObject.Id);
			}
			InitialSessionStateBuilder.InitializeWellKnownSnapinsIfNeeded();
			SessionStateCommandEntryWithMetadata cmdletEntry = InitialSessionStateBuilder.GetCmdletEntry(new CmdletRoleEntry(cmdletName, snapinName, parameters));
			if (cmdletEntry == null || cmdletEntry.SessionStateCommandEntry.CommandType != CommandTypes.Cmdlet)
			{
				base.WriteError(new ArgumentException(Strings.CannotFindCmdletInSnapin(cmdletName, snapinName)), ErrorCategory.InvalidData, this.DataObject.Id);
			}
			this.VerifyParametersWithMetadata(cmdletEntry.CommandMetadata, parameters);
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000FA03F File Offset: 0x000F823F
		protected bool IsEntryValidationRequired()
		{
			return this.DataObject.IsUnscoped;
		}

		// Token: 0x040026A3 RID: 9891
		protected const string ParameterSkipScriptExistenceCheck = "SkipScriptExistenceCheck";

		// Token: 0x040026A4 RID: 9892
		private ExchangeRole parentRole;
	}
}
