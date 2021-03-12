using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E1B RID: 3611
	internal class PropertyNotSupportFilterException : InvalidPropertyException
	{
		// Token: 0x06005D58 RID: 23896 RVA: 0x00123019 File Offset: 0x00121219
		public PropertyNotSupportFilterException(string propertyName) : base(propertyName, CoreResources.ErrorPropertyNotSupportFilter(propertyName))
		{
		}
	}
}
