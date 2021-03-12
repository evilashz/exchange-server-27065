using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200003A RID: 58
	internal class BackOffValue : IComparable
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00015664 File Offset: 0x00013864
		static BackOffValue()
		{
			BackOffValue.backOffTypeMapping.Add(BackOffType.Low, "L");
			BackOffValue.backOffTypeMapping.Add(BackOffType.Medium, "M");
			BackOffValue.backOffTypeMapping.Add(BackOffType.High, "H");
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000156F7 File Offset: 0x000138F7
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000156FF File Offset: 0x000138FF
		public double BackOffDuration { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00015708 File Offset: 0x00013908
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00015710 File Offset: 0x00013910
		public BackOffType BackOffType { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00015719 File Offset: 0x00013919
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00015721 File Offset: 0x00013921
		public string BackOffReason { get; set; }

		// Token: 0x060003A3 RID: 931 RVA: 0x0001572C File Offset: 0x0001392C
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			BackOffValue backOffValue = obj as BackOffValue;
			if (backOffValue != null)
			{
				return this.BackOffDuration.CompareTo(backOffValue.BackOffDuration);
			}
			throw new ArgumentException("Object is not a valid BackOffValue");
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015767 File Offset: 0x00013967
		public override string ToString()
		{
			return string.Format("{0}/{1}", BackOffValue.backOffTypeMapping[this.BackOffType], this.BackOffDuration);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00015790 File Offset: 0x00013990
		public static BackOffValue GetEffectiveBackOffValue(BackOffValue budgetBackOff, BackOffValue abBackOff)
		{
			BackOffValue backOffValue = new BackOffValue();
			backOffValue.BackOffDuration = Math.Ceiling(Math.Max(budgetBackOff.BackOffDuration, abBackOff.BackOffDuration));
			backOffValue.BackOffType = ((budgetBackOff.BackOffType >= abBackOff.BackOffType) ? budgetBackOff.BackOffType : abBackOff.BackOffType);
			backOffValue.BackOffReason = ((budgetBackOff.BackOffType >= abBackOff.BackOffType) ? budgetBackOff.BackOffReason : abBackOff.BackOffReason);
			if (backOffValue.BackOffDuration > GlobalSettings.MaxBackOffDuration.TotalSeconds)
			{
				AirSyncDiagnostics.TraceDebug<double, TimeSpan>(ExTraceGlobals.RequestsTracer, null, "Calculated backoff time exceed max allowed backoff time, using predefined MaxBackOffDuration. CalculatedbackOff:{0} sec, MaxValue as per Settings:{1}", backOffValue.BackOffDuration, GlobalSettings.MaxBackOffDuration);
				backOffValue.BackOffDuration = GlobalSettings.MaxBackOffDuration.TotalSeconds;
			}
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.SuggestedBackOffValue, string.Format("BBkOff:{0}, ABBkOff:{1}, EffBkOff:{2}", budgetBackOff.ToString(), abBackOff.ToString(), backOffValue.ToString()));
			}
			return backOffValue;
		}

		// Token: 0x040002B2 RID: 690
		public static BackOffValue NoBackOffValue = new BackOffValue
		{
			BackOffDuration = Math.Ceiling(-1.0 * ThrottlingPolicyDefaults.EasMaxBurst.Value / 1000.0),
			BackOffType = BackOffType.Low,
			BackOffReason = string.Empty
		};

		// Token: 0x040002B3 RID: 691
		public static Dictionary<BackOffType, string> backOffTypeMapping = new Dictionary<BackOffType, string>();
	}
}
