using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000146 RID: 326
	internal interface IPropertyContainer : IProperty
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000FC4 RID: 4036
		IList<IProperty> Children { get; }

		// Token: 0x06000FC5 RID: 4037
		void SetCopyDestination(IPropertyContainer dstPropertyContainer);
	}
}
