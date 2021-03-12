using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005A0 RID: 1440
	[__DynamicallyInvokable]
	public static class CustomAttributeExtensions
	{
		// Token: 0x06004398 RID: 17304 RVA: 0x000F868A File Offset: 0x000F688A
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000F8693 File Offset: 0x000F6893
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000F869C File Offset: 0x000F689C
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000F86A5 File Offset: 0x000F68A5
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000F86AE File Offset: 0x000F68AE
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000F86C5 File Offset: 0x000F68C5
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this Module element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000F86DC File Offset: 0x000F68DC
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000F86F3 File Offset: 0x000F68F3
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000F870A File Offset: 0x000F690A
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000F8714 File Offset: 0x000F6914
		[__DynamicallyInvokable]
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000F871E File Offset: 0x000F691E
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000F8736 File Offset: 0x000F6936
		[__DynamicallyInvokable]
		public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000F874E File Offset: 0x000F694E
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000F8756 File Offset: 0x000F6956
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000F875E File Offset: 0x000F695E
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000F8766 File Offset: 0x000F6966
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000F876E File Offset: 0x000F696E
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000F8777 File Offset: 0x000F6977
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000F8780 File Offset: 0x000F6980
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000F8789 File Offset: 0x000F6989
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000F8792 File Offset: 0x000F6992
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000F879B File Offset: 0x000F699B
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000F87A4 File Offset: 0x000F69A4
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000F87BB File Offset: 0x000F69BB
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000F87D2 File Offset: 0x000F69D2
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000F87E9 File Offset: 0x000F69E9
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000F8800 File Offset: 0x000F6A00
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000F880A File Offset: 0x000F6A0A
		[__DynamicallyInvokable]
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000F8814 File Offset: 0x000F6A14
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000F882C File Offset: 0x000F6A2C
		[__DynamicallyInvokable]
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000F8844 File Offset: 0x000F6A44
		[__DynamicallyInvokable]
		public static bool IsDefined(this Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000F884D File Offset: 0x000F6A4D
		[__DynamicallyInvokable]
		public static bool IsDefined(this Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000F8856 File Offset: 0x000F6A56
		[__DynamicallyInvokable]
		public static bool IsDefined(this MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000F885F File Offset: 0x000F6A5F
		[__DynamicallyInvokable]
		public static bool IsDefined(this ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000F8868 File Offset: 0x000F6A68
		[__DynamicallyInvokable]
		public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000F8872 File Offset: 0x000F6A72
		[__DynamicallyInvokable]
		public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}
	}
}
