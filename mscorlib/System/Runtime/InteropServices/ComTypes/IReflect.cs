using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A06 RID: 2566
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x06006535 RID: 25909
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06006536 RID: 25910
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06006537 RID: 25911
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06006538 RID: 25912
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06006539 RID: 25913
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x0600653A RID: 25914
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x0600653B RID: 25915
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600653C RID: 25916
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x0600653D RID: 25917
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x0600653E RID: 25918
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x0600653F RID: 25919
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06006540 RID: 25920
		Type UnderlyingSystemType { get; }
	}
}
