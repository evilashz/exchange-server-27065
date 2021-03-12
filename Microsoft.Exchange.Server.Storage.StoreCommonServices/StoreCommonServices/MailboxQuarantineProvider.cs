using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000158 RID: 344
	public class MailboxQuarantineProvider : IMailboxQuarantineProvider
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x00043171 File Offset: 0x00041371
		private MailboxQuarantineProvider()
		{
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00043179 File Offset: 0x00041379
		public static IMailboxQuarantineProvider Instance
		{
			get
			{
				return MailboxQuarantineProvider.hookableInstance.Value;
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00043185 File Offset: 0x00041385
		internal static IDisposable SetTestHook(IMailboxQuarantineProvider testHook)
		{
			return MailboxQuarantineProvider.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00043194 File Offset: 0x00041394
		public bool IsMigrationAccessAllowed(Guid databaseGuid, Guid mailboxGuid)
		{
			string subkeyName = string.Format("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}\\QuarantinedMailboxes\\{2}", Environment.MachineName, databaseGuid, mailboxGuid);
			bool value = RegistryReader.Instance.GetValue<bool>(Registry.LocalMachine, subkeyName, "AllowMigrationOfQuarantinedMailbox", false);
			if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxQuarantineTracer.TraceDebug<Guid, Guid, bool>(0L, "IsMigrationAccessAllowed check (database={0}, mailbox={1}, result={2}", databaseGuid, mailboxGuid, value);
			}
			return value;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000431F8 File Offset: 0x000413F8
		public void PrequarantineMailbox(Guid databaseGuid, Guid mailboxGuid, string reason)
		{
			try
			{
				string subkeyName = string.Format("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}\\QuarantinedMailboxes\\{2}", Environment.MachineName, databaseGuid, mailboxGuid);
				RegistryWriter.Instance.CreateSubKey(Registry.LocalMachine, subkeyName);
				int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, subkeyName, "CrashCount", 0);
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "CrashCount", value + 1, RegistryValueKind.DWord);
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "LastCrashTime", DateTime.UtcNow.ToFileTime(), RegistryValueKind.QWord);
				if (string.IsNullOrEmpty(reason))
				{
					RegistryWriter.Instance.DeleteValue(Registry.LocalMachine, subkeyName, "MailboxQuarantineDescription");
				}
				else
				{
					RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "MailboxQuarantineDescription", PrequarantinedMailbox.TruncateQuarantineReason(reason), RegistryValueKind.String);
				}
				if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Pre-quarantining mailbox \"" + mailboxGuid.ToString() + "\"");
				}
			}
			catch (ArgumentException ex)
			{
				if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.MailboxQuarantineTracer.TraceError(0L, "Unexpected error druing pre-quarantine a mailbox " + ex.ToString());
				}
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00043340 File Offset: 0x00041540
		public void UnquarantineMailbox(Guid databaseGuid, Guid mailboxGuid)
		{
			string subkeyName = string.Format("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}\\QuarantinedMailboxes\\{2}", Environment.MachineName, databaseGuid, mailboxGuid);
			try
			{
				RegistryWriter.Instance.DeleteSubKeyTree(Registry.LocalMachine, subkeyName);
				if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Un-quarantining mailbox \"" + mailboxGuid.ToString() + "\"");
				}
			}
			catch (ArgumentException)
			{
				if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Quarantine key already removed");
				}
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000433E4 File Offset: 0x000415E4
		public List<PrequarantinedMailbox> GetPreQuarantinedMailboxes(Guid databaseGuid)
		{
			IRegistryReader instance = RegistryReader.Instance;
			IRegistryWriter instance2 = RegistryWriter.Instance;
			string text = string.Format("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}\\QuarantinedMailboxes", Environment.MachineName, databaseGuid);
			string[] array = null;
			try
			{
				array = instance.GetSubKeyNames(Registry.LocalMachine, text);
			}
			catch (ArgumentException)
			{
				if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Parent key deleted, un-quarantining all mailboxes");
				}
				array = null;
			}
			if (array == null)
			{
				return new List<PrequarantinedMailbox>(0);
			}
			List<PrequarantinedMailbox> list = new List<PrequarantinedMailbox>(array.Length);
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text2 = array2[i];
				Guid guid = Guid.Empty;
				string text3 = Path.Combine(text, text2);
				try
				{
					guid = new Guid(text2);
				}
				catch (FormatException)
				{
					if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.MailboxQuarantineTracer.TraceError(0L, "Unrecognized key \"" + text2 + "\" deleted");
					}
					try
					{
						instance2.DeleteSubKeyTree(Registry.LocalMachine, text3);
					}
					catch (ArgumentException)
					{
						if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Key \"" + text3 + "\" has been deleted");
						}
					}
					goto IL_28D;
				}
				goto IL_104;
				IL_28D:
				i++;
				continue;
				IL_104:
				int num = Convert.ToInt32(MailboxQuarantineProvider.defaultQuarantineDuration.TotalSeconds);
				try
				{
					num = instance.GetValue<int>(Registry.LocalMachine, text3, "MailboxQuarantineDurationInSeconds", num);
				}
				catch (OverflowException)
				{
					num = Convert.ToInt32(MailboxQuarantineProvider.defaultQuarantineDuration.TotalSeconds);
				}
				string text4 = string.Empty;
				text4 = instance.GetValue<string>(Registry.LocalMachine, text3, "MailboxQuarantineDescription", text4);
				text4 = PrequarantinedMailbox.TruncateQuarantineReason(text4);
				int value = instance.GetValue<int>(Registry.LocalMachine, text3, "CrashCount", -1);
				long value2 = instance.GetValue<long>(Registry.LocalMachine, text3, "LastCrashTime", (long)((ulong)-1));
				if (value2 != (long)((ulong)-1) && value != -1)
				{
					try
					{
						DateTime dateTime = DateTime.FromFileTimeUtc(value2);
						list.Add(new PrequarantinedMailbox(guid, value, dateTime, TimeSpan.FromSeconds((double)num), text4));
						if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Mailbox {0} quarantined: crash count={1}, lastCrashtime={2}, duration={3}, reason = {4}", new object[]
							{
								guid,
								value,
								dateTime,
								num,
								text4
							});
						}
					}
					catch (ArgumentOutOfRangeException)
					{
						if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Invalid last crash time, key \"" + text2 + "\" deleted");
						}
						try
						{
							instance2.DeleteSubKeyTree(Registry.LocalMachine, text3);
						}
						catch (ArgumentException)
						{
							if (ExTraceGlobals.MailboxQuarantineTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.MailboxQuarantineTracer.TraceDebug(0L, "Key \"" + text3 + "\" has been deleted");
							}
						}
					}
					goto IL_28D;
				}
				goto IL_28D;
			}
			return list;
		}

		// Token: 0x0400076F RID: 1903
		private const string ValueNameCrashCount = "CrashCount";

		// Token: 0x04000770 RID: 1904
		private const string ValueNameLastCrashTime = "LastCrashTime";

		// Token: 0x04000771 RID: 1905
		private const string ValueNameAllowMigration = "AllowMigrationOfQuarantinedMailbox";

		// Token: 0x04000772 RID: 1906
		private const string ValueQuarantineDuration = "MailboxQuarantineDurationInSeconds";

		// Token: 0x04000773 RID: 1907
		private const string ValueQuarantineReason = "MailboxQuarantineDescription";

		// Token: 0x04000774 RID: 1908
		private const string KeyNameFormatQuarantinedMailboxRoot = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}\\QuarantinedMailboxes";

		// Token: 0x04000775 RID: 1909
		private const string KeyNameFormatQuarantinedMailbox = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}\\QuarantinedMailboxes\\{2}";

		// Token: 0x04000776 RID: 1910
		private static TimeSpan defaultQuarantineDuration = TimeSpan.FromHours(24.0);

		// Token: 0x04000777 RID: 1911
		private static Hookable<IMailboxQuarantineProvider> hookableInstance = Hookable<IMailboxQuarantineProvider>.Create(false, new MailboxQuarantineProvider());
	}
}
