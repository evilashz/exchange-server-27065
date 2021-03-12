using System;
using Microsoft.Exchange.Configuration.ObjectModel;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200009B RID: 155
	public abstract class SetObjectTask<TDataObject> : SetObjectTask<ConfigObjectIdParameter, TDataObject> where TDataObject : ConfigObject, new()
	{
	}
}
