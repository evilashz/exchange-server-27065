using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000033 RID: 51
	internal class AmClusterPropList
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00009887 File Offset: 0x00007A87
		public AmClusterPropList(IntPtr rawBuffer, uint bufferSize)
		{
			this.RawBuffer = rawBuffer;
			this.BufferSize = bufferSize;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000989D File Offset: 0x00007A9D
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000098A5 File Offset: 0x00007AA5
		public IntPtr RawBuffer { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000098AE File Offset: 0x00007AAE
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000098B6 File Offset: 0x00007AB6
		public uint BufferSize { get; private set; }

		// Token: 0x06000213 RID: 531 RVA: 0x000098C0 File Offset: 0x00007AC0
		public static IntPtr IntPtrAdd(IntPtr left, int right)
		{
			long num = left.ToInt64();
			num += (long)right;
			return new IntPtr(num);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000098E0 File Offset: 0x00007AE0
		public int ParseIntFromPropList(string propName)
		{
			uint result;
			uint num = ClusapiMethods.ResUtilFindDwordProperty(this.RawBuffer, this.BufferSize, propName, out result);
			if (num == 2U)
			{
				result = 0U;
			}
			else if (num != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)num, "ResUtilFindDwordProperty()", new object[0]);
			}
			return (int)result;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009920 File Offset: 0x00007B20
		public string ParseStringFromPropList(string propName)
		{
			string empty;
			uint num = ClusapiMethods.ResUtilFindSzProperty(this.RawBuffer, this.BufferSize, propName, out empty);
			if (num == 2U)
			{
				empty = string.Empty;
			}
			else if (num != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)num, "ResUtilFindSzProperty()", new object[0]);
			}
			return empty;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009964 File Offset: 0x00007B64
		public string[] ParseMultipleStringsFromPropList(string propName)
		{
			List<string> list = new List<string>(10);
			SafeHGlobalHandle safeHGlobalHandle;
			uint num2;
			uint num = ClusapiMethods.ResUtilFindMultiSzProperty(this.RawBuffer, this.BufferSize, propName, out safeHGlobalHandle, out num2);
			if (num != 2U)
			{
				if (num != 0U)
				{
					throw AmExceptionHelper.ConstructClusterApiException((int)num, "ResUtilFindMultiSzProperty()", new object[0]);
				}
				IntPtr left = safeHGlobalHandle.DangerousGetHandle();
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					string text = Marshal.PtrToStringUni(AmClusterPropList.IntPtrAdd(left, num3));
					list.Add(text);
					num3 += (text.Length + 1) * 2;
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000099EC File Offset: 0x00007BEC
		public MyType Read<MyType>(string key)
		{
			MyType result = default(MyType);
			Type typeFromHandle = typeof(MyType);
			if (typeFromHandle == typeof(int))
			{
				result = (MyType)((object)this.ParseIntFromPropList(key));
			}
			else if (typeFromHandle == typeof(string))
			{
				result = (MyType)((object)this.ParseStringFromPropList(key));
			}
			else
			{
				if (!(typeFromHandle == typeof(string[])))
				{
					throw new ClusterApiException("GetCommonProperty", new NotImplementedException(string.Format("Unknown type: {0}", typeof(MyType))));
				}
				result = (MyType)((object)this.ParseMultipleStringsFromPropList(key));
			}
			return result;
		}
	}
}
