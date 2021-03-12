using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FxProxyMessageWrapper : FxProxyWrapper, IMessage, IMAPIProp
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000AC75 File Offset: 0x00008E75
		internal FxProxyMessageWrapper(IMapiFxCollector iFxCollector) : base(iFxCollector)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AC7E File Offset: 0x00008E7E
		public int GetAttachmentTable(int ulFlags, out IMAPITable iMAPITable)
		{
			iMAPITable = null;
			return -2147221246;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000AC88 File Offset: 0x00008E88
		public int OpenAttach(int attachmentNumber, Guid lpInterface, int ulFlags, out IAttach iAttach)
		{
			iAttach = null;
			return -2147221246;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AC93 File Offset: 0x00008E93
		public int CreateAttach(Guid lpInterface, int ulFlags, out int attachmentNumber, out IAttach iAttach)
		{
			attachmentNumber = 0;
			iAttach = null;
			return -2147221246;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		public int DeleteAttach(int attachmentNumber, IntPtr ulUiParam, IntPtr lpProgress, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public int GetRecipientTable(int ulFlags, out IMAPITable iMAPITable)
		{
			iMAPITable = null;
			return -2147221246;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		public unsafe int ModifyRecipients(int ulFlags, _AdrList* padrList)
		{
			return -2147221246;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000ACB9 File Offset: 0x00008EB9
		public int SubmitMessage(int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public int SetReadFlag(int ulFlags)
		{
			return -2147221246;
		}
	}
}
