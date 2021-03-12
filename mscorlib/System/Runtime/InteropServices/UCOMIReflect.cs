using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095D RID: 2397
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IReflect instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIReflect
	{
		// Token: 0x060061A1 RID: 24993
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060061A2 RID: 24994
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x060061A3 RID: 24995
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060061A4 RID: 24996
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060061A5 RID: 24997
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060061A6 RID: 24998
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x060061A7 RID: 24999
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060061A8 RID: 25000
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x060061A9 RID: 25001
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x060061AA RID: 25002
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060061AB RID: 25003
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060061AC RID: 25004
		Type UnderlyingSystemType { get; }
	}
}
