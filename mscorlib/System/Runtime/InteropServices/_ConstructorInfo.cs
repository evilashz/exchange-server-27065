using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008DA RID: 2266
	[Guid("E9A19478-9646-3679-9B10-8411AE1FD57D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ConstructorInfo))]
	[ComVisible(true)]
	public interface _ConstructorInfo
	{
		// Token: 0x06005E3A RID: 24122
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E3B RID: 24123
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E3C RID: 24124
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E3D RID: 24125
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005E3E RID: 24126
		string ToString();

		// Token: 0x06005E3F RID: 24127
		bool Equals(object other);

		// Token: 0x06005E40 RID: 24128
		int GetHashCode();

		// Token: 0x06005E41 RID: 24129
		Type GetType();

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06005E42 RID: 24130
		MemberTypes MemberType { get; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06005E43 RID: 24131
		string Name { get; }

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06005E44 RID: 24132
		Type DeclaringType { get; }

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06005E45 RID: 24133
		Type ReflectedType { get; }

		// Token: 0x06005E46 RID: 24134
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005E47 RID: 24135
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005E48 RID: 24136
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06005E49 RID: 24137
		ParameterInfo[] GetParameters();

		// Token: 0x06005E4A RID: 24138
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06005E4B RID: 24139
		RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06005E4C RID: 24140
		MethodAttributes Attributes { get; }

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06005E4D RID: 24141
		CallingConventions CallingConvention { get; }

		// Token: 0x06005E4E RID: 24142
		object Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06005E4F RID: 24143
		bool IsPublic { get; }

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06005E50 RID: 24144
		bool IsPrivate { get; }

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06005E51 RID: 24145
		bool IsFamily { get; }

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06005E52 RID: 24146
		bool IsAssembly { get; }

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06005E53 RID: 24147
		bool IsFamilyAndAssembly { get; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06005E54 RID: 24148
		bool IsFamilyOrAssembly { get; }

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06005E55 RID: 24149
		bool IsStatic { get; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06005E56 RID: 24150
		bool IsFinal { get; }

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06005E57 RID: 24151
		bool IsVirtual { get; }

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06005E58 RID: 24152
		bool IsHideBySig { get; }

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06005E59 RID: 24153
		bool IsAbstract { get; }

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06005E5A RID: 24154
		bool IsSpecialName { get; }

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06005E5B RID: 24155
		bool IsConstructor { get; }

		// Token: 0x06005E5C RID: 24156
		object Invoke_3(object obj, object[] parameters);

		// Token: 0x06005E5D RID: 24157
		object Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x06005E5E RID: 24158
		object Invoke_5(object[] parameters);
	}
}
