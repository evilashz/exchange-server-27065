using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAttachmentProvider : IDisposable
	{
		// Token: 0x0600031A RID: 794
		void SetCollection(CoreAttachmentCollection collection);

		// Token: 0x0600031B RID: 795
		PropertyBag[] QueryAttachmentTable(NativeStorePropertyDefinition[] properties);

		// Token: 0x0600031C RID: 796
		PersistablePropertyBag CreateAttachment(ICollection<PropertyDefinition> prefetchProperties, CoreAttachment attachmentToCopy, IItem itemToAttach, out int attachmentNumber);

		// Token: 0x0600031D RID: 797
		bool SupportsCreateClone(AttachmentPropertyBag attachmentBagToClone);

		// Token: 0x0600031E RID: 798
		PersistablePropertyBag OpenAttachment(ICollection<PropertyDefinition> prefetchProperties, AttachmentPropertyBag attachmentBag);

		// Token: 0x0600031F RID: 799
		void DeleteAttachment(int attachmentNumber);

		// Token: 0x06000320 RID: 800
		ICoreItem OpenAttachedItem(ICollection<PropertyDefinition> prefetchProperties, AttachmentPropertyBag attachmentBag, bool isNew);

		// Token: 0x06000321 RID: 801
		bool ExistsInCollection(AttachmentPropertyBag attachmentBag);

		// Token: 0x06000322 RID: 802
		void OnAttachmentLoad(AttachmentPropertyBag attachmentBag);

		// Token: 0x06000323 RID: 803
		void OnBeforeAttachmentSave(AttachmentPropertyBag attachmentBag);

		// Token: 0x06000324 RID: 804
		void OnAfterAttachmentSave(AttachmentPropertyBag attachmentBag);

		// Token: 0x06000325 RID: 805
		void OnAttachmentDisconnected(AttachmentPropertyBag attachmentBag, PersistablePropertyBag persistablePropertyBag);

		// Token: 0x06000326 RID: 806
		void OnCollectionDisposed(AttachmentPropertyBag attachmentBag, PersistablePropertyBag persistablePropertyBag);

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000327 RID: 807
		NativeStorePropertyDefinition[] AttachmentTablePropertyList { get; }
	}
}
