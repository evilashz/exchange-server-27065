using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000074 RID: 116
	internal sealed class PoisonEventControl : PoisonControl
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00010F80 File Offset: 0x0000F180
		public PoisonEventControl(PoisonControlMaster master, DatabaseInfo databaseInfo) : base(master, databaseInfo, "Event")
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010F9A File Offset: 0x0000F19A
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Poison event control control for " + base.DatabaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010FC5 File Offset: 0x0000F1C5
		public bool IsPoisonEvent(MapiEvent mapiEvent)
		{
			return this.GetCrashCount(mapiEvent) >= base.Master.PoisonCrashCount && base.Master.Enabled;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		public bool IsToxicEvent(MapiEvent mapiEvent)
		{
			return this.GetCrashCount(mapiEvent) >= base.Master.ToxicCrashCount && base.Master.Enabled;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001100C File Offset: 0x0000F20C
		public int GetCrashCount(MapiEvent mapiEvent)
		{
			int result;
			if (this.crashCounts.TryGetValue(mapiEvent.EventCounter, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011034 File Offset: 0x0000F234
		protected override void LoadCrashData(string subKeyName, int crashCount)
		{
			Exception ex = null;
			try
			{
				long key = long.Parse(subKeyName, NumberStyles.HexNumber);
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
			catch (ArgumentOutOfRangeException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.PoisonControlTracer.TraceError<PoisonEventControl, string, Exception>((long)this.GetHashCode(), "{0}: Unable to load crash data from {1}. Exception: {2}", this, subKeyName, ex);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000110B4 File Offset: 0x0000F2B4
		protected override void HandleUnhandledException(object exception, EmergencyKit kit)
		{
			int num = this.GetCrashCount(kit.MapiEvent) + 1;
			base.SaveCrashData(kit.MapiEvent.EventCounter.ToString("x16"), num);
			ExTraceGlobals.PoisonControlTracer.TraceError((long)this.GetHashCode(), "{0}: Unhandled Exception while processing eventCounter: {1}, crashCount: {2}, exception: {3}", new object[]
			{
				this,
				kit.MapiEvent.EventCounter,
				num,
				exception
			});
			if (num < base.Master.PoisonCrashCount || !base.Master.Enabled)
			{
				base.LogEvent(AssistantsEventLogConstants.Tuple_CrashEvent, null, new object[]
				{
					kit.AssistantName,
					num,
					kit.MapiEvent.EventCounter,
					base.DatabaseInfo.DisplayName,
					kit.MailboxDisplayName,
					exception
				});
				return;
			}
			base.LogEvent(AssistantsEventLogConstants.Tuple_PoisonEvent, null, new object[]
			{
				kit.MapiEvent.EventCounter,
				base.DatabaseInfo.DisplayName,
				kit.MailboxDisplayName,
				kit.AssistantName,
				num,
				exception
			});
		}

		// Token: 0x040001F5 RID: 501
		private Dictionary<long, int> crashCounts = new Dictionary<long, int>();

		// Token: 0x040001F6 RID: 502
		private string toString;
	}
}
