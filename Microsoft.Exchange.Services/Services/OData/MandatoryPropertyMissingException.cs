using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E1E RID: 3614
	internal class MandatoryPropertyMissingException : InvalidPropertyException
	{
		// Token: 0x06005D5B RID: 23899 RVA: 0x00123046 File Offset: 0x00121246
		public MandatoryPropertyMissingException(string propertyName) : base(propertyName, CoreResources.ErrorMandatoryPropertyMissing(propertyName))
		{
		}
	}
}
