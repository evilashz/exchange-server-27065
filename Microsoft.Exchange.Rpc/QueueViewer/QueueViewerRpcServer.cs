using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.QueueViewer
{
	// Token: 0x020003A3 RID: 931
	internal abstract class QueueViewerRpcServer : RpcServerBase
	{
		// Token: 0x0600104E RID: 4174
		public abstract byte[] GetQueueViewerObjectPage(QVObjectType objectType, byte[] queryFilterBytes, byte[] sortOrderBytes, [MarshalAs(UnmanagedType.U1)] bool searchForward, int pageSize, byte[] bookmarkObjectBytes, int bookmarkIndex, [MarshalAs(UnmanagedType.U1)] bool includeBookmark, [MarshalAs(UnmanagedType.U1)] bool includeDetails, byte[] propertyBagBytes, ref int totalCount, ref int pageOffset);

		// Token: 0x0600104F RID: 4175
		public abstract int ReadMessageBody(byte[] mailItemIdBytes, byte[] buffer, int position, int count);

		// Token: 0x06001050 RID: 4176
		public abstract void FreezeMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes);

		// Token: 0x06001051 RID: 4177
		public abstract void UnfreezeMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes);

		// Token: 0x06001052 RID: 4178
		public abstract void DeleteMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes, [MarshalAs(UnmanagedType.U1)] bool withNDR);

		// Token: 0x06001053 RID: 4179
		public abstract void RedirectMessage(byte[] targetServersBytes);

		// Token: 0x06001054 RID: 4180
		public abstract void FreezeQueue(byte[] queueIdentityBytes, byte[] queryFilterBytes);

		// Token: 0x06001055 RID: 4181
		public abstract void UnfreezeQueue(byte[] queueIdentityBytes, byte[] queryFilterBytes);

		// Token: 0x06001056 RID: 4182
		public abstract void RetryQueue(byte[] queueIdentityBytes, byte[] queryFilterBytes, [MarshalAs(UnmanagedType.U1)] bool resubmit);

		// Token: 0x06001057 RID: 4183
		public abstract byte[] GetPropertyBagBasedQueueViewerObjectPage(QVObjectType objectType, byte[] inputObjectBytes, ref int totalCount, ref int pageOffset);

		// Token: 0x06001058 RID: 4184
		public abstract byte[] GetPropertyBagBasedQueueViewerObjectPageCustomSerialization(QVObjectType objectType, byte[] inputObjectBytes, ref int totalCount, ref int pageOffset);

		// Token: 0x06001059 RID: 4185
		public abstract void SetMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes, byte[] inputPropertiesBytes, [MarshalAs(UnmanagedType.U1)] bool resubmit);

		// Token: 0x0600105A RID: 4186 RVA: 0x0004BDE0 File Offset: 0x0004B1E0
		public QueueViewerRpcServer()
		{
		}

		// Token: 0x04000F96 RID: 3990
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IQueueViewer_v1_0_s_ifspec;
	}
}
