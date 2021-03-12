using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007CF RID: 1999
	internal class MalformedRoutingTypeException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003B04 RID: 15108 RVA: 0x000CFADC File Offset: 0x000CDCDC
		public MalformedRoutingTypeException(PropertyPath propertyPath, Exception innerException) : base((CoreResources.IDs)4103342537U, propertyPath, innerException)
		{
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000CFAF0 File Offset: 0x000CDCF0
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
