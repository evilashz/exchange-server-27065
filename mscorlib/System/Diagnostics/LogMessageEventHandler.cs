using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020003C5 RID: 965
	// (Invoke) Token: 0x06003211 RID: 12817
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	internal delegate void LogMessageEventHandler(LoggingLevels level, LogSwitch category, string message, StackTrace location);
}
