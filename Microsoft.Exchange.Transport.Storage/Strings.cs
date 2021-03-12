using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000005 RID: 5
	internal static class Strings
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000035C8 File Offset: 0x000017C8
		public static LocalizedString ItemNotFound(TransportMessageId messageId)
		{
			return new LocalizedString("ItemNotFound", Strings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035F0 File Offset: 0x000017F0
		public static LocalizedString FailedToRemove(TransportMessageId messageId)
		{
			return new LocalizedString("FailedToRemove", Strings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003618 File Offset: 0x00001818
		public static LocalizedString InvalidMessageStateTransition(TransportMessageId messageId, MessageDepotItemState currentState, MessageDepotItemState nextState)
		{
			return new LocalizedString("InvalidMessageStateTransition", Strings.ResourceManager, new object[]
			{
				messageId,
				currentState,
				nextState
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003654 File Offset: 0x00001854
		public static LocalizedString InvalidMessageStateForRemove(TransportMessageId messageId, MessageDepotItemState currentState)
		{
			return new LocalizedString("InvalidMessageStateForRemove", Strings.ResourceManager, new object[]
			{
				messageId,
				currentState
			});
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003688 File Offset: 0x00001888
		public static LocalizedString AcquireTokenMatchFail(TransportMessageId messageId)
		{
			return new LocalizedString("AcquireTokenMatchFail", Strings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036B0 File Offset: 0x000018B0
		public static LocalizedString DuplicateItemFound(TransportMessageId messageId)
		{
			return new LocalizedString("DuplicateItemFound", Strings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x04000027 RID: 39
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Storage.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000006 RID: 6
		private enum ParamIDs
		{
			// Token: 0x04000029 RID: 41
			ItemNotFound,
			// Token: 0x0400002A RID: 42
			FailedToRemove,
			// Token: 0x0400002B RID: 43
			InvalidMessageStateTransition,
			// Token: 0x0400002C RID: 44
			InvalidMessageStateForRemove,
			// Token: 0x0400002D RID: 45
			AcquireTokenMatchFail,
			// Token: 0x0400002E RID: 46
			DuplicateItemFound
		}
	}
}
