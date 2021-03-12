using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A4 RID: 676
	public class ServicePlanSchemaValidationError : ValidationError
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x0004DA68 File Offset: 0x0004BC68
		public ServicePlanSchemaValidationError(string schemaError) : base(DataStrings.ServicePlanSchemaCheckFailed(schemaError))
		{
		}
	}
}
