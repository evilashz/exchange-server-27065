using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B3 RID: 179
	[TypeConverter(typeof(SimpleGenericsTypeConverter))]
	[Serializable]
	public struct Unlimited<T> : IComparable, IFormattable, IEquatable<T>, IComparable<T> where T : struct, IComparable
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public Unlimited(T limitedValue)
		{
			this.limitedValue = limitedValue;
			this.isUnlimited = false;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		private Unlimited(string expression)
		{
			this = default(Unlimited<T>);
			if (StringComparer.OrdinalIgnoreCase.Compare(expression, "Unlimited") == 0)
			{
				this.isUnlimited = true;
				return;
			}
			object obj;
			if (typeof(T) == typeof(ByteQuantifiedSize))
			{
				obj = ByteQuantifiedSize.Parse(expression);
			}
			else if (typeof(T) == typeof(int))
			{
				obj = int.Parse(expression);
			}
			else if (typeof(T) == typeof(uint))
			{
				obj = uint.Parse(expression);
			}
			else if (typeof(T) == typeof(TimeSpan))
			{
				obj = TimeSpan.Parse(expression);
			}
			else
			{
				if (!(typeof(T) == typeof(EnhancedTimeSpan)))
				{
					throw new InvalidOperationException(DataStrings.ExceptionParseNotSupported);
				}
				obj = EnhancedTimeSpan.Parse(expression);
			}
			this.isUnlimited = false;
			this.limitedValue = (T)((object)obj);
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public static Unlimited<T> UnlimitedValue
		{
			get
			{
				return new Unlimited<T>
				{
					isUnlimited = true
				};
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000FBF2 File Offset: 0x0000DDF2
		public static string UnlimitedString
		{
			get
			{
				return "Unlimited";
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000FBF9 File Offset: 0x0000DDF9
		public bool IsUnlimited
		{
			get
			{
				return this.isUnlimited;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000FC01 File Offset: 0x0000DE01
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000FC21 File Offset: 0x0000DE21
		public T Value
		{
			get
			{
				if (this.isUnlimited)
				{
					throw new InvalidOperationException(DataStrings.ExceptionNoValue);
				}
				return this.limitedValue;
			}
			set
			{
				this.limitedValue = value;
				this.isUnlimited = false;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public override bool Equals(object other)
		{
			if (other is Unlimited<T>)
			{
				Unlimited<T> other2 = (Unlimited<T>)other;
				return this.Equals(other2);
			}
			return false;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000FC59 File Offset: 0x0000DE59
		public bool Equals(T other)
		{
			return !this.IsUnlimited && this.limitedValue.Equals(other);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public override int GetHashCode()
		{
			if (!this.isUnlimited)
			{
				return this.limitedValue.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000FC99 File Offset: 0x0000DE99
		public override string ToString()
		{
			if (!this.isUnlimited)
			{
				return this.limitedValue.ToString();
			}
			return "Unlimited";
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000FCBA File Offset: 0x0000DEBA
		public static Unlimited<ByteQuantifiedSize> Parse(string expression, ByteQuantifiedSize.Quantifier defaultUnit)
		{
			if (StringComparer.OrdinalIgnoreCase.Compare(expression, "Unlimited") == 0)
			{
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
			return ByteQuantifiedSize.Parse(expression, defaultUnit);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public static bool TryParse(string expression, ByteQuantifiedSize.Quantifier defaultUnit, out Unlimited<ByteQuantifiedSize> result)
		{
			bool result2 = false;
			result = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			if (StringComparer.OrdinalIgnoreCase.Compare(expression, "Unlimited") == 0)
			{
				return true;
			}
			ByteQuantifiedSize fromValue;
			if (ByteQuantifiedSize.TryParse(expression, defaultUnit, out fromValue))
			{
				result = fromValue;
			}
			return result2;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000FD26 File Offset: 0x0000DF26
		public static Unlimited<T> Parse(string expression)
		{
			return new Unlimited<T>(expression);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public static bool TryParse(string expression, out Unlimited<T> result)
		{
			bool flag = false;
			result = Unlimited<T>.UnlimitedValue;
			if (StringComparer.OrdinalIgnoreCase.Compare(expression, "Unlimited") == 0)
			{
				return true;
			}
			object obj = null;
			if (typeof(T) == typeof(ByteQuantifiedSize))
			{
				ByteQuantifiedSize byteQuantifiedSize;
				flag = ByteQuantifiedSize.TryParse(expression, out byteQuantifiedSize);
				obj = byteQuantifiedSize;
			}
			else if (typeof(T) == typeof(int))
			{
				int num;
				flag = int.TryParse(expression, out num);
				obj = num;
			}
			else if (typeof(T) == typeof(uint))
			{
				uint num2;
				flag = uint.TryParse(expression, out num2);
				obj = num2;
			}
			else if (typeof(T) == typeof(TimeSpan))
			{
				TimeSpan timeSpan;
				flag = TimeSpan.TryParse(expression, out timeSpan);
				obj = timeSpan;
			}
			else if (typeof(T) == typeof(EnhancedTimeSpan))
			{
				EnhancedTimeSpan enhancedTimeSpan;
				flag = EnhancedTimeSpan.TryParse(expression, out enhancedTimeSpan);
				obj = enhancedTimeSpan;
			}
			if (flag && obj != null)
			{
				result = (T)((object)obj);
			}
			return flag;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000FE60 File Offset: 0x0000E060
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (typeof(IFormattable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()) && !this.IsUnlimited)
			{
				return ((IFormattable)((object)this.Value)).ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000FEBE File Offset: 0x0000E0BE
		public int CompareTo(Unlimited<T> other)
		{
			if (this.isUnlimited)
			{
				if (!other.isUnlimited)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (!other.isUnlimited)
				{
					return Comparer<T>.Default.Compare(this.limitedValue, other.limitedValue);
				}
				return -1;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000FEF7 File Offset: 0x0000E0F7
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (other is Unlimited<T>)
			{
				return this.CompareTo((Unlimited<T>)other);
			}
			throw new ArgumentException(DataStrings.ExceptionObjectInvalid);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000FF22 File Offset: 0x0000E122
		public int CompareTo(T other)
		{
			if (!this.isUnlimited)
			{
				return Comparer<T>.Default.Compare(this.limitedValue, other);
			}
			return 1;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000FF3F File Offset: 0x0000E13F
		public bool Equals(Unlimited<T> other)
		{
			return this.isUnlimited == other.isUnlimited && (this.isUnlimited || EqualityComparer<T>.Default.Equals(this.limitedValue, other.limitedValue));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000FF73 File Offset: 0x0000E173
		public static bool operator ==(Unlimited<T> value1, Unlimited<T> value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000FF7D File Offset: 0x0000E17D
		public static bool operator !=(Unlimited<T> value1, Unlimited<T> value2)
		{
			return !value1.Equals(value2);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000FF8A File Offset: 0x0000E18A
		public static bool operator >(Unlimited<T> value1, Unlimited<T> value2)
		{
			return value1.CompareTo(value2) > 0;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000FF97 File Offset: 0x0000E197
		public static bool operator >=(Unlimited<T> value1, Unlimited<T> value2)
		{
			return value1.CompareTo(value2) >= 0;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000FFA7 File Offset: 0x0000E1A7
		public static bool operator <(Unlimited<T> value1, Unlimited<T> value2)
		{
			return value1.CompareTo(value2) < 0;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		public static bool operator <=(Unlimited<T> value1, Unlimited<T> value2)
		{
			return value1.CompareTo(value2) <= 0;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public static Unlimited<T>operator /(Unlimited<T> value1, object value2)
		{
			return Unlimited<T>.ExecDynamicOperation(value1, value2, "op_Division");
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000FFD2 File Offset: 0x0000E1D2
		public static Unlimited<T>operator *(Unlimited<T> value1, object value2)
		{
			return Unlimited<T>.ExecDynamicOperation(value1, value2, "op_Multiply");
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public static Unlimited<T>operator +(Unlimited<T> value1, object value2)
		{
			return Unlimited<T>.ExecDynamicOperation(value1, value2, "op_Addition");
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000FFEE File Offset: 0x0000E1EE
		public static Unlimited<T>operator -(Unlimited<T> value1, object value2)
		{
			return Unlimited<T>.ExecDynamicOperation(value1, value2, "op_Subtraction");
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		private static Unlimited<T> ExecDynamicOperation(Unlimited<T> value1, object value2, string operationName)
		{
			object obj = Unlimited<T>.UnBucketT(value2);
			if (!Unlimited<T>.IsValidRightOperand(obj))
			{
				throw new InvalidOperationException(DataStrings.ExceptionInvalidOperation(operationName, (value2 == null) ? null : value2.GetType().Name));
			}
			if (value1.IsUnlimited)
			{
				return Unlimited<T>.UnlimitedValue;
			}
			T value3 = Unlimited<T>.DynamicResolveOperation(value1, obj, operationName);
			return new Unlimited<T>
			{
				Value = value3
			};
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00010064 File Offset: 0x0000E264
		private static object UnBucketT(object value)
		{
			if (value != null && value.GetType() == typeof(Unlimited<T>))
			{
				return ((Unlimited<T>)value).Value;
			}
			return value;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000100A0 File Offset: 0x0000E2A0
		private static bool IsValidRightOperand(object value)
		{
			if (value == null || value.GetType().GetTypeInfo().IsGenericType)
			{
				return false;
			}
			foreach (Type right in Unlimited<T>.ValidTypeMathOperations)
			{
				if (value.GetType() == right)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001010C File Offset: 0x0000E30C
		private static T DynamicResolveOperation(Unlimited<T> value1, object value2, string operationName)
		{
			Type[] genericTypeArguments = value1.GetType().GetTypeInfo().GenericTypeArguments;
			if (genericTypeArguments[0] == typeof(int))
			{
				int? num = value1.Value as int?;
				int? num2 = value2 as int?;
				if (num2 == null)
				{
					num2 = new int?((int)Convert.ChangeType(value2, typeof(int)));
				}
				if (num2 == null)
				{
					throw new InvalidOperationException(DataStrings.ExceptionCannotResolveOperation(operationName, typeof(T).Name, value2.GetType().Name));
				}
				if ("op_Subtraction".Equals(operationName))
				{
					return (T)((object)(num.Value - num2.Value));
				}
				if ("op_Division".Equals(operationName))
				{
					return (T)((object)(num.Value / num2.Value));
				}
				if ("op_Multiply".Equals(operationName))
				{
					return (T)((object)(num.Value * num2.Value));
				}
				return (T)((object)(num.Value + num2.Value));
			}
			else if (genericTypeArguments[0] == typeof(uint))
			{
				uint? num3 = value1.Value as uint?;
				uint? num4 = value2 as uint?;
				if (num4 == null)
				{
					num4 = new uint?((uint)Convert.ChangeType(value2, typeof(uint)));
				}
				if (num4 == null)
				{
					throw new InvalidOperationException(DataStrings.ExceptionCannotResolveOperation(operationName, typeof(T).Name, value2.GetType().Name));
				}
				if ("op_Subtraction".Equals(operationName))
				{
					return (T)((object)(num3.Value - num4.Value));
				}
				if ("op_Division".Equals(operationName))
				{
					return (T)((object)(num3.Value / num4.Value));
				}
				if ("op_Multiply".Equals(operationName))
				{
					return (T)((object)(num3.Value * num4.Value));
				}
				return (T)((object)(num3.Value + num4.Value));
			}
			else
			{
				MethodInfo methodInfo = null;
				foreach (MethodInfo methodInfo2 in from x in genericTypeArguments[0].GetTypeInfo().DeclaredMethods
				where x.Name == operationName
				select x)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					if (parameters.Length == 2 && parameters[0].ParameterType == genericTypeArguments[0] && parameters[1].ParameterType == value2.GetType())
					{
						methodInfo = methodInfo2;
						break;
					}
				}
				if (methodInfo == null)
				{
					throw new InvalidOperationException(DataStrings.ExceptionCannotResolveOperation(operationName, typeof(T).Name, value2.GetType().Name));
				}
				object[] parameters2 = new object[]
				{
					value1.Value,
					value2
				};
				T result;
				try
				{
					result = (T)((object)methodInfo.Invoke(value1.Value, parameters2));
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				return result;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000104EC File Offset: 0x0000E6EC
		public static implicit operator Unlimited<T>(T fromValue)
		{
			return new Unlimited<T>(fromValue);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000104F4 File Offset: 0x0000E6F4
		public static explicit operator T(Unlimited<T> fromValue)
		{
			return fromValue.Value;
		}

		// Token: 0x040002C2 RID: 706
		private const string unlimitedString = "Unlimited";

		// Token: 0x040002C3 RID: 707
		private T limitedValue;

		// Token: 0x040002C4 RID: 708
		private bool isUnlimited;

		// Token: 0x040002C5 RID: 709
		private static Type[] ValidTypeMathOperations = new Type[]
		{
			typeof(uint),
			typeof(int),
			typeof(ulong),
			typeof(long),
			typeof(ByteQuantifiedSize)
		};
	}
}
