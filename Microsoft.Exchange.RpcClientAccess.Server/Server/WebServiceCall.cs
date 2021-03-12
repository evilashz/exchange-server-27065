using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200004D RID: 77
	internal class WebServiceCall : EasyAsyncResult
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000F24E File Offset: 0x0000D44E
		public WebServiceCall(AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000F258 File Offset: 0x0000D458
		internal static byte[] GetBuffer(int size)
		{
			return AsyncBufferPools.GetBuffer(size);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000F260 File Offset: 0x0000D460
		internal static void ReleaseBuffer(byte[] buffer)
		{
			AsyncBufferPools.ReleaseBuffer(buffer);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000F268 File Offset: 0x0000D468
		internal static ArraySegment<byte> GetResponseAuxSegment(int size, out byte[] buffer)
		{
			int num = Math.Min(size, EmsmdbConstants.MaxExtendedAuxBufferSize);
			buffer = WebServiceCall.GetBuffer(num);
			return new ArraySegment<byte>(buffer, 0, num);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000F294 File Offset: 0x0000D494
		internal static ArraySegment<byte> GetResponseRopSegment(int size, out byte[] buffer)
		{
			int num = Math.Min(size, EmsmdbConstants.MaxChainedExtendedRopBufferSize);
			buffer = WebServiceCall.GetBuffer(num);
			return new ArraySegment<byte>(buffer, 0, num);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000F2BE File Offset: 0x0000D4BE
		internal static ArraySegment<byte> BuildRequestSegment(byte[] buffer)
		{
			if (buffer != null && buffer.Length > 0)
			{
				return new ArraySegment<byte>(buffer);
			}
			return Array<byte>.EmptySegment;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		internal static byte[] BuildResponseArray(ArraySegment<byte> segment)
		{
			byte[] array = new byte[segment.Count];
			Array.Copy(segment.Array, segment.Offset, array, 0, segment.Count);
			return array;
		}
	}
}
