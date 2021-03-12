using System;
using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
	// Token: 0x020009F5 RID: 2549
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	[ComVisible(true)]
	public interface IExpando : IReflect
	{
		// Token: 0x060064EA RID: 25834
		FieldInfo AddField(string name);

		// Token: 0x060064EB RID: 25835
		PropertyInfo AddProperty(string name);

		// Token: 0x060064EC RID: 25836
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x060064ED RID: 25837
		void RemoveMember(MemberInfo m);
	}
}
