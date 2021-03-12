using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000913 RID: 2323
	[Obsolete("This attribute has been deprecated.  Application Domains no longer respect Activation Context boundaries in IDispatch calls.", false)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class SetWin32ContextInIDispatchAttribute : Attribute
	{
	}
}
