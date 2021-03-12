using System;
using Microsoft.Exchange.Configuration.ObjectModel;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000063 RID: 99
	public abstract class GetObjectTask<TDataObject> : GetObjectTask<ConfigObjectIdParameter, TDataObject> where TDataObject : ConfigObject, new()
	{
	}
}
