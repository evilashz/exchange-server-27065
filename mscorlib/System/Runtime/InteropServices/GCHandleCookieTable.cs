using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091C RID: 2332
	internal class GCHandleCookieTable
	{
		// Token: 0x06005F80 RID: 24448 RVA: 0x00147AA8 File Offset: 0x00145CA8
		internal GCHandleCookieTable()
		{
			this.m_HandleList = new IntPtr[10];
			this.m_CycleCounts = new byte[10];
			this.m_HandleToCookieMap = new Dictionary<IntPtr, IntPtr>(10);
			this.m_syncObject = new object();
			for (int i = 0; i < 10; i++)
			{
				this.m_HandleList[i] = IntPtr.Zero;
				this.m_CycleCounts[i] = 0;
			}
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x00147B18 File Offset: 0x00145D18
		internal IntPtr FindOrAddHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = IntPtr.Zero;
			object syncObject = this.m_syncObject;
			lock (syncObject)
			{
				if (this.m_HandleToCookieMap.ContainsKey(handle))
				{
					return this.m_HandleToCookieMap[handle];
				}
				if (this.m_FreeIndex < this.m_HandleList.Length && Volatile.Read(ref this.m_HandleList[this.m_FreeIndex]) == IntPtr.Zero)
				{
					Volatile.Write(ref this.m_HandleList[this.m_FreeIndex], handle);
					intPtr = this.GetCookieFromData((uint)this.m_FreeIndex, this.m_CycleCounts[this.m_FreeIndex]);
					this.m_FreeIndex++;
				}
				else
				{
					this.m_FreeIndex = 0;
					while (this.m_FreeIndex < 16777215)
					{
						if (this.m_HandleList[this.m_FreeIndex] == IntPtr.Zero)
						{
							Volatile.Write(ref this.m_HandleList[this.m_FreeIndex], handle);
							intPtr = this.GetCookieFromData((uint)this.m_FreeIndex, this.m_CycleCounts[this.m_FreeIndex]);
							this.m_FreeIndex++;
							break;
						}
						if (this.m_FreeIndex + 1 == this.m_HandleList.Length)
						{
							this.GrowArrays();
						}
						this.m_FreeIndex++;
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException(Environment.GetResourceString("OutOfMemory_GCHandleMDA"));
				}
				this.m_HandleToCookieMap.Add(handle, intPtr);
			}
			return intPtr;
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x00147CE8 File Offset: 0x00145EE8
		internal IntPtr GetHandle(IntPtr cookie)
		{
			IntPtr zero = IntPtr.Zero;
			if (!this.ValidateCookie(cookie))
			{
				return IntPtr.Zero;
			}
			return Volatile.Read(ref this.m_HandleList[this.GetIndexFromCookie(cookie)]);
		}

		// Token: 0x06005F83 RID: 24451 RVA: 0x00147D28 File Offset: 0x00145F28
		internal void RemoveHandleIfPresent(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return;
			}
			object syncObject = this.m_syncObject;
			lock (syncObject)
			{
				if (this.m_HandleToCookieMap.ContainsKey(handle))
				{
					IntPtr cookie = this.m_HandleToCookieMap[handle];
					if (this.ValidateCookie(cookie))
					{
						int indexFromCookie = this.GetIndexFromCookie(cookie);
						byte[] cycleCounts = this.m_CycleCounts;
						int num = indexFromCookie;
						cycleCounts[num] += 1;
						Volatile.Write(ref this.m_HandleList[indexFromCookie], IntPtr.Zero);
						this.m_HandleToCookieMap.Remove(handle);
						this.m_FreeIndex = indexFromCookie;
					}
				}
			}
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x00147DE0 File Offset: 0x00145FE0
		private bool ValidateCookie(IntPtr cookie)
		{
			int num;
			byte b;
			this.GetDataFromCookie(cookie, out num, out b);
			if (num >= 16777215)
			{
				return false;
			}
			if (num >= this.m_HandleList.Length)
			{
				return false;
			}
			if (Volatile.Read(ref this.m_HandleList[num]) == IntPtr.Zero)
			{
				return false;
			}
			byte b2 = (byte)(AppDomain.CurrentDomain.Id % 255);
			byte b3 = Volatile.Read(ref this.m_CycleCounts[num]) ^ b2;
			return b == b3;
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x00147E64 File Offset: 0x00146064
		private void GrowArrays()
		{
			int num = this.m_HandleList.Length;
			IntPtr[] array = new IntPtr[num * 2];
			byte[] array2 = new byte[num * 2];
			Array.Copy(this.m_HandleList, array, num);
			Array.Copy(this.m_CycleCounts, array2, num);
			this.m_HandleList = array;
			this.m_CycleCounts = array2;
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x00147EC0 File Offset: 0x001460C0
		private IntPtr GetCookieFromData(uint index, byte cycleCount)
		{
			byte b = (byte)(AppDomain.CurrentDomain.Id % 255);
			return (IntPtr)((long)((long)(cycleCount ^ b) << 24) + (long)((ulong)index) + 1L);
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x00147EF4 File Offset: 0x001460F4
		private void GetDataFromCookie(IntPtr cookie, out int index, out byte xorData)
		{
			uint num = (uint)((int)cookie);
			index = (int)((num & 16777215U) - 1U);
			xorData = (byte)((num & 4278190080U) >> 24);
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x00147F20 File Offset: 0x00146120
		private int GetIndexFromCookie(IntPtr cookie)
		{
			uint num = (uint)((int)cookie);
			return (int)((num & 16777215U) - 1U);
		}

		// Token: 0x04002A8E RID: 10894
		private const int InitialHandleCount = 10;

		// Token: 0x04002A8F RID: 10895
		private const int MaxListSize = 16777215;

		// Token: 0x04002A90 RID: 10896
		private const uint CookieMaskIndex = 16777215U;

		// Token: 0x04002A91 RID: 10897
		private const uint CookieMaskSentinal = 4278190080U;

		// Token: 0x04002A92 RID: 10898
		private Dictionary<IntPtr, IntPtr> m_HandleToCookieMap;

		// Token: 0x04002A93 RID: 10899
		private volatile IntPtr[] m_HandleList;

		// Token: 0x04002A94 RID: 10900
		private volatile byte[] m_CycleCounts;

		// Token: 0x04002A95 RID: 10901
		private int m_FreeIndex;

		// Token: 0x04002A96 RID: 10902
		private readonly object m_syncObject;
	}
}
