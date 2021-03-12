using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D7 RID: 2263
	[Guid("f7102fa9-cabb-3a74-a6da-b4567ef1b079")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(MemberInfo))]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _MemberInfo
	{
		// Token: 0x06005DE2 RID: 24034
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005DE3 RID: 24035
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005DE4 RID: 24036
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005DE5 RID: 24037
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005DE6 RID: 24038
		string ToString();

		// Token: 0x06005DE7 RID: 24039
		bool Equals(object other);

		// Token: 0x06005DE8 RID: 24040
		int GetHashCode();

		// Token: 0x06005DE9 RID: 24041
		Type GetType();

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06005DEA RID: 24042
		MemberTypes MemberType { get; }

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06005DEB RID: 24043
		string Name { get; }

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06005DEC RID: 24044
		Type DeclaringType { get; }

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06005DED RID: 24045
		Type ReflectedType { get; }

		// Token: 0x06005DEE RID: 24046
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005DEF RID: 24047
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005DF0 RID: 24048
		bool IsDefined(Type attributeType, bool inherit);
	}
}
