using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008DC RID: 2268
	[Guid("F59ED4E4-E68F-3218-BD77-061AA82824BF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(PropertyInfo))]
	[ComVisible(true)]
	public interface _PropertyInfo
	{
		// Token: 0x06005E82 RID: 24194
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E83 RID: 24195
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E84 RID: 24196
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E85 RID: 24197
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005E86 RID: 24198
		string ToString();

		// Token: 0x06005E87 RID: 24199
		bool Equals(object other);

		// Token: 0x06005E88 RID: 24200
		int GetHashCode();

		// Token: 0x06005E89 RID: 24201
		Type GetType();

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06005E8A RID: 24202
		MemberTypes MemberType { get; }

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06005E8B RID: 24203
		string Name { get; }

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06005E8C RID: 24204
		Type DeclaringType { get; }

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005E8D RID: 24205
		Type ReflectedType { get; }

		// Token: 0x06005E8E RID: 24206
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005E8F RID: 24207
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005E90 RID: 24208
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005E91 RID: 24209
		Type PropertyType { get; }

		// Token: 0x06005E92 RID: 24210
		object GetValue(object obj, object[] index);

		// Token: 0x06005E93 RID: 24211
		object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06005E94 RID: 24212
		void SetValue(object obj, object value, object[] index);

		// Token: 0x06005E95 RID: 24213
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06005E96 RID: 24214
		MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x06005E97 RID: 24215
		MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x06005E98 RID: 24216
		MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06005E99 RID: 24217
		ParameterInfo[] GetIndexParameters();

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005E9A RID: 24218
		PropertyAttributes Attributes { get; }

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06005E9B RID: 24219
		bool CanRead { get; }

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06005E9C RID: 24220
		bool CanWrite { get; }

		// Token: 0x06005E9D RID: 24221
		MethodInfo[] GetAccessors();

		// Token: 0x06005E9E RID: 24222
		MethodInfo GetGetMethod();

		// Token: 0x06005E9F RID: 24223
		MethodInfo GetSetMethod();

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06005EA0 RID: 24224
		bool IsSpecialName { get; }
	}
}
