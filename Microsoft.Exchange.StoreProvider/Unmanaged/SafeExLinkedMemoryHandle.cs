using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C0 RID: 704
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExLinkedMemoryHandle : SafeExMemoryHandle
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x000349EF File Offset: 0x00032BEF
		internal SafeExLinkedMemoryHandle()
		{
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000349F7 File Offset: 0x00032BF7
		internal SafeExLinkedMemoryHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00034A00 File Offset: 0x00032C00
		public PropTag[] ReadPropTagArray()
		{
			if (this.IsInvalid)
			{
				return Array<PropTag>.Empty;
			}
			int size = Marshal.ReadInt32(this.handle, 0);
			PropTag[] array = Array<PropTag>.New(size);
			int num = 4;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (PropTag)Marshal.ReadInt32(this.handle, num);
				num += 4;
			}
			return array;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00034A54 File Offset: 0x00032C54
		public unsafe PropProblem[] ReadPropProblemArray()
		{
			if (this.IsInvalid)
			{
				return null;
			}
			SPropProblemArray* ptr = (SPropProblemArray*)this.handle.ToPointer();
			PropProblem[] array = null;
			if (ptr->cProblem > 0)
			{
				SPropProblem* ptr2 = &ptr->aProblem;
				array = new PropProblem[ptr->cProblem];
				int i = 0;
				while (i < array.Length)
				{
					array[i] = new PropProblem(ptr2);
					i++;
					ptr2++;
				}
			}
			return array;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00034AC0 File Offset: 0x00032CC0
		public int[] ReadInt32Array()
		{
			if (this.IsInvalid)
			{
				return null;
			}
			int size = Marshal.ReadInt32(this.handle, 0);
			int[] array = Array<int>.New(size);
			int num = 4;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Marshal.ReadInt32(this.handle, num);
				num += 4;
			}
			return array;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00034B10 File Offset: 0x00032D10
		public unsafe void CopyTo(NamedProp[] destination, int startIndex, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("Handle is invalid");
			}
			SNameId** ptr = (SNameId**)this.handle.ToPointer();
			for (int i = 0; i < length; i++)
			{
				if (*(IntPtr*)(ptr + (IntPtr)i * (IntPtr)sizeof(SNameId*) / (IntPtr)sizeof(SNameId*)) != (IntPtr)((UIntPtr)0))
				{
					destination[startIndex + i] = NamedProp.MarshalFromNative(*(IntPtr*)(ptr + (IntPtr)i * (IntPtr)sizeof(SNameId*) / (IntPtr)sizeof(SNameId*)));
				}
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00034B8A File Offset: 0x00032D8A
		public unsafe FastTransferBlock[] ReadFastTransferBlockArray(int cBlocks)
		{
			return base.ReadObjectArray<FastTransferBlock>(cBlocks, (SafeExMemoryHandle handle, IntPtr pointer, int index) => new FastTransferBlock((FxBlock*)((byte*)pointer.ToPointer() + (IntPtr)index * (IntPtr)sizeof(FxBlock))));
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00034BB0 File Offset: 0x00032DB0
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExLinkedMemoryHandle>(this);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00034BB8 File Offset: 0x00032DB8
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				SafeExLinkedMemoryHandle.FreeLinkedFnEx(this.handle);
			}
			return true;
		}

		// Token: 0x06000D43 RID: 3395
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern void FreeLinkedFnEx(IntPtr lpBuffer);
	}
}
