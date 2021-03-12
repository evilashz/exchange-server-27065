using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D9 RID: 2265
	public abstract class RemoveSingletonSystemConfigurationObjectTask<TDataObject> : DataAccessTask<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06005052 RID: 20562 RVA: 0x0015012F File Offset: 0x0014E32F
		// (set) Token: 0x06005053 RID: 20563 RVA: 0x00150137 File Offset: 0x0014E337
		protected TDataObject DataObject
		{
			get
			{
				return this.dataObject;
			}
			set
			{
				this.dataObject = value;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x00150140 File Offset: 0x0014E340
		// (set) Token: 0x06005055 RID: 20565 RVA: 0x00150148 File Offset: 0x0014E348
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x00150151 File Offset: 0x0014E351
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00150159 File Offset: 0x0014E359
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, this.SessionSettings, 84, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeRpcClientAccess\\RemoveSingletonSystemConfigurationObjectTask.cs");
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x00150184 File Offset: 0x0014E384
		protected override void InternalStateReset()
		{
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			base.InternalStateReset();
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x001501B0 File Offset: 0x0014E3B0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			IConfigurable[] array = base.DataSession.Find<TDataObject>(null, this.RootId, true, null);
			if (array != null && array.Length == 1)
			{
				this.dataObject = (TDataObject)((object)array[0]);
			}
			IVersionable versionable = this.dataObject as IVersionable;
			if (versionable != null && versionable.MaximumSupportedExchangeObjectVersion.IsOlderThan(versionable.ExchangeVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorRemoveNewerObject(this.dataObject.Identity.ToString(), versionable.ExchangeVersion.ExchangeBuild.ToString())), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00150260 File Offset: 0x0014E460
		protected override void InternalProvisioningValidation()
		{
			ProvisioningValidationError[] array = ProvisioningLayer.Validate(this, this.ConvertDataObjectToPresentationObject(this.DataObject));
			if (array != null && array.Length > 0)
			{
				foreach (ProvisioningValidationError provisioningValidationError in array)
				{
					ProvisioningValidationException exception = new ProvisioningValidationException(provisioningValidationError.Description, provisioningValidationError.AgentName, provisioningValidationError.Exception);
					this.WriteError(exception, (ErrorCategory)provisioningValidationError.ErrorCategory, null, false);
				}
			}
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x001502D4 File Offset: 0x0014E4D4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RootId,
				this.DataObject
			});
			try
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetDeleteObjectVerboseString(this.dataObject.Identity, base.DataSession, typeof(TDataObject)));
				base.DataSession.Delete(this.dataObject);
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.WriteError, null);
			}
			catch (NullReferenceException innerException)
			{
				LocalizedException exception2 = new LocalizedException(Strings.ExceptionRemoveNoneExistenceObject, innerException);
				base.WriteError(exception2, ErrorCategory.WriteError, null);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002F51 RID: 12113
		private TDataObject dataObject;

		// Token: 0x04002F52 RID: 12114
		private ADSessionSettings sessionSettings;
	}
}
