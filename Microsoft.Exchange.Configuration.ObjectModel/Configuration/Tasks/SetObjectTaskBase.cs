using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200008B RID: 139
	public abstract class SetObjectTaskBase<TPublicObject, TDataObject> : SetTaskBase<TDataObject>, IDynamicParameters where TPublicObject : IConfigurable, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00015449 File Offset: 0x00013649
		protected SetObjectTaskBase()
		{
			this.dynamicParametersInstance = this.CreateBlankPublicObjectInstance();
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001545D File Offset: 0x0001365D
		protected TPublicObject DynamicParametersInstance
		{
			get
			{
				return this.dynamicParametersInstance;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00015465 File Offset: 0x00013665
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001547C File Offset: 0x0001367C
		protected virtual TPublicObject Instance
		{
			get
			{
				return (TPublicObject)((object)base.Fields["Instance"]);
			}
			set
			{
				base.Fields["Instance"] = value;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00015494 File Offset: 0x00013694
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.IsObjectStateChanged())
			{
				bool flag = false;
				if (!base.TryGetVariableValue<bool>("ExchangeDisableNotChangedWarning", out flag) || !flag)
				{
					TDataObject dataObject = this.DataObject;
					if (dataObject.Identity != null)
					{
						TDataObject dataObject2 = this.DataObject;
						this.WriteWarning(Strings.WarningForceMessageWithId(dataObject2.Identity.ToString()));
					}
					else
					{
						this.WriteWarning(Strings.WarningForceMessage);
					}
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00015514 File Offset: 0x00013714
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (base.ParameterSetName != "Instance")
			{
				this.Instance = this.CreateBlankPublicObjectInstance();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00015544 File Offset: 0x00013744
		public virtual object GetDynamicParameters()
		{
			return this.dynamicParametersInstance;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00015554 File Offset: 0x00013754
		protected virtual TPublicObject CreateBlankPublicObjectInstance()
		{
			TPublicObject tpublicObject = (default(TPublicObject) == null) ? Activator.CreateInstance<TPublicObject>() : default(TPublicObject);
			ConfigurableObject configurableObject = tpublicObject as ConfigurableObject;
			if (configurableObject != null)
			{
				configurableObject.EnableSaveCalculatedValues();
				configurableObject.InitializeSchema();
			}
			return tpublicObject;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000155A0 File Offset: 0x000137A0
		protected virtual bool IsObjectStateChanged()
		{
			TDataObject dataObject = this.DataObject;
			return dataObject.ObjectState != ObjectState.Unchanged;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000155C7 File Offset: 0x000137C7
		protected virtual void StampChangesOn(IConfigurable dataObject)
		{
			dataObject.CopyChangesFrom(this.Instance);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000155DC File Offset: 0x000137DC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable configurable = this.ResolveDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			ADObject adobject = configurable as ADObject;
			if (adobject != null)
			{
				base.CurrentOrganizationId = adobject.OrganizationId;
			}
			if (base.CurrentObjectIndex == 0)
			{
				this.ResolveLocalSecondaryIdentities();
				if (base.HasErrors)
				{
					return null;
				}
			}
			TPublicObject instance = this.Instance;
			instance.CopyChangesFrom(this.dynamicParametersInstance);
			this.StampChangesOn(configurable);
			if (base.HasErrors)
			{
				return null;
			}
			TaskLogger.LogExit();
			return configurable;
		}

		// Token: 0x0600059D RID: 1437
		protected abstract IConfigurable ResolveDataObject();

		// Token: 0x0400012F RID: 303
		private readonly TPublicObject dynamicParametersInstance;
	}
}
