using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005A9 RID: 1449
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct CustomAttributeTypedArgument
	{
		// Token: 0x06004435 RID: 17461 RVA: 0x000FA3D4 File Offset: 0x000F85D4
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x000FA3E9 File Offset: 0x000F85E9
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x000FA404 File Offset: 0x000F8604
		private static Type CustomAttributeEncodingToType(CustomAttributeEncoding encodedType)
		{
			if (encodedType <= CustomAttributeEncoding.Array)
			{
				switch (encodedType)
				{
				case CustomAttributeEncoding.Boolean:
					return typeof(bool);
				case CustomAttributeEncoding.Char:
					return typeof(char);
				case CustomAttributeEncoding.SByte:
					return typeof(sbyte);
				case CustomAttributeEncoding.Byte:
					return typeof(byte);
				case CustomAttributeEncoding.Int16:
					return typeof(short);
				case CustomAttributeEncoding.UInt16:
					return typeof(ushort);
				case CustomAttributeEncoding.Int32:
					return typeof(int);
				case CustomAttributeEncoding.UInt32:
					return typeof(uint);
				case CustomAttributeEncoding.Int64:
					return typeof(long);
				case CustomAttributeEncoding.UInt64:
					return typeof(ulong);
				case CustomAttributeEncoding.Float:
					return typeof(float);
				case CustomAttributeEncoding.Double:
					return typeof(double);
				case CustomAttributeEncoding.String:
					return typeof(string);
				default:
					if (encodedType == CustomAttributeEncoding.Array)
					{
						return typeof(Array);
					}
					break;
				}
			}
			else
			{
				if (encodedType == CustomAttributeEncoding.Type)
				{
					return typeof(Type);
				}
				if (encodedType == CustomAttributeEncoding.Object)
				{
					return typeof(object);
				}
				if (encodedType == CustomAttributeEncoding.Enum)
				{
					return typeof(Enum);
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
			{
				(int)encodedType
			}), "encodedType");
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x000FA550 File Offset: 0x000F8750
		[SecuritySafeCritical]
		private unsafe static object EncodedValueToRawValue(long val, CustomAttributeEncoding encodedType)
		{
			switch (encodedType)
			{
			case CustomAttributeEncoding.Boolean:
				return (byte)val > 0;
			case CustomAttributeEncoding.Char:
				return (char)val;
			case CustomAttributeEncoding.SByte:
				return (sbyte)val;
			case CustomAttributeEncoding.Byte:
				return (byte)val;
			case CustomAttributeEncoding.Int16:
				return (short)val;
			case CustomAttributeEncoding.UInt16:
				return (ushort)val;
			case CustomAttributeEncoding.Int32:
				return (int)val;
			case CustomAttributeEncoding.UInt32:
				return (uint)val;
			case CustomAttributeEncoding.Int64:
				return val;
			case CustomAttributeEncoding.UInt64:
				return (ulong)val;
			case CustomAttributeEncoding.Float:
				return *(float*)(&val);
			case CustomAttributeEncoding.Double:
				return *(double*)(&val);
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)val
				}), "val");
			}
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x000FA620 File Offset: 0x000F8820
		private static RuntimeType ResolveType(RuntimeModule scope, string typeName)
		{
			RuntimeType typeByNameUsingCARules = RuntimeTypeHandle.GetTypeByNameUsingCARules(typeName, scope);
			if (typeByNameUsingCARules == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_CATypeResolutionFailed"), typeName));
			}
			return typeByNameUsingCARules;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x000FA65A File Offset: 0x000F885A
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.m_value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.m_argumentType = argumentType;
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x000FA689 File Offset: 0x000F8889
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.m_argumentType = value.GetType();
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x000FA6B1 File Offset: 0x000F88B1
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x000FA6D0 File Offset: 0x000F88D0
		internal CustomAttributeTypedArgument(RuntimeModule scope, CustomAttributeEncodedArgument encodedArg)
		{
			CustomAttributeEncoding customAttributeEncoding = encodedArg.CustomAttributeType.EncodedType;
			if (customAttributeEncoding == CustomAttributeEncoding.Undefined)
			{
				throw new ArgumentException("encodedArg");
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum)
			{
				this.m_argumentType = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, encodedArg.CustomAttributeType.EncodedEnumType);
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.String)
			{
				this.m_argumentType = typeof(string);
				this.m_value = encodedArg.StringValue;
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Type)
			{
				this.m_argumentType = typeof(Type);
				this.m_value = null;
				if (encodedArg.StringValue != null)
				{
					this.m_value = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.StringValue);
					return;
				}
			}
			else if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				customAttributeEncoding = encodedArg.CustomAttributeType.EncodedArrayType;
				Type type;
				if (customAttributeEncoding == CustomAttributeEncoding.Enum)
				{
					type = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				}
				else
				{
					type = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				}
				this.m_argumentType = type.MakeArrayType();
				if (encodedArg.ArrayValue == null)
				{
					this.m_value = null;
					return;
				}
				CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[encodedArg.ArrayValue.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new CustomAttributeTypedArgument(scope, encodedArg.ArrayValue[i]);
				}
				this.m_value = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				return;
			}
			else
			{
				this.m_argumentType = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, customAttributeEncoding);
			}
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000FA85E File Offset: 0x000F8A5E
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x000FA868 File Offset: 0x000F8A68
		internal string ToString(bool typed)
		{
			if (this.m_argumentType == null)
			{
				return base.ToString();
			}
			if (this.ArgumentType.IsEnum)
			{
				return string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullName);
			}
			if (this.Value == null)
			{
				return string.Format(CultureInfo.CurrentCulture, typed ? "null" : "({0})null", this.ArgumentType.Name);
			}
			if (this.ArgumentType == typeof(string))
			{
				return string.Format(CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
			}
			if (this.ArgumentType == typeof(char))
			{
				return string.Format(CultureInfo.CurrentCulture, "'{0}'", this.Value);
			}
			if (this.ArgumentType == typeof(Type))
			{
				return string.Format(CultureInfo.CurrentCulture, "typeof({0})", ((Type)this.Value).FullName);
			}
			if (this.ArgumentType.IsArray)
			{
				IList<CustomAttributeTypedArgument> list = this.Value as IList<CustomAttributeTypedArgument>;
				Type elementType = this.ArgumentType.GetElementType();
				string str = string.Format(CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullName : elementType.Name, list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					str += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
				}
				return str + " }";
			}
			return string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.Name);
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000FAA6E File Offset: 0x000F8C6E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000FAA80 File Offset: 0x000F8C80
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x000FAA90 File Offset: 0x000F8C90
		[__DynamicallyInvokable]
		public Type ArgumentType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_argumentType;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x000FAA98 File Offset: 0x000F8C98
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001BA7 RID: 7079
		private object m_value;

		// Token: 0x04001BA8 RID: 7080
		private Type m_argumentType;
	}
}
