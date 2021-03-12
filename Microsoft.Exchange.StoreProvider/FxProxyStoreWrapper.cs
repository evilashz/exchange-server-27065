using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200005F RID: 95
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FxProxyStoreWrapper : FxProxyWrapper, IMsgStore, IMAPIProp
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000B99B File Offset: 0x00009B9B
		internal FxProxyStoreWrapper(IMapiFxCollector iFxCollector) : base(iFxCollector)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public int Advise(int cbEntryID, byte[] lpEntryId, AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection)
		{
			piConnection = IntPtr.Zero;
			return -2147221246;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		public int Unadvise(IntPtr iConnection)
		{
			return -2147221246;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B9BE File Offset: 0x00009BBE
		public int Slot10()
		{
			return -2147221246;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B9C5 File Offset: 0x00009BC5
		public int OpenEntry(int cbEntryID, byte[] lpEntryID, Guid lpInterface, int ulFlags, out int lpulObjType, out object obj)
		{
			lpulObjType = 0;
			obj = null;
			return -2147221246;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		public int SetReceiveFolder(string lpwszMessageClass, int ulFlags, int cbEntryId, byte[] lpEntryID)
		{
			return -2147221246;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B9DB File Offset: 0x00009BDB
		public int GetReceiveFolder(string lpwszMessageClass, int ulFlags, out int lpcbEntryId, out SafeExLinkedMemoryHandle lppEntryId, out SafeExLinkedMemoryHandle lppszExplicitClass)
		{
			lpcbEntryId = 0;
			lppEntryId = null;
			lppszExplicitClass = null;
			return -2147221246;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B9ED File Offset: 0x00009BED
		public int Slot14()
		{
			return -2147221246;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B9F4 File Offset: 0x00009BF4
		public int StoreLogoff(ref int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B9FB File Offset: 0x00009BFB
		public int AbortSubmit(int cbEntryID, byte[] lpEntryID, int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000BA02 File Offset: 0x00009C02
		public int Slot17()
		{
			return -2147221246;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000BA09 File Offset: 0x00009C09
		public int Slot18()
		{
			return -2147221246;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000BA10 File Offset: 0x00009C10
		public int Slot19()
		{
			return -2147221246;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000BA17 File Offset: 0x00009C17
		public int Slot1a()
		{
			return -2147221246;
		}
	}
}
