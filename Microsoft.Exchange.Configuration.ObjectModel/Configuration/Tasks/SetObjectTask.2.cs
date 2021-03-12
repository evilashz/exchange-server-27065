using System;
using Microsoft.Exchange.Configuration.ObjectModel;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200009A RID: 154
	public abstract class SetObjectTask<TIdentity, TDataObject> : SetObjectTask<TIdentity, TDataObject, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ConfigObject, new()
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00016985 File Offset: 0x00014B85
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.Instance = this.DataObject;
		}
	}
}
