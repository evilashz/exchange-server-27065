using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000676 RID: 1654
	[Cmdlet("Remove", "ManagementRole", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveManagementRole : RemoveSystemConfigurationObjectTask<RoleIdParameter, ExchangeRole>
	{
		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000F7780 File Offset: 0x000F5980
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x000F77A6 File Offset: 0x000F59A6
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

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000F77BE File Offset: 0x000F59BE
		// (set) Token: 0x06003A95 RID: 14997 RVA: 0x000F77E4 File Offset: 0x000F59E4
		[Parameter]
		public SwitchParameter Recurse
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recurse"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Recurse"] = value;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000F77FC File Offset: 0x000F59FC
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000F7822 File Offset: 0x000F5A22
		[Parameter(Mandatory = false)]
		public SwitchParameter UnScopedTopLevel
		{
			get
			{
				return (SwitchParameter)(base.Fields["UnScopedTopLevel"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UnScopedTopLevel"] = value;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000F783A File Offset: 0x000F5A3A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Recurse)
				{
					return Strings.ConfirmationMessageRemoveManagementRoleRecursive(this.Identity.ToString(), this.rolesToRemove);
				}
				return Strings.ConfirmationMessageRemoveManagementRole(this.Identity.ToString());
			}
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000F7870 File Offset: 0x000F5A70
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if ((!base.DataObject.IsUnscopedTopLevel || !this.UnScopedTopLevel) && base.DataObject.IsRootRole)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotDeletePrecannedRole(this.Identity.ToString())), ErrorCategory.InvalidOperation, base.DataObject.Id);
				}
				if (!base.DataObject.IsUnscopedTopLevel && this.UnScopedTopLevel)
				{
					base.WriteError(new InvalidOperationException(Strings.ParameterAllowedOnlyForTopLevelRoleManipulation("UnScopedTopLevel", RoleType.UnScoped.ToString())), ErrorCategory.InvalidOperation, null);
				}
			}
			ADPagedReader<ADRawEntry> adpagedReader = this.ConfigurationSession.FindPagedADRawEntryWithDefaultFilters<ExchangeRole>(base.DataObject.Id, this.Recurse ? QueryScope.SubTree : QueryScope.Base, null, null, 0, new PropertyDefinition[]
			{
				ADObjectSchema.Id
			});
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ADRawEntry adrawEntry in adpagedReader)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append(adrawEntry.Id.ToString());
			}
			this.rolesToRemove = stringBuilder.ToString();
			TaskLogger.LogExit();
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F79D4 File Offset: 0x000F5BD4
		private void MoveToNextPercent()
		{
			this.currentPercent = 5 + this.currentPercent % 95;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000F79E8 File Offset: 0x000F5BE8
		private void ReportDeleteTreeProgress(ADTreeDeleteNotFinishedException de)
		{
			this.MoveToNextPercent();
			if (de != null)
			{
				base.WriteVerbose(de.LocalizedString);
			}
			else
			{
				base.WriteVerbose(Strings.ProgressStatusRemovingManagementRoleTree);
			}
			base.WriteProgress(Strings.ProgressActivityRemovingManagementRoleTree(base.DataObject.Id.ToString()), Strings.ProgressStatusRemovingManagementRoleTree, this.currentPercent);
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000F7A3D File Offset: 0x000F5C3D
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000F7A5C File Offset: 0x000F5C5C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			ADPagedReader<ADRawEntry> adpagedReader = this.ConfigurationSession.FindPagedADRawEntryWithDefaultFilters<ExchangeRole>(base.DataObject.Id, this.Recurse ? QueryScope.SubTree : QueryScope.Base, new ExistsFilter(ExchangeRoleSchema.RoleAssignments), null, 0, new PropertyDefinition[]
			{
				ADObjectSchema.Id,
				ExchangeRoleSchema.RoleAssignments
			});
			foreach (ADRawEntry adrawEntry in adpagedReader)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotDeleteRoleWithAssignment(adrawEntry.Id.ToString())), ErrorCategory.InvalidOperation, base.DataObject.Id);
			}
			if (this.Recurse)
			{
				((IConfigurationSession)base.DataSession).DeleteTree(base.DataObject, new TreeDeleteNotFinishedHandler(this.ReportDeleteTreeProgress));
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002663 RID: 9827
		private const string ParameterRecurse = "Recurse";

		// Token: 0x04002664 RID: 9828
		private int currentPercent;

		// Token: 0x04002665 RID: 9829
		private string rolesToRemove;
	}
}
