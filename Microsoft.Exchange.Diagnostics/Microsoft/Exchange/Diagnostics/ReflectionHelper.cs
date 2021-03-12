using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200006B RID: 107
	public static class ReflectionHelper
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00006560 File Offset: 0x00004760
		public static TResult TraverseTypeHierarchy<TResult, TParam>(Type type, TParam param, MatchType<TResult, TParam> matchType) where TResult : class
		{
			TResult tresult = default(TResult);
			Type baseType = type;
			while (tresult == null && type != null)
			{
				tresult = matchType(baseType, type, param);
				type = type.GetTypeInfo().BaseType;
			}
			return tresult;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000065A4 File Offset: 0x000047A4
		public static List<TResult> AggregateTypeHierarchy<TResult>(Type type, AggregateType<TResult> aggregateType) where TResult : class
		{
			List<TResult> list = new List<TResult>();
			Type baseType = type;
			while (type != null)
			{
				aggregateType(baseType, type, list);
				type = type.GetTypeInfo().BaseType;
			}
			return list;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000065DC File Offset: 0x000047DC
		public static FieldInfo MatchStaticField(Type baseType, Type type, string fieldName)
		{
			FieldInfo declaredField = type.GetTypeInfo().GetDeclaredField(fieldName);
			if (declaredField != null && !declaredField.IsPublic && declaredField.IsStatic && (!declaredField.IsPrivate || (declaredField.IsPrivate && baseType == type)))
			{
				return declaredField;
			}
			return null;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000662C File Offset: 0x0000482C
		public static FieldInfo MatchInstanceField(Type baseType, Type type, string fieldName)
		{
			FieldInfo declaredField = type.GetTypeInfo().GetDeclaredField(fieldName);
			if (declaredField != null && !declaredField.IsPublic && !declaredField.IsStatic && (!declaredField.IsPrivate || (declaredField.IsPrivate && baseType == type)))
			{
				return declaredField;
			}
			return null;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000667C File Offset: 0x0000487C
		public static void AggregateStaticFields(Type baseType, Type type, List<FieldInfo> fields)
		{
			foreach (FieldInfo fieldInfo in type.GetTypeInfo().DeclaredFields)
			{
				if (fieldInfo.IsStatic && (!fieldInfo.IsPrivate || (fieldInfo.IsPrivate && baseType == type)))
				{
					fields.Add(fieldInfo);
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000066F0 File Offset: 0x000048F0
		public static bool IsInstanceOfType(object instanceToCheck, Type typeToCheckAgainst)
		{
			bool result = false;
			if (instanceToCheck != null && typeToCheckAgainst != null)
			{
				result = typeToCheckAgainst.GetTypeInfo().IsAssignableFrom(instanceToCheck.GetType().GetTypeInfo());
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006724 File Offset: 0x00004924
		public static bool HasParameterlessConstructor(Type typeToCheck)
		{
			bool result = false;
			foreach (ConstructorInfo constructorInfo in typeToCheck.GetTypeInfo().DeclaredConstructors)
			{
				if (constructorInfo.GetParameters().Length == 0)
				{
					result = (constructorInfo.IsPublic || constructorInfo.IsStatic);
					break;
				}
			}
			return result;
		}
	}
}
