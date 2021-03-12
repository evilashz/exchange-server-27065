using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000293 RID: 659
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiMessage : IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C0F RID: 3087
		int GetAttachmentTable(int ulFlags, out IExMapiTable iMAPITable);

		// Token: 0x06000C10 RID: 3088
		int OpenAttach(int attachmentNumber, Guid lpInterface, int ulFlags, out IExMapiAttach iAttach);

		// Token: 0x06000C11 RID: 3089
		int CreateAttach(Guid lpInterface, int ulFlags, out int attachmentNumber, out IExMapiAttach iAttach);

		// Token: 0x06000C12 RID: 3090
		int DeleteAttach(int attachmentNumber, IntPtr ulUiParam, IntPtr lpProgress, int ulFlags);

		// Token: 0x06000C13 RID: 3091
		int GetRecipientTable(int ulFlags, out IExMapiTable iMAPITable);

		// Token: 0x06000C14 RID: 3092
		int ModifyRecipients(int ulFlags, AdrEntry[] padrList);

		// Token: 0x06000C15 RID: 3093
		int SubmitMessage(int ulFlags);

		// Token: 0x06000C16 RID: 3094
		int SetReadFlag(int ulFlags);

		// Token: 0x06000C17 RID: 3095
		int Deliver(int ulFlags);

		// Token: 0x06000C18 RID: 3096
		int DoneWithMessage();

		// Token: 0x06000C19 RID: 3097
		int DuplicateDeliveryCheck(string internetMessageId, long submitTimeAsLong);

		// Token: 0x06000C1A RID: 3098
		int TransportSendMessage(out PropValue[] lppPropArray);

		// Token: 0x06000C1B RID: 3099
		int SubmitMessageEx(int ulFlags);
	}
}
