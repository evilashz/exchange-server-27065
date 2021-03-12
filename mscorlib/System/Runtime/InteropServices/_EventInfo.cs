using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008DD RID: 2269
	[Guid("9DE59C64-D889-35A1-B897-587D74469E5B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(EventInfo))]
	[ComVisible(true)]
	public interface _EventInfo
	{
		// Token: 0x06005EA1 RID: 24225
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005EA2 RID: 24226
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005EA3 RID: 24227
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005EA4 RID: 24228
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06005EA5 RID: 24229
		string ToString();

		// Token: 0x06005EA6 RID: 24230
		bool Equals(object other);

		// Token: 0x06005EA7 RID: 24231
		int GetHashCode();

		// Token: 0x06005EA8 RID: 24232
		Type GetType();

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06005EA9 RID: 24233
		MemberTypes MemberType { get; }

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06005EAA RID: 24234
		string Name { get; }

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06005EAB RID: 24235
		Type DeclaringType { get; }

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06005EAC RID: 24236
		Type ReflectedType { get; }

		// Token: 0x06005EAD RID: 24237
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06005EAE RID: 24238
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06005EAF RID: 24239
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06005EB0 RID: 24240
		MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x06005EB1 RID: 24241
		MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x06005EB2 RID: 24242
		MethodInfo GetRaiseMethod(bool nonPublic);

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06005EB3 RID: 24243
		EventAttributes Attributes { get; }

		// Token: 0x06005EB4 RID: 24244
		MethodInfo GetAddMethod();

		// Token: 0x06005EB5 RID: 24245
		MethodInfo GetRemoveMethod();

		// Token: 0x06005EB6 RID: 24246
		MethodInfo GetRaiseMethod();

		// Token: 0x06005EB7 RID: 24247
		void AddEventHandler(object target, Delegate handler);

		// Token: 0x06005EB8 RID: 24248
		void RemoveEventHandler(object target, Delegate handler);

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06005EB9 RID: 24249
		Type EventHandlerType { get; }

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06005EBA RID: 24250
		bool IsSpecialName { get; }

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06005EBB RID: 24251
		bool IsMulticast { get; }
	}
}
