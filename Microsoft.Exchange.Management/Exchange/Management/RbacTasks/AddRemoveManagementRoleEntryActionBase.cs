using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200067E RID: 1662
	public abstract class AddRemoveManagementRoleEntryActionBase : ManagementRoleEntryActionBase
	{
		// Token: 0x06003ACC RID: 15052
		protected abstract void InternalAddRemoveRoleEntry(MultiValuedProperty<RoleEntry> roleEntries);

		// Token: 0x06003ACD RID: 15053
		protected abstract string GetRoleEntryString();

		// Token: 0x06003ACE RID: 15054
		protected abstract LocalizedException GetRoleSaveException(string roleEntry, string role, string exception);

		// Token: 0x06003ACF RID: 15055 RVA: 0x000FA054 File Offset: 0x000F8254
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			base.Validate(this.DataObject);
			if (base.HasErrors)
			{
				return;
			}
			if (!base.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			this.DataObject.CheckWritable();
			string key = this.DataObject.Guid.ToString();
			if (this.roleAndSessionCache.ContainsKey(key))
			{
				AddRemoveManagementRoleEntryActionBase.RoleAndSession roleAndSession = this.roleAndSessionCache[key];
				this.InternalAddRemoveRoleEntry(roleAndSession.Role.RoleEntries);
				base.Validate(roleAndSession.Role);
				if (base.HasErrors)
				{
					return;
				}
			}
			else
			{
				this.roleAndSessionCache[key] = new AddRemoveManagementRoleEntryActionBase.RoleAndSession(this.DataObject, base.DataSession, base.ParentRole);
			}
			this.roleAndSessionCache[key].PipelinedElements.Add(new AddRemoveManagementRoleEntryActionBase.PipelinedElement(base.CurrentObjectIndex, this.GetRoleEntryString()));
			TaskLogger.LogExit();
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000FA18C File Offset: 0x000F838C
		protected override void InternalEndProcessing()
		{
			foreach (AddRemoveManagementRoleEntryActionBase.RoleAndSession roleAndSession in this.roleAndSessionCache.Values)
			{
				LocalizedException ex = null;
				try
				{
					if (roleAndSession.Role.Identity != null)
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(roleAndSession.Role, roleAndSession.ConfigSession, typeof(ExchangeRole)));
					}
					roleAndSession.Role.ApplyChangesToDownlevelData(roleAndSession.ParentRole ?? roleAndSession.Role);
					roleAndSession.ConfigSession.Save(roleAndSession.Role);
				}
				catch (ADOperationException ex2)
				{
					ex = ex2;
				}
				catch (DataSourceTransientException ex3)
				{
					ex = ex3;
				}
				finally
				{
					if (roleAndSession.Role.Identity != null)
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(roleAndSession.ConfigSession));
					}
				}
				if (ex != null)
				{
					foreach (AddRemoveManagementRoleEntryActionBase.PipelinedElement pipelinedElement in roleAndSession.PipelinedElements)
					{
						this.WriteError(this.GetRoleSaveException(pipelinedElement.RoleEntryString, roleAndSession.Role.Id.ToString(), ex.Message), ErrorCategory.WriteError, pipelinedElement.ObjectIndex, false);
					}
				}
			}
			base.InternalEndProcessing();
		}

		// Token: 0x040026A5 RID: 9893
		private Dictionary<string, AddRemoveManagementRoleEntryActionBase.RoleAndSession> roleAndSessionCache = new Dictionary<string, AddRemoveManagementRoleEntryActionBase.RoleAndSession>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x0200067F RID: 1663
		private struct PipelinedElement
		{
			// Token: 0x06003AD2 RID: 15058 RVA: 0x000FA370 File Offset: 0x000F8570
			internal PipelinedElement(int objectIndex, string roleEntryString)
			{
				this.ObjectIndex = objectIndex;
				this.RoleEntryString = roleEntryString;
			}

			// Token: 0x040026A6 RID: 9894
			internal int ObjectIndex;

			// Token: 0x040026A7 RID: 9895
			internal string RoleEntryString;
		}

		// Token: 0x02000680 RID: 1664
		private struct RoleAndSession
		{
			// Token: 0x06003AD3 RID: 15059 RVA: 0x000FA380 File Offset: 0x000F8580
			internal RoleAndSession(ExchangeRole role, IConfigDataProvider configSession, ExchangeRole parentRole)
			{
				this.Role = role;
				this.ConfigSession = configSession;
				this.ParentRole = parentRole;
				this.PipelinedElements = new List<AddRemoveManagementRoleEntryActionBase.PipelinedElement>();
			}

			// Token: 0x040026A8 RID: 9896
			internal ExchangeRole Role;

			// Token: 0x040026A9 RID: 9897
			internal IConfigDataProvider ConfigSession;

			// Token: 0x040026AA RID: 9898
			internal ExchangeRole ParentRole;

			// Token: 0x040026AB RID: 9899
			internal List<AddRemoveManagementRoleEntryActionBase.PipelinedElement> PipelinedElements;
		}
	}
}
