using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005FA RID: 1530
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x060047D2 RID: 18386 RVA: 0x001032C8 File Offset: 0x001014C8
		[FriendAccessAllowed]
		internal TypeInfo()
		{
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x001032D0 File Offset: 0x001014D0
		[__DynamicallyInvokable]
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x001032D3 File Offset: 0x001014D3
		[__DynamicallyInvokable]
		public virtual Type AsType()
		{
			return this;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x001032D6 File Offset: 0x001014D6
		[__DynamicallyInvokable]
		public virtual Type[] GenericTypeParameters
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.IsGenericTypeDefinition)
				{
					return this.GetGenericArguments();
				}
				return Type.EmptyTypes;
			}
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x001032EC File Offset: 0x001014EC
		[__DynamicallyInvokable]
		public virtual bool IsAssignableFrom(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x00103357 File Offset: 0x00101557
		[__DynamicallyInvokable]
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x00103362 File Offset: 0x00101562
		[__DynamicallyInvokable]
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0010336D File Offset: 0x0010156D
		[__DynamicallyInvokable]
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x00103378 File Offset: 0x00101578
		[__DynamicallyInvokable]
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x00103390 File Offset: 0x00101590
		[__DynamicallyInvokable]
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x001033B8 File Offset: 0x001015B8
		[__DynamicallyInvokable]
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x001033C3 File Offset: 0x001015C3
		[__DynamicallyInvokable]
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x001033CD File Offset: 0x001015CD
		[__DynamicallyInvokable]
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x001033D7 File Offset: 0x001015D7
		[__DynamicallyInvokable]
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x001033E1 File Offset: 0x001015E1
		[__DynamicallyInvokable]
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x001033EB File Offset: 0x001015EB
		[__DynamicallyInvokable]
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x001033F8 File Offset: 0x001015F8
		[__DynamicallyInvokable]
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				foreach (Type type in this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x00103415 File Offset: 0x00101615
		[__DynamicallyInvokable]
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x0010341F File Offset: 0x0010161F
		[__DynamicallyInvokable]
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetInterfaces();
			}
		}
	}
}
