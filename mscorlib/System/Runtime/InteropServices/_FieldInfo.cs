using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008DB RID: 2267
	[Guid("8A7C1442-A9FB-366B-80D8-4939FFA6DBE0")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(FieldInfo))]
	[ComVisible(true)]
	public interface _FieldInfo
	{
		// Token: 0x06005E5F RID: 24159
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E60 RID: 24160
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E61 RID: 24161
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E62 RID: 24162
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005E63 RID: 24163
		string ToString();

		// Token: 0x06005E64 RID: 24164
		bool Equals(object other);

		// Token: 0x06005E65 RID: 24165
		int GetHashCode();

		// Token: 0x06005E66 RID: 24166
		Type GetType();

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06005E67 RID: 24167
		MemberTypes MemberType { get; }

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06005E68 RID: 24168
		string Name { get; }

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06005E69 RID: 24169
		Type DeclaringType { get; }

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06005E6A RID: 24170
		Type ReflectedType { get; }

		// Token: 0x06005E6B RID: 24171
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005E6C RID: 24172
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005E6D RID: 24173
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06005E6E RID: 24174
		Type FieldType { get; }

		// Token: 0x06005E6F RID: 24175
		object GetValue(object obj);

		// Token: 0x06005E70 RID: 24176
		object GetValueDirect(TypedReference obj);

		// Token: 0x06005E71 RID: 24177
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x06005E72 RID: 24178
		void SetValueDirect(TypedReference obj, object value);

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06005E73 RID: 24179
		RuntimeFieldHandle FieldHandle { get; }

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06005E74 RID: 24180
		FieldAttributes Attributes { get; }

		// Token: 0x06005E75 RID: 24181
		void SetValue(object obj, object value);

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06005E76 RID: 24182
		bool IsPublic { get; }

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06005E77 RID: 24183
		bool IsPrivate { get; }

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06005E78 RID: 24184
		bool IsFamily { get; }

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06005E79 RID: 24185
		bool IsAssembly { get; }

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06005E7A RID: 24186
		bool IsFamilyAndAssembly { get; }

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06005E7B RID: 24187
		bool IsFamilyOrAssembly { get; }

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06005E7C RID: 24188
		bool IsStatic { get; }

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06005E7D RID: 24189
		bool IsInitOnly { get; }

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06005E7E RID: 24190
		bool IsLiteral { get; }

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06005E7F RID: 24191
		bool IsNotSerialized { get; }

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06005E80 RID: 24192
		bool IsSpecialName { get; }

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06005E81 RID: 24193
		bool IsPinvokeImpl { get; }
	}
}
