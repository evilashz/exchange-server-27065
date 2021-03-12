using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200084F RID: 2127
	internal sealed class PropertyUpdateException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003D55 RID: 15701 RVA: 0x000D74AA File Offset: 0x000D56AA
		public PropertyUpdateException(PropertyPath[] properties, Exception innerException) : base(CoreResources.IDs.ErrorPropertyUpdate, properties, innerException)
		{
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x000D74BE File Offset: 0x000D56BE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
