using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E1D RID: 3613
	internal class PropertyNotSupportUpdateException : InvalidPropertyException
	{
		// Token: 0x06005D5A RID: 23898 RVA: 0x00123037 File Offset: 0x00121237
		public PropertyNotSupportUpdateException(string propertyName) : base(propertyName, CoreResources.ErrorPropertyNotSupportUpdate(propertyName))
		{
		}
	}
}
