using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000822 RID: 2082
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelSinkWithProperties : BaseChannelObjectWithProperties
	{
	}
}
