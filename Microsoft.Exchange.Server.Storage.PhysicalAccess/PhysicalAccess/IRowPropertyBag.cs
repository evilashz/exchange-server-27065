using System;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000014 RID: 20
	public interface IRowPropertyBag
	{
		// Token: 0x060000B4 RID: 180
		object GetPropertyValue(Connection connection, StorePropTag propTag);
	}
}
