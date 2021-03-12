using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E1C RID: 3612
	internal class PropertyNotSupportCreateException : InvalidPropertyException
	{
		// Token: 0x06005D59 RID: 23897 RVA: 0x00123028 File Offset: 0x00121228
		public PropertyNotSupportCreateException(string propertyName) : base(propertyName, CoreResources.ErrorPropertyNotSupportCreate(propertyName))
		{
		}
	}
}
