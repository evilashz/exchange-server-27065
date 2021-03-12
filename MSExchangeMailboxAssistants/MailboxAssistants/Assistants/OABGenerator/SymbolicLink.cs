using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001F7 RID: 503
	internal static class SymbolicLink
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x00071B54 File Offset: 0x0006FD54
		public static bool CreateDirectorySymbolicLink(string linkName, string targetDirectoryName)
		{
			int num = SymbolicLink.CreateSymbolicLink(linkName, targetDirectoryName, 1);
			if (num != 0)
			{
				return true;
			}
			uint lastError = SymbolicLink.GetLastError();
			if (lastError == 183U)
			{
				return true;
			}
			SymbolicLink.Tracer.TraceError<string, string, uint>(0L, "CreateSymbolicLink({0},{1} failed due error {2}", linkName, targetDirectoryName, lastError);
			return false;
		}

		// Token: 0x0600137B RID: 4987
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateSymbolicLinkW")]
		private static extern int CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

		// Token: 0x0600137C RID: 4988
		[DllImport("kernel32.dll")]
		private static extern uint GetLastError();

		// Token: 0x04000BCE RID: 3022
		private const int SYMBOLIC_LINK_FLAG_DIRECTORY = 1;

		// Token: 0x04000BCF RID: 3023
		private const uint ERROR_ALREADY_EXISTS = 183U;

		// Token: 0x04000BD0 RID: 3024
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;
	}
}
