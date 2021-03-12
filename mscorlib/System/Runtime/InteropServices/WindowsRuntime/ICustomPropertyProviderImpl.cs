using System;
using System.Reflection;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DF RID: 2527
	internal static class ICustomPropertyProviderImpl
	{
		// Token: 0x06006468 RID: 25704 RVA: 0x001549A8 File Offset: 0x00152BA8
		internal static ICustomProperty CreateProperty(object target, string propertyName)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			if (property == null)
			{
				return null;
			}
			return new CustomPropertyImpl(property);
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x001549D8 File Offset: 0x00152BD8
		[SecurityCritical]
		internal unsafe static ICustomProperty CreateIndexedProperty(object target, string propertyName, TypeNameNative* pIndexedParamType)
		{
			Type indexedParamType = null;
			SystemTypeMarshaler.ConvertToManaged(pIndexedParamType, ref indexedParamType);
			return ICustomPropertyProviderImpl.CreateIndexedProperty(target, propertyName, indexedParamType);
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x001549F8 File Offset: 0x00152BF8
		internal static ICustomProperty CreateIndexedProperty(object target, string propertyName, Type indexedParamType)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, new Type[]
			{
				indexedParamType
			}, null);
			if (property == null)
			{
				return null;
			}
			return new CustomPropertyImpl(property);
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00154A32 File Offset: 0x00152C32
		[SecurityCritical]
		internal unsafe static void GetType(object target, TypeNameNative* pIndexedParamType)
		{
			SystemTypeMarshaler.ConvertToNative(target.GetType(), pIndexedParamType);
		}
	}
}
