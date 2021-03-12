using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005B2 RID: 1458
	internal static class PseudoCustomAttribute
	{
		// Token: 0x0600447C RID: 17532
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes);

		// Token: 0x0600447D RID: 17533 RVA: 0x000FBBF0 File Offset: 0x000F9DF0
		[SecurityCritical]
		internal static void GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes)
		{
			PseudoCustomAttribute._GetSecurityAttributes(module.GetNativeHandle(), token, assembly, out securityAttributes);
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000FBC00 File Offset: 0x000F9E00
		[SecurityCritical]
		static PseudoCustomAttribute()
		{
			RuntimeType[] array = new RuntimeType[]
			{
				typeof(FieldOffsetAttribute) as RuntimeType,
				typeof(SerializableAttribute) as RuntimeType,
				typeof(MarshalAsAttribute) as RuntimeType,
				typeof(ComImportAttribute) as RuntimeType,
				typeof(NonSerializedAttribute) as RuntimeType,
				typeof(InAttribute) as RuntimeType,
				typeof(OutAttribute) as RuntimeType,
				typeof(OptionalAttribute) as RuntimeType,
				typeof(DllImportAttribute) as RuntimeType,
				typeof(PreserveSigAttribute) as RuntimeType,
				typeof(TypeForwardedToAttribute) as RuntimeType
			};
			PseudoCustomAttribute.s_pcasCount = array.Length;
			Dictionary<RuntimeType, RuntimeType> dictionary = new Dictionary<RuntimeType, RuntimeType>(PseudoCustomAttribute.s_pcasCount);
			for (int i = 0; i < PseudoCustomAttribute.s_pcasCount; i++)
			{
				dictionary[array[i]] = array[i];
			}
			PseudoCustomAttribute.s_pca = dictionary;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x000FBD14 File Offset: 0x000F9F14
		[SecurityCritical]
		[Conditional("_DEBUG")]
		private static void VerifyPseudoCustomAttribute(RuntimeType pca)
		{
			AttributeUsageAttribute attributeUsage = CustomAttribute.GetAttributeUsage(pca);
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000FBD28 File Offset: 0x000F9F28
		internal static bool IsSecurityAttribute(RuntimeType type)
		{
			return type == (RuntimeType)typeof(SecurityAttribute) || type.IsSubclassOf(typeof(SecurityAttribute));
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000FBD54 File Offset: 0x000F9F54
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (flag || caType == (RuntimeType)typeof(SerializableAttribute))
			{
				Attribute customAttribute = SerializableAttribute.GetCustomAttribute(type);
				if (customAttribute != null)
				{
					list.Add(customAttribute);
				}
			}
			if (flag || caType == (RuntimeType)typeof(ComImportAttribute))
			{
				Attribute customAttribute = ComImportAttribute.GetCustomAttribute(type);
				if (customAttribute != null)
				{
					list.Add(customAttribute);
				}
			}
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && !type.IsGenericParameter && type.GetElementType() == null)
			{
				if (type.IsGenericType)
				{
					type = (RuntimeType)type.GetGenericTypeDefinition();
				}
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(type.Module.ModuleHandle.GetRuntimeModule(), type.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x000FBED8 File Offset: 0x000FA0D8
		[SecurityCritical]
		internal static bool IsDefined(RuntimeType type, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null) || PseudoCustomAttribute.IsSecurityAttribute(caType)) && (((flag || caType == (RuntimeType)typeof(SerializableAttribute)) && SerializableAttribute.IsDefined(type)) || ((flag || caType == (RuntimeType)typeof(ComImportAttribute)) && ComImportAttribute.IsDefined(type)) || ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(type, caType, true, out num).Length != 0));
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000FBF98 File Offset: 0x000FA198
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (flag || caType == (RuntimeType)typeof(DllImportAttribute))
			{
				Attribute customAttribute = DllImportAttribute.GetCustomAttribute(method);
				if (customAttribute != null)
				{
					list.Add(customAttribute);
				}
			}
			if (flag || caType == (RuntimeType)typeof(PreserveSigAttribute))
			{
				Attribute customAttribute = PreserveSigAttribute.GetCustomAttribute(method);
				if (customAttribute != null)
				{
					list.Add(customAttribute);
				}
			}
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(method.Module.ModuleHandle.GetRuntimeModule(), method.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x000FC0E4 File Offset: 0x000FA2E4
		[SecurityCritical]
		internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(DllImportAttribute)) && DllImportAttribute.IsDefined(method)) || ((flag || caType == (RuntimeType)typeof(PreserveSigAttribute)) && PreserveSigAttribute.IsDefined(method)) || ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(method, caType, true, out num).Length != 0));
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000FC19C File Offset: 0x000FA39C
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)
			{
				return null;
			}
			Attribute[] array = new Attribute[PseudoCustomAttribute.s_pcasCount];
			if (flag || caType == (RuntimeType)typeof(InAttribute))
			{
				Attribute customAttribute = InAttribute.GetCustomAttribute(parameter);
				if (customAttribute != null)
				{
					Attribute[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = customAttribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(OutAttribute))
			{
				Attribute customAttribute = OutAttribute.GetCustomAttribute(parameter);
				if (customAttribute != null)
				{
					Attribute[] array3 = array;
					int num = count;
					count = num + 1;
					array3[num] = customAttribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(OptionalAttribute))
			{
				Attribute customAttribute = OptionalAttribute.GetCustomAttribute(parameter);
				if (customAttribute != null)
				{
					Attribute[] array4 = array;
					int num = count;
					count = num + 1;
					array4[num] = customAttribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(MarshalAsAttribute))
			{
				Attribute customAttribute = MarshalAsAttribute.GetCustomAttribute(parameter);
				if (customAttribute != null)
				{
					Attribute[] array5 = array;
					int num = count;
					count = num + 1;
					array5[num] = customAttribute;
				}
			}
			return array;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x000FC2C4 File Offset: 0x000FA4C4
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(InAttribute)) && InAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(OutAttribute)) && OutAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(OptionalAttribute)) && OptionalAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(parameter)));
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x000FC3AC File Offset: 0x000FA5AC
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(assembly.ManifestModule.ModuleHandle.GetRuntimeModule(), RuntimeAssembly.GetToken(assembly.GetNativeHandle()), true, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000FC4A4 File Offset: 0x000FA6A4
		[SecurityCritical]
		internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
		{
			int num;
			return PseudoCustomAttribute.GetCustomAttributes(assembly, caType, true, out num).Length != 0;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000FC4BF File Offset: 0x000FA6BF
		internal static Attribute[] GetCustomAttributes(RuntimeModule module, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000FC4C5 File Offset: 0x000FA6C5
		internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000FC4C8 File Offset: 0x000FA6C8
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)
			{
				return null;
			}
			Attribute[] array = new Attribute[PseudoCustomAttribute.s_pcasCount];
			if (flag || caType == (RuntimeType)typeof(MarshalAsAttribute))
			{
				Attribute customAttribute = MarshalAsAttribute.GetCustomAttribute(field);
				if (customAttribute != null)
				{
					Attribute[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = customAttribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(FieldOffsetAttribute))
			{
				Attribute customAttribute = FieldOffsetAttribute.GetCustomAttribute(field);
				if (customAttribute != null)
				{
					Attribute[] array3 = array;
					int num = count;
					count = num + 1;
					array3[num] = customAttribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(NonSerializedAttribute))
			{
				Attribute customAttribute = NonSerializedAttribute.GetCustomAttribute(field);
				if (customAttribute != null)
				{
					Attribute[] array4 = array;
					int num = count;
					count = num + 1;
					array4[num] = customAttribute;
				}
			}
			return array;
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x000FC5C0 File Offset: 0x000FA7C0
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(field)) || ((flag || caType == (RuntimeType)typeof(FieldOffsetAttribute)) && FieldOffsetAttribute.IsDefined(field)) || ((flag || caType == (RuntimeType)typeof(NonSerializedAttribute)) && NonSerializedAttribute.IsDefined(field)));
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x000FC684 File Offset: 0x000FA884
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(ctor.Module.ModuleHandle.GetRuntimeModule(), ctor.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000FC778 File Offset: 0x000FA978
		[SecurityCritical]
		internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(ctor, caType, true, out num).Length != 0);
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x000FC7E8 File Offset: 0x000FA9E8
		internal static Attribute[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000FC7EE File Offset: 0x000FA9EE
		internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000FC7F1 File Offset: 0x000FA9F1
		internal static Attribute[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000FC7F7 File Offset: 0x000FA9F7
		internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x04001BD6 RID: 7126
		private static Dictionary<RuntimeType, RuntimeType> s_pca;

		// Token: 0x04001BD7 RID: 7127
		private static int s_pcasCount;
	}
}
