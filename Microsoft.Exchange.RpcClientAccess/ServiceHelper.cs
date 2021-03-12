using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ServiceHelper
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00005634 File Offset: 0x00003834
		public static string FormatWin32ErrorString(int win32error)
		{
			Win32Exception ex = new Win32Exception(win32error);
			string format = (win32error >= -32768 && win32error <= 32767) ? "{0} ({1:d})" : "{0} (0x{1:X8})";
			return string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				ex.Message,
				win32error
			});
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000568C File Offset: 0x0000388C
		public static void RegisterSPN(string className, ExEventLog eventLog, ExEventLog.EventTuple tupleError)
		{
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				if (current.User.IsWellKnown(WellKnownSidType.NetworkServiceSid) || current.User.IsWellKnown(WellKnownSidType.LocalSystemSid))
				{
					int num = ServicePrincipalName.RegisterServiceClass(className);
					if (num != 0)
					{
						eventLog.LogEvent(tupleError, string.Empty, new object[]
						{
							className,
							ServiceHelper.FormatWin32ErrorString(num)
						});
					}
				}
			}
		}
	}
}
