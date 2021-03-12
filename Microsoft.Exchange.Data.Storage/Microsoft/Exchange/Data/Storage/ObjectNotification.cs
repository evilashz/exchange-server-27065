using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200070C RID: 1804
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ObjectNotification : Notification
	{
		// Token: 0x06004761 RID: 18273 RVA: 0x0012FA72 File Offset: 0x0012DC72
		internal ObjectNotification(StoreObjectId notifyingItemId, StoreObjectId parentFolderId, StoreObjectId previousId, StoreObjectId previousParentFolderId, NotificationObjectType objectType, UnresolvedPropertyDefinition[] propertyDefinitions, NotificationType type) : base(type)
		{
			this.notifyingItemId = notifyingItemId;
			this.parentFolderId = parentFolderId;
			this.previousId = previousId;
			this.previousParentFolderId = previousParentFolderId;
			this.objectType = objectType;
			this.propertyDefinitions = propertyDefinitions;
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x0012FAA9 File Offset: 0x0012DCA9
		public StoreObjectId NotifyingItemId
		{
			get
			{
				return this.notifyingItemId;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x0012FAB1 File Offset: 0x0012DCB1
		public StoreObjectId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x0012FAB9 File Offset: 0x0012DCB9
		public StoreObjectId PreviousId
		{
			get
			{
				return this.previousId;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x0012FAC1 File Offset: 0x0012DCC1
		public StoreObjectId PreviousParentFolderId
		{
			get
			{
				return this.previousParentFolderId;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x0012FAC9 File Offset: 0x0012DCC9
		public NotificationObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x0012FAD1 File Offset: 0x0012DCD1
		public UnresolvedPropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return this.propertyDefinitions;
			}
		}

		// Token: 0x04002709 RID: 9993
		private readonly StoreObjectId notifyingItemId;

		// Token: 0x0400270A RID: 9994
		private readonly StoreObjectId parentFolderId;

		// Token: 0x0400270B RID: 9995
		private readonly StoreObjectId previousId;

		// Token: 0x0400270C RID: 9996
		private readonly StoreObjectId previousParentFolderId;

		// Token: 0x0400270D RID: 9997
		private readonly NotificationObjectType objectType;

		// Token: 0x0400270E RID: 9998
		private readonly UnresolvedPropertyDefinition[] propertyDefinitions;
	}
}
