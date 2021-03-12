using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200008C RID: 140
	public abstract class SetObjectWithIdentityTaskBase<TIdentity, TPublicObject, TDataObject> : SetObjectTaskBase<TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001566B File Offset: 0x0001386B
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00015682 File Offset: 0x00013882
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001569A File Offset: 0x0001389A
		protected override void InternalValidate()
		{
			if (this.Identity == null && base.ParameterSetName == "Identity")
			{
				base.WriteError(new ArgumentNullException("Identity"), (ErrorCategory)1000, null);
			}
			base.InternalValidate();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000156D8 File Offset: 0x000138D8
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<TDataObject>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, null, null);
		}
	}
}
