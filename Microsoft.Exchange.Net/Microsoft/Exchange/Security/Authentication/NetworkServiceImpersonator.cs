using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000C5E RID: 3166
	internal static class NetworkServiceImpersonator
	{
		// Token: 0x06004631 RID: 17969 RVA: 0x000BB5F4 File Offset: 0x000B97F4
		public static void Initialize()
		{
			if (!NetworkServiceImpersonator.initialized)
			{
				lock (NetworkServiceImpersonator.locker)
				{
					if (!NetworkServiceImpersonator.initialized)
					{
						ExWatson.SendReportOnUnhandledException(new ExWatson.MethodDelegate(NetworkServiceImpersonator.InitializeInternal));
					}
				}
			}
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x000BB64C File Offset: 0x000B984C
		private static void InitializeInternal()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			if (current.User.IsWellKnown(WellKnownSidType.NetworkServiceSid))
			{
				ExTraceGlobals.AuthenticationTracer.Information(0L, "Already running as NT AUTHORITY\\NetworkService");
				NetworkServiceImpersonator.windowsIdentity = current;
				NetworkServiceImpersonator.initialized = true;
				return;
			}
			current.Dispose();
			try
			{
				NetworkServiceImpersonator.windowsIdentity = NetworkServiceImpersonator.LogonAsNetworkService();
			}
			catch (LocalizedException ex)
			{
				NetworkServiceImpersonator.exception = ex;
			}
			finally
			{
				if (NetworkServiceImpersonator.windowsIdentity != null)
				{
					NetworkServiceImpersonator.initialized = true;
				}
				else
				{
					if (NetworkServiceImpersonator.exception == null)
					{
						NetworkServiceImpersonator.exception = new LogonAsNetworkServiceException("Unknown exception");
					}
					NetworkServiceImpersonator.failedInitializationAttempts++;
					if (NetworkServiceImpersonator.failedInitializationAttempts >= 20)
					{
						NetworkServiceImpersonator.initialized = true;
					}
				}
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06004633 RID: 17971 RVA: 0x000BB708 File Offset: 0x000B9908
		public static LocalizedException Exception
		{
			get
			{
				return NetworkServiceImpersonator.exception;
			}
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000BB70F File Offset: 0x000B990F
		public static WindowsImpersonationContext Impersonate()
		{
			if (NetworkServiceImpersonator.exception != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<LocalizedException>(0L, "Impersonation failed because of exception: {0}", NetworkServiceImpersonator.exception);
				throw NetworkServiceImpersonator.exception;
			}
			ExTraceGlobals.AuthenticationTracer.Information(0L, "Impersonating network service");
			return NetworkServiceImpersonator.windowsIdentity.Impersonate();
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000BB750 File Offset: 0x000B9950
		private static WindowsIdentity LogonAsNetworkService()
		{
			SafeTokenHandle safeTokenHandle = new SafeTokenHandle();
			try
			{
				ExTraceGlobals.AuthenticationTracer.Information(0L, "Calling LogonUser for NT AUTHORITY\\NetworkService");
				if (!SspiNativeMethods.LogonUser("NetworkService", "NT AUTHORITY", IntPtr.Zero, LogonType.Service, LogonProvider.Default, ref safeTokenHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					ExTraceGlobals.AuthenticationTracer.TraceError<int>(0L, "LogonUser failed: {0}", lastWin32Error);
					throw new LogonAsNetworkServiceException(lastWin32Error.ToString());
				}
				ExTraceGlobals.AuthenticationTracer.Information(0L, "LogonUser succeeded");
				Exception ex2;
				try
				{
					return new WindowsIdentity(safeTokenHandle.DangerousGetHandle(), "Logon", WindowsAccountType.Normal, true);
				}
				catch (ArgumentException ex)
				{
					ex2 = ex;
				}
				catch (SecurityException ex3)
				{
					ex2 = ex3;
				}
				ExTraceGlobals.AuthenticationTracer.TraceError<Exception>(0L, "WindowsIdentity failed: {0}", ex2);
				throw new LogonAsNetworkServiceException(ex2.Message, ex2);
			}
			finally
			{
				safeTokenHandle.Close();
			}
			WindowsIdentity result;
			return result;
		}

		// Token: 0x04003A98 RID: 15000
		private const int maximumFailedInitializationAttempts = 20;

		// Token: 0x04003A99 RID: 15001
		private static bool initialized;

		// Token: 0x04003A9A RID: 15002
		private static WindowsIdentity windowsIdentity;

		// Token: 0x04003A9B RID: 15003
		private static object locker = new object();

		// Token: 0x04003A9C RID: 15004
		private static LocalizedException exception;

		// Token: 0x04003A9D RID: 15005
		private static int failedInitializationAttempts;
	}
}
