using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200003D RID: 61
	public abstract class ObjectActionTask<TIdentity, TDataObject> : SetTaskBase<TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BA4E File Offset: 0x00009C4E
		protected virtual bool DelayProvisioning
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000BA59 File Offset: 0x00009C59
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000BA70 File Offset: 0x00009C70
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

		// Token: 0x060002ED RID: 749 RVA: 0x0000BA88 File Offset: 0x00009C88
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TDataObject tdataObject = (TDataObject)((object)this.ResolveDataObject());
			ADObject adobject = tdataObject as ADObject;
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
			if (!this.DelayProvisioning && base.IsProvisioningLayerAvailable)
			{
				this.ProvisionDefaultValues((default(TDataObject) == null) ? Activator.CreateInstance<TDataObject>() : default(TDataObject), tdataObject);
			}
			TaskLogger.LogExit();
			return tdataObject;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000BB25 File Offset: 0x00009D25
		protected virtual void ProvisionDefaultValues(IConfigurable temporaryObject, IConfigurable dataObject)
		{
			ProvisioningLayer.ProvisionDefaultProperties(this, this.ConvertDataObjectToPresentationObject(temporaryObject), this.ConvertDataObjectToPresentationObject(dataObject), false);
			this.ValidateProvisionedProperties(dataObject);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BB43 File Offset: 0x00009D43
		protected virtual void ValidateProvisionedProperties(IConfigurable dataObject)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000BB48 File Offset: 0x00009D48
		protected virtual IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<TDataObject>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, null, null);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BB8A File Offset: 0x00009D8A
		protected override void InternalValidate()
		{
			if (this.Identity == null && base.ParameterSetName == "Identity")
			{
				base.WriteError(new ArgumentNullException("Identity"), (ErrorCategory)1000, null);
			}
			base.InternalValidate();
		}
	}
}
