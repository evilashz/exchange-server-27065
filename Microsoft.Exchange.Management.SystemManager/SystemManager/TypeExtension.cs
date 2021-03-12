using System;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000033 RID: 51
	public static class TypeExtension
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00008798 File Offset: 0x00006998
		public static PropertyInfo GetPropertyEx(this Type type, string name)
		{
			PropertyInfo propertyInfo = null;
			try
			{
				propertyInfo = type.GetProperty(name);
			}
			catch (AmbiguousMatchException)
			{
				do
				{
					propertyInfo = type.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
				}
				while (propertyInfo == null && (type = type.BaseType) != null);
			}
			return propertyInfo;
		}
	}
}
