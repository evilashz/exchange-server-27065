using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000AE RID: 174
	internal static class TraceConfigSync
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00012324 File Offset: 0x00010524
		public static void Signal(uint configFileContentHash, string tagIdentifier, InternalBypassTrace tracer)
		{
			try
			{
				string text = TraceConfigSync.CreateConfigLoadSyncEventName(AppDomain.CurrentDomain.FriendlyName, tagIdentifier, configFileContentHash);
				bool flag = false;
				using (EventWaitHandle eventWaitHandle = TraceConfigSync.CreateOrOpenConfigLoadEvent(text, out flag))
				{
					if (!flag)
					{
						eventWaitHandle.Set();
						eventWaitHandle.Dispose();
						tracer.TraceDebug(47663, 0L, "Signalled event {0} to let the test code know that the configuration document has been loaded", new object[]
						{
							text
						});
					}
					else
					{
						tracer.TraceDebug(47663, 0L, "Event {0} didn't exist, and so no synchronization with the test code was performed", new object[]
						{
							text
						});
					}
				}
			}
			catch (WaitHandleCannotBeOpenedException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000123DC File Offset: 0x000105DC
		internal static uint ComputeContentHash(byte[] fileContent)
		{
			uint num = 0U;
			for (int i = 0; i < fileContent.Length; i++)
			{
				num = (num << 5 ^ num >> 27 ^ (uint)fileContent[i]);
			}
			return num;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012408 File Offset: 0x00010608
		internal static string CreateConfigLoadSyncEventName(string appDomainName, string tagIdentifier, uint configContentHash)
		{
			appDomainName = appDomainName.Replace('\\', '@');
			if (appDomainName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
			{
				appDomainName = appDomainName.Substring(0, appDomainName.Length - ".exe".Length);
			}
			if (appDomainName.StartsWith("/LM/W3SVC", StringComparison.OrdinalIgnoreCase))
			{
				int num = appDomainName.LastIndexOf('-');
				if (num != -1)
				{
					appDomainName = appDomainName.Substring(0, num);
				}
			}
			string text = string.Format("{0}_{1}{2}_{3:X8}", new object[]
			{
				"Global\\IFXE",
				appDomainName.ToUpperInvariant(),
				string.IsNullOrEmpty(tagIdentifier) ? string.Empty : ("_" + tagIdentifier.ToUpperInvariant()),
				configContentHash
			});
			if (text.Length <= 118)
			{
				return text;
			}
			return string.Format("BUG_APPDOMAIN_NAME_TOO_BIG_{0}", Guid.NewGuid());
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000124DD File Offset: 0x000106DD
		internal static EventWaitHandle CreateOrOpenConfigLoadEvent(string fullEventName, out bool isNew)
		{
			return new EventWaitHandle(false, EventResetMode.ManualReset, fullEventName, ref isNew, TraceConfigSync.waitHandleSecurity);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000124F0 File Offset: 0x000106F0
		private static EventWaitHandleSecurity CreateDefaultWaitHandleSecurity()
		{
			EventWaitHandleSecurity eventWaitHandleSecurity = new EventWaitHandleSecurity();
			eventWaitHandleSecurity.AddAccessRule(new EventWaitHandleAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), EventWaitHandleRights.FullControl, AccessControlType.Allow));
			return eventWaitHandleSecurity;
		}

		// Token: 0x0400035E RID: 862
		internal const string EventNamePrefix = "Global\\IFXE";

		// Token: 0x0400035F RID: 863
		private static readonly EventWaitHandleSecurity waitHandleSecurity = TraceConfigSync.CreateDefaultWaitHandleSecurity();
	}
}
