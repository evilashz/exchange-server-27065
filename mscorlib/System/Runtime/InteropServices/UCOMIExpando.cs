using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000959 RID: 2393
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IExpando instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIExpando : UCOMIReflect
	{
		// Token: 0x06006183 RID: 24963
		FieldInfo AddField(string name);

		// Token: 0x06006184 RID: 24964
		PropertyInfo AddProperty(string name);

		// Token: 0x06006185 RID: 24965
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x06006186 RID: 24966
		void RemoveMember(MemberInfo m);
	}
}
