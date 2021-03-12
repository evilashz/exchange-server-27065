using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000AFF RID: 2815
	public static class EnumValidator
	{
		// Token: 0x06003C7C RID: 15484 RVA: 0x0009CFD7 File Offset: 0x0009B1D7
		public static void AssertValid<T>(T valueToCheck) where T : struct
		{
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x0009CFD9 File Offset: 0x0009B1D9
		public static void ThrowIfInvalid<T>(T valueToCheck) where T : struct
		{
			EnumValidator<T>.ThrowIfInvalid(valueToCheck);
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x0009CFE1 File Offset: 0x0009B1E1
		public static void ThrowIfInvalid<T>(T valueToCheck, string paramName) where T : struct
		{
			EnumValidator<T>.ThrowIfInvalid(valueToCheck, paramName);
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x0009CFEA File Offset: 0x0009B1EA
		public static void ThrowIfInvalid<T>(T valueToCheck, T validValue) where T : struct
		{
			EnumValidator<T>.ThrowIfInvalid(valueToCheck, validValue);
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x0009CFF3 File Offset: 0x0009B1F3
		public static void ThrowIfInvalid<T>(T valueToCheck, T[] validValues) where T : struct
		{
			EnumValidator<T>.ThrowIfInvalid(valueToCheck, validValues);
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x0009CFFC File Offset: 0x0009B1FC
		public static bool IsValidValue<T>(T valueToCheck) where T : struct
		{
			return EnumValidator<T>.IsValidValue(valueToCheck);
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x0009D004 File Offset: 0x0009B204
		public static bool TryParse<T>(string value, EnumParseOptions options, out T result)
		{
			object obj = null;
			if (EnumValidator.TryParse(typeof(T), value, options, out obj))
			{
				result = (T)((object)obj);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x0009D040 File Offset: 0x0009B240
		public static bool TryParse(Type enumType, string value, EnumParseOptions options, out object result)
		{
			if (!enumType.IsEnum)
			{
				throw new EnumArgumentException(SystemStrings.InvalidTypeParam(enumType));
			}
			IEnumConvert converter = EnumValidator.GetConverter(enumType);
			return converter.TryParse(value, options, out result);
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x0009D074 File Offset: 0x0009B274
		public static object Parse(Type enumType, string value, EnumParseOptions options)
		{
			object result;
			if (EnumValidator.TryParse(enumType, value, options, out result))
			{
				return result;
			}
			throw new EnumArgumentException(SystemStrings.BadEnumValue(enumType, value));
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x0009D09C File Offset: 0x0009B29C
		private static IEnumConvert GetConverter(Type enumType)
		{
			IEnumConvert enumConvert;
			try
			{
				EnumValidator.cacheLock.EnterReadLock();
				EnumValidator.typeMap.TryGetValue(enumType, out enumConvert);
			}
			finally
			{
				try
				{
					EnumValidator.cacheLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (enumConvert == null)
			{
				Type type = EnumValidator.genericType.MakeGenericType(new Type[]
				{
					enumType
				});
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				if (constructor == null)
				{
					throw new InvalidOperationException("Unsupported EnumValidator type: " + enumType.ToString());
				}
				object obj = constructor.Invoke(null);
				enumConvert = (obj as IEnumConvert);
				try
				{
					if (EnumValidator.cacheLock.TryEnterWriteLock(EnumValidator.cacheTimeout) && !EnumValidator.typeMap.ContainsKey(enumType))
					{
						EnumValidator.typeMap.Add(enumType, enumConvert);
					}
				}
				finally
				{
					try
					{
						EnumValidator.cacheLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			return enumConvert;
		}

		// Token: 0x04003538 RID: 13624
		private static Type genericType = typeof(EnumValidator<>);

		// Token: 0x04003539 RID: 13625
		private static Dictionary<Type, IEnumConvert> typeMap = new Dictionary<Type, IEnumConvert>();

		// Token: 0x0400353A RID: 13626
		private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

		// Token: 0x0400353B RID: 13627
		private static TimeSpan cacheTimeout = TimeSpan.FromSeconds(10.0);
	}
}
