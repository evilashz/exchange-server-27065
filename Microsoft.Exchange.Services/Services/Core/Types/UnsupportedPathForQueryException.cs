using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B2 RID: 2226
	internal sealed class UnsupportedPathForQueryException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003F3F RID: 16191 RVA: 0x000DB26C File Offset: 0x000D946C
		public UnsupportedPathForQueryException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorUnsupportedPathForQuery, propertyPath)
		{
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000DB27F File Offset: 0x000D947F
		public UnsupportedPathForQueryException(PropertyDefinition propertyDefinition, Exception innerException) : base(CoreResources.IDs.ErrorUnsupportedPathForQuery, SearchSchemaMap.GetPropertyPath(propertyDefinition), innerException)
		{
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x000DB298 File Offset: 0x000D9498
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
