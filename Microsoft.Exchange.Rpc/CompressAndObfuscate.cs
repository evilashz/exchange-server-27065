using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000252 RID: 594
	public class CompressAndObfuscate : ICompressAndObfuscate
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x00026B84 File Offset: 0x00025F84
		private CompressAndObfuscate()
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00026B98 File Offset: 0x00025F98
		public static ICompressAndObfuscate Instance
		{
			get
			{
				if (CompressAndObfuscate.instance == null)
				{
					CompressAndObfuscate.instance = new CompressAndObfuscate();
				}
				return CompressAndObfuscate.instance;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00026BBC File Offset: 0x00025FBC
		public virtual int MaxCompressionSize
		{
			get
			{
				return EmsmdbConstants.MaxRopBufferSize;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00026BD0 File Offset: 0x00025FD0
		public virtual int MinCompressionSize
		{
			get
			{
				return EmsmdbConstants.MinCompressionSize;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00026BE4 File Offset: 0x00025FE4
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe virtual bool TryCompress(ArraySegment<byte> source, ArraySegment<byte> destination, ref int compressedSize)
		{
			compressedSize = 0;
			if (source.Count > destination.Count)
			{
				throw new ArgumentException("source is larger than destination");
			}
			if (source.Count < EmsmdbConstants.MinCompressionSize)
			{
				throw new ArgumentException("source is too small to compress");
			}
			uint count = source.Count;
			uint num = count;
			ref byte pbOrig = ref source.Array[source.Offset];
			try
			{
				ref byte pbComp = ref destination.Array[destination.Offset];
				try
				{
					<Module>.Compress(ref pbOrig, count, ref pbComp, &num);
				}
				catch
				{
					throw;
				}
			}
			catch
			{
				throw;
			}
			byte condition = (num <= count) ? 1 : 0;
			ExAssert.Assert(condition != 0, "Compress should never return anything larger than the max destination buffer");
			if (num >= count)
			{
				return false;
			}
			compressedSize = num;
			return true;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00026CCC File Offset: 0x000260CC
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool TryExpand(ArraySegment<byte> source, ArraySegment<byte> destination)
		{
			uint count = source.Count;
			uint count2 = destination.Count;
			ref byte pbComp = ref source.Array[source.Offset];
			try
			{
				ref byte pbOrig = ref destination.Array[destination.Offset];
				try
				{
					if (<Module>.Decompress(ref pbOrig, count2, ref pbComp, count) != null)
					{
						goto IL_59;
					}
				}
				catch
				{
					throw;
				}
			}
			catch
			{
				throw;
			}
			return false;
			IL_59:
			try
			{
			}
			catch
			{
				throw;
			}
			return true;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00026D8C File Offset: 0x0002618C
		public unsafe virtual void Obfuscate(ArraySegment<byte> buffer)
		{
			uint count = buffer.Count;
			ref byte byte& = ref buffer.Array[buffer.Offset];
			try
			{
				byte* ptr = ref byte&;
				if (0 < count)
				{
					uint num = count;
					do
					{
						*ptr ^= 165;
						ptr += 1L;
						num += -1;
					}
					while (num > 0);
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x04000CBB RID: 3259
		private static CompressAndObfuscate instance = null;
	}
}
