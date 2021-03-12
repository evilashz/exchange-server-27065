using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200022B RID: 555
	internal class UnsupportedPropertyForSortGroupExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x000458CD File Offset: 0x00043ACD
		public UnsupportedPropertyForSortGroupExceptionMapping() : base(typeof(UnsupportedPropertyForSortGroupException), ResponseCodeType.ErrorUnsupportedPathForSortGroup, CoreResources.IDs.ErrorUnsupportedPathForSortGroup)
		{
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000458EC File Offset: 0x00043AEC
		protected override PropertyPath[] GetPropertyPaths(LocalizedException exception)
		{
			UnsupportedPropertyForSortGroupException ex = base.VerifyExceptionType<UnsupportedPropertyForSortGroupException>(exception);
			return new List<PropertyPath>
			{
				SearchSchemaMap.GetPropertyPath(ex.UnsupportedProperty)
			}.ToArray();
		}
	}
}
