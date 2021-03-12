using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200008E RID: 142
	public abstract class SetADTaskBase<TIdentity, TPublicObject, TDataObject> : SetTenantADTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : ADObject, new()
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000157BB File Offset: 0x000139BB
		protected bool IsUpgrading
		{
			get
			{
				return this.isUpgrading;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000157C3 File Offset: 0x000139C3
		protected virtual bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000157C6 File Offset: 0x000139C6
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ADObjectTaskModuleFactory();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000157CD File Offset: 0x000139CD
		protected virtual bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return false;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000157D0 File Offset: 0x000139D0
		protected override void InternalProcessRecord()
		{
			if (base.IsVerboseOn)
			{
				if (this.IsObjectStateChanged())
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetConfigurableObjectChangedProperties(this.DataObject));
				}
				else
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetConfigurableObjectNonChangedProperties(this.DataObject));
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00015824 File Offset: 0x00013A24
		protected virtual void UpgradeExchangeVersion(ADObject adObject)
		{
			adObject.SetExchangeVersion(adObject.MaximumSupportedExchangeObjectVersion);
			ADLegacyVersionableObject adlegacyVersionableObject = adObject as ADLegacyVersionableObject;
			if (adlegacyVersionableObject != null)
			{
				adlegacyVersionableObject.MinAdminVersion = new int?(adObject.ExchangeVersion.ExchangeBuild.ToExchange2003FormatInt32());
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00015865 File Offset: 0x00013A65
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.isUpgrading = false;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00015880 File Offset: 0x00013A80
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			if (this.ExchangeVersionUpgradeSupported)
			{
				ADObject adobject = (ADObject)dataObject;
				if (adobject.ExchangeVersion.IsOlderThan(adobject.MaximumSupportedExchangeObjectVersion) && this.ShouldUpgradeExchangeVersion((ADObject)((object)this.Instance)))
				{
					this.UpgradeExchangeVersion(adobject);
					this.isUpgrading = true;
				}
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x04000131 RID: 305
		private bool isUpgrading;
	}
}
