using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200003F RID: 63
	internal static class ExtensionMethods
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00005E90 File Offset: 0x00004090
		public static bool TryGet<TValue>(this IPropertyBag propertyBag, ContextProperty property, out TValue value)
		{
			if (!typeof(TValue).IsAssignableFrom(property.Type))
			{
				throw new InvalidOperationException(string.Format("Property {0} of type {1} cannot be retrieved as type {2}", property, property.Type, typeof(TValue)));
			}
			object obj;
			if (propertyBag.TryGet(property, out obj))
			{
				value = (TValue)((object)obj);
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005EF6 File Offset: 0x000040F6
		public static bool TryGet<TValue>(this IPropertyBag propertyBag, ContextProperty<TValue> property, out TValue value)
		{
			return propertyBag.TryGet(property, out value);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005F00 File Offset: 0x00004100
		public static TValue GetOrDefault<TValue>(this IPropertyBag propertyBag, ContextProperty<TValue> property) where TValue : class
		{
			TValue result;
			if (!propertyBag.TryGet(property, out result))
			{
				return default(TValue);
			}
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005F24 File Offset: 0x00004124
		public static TValue? GetNullableOrDefault<TValue>(this IPropertyBag propertyBag, ContextProperty<TValue> property) where TValue : struct
		{
			TValue value;
			if (!propertyBag.TryGet(property, out value))
			{
				return null;
			}
			return new TValue?(value);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005F4C File Offset: 0x0000414C
		public static TValue Get<TValue>(this IPropertyBag propertyBag, ContextProperty<TValue> property)
		{
			TValue result;
			if (propertyBag.TryGet(property, out result))
			{
				return result;
			}
			throw new PropertyNotFoundException(property.ToString());
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005F74 File Offset: 0x00004174
		public static bool IsPropertyFound(this IPropertyBag propertyBag, ContextProperty property)
		{
			object obj;
			return propertyBag.TryGet(property, out obj);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005F8C File Offset: 0x0000418C
		public static T[] Concat<T>(this T[] x, params T[] y)
		{
			T[] array = new T[x.Length + y.Length];
			Array.Copy(x, 0, array, 0, x.Length);
			Array.Copy(y, 0, array, x.Length, y.Length);
			return array;
		}
	}
}
