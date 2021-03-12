using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C4 RID: 1988
	[Serializable]
	internal sealed class InvalidPropertyForOperationException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AE5 RID: 15077 RVA: 0x000CF94F File Offset: 0x000CDB4F
		public InvalidPropertyForOperationException(PropertyPath propertyPath) : base((CoreResources.IDs)2517173182U, propertyPath)
		{
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000CF962 File Offset: 0x000CDB62
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
