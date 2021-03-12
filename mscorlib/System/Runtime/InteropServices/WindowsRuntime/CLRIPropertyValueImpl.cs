using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D3 RID: 2515
	internal class CLRIPropertyValueImpl : IPropertyValue
	{
		// Token: 0x060063E2 RID: 25570 RVA: 0x001532A1 File Offset: 0x001514A1
		internal CLRIPropertyValueImpl(PropertyType type, object data)
		{
			this._type = type;
			this._data = data;
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060063E3 RID: 25571 RVA: 0x001532B8 File Offset: 0x001514B8
		private static Tuple<Type, PropertyType>[] NumericScalarTypes
		{
			get
			{
				if (CLRIPropertyValueImpl.s_numericScalarTypes == null)
				{
					Tuple<Type, PropertyType>[] array = new Tuple<Type, PropertyType>[]
					{
						new Tuple<Type, PropertyType>(typeof(byte), PropertyType.UInt8),
						new Tuple<Type, PropertyType>(typeof(short), PropertyType.Int16),
						new Tuple<Type, PropertyType>(typeof(ushort), PropertyType.UInt16),
						new Tuple<Type, PropertyType>(typeof(int), PropertyType.Int32),
						new Tuple<Type, PropertyType>(typeof(uint), PropertyType.UInt32),
						new Tuple<Type, PropertyType>(typeof(long), PropertyType.Int64),
						new Tuple<Type, PropertyType>(typeof(ulong), PropertyType.UInt64),
						new Tuple<Type, PropertyType>(typeof(float), PropertyType.Single),
						new Tuple<Type, PropertyType>(typeof(double), PropertyType.Double)
					};
					CLRIPropertyValueImpl.s_numericScalarTypes = array;
				}
				return CLRIPropertyValueImpl.s_numericScalarTypes;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060063E4 RID: 25572 RVA: 0x00153394 File Offset: 0x00151594
		public PropertyType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060063E5 RID: 25573 RVA: 0x0015339C File Offset: 0x0015159C
		public bool IsNumericScalar
		{
			get
			{
				return CLRIPropertyValueImpl.IsNumericScalarImpl(this._type, this._data);
			}
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x001533AF File Offset: 0x001515AF
		public override string ToString()
		{
			if (this._data != null)
			{
				return this._data.ToString();
			}
			return base.ToString();
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x001533CB File Offset: 0x001515CB
		public byte GetUInt8()
		{
			return this.CoerceScalarValue<byte>(PropertyType.UInt8);
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x001533D4 File Offset: 0x001515D4
		public short GetInt16()
		{
			return this.CoerceScalarValue<short>(PropertyType.Int16);
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x001533DD File Offset: 0x001515DD
		public ushort GetUInt16()
		{
			return this.CoerceScalarValue<ushort>(PropertyType.UInt16);
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x001533E6 File Offset: 0x001515E6
		public int GetInt32()
		{
			return this.CoerceScalarValue<int>(PropertyType.Int32);
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x001533EF File Offset: 0x001515EF
		public uint GetUInt32()
		{
			return this.CoerceScalarValue<uint>(PropertyType.UInt32);
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x001533F8 File Offset: 0x001515F8
		public long GetInt64()
		{
			return this.CoerceScalarValue<long>(PropertyType.Int64);
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x00153401 File Offset: 0x00151601
		public ulong GetUInt64()
		{
			return this.CoerceScalarValue<ulong>(PropertyType.UInt64);
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x0015340A File Offset: 0x0015160A
		public float GetSingle()
		{
			return this.CoerceScalarValue<float>(PropertyType.Single);
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x00153413 File Offset: 0x00151613
		public double GetDouble()
		{
			return this.CoerceScalarValue<double>(PropertyType.Double);
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00153420 File Offset: 0x00151620
		public char GetChar16()
		{
			if (this.Type != PropertyType.Char16)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Char16"
				}), -2147316576);
			}
			return (char)this._data;
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x00153474 File Offset: 0x00151674
		public bool GetBoolean()
		{
			if (this.Type != PropertyType.Boolean)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Boolean"
				}), -2147316576);
			}
			return (bool)this._data;
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x001534C7 File Offset: 0x001516C7
		public string GetString()
		{
			return this.CoerceScalarValue<string>(PropertyType.String);
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x001534D4 File Offset: 0x001516D4
		public object GetInspectable()
		{
			if (this.Type != PropertyType.Inspectable)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Inspectable"
				}), -2147316576);
			}
			return this._data;
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x00153522 File Offset: 0x00151722
		public Guid GetGuid()
		{
			return this.CoerceScalarValue<Guid>(PropertyType.Guid);
		}

		// Token: 0x060063F5 RID: 25589 RVA: 0x0015352C File Offset: 0x0015172C
		public DateTimeOffset GetDateTime()
		{
			if (this.Type != PropertyType.DateTime)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"DateTime"
				}), -2147316576);
			}
			return (DateTimeOffset)this._data;
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x00153580 File Offset: 0x00151780
		public TimeSpan GetTimeSpan()
		{
			if (this.Type != PropertyType.TimeSpan)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"TimeSpan"
				}), -2147316576);
			}
			return (TimeSpan)this._data;
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x001535D4 File Offset: 0x001517D4
		[SecuritySafeCritical]
		public Point GetPoint()
		{
			if (this.Type != PropertyType.Point)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Point"
				}), -2147316576);
			}
			return this.Unbox<Point>(IReferenceFactory.s_pointType);
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x00153628 File Offset: 0x00151828
		[SecuritySafeCritical]
		public Size GetSize()
		{
			if (this.Type != PropertyType.Size)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Size"
				}), -2147316576);
			}
			return this.Unbox<Size>(IReferenceFactory.s_sizeType);
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x0015367C File Offset: 0x0015187C
		[SecuritySafeCritical]
		public Rect GetRect()
		{
			if (this.Type != PropertyType.Rect)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Rect"
				}), -2147316576);
			}
			return this.Unbox<Rect>(IReferenceFactory.s_rectType);
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x001536CF File Offset: 0x001518CF
		public byte[] GetUInt8Array()
		{
			return this.CoerceArrayValue<byte>(PropertyType.UInt8Array);
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001536DC File Offset: 0x001518DC
		public short[] GetInt16Array()
		{
			return this.CoerceArrayValue<short>(PropertyType.Int16Array);
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x001536E9 File Offset: 0x001518E9
		public ushort[] GetUInt16Array()
		{
			return this.CoerceArrayValue<ushort>(PropertyType.UInt16Array);
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x001536F6 File Offset: 0x001518F6
		public int[] GetInt32Array()
		{
			return this.CoerceArrayValue<int>(PropertyType.Int32Array);
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x00153703 File Offset: 0x00151903
		public uint[] GetUInt32Array()
		{
			return this.CoerceArrayValue<uint>(PropertyType.UInt32Array);
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x00153710 File Offset: 0x00151910
		public long[] GetInt64Array()
		{
			return this.CoerceArrayValue<long>(PropertyType.Int64Array);
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x0015371D File Offset: 0x0015191D
		public ulong[] GetUInt64Array()
		{
			return this.CoerceArrayValue<ulong>(PropertyType.UInt64Array);
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x0015372A File Offset: 0x0015192A
		public float[] GetSingleArray()
		{
			return this.CoerceArrayValue<float>(PropertyType.SingleArray);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x00153737 File Offset: 0x00151937
		public double[] GetDoubleArray()
		{
			return this.CoerceArrayValue<double>(PropertyType.DoubleArray);
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x00153744 File Offset: 0x00151944
		public char[] GetChar16Array()
		{
			if (this.Type != PropertyType.Char16Array)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Char16[]"
				}), -2147316576);
			}
			return (char[])this._data;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x0015379C File Offset: 0x0015199C
		public bool[] GetBooleanArray()
		{
			if (this.Type != PropertyType.BooleanArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Boolean[]"
				}), -2147316576);
			}
			return (bool[])this._data;
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x001537F2 File Offset: 0x001519F2
		public string[] GetStringArray()
		{
			return this.CoerceArrayValue<string>(PropertyType.StringArray);
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00153800 File Offset: 0x00151A00
		public object[] GetInspectableArray()
		{
			if (this.Type != PropertyType.InspectableArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Inspectable[]"
				}), -2147316576);
			}
			return (object[])this._data;
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x00153856 File Offset: 0x00151A56
		public Guid[] GetGuidArray()
		{
			return this.CoerceArrayValue<Guid>(PropertyType.GuidArray);
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x00153864 File Offset: 0x00151A64
		public DateTimeOffset[] GetDateTimeArray()
		{
			if (this.Type != PropertyType.DateTimeArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"DateTimeOffset[]"
				}), -2147316576);
			}
			return (DateTimeOffset[])this._data;
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x001538BC File Offset: 0x00151ABC
		public TimeSpan[] GetTimeSpanArray()
		{
			if (this.Type != PropertyType.TimeSpanArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"TimeSpan[]"
				}), -2147316576);
			}
			return (TimeSpan[])this._data;
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x00153914 File Offset: 0x00151B14
		[SecuritySafeCritical]
		public Point[] GetPointArray()
		{
			if (this.Type != PropertyType.PointArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Point[]"
				}), -2147316576);
			}
			return this.UnboxArray<Point>(IReferenceFactory.s_pointType);
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0015396C File Offset: 0x00151B6C
		[SecuritySafeCritical]
		public Size[] GetSizeArray()
		{
			if (this.Type != PropertyType.SizeArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Size[]"
				}), -2147316576);
			}
			return this.UnboxArray<Size>(IReferenceFactory.s_sizeType);
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x001539C4 File Offset: 0x00151BC4
		[SecuritySafeCritical]
		public Rect[] GetRectArray()
		{
			if (this.Type != PropertyType.RectArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					"Rect[]"
				}), -2147316576);
			}
			return this.UnboxArray<Rect>(IReferenceFactory.s_rectType);
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x00153A1C File Offset: 0x00151C1C
		private T[] CoerceArrayValue<T>(PropertyType unboxType)
		{
			if (this.Type == unboxType)
			{
				return (T[])this._data;
			}
			Array array = this._data as Array;
			if (array == null)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					typeof(T).MakeArrayType().Name
				}), -2147316576);
			}
			PropertyType type = this.Type - 1024;
			T[] array2 = new T[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					array2[i] = CLRIPropertyValueImpl.CoerceScalarValue<T>(type, array.GetValue(i));
				}
				catch (InvalidCastException ex)
				{
					Exception ex2 = new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueArrayCoersion", new object[]
					{
						this.Type,
						typeof(T).MakeArrayType().Name,
						i,
						ex.Message
					}), ex);
					ex2.SetErrorCode(ex._HResult);
					throw ex2;
				}
			}
			return array2;
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x00153B48 File Offset: 0x00151D48
		private T CoerceScalarValue<T>(PropertyType unboxType)
		{
			if (this.Type == unboxType)
			{
				return (T)((object)this._data);
			}
			return CLRIPropertyValueImpl.CoerceScalarValue<T>(this.Type, this._data);
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x00153B70 File Offset: 0x00151D70
		private static T CoerceScalarValue<T>(PropertyType type, object value)
		{
			if (!CLRIPropertyValueImpl.IsCoercable(type, value) && type != PropertyType.Inspectable)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					type,
					typeof(T).Name
				}), -2147316576);
			}
			try
			{
				if (type == PropertyType.String && typeof(T) == typeof(Guid))
				{
					return (T)((object)Guid.Parse((string)value));
				}
				if (type == PropertyType.Guid && typeof(T) == typeof(string))
				{
					return (T)((object)((Guid)value).ToString("D", CultureInfo.InvariantCulture));
				}
				foreach (Tuple<Type, PropertyType> tuple in CLRIPropertyValueImpl.NumericScalarTypes)
				{
					if (tuple.Item1 == typeof(T))
					{
						return (T)((object)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
					}
				}
			}
			catch (FormatException)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					type,
					typeof(T).Name
				}), -2147316576);
			}
			catch (InvalidCastException)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					type,
					typeof(T).Name
				}), -2147316576);
			}
			catch (OverflowException)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueCoersion", new object[]
				{
					type,
					value,
					typeof(T).Name
				}), -2147352566);
			}
			IPropertyValue propertyValue = value as IPropertyValue;
			if (type == PropertyType.Inspectable && propertyValue != null)
			{
				if (typeof(T) == typeof(byte))
				{
					return (T)((object)propertyValue.GetUInt8());
				}
				if (typeof(T) == typeof(short))
				{
					return (T)((object)propertyValue.GetInt16());
				}
				if (typeof(T) == typeof(ushort))
				{
					return (T)((object)propertyValue.GetUInt16());
				}
				if (typeof(T) == typeof(int))
				{
					return (T)((object)propertyValue.GetUInt32());
				}
				if (typeof(T) == typeof(uint))
				{
					return (T)((object)propertyValue.GetUInt32());
				}
				if (typeof(T) == typeof(long))
				{
					return (T)((object)propertyValue.GetInt64());
				}
				if (typeof(T) == typeof(ulong))
				{
					return (T)((object)propertyValue.GetUInt64());
				}
				if (typeof(T) == typeof(float))
				{
					return (T)((object)propertyValue.GetSingle());
				}
				if (typeof(T) == typeof(double))
				{
					return (T)((object)propertyValue.GetDouble());
				}
			}
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
			{
				type,
				typeof(T).Name
			}), -2147316576);
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x00153F44 File Offset: 0x00152144
		private static bool IsCoercable(PropertyType type, object data)
		{
			return type == PropertyType.Guid || type == PropertyType.String || CLRIPropertyValueImpl.IsNumericScalarImpl(type, data);
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00153F5C File Offset: 0x0015215C
		private static bool IsNumericScalarImpl(PropertyType type, object data)
		{
			if (data.GetType().IsEnum)
			{
				return true;
			}
			foreach (Tuple<Type, PropertyType> tuple in CLRIPropertyValueImpl.NumericScalarTypes)
			{
				if (tuple.Item2 == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x00153F9C File Offset: 0x0015219C
		[SecurityCritical]
		private unsafe T Unbox<T>(Type expectedBoxedType) where T : struct
		{
			if (this._data.GetType() != expectedBoxedType)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this._data.GetType(),
					expectedBoxedType.Name
				}), -2147316576);
			}
			T t = Activator.CreateInstance<T>();
			fixed (byte* ptr = &JitHelpers.GetPinningHelper(this._data).m_data)
			{
				byte* dest = (byte*)((void*)JitHelpers.UnsafeCastToStackPointer<T>(ref t));
				Buffer.Memcpy(dest, ptr, Marshal.SizeOf<T>(t));
			}
			return t;
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x00154024 File Offset: 0x00152224
		[SecurityCritical]
		private unsafe T[] UnboxArray<T>(Type expectedArrayElementType) where T : struct
		{
			Array array = this._data as Array;
			if (array == null || this._data.GetType().GetElementType() != expectedArrayElementType)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this._data.GetType(),
					expectedArrayElementType.MakeArrayType().Name
				}), -2147316576);
			}
			T[] array2 = new T[array.Length];
			if (array2.Length != 0)
			{
				fixed (byte* ptr = &JitHelpers.GetPinningHelper(array).m_data)
				{
					fixed (byte* ptr2 = &JitHelpers.GetPinningHelper(array2).m_data)
					{
						byte* src = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(array, 0));
						byte* dest = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement<T>(array2, 0));
						Buffer.Memcpy(dest, src, checked(Marshal.SizeOf(typeof(T)) * array2.Length));
					}
				}
			}
			return array2;
		}

		// Token: 0x04002C76 RID: 11382
		private PropertyType _type;

		// Token: 0x04002C77 RID: 11383
		private object _data;

		// Token: 0x04002C78 RID: 11384
		private static volatile Tuple<Type, PropertyType>[] s_numericScalarTypes;
	}
}
