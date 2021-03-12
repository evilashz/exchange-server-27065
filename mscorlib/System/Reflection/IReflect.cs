using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C3 RID: 1475
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	[ComVisible(true)]
	public interface IReflect
	{
		// Token: 0x06004545 RID: 17733
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004546 RID: 17734
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06004547 RID: 17735
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06004548 RID: 17736
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06004549 RID: 17737
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x0600454A RID: 17738
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x0600454B RID: 17739
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600454C RID: 17740
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x0600454D RID: 17741
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x0600454E RID: 17742
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x0600454F RID: 17743
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004550 RID: 17744
		Type UnderlyingSystemType { get; }
	}
}
