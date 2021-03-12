using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x0200048B RID: 1163
	internal static class ReadOnlyDictionaryHelpers
	{
		// Token: 0x060038F2 RID: 14578 RVA: 0x000D8C8C File Offset: 0x000D6E8C
		internal static void CopyToNonGenericICollectionHelper<T>(ICollection<T> collection, Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < collection.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			ICollection collection2 = collection as ICollection;
			if (collection2 != null)
			{
				collection2.CopyTo(array, index);
				return;
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				collection.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				foreach (T t in collection)
				{
					array3[index++] = t;
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}
	}
}
