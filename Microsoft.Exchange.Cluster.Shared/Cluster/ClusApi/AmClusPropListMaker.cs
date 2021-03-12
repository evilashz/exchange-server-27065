using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000032 RID: 50
	internal static class AmClusPropListMaker
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00009394 File Offset: 0x00007594
		public static AmClusterPropListDisposable CreatePropListString(string name, string value, out int bufferSize)
		{
			int num = 4 + AmClusPropListMaker.ClusPropValueSize(name) + AmClusPropListMaker.ClusPropValueSize(value) + 4;
			int num2 = AmClusPropListMaker.PaddingSize(num, 4);
			bufferSize = num + num2;
			IntPtr intPtr = Marshal.AllocHGlobal(bufferSize);
			string.Format("buffer is alloc'ed 0x{0:x} at 0x{1:x}", bufferSize, intPtr.ToInt64());
			int num3 = 0;
			num3 += AmClusPropListMaker.WriteHeader(intPtr, num3, 1);
			num3 += AmClusPropListMaker.WriteStringValue(intPtr, num3, 262147, name);
			num3 += AmClusPropListMaker.WriteStringValue(intPtr, num3, 65539, value);
			num3 += AmClusPropListMaker.WriteTerminator(intPtr, num3);
			ClusapiMethods.ResUtilVerifyPrivatePropertyList(intPtr, bufferSize);
			return new AmClusterPropListDisposable(new SafeHGlobalHandle(intPtr), (uint)bufferSize);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009434 File Offset: 0x00007634
		public static AmClusterPropListDisposable CreatePropListInt(string name, int value, out int bufferSize)
		{
			int num = 4 + AmClusPropListMaker.ClusPropValueSize(name) + AmClusPropListMaker.ClusPropValueSize(value) + 4;
			int num2 = AmClusPropListMaker.PaddingSize(num, 4);
			bufferSize = num + num2;
			IntPtr intPtr = Marshal.AllocHGlobal(bufferSize);
			string.Format("buffer is alloc'ed 0x{0:x} at 0x{1:x}", bufferSize, intPtr.ToInt64());
			int num3 = 0;
			num3 += AmClusPropListMaker.WriteHeader(intPtr, num3, 1);
			num3 += AmClusPropListMaker.WriteStringValue(intPtr, num3, 262147, name);
			num3 += AmClusPropListMaker.WriteIntValue(intPtr, num3, 65538, value);
			num3 += AmClusPropListMaker.WriteTerminator(intPtr, num3);
			ClusapiMethods.ResUtilVerifyPrivatePropertyList(intPtr, bufferSize);
			return new AmClusterPropListDisposable(new SafeHGlobalHandle(intPtr), (uint)bufferSize);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000094D4 File Offset: 0x000076D4
		public static AmClusterPropListDisposable CreatePropListMultiString(string name, string[] value, out int bufferSize)
		{
			int num = 4 + AmClusPropListMaker.ClusPropValueSize(name) + AmClusPropListMaker.ClusPropValueSize(value) + 4;
			int num2 = AmClusPropListMaker.PaddingSize(num, 4);
			bufferSize = num + num2;
			IntPtr intPtr = Marshal.AllocHGlobal(bufferSize);
			string.Format("buffer is alloc'ed 0x{0:x} at 0x{1:x}", bufferSize, intPtr.ToInt64());
			int num3 = 0;
			num3 += AmClusPropListMaker.WriteHeader(intPtr, num3, 1);
			num3 += AmClusPropListMaker.WriteStringValue(intPtr, num3, 262147, name);
			num3 += AmClusPropListMaker.WriteMultiStringValue(intPtr, num3, 65541, value);
			num3 += AmClusPropListMaker.WriteTerminator(intPtr, num3);
			ClusapiMethods.ResUtilVerifyPrivatePropertyList(intPtr, bufferSize);
			throw new NotImplementedException("CreatePropListMultiString entire function may work, but has not been tested properly.");
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009570 File Offset: 0x00007770
		public static AmClusterPropListDisposable DupeAndAppendPropListString(IntPtr pvCurrentList, int cbCurrentList, string name, string value, out int bufferSize)
		{
			int num = cbCurrentList + AmClusPropListMaker.ClusPropValueSize(name) + AmClusPropListMaker.ClusPropValueSize(value) + 4;
			int num2 = AmClusPropListMaker.PaddingSize(num, 4);
			bufferSize = num + num2;
			IntPtr intPtr = Marshal.AllocHGlobal(bufferSize);
			string.Format("buffer is alloc'ed 0x{0:x} at 0x{1:x}", bufferSize, intPtr.ToInt64());
			int num3 = 0;
			ClusapiMethods.memcpy(intPtr, pvCurrentList, cbCurrentList);
			int num4 = Marshal.ReadInt32(pvCurrentList);
			num3 += AmClusPropListMaker.WriteHeader(intPtr, num3, num4 + 1);
			num3 = cbCurrentList + AmClusPropListMaker.WriteStringValue(intPtr, cbCurrentList, 262147, name);
			num3 += AmClusPropListMaker.WriteStringValue(intPtr, num3, 65539, value);
			num3 += AmClusPropListMaker.WriteTerminator(intPtr, num3);
			ClusapiMethods.ResUtilVerifyPrivatePropertyList(intPtr, bufferSize);
			return new AmClusterPropListDisposable(new SafeHGlobalHandle(intPtr), (uint)bufferSize);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000962A File Offset: 0x0000782A
		private static int WriteHeader(IntPtr buffer, int offset, int numberOfProperties)
		{
			Marshal.WriteInt32(buffer, offset, numberOfProperties);
			return 4;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009635 File Offset: 0x00007835
		private static int WriteTerminator(IntPtr buffer, int offset)
		{
			Marshal.WriteInt32(buffer, offset, 0);
			return 4;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009640 File Offset: 0x00007840
		private static int WriteStringValue(IntPtr buffer, int offset, int syntax, string data)
		{
			int num = 0;
			Marshal.WriteInt32(buffer, offset + num, syntax);
			num += 4;
			int num2 = (data.Length + 1) * 2;
			Marshal.WriteInt32(buffer, offset + num, num2);
			num += 4;
			IntPtr destination = (IntPtr)(buffer.ToInt64() + (long)offset + (long)num);
			Marshal.Copy(data.ToCharArray(), 0, destination, data.Length);
			num += data.Length * 2;
			Marshal.WriteInt16(buffer, offset + num, 0);
			num += 2;
			int num3 = AmClusPropListMaker.PaddingSize(num2, 4);
			for (int i = 0; i < num3; i++)
			{
				Marshal.WriteByte(buffer, offset + num, 0);
				num++;
			}
			return num;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000096DC File Offset: 0x000078DC
		private static int WriteMultiStringValue(IntPtr buffer, int offset, int syntax, string[] values)
		{
			int num = 0;
			Marshal.WriteInt32(buffer, offset + num, syntax);
			num += 4;
			int num2 = 0;
			foreach (string text in values)
			{
				num2 += (text.Length + 1) * 2;
			}
			num2 += 2;
			Marshal.WriteInt32(buffer, offset + num, num2);
			num += 4;
			foreach (string text2 in values)
			{
				IntPtr destination = (IntPtr)(buffer.ToInt64() + (long)offset + (long)num);
				Marshal.Copy(text2.ToCharArray(), 0, destination, text2.Length);
				num += text2.Length * 2;
				Marshal.WriteInt16(buffer, offset + num, 0);
				num += 2;
			}
			Marshal.WriteInt16(buffer, offset + num, 0);
			num += 2;
			int num3 = AmClusPropListMaker.PaddingSize(num2, 4);
			for (int k = 0; k < num3; k++)
			{
				Marshal.WriteByte(buffer, offset + num, 0);
				num++;
			}
			return num;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000097CC File Offset: 0x000079CC
		private static int WriteIntValue(IntPtr buffer, int offset, int syntax, int data)
		{
			int num = 0;
			Marshal.WriteInt32(buffer, offset + num, syntax);
			num += 4;
			int val = 4;
			Marshal.WriteInt32(buffer, offset + num, val);
			num += 4;
			Marshal.WriteInt32(buffer, offset + num, data);
			return num + 4;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009808 File Offset: 0x00007A08
		private static int ClusPropValueSize(string data)
		{
			int num = 8;
			int num2 = (data.Length + 1) * 2;
			return num + num2;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009828 File Offset: 0x00007A28
		private static int ClusPropValueSize(string[] data)
		{
			int num = 8;
			int num2 = 0;
			foreach (string text in data)
			{
				num2 += (text.Length + 1) * 2;
			}
			num2 += 2;
			return num + num2;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009868 File Offset: 0x00007A68
		private static int ClusPropValueSize(int data)
		{
			int num = 8;
			int num2 = 4;
			return num + num2;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000987C File Offset: 0x00007A7C
		private static int PaddingSize(int dataBytes, int desiredAlignment)
		{
			return desiredAlignment - ((dataBytes - 1) % desiredAlignment + 1);
		}

		// Token: 0x04000088 RID: 136
		private const int ClusPropListSize = 4;

		// Token: 0x04000089 RID: 137
		private const int ClusPropSyntaxSize = 4;

		// Token: 0x0400008A RID: 138
		private const int CLUSPROP_SYNTAX_NAME = 262147;

		// Token: 0x0400008B RID: 139
		private const int CLUSPROP_SYNTAX_LIST_VALUE_SZ = 65539;

		// Token: 0x0400008C RID: 140
		private const int CLUSPROP_SYNTAX_LIST_VALUE_MULTI_SZ = 65541;

		// Token: 0x0400008D RID: 141
		private const int CLUSPROP_SYNTAX_LIST_VALUE_DWORD = 65538;

		// Token: 0x0400008E RID: 142
		private const int CLUSPROP_SYNTAX_ENDMARK = 0;
	}
}
