using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x0200004C RID: 76
	public static class TypeExtensions
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000644C File Offset: 0x0000464C
		public static Type FindGenericType(this Type typeToInspect, Type genericTypeToFind)
		{
			while (typeToInspect != null && typeToInspect != typeof(object))
			{
				if (typeToInspect.IsGenericType && typeToInspect.GetGenericTypeDefinition() == genericTypeToFind)
				{
					return typeToInspect;
				}
				if (genericTypeToFind.IsInterface)
				{
					foreach (Type typeToInspect2 in typeToInspect.GetInterfaces())
					{
						Type type = typeToInspect2.FindGenericType(genericTypeToFind);
						if (type != null)
						{
							return type;
						}
					}
				}
				typeToInspect = typeToInspect.BaseType;
			}
			return null;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000064D4 File Offset: 0x000046D4
		public static Type GetEnumerableElementTypeOrSameType(this Type typeToInspect)
		{
			Type type = typeToInspect.FindGenericType(typeof(IEnumerable<>));
			if (!(type != null))
			{
				return typeToInspect;
			}
			return type.GetGenericArguments()[0];
		}
	}
}
