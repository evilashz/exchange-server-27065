using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002C3 RID: 707
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeAccessSecurityAttribute : SecurityAttribute
	{
		// Token: 0x0600253C RID: 9532 RVA: 0x00087933 File Offset: 0x00085B33
		protected CodeAccessSecurityAttribute(SecurityAction action) : base(action)
		{
		}
	}
}
