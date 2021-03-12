using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002CA RID: 714
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContact : IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001E9E RID: 7838
		IDictionary<EmailAddressIndex, Participant> EmailAddresses { get; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001E9F RID: 7839
		// (set) Token: 0x06001EA0 RID: 7840
		string ImAddress { get; set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001EA1 RID: 7841
		// (set) Token: 0x06001EA2 RID: 7842
		bool IsFavorite { get; set; }

		// Token: 0x06001EA3 RID: 7843
		Stream GetPhotoStream();
	}
}
