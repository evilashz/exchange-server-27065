using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A02 RID: 2562
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x06006517 RID: 25879
		FieldInfo AddField(string name);

		// Token: 0x06006518 RID: 25880
		PropertyInfo AddProperty(string name);

		// Token: 0x06006519 RID: 25881
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x0600651A RID: 25882
		void RemoveMember(MemberInfo m);
	}
}
