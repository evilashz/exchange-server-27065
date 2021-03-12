using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000638 RID: 1592
	[ComVisible(true)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		// Token: 0x06004D0F RID: 19727 RVA: 0x00116AA1 File Offset: 0x00114CA1
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x00116ABA File Offset: 0x00114CBA
		internal GenericTypeParameterBuilder(TypeBuilder type)
		{
			this.m_type = type;
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x00116AC9 File Offset: 0x00114CC9
		public override string ToString()
		{
			return this.m_type.Name;
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x00116AD8 File Offset: 0x00114CD8
		public override bool Equals(object o)
		{
			GenericTypeParameterBuilder genericTypeParameterBuilder = o as GenericTypeParameterBuilder;
			return !(genericTypeParameterBuilder == null) && genericTypeParameterBuilder.m_type == this.m_type;
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x00116B05 File Offset: 0x00114D05
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004D14 RID: 19732 RVA: 0x00116B12 File Offset: 0x00114D12
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x00116B1F File Offset: 0x00114D1F
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004D16 RID: 19734 RVA: 0x00116B2C File Offset: 0x00114D2C
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x00116B39 File Offset: 0x00114D39
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x00116B46 File Offset: 0x00114D46
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_type.MetadataTokenInternal;
			}
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x00116B53 File Offset: 0x00114D53
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x00116B66 File Offset: 0x00114D66
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x00116B79 File Offset: 0x00114D79
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x00116B8C File Offset: 0x00114D8C
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0) as SymbolType;
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x00116BF2 File Offset: 0x00114DF2
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00116BF9 File Offset: 0x00114DF9
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x00116C00 File Offset: 0x00114E00
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004D20 RID: 19744 RVA: 0x00116C0D File Offset: 0x00114E0D
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x00116C14 File Offset: 0x00114E14
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x00116C17 File Offset: 0x00114E17
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x00116C1A File Offset: 0x00114E1A
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x00116C1D File Offset: 0x00114E1D
		public override Type BaseType
		{
			get
			{
				return this.m_type.BaseType;
			}
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x00116C2A File Offset: 0x00114E2A
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x00116C31 File Offset: 0x00114E31
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x00116C38 File Offset: 0x00114E38
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x00116C3F File Offset: 0x00114E3F
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x00116C46 File Offset: 0x00114E46
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x00116C4D File Offset: 0x00114E4D
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00116C54 File Offset: 0x00114E54
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x00116C5B File Offset: 0x00114E5B
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x00116C62 File Offset: 0x00114E62
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x00116C69 File Offset: 0x00114E69
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x00116C70 File Offset: 0x00114E70
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x00116C77 File Offset: 0x00114E77
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00116C7E File Offset: 0x00114E7E
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x00116C85 File Offset: 0x00114E85
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x00116C8C File Offset: 0x00114E8C
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00116C93 File Offset: 0x00114E93
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x00116C9A File Offset: 0x00114E9A
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x00116CA1 File Offset: 0x00114EA1
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00116CA8 File Offset: 0x00114EA8
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x00116CAB File Offset: 0x00114EAB
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x00116CAE File Offset: 0x00114EAE
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x00116CB1 File Offset: 0x00114EB1
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00116CB4 File Offset: 0x00114EB4
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x00116CB7 File Offset: 0x00114EB7
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x00116CBA File Offset: 0x00114EBA
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x00116CC1 File Offset: 0x00114EC1
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x00116CC4 File Offset: 0x00114EC4
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00116CC7 File Offset: 0x00114EC7
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x00116CCE File Offset: 0x00114ECE
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x00116CD1 File Offset: 0x00114ED1
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x00116CD4 File Offset: 0x00114ED4
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x00116CD7 File Offset: 0x00114ED7
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x00116CDA File Offset: 0x00114EDA
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_type.GenericParameterPosition;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004D46 RID: 19782 RVA: 0x00116CE7 File Offset: 0x00114EE7
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_type.ContainsGenericParameters;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x00116CF4 File Offset: 0x00114EF4
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return this.m_type.GenericParameterAttributes;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004D48 RID: 19784 RVA: 0x00116D01 File Offset: 0x00114F01
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.m_type.DeclaringMethod;
			}
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x00116D0E File Offset: 0x00114F0E
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x00116D15 File Offset: 0x00114F15
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x00116D26 File Offset: 0x00114F26
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x00116D29 File Offset: 0x00114F29
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00116D30 File Offset: 0x00114F30
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00116D37 File Offset: 0x00114F37
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x00116D3E File Offset: 0x00114F3E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x00116D45 File Offset: 0x00114F45
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00116D4C File Offset: 0x00114F4C
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_type.SetGenParamCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x00116D5B File Offset: 0x00114F5B
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_type.SetGenParamCustomAttribute(customBuilder);
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x00116D69 File Offset: 0x00114F69
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.m_type.CheckContext(new Type[]
			{
				baseTypeConstraint
			});
			this.m_type.SetParent(baseTypeConstraint);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00116D8C File Offset: 0x00114F8C
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.m_type.CheckContext(interfaceConstraints);
			this.m_type.SetInterfaces(interfaceConstraints);
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x00116DA6 File Offset: 0x00114FA6
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_type.SetGenParamAttributes(genericParameterAttributes);
		}

		// Token: 0x04002115 RID: 8469
		internal TypeBuilder m_type;
	}
}
