using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D5 RID: 2261
	[Guid("BCA8B44D-AAD6-3A86-8AB7-03349F4F2DA2")]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(Type))]
	[ComVisible(true)]
	public interface _Type
	{
		// Token: 0x06005D46 RID: 23878
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005D47 RID: 23879
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005D48 RID: 23880
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005D49 RID: 23881
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005D4A RID: 23882
		string ToString();

		// Token: 0x06005D4B RID: 23883
		bool Equals(object other);

		// Token: 0x06005D4C RID: 23884
		int GetHashCode();

		// Token: 0x06005D4D RID: 23885
		Type GetType();

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06005D4E RID: 23886
		MemberTypes MemberType { get; }

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06005D4F RID: 23887
		string Name { get; }

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06005D50 RID: 23888
		Type DeclaringType { get; }

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06005D51 RID: 23889
		Type ReflectedType { get; }

		// Token: 0x06005D52 RID: 23890
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005D53 RID: 23891
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005D54 RID: 23892
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06005D55 RID: 23893
		Guid GUID { get; }

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06005D56 RID: 23894
		Module Module { get; }

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06005D57 RID: 23895
		Assembly Assembly { get; }

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06005D58 RID: 23896
		RuntimeTypeHandle TypeHandle { get; }

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06005D59 RID: 23897
		string FullName { get; }

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06005D5A RID: 23898
		string Namespace { get; }

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06005D5B RID: 23899
		string AssemblyQualifiedName { get; }

		// Token: 0x06005D5C RID: 23900
		int GetArrayRank();

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06005D5D RID: 23901
		Type BaseType { get; }

		// Token: 0x06005D5E RID: 23902
		ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		// Token: 0x06005D5F RID: 23903
		Type GetInterface(string name, bool ignoreCase);

		// Token: 0x06005D60 RID: 23904
		Type[] GetInterfaces();

		// Token: 0x06005D61 RID: 23905
		Type[] FindInterfaces(TypeFilter filter, object filterCriteria);

		// Token: 0x06005D62 RID: 23906
		EventInfo GetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x06005D63 RID: 23907
		EventInfo[] GetEvents();

		// Token: 0x06005D64 RID: 23908
		EventInfo[] GetEvents(BindingFlags bindingAttr);

		// Token: 0x06005D65 RID: 23909
		Type[] GetNestedTypes(BindingFlags bindingAttr);

		// Token: 0x06005D66 RID: 23910
		Type GetNestedType(string name, BindingFlags bindingAttr);

		// Token: 0x06005D67 RID: 23911
		MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr);

		// Token: 0x06005D68 RID: 23912
		MemberInfo[] GetDefaultMembers();

		// Token: 0x06005D69 RID: 23913
		MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria);

		// Token: 0x06005D6A RID: 23914
		Type GetElementType();

		// Token: 0x06005D6B RID: 23915
		bool IsSubclassOf(Type c);

		// Token: 0x06005D6C RID: 23916
		bool IsInstanceOfType(object o);

		// Token: 0x06005D6D RID: 23917
		bool IsAssignableFrom(Type c);

		// Token: 0x06005D6E RID: 23918
		InterfaceMapping GetInterfaceMap(Type interfaceType);

		// Token: 0x06005D6F RID: 23919
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D70 RID: 23920
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06005D71 RID: 23921
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06005D72 RID: 23922
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06005D73 RID: 23923
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06005D74 RID: 23924
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06005D75 RID: 23925
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D76 RID: 23926
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06005D77 RID: 23927
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06005D78 RID: 23928
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06005D79 RID: 23929
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06005D7A RID: 23930
		Type UnderlyingSystemType { get; }

		// Token: 0x06005D7B RID: 23931
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture);

		// Token: 0x06005D7C RID: 23932
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args);

		// Token: 0x06005D7D RID: 23933
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D7E RID: 23934
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D7F RID: 23935
		ConstructorInfo GetConstructor(Type[] types);

		// Token: 0x06005D80 RID: 23936
		ConstructorInfo[] GetConstructors();

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06005D81 RID: 23937
		ConstructorInfo TypeInitializer { get; }

		// Token: 0x06005D82 RID: 23938
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D83 RID: 23939
		MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D84 RID: 23940
		MethodInfo GetMethod(string name, Type[] types);

		// Token: 0x06005D85 RID: 23941
		MethodInfo GetMethod(string name);

		// Token: 0x06005D86 RID: 23942
		MethodInfo[] GetMethods();

		// Token: 0x06005D87 RID: 23943
		FieldInfo GetField(string name);

		// Token: 0x06005D88 RID: 23944
		FieldInfo[] GetFields();

		// Token: 0x06005D89 RID: 23945
		Type GetInterface(string name);

		// Token: 0x06005D8A RID: 23946
		EventInfo GetEvent(string name);

		// Token: 0x06005D8B RID: 23947
		PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06005D8C RID: 23948
		PropertyInfo GetProperty(string name, Type returnType, Type[] types);

		// Token: 0x06005D8D RID: 23949
		PropertyInfo GetProperty(string name, Type[] types);

		// Token: 0x06005D8E RID: 23950
		PropertyInfo GetProperty(string name, Type returnType);

		// Token: 0x06005D8F RID: 23951
		PropertyInfo GetProperty(string name);

		// Token: 0x06005D90 RID: 23952
		PropertyInfo[] GetProperties();

		// Token: 0x06005D91 RID: 23953
		Type[] GetNestedTypes();

		// Token: 0x06005D92 RID: 23954
		Type GetNestedType(string name);

		// Token: 0x06005D93 RID: 23955
		MemberInfo[] GetMember(string name);

		// Token: 0x06005D94 RID: 23956
		MemberInfo[] GetMembers();

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06005D95 RID: 23957
		TypeAttributes Attributes { get; }

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06005D96 RID: 23958
		bool IsNotPublic { get; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06005D97 RID: 23959
		bool IsPublic { get; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06005D98 RID: 23960
		bool IsNestedPublic { get; }

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06005D99 RID: 23961
		bool IsNestedPrivate { get; }

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06005D9A RID: 23962
		bool IsNestedFamily { get; }

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06005D9B RID: 23963
		bool IsNestedAssembly { get; }

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06005D9C RID: 23964
		bool IsNestedFamANDAssem { get; }

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06005D9D RID: 23965
		bool IsNestedFamORAssem { get; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06005D9E RID: 23966
		bool IsAutoLayout { get; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06005D9F RID: 23967
		bool IsLayoutSequential { get; }

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06005DA0 RID: 23968
		bool IsExplicitLayout { get; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06005DA1 RID: 23969
		bool IsClass { get; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06005DA2 RID: 23970
		bool IsInterface { get; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06005DA3 RID: 23971
		bool IsValueType { get; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06005DA4 RID: 23972
		bool IsAbstract { get; }

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06005DA5 RID: 23973
		bool IsSealed { get; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06005DA6 RID: 23974
		bool IsEnum { get; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06005DA7 RID: 23975
		bool IsSpecialName { get; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06005DA8 RID: 23976
		bool IsImport { get; }

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06005DA9 RID: 23977
		bool IsSerializable { get; }

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06005DAA RID: 23978
		bool IsAnsiClass { get; }

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06005DAB RID: 23979
		bool IsUnicodeClass { get; }

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06005DAC RID: 23980
		bool IsAutoClass { get; }

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06005DAD RID: 23981
		bool IsArray { get; }

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06005DAE RID: 23982
		bool IsByRef { get; }

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06005DAF RID: 23983
		bool IsPointer { get; }

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06005DB0 RID: 23984
		bool IsPrimitive { get; }

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06005DB1 RID: 23985
		bool IsCOMObject { get; }

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06005DB2 RID: 23986
		bool HasElementType { get; }

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06005DB3 RID: 23987
		bool IsContextful { get; }

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06005DB4 RID: 23988
		bool IsMarshalByRef { get; }

		// Token: 0x06005DB5 RID: 23989
		bool Equals(Type o);
	}
}
