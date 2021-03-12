using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000B00 RID: 2816
	internal class EnumValidator<T> : IEnumConvert where T : struct
	{
		// Token: 0x06003C87 RID: 15495 RVA: 0x0009D1D8 File Offset: 0x0009B3D8
		public static int Compare(T x, T y)
		{
			return EnumValidator<T>.compare(x, y);
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0009D1E6 File Offset: 0x0009B3E6
		public static bool IsMemberOf(T value, T[] subset)
		{
			return EnumValidator<T>.memberOf(value, subset);
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x0009D1F4 File Offset: 0x0009B3F4
		public static bool IsValidValue(T value)
		{
			return EnumValidator<T>.validate(value);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x0009D201 File Offset: 0x0009B401
		[Conditional("DEBUG")]
		public static void AssertValid(T valueToCheck)
		{
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x0009D203 File Offset: 0x0009B403
		public static void ThrowIfInvalid(T valueToCheck)
		{
			EnumValidator<T>.ThrowIfInvalid(valueToCheck, "value");
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x0009D210 File Offset: 0x0009B410
		public static void ThrowIfInvalid(T valueToCheck, string paramName)
		{
			if (!EnumValidator<T>.IsValidValue(valueToCheck))
			{
				throw new EnumOutOfRangeException(paramName, SystemStrings.BadEnumValue(EnumValidator<T>.enumType, valueToCheck));
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x0009D231 File Offset: 0x0009B431
		public static void ThrowIfInvalid(T valueToCheck, T validValue)
		{
			if (EnumValidator<T>.Compare(valueToCheck, validValue) != 0)
			{
				throw new EnumOutOfRangeException("value", SystemStrings.BadEnumValue(EnumValidator<T>.enumType, valueToCheck));
			}
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x0009D257 File Offset: 0x0009B457
		public static void ThrowIfInvalid(T valueToCheck, T[] validValues)
		{
			if (!EnumValidator<T>.IsMemberOf(valueToCheck, validValues))
			{
				throw new EnumOutOfRangeException("value", SystemStrings.BadEnumValue(EnumValidator<T>.enumType, valueToCheck));
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x0009D280 File Offset: 0x0009B480
		public static T Parse(string value, EnumParseOptions options)
		{
			T result;
			if (!EnumValidator<T>.TryParse(value, options, out result))
			{
				throw new EnumArgumentException(SystemStrings.BadEnumValue(EnumValidator<T>.enumType, value));
			}
			return result;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x0009D2AC File Offset: 0x0009B4AC
		public static bool TryParse(string value, EnumParseOptions options, out T result)
		{
			result = default(T);
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return false;
			}
			if (char.IsDigit(value[0]) || value[0] == '-' || value[0] == '+')
			{
				if ((options & EnumParseOptions.AllowNumericConstants) != EnumParseOptions.AllowNumericConstants)
				{
					return false;
				}
				long num;
				if (!long.TryParse(value, out num))
				{
					return false;
				}
				uint value2 = (uint)num;
				return EnumValidator<T>.TryParseUInt32(value2, options, out result);
			}
			else
			{
				int num2 = value.IndexOf(',');
				if (num2 == -1)
				{
					if (EnumValidator<T>.nameMap.TryGetValue(value, out num2) && ((options & EnumParseOptions.IgnoreCase) == EnumParseOptions.IgnoreCase || string.Compare(value, EnumValidator<T>.stringValues[num2], StringComparison.Ordinal) == 0))
					{
						result = EnumValidator<T>.values[num2];
						return true;
					}
					return (options & EnumParseOptions.IgnoreUnknownValues) == EnumParseOptions.IgnoreUnknownValues;
				}
				else
				{
					if (!EnumValidator<T>.isFlags)
					{
						return (options & EnumParseOptions.IgnoreUnknownValues) == EnumParseOptions.IgnoreUnknownValues;
					}
					string[] array = value.Split(new char[]
					{
						','
					});
					uint num3 = 0U;
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i].Trim();
						if (text.Length != 0)
						{
							bool flag = false;
							if (EnumValidator<T>.nameMap.TryGetValue(text, out num2) && ((options & EnumParseOptions.IgnoreCase) == EnumParseOptions.IgnoreCase || string.Compare(text, EnumValidator<T>.stringValues[num2], StringComparison.Ordinal) == 0))
							{
								flag = true;
								num3 |= EnumValidator<T>.rawValues[num2];
							}
							if (!flag && (options & EnumParseOptions.IgnoreUnknownValues) != EnumParseOptions.IgnoreUnknownValues)
							{
								return false;
							}
						}
					}
					return EnumValidator<T>.TryParseUInt32(num3, options, out result);
				}
			}
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x0009D40C File Offset: 0x0009B60C
		bool IEnumConvert.TryParse(string value, EnumParseOptions options, out object result)
		{
			T t;
			if (!EnumValidator<T>.TryParse(value, options, out t))
			{
				result = null;
				return false;
			}
			result = t;
			return true;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x0009D434 File Offset: 0x0009B634
		private static bool TryParseUInt32(uint value, EnumParseOptions options, out T result)
		{
			if ((options & EnumParseOptions.IgnoreUnknownValues) == EnumParseOptions.IgnoreUnknownValues)
			{
				result = EnumValidator<T>.convertFrom(value);
				return true;
			}
			if (!EnumValidator<T>.isFlags)
			{
				for (int i = 0; i < EnumValidator<T>.rawValues.Length; i++)
				{
					if (value == EnumValidator<T>.rawValues[i])
					{
						result = EnumValidator<T>.values[i];
						return true;
					}
				}
				result = default(T);
				return false;
			}
			if ((value & ~(EnumValidator<T>.allBits != 0U)) != 0U)
			{
				result = default(T);
				return false;
			}
			result = EnumValidator<T>.convertFrom(value);
			return EnumValidator<T>.IsValidValue(result);
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x0009D4C8 File Offset: 0x0009B6C8
		private static Delegate CreateEnumValidator(Type enumType, Type delegateType)
		{
			if (!enumType.IsEnum)
			{
				throw new EnumArgumentException(SystemStrings.InvalidTypeParam(enumType), "enumType");
			}
			switch (Type.GetTypeCode(enumType))
			{
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			{
				Attribute customAttribute = Attribute.GetCustomAttribute(enumType, typeof(FlagsAttribute));
				bool flag = customAttribute != null;
				EnumValidator<T>.isFlags = flag;
				uint[] array = EnumValidator<T>.rawValues;
				DynamicMethod dynamicMethod = new DynamicMethod("IsValidEnum", typeof(bool), new Type[]
				{
					enumType
				}, typeof(EnumValidator<T>).Module);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				if (flag)
				{
					uint num = 0U;
					bool flag2 = false;
					uint num2 = (array.Length > 0) ? array[0] : 1U;
					foreach (uint num3 in array)
					{
						uint num4 = num3;
						if (num4 == 0U)
						{
							flag2 = true;
						}
						num |= num4;
					}
					EnumValidator<T>.allBits = num;
					if (flag2)
					{
						Label label = ilgenerator.DefineLabel();
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Brtrue_S, label);
						ilgenerator.Emit(OpCodes.Ldc_I4_1);
						ilgenerator.Emit(OpCodes.Ret);
						ilgenerator.MarkLabel(label);
					}
					else if (num2 == 1U)
					{
						Label label2 = ilgenerator.DefineLabel();
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Ldc_I4_0);
						ilgenerator.Emit(OpCodes.Bgt_Un_S, label2);
						ilgenerator.Emit(OpCodes.Ldc_I4_0);
						ilgenerator.Emit(OpCodes.Ret);
						ilgenerator.MarkLabel(label2);
					}
					else if (num2 > 1U)
					{
						Label label3 = ilgenerator.DefineLabel();
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Ldc_I4, (long)((ulong)num2));
						ilgenerator.Emit(OpCodes.Bge_Un_S, label3);
						ilgenerator.Emit(OpCodes.Ldc_I4_0);
						ilgenerator.Emit(OpCodes.Ret);
						ilgenerator.MarkLabel(label3);
					}
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldc_I4, (long)((ulong)(~(ulong)num)));
					ilgenerator.Emit(OpCodes.And);
					ilgenerator.Emit(OpCodes.Ldc_I4_0);
					ilgenerator.Emit(OpCodes.Ceq);
					ilgenerator.Emit(OpCodes.Ret);
				}
				else
				{
					ilgenerator.DeclareLocal(typeof(uint));
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Stloc_0);
					Label label4 = ilgenerator.DefineLabel();
					Label label5 = ilgenerator.DefineLabel();
					uint num5 = array[0];
					int j = 0;
					while (j < array.Length)
					{
						int num6 = EnumValidator<T>.FindRange(array, j, array.Length - j);
						if (num6 > 1)
						{
							ilgenerator.Emit(OpCodes.Ldloc_0);
							num5 = array[j];
							uint num7 = array[j + num6 - 1];
							if (num5 != 0U)
							{
								ilgenerator.Emit(OpCodes.Ldc_I4, (long)((ulong)num5));
								ilgenerator.Emit(OpCodes.Sub);
							}
							Label[] array3 = new Label[num7 - num5 + 1U];
							int num8 = 0;
							array3[num8++] = label5;
							j++;
							uint num9 = num5;
							int k = 1;
							while (k < num6)
							{
								uint num10 = array[j];
								if (num10 != num9)
								{
									while ((ulong)(num10 - num5) > (ulong)((long)num8))
									{
										array3[num8++] = label4;
									}
									array3[num8++] = label5;
									num9 = num10;
								}
								k++;
								j++;
							}
							ilgenerator.Emit(OpCodes.Switch, array3);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldloc_0);
							uint num11 = array[j++];
							ilgenerator.Emit(OpCodes.Ldc_I4, (long)((ulong)num11));
							ilgenerator.Emit(OpCodes.Beq, label5);
						}
					}
					ilgenerator.MarkLabel(label4);
					ilgenerator.Emit(OpCodes.Ldc_I4_0);
					ilgenerator.Emit(OpCodes.Ret);
					ilgenerator.MarkLabel(label5);
					ilgenerator.Emit(OpCodes.Ldc_I4_1);
					ilgenerator.Emit(OpCodes.Ret);
				}
				return dynamicMethod.CreateDelegate(delegateType);
			}
			default:
				throw new EnumArgumentException(SystemStrings.InvalidBaseType(enumType), "enumType");
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x0009D8F4 File Offset: 0x0009BAF4
		private static int FindRange(uint[] values, int start, int count)
		{
			uint num = values[start];
			int num2 = 1;
			for (int i = 1; i < count; i++)
			{
				uint num3 = values[start + i];
				if (num3 - num > 4U)
				{
					return num2;
				}
				num2++;
				num = num3;
			}
			return num2;
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x0009D928 File Offset: 0x0009BB28
		private static uint[] GetValues(T[] values)
		{
			uint[] array = new uint[values.Length];
			switch (Type.GetTypeCode(EnumValidator<T>.enumType))
			{
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = (uint)Convert.ToInt32(values[i]);
				}
				break;
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
				for (int j = 0; j < values.Length; j++)
				{
					array[j] = Convert.ToUInt32(values[j]);
				}
				break;
			default:
				throw new EnumArgumentException(SystemStrings.InvalidBaseType(EnumValidator<T>.enumType), "enumType");
			}
			return array;
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x0009D9CC File Offset: 0x0009BBCC
		private static Dictionary<string, int> GetNameMap(string[] names)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(names.Length, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < names.Length; i++)
			{
				dictionary.Add(names[i], i);
			}
			return dictionary;
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x0009DA00 File Offset: 0x0009BC00
		private static Delegate CreateSetValidator(Type enumType, Type delegateType)
		{
			if (!enumType.IsEnum)
			{
				throw new EnumArgumentException(SystemStrings.InvalidTypeParam(enumType), "enumType");
			}
			OpCode opcode;
			switch (Type.GetTypeCode(enumType))
			{
			case TypeCode.SByte:
				opcode = OpCodes.Ldelem_I1;
				goto IL_8D;
			case TypeCode.Byte:
				opcode = OpCodes.Ldelem_U1;
				goto IL_8D;
			case TypeCode.Int16:
				opcode = OpCodes.Ldelem_I2;
				goto IL_8D;
			case TypeCode.UInt16:
				opcode = OpCodes.Ldelem_U2;
				goto IL_8D;
			case TypeCode.Int32:
				opcode = OpCodes.Ldelem_I4;
				goto IL_8D;
			case TypeCode.UInt32:
				opcode = OpCodes.Ldelem_U4;
				goto IL_8D;
			}
			throw new EnumArgumentException(SystemStrings.InvalidBaseType(enumType), "enumType");
			IL_8D:
			DynamicMethod dynamicMethod = new DynamicMethod("IsMemberOf", typeof(bool), new Type[]
			{
				enumType,
				typeof(T[])
			}, typeof(EnumValidator<T>).Module);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.DeclareLocal(typeof(uint));
			Label label = ilgenerator.DefineLabel();
			Label label2 = ilgenerator.DefineLabel();
			Label label3 = ilgenerator.DefineLabel();
			Label label4 = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Brtrue_S, label);
			ilgenerator.Emit(OpCodes.Ldstr, "values");
			ConstructorInfo constructor = typeof(ArgumentNullException).GetConstructor(new Type[]
			{
				typeof(string)
			});
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Throw);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Stloc_0);
			ilgenerator.Emit(OpCodes.Br_S, label2);
			ilgenerator.MarkLabel(label4);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(opcode);
			ilgenerator.Emit(OpCodes.Bne_Un_S, label3);
			ilgenerator.Emit(OpCodes.Ldc_I4_1);
			ilgenerator.Emit(OpCodes.Ret);
			ilgenerator.MarkLabel(label3);
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Ldc_I4_1);
			ilgenerator.Emit(OpCodes.Add);
			ilgenerator.Emit(OpCodes.Stloc_0);
			ilgenerator.MarkLabel(label2);
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Ldlen);
			ilgenerator.Emit(OpCodes.Conv_I4);
			ilgenerator.Emit(OpCodes.Blt_S, label4);
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod.CreateDelegate(delegateType);
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x0009DC90 File Offset: 0x0009BE90
		private static Delegate CreateCastFunction(Type enumType, Type delegateType)
		{
			DynamicMethod dynamicMethod = new DynamicMethod("CastFunction", enumType, new Type[]
			{
				typeof(uint)
			}, typeof(EnumValidator<T>).Module);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod.CreateDelegate(delegateType);
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x0009DCF4 File Offset: 0x0009BEF4
		private static Delegate CreateCompareFunction(Type enumType, Type delegateType)
		{
			DynamicMethod dynamicMethod = new DynamicMethod("CompareFunction", typeof(int), new Type[]
			{
				enumType,
				enumType
			}, typeof(EnumValidator<T>).Module);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Sub);
			ilgenerator.Emit(OpCodes.Conv_I4);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod.CreateDelegate(delegateType);
		}

		// Token: 0x0400353C RID: 13628
		private const string DefaultParameterName = "value";

		// Token: 0x0400353D RID: 13629
		private static Type enumType = typeof(T);

		// Token: 0x0400353E RID: 13630
		private static bool isFlags;

		// Token: 0x0400353F RID: 13631
		private static uint allBits;

		// Token: 0x04003540 RID: 13632
		private static T[] values = (T[])Enum.GetValues(EnumValidator<T>.enumType);

		// Token: 0x04003541 RID: 13633
		private static uint[] rawValues = EnumValidator<T>.GetValues(EnumValidator<T>.values);

		// Token: 0x04003542 RID: 13634
		private static string[] stringValues = Enum.GetNames(EnumValidator<T>.enumType);

		// Token: 0x04003543 RID: 13635
		private static Dictionary<string, int> nameMap = EnumValidator<T>.GetNameMap(EnumValidator<T>.stringValues);

		// Token: 0x04003544 RID: 13636
		private static Converter<uint, T> convertFrom = (Converter<uint, T>)EnumValidator<T>.CreateCastFunction(EnumValidator<T>.enumType, typeof(Converter<uint, T>));

		// Token: 0x04003545 RID: 13637
		private static Comparison<T> compare = (Comparison<T>)EnumValidator<T>.CreateCompareFunction(EnumValidator<T>.enumType, typeof(Comparison<T>));

		// Token: 0x04003546 RID: 13638
		private static Predicate<T> validate = (Predicate<T>)EnumValidator<T>.CreateEnumValidator(EnumValidator<T>.enumType, typeof(Predicate<T>));

		// Token: 0x04003547 RID: 13639
		private static EnumValidator<T>.SetTest memberOf = (EnumValidator<T>.SetTest)EnumValidator<T>.CreateSetValidator(EnumValidator<T>.enumType, typeof(EnumValidator<T>.SetTest));

		// Token: 0x02000B01 RID: 2817
		// (Invoke) Token: 0x06003C9D RID: 15517
		private delegate bool SetTest(T value, T[] set);
	}
}
