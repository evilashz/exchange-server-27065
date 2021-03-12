using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000681 RID: 1665
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MatchContainerClassOrThrow : MatchContainerClass
	{
		// Token: 0x0600446C RID: 17516 RVA: 0x00123D43 File Offset: 0x00121F43
		internal MatchContainerClassOrThrow(string containerClass) : base(containerClass)
		{
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x00123D4C File Offset: 0x00121F4C
		public override bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			if (!base.Validate(context, propertyBag))
			{
				throw new DefaultFolderPropertyValidationException(ServerStrings.MatchContainerClassValidationFailed);
			}
			return true;
		}
	}
}
