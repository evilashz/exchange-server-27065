using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000639 RID: 1593
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		// Token: 0x06004D56 RID: 19798 RVA: 0x00116DB4 File Offset: 0x00114FB4
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x00116DD0 File Offset: 0x00114FD0
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00116DF5 File Offset: 0x00114FF5
		public TypeInfo CreateTypeInfo()
		{
			return this.m_typeBuilder.CreateTypeInfo();
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x00116E02 File Offset: 0x00115002
		public Type CreateType()
		{
			return this.m_typeBuilder.CreateType();
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x00116E0F File Offset: 0x0011500F
		public TypeToken TypeToken
		{
			get
			{
				return this.m_typeBuilder.TypeToken;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004D5B RID: 19803 RVA: 0x00116E1C File Offset: 0x0011501C
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this.m_underlyingField;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x00116E24 File Offset: 0x00115024
		public override string Name
		{
			get
			{
				return this.m_typeBuilder.Name;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004D5D RID: 19805 RVA: 0x00116E31 File Offset: 0x00115031
		public override Guid GUID
		{
			get
			{
				return this.m_typeBuilder.GUID;
			}
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x00116E40 File Offset: 0x00115040
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004D5F RID: 19807 RVA: 0x00116E65 File Offset: 0x00115065
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x00116E72 File Offset: 0x00115072
		public override Assembly Assembly
		{
			get
			{
				return this.m_typeBuilder.Assembly;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004D61 RID: 19809 RVA: 0x00116E7F File Offset: 0x0011507F
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.m_typeBuilder.TypeHandle;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x00116E8C File Offset: 0x0011508C
		public override string FullName
		{
			get
			{
				return this.m_typeBuilder.FullName;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x00116E99 File Offset: 0x00115099
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.m_typeBuilder.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x00116EA6 File Offset: 0x001150A6
		public override string Namespace
		{
			get
			{
				return this.m_typeBuilder.Namespace;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x00116EB3 File Offset: 0x001150B3
		public override Type BaseType
		{
			get
			{
				return this.m_typeBuilder.BaseType;
			}
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x00116EC0 File Offset: 0x001150C0
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x00116ED4 File Offset: 0x001150D4
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetConstructors(bindingAttr);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00116EE2 File Offset: 0x001150E2
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.m_typeBuilder.GetMethod(name, bindingAttr);
			}
			return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x00116F0A File Offset: 0x0011510A
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMethods(bindingAttr);
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x00116F18 File Offset: 0x00115118
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetField(name, bindingAttr);
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x00116F27 File Offset: 0x00115127
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetFields(bindingAttr);
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x00116F35 File Offset: 0x00115135
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.m_typeBuilder.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00116F44 File Offset: 0x00115144
		public override Type[] GetInterfaces()
		{
			return this.m_typeBuilder.GetInterfaces();
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x00116F51 File Offset: 0x00115151
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x00116F60 File Offset: 0x00115160
		public override EventInfo[] GetEvents()
		{
			return this.m_typeBuilder.GetEvents();
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x00116F6D File Offset: 0x0011516D
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x00116F7E File Offset: 0x0011517E
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetProperties(bindingAttr);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x00116F8C File Offset: 0x0011518C
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00116F9A File Offset: 0x0011519A
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00116FA9 File Offset: 0x001151A9
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x00116FB9 File Offset: 0x001151B9
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMembers(bindingAttr);
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00116FC7 File Offset: 0x001151C7
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.m_typeBuilder.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x00116FD5 File Offset: 0x001151D5
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvents(bindingAttr);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00116FE3 File Offset: 0x001151E3
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_typeBuilder.Attributes;
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x00116FF0 File Offset: 0x001151F0
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00116FF3 File Offset: 0x001151F3
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x00116FF6 File Offset: 0x001151F6
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00116FF9 File Offset: 0x001151F9
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00116FFC File Offset: 0x001151FC
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x00116FFF File Offset: 0x001151FF
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004D7F RID: 19839 RVA: 0x00117002 File Offset: 0x00115202
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x00117005 File Offset: 0x00115205
		public override Type GetElementType()
		{
			return this.m_typeBuilder.GetElementType();
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x00117012 File Offset: 0x00115212
		protected override bool HasElementTypeImpl()
		{
			return this.m_typeBuilder.HasElementType;
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x0011701F File Offset: 0x0011521F
		public override Type GetEnumUnderlyingType()
		{
			return this.m_underlyingField.FieldType;
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x0011702C File Offset: 0x0011522C
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.GetEnumUnderlyingType();
			}
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x00117034 File Offset: 0x00115234
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x00117042 File Offset: 0x00115242
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x00117051 File Offset: 0x00115251
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x00117060 File Offset: 0x00115260
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_typeBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004D88 RID: 19848 RVA: 0x0011706E File Offset: 0x0011526E
		public override Type DeclaringType
		{
			get
			{
				return this.m_typeBuilder.DeclaringType;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x0011707B File Offset: 0x0011527B
		public override Type ReflectedType
		{
			get
			{
				return this.m_typeBuilder.ReflectedType;
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x00117088 File Offset: 0x00115288
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00117097 File Offset: 0x00115297
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_typeBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x001170A4 File Offset: 0x001152A4
		private EnumBuilder()
		{
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x001170AC File Offset: 0x001152AC
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x001170BF File Offset: 0x001152BF
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x001170D2 File Offset: 0x001152D2
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x001170E8 File Offset: 0x001152E8
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
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x00117148 File Offset: 0x00115348
		[SecurityCritical]
		internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, ModuleBuilder module)
		{
			if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ShouldOnlySetVisibilityFlags"), "name");
			}
			this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof(Enum), null, module, PackingSize.Unspecified, 0, null);
			this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x001171B5 File Offset: 0x001153B5
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x001171BC File Offset: 0x001153BC
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x001171C3 File Offset: 0x001153C3
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x001171CA File Offset: 0x001153CA
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002116 RID: 8470
		internal TypeBuilder m_typeBuilder;

		// Token: 0x04002117 RID: 8471
		private FieldBuilder m_underlyingField;
	}
}
