using System;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x0200078C RID: 1932
	[Serializable]
	internal class DynamicTypeInfo : TypeInfo
	{
		// Token: 0x0600547A RID: 21626 RVA: 0x0012B54F File Offset: 0x0012974F
		[SecurityCritical]
		internal DynamicTypeInfo(RuntimeType typeOfObj) : base(typeOfObj)
		{
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0012B558 File Offset: 0x00129758
		[SecurityCritical]
		public override bool CanCastTo(Type castType, object o)
		{
			return ((MarshalByRefObject)o).IsInstanceOfType(castType);
		}
	}
}
