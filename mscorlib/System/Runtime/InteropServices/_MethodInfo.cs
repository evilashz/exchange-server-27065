using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D9 RID: 2265
	[Guid("FFCC1B5D-ECB8-38DD-9B01-3DC8ABC2AA5F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(MethodInfo))]
	[ComVisible(true)]
	public interface _MethodInfo
	{
		// Token: 0x06005E14 RID: 24084
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E15 RID: 24085
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E16 RID: 24086
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E17 RID: 24087
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005E18 RID: 24088
		string ToString();

		// Token: 0x06005E19 RID: 24089
		bool Equals(object other);

		// Token: 0x06005E1A RID: 24090
		int GetHashCode();

		// Token: 0x06005E1B RID: 24091
		Type GetType();

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06005E1C RID: 24092
		MemberTypes MemberType { get; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06005E1D RID: 24093
		string Name { get; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06005E1E RID: 24094
		Type DeclaringType { get; }

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06005E1F RID: 24095
		Type ReflectedType { get; }

		// Token: 0x06005E20 RID: 24096
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005E21 RID: 24097
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005E22 RID: 24098
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06005E23 RID: 24099
		ParameterInfo[] GetParameters();

		// Token: 0x06005E24 RID: 24100
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06005E25 RID: 24101
		RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06005E26 RID: 24102
		MethodAttributes Attributes { get; }

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06005E27 RID: 24103
		CallingConventions CallingConvention { get; }

		// Token: 0x06005E28 RID: 24104
		object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06005E29 RID: 24105
		bool IsPublic { get; }

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06005E2A RID: 24106
		bool IsPrivate { get; }

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06005E2B RID: 24107
		bool IsFamily { get; }

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06005E2C RID: 24108
		bool IsAssembly { get; }

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06005E2D RID: 24109
		bool IsFamilyAndAssembly { get; }

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06005E2E RID: 24110
		bool IsFamilyOrAssembly { get; }

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06005E2F RID: 24111
		bool IsStatic { get; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06005E30 RID: 24112
		bool IsFinal { get; }

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06005E31 RID: 24113
		bool IsVirtual { get; }

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06005E32 RID: 24114
		bool IsHideBySig { get; }

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06005E33 RID: 24115
		bool IsAbstract { get; }

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06005E34 RID: 24116
		bool IsSpecialName { get; }

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06005E35 RID: 24117
		bool IsConstructor { get; }

		// Token: 0x06005E36 RID: 24118
		object Invoke(object obj, object[] parameters);

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005E37 RID: 24119
		Type ReturnType { get; }

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06005E38 RID: 24120
		ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		// Token: 0x06005E39 RID: 24121
		MethodInfo GetBaseDefinition();
	}
}
