using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D8 RID: 2264
	[Guid("6240837A-707F-3181-8E98-A36AE086766B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(MethodBase))]
	[ComVisible(true)]
	public interface _MethodBase
	{
		// Token: 0x06005DF1 RID: 24049
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005DF2 RID: 24050
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005DF3 RID: 24051
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005DF4 RID: 24052
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005DF5 RID: 24053
		string ToString();

		// Token: 0x06005DF6 RID: 24054
		bool Equals(object other);

		// Token: 0x06005DF7 RID: 24055
		int GetHashCode();

		// Token: 0x06005DF8 RID: 24056
		Type GetType();

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06005DF9 RID: 24057
		MemberTypes MemberType { get; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06005DFA RID: 24058
		string Name { get; }

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06005DFB RID: 24059
		Type DeclaringType { get; }

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06005DFC RID: 24060
		Type ReflectedType { get; }

		// Token: 0x06005DFD RID: 24061
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005DFE RID: 24062
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005DFF RID: 24063
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06005E00 RID: 24064
		ParameterInfo[] GetParameters();

		// Token: 0x06005E01 RID: 24065
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06005E02 RID: 24066
		RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06005E03 RID: 24067
		MethodAttributes Attributes { get; }

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06005E04 RID: 24068
		CallingConventions CallingConvention { get; }

		// Token: 0x06005E05 RID: 24069
		object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06005E06 RID: 24070
		bool IsPublic { get; }

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06005E07 RID: 24071
		bool IsPrivate { get; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06005E08 RID: 24072
		bool IsFamily { get; }

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06005E09 RID: 24073
		bool IsAssembly { get; }

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06005E0A RID: 24074
		bool IsFamilyAndAssembly { get; }

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06005E0B RID: 24075
		bool IsFamilyOrAssembly { get; }

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005E0C RID: 24076
		bool IsStatic { get; }

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005E0D RID: 24077
		bool IsFinal { get; }

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06005E0E RID: 24078
		bool IsVirtual { get; }

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06005E0F RID: 24079
		bool IsHideBySig { get; }

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06005E10 RID: 24080
		bool IsAbstract { get; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06005E11 RID: 24081
		bool IsSpecialName { get; }

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06005E12 RID: 24082
		bool IsConstructor { get; }

		// Token: 0x06005E13 RID: 24083
		object Invoke(object obj, object[] parameters);
	}
}
