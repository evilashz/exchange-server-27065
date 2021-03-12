using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200021D RID: 541
	internal class FilterNotSupportedExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x000451C3 File Offset: 0x000433C3
		public FilterNotSupportedExceptionMapping() : base(typeof(FilterNotSupportedException), ResponseCodeType.ErrorUnsupportedPathForQuery, CoreResources.IDs.ErrorUnsupportedPathForQuery)
		{
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000451E0 File Offset: 0x000433E0
		protected override PropertyPath[] GetPropertyPaths(LocalizedException exception)
		{
			FilterNotSupportedException ex = base.VerifyExceptionType<FilterNotSupportedException>(exception);
			List<PropertyPath> list = new List<PropertyPath>();
			foreach (PropertyDefinition propertyDefinition in ex.Properties)
			{
				list.Add(SearchSchemaMap.GetPropertyPath(propertyDefinition));
			}
			return list.ToArray();
		}
	}
}
