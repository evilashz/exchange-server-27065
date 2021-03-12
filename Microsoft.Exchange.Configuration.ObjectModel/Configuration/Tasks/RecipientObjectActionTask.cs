using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200003F RID: 63
	public abstract class RecipientObjectActionTask<TIdentity, TDataObject> : ObjectActionTenantADTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ADObject, new()
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BC64 File Offset: 0x00009E64
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000BC8A File Offset: 0x00009E8A
		protected SwitchParameter InternalIgnoreDefaultScope
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

		// Token: 0x060002FB RID: 763 RVA: 0x0000BCA2 File Offset: 0x00009EA2
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ADObjectTaskModuleFactory();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 65, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADObjectActionTask.cs");
			if (this.InternalIgnoreDefaultScope)
			{
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			}
			tenantOrRootOrgRecipientSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(base.SessionSettings.GetAccountOrResourceForestFqdn());
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BD17 File Offset: 0x00009F17
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BD1C File Offset: 0x00009F1C
		protected override void InternalValidate()
		{
			ADObjectId adobjectId;
			if (this.InternalIgnoreDefaultScope && !RecipientTaskHelper.IsValidDistinguishedName(this.Identity, out adobjectId))
			{
				base.WriteError(new ArgumentException(Strings.ErrorOnlyDNSupportedWithIgnoreDefaultScope), (ErrorCategory)1000, this.Identity);
			}
			base.InternalValidate();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BD78 File Offset: 0x00009F78
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)RecipientTaskHelper.ResolveDataObject<TDataObject>(base.DataSession, base.TenantGlobalCatalogSession, base.ServerSettings, this.Identity, this.RootId, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<TDataObject>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, adobject))
			{
				base.UnderscopeDataSession(adobject.OrganizationId);
			}
			return adobject;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BE08 File Offset: 0x0000A008
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.InternalIgnoreDefaultScope && base.DomainController != null)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorIgnoreDefaultScopeAndDCSetTogether), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}
	}
}
