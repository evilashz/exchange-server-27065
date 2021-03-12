using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001B1 RID: 433
	internal interface IXsoDataObjectGenerator : IDataObjectGenerator
	{
		// Token: 0x0600123C RID: 4668
		XsoDataObject GetInnerXsoDataObject();
	}
}
