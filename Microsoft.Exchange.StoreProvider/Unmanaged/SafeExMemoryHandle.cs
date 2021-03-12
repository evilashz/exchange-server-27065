using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002BF RID: 703
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComVisible(false)]
	internal class SafeExMemoryHandle : DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x0003478C File Offset: 0x0003298C
		protected SafeExMemoryHandle()
		{
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00034794 File Offset: 0x00032994
		internal SafeExMemoryHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003479D File Offset: 0x0003299D
		public void CopyTo(byte[] destination, int startIndex, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Handle is invalid");
			}
			Marshal.Copy(this.handle, destination, startIndex, length);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000347C4 File Offset: 0x000329C4
		public void CopyTo(short[] destination, int startIndex, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Handle is invalid");
			}
			Marshal.Copy(this.handle, destination, startIndex, length);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000347EC File Offset: 0x000329EC
		public void CopyTo(uint[] destination, int startIndex, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Handle is invalid");
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				destination[i] = (uint)Marshal.ReadInt32(this.handle, num);
				num += 4;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00034834 File Offset: 0x00032A34
		public T[] ReadObjectArray<T>(int count, Func<SafeExMemoryHandle, IntPtr, int, T> createObjectDelegate)
		{
			if (this.IsInvalid)
			{
				return null;
			}
			if (count == 0)
			{
				return Array<T>.Empty;
			}
			T[] array = Array<T>.New(count);
			for (int i = 0; i < count; i++)
			{
				array[i] = createObjectDelegate(this, this.handle, i);
			}
			return array;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00034895 File Offset: 0x00032A95
		public unsafe MapiLtidNative[] ReadMapiLtidNativeArray(int cLtids)
		{
			return this.ReadObjectArray<MapiLtidNative>(cLtids, (SafeExMemoryHandle handle, IntPtr pointer, int index) => new MapiLtidNative((NativeLtid*)((byte*)pointer.ToPointer() + (IntPtr)index * (IntPtr)sizeof(NativeLtid))));
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000348D3 File Offset: 0x00032AD3
		public unsafe MapiPUDNative[] ReadMapiPudNativeArray(int cpud)
		{
			return this.ReadObjectArray<MapiPUDNative>(cpud, (SafeExMemoryHandle handle, IntPtr pointer, int index) => new MapiPUDNative((NativePUD*)((byte*)pointer.ToPointer() + (IntPtr)index * (IntPtr)sizeof(NativePUD))));
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000348FC File Offset: 0x00032AFC
		public unsafe void CopyTo(Guid[] destination, int startIndex, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Handle is invalid");
			}
			Guid* ptr = (Guid*)base.DangerousGetHandle().ToPointer();
			for (int i = 0; i < length; i++)
			{
				destination[startIndex + i] = ptr[i];
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003495C File Offset: 0x00032B5C
		public void ForEach<T>(int count, Action<T> action)
		{
			if (count == 0)
			{
				return;
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Handle is invalid");
			}
			IntPtr intPtr = base.DangerousGetHandle();
			long num = (long)Marshal.SizeOf(typeof(T));
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)count))
			{
				T obj = (T)((object)Marshal.PtrToStructure(intPtr, typeof(T)));
				action(obj);
				intPtr = (IntPtr)((long)intPtr + num);
				num2 += 1U;
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000349D1 File Offset: 0x00032BD1
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMemoryHandle>(this);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000349D9 File Offset: 0x00032BD9
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				SafeExMemoryHandle.FreePvFnEx(this.handle);
			}
			return true;
		}

		// Token: 0x06000D37 RID: 3383
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		internal static extern void FreePvFnEx(IntPtr lpBuffer);
	}
}
