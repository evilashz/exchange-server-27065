using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000637 RID: 1591
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x06004CCF RID: 19663 RVA: 0x001166EF File Offset: 0x001148EF
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x00116708 File Offset: 0x00114908
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			if (!type.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException();
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			foreach (Type left in typeArguments)
			{
				if (left == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00116760 File Offset: 0x00114960
		private TypeBuilderInstantiation(Type type, Type[] inst)
		{
			this.m_type = type;
			this.m_inst = inst;
			this.m_hashtable = new Hashtable();
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0011678C File Offset: 0x0011498C
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x00116795 File Offset: 0x00114995
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x001167A2 File Offset: 0x001149A2
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x001167AF File Offset: 0x001149AF
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x001167BC File Offset: 0x001149BC
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x001167C9 File Offset: 0x001149C9
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x001167DC File Offset: 0x001149DC
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x001167EF File Offset: 0x001149EF
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x00116804 File Offset: 0x00114A04
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			for (int i = 1; i < rank; i++)
			{
				text += ",";
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x00116857 File Offset: 0x00114A57
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0011685E File Offset: 0x00114A5E
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x00116865 File Offset: 0x00114A65
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x00116872 File Offset: 0x00114A72
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x00116879 File Offset: 0x00114A79
		public override string FullName
		{
			get
			{
				if (this.m_strFullQualName == null)
				{
					this.m_strFullQualName = TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
				}
				return this.m_strFullQualName;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x00116896 File Offset: 0x00114A96
		public override string Namespace
		{
			get
			{
				return this.m_type.Namespace;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x001168A3 File Offset: 0x00114AA3
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x001168AC File Offset: 0x00114AAC
		private Type Substitute(Type[] substitutes)
		{
			Type[] genericArguments = this.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Type type = genericArguments[i];
				if (type is TypeBuilderInstantiation)
				{
					array[i] = (type as TypeBuilderInstantiation).Substitute(substitutes);
				}
				else if (type is GenericTypeParameterBuilder)
				{
					array[i] = substitutes[type.GenericParameterPosition];
				}
				else
				{
					array[i] = type;
				}
			}
			return this.GetGenericTypeDefinition().MakeGenericType(array);
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x0011691C File Offset: 0x00114B1C
		public override Type BaseType
		{
			get
			{
				Type baseType = this.m_type.BaseType;
				if (baseType == null)
				{
					return null;
				}
				TypeBuilderInstantiation typeBuilderInstantiation = baseType as TypeBuilderInstantiation;
				if (typeBuilderInstantiation == null)
				{
					return baseType;
				}
				return typeBuilderInstantiation.Substitute(this.GetGenericArguments());
			}
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0011695E File Offset: 0x00114B5E
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00116965 File Offset: 0x00114B65
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0011696C File Offset: 0x00114B6C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x00116973 File Offset: 0x00114B73
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0011697A File Offset: 0x00114B7A
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x00116981 File Offset: 0x00114B81
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x00116988 File Offset: 0x00114B88
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0011698F File Offset: 0x00114B8F
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x00116996 File Offset: 0x00114B96
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0011699D File Offset: 0x00114B9D
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x001169A4 File Offset: 0x00114BA4
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x001169AB File Offset: 0x00114BAB
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x001169B2 File Offset: 0x00114BB2
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x001169B9 File Offset: 0x00114BB9
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x001169C0 File Offset: 0x00114BC0
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x001169C7 File Offset: 0x00114BC7
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x001169CE File Offset: 0x00114BCE
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x001169D5 File Offset: 0x00114BD5
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x001169DC File Offset: 0x00114BDC
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_type.Attributes;
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x001169E9 File Offset: 0x00114BE9
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x001169EC File Offset: 0x00114BEC
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x001169EF File Offset: 0x00114BEF
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x001169F2 File Offset: 0x00114BF2
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x001169F5 File Offset: 0x00114BF5
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x001169F8 File Offset: 0x00114BF8
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x001169FF File Offset: 0x00114BFF
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x00116A02 File Offset: 0x00114C02
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x00116A05 File Offset: 0x00114C05
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004D00 RID: 19712 RVA: 0x00116A0D File Offset: 0x00114C0D
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x00116A10 File Offset: 0x00114C10
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x00116A13 File Offset: 0x00114C13
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x00116A16 File Offset: 0x00114C16
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004D04 RID: 19716 RVA: 0x00116A19 File Offset: 0x00114C19
		public override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00116A20 File Offset: 0x00114C20
		protected override bool IsValueTypeImpl()
		{
			return this.m_type.IsValueType;
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004D06 RID: 19718 RVA: 0x00116A30 File Offset: 0x00114C30
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x00116A62 File Offset: 0x00114C62
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x00116A65 File Offset: 0x00114C65
		public override Type GetGenericTypeDefinition()
		{
			return this.m_type;
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x00116A6D File Offset: 0x00114C6D
		public override Type MakeGenericType(params Type[] inst)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x00116A7E File Offset: 0x00114C7E
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x00116A85 File Offset: 0x00114C85
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00116A8C File Offset: 0x00114C8C
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x00116A93 File Offset: 0x00114C93
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x00116A9A File Offset: 0x00114C9A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002111 RID: 8465
		private Type m_type;

		// Token: 0x04002112 RID: 8466
		private Type[] m_inst;

		// Token: 0x04002113 RID: 8467
		private string m_strFullQualName;

		// Token: 0x04002114 RID: 8468
		internal Hashtable m_hashtable = new Hashtable();
	}
}
