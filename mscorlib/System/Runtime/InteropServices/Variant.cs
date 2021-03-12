using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000984 RID: 2436
	[SecurityCritical]
	[StructLayout(LayoutKind.Explicit)]
	internal struct Variant
	{
		// Token: 0x06006218 RID: 25112 RVA: 0x0014D830 File Offset: 0x0014BA30
		internal static bool IsPrimitiveType(VarEnum varEnum)
		{
			switch (varEnum)
			{
			case VarEnum.VT_I2:
			case VarEnum.VT_I4:
			case VarEnum.VT_R4:
			case VarEnum.VT_R8:
			case VarEnum.VT_DATE:
			case VarEnum.VT_BSTR:
			case VarEnum.VT_BOOL:
			case VarEnum.VT_DECIMAL:
			case VarEnum.VT_I1:
			case VarEnum.VT_UI1:
			case VarEnum.VT_UI2:
			case VarEnum.VT_UI4:
			case VarEnum.VT_I8:
			case VarEnum.VT_UI8:
			case VarEnum.VT_INT:
			case VarEnum.VT_UINT:
				return true;
			}
			return false;
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x0014D8A4 File Offset: 0x0014BAA4
		public unsafe void CopyFromIndirect(object value)
		{
			VarEnum varEnum = this.VariantType & (VarEnum)(-16385);
			if (value == null)
			{
				if (varEnum == VarEnum.VT_DISPATCH || varEnum == VarEnum.VT_UNKNOWN || varEnum == VarEnum.VT_BSTR)
				{
					*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = IntPtr.Zero;
				}
				return;
			}
			if (!AppContextSwitches.DoNotMarshalOutByrefSafeArrayOnInvoke && (varEnum & VarEnum.VT_ARRAY) != VarEnum.VT_EMPTY)
			{
				Variant variant;
				Marshal.GetNativeVariantForObject(value, (IntPtr)((void*)(&variant)));
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = variant._typeUnion._unionTypes._byref;
				return;
			}
			switch (varEnum)
			{
			case VarEnum.VT_I2:
				*(short*)((void*)this._typeUnion._unionTypes._byref) = (short)value;
				return;
			case VarEnum.VT_I4:
			case VarEnum.VT_INT:
				*(int*)((void*)this._typeUnion._unionTypes._byref) = (int)value;
				return;
			case VarEnum.VT_R4:
				*(float*)((void*)this._typeUnion._unionTypes._byref) = (float)value;
				return;
			case VarEnum.VT_R8:
				*(double*)((void*)this._typeUnion._unionTypes._byref) = (double)value;
				return;
			case VarEnum.VT_CY:
				*(long*)((void*)this._typeUnion._unionTypes._byref) = decimal.ToOACurrency((decimal)value);
				return;
			case VarEnum.VT_DATE:
				*(double*)((void*)this._typeUnion._unionTypes._byref) = ((DateTime)value).ToOADate();
				return;
			case VarEnum.VT_BSTR:
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = Marshal.StringToBSTR((string)value);
				return;
			case VarEnum.VT_DISPATCH:
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = Marshal.GetIDispatchForObject(value);
				return;
			case VarEnum.VT_ERROR:
				*(int*)((void*)this._typeUnion._unionTypes._byref) = ((ErrorWrapper)value).ErrorCode;
				return;
			case VarEnum.VT_BOOL:
				*(short*)((void*)this._typeUnion._unionTypes._byref) = (((bool)value) ? -1 : 0);
				return;
			case VarEnum.VT_VARIANT:
				Marshal.GetNativeVariantForObject(value, this._typeUnion._unionTypes._byref);
				return;
			case VarEnum.VT_UNKNOWN:
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = Marshal.GetIUnknownForObject(value);
				return;
			case VarEnum.VT_DECIMAL:
				*(decimal*)((void*)this._typeUnion._unionTypes._byref) = (decimal)value;
				return;
			case VarEnum.VT_I1:
				*(byte*)((void*)this._typeUnion._unionTypes._byref) = (byte)((sbyte)value);
				return;
			case VarEnum.VT_UI1:
				*(byte*)((void*)this._typeUnion._unionTypes._byref) = (byte)value;
				return;
			case VarEnum.VT_UI2:
				*(short*)((void*)this._typeUnion._unionTypes._byref) = (short)((ushort)value);
				return;
			case VarEnum.VT_UI4:
			case VarEnum.VT_UINT:
				*(int*)((void*)this._typeUnion._unionTypes._byref) = (int)((uint)value);
				return;
			case VarEnum.VT_I8:
				*(long*)((void*)this._typeUnion._unionTypes._byref) = (long)value;
				return;
			case VarEnum.VT_UI8:
				*(long*)((void*)this._typeUnion._unionTypes._byref) = (long)((ulong)value);
				return;
			}
			throw new ArgumentException("invalid argument type");
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x0014DBE4 File Offset: 0x0014BDE4
		public unsafe object ToObject()
		{
			if (this.IsEmpty)
			{
				return null;
			}
			switch (this.VariantType)
			{
			case VarEnum.VT_NULL:
				return DBNull.Value;
			case VarEnum.VT_I2:
				return this.AsI2;
			case VarEnum.VT_I4:
				return this.AsI4;
			case VarEnum.VT_R4:
				return this.AsR4;
			case VarEnum.VT_R8:
				return this.AsR8;
			case VarEnum.VT_CY:
				return this.AsCy;
			case VarEnum.VT_DATE:
				return this.AsDate;
			case VarEnum.VT_BSTR:
				return this.AsBstr;
			case VarEnum.VT_DISPATCH:
				return this.AsDispatch;
			case VarEnum.VT_ERROR:
				return this.AsError;
			case VarEnum.VT_BOOL:
				return this.AsBool;
			case VarEnum.VT_UNKNOWN:
				return this.AsUnknown;
			case VarEnum.VT_DECIMAL:
				return this.AsDecimal;
			case VarEnum.VT_I1:
				return this.AsI1;
			case VarEnum.VT_UI1:
				return this.AsUi1;
			case VarEnum.VT_UI2:
				return this.AsUi2;
			case VarEnum.VT_UI4:
				return this.AsUi4;
			case VarEnum.VT_I8:
				return this.AsI8;
			case VarEnum.VT_UI8:
				return this.AsUi8;
			case VarEnum.VT_INT:
				return this.AsInt;
			case VarEnum.VT_UINT:
				return this.AsUint;
			}
			object objectForNativeVariant;
			try
			{
				try
				{
					fixed (IntPtr* ptr = (IntPtr*)(&this))
					{
						objectForNativeVariant = Marshal.GetObjectForNativeVariant((IntPtr)((void*)ptr));
					}
				}
				finally
				{
					IntPtr* ptr = null;
				}
			}
			catch (Exception inner)
			{
				throw new NotImplementedException("Variant.ToObject cannot handle" + this.VariantType, inner);
			}
			return objectForNativeVariant;
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x0014DDA4 File Offset: 0x0014BFA4
		public unsafe void Clear()
		{
			VarEnum variantType = this.VariantType;
			if ((variantType & VarEnum.VT_BYREF) != VarEnum.VT_EMPTY)
			{
				this.VariantType = VarEnum.VT_EMPTY;
				return;
			}
			if ((variantType & VarEnum.VT_ARRAY) != VarEnum.VT_EMPTY || variantType == VarEnum.VT_BSTR || variantType == VarEnum.VT_UNKNOWN || variantType == VarEnum.VT_DISPATCH || variantType == VarEnum.VT_VARIANT || variantType == VarEnum.VT_RECORD || variantType == VarEnum.VT_VARIANT)
			{
				fixed (IntPtr* ptr = (IntPtr*)(&this))
				{
					NativeMethods.VariantClear((IntPtr)((void*)ptr));
				}
				return;
			}
			this.VariantType = VarEnum.VT_EMPTY;
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x0014DE08 File Offset: 0x0014C008
		// (set) Token: 0x0600621D RID: 25117 RVA: 0x0014DE15 File Offset: 0x0014C015
		public VarEnum VariantType
		{
			get
			{
				return (VarEnum)this._typeUnion._vt;
			}
			set
			{
				this._typeUnion._vt = (ushort)value;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x0600621E RID: 25118 RVA: 0x0014DE24 File Offset: 0x0014C024
		internal bool IsEmpty
		{
			get
			{
				return this._typeUnion._vt == 0;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600621F RID: 25119 RVA: 0x0014DE34 File Offset: 0x0014C034
		internal bool IsByRef
		{
			get
			{
				return (this._typeUnion._vt & 16384) > 0;
			}
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x0014DE4A File Offset: 0x0014C04A
		public void SetAsNULL()
		{
			this.VariantType = VarEnum.VT_NULL;
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06006221 RID: 25121 RVA: 0x0014DE53 File Offset: 0x0014C053
		// (set) Token: 0x06006222 RID: 25122 RVA: 0x0014DE65 File Offset: 0x0014C065
		public sbyte AsI1
		{
			get
			{
				return this._typeUnion._unionTypes._i1;
			}
			set
			{
				this.VariantType = VarEnum.VT_I1;
				this._typeUnion._unionTypes._i1 = value;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06006223 RID: 25123 RVA: 0x0014DE80 File Offset: 0x0014C080
		// (set) Token: 0x06006224 RID: 25124 RVA: 0x0014DE92 File Offset: 0x0014C092
		public short AsI2
		{
			get
			{
				return this._typeUnion._unionTypes._i2;
			}
			set
			{
				this.VariantType = VarEnum.VT_I2;
				this._typeUnion._unionTypes._i2 = value;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x0014DEAC File Offset: 0x0014C0AC
		// (set) Token: 0x06006226 RID: 25126 RVA: 0x0014DEBE File Offset: 0x0014C0BE
		public int AsI4
		{
			get
			{
				return this._typeUnion._unionTypes._i4;
			}
			set
			{
				this.VariantType = VarEnum.VT_I4;
				this._typeUnion._unionTypes._i4 = value;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x0014DED8 File Offset: 0x0014C0D8
		// (set) Token: 0x06006228 RID: 25128 RVA: 0x0014DEEA File Offset: 0x0014C0EA
		public long AsI8
		{
			get
			{
				return this._typeUnion._unionTypes._i8;
			}
			set
			{
				this.VariantType = VarEnum.VT_I8;
				this._typeUnion._unionTypes._i8 = value;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06006229 RID: 25129 RVA: 0x0014DF05 File Offset: 0x0014C105
		// (set) Token: 0x0600622A RID: 25130 RVA: 0x0014DF17 File Offset: 0x0014C117
		public byte AsUi1
		{
			get
			{
				return this._typeUnion._unionTypes._ui1;
			}
			set
			{
				this.VariantType = VarEnum.VT_UI1;
				this._typeUnion._unionTypes._ui1 = value;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x0600622B RID: 25131 RVA: 0x0014DF32 File Offset: 0x0014C132
		// (set) Token: 0x0600622C RID: 25132 RVA: 0x0014DF44 File Offset: 0x0014C144
		public ushort AsUi2
		{
			get
			{
				return this._typeUnion._unionTypes._ui2;
			}
			set
			{
				this.VariantType = VarEnum.VT_UI2;
				this._typeUnion._unionTypes._ui2 = value;
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x0600622D RID: 25133 RVA: 0x0014DF5F File Offset: 0x0014C15F
		// (set) Token: 0x0600622E RID: 25134 RVA: 0x0014DF71 File Offset: 0x0014C171
		public uint AsUi4
		{
			get
			{
				return this._typeUnion._unionTypes._ui4;
			}
			set
			{
				this.VariantType = VarEnum.VT_UI4;
				this._typeUnion._unionTypes._ui4 = value;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600622F RID: 25135 RVA: 0x0014DF8C File Offset: 0x0014C18C
		// (set) Token: 0x06006230 RID: 25136 RVA: 0x0014DF9E File Offset: 0x0014C19E
		public ulong AsUi8
		{
			get
			{
				return this._typeUnion._unionTypes._ui8;
			}
			set
			{
				this.VariantType = VarEnum.VT_UI8;
				this._typeUnion._unionTypes._ui8 = value;
			}
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x0014DFB9 File Offset: 0x0014C1B9
		// (set) Token: 0x06006232 RID: 25138 RVA: 0x0014DFCB File Offset: 0x0014C1CB
		public int AsInt
		{
			get
			{
				return this._typeUnion._unionTypes._int;
			}
			set
			{
				this.VariantType = VarEnum.VT_INT;
				this._typeUnion._unionTypes._int = value;
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06006233 RID: 25139 RVA: 0x0014DFE6 File Offset: 0x0014C1E6
		// (set) Token: 0x06006234 RID: 25140 RVA: 0x0014DFF8 File Offset: 0x0014C1F8
		public uint AsUint
		{
			get
			{
				return this._typeUnion._unionTypes._uint;
			}
			set
			{
				this.VariantType = VarEnum.VT_UINT;
				this._typeUnion._unionTypes._uint = value;
			}
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06006235 RID: 25141 RVA: 0x0014E013 File Offset: 0x0014C213
		// (set) Token: 0x06006236 RID: 25142 RVA: 0x0014E028 File Offset: 0x0014C228
		public bool AsBool
		{
			get
			{
				return this._typeUnion._unionTypes._bool != 0;
			}
			set
			{
				this.VariantType = VarEnum.VT_BOOL;
				this._typeUnion._unionTypes._bool = (value ? -1 : 0);
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06006237 RID: 25143 RVA: 0x0014E049 File Offset: 0x0014C249
		// (set) Token: 0x06006238 RID: 25144 RVA: 0x0014E05B File Offset: 0x0014C25B
		public int AsError
		{
			get
			{
				return this._typeUnion._unionTypes._error;
			}
			set
			{
				this.VariantType = VarEnum.VT_ERROR;
				this._typeUnion._unionTypes._error = value;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06006239 RID: 25145 RVA: 0x0014E076 File Offset: 0x0014C276
		// (set) Token: 0x0600623A RID: 25146 RVA: 0x0014E088 File Offset: 0x0014C288
		public float AsR4
		{
			get
			{
				return this._typeUnion._unionTypes._r4;
			}
			set
			{
				this.VariantType = VarEnum.VT_R4;
				this._typeUnion._unionTypes._r4 = value;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x0600623B RID: 25147 RVA: 0x0014E0A2 File Offset: 0x0014C2A2
		// (set) Token: 0x0600623C RID: 25148 RVA: 0x0014E0B4 File Offset: 0x0014C2B4
		public double AsR8
		{
			get
			{
				return this._typeUnion._unionTypes._r8;
			}
			set
			{
				this.VariantType = VarEnum.VT_R8;
				this._typeUnion._unionTypes._r8 = value;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x0014E0D0 File Offset: 0x0014C2D0
		// (set) Token: 0x0600623E RID: 25150 RVA: 0x0014E0F7 File Offset: 0x0014C2F7
		public decimal AsDecimal
		{
			get
			{
				Variant variant = this;
				variant._typeUnion._vt = 0;
				return variant._decimal;
			}
			set
			{
				this.VariantType = VarEnum.VT_DECIMAL;
				this._decimal = value;
				this._typeUnion._vt = 14;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600623F RID: 25151 RVA: 0x0014E115 File Offset: 0x0014C315
		// (set) Token: 0x06006240 RID: 25152 RVA: 0x0014E12C File Offset: 0x0014C32C
		public decimal AsCy
		{
			get
			{
				return decimal.FromOACurrency(this._typeUnion._unionTypes._cy);
			}
			set
			{
				this.VariantType = VarEnum.VT_CY;
				this._typeUnion._unionTypes._cy = decimal.ToOACurrency(value);
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06006241 RID: 25153 RVA: 0x0014E14B File Offset: 0x0014C34B
		// (set) Token: 0x06006242 RID: 25154 RVA: 0x0014E162 File Offset: 0x0014C362
		public DateTime AsDate
		{
			get
			{
				return DateTime.FromOADate(this._typeUnion._unionTypes._date);
			}
			set
			{
				this.VariantType = VarEnum.VT_DATE;
				this._typeUnion._unionTypes._date = value.ToOADate();
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06006243 RID: 25155 RVA: 0x0014E182 File Offset: 0x0014C382
		// (set) Token: 0x06006244 RID: 25156 RVA: 0x0014E199 File Offset: 0x0014C399
		public string AsBstr
		{
			get
			{
				return Marshal.PtrToStringBSTR(this._typeUnion._unionTypes._bstr);
			}
			set
			{
				this.VariantType = VarEnum.VT_BSTR;
				this._typeUnion._unionTypes._bstr = Marshal.StringToBSTR(value);
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06006245 RID: 25157 RVA: 0x0014E1B8 File Offset: 0x0014C3B8
		// (set) Token: 0x06006246 RID: 25158 RVA: 0x0014E1ED File Offset: 0x0014C3ED
		public object AsUnknown
		{
			get
			{
				if (this._typeUnion._unionTypes._unknown == IntPtr.Zero)
				{
					return null;
				}
				return Marshal.GetObjectForIUnknown(this._typeUnion._unionTypes._unknown);
			}
			set
			{
				this.VariantType = VarEnum.VT_UNKNOWN;
				if (value == null)
				{
					this._typeUnion._unionTypes._unknown = IntPtr.Zero;
					return;
				}
				this._typeUnion._unionTypes._unknown = Marshal.GetIUnknownForObject(value);
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06006247 RID: 25159 RVA: 0x0014E226 File Offset: 0x0014C426
		// (set) Token: 0x06006248 RID: 25160 RVA: 0x0014E25B File Offset: 0x0014C45B
		public object AsDispatch
		{
			get
			{
				if (this._typeUnion._unionTypes._dispatch == IntPtr.Zero)
				{
					return null;
				}
				return Marshal.GetObjectForIUnknown(this._typeUnion._unionTypes._dispatch);
			}
			set
			{
				this.VariantType = VarEnum.VT_DISPATCH;
				if (value == null)
				{
					this._typeUnion._unionTypes._dispatch = IntPtr.Zero;
					return;
				}
				this._typeUnion._unionTypes._dispatch = Marshal.GetIDispatchForObject(value);
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06006249 RID: 25161 RVA: 0x0014E294 File Offset: 0x0014C494
		internal IntPtr AsByRefVariant
		{
			get
			{
				return this._typeUnion._unionTypes._pvarVal;
			}
		}

		// Token: 0x04002C08 RID: 11272
		[FieldOffset(0)]
		private Variant.TypeUnion _typeUnion;

		// Token: 0x04002C09 RID: 11273
		[FieldOffset(0)]
		private decimal _decimal;

		// Token: 0x02000C69 RID: 3177
		private struct TypeUnion
		{
			// Token: 0x0400377B RID: 14203
			internal ushort _vt;

			// Token: 0x0400377C RID: 14204
			internal ushort _wReserved1;

			// Token: 0x0400377D RID: 14205
			internal ushort _wReserved2;

			// Token: 0x0400377E RID: 14206
			internal ushort _wReserved3;

			// Token: 0x0400377F RID: 14207
			internal Variant.UnionTypes _unionTypes;
		}

		// Token: 0x02000C6A RID: 3178
		private struct Record
		{
			// Token: 0x04003780 RID: 14208
			private IntPtr _record;

			// Token: 0x04003781 RID: 14209
			private IntPtr _recordInfo;
		}

		// Token: 0x02000C6B RID: 3179
		[StructLayout(LayoutKind.Explicit)]
		private struct UnionTypes
		{
			// Token: 0x04003782 RID: 14210
			[FieldOffset(0)]
			internal sbyte _i1;

			// Token: 0x04003783 RID: 14211
			[FieldOffset(0)]
			internal short _i2;

			// Token: 0x04003784 RID: 14212
			[FieldOffset(0)]
			internal int _i4;

			// Token: 0x04003785 RID: 14213
			[FieldOffset(0)]
			internal long _i8;

			// Token: 0x04003786 RID: 14214
			[FieldOffset(0)]
			internal byte _ui1;

			// Token: 0x04003787 RID: 14215
			[FieldOffset(0)]
			internal ushort _ui2;

			// Token: 0x04003788 RID: 14216
			[FieldOffset(0)]
			internal uint _ui4;

			// Token: 0x04003789 RID: 14217
			[FieldOffset(0)]
			internal ulong _ui8;

			// Token: 0x0400378A RID: 14218
			[FieldOffset(0)]
			internal int _int;

			// Token: 0x0400378B RID: 14219
			[FieldOffset(0)]
			internal uint _uint;

			// Token: 0x0400378C RID: 14220
			[FieldOffset(0)]
			internal short _bool;

			// Token: 0x0400378D RID: 14221
			[FieldOffset(0)]
			internal int _error;

			// Token: 0x0400378E RID: 14222
			[FieldOffset(0)]
			internal float _r4;

			// Token: 0x0400378F RID: 14223
			[FieldOffset(0)]
			internal double _r8;

			// Token: 0x04003790 RID: 14224
			[FieldOffset(0)]
			internal long _cy;

			// Token: 0x04003791 RID: 14225
			[FieldOffset(0)]
			internal double _date;

			// Token: 0x04003792 RID: 14226
			[FieldOffset(0)]
			internal IntPtr _bstr;

			// Token: 0x04003793 RID: 14227
			[FieldOffset(0)]
			internal IntPtr _unknown;

			// Token: 0x04003794 RID: 14228
			[FieldOffset(0)]
			internal IntPtr _dispatch;

			// Token: 0x04003795 RID: 14229
			[FieldOffset(0)]
			internal IntPtr _pvarVal;

			// Token: 0x04003796 RID: 14230
			[FieldOffset(0)]
			internal IntPtr _byref;

			// Token: 0x04003797 RID: 14231
			[FieldOffset(0)]
			internal Variant.Record _record;
		}
	}
}
