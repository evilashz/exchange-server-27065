using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000083 RID: 131
	public abstract class RemoveTaskBase<TIdentity, TDataObject> : DataAccessTask<TDataObject> where TIdentity : IIdentityParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001490D File Offset: 0x00012B0D
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00014915 File Offset: 0x00012B15
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001491E File Offset: 0x00012B1E
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00014935 File Offset: 0x00012B35
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public virtual TIdentity Identity
		{
			get
			{
				return (TIdentity)((object)base.Fields["Identity"]);
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00014950 File Offset: 0x00012B50
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Identity == null)
			{
				base.WriteError(new ArgumentNullException("Identity"), (ErrorCategory)1000, null);
			}
			else
			{
				this.dataObject = (TDataObject)((object)this.ResolveDataObject());
			}
			IVersionable versionable = this.dataObject as IVersionable;
			if (versionable != null && versionable.MaximumSupportedExchangeObjectVersion.IsOlderThan(versionable.ExchangeVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorRemoveNewerObject(this.dataObject.Identity.ToString(), versionable.ExchangeVersion.ExchangeBuild.ToString())), (ErrorCategory)1004, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00014A0C File Offset: 0x00012C0C
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

		// Token: 0x0600055D RID: 1373 RVA: 0x00014A80 File Offset: 0x00012C80
		protected virtual IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<TDataObject>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, null, null);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00014AC2 File Offset: 0x00012CC2
		protected virtual bool ShouldSoftDeleteObject()
		{
			return false;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00014AC5 File Offset: 0x00012CC5
		protected virtual void SaveSoftDeletedObject()
		{
			throw new NotImplementedException("Caller does not implement SaveSoftDeletedObject method.");
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00014AD4 File Offset: 0x00012CD4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity,
				this.DataObject
			});
			base.WriteVerbose(TaskVerboseStringHelper.GetDeleteObjectVerboseString(this.dataObject.Identity, base.DataSession, typeof(TDataObject)));
			try
			{
				if (this.ShouldSoftDeleteObject())
				{
					this.SaveSoftDeletedObject();
				}
				else
				{
					base.DataSession.Delete(this.dataObject);
				}
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00014BA4 File Offset: 0x00012DA4
		internal sealed override void PreInternalProcessRecord()
		{
			if (base.IsProvisioningLayerAvailable && this.dataObject != null)
			{
				ProvisioningLayer.PreInternalProcessRecord(this, this.ConvertDataObjectToPresentationObject(this.dataObject), false);
			}
		}

		// Token: 0x0400012A RID: 298
		private TDataObject dataObject;
	}
}
