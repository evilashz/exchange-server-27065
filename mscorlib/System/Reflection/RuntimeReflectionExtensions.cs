using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005C0 RID: 1472
	[__DynamicallyInvokable]
	public static class RuntimeReflectionExtensions
	{
		// Token: 0x06004534 RID: 17716 RVA: 0x000FDB1B File Offset: 0x000FBD1B
		private static void CheckAndThrow(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(t is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000FDB49 File Offset: 0x000FBD49
		private static void CheckAndThrow(MethodInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!(m is RuntimeMethodInfo))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000FDB77 File Offset: 0x000FBD77
		[__DynamicallyInvokable]
		public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x000FDB87 File Offset: 0x000FBD87
		[__DynamicallyInvokable]
		public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x000FDB97 File Offset: 0x000FBD97
		[__DynamicallyInvokable]
		public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x000FDBA7 File Offset: 0x000FBDA7
		[__DynamicallyInvokable]
		public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x000FDBB7 File Offset: 0x000FBDB7
		[__DynamicallyInvokable]
		public static PropertyInfo GetRuntimeProperty(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetProperty(name);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x000FDBC6 File Offset: 0x000FBDC6
		[__DynamicallyInvokable]
		public static EventInfo GetRuntimeEvent(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetEvent(name);
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000FDBD5 File Offset: 0x000FBDD5
		[__DynamicallyInvokable]
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetMethod(name, parameters);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000FDBE5 File Offset: 0x000FBDE5
		[__DynamicallyInvokable]
		public static FieldInfo GetRuntimeField(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetField(name);
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000FDBF4 File Offset: 0x000FBDF4
		[__DynamicallyInvokable]
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			RuntimeReflectionExtensions.CheckAndThrow(method);
			return method.GetBaseDefinition();
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000FDC02 File Offset: 0x000FBE02
		[__DynamicallyInvokable]
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			if (!(typeInfo is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000FDC37 File Offset: 0x000FBE37
		[__DynamicallyInvokable]
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}

		// Token: 0x04001C13 RID: 7187
		private const BindingFlags everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
