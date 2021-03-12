using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000075 RID: 117
	internal sealed class PoisonMailboxControl : PoisonControl
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00011207 File Offset: 0x0000F407
		public PoisonMailboxControl(PoisonControlMaster master, DatabaseInfo databaseInfo) : base(master, databaseInfo, "Mailbox")
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011221 File Offset: 0x0000F421
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Poison mailbox control for " + base.DatabaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001124C File Offset: 0x0000F44C
		public bool IsPoisonMailbox(Guid mailboxGuid)
		{
			return this.GetCrashCount(mailboxGuid) >= base.Master.PoisonCrashCount && base.Master.Enabled;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00011270 File Offset: 0x0000F470
		public int GetCrashCount(Guid mailboxGuid)
		{
			int result;
			if (!this.crashCounts.TryGetValue(mailboxGuid, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011290 File Offset: 0x0000F490
		public void Clear()
		{
			base.RemoveDatabaseKey();
			this.crashCounts = new Dictionary<Guid, int>();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000112A4 File Offset: 0x0000F4A4
		protected override void LoadCrashData(string subKeyName, int crashCount)
		{
			Exception ex = null;
			try
			{
				Guid key = new Guid(subKeyName);
				this.crashCounts.Add(key, crashCount);
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			catch (OverflowException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.PoisonControlTracer.TraceError<PoisonMailboxControl, string, Exception>((long)this.GetHashCode(), "{0}: Unable to load crash data from {1}. Exception: {2}", this, subKeyName, ex);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001130C File Offset: 0x0000F50C
		protected override void HandleUnhandledException(object exception, EmergencyKit kit)
		{
			int num = this.GetCrashCount(kit.MailboxGuid) + 1;
			base.SaveCrashData(kit.MailboxGuid.ToString(), num);
			ExTraceGlobals.PoisonControlTracer.TraceError((long)this.GetHashCode(), "{0}: Unhandled exception while processing mailbox: {1}, crashCount: {2}, exception: {3}", new object[]
			{
				this,
				kit.MailboxDisplayName,
				num,
				exception
			});
			if (num < base.Master.PoisonCrashCount || !base.Master.Enabled)
			{
				base.LogEvent(AssistantsEventLogConstants.Tuple_CrashMailbox, null, new object[]
				{
					kit.AssistantName,
					num,
					kit.MailboxDisplayName,
					base.DatabaseInfo.DisplayName,
					exception
				});
				return;
			}
			base.LogEvent(AssistantsEventLogConstants.Tuple_PoisonMailbox, null, new object[]
			{
				kit.AssistantName,
				num,
				kit.MailboxDisplayName,
				base.DatabaseInfo.DisplayName,
				exception
			});
		}

		// Token: 0x040001F7 RID: 503
		private Dictionary<Guid, int> crashCounts = new Dictionary<Guid, int>();

		// Token: 0x040001F8 RID: 504
		private string toString;
	}
}
