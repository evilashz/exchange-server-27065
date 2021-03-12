using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000929 RID: 2345
	[SecurityCritical]
	[__DynamicallyInvokable]
	public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060060A6 RID: 24742 RVA: 0x00149B91 File Offset: 0x00147D91
		[__DynamicallyInvokable]
		protected SafeBuffer(bool ownsHandle) : base(ownsHandle)
		{
			this._numBytes = SafeBuffer.Uninitialized;
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x00149BA8 File Offset: 0x00147DA8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public void Initialize(ulong numBytes)
		{
			if (numBytes < 0UL)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (IntPtr.Size == 4 && numBytes > (ulong)-1)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
			}
			if (numBytes >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
			}
			this._numBytes = (UIntPtr)numBytes;
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x00149C20 File Offset: 0x00147E20
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public void Initialize(uint numElements, uint sizeOfEachElement)
		{
			if (numElements < 0U)
			{
				throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (sizeOfEachElement < 0U)
			{
				throw new ArgumentOutOfRangeException("sizeOfEachElement", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (IntPtr.Size == 4 && numElements * sizeOfEachElement > 4294967295U)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
			}
			if ((ulong)(numElements * sizeOfEachElement) >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
			}
			this._numBytes = (UIntPtr)(checked(numElements * sizeOfEachElement));
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x00149CB5 File Offset: 0x00147EB5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public void Initialize<T>(uint numElements) where T : struct
		{
			this.Initialize(numElements, Marshal.AlignedSizeOf<T>());
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x00149CC4 File Offset: 0x00147EC4
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			pointer = (IntPtr)((UIntPtr)0);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = false;
				base.DangerousAddRef(ref flag);
				pointer = (void*)this.handle;
			}
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x00149D1C File Offset: 0x00147F1C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void ReleasePointer()
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			base.DangerousRelease();
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x00149D3C File Offset: 0x00147F3C
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe T Read<T>(ulong byteOffset) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = Marshal.SizeOfType(typeof(T));
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			T result;
			try
			{
				base.DangerousAddRef(ref flag);
				SafeBuffer.GenericPtrToStructure<T>(ptr, out result, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x00149DC0 File Offset: 0x00147FC0
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe void ReadArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint sizeofT = Marshal.SizeOfType(typeof(T));
			uint num = Marshal.AlignedSizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, checked(unchecked((ulong)num) * (ulong)(unchecked((long)count))));
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				for (int i = 0; i < count; i++)
				{
					SafeBuffer.GenericPtrToStructure<T>(ptr + (ulong)num * (ulong)((long)i), out array[i + index], sizeofT);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x00149ED4 File Offset: 0x001480D4
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = Marshal.SizeOfType(typeof(T));
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				SafeBuffer.GenericStructureToPtr<T>(ref value, ptr, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x00149F58 File Offset: 0x00148158
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe void WriteArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint sizeofT = Marshal.SizeOfType(typeof(T));
			uint num = Marshal.AlignedSizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, checked(unchecked((ulong)num) * (ulong)(unchecked((long)count))));
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				for (int i = 0; i < count; i++)
				{
					SafeBuffer.GenericStructureToPtr<T>(ref array[i + index], ptr + (ulong)num * (ulong)((long)i), sizeofT);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060060B0 RID: 24752 RVA: 0x0014A06C File Offset: 0x0014826C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public ulong ByteLength
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (this._numBytes == SafeBuffer.Uninitialized)
				{
					throw SafeBuffer.NotInitialized();
				}
				return (ulong)this._numBytes;
			}
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x0014A091 File Offset: 0x00148291
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe void SpaceCheck(byte* ptr, ulong sizeInBytes)
		{
			if ((ulong)this._numBytes < sizeInBytes)
			{
				SafeBuffer.NotEnoughRoom();
			}
			if ((long)((byte*)ptr - (byte*)((void*)this.handle)) > (long)((ulong)this._numBytes - sizeInBytes))
			{
				SafeBuffer.NotEnoughRoom();
			}
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x0014A0CA File Offset: 0x001482CA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void NotEnoughRoom()
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x0014A0DB File Offset: 0x001482DB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static InvalidOperationException NotInitialized()
		{
			return new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustCallInitialize"));
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x0014A0EC File Offset: 0x001482EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void GenericPtrToStructure<T>(byte* ptr, out T structure, uint sizeofT) where T : struct
		{
			structure = default(T);
			SafeBuffer.PtrToStructureNative(ptr, __makeref(structure), sizeofT);
		}

		// Token: 0x060060B5 RID: 24757
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void PtrToStructureNative(byte* ptr, TypedReference structure, uint sizeofT);

		// Token: 0x060060B6 RID: 24758 RVA: 0x0014A102 File Offset: 0x00148302
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void GenericStructureToPtr<T>(ref T structure, byte* ptr, uint sizeofT) where T : struct
		{
			SafeBuffer.StructureToPtrNative(__makeref(structure), ptr, sizeofT);
		}

		// Token: 0x060060B7 RID: 24759
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void StructureToPtrNative(TypedReference structure, byte* ptr, uint sizeofT);

		// Token: 0x04002AC2 RID: 10946
		private static readonly UIntPtr Uninitialized = (UIntPtr.Size == 4) ? ((UIntPtr)uint.MaxValue) : ((UIntPtr)ulong.MaxValue);

		// Token: 0x04002AC3 RID: 10947
		private UIntPtr _numBytes;
	}
}
