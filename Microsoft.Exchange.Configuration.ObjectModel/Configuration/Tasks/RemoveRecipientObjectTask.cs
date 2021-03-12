using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000086 RID: 134
	public abstract class RemoveRecipientObjectTask<TIdentity, TDataObject> : RemoveADTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014C7B File Offset: 0x00012E7B
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014CA1 File Offset: 0x00012EA1
		protected SwitchParameter ForReconciliation
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForReconciliation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForReconciliation"] = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00014CB9 File Offset: 0x00012EB9
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00014CDF File Offset: 0x00012EDF
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreDefaultScope"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreDefaultScope"] = value;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014CF8 File Offset: 0x00012EF8
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 162, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\RemoveAdObjectTask.cs");
			if (this.IgnoreDefaultScope)
			{
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			}
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014D47 File Offset: 0x00012F47
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00014D4C File Offset: 0x00012F4C
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)RecipientTaskHelper.ResolveDataObject<TDataObject>(base.DataSession, base.TenantGlobalCatalogSession, base.ServerSettings, this.Identity, this.RootId, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<TDataObject>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, adobject))
			{
				base.UnderscopeDataSession(adobject.OrganizationId);
				base.CurrentOrganizationId = adobject.OrganizationId;
			}
			ADRecipient adrecipient = adobject as ADRecipient;
			if (adrecipient != null)
			{
				adrecipient.BypassModerationCheck = true;
			}
			return adobject;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014DFC File Offset: 0x00012FFC
		protected override void InternalValidate()
		{
			ADObjectId adobjectId;
			if (this.IgnoreDefaultScope && !RecipientTaskHelper.IsValidDistinguishedName(this.Identity, out adobjectId))
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorOnlyDNSupportedWithIgnoreDefaultScope), ExchangeErrorCategory.Client, this.Identity);
			}
			base.InternalValidate();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014E50 File Offset: 0x00013050
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.IgnoreDefaultScope && base.DomainController != null)
			{
				base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorIgnoreDefaultScopeAndDCSetTogether), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00014E90 File Offset: 0x00013090
		protected override void InternalProcessRecord()
		{
			ADRecipient adrecipient = base.DataObject as ADRecipient;
			bool flag = this.ShouldSoftDeleteObject();
			if (!flag)
			{
				this.LoadRoleAssignments();
			}
			if (this.ForReconciliation && adrecipient != null && !string.IsNullOrEmpty(adrecipient.ExternalDirectoryObjectId))
			{
				adrecipient.ExternalDirectoryObjectId = string.Empty;
				try
				{
					if (!flag)
					{
						(base.DataSession as IRecipientSession).Save(base.DataObject as ADRecipient, true);
					}
					else
					{
						base.DataSession.Save(base.DataObject);
					}
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.ServerTransient, null);
				}
			}
			base.InternalProcessRecord();
			if (!flag)
			{
				this.RemoveRolesAssigments();
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014F54 File Offset: 0x00013154
		protected override void SaveSoftDeletedObject()
		{
			TDataObject dataObject = base.DataObject;
			RecipientTaskHelper.CreateSoftDeletedObjectsContainerIfNecessary(dataObject.Id.Parent, base.DomainController);
			base.DataSession.Save(base.DataObject);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014FA0 File Offset: 0x000131A0
		private void LoadRoleAssignments()
		{
			if (!typeof(TDataObject).IsAssignableFrom(typeof(ADUser)))
			{
				this.roleAssignments = null;
				return;
			}
			TDataObject dataObject = base.DataObject;
			ADObjectId adobjectId;
			if (!dataObject.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				TDataObject dataObject2 = base.DataObject;
				adobjectId = dataObject2.OrganizationId.ConfigurationUnit;
			}
			else
			{
				adobjectId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			}
			ADObjectId adobjectId2 = adobjectId;
			ADObjectId rootOrgId = adobjectId2;
			TDataObject dataObject3 = base.DataObject;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgId, dataObject3.OrganizationId, base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.TenantGlobalCatalogSession.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 340, "LoadRoleAssignments", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\RemoveAdObjectTask.cs");
			IConfigurationSession configurationSession = tenantOrTopologyConfigurationSession;
			ADObjectId[] array = new ADObjectId[1];
			ADObjectId[] array2 = array;
			int num = 0;
			TDataObject dataObject4 = base.DataObject;
			array2[num] = dataObject4.Id;
			this.roleAssignments = configurationSession.FindRoleAssignmentsByUserIds(array, false);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00015090 File Offset: 0x00013290
		private void RemoveRolesAssigments()
		{
			if (this.roleAssignments == null)
			{
				return;
			}
			foreach (Result<ExchangeRoleAssignment> result in this.roleAssignments)
			{
				string id = result.Data.Id.ToString();
				try
				{
					base.WriteVerbose(Strings.VerboseRemovingRoleAssignment(id));
					result.Data.Session.Delete(result.Data);
					base.WriteVerbose(Strings.VerboseRemovedRoleAssignment(id));
				}
				catch (Exception ex)
				{
					if (!this.IsKnownException(ex))
					{
						throw;
					}
					this.WriteWarning(Strings.WarningCouldNotRemoveRoleAssignment(id, ex.Message));
				}
			}
			this.roleAssignments = null;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00015148 File Offset: 0x00013348
		[Obsolete("Use GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) instead")]
		protected new IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, OptionalIdentityData optionalData, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new()
		{
			return base.GetDataObject<TObject>(id, session, rootID, optionalData, notFoundError, multipleFoundError);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00015159 File Offset: 0x00013359
		[Obsolete("Use GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) instead")]
		protected new IConfigurable GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new()
		{
			return base.GetDataObject<TObject>(id, session, rootID, null, notFoundError, multipleFoundError);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00015169 File Offset: 0x00013369
		[Obsolete("Use ThrowTerminatingError(Exception exception, ExchangeErrorCategory category, object target) instead.")]
		protected new void ThrowTerminatingError(Exception exception, ErrorCategory category, object target)
		{
			base.ThrowTerminatingError(exception, category, target);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00015174 File Offset: 0x00013374
		[Obsolete("Use WriteError(Exception exception, ExchangeErrorCategory category, object target, bool reThrow) instead.")]
		protected new void WriteError(Exception exception, ErrorCategory category, object target, bool reThrow)
		{
			base.WriteError(exception, category, target, reThrow);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00015181 File Offset: 0x00013381
		[Obsolete("Use WriteError(Exception exception, ExchangeErrorCategory category, object target) instead.")]
		internal new void WriteError(Exception exception, ErrorCategory category, object target)
		{
			base.WriteError(exception, category, target, true);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001518D File Offset: 0x0001338D
		[Obsolete("Use WriteError(Exception exception, ExchangeErrorCategory category, object target, bool reThrow, string helpUrl) instead.")]
		protected new void WriteError(Exception exception, ErrorCategory category, object target, bool reThrow, string helpUrl)
		{
			base.WriteError(exception, category, target, reThrow, helpUrl);
		}

		// Token: 0x0400012C RID: 300
		private Result<ExchangeRoleAssignment>[] roleAssignments;
	}
}
