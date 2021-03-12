using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.IPFilter
{
	// Token: 0x02000273 RID: 627
	internal class IPFilterRpcClient : RpcClientBase
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x00029278 File Offset: 0x00028678
		public IPFilterRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002928C File Offset: 0x0002868C
		[HandleProcessCorruptedStateExceptions]
		public unsafe int Add([MarshalAs(UnmanagedType.U1)] bool denyEntry, IPFilterRange element)
		{
			int result = -1;
			IPRangeData iprangeData = 0;
			DateTime expiresOn = element.ExpiresOn;
			*(ref iprangeData + 40) = expiresOn.ToFileTimeUtc();
			*(ref iprangeData + 4) = (element.Flags | 256);
			int num = denyEntry ? 32 : 16;
			*(ref iprangeData + 4) = (*(ref iprangeData + 4) | num);
			element.GetLowerBound(ref iprangeData + 8, ref iprangeData + 16);
			element.GetUpperBound(ref iprangeData + 24, ref iprangeData + 32);
			int num2 = <Module>.StringToUnmanaged(element.Comment, ref iprangeData + 48);
			if (num2 >= 0)
			{
				try
				{
					num2 = <Module>.cli_IpFilterAdd(base.BindingHandle, iprangeData, &result);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "Add");
				}
				finally
				{
					if (*(ref iprangeData + 48) != 0L)
					{
						<Module>.MIDL_user_free(*(ref iprangeData + 48));
						*(ref iprangeData + 48) = 0L;
					}
				}
				return result;
			}
			if (num2 == -2147024882)
			{
				throw new OutOfMemoryException();
			}
			if (num2 != -2147024809)
			{
				throw new InvalidOperationException("Attempt to marshal string failed");
			}
			throw new ArgumentException("String copy failure", "item.Comment");
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000293BC File Offset: 0x000287BC
		[HandleProcessCorruptedStateExceptions]
		public unsafe int Remove(int identity, [MarshalAs(UnmanagedType.U1)] bool denyEntries)
		{
			int num = 0;
			int num2 = (!denyEntries) ? 3856 : 3872;
			try
			{
				int num3;
				num = ((<Module>.cli_IpFilterRemove(base.BindingHandle, identity, num2, &num3) >= 0) ? num3 : num);
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "Remove");
			}
			finally
			{
			}
			return num;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00029444 File Offset: 0x00028844
		[HandleProcessCorruptedStateExceptions]
		public unsafe IPFilterRange[] GetItems(int startIdentity, int flags, ulong highBits, ulong lowBits, int count)
		{
			IPFilterRange[] array = null;
			int num = 0;
			IPRangeData* ptr = null;
			ExInt128 exInt = highBits;
			*(ref exInt + 8) = (long)lowBits;
			try
			{
				if (<Module>.cli_IpFilterGetItems(base.BindingHandle, startIdentity, flags, exInt, count, &ptr, &num) >= 0)
				{
					if (num == 0)
					{
						return null;
					}
					array = new IPFilterRange[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = new IPFilterRange();
						this.Convert(ref array[i], (long)i * 56L + ptr / sizeof(IPRangeData));
					}
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "Remove");
			}
			finally
			{
				for (int j = 0; j < num; j++)
				{
					IPRangeData* ptr2 = (long)j * 56L + ptr / sizeof(IPRangeData) + 48L / (long)sizeof(IPRangeData);
					ulong num2 = (ulong)(*(long*)ptr2);
					if (num2 != 0UL)
					{
						<Module>.MIDL_user_free(num2);
						*(long*)ptr2 = 0L;
					}
				}
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000291DC File Offset: 0x000285DC
		private unsafe int Convert(ref IPFilterRange destination, IPRangeData* source)
		{
			destination.Identity = *(int*)source;
			destination.SetLowerBound((ulong)(*(long*)(source + 8L / (long)sizeof(IPRangeData))), (ulong)(*(long*)(source + 16L / (long)sizeof(IPRangeData))));
			destination.SetUpperBound((ulong)(*(long*)(source + 24L / (long)sizeof(IPRangeData))), (ulong)(*(long*)(source + 32L / (long)sizeof(IPRangeData))));
			destination.Flags = *(int*)(source + 4L / (long)sizeof(IPRangeData));
			DateTime expiresOn = DateTime.FromFileTimeUtc(*(long*)(source + 40L / (long)sizeof(IPRangeData)));
			destination.ExpiresOn = expiresOn;
			ulong num = (ulong)(*(long*)(source + 48L / (long)sizeof(IPRangeData)));
			if (num != 0UL)
			{
				destination.Comment = new string(num);
			}
			return 0;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00029250 File Offset: 0x00028650
		private unsafe void FreeRecord(IPRangeData* destination)
		{
			ulong num = (ulong)(*(long*)(destination + 48L / (long)sizeof(IPRangeData)));
			if (num != 0UL)
			{
				<Module>.MIDL_user_free(num);
				*(long*)(destination + 48L / (long)sizeof(IPRangeData)) = 0L;
			}
		}
	}
}
