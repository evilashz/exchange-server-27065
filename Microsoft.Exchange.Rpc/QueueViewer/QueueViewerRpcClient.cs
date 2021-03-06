using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.QueueViewer
{
	// Token: 0x020003A0 RID: 928
	internal class QueueViewerRpcClient : RpcClientBase
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x0004B2E4 File Offset: 0x0004A6E4
		public QueueViewerRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004B2F8 File Offset: 0x0004A6F8
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetQueueViewerObjectPage(QVObjectType objectType, byte[] queryFilterBytes, byte[] sortOrder, [MarshalAs(UnmanagedType.U1)] bool searchForward, int pageSize, byte[] bookmarkObjectBytes, int bookmarkIndex, [MarshalAs(UnmanagedType.U1)] bool includeBookmark, [MarshalAs(UnmanagedType.U1)] bool includeDetails, [MarshalAs(UnmanagedType.U1)] bool usePreE14R4API, byte[] propertyBagBytes, ref int totalCount, ref int pageOffset)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			byte* ptr3 = null;
			byte[] result = null;
			byte* ptr4 = null;
			int num = 0;
			try
			{
				int num2 = 0;
				ptr = <Module>.MToUBytesClient(queryFilterBytes, &num2);
				int num3 = 0;
				ptr2 = <Module>.MToUBytesClient(sortOrder, &num3);
				int num4 = 0;
				ptr3 = <Module>.MToUBytesClient(bookmarkObjectBytes, &num4);
				int num5 = 0;
				byte* ptr5 = <Module>.MToUBytesClient(propertyBagBytes, &num5);
				int cBytes;
				int num9;
				int num10;
				try
				{
					if (usePreE14R4API)
					{
						int num6 = includeDetails ? 1 : 0;
						int num7 = includeBookmark ? 1 : 0;
						int num8 = searchForward ? 1 : 0;
						num = <Module>.cli_GetQueueViewerObjectPage(base.BindingHandle, (tagObjectType)objectType, num2, ptr, num3, ptr2, num8, pageSize, num4, ptr3, bookmarkIndex, num7, num6, &cBytes, &ptr4, &num9, &num10);
					}
					else
					{
						int num11 = includeDetails ? 1 : 0;
						int num12 = includeBookmark ? 1 : 0;
						int num13 = searchForward ? 1 : 0;
						num = <Module>.cli_GetQueueViewerObjectPageWithPropertyBag(base.BindingHandle, (tagObjectType)objectType, num2, ptr, num3, ptr2, num13, pageSize, num4, ptr3, bookmarkIndex, num12, num11, num5, ptr5, &cBytes, &ptr4, &num9, &num10);
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "GetQueueViewerObjectPage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
				result = <Module>.UToMBytes(cBytes, ptr4);
				totalCount = num9;
				pageOffset = num10;
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
				if (ptr3 != null)
				{
					<Module>.MIDL_user_free((void*)ptr3);
				}
				if (ptr4 != null)
				{
					<Module>.MIDL_user_free((void*)ptr4);
				}
			}
			return result;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004B480 File Offset: 0x0004A880
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] ReadMessageBody(byte[] mailItemIdBytes, int position, int count)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			byte[] result = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(mailItemIdBytes, &num2);
				int cBytes;
				try
				{
					num = <Module>.cli_ReadMessageBody(base.BindingHandle, num2, ptr, position, count, &cBytes, &ptr2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ReadMessageBody");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
			return result;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004B52C File Offset: 0x0004A92C
		[HandleProcessCorruptedStateExceptions]
		public unsafe void FreezeMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(mailItemIdBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				try
				{
					num = <Module>.cli_FreezeMessage(base.BindingHandle, num2, ptr, num3, ptr2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "FreezeMessage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004B5D0 File Offset: 0x0004A9D0
		[HandleProcessCorruptedStateExceptions]
		public unsafe void UnfreezeMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(mailItemIdBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				try
				{
					num = <Module>.cli_UnfreezeMessage(base.BindingHandle, num2, ptr, num3, ptr2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "UnfreezeMessage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004B674 File Offset: 0x0004AA74
		[HandleProcessCorruptedStateExceptions]
		public unsafe void DeleteMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes, [MarshalAs(UnmanagedType.U1)] bool withNDR)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(mailItemIdBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				try
				{
					int num4 = withNDR ? 1 : 0;
					num = <Module>.cli_DeleteMessage(base.BindingHandle, num2, ptr, num3, ptr2, num4);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "DeleteMessage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004B724 File Offset: 0x0004AB24
		[HandleProcessCorruptedStateExceptions]
		public unsafe void RedirectMessage(byte[] targetServersBytes)
		{
			byte* ptr = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(targetServersBytes, &num2);
				try
				{
					num = <Module>.cli_RedirectMessage(base.BindingHandle, num2, ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "RedirectMessage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004B7B0 File Offset: 0x0004ABB0
		[HandleProcessCorruptedStateExceptions]
		public unsafe void FreezeQueue(byte[] queueIdentityBytes, byte[] queryFilterBytes)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(queueIdentityBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				try
				{
					num = <Module>.cli_FreezeQueue(base.BindingHandle, num2, ptr, num3, ptr2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "FreezeQueue");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004B854 File Offset: 0x0004AC54
		[HandleProcessCorruptedStateExceptions]
		public unsafe void UnfreezeQueue(byte[] queueIdentityBytes, byte[] queryFilterBytes)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(queueIdentityBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				try
				{
					num = <Module>.cli_UnfreezeQueue(base.BindingHandle, num2, ptr, num3, ptr2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "UnfreezeQueue");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004B8F8 File Offset: 0x0004ACF8
		[HandleProcessCorruptedStateExceptions]
		public unsafe void RetryQueue(byte[] queueIdentityBytes, byte[] queryFilterBytes, [MarshalAs(UnmanagedType.U1)] bool resubmit)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(queueIdentityBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				try
				{
					int num4 = resubmit ? 1 : 0;
					num = <Module>.cli_RetryQueue(base.BindingHandle, num2, ptr, num3, ptr2, num4);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "RetryQueue");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004B9A8 File Offset: 0x0004ADA8
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetPropertyBagBasedQueueViewerObjectPage(QVObjectType objectType, byte[] inputObjectBytes, ref int totalCount, ref int pageOffset)
		{
			byte* ptr = null;
			byte[] result = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2 = 0;
				ptr = <Module>.MToUBytesClient(inputObjectBytes, &num2);
				int cBytes;
				int num3;
				int num4;
				try
				{
					num = <Module>.cli_GetPropertyBagBasedQueueViewerObjectPage(base.BindingHandle, (tagObjectType)objectType, num2, ptr, &cBytes, &ptr2, &num3, &num4);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "GetPropertyBagBasedQueueViewerObjectPage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
				totalCount = num3;
				pageOffset = num4;
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
			return result;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004BA64 File Offset: 0x0004AE64
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetPropertyBagBasedQueueViewerObjectPageCustomSerialization(QVObjectType objectType, byte[] inputObjectBytes, ref int totalCount, ref int pageOffset)
		{
			byte* ptr = null;
			byte[] result = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2 = 0;
				ptr = <Module>.MToUBytesClient(inputObjectBytes, &num2);
				int cBytes;
				int num3;
				int num4;
				try
				{
					num = <Module>.cli_GetPropertyBagBasedQueueViewerObjectPageCustomSerialization(base.BindingHandle, (tagObjectType)objectType, num2, ptr, &cBytes, &ptr2, &num3, &num4);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "GetPropertyBagBasedQueueViewerObjectPage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
				totalCount = num3;
				pageOffset = num4;
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
			return result;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0004BB20 File Offset: 0x0004AF20
		[HandleProcessCorruptedStateExceptions]
		public unsafe void SetMessage(byte[] mailItemIdBytes, byte[] queryFilterBytes, byte[] inputPropertiesBytes, [MarshalAs(UnmanagedType.U1)] bool resubmit)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int num = 0;
			try
			{
				int num2;
				ptr = <Module>.MToUBytesClient(mailItemIdBytes, &num2);
				int num3;
				ptr2 = <Module>.MToUBytesClient(queryFilterBytes, &num3);
				int num4;
				byte* ptr3 = <Module>.MToUBytesClient(inputPropertiesBytes, &num4);
				try
				{
					int num5 = resubmit ? 1 : 0;
					num = <Module>.cli_SetMessage(base.BindingHandle, num2, ptr, num3, ptr2, num4, ptr3, num5);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "SetMessage");
				}
				if (num < 0)
				{
					throw new QueueViewerException(num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
		}
	}
}
