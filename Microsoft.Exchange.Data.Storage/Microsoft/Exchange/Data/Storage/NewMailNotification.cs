using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200070B RID: 1803
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NewMailNotification : Notification
	{
		// Token: 0x0600475C RID: 18268 RVA: 0x0012FA2C File Offset: 0x0012DC2C
		internal NewMailNotification(StoreObjectId newMailItemId, StoreObjectId parentFolderId, string messageClass, MessageFlags messageFlags) : base(NotificationType.NewMail)
		{
			this.newMailItemId = newMailItemId;
			this.parentFolderId = parentFolderId;
			this.messageClass = messageClass;
			this.messageFlags = messageFlags;
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x0012FA52 File Offset: 0x0012DC52
		public StoreObjectId NewMailItemId
		{
			get
			{
				return this.newMailItemId;
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x0012FA5A File Offset: 0x0012DC5A
		public StoreObjectId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x0600475F RID: 18271 RVA: 0x0012FA62 File Offset: 0x0012DC62
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x0012FA6A File Offset: 0x0012DC6A
		public MessageFlags MessageFlags
		{
			get
			{
				return this.messageFlags;
			}
		}

		// Token: 0x04002705 RID: 9989
		private readonly StoreObjectId newMailItemId;

		// Token: 0x04002706 RID: 9990
		private readonly StoreObjectId parentFolderId;

		// Token: 0x04002707 RID: 9991
		private readonly string messageClass;

		// Token: 0x04002708 RID: 9992
		private readonly MessageFlags messageFlags;
	}
}
