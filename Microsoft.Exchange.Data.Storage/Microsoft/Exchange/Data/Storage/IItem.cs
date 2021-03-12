using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000075 RID: 117
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IItem : IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060007F2 RID: 2034
		ConflictResolutionResult Save(SaveMode saveMode);

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007F3 RID: 2035
		AttachmentCollection AttachmentCollection { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007F4 RID: 2036
		Body Body { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007F5 RID: 2037
		IBody IBody { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007F6 RID: 2038
		ItemCategoryList Categories { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007F7 RID: 2039
		ICoreItem CoreItem { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007F8 RID: 2040
		ItemCharsetDetector CharsetDetector { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007F9 RID: 2041
		IAttachmentCollection IAttachmentCollection { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007FA RID: 2042
		MapiMessage MapiMessage { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007FB RID: 2043
		// (set) Token: 0x060007FC RID: 2044
		PropertyBagSaveFlags SaveFlags { get; set; }

		// Token: 0x060007FD RID: 2045
		void OpenAsReadWrite();
	}
}
