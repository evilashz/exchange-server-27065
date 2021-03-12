using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x02000055 RID: 85
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Array : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00005D04 File Offset: 0x00003F04
		internal Array()
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00005D0C File Offset: 0x00003F0C
		public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new ReadOnlyCollection<T>(array);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00005D24 File Offset: 0x00003F24
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Resize<T>(ref T[] array, int newSize)
		{
			if (newSize < 0)
			{
				throw new ArgumentOutOfRangeException("newSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			T[] array2 = array;
			if (array2 == null)
			{
				array = new T[newSize];
				return;
			}
			if (array2.Length != newSize)
			{
				T[] array3 = new T[newSize];
				Array.Copy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
				array = array3;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00005D80 File Offset: 0x00003F80
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, int length)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 1, &length, null);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00005DFC File Offset: 0x00003FFC
		[SecuritySafeCritical]
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length1 < 0 || length2 < 0)
			{
				throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)2) * 4)];
			*ptr = length1;
			ptr[1] = length2;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 2, ptr, null);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00005E94 File Offset: 0x00004094
		[SecuritySafeCritical]
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length1 < 0)
			{
				throw new ArgumentOutOfRangeException("length1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length2 < 0)
			{
				throw new ArgumentOutOfRangeException("length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length3 < 0)
			{
				throw new ArgumentOutOfRangeException("length3", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)3) * 4)];
			*ptr = length1;
			ptr[1] = length2;
			ptr[2] = length3;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 3, ptr, null);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00005F58 File Offset: 0x00004158
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths[" + i + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
			}
			fixed (int* ptr = lengths)
			{
				return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, ptr, null);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006030 File Offset: 0x00004230
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			int[] array = new int[lengths.Length];
			for (int i = 0; i < lengths.Length; i++)
			{
				long num = lengths[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("len", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000060B0 File Offset: 0x000042B0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lowerBounds == null)
			{
				throw new ArgumentNullException("lowerBounds");
			}
			if (lengths.Length != lowerBounds.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RanksAndBounds"));
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths[" + i + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
			}
			fixed (int* ptr = lengths)
			{
				fixed (int* ptr2 = lowerBounds)
				{
					return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, ptr, ptr2);
				}
			}
		}

		// Token: 0x06000294 RID: 660
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Array InternalCreate(void* elementType, int rank, int* pLengths, int* pLowerBounds);

		// Token: 0x06000295 RID: 661 RVA: 0x000061D0 File Offset: 0x000043D0
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int length)
		{
			return Array.CreateInstance(elementType, length);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000061D9 File Offset: 0x000043D9
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int length1, int length2)
		{
			return Array.CreateInstance(elementType, length1, length2);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000061E3 File Offset: 0x000043E3
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, params int[] lengths)
		{
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000061EC File Offset: 0x000043EC
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			return Array.CreateInstance(elementType, lengths, lowerBounds);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000061F6 File Offset: 0x000043F6
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length, false);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000622B File Offset: 0x0000442B
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, false);
		}

		// Token: 0x0600029B RID: 667
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length, bool reliable);

		// Token: 0x0600029C RID: 668 RVA: 0x00006239 File Offset: 0x00004439
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, true);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00006247 File Offset: 0x00004447
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			Array.Copy(sourceArray, destinationArray, (int)length);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000627C File Offset: 0x0000447C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			if (sourceIndex > 2147483647L || sourceIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (destinationIndex > 2147483647L || destinationIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
		}

		// Token: 0x0600029F RID: 671
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear(Array array, int index, int length);

		// Token: 0x060002A0 RID: 672 RVA: 0x00006310 File Offset: 0x00004510
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe object GetValue(params int[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int* ptr = indices)
			{
				this.InternalGetReference((void*)(&typedReference), indices.Length, ptr);
			}
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006380 File Offset: 0x00004580
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000063C4 File Offset: 0x000045C4
		[SecuritySafeCritical]
		public unsafe object GetValue(int index1, int index2)
		{
			if (this.Rank != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
			}
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)2) * 4)];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00006414 File Offset: 0x00004614
		[SecuritySafeCritical]
		public unsafe object GetValue(int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
			}
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)3) * 4)];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000646C File Offset: 0x0000466C
		[ComVisible(false)]
		public object GetValue(long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000064A0 File Offset: 0x000046A0
		[ComVisible(false)]
		public object GetValue(long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index1, (int)index2);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00006508 File Offset: 0x00004708
		[ComVisible(false)]
		public object GetValue(long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index1, (int)index2, (int)index3);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006598 File Offset: 0x00004798
		[ComVisible(false)]
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			return this.GetValue(array);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000661C File Offset: 0x0000481C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006660 File Offset: 0x00004860
		[SecuritySafeCritical]
		public unsafe void SetValue(object value, int index1, int index2)
		{
			if (this.Rank != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
			}
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)2) * 4)];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000066B4 File Offset: 0x000048B4
		[SecuritySafeCritical]
		public unsafe void SetValue(object value, int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
			}
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)3) * 4)];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006710 File Offset: 0x00004910
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void SetValue(object value, params int[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int* ptr = indices)
			{
				this.InternalGetReference((void*)(&typedReference), indices.Length, ptr);
			}
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000677F File Offset: 0x0000497F
		[ComVisible(false)]
		public void SetValue(object value, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000067B4 File Offset: 0x000049B4
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index1, (int)index2);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000681C File Offset: 0x00004A1C
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index1, (int)index2, (int)index3);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000068B0 File Offset: 0x00004AB0
		[ComVisible(false)]
		public void SetValue(object value, params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			this.SetValue(value, array);
		}

		// Token: 0x060002B0 RID: 688
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void InternalGetReference(void* elemRef, int rank, int* pIndices);

		// Token: 0x060002B1 RID: 689
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalSetValue(void* target, object value);

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002B2 RID: 690
		[__DynamicallyInvokable]
		public extern int Length { [SecuritySafeCritical] [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] [__DynamicallyInvokable] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060002B3 RID: 691 RVA: 0x00006935 File Offset: 0x00004B35
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002B4 RID: 692
		[ComVisible(false)]
		public extern long LongLength { [SecuritySafeCritical] [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060002B5 RID: 693
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLength(int dimension);

		// Token: 0x060002B6 RID: 694 RVA: 0x0000693E File Offset: 0x00004B3E
		[ComVisible(false)]
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002B7 RID: 695
		[__DynamicallyInvokable]
		public extern int Rank { [SecuritySafeCritical] [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] [__DynamicallyInvokable] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060002B8 RID: 696
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUpperBound(int dimension);

		// Token: 0x060002B9 RID: 697
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLowerBound(int dimension);

		// Token: 0x060002BA RID: 698
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetDataPtrOffsetInternal();

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00006948 File Offset: 0x00004B48
		[__DynamicallyInvokable]
		int ICollection.Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006950 File Offset: 0x00004B50
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00006953 File Offset: 0x00004B53
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006956 File Offset: 0x00004B56
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00006959 File Offset: 0x00004B59
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000037 RID: 55
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValue(index);
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetValue(value, index);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000696F File Offset: 0x00004B6F
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00006980 File Offset: 0x00004B80
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return Array.IndexOf(this, value) >= this.GetLowerBound(0);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006995 File Offset: 0x00004B95
		[__DynamicallyInvokable]
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000069AA File Offset: 0x00004BAA
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			return Array.IndexOf(this, value);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000069B3 File Offset: 0x00004BB3
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000069C4 File Offset: 0x00004BC4
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000069D5 File Offset: 0x00004BD5
		[__DynamicallyInvokable]
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000069E6 File Offset: 0x00004BE6
		[__DynamicallyInvokable]
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000069F0 File Offset: 0x00004BF0
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Array array = other as Array;
			if (array == null || this.Length != array.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_OtherNotArrayOfCorrectLength"), "other");
			}
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 == 0)
			{
				object value = this.GetValue(num);
				object value2 = array.GetValue(num);
				num2 = comparer.Compare(value, value2);
				num++;
			}
			return num2;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00006A64 File Offset: 0x00004C64
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Array array = other as Array;
			if (array == null || array.Length != this.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.GetValue(i);
				object value2 = array.GetValue(i);
				if (!comparer.Equals(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006AC4 File Offset: 0x00004CC4
		internal static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00006AD0 File Offset: 0x00004CD0
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			for (int i = (this.Length >= 8) ? (this.Length - 8) : 0; i < this.Length; i++)
			{
				num = Array.CombineHashCodes(num, comparer.GetHashCode(this.GetValue(i)));
			}
			return num;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00006B28 File Offset: 0x00004D28
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.BinarySearch(array, lowerBound, array.Length, value, null);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00006B5A File Offset: 0x00004D5A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, int index, int length, object value)
		{
			return Array.BinarySearch(array, index, length, value, null);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00006B68 File Offset: 0x00004D68
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.BinarySearch(array, lowerBound, array.Length, value, comparer);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00006B9C File Offset: 0x00004D9C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < lowerBound) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - (index - lowerBound) < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			if (comparer == Comparer.Default)
			{
				int result;
				bool flag = Array.TrySZBinarySearch(array, index, length, value, out result);
				if (flag)
				{
					return result;
				}
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i <= num)
				{
					int median = Array.GetMedian(i, num);
					int num2;
					try
					{
						num2 = comparer.Compare(array2[median], value);
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
					}
					if (num2 == 0)
					{
						return median;
					}
					if (num2 < 0)
					{
						i = median + 1;
					}
					else
					{
						num = median - 1;
					}
				}
			}
			else
			{
				while (i <= num)
				{
					int median2 = Array.GetMedian(i, num);
					int num3;
					try
					{
						num3 = comparer.Compare(array.GetValue(median2), value);
					}
					catch (Exception innerException2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException2);
					}
					if (num3 == 0)
					{
						return median2;
					}
					if (num3 < 0)
					{
						i = median2 + 1;
					}
					else
					{
						num = median2 - 1;
					}
				}
			}
			return ~i;
		}

		// Token: 0x060002D2 RID: 722
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZBinarySearch(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		// Token: 0x060002D3 RID: 723 RVA: 0x00006D14 File Offset: 0x00004F14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00006D30 File Offset: 0x00004F30
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00006D4C File Offset: 0x00004F4C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006D58 File Offset: 0x00004F58
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return ArraySortHelper<T>.Default.BinarySearch(array, index, length, value, comparer);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006E19 File Offset: 0x00005019
		[__DynamicallyInvokable]
		public void CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00006E4C File Offset: 0x0000504C
		[ComVisible(false)]
		public void CopyTo(Array array, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.CopyTo(array, (int)index);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006E7E File Offset: 0x0000507E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static T[] Empty<T>()
		{
			return EmptyArray<T>.Value;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00006E85 File Offset: 0x00005085
		[__DynamicallyInvokable]
		public static bool Exists<T>(T[] array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array, match) != -1;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006E94 File Offset: 0x00005094
		[__DynamicallyInvokable]
		public static T Find<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00006EEC File Offset: 0x000050EC
		[__DynamicallyInvokable]
		public static T[] FindAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> list = new List<T>();
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					list.Add(array[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006F4B File Offset: 0x0000514B
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006F66 File Offset: 0x00005166
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006F84 File Offset: 0x00005184
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00007010 File Offset: 0x00005210
		[__DynamicallyInvokable]
		public static T FindLast<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00007069 File Offset: 0x00005269
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, array.Length - 1, array.Length, match);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007088 File Offset: 0x00005288
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, startIndex, startIndex + 1, match);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000070A4 File Offset: 0x000052A4
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
			}
			else if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000714C File Offset: 0x0000534C
		public static void ForEach<T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007190 File Offset: 0x00005390
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			int lowerBound = this.GetLowerBound(0);
			if (this.Rank == 1 && lowerBound == 0)
			{
				return new Array.SZArrayEnumerator(this);
			}
			return new Array.ArrayEnumerator(this, lowerBound, this.Length);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000071C8 File Offset: 0x000053C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, lowerBound, array.Length);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000071FC File Offset: 0x000053FC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00007234 File Offset: 0x00005434
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			int lowerBound = array.GetLowerBound(0);
			if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > array.Length - startIndex + lowerBound)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			int result;
			bool flag = Array.TrySZIndexOf(array, startIndex, count, value, out result);
			if (flag)
			{
				return result;
			}
			object[] array2 = array as object[];
			int num = startIndex + count;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < num; j++)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k < num; k++)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00007358 File Offset: 0x00005558
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, 0, array.Length);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00007373 File Offset: 0x00005573
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007390 File Offset: 0x00005590
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > array.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
		}

		// Token: 0x060002ED RID: 749
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		// Token: 0x060002EE RID: 750 RVA: 0x000073FC File Offset: 0x000055FC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, array.Length - 1 + lowerBound, array.Length);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00007438 File Offset: 0x00005638
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, startIndex, startIndex + 1 - lowerBound);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00007468 File Offset: 0x00005668
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (array.Length == 0)
			{
				return lowerBound - 1;
			}
			if (startIndex < lowerBound || startIndex >= array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (count > startIndex - lowerBound + 1)
			{
				throw new ArgumentOutOfRangeException("endIndex", Environment.GetResourceString("ArgumentOutOfRange_EndIndexStartIndex"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			int result;
			bool flag = Array.TrySZLastIndexOf(array, startIndex, count, value, out result);
			if (flag)
			{
				return result;
			}
			object[] array2 = array as object[];
			int num = startIndex - count + 1;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j >= num; j--)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k >= num; k--)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000075AA File Offset: 0x000057AA
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000075C9 File Offset: 0x000057C9
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000075EC File Offset: 0x000057EC
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				return -1;
			}
			else
			{
				if (startIndex < 0 || startIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
			}
		}

		// Token: 0x060002F4 RID: 756
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZLastIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		// Token: 0x060002F5 RID: 757 RVA: 0x0000768F File Offset: 0x0000588F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse(array, array.GetLowerBound(0), array.Length);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000076B4 File Offset: 0x000058B4
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < array.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - (index - array.GetLowerBound(0)) < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			bool flag = Array.TrySZReverse(array, index, length);
			if (flag)
			{
				return;
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < num)
				{
					object obj = array2[i];
					array2[i] = array2[num];
					array2[num] = obj;
					i++;
					num--;
				}
				return;
			}
			while (i < num)
			{
				object value = array.GetValue(i);
				array.SetValue(array.GetValue(num), i);
				array.SetValue(value, num);
				i++;
				num--;
			}
		}

		// Token: 0x060002F7 RID: 759
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZReverse(Array array, int index, int count);

		// Token: 0x060002F8 RID: 760 RVA: 0x000077A3 File Offset: 0x000059A3
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, null);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000077C8 File Offset: 0x000059C8
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, null);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000077ED File Offset: 0x000059ED
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000077F9 File Offset: 0x000059F9
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00007805 File Offset: 0x00005A05
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, comparer);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000782A File Offset: 0x00005A2A
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000784F File Offset: 0x00005A4F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, int index, int length, IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000785C File Offset: 0x00005A5C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank != 1 || (items != null && items.Rank != 1))
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (items != null && keys.GetLowerBound(0) != items.GetLowerBound(0))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_LowerBoundsMustMatch"));
			}
			if (index < keys.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (keys.Length - (index - keys.GetLowerBound(0)) < length || (items != null && index - items.GetLowerBound(0) > items.Length - length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if (comparer == Comparer.Default || comparer == null)
				{
					bool flag = Array.TrySZSort(keys, items, index, index + length - 1);
					if (flag)
					{
						return;
					}
				}
				object[] array = keys as object[];
				object[] array2 = null;
				if (array != null)
				{
					array2 = (items as object[]);
				}
				if (array != null && (items == null || array2 != null))
				{
					Array.SorterObjectArray sorterObjectArray = new Array.SorterObjectArray(array, array2, comparer);
					sorterObjectArray.Sort(index, length);
					return;
				}
				Array.SorterGenericArray sorterGenericArray = new Array.SorterGenericArray(keys, items, comparer);
				sorterGenericArray.Sort(index, length);
			}
		}

		// Token: 0x06000300 RID: 768
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZSort(Array keys, Array items, int left, int right);

		// Token: 0x06000301 RID: 769 RVA: 0x0000798E File Offset: 0x00005B8E
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, array.GetLowerBound(0), array.Length, null);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000079AF File Offset: 0x00005BAF
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000079CB File Offset: 0x00005BCB
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, int index, int length)
		{
			Array.Sort<T>(array, index, length, null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000079D6 File Offset: 0x00005BD6
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000079E2 File Offset: 0x00005BE2
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, comparer);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000079FD File Offset: 0x00005BFD
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007A1C File Offset: 0x00005C1C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if ((comparer == null || comparer == Comparer<T>.Default) && Array.TrySZSort(array, null, index, index + length - 1))
				{
					return;
				}
				ArraySortHelper<T>.Default.Sort(array, index, length, comparer);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007AA4 File Offset: 0x00005CA4
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if ((comparer == null || comparer == Comparer<TKey>.Default) && Array.TrySZSort(keys, items, index, index + length - 1))
				{
					return;
				}
				if (items == null)
				{
					Array.Sort<TKey>(keys, index, length, comparer);
					return;
				}
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, index, length, comparer);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007B4C File Offset: 0x00005D4C
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
			Array.Sort<T>(array, comparer);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007B84 File Offset: 0x00005D84
		[__DynamicallyInvokable]
		public static bool TrueForAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600030B RID: 779
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Initialize();

		// Token: 0x040001EE RID: 494
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x040001EF RID: 495
		internal const int MaxByteArrayLength = 2147483591;

		// Token: 0x02000A93 RID: 2707
		internal sealed class FunctorComparer<T> : IComparer<T>
		{
			// Token: 0x0600687F RID: 26751 RVA: 0x00166DFC File Offset: 0x00164FFC
			public FunctorComparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x06006880 RID: 26752 RVA: 0x00166E0B File Offset: 0x0016500B
			public int Compare(T x, T y)
			{
				return this.comparison(x, y);
			}

			// Token: 0x04002FF1 RID: 12273
			private Comparison<T> comparison;
		}

		// Token: 0x02000A94 RID: 2708
		private struct SorterObjectArray
		{
			// Token: 0x06006881 RID: 26753 RVA: 0x00166E1A File Offset: 0x0016501A
			internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x06006882 RID: 26754 RVA: 0x00166E3C File Offset: 0x0016503C
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
				{
					object obj = this.keys[a];
					this.keys[a] = this.keys[b];
					this.keys[b] = obj;
					if (this.items != null)
					{
						object obj2 = this.items[a];
						this.items[a] = this.items[b];
						this.items[b] = obj2;
					}
				}
			}

			// Token: 0x06006883 RID: 26755 RVA: 0x00166EB8 File Offset: 0x001650B8
			private void Swap(int i, int j)
			{
				object obj = this.keys[i];
				this.keys[i] = this.keys[j];
				this.keys[j] = obj;
				if (this.items != null)
				{
					object obj2 = this.items[i];
					this.items[i] = this.items[j];
					this.items[j] = obj2;
				}
			}

			// Token: 0x06006884 RID: 26756 RVA: 0x00166F11 File Offset: 0x00165111
			internal void Sort(int left, int length)
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					this.IntrospectiveSort(left, length);
					return;
				}
				this.DepthLimitedQuickSort(left, length + left - 1, 32);
			}

			// Token: 0x06006885 RID: 26757 RVA: 0x00166F34 File Offset: 0x00165134
			private void DepthLimitedQuickSort(int left, int right, int depthLimit)
			{
				do
				{
					if (depthLimit == 0)
					{
						try
						{
							this.Heapsort(left, right);
							break;
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[]
							{
								this.comparer
							}));
						}
						catch (Exception innerException)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
						}
					}
					int num = left;
					int num2 = right;
					int median = Array.GetMedian(num, num2);
					try
					{
						this.SwapIfGreaterWithItems(num, median);
						this.SwapIfGreaterWithItems(num, num2);
						this.SwapIfGreaterWithItems(median, num2);
					}
					catch (Exception innerException2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException2);
					}
					object obj = this.keys[median];
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys[num], obj) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(obj, this.keys[num2]) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[]
							{
								this.comparer
							}));
						}
						catch (Exception innerException3)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException3);
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object obj2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = obj2;
							if (this.items != null)
							{
								object obj3 = this.items[num];
								this.items[num] = this.items[num2];
								this.items[num2] = obj3;
							}
						}
						num++;
						num2--;
					}
					while (num <= num2);
					depthLimit--;
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.DepthLimitedQuickSort(left, num2, depthLimit);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.DepthLimitedQuickSort(num, right, depthLimit);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x06006886 RID: 26758 RVA: 0x00167118 File Offset: 0x00165318
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
				}
			}

			// Token: 0x06006887 RID: 26759 RVA: 0x00167184 File Offset: 0x00165384
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x06006888 RID: 26760 RVA: 0x00167208 File Offset: 0x00165408
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object obj = this.keys[num];
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys[++i], obj) < 0)
					{
					}
					while (this.comparer.Compare(obj, this.keys[--num2]) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x06006889 RID: 26761 RVA: 0x001672A4 File Offset: 0x001654A4
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x0600688A RID: 26762 RVA: 0x001672F4 File Offset: 0x001654F4
			private void DownHeap(int i, int n, int lo)
			{
				object obj = this.keys[lo + i - 1];
				object obj2 = (this.items != null) ? this.items[lo + i - 1] : null;
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(obj, this.keys[lo + num - 1]) >= 0)
					{
						break;
					}
					this.keys[lo + i - 1] = this.keys[lo + num - 1];
					if (this.items != null)
					{
						this.items[lo + i - 1] = this.items[lo + num - 1];
					}
					i = num;
				}
				this.keys[lo + i - 1] = obj;
				if (this.items != null)
				{
					this.items[lo + i - 1] = obj2;
				}
			}

			// Token: 0x0600688B RID: 26763 RVA: 0x001673DC File Offset: 0x001655DC
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object obj = this.keys[i + 1];
					object obj2 = (this.items != null) ? this.items[i + 1] : null;
					while (num >= lo && this.comparer.Compare(obj, this.keys[num]) < 0)
					{
						this.keys[num + 1] = this.keys[num];
						if (this.items != null)
						{
							this.items[num + 1] = this.items[num];
						}
						num--;
					}
					this.keys[num + 1] = obj;
					if (this.items != null)
					{
						this.items[num + 1] = obj2;
					}
				}
			}

			// Token: 0x04002FF2 RID: 12274
			private object[] keys;

			// Token: 0x04002FF3 RID: 12275
			private object[] items;

			// Token: 0x04002FF4 RID: 12276
			private IComparer comparer;
		}

		// Token: 0x02000A95 RID: 2709
		private struct SorterGenericArray
		{
			// Token: 0x0600688C RID: 26764 RVA: 0x00167489 File Offset: 0x00165689
			internal SorterGenericArray(Array keys, Array items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x0600688D RID: 26765 RVA: 0x001674AC File Offset: 0x001656AC
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) > 0)
				{
					object value = this.keys.GetValue(a);
					this.keys.SetValue(this.keys.GetValue(b), a);
					this.keys.SetValue(value, b);
					if (this.items != null)
					{
						object value2 = this.items.GetValue(a);
						this.items.SetValue(this.items.GetValue(b), a);
						this.items.SetValue(value2, b);
					}
				}
			}

			// Token: 0x0600688E RID: 26766 RVA: 0x00167554 File Offset: 0x00165754
			private void Swap(int i, int j)
			{
				object value = this.keys.GetValue(i);
				this.keys.SetValue(this.keys.GetValue(j), i);
				this.keys.SetValue(value, j);
				if (this.items != null)
				{
					object value2 = this.items.GetValue(i);
					this.items.SetValue(this.items.GetValue(j), i);
					this.items.SetValue(value2, j);
				}
			}

			// Token: 0x0600688F RID: 26767 RVA: 0x001675CD File Offset: 0x001657CD
			internal void Sort(int left, int length)
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					this.IntrospectiveSort(left, length);
					return;
				}
				this.DepthLimitedQuickSort(left, length + left - 1, 32);
			}

			// Token: 0x06006890 RID: 26768 RVA: 0x001675F0 File Offset: 0x001657F0
			private void DepthLimitedQuickSort(int left, int right, int depthLimit)
			{
				do
				{
					if (depthLimit == 0)
					{
						try
						{
							this.Heapsort(left, right);
							break;
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[]
							{
								this.comparer
							}));
						}
						catch (Exception innerException)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
						}
					}
					int num = left;
					int num2 = right;
					int median = Array.GetMedian(num, num2);
					try
					{
						this.SwapIfGreaterWithItems(num, median);
						this.SwapIfGreaterWithItems(num, num2);
						this.SwapIfGreaterWithItems(median, num2);
					}
					catch (Exception innerException2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException2);
					}
					object value = this.keys.GetValue(median);
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys.GetValue(num), value) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(value, this.keys.GetValue(num2)) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[]
							{
								this.comparer
							}));
						}
						catch (Exception innerException3)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException3);
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object value2 = this.keys.GetValue(num);
							this.keys.SetValue(this.keys.GetValue(num2), num);
							this.keys.SetValue(value2, num2);
							if (this.items != null)
							{
								object value3 = this.items.GetValue(num);
								this.items.SetValue(this.items.GetValue(num2), num);
								this.items.SetValue(value3, num2);
							}
						}
						if (num != 2147483647)
						{
							num++;
						}
						if (num2 != -2147483648)
						{
							num2--;
						}
					}
					while (num <= num2);
					depthLimit--;
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.DepthLimitedQuickSort(left, num2, depthLimit);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.DepthLimitedQuickSort(num, right, depthLimit);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x06006891 RID: 26769 RVA: 0x00167814 File Offset: 0x00165A14
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException);
				}
			}

			// Token: 0x06006892 RID: 26770 RVA: 0x00167884 File Offset: 0x00165A84
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x06006893 RID: 26771 RVA: 0x00167908 File Offset: 0x00165B08
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object value = this.keys.GetValue(num);
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys.GetValue(++i), value) < 0)
					{
					}
					while (this.comparer.Compare(value, this.keys.GetValue(--num2)) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x06006894 RID: 26772 RVA: 0x001679B0 File Offset: 0x00165BB0
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x06006895 RID: 26773 RVA: 0x00167A00 File Offset: 0x00165C00
			private void DownHeap(int i, int n, int lo)
			{
				object value = this.keys.GetValue(lo + i - 1);
				object value2 = (this.items != null) ? this.items.GetValue(lo + i - 1) : null;
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(value, this.keys.GetValue(lo + num - 1)) >= 0)
					{
						break;
					}
					this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
					if (this.items != null)
					{
						this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
					}
					i = num;
				}
				this.keys.SetValue(value, lo + i - 1);
				if (this.items != null)
				{
					this.items.SetValue(value2, lo + i - 1);
				}
			}

			// Token: 0x06006896 RID: 26774 RVA: 0x00167B14 File Offset: 0x00165D14
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object value = this.keys.GetValue(i + 1);
					object value2 = (this.items != null) ? this.items.GetValue(i + 1) : null;
					while (num >= lo && this.comparer.Compare(value, this.keys.GetValue(num)) < 0)
					{
						this.keys.SetValue(this.keys.GetValue(num), num + 1);
						if (this.items != null)
						{
							this.items.SetValue(this.items.GetValue(num), num + 1);
						}
						num--;
					}
					this.keys.SetValue(value, num + 1);
					if (this.items != null)
					{
						this.items.SetValue(value2, num + 1);
					}
				}
			}

			// Token: 0x04002FF5 RID: 12277
			private Array keys;

			// Token: 0x04002FF6 RID: 12278
			private Array items;

			// Token: 0x04002FF7 RID: 12279
			private IComparer comparer;
		}

		// Token: 0x02000A96 RID: 2710
		[Serializable]
		private sealed class SZArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006897 RID: 26775 RVA: 0x00167BE5 File Offset: 0x00165DE5
			internal SZArrayEnumerator(Array array)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = array.Length;
			}

			// Token: 0x06006898 RID: 26776 RVA: 0x00167C07 File Offset: 0x00165E07
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006899 RID: 26777 RVA: 0x00167C0F File Offset: 0x00165E0F
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x170011D0 RID: 4560
			// (get) Token: 0x0600689A RID: 26778 RVA: 0x00167C40 File Offset: 0x00165E40
			public object Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array.GetValue(this._index);
				}
			}

			// Token: 0x0600689B RID: 26779 RVA: 0x00167C95 File Offset: 0x00165E95
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x04002FF8 RID: 12280
			private Array _array;

			// Token: 0x04002FF9 RID: 12281
			private int _index;

			// Token: 0x04002FFA RID: 12282
			private int _endIndex;
		}

		// Token: 0x02000A97 RID: 2711
		[Serializable]
		private sealed class ArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600689C RID: 26780 RVA: 0x00167CA0 File Offset: 0x00165EA0
			internal ArrayEnumerator(Array array, int index, int count)
			{
				this.array = array;
				this.index = index - 1;
				this.startIndex = index;
				this.endIndex = index + count;
				this._indices = new int[array.Rank];
				int num = 1;
				for (int i = 0; i < array.Rank; i++)
				{
					this._indices[i] = array.GetLowerBound(i);
					num *= array.GetLength(i);
				}
				this._indices[this._indices.Length - 1]--;
				this._complete = (num == 0);
			}

			// Token: 0x0600689D RID: 26781 RVA: 0x00167D34 File Offset: 0x00165F34
			private void IncArray()
			{
				int rank = this.array.Rank;
				this._indices[rank - 1]++;
				for (int i = rank - 1; i >= 0; i--)
				{
					if (this._indices[i] > this.array.GetUpperBound(i))
					{
						if (i == 0)
						{
							this._complete = true;
							return;
						}
						for (int j = i; j < rank; j++)
						{
							this._indices[j] = this.array.GetLowerBound(j);
						}
						this._indices[i - 1]++;
					}
				}
			}

			// Token: 0x0600689E RID: 26782 RVA: 0x00167DC2 File Offset: 0x00165FC2
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x0600689F RID: 26783 RVA: 0x00167DCA File Offset: 0x00165FCA
			public bool MoveNext()
			{
				if (this._complete)
				{
					this.index = this.endIndex;
					return false;
				}
				this.index++;
				this.IncArray();
				return !this._complete;
			}

			// Token: 0x170011D1 RID: 4561
			// (get) Token: 0x060068A0 RID: 26784 RVA: 0x00167E00 File Offset: 0x00166000
			public object Current
			{
				get
				{
					if (this.index < this.startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._complete)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.array.GetValue(this._indices);
				}
			}

			// Token: 0x060068A1 RID: 26785 RVA: 0x00167E54 File Offset: 0x00166054
			public void Reset()
			{
				this.index = this.startIndex - 1;
				int num = 1;
				for (int i = 0; i < this.array.Rank; i++)
				{
					this._indices[i] = this.array.GetLowerBound(i);
					num *= this.array.GetLength(i);
				}
				this._complete = (num == 0);
				this._indices[this._indices.Length - 1]--;
			}

			// Token: 0x04002FFB RID: 12283
			private Array array;

			// Token: 0x04002FFC RID: 12284
			private int index;

			// Token: 0x04002FFD RID: 12285
			private int endIndex;

			// Token: 0x04002FFE RID: 12286
			private int startIndex;

			// Token: 0x04002FFF RID: 12287
			private int[] _indices;

			// Token: 0x04003000 RID: 12288
			private bool _complete;
		}
	}
}
