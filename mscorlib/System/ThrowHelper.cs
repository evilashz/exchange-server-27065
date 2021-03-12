﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x0200005B RID: 91
	internal static class ThrowHelper
	{
		// Token: 0x06000335 RID: 821 RVA: 0x000080A5 File Offset: 0x000062A5
		internal static void ThrowArgumentOutOfRangeException()
		{
			ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000080B0 File Offset: 0x000062B0
		internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_WrongType", new object[]
			{
				key,
				targetType
			}), "key");
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000080D4 File Offset: 0x000062D4
		internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_WrongType", new object[]
			{
				value,
				targetType
			}), "value");
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000080F8 File Offset: 0x000062F8
		internal static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000080FF File Offset: 0x000062FF
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008111 File Offset: 0x00006311
		internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)), ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008129 File Offset: 0x00006329
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008136 File Offset: 0x00006336
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008143 File Offset: 0x00006343
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), string.Empty);
			}
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008173 File Offset: 0x00006373
		internal static void ThrowInvalidOperationException(ExceptionResource resource)
		{
			throw new InvalidOperationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008185 File Offset: 0x00006385
		internal static void ThrowSerializationException(ExceptionResource resource)
		{
			throw new SerializationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00008197 File Offset: 0x00006397
		internal static void ThrowSecurityException(ExceptionResource resource)
		{
			throw new SecurityException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000081A9 File Offset: 0x000063A9
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000081BB File Offset: 0x000063BB
		internal static void ThrowUnauthorizedAccessException(ExceptionResource resource)
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000081CD File Offset: 0x000063CD
		internal static void ThrowObjectDisposedException(string objectName, ExceptionResource resource)
		{
			throw new ObjectDisposedException(objectName, Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000081E0 File Offset: 0x000063E0
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (value == null && default(T) != null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008208 File Offset: 0x00006408
		internal static string GetArgumentName(ExceptionArgument argument)
		{
			string result;
			switch (argument)
			{
			case ExceptionArgument.obj:
				result = "obj";
				break;
			case ExceptionArgument.dictionary:
				result = "dictionary";
				break;
			case ExceptionArgument.dictionaryCreationThreshold:
				result = "dictionaryCreationThreshold";
				break;
			case ExceptionArgument.array:
				result = "array";
				break;
			case ExceptionArgument.info:
				result = "info";
				break;
			case ExceptionArgument.key:
				result = "key";
				break;
			case ExceptionArgument.collection:
				result = "collection";
				break;
			case ExceptionArgument.list:
				result = "list";
				break;
			case ExceptionArgument.match:
				result = "match";
				break;
			case ExceptionArgument.converter:
				result = "converter";
				break;
			case ExceptionArgument.queue:
				result = "queue";
				break;
			case ExceptionArgument.stack:
				result = "stack";
				break;
			case ExceptionArgument.capacity:
				result = "capacity";
				break;
			case ExceptionArgument.index:
				result = "index";
				break;
			case ExceptionArgument.startIndex:
				result = "startIndex";
				break;
			case ExceptionArgument.value:
				result = "value";
				break;
			case ExceptionArgument.count:
				result = "count";
				break;
			case ExceptionArgument.arrayIndex:
				result = "arrayIndex";
				break;
			case ExceptionArgument.name:
				result = "name";
				break;
			case ExceptionArgument.mode:
				result = "mode";
				break;
			case ExceptionArgument.item:
				result = "item";
				break;
			case ExceptionArgument.options:
				result = "options";
				break;
			case ExceptionArgument.view:
				result = "view";
				break;
			case ExceptionArgument.sourceBytesToCopy:
				result = "sourceBytesToCopy";
				break;
			default:
				return string.Empty;
			}
			return result;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008364 File Offset: 0x00006564
		internal static string GetResourceName(ExceptionResource resource)
		{
			string result;
			switch (resource)
			{
			case ExceptionResource.Argument_ImplementIComparable:
				result = "Argument_ImplementIComparable";
				break;
			case ExceptionResource.Argument_InvalidType:
				result = "Argument_InvalidType";
				break;
			case ExceptionResource.Argument_InvalidArgumentForComparison:
				result = "Argument_InvalidArgumentForComparison";
				break;
			case ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck:
				result = "Argument_InvalidRegistryKeyPermissionCheck";
				break;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				result = "ArgumentOutOfRange_NeedNonNegNum";
				break;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				result = "Arg_ArrayPlusOffTooSmall";
				break;
			case ExceptionResource.Arg_NonZeroLowerBound:
				result = "Arg_NonZeroLowerBound";
				break;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				result = "Arg_RankMultiDimNotSupported";
				break;
			case ExceptionResource.Arg_RegKeyDelHive:
				result = "Arg_RegKeyDelHive";
				break;
			case ExceptionResource.Arg_RegKeyStrLenBug:
				result = "Arg_RegKeyStrLenBug";
				break;
			case ExceptionResource.Arg_RegSetStrArrNull:
				result = "Arg_RegSetStrArrNull";
				break;
			case ExceptionResource.Arg_RegSetMismatchedKind:
				result = "Arg_RegSetMismatchedKind";
				break;
			case ExceptionResource.Arg_RegSubKeyAbsent:
				result = "Arg_RegSubKeyAbsent";
				break;
			case ExceptionResource.Arg_RegSubKeyValueAbsent:
				result = "Arg_RegSubKeyValueAbsent";
				break;
			case ExceptionResource.Argument_AddingDuplicate:
				result = "Argument_AddingDuplicate";
				break;
			case ExceptionResource.Serialization_InvalidOnDeser:
				result = "Serialization_InvalidOnDeser";
				break;
			case ExceptionResource.Serialization_MissingKeys:
				result = "Serialization_MissingKeys";
				break;
			case ExceptionResource.Serialization_NullKey:
				result = "Serialization_NullKey";
				break;
			case ExceptionResource.Argument_InvalidArrayType:
				result = "Argument_InvalidArrayType";
				break;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				result = "NotSupported_KeyCollectionSet";
				break;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				result = "NotSupported_ValueCollectionSet";
				break;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				result = "ArgumentOutOfRange_SmallCapacity";
				break;
			case ExceptionResource.ArgumentOutOfRange_Index:
				result = "ArgumentOutOfRange_Index";
				break;
			case ExceptionResource.Argument_InvalidOffLen:
				result = "Argument_InvalidOffLen";
				break;
			case ExceptionResource.Argument_ItemNotExist:
				result = "Argument_ItemNotExist";
				break;
			case ExceptionResource.ArgumentOutOfRange_Count:
				result = "ArgumentOutOfRange_Count";
				break;
			case ExceptionResource.ArgumentOutOfRange_InvalidThreshold:
				result = "ArgumentOutOfRange_InvalidThreshold";
				break;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				result = "ArgumentOutOfRange_ListInsert";
				break;
			case ExceptionResource.NotSupported_ReadOnlyCollection:
				result = "NotSupported_ReadOnlyCollection";
				break;
			case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
				result = "InvalidOperation_CannotRemoveFromStackOrQueue";
				break;
			case ExceptionResource.InvalidOperation_EmptyQueue:
				result = "InvalidOperation_EmptyQueue";
				break;
			case ExceptionResource.InvalidOperation_EnumOpCantHappen:
				result = "InvalidOperation_EnumOpCantHappen";
				break;
			case ExceptionResource.InvalidOperation_EnumFailedVersion:
				result = "InvalidOperation_EnumFailedVersion";
				break;
			case ExceptionResource.InvalidOperation_EmptyStack:
				result = "InvalidOperation_EmptyStack";
				break;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				result = "ArgumentOutOfRange_BiggerThanCollection";
				break;
			case ExceptionResource.InvalidOperation_EnumNotStarted:
				result = "InvalidOperation_EnumNotStarted";
				break;
			case ExceptionResource.InvalidOperation_EnumEnded:
				result = "InvalidOperation_EnumEnded";
				break;
			case ExceptionResource.NotSupported_SortedListNestedWrite:
				result = "NotSupported_SortedListNestedWrite";
				break;
			case ExceptionResource.InvalidOperation_NoValue:
				result = "InvalidOperation_NoValue";
				break;
			case ExceptionResource.InvalidOperation_RegRemoveSubKey:
				result = "InvalidOperation_RegRemoveSubKey";
				break;
			case ExceptionResource.Security_RegistryPermission:
				result = "Security_RegistryPermission";
				break;
			case ExceptionResource.UnauthorizedAccess_RegistryNoWrite:
				result = "UnauthorizedAccess_RegistryNoWrite";
				break;
			case ExceptionResource.ObjectDisposed_RegKeyClosed:
				result = "ObjectDisposed_RegKeyClosed";
				break;
			case ExceptionResource.NotSupported_InComparableType:
				result = "NotSupported_InComparableType";
				break;
			case ExceptionResource.Argument_InvalidRegistryOptionsCheck:
				result = "Argument_InvalidRegistryOptionsCheck";
				break;
			case ExceptionResource.Argument_InvalidRegistryViewCheck:
				result = "Argument_InvalidRegistryViewCheck";
				break;
			default:
				return string.Empty;
			}
			return result;
		}
	}
}
