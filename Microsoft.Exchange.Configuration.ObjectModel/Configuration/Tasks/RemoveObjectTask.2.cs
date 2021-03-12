using System;
using Microsoft.Exchange.Configuration.ObjectModel;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200008A RID: 138
	public abstract class RemoveObjectTask<TDataObject> : RemoveObjectTask<ConfigObjectIdParameter, TDataObject> where TDataObject : ConfigObject, new()
	{
	}
}
