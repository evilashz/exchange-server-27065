using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A3 RID: 675
	public class ServicePlanFeaturesValidationError : ValidationError
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0004DA59 File Offset: 0x0004BC59
		public ServicePlanFeaturesValidationError(string feature, string sku) : base(DataStrings.ServicePlanFeatureCheckFailed(feature, sku))
		{
		}
	}
}
