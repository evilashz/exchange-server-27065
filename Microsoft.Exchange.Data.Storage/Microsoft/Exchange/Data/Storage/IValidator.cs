using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000677 RID: 1655
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IValidator
	{
		// Token: 0x06004447 RID: 17479
		bool Validate(DefaultFolderContext context, PropertyBag propertyBag);

		// Token: 0x06004448 RID: 17480
		void SetProperties(DefaultFolderContext context, Folder folder);
	}
}
