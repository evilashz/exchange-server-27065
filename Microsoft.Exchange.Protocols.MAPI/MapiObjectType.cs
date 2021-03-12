using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000018 RID: 24
	public enum MapiObjectType : uint
	{
		// Token: 0x040000C9 RID: 201
		Invalid,
		// Token: 0x040000CA RID: 202
		Attachment,
		// Token: 0x040000CB RID: 203
		Event,
		// Token: 0x040000CC RID: 204
		Folder,
		// Token: 0x040000CD RID: 205
		Logon,
		// Token: 0x040000CE RID: 206
		Message,
		// Token: 0x040000CF RID: 207
		EmbeddedMessage,
		// Token: 0x040000D0 RID: 208
		Person,
		// Token: 0x040000D1 RID: 209
		FastTransferContext,
		// Token: 0x040000D2 RID: 210
		Notify,
		// Token: 0x040000D3 RID: 211
		Stream,
		// Token: 0x040000D4 RID: 212
		MessageView,
		// Token: 0x040000D5 RID: 213
		FolderView,
		// Token: 0x040000D6 RID: 214
		AttachmentView,
		// Token: 0x040000D7 RID: 215
		IcsUploadContext,
		// Token: 0x040000D8 RID: 216
		FastTransferStream
	}
}
