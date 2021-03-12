using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000159 RID: 345
	internal class StatefulEventLog
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x00029864 File Offset: 0x00027A64
		private StatefulEventLog()
		{
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00029877 File Offset: 0x00027A77
		internal static StatefulEventLog Instance
		{
			get
			{
				return StatefulEventLog.instance;
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002987E File Offset: 0x00027A7E
		public void LogGreenEvent(string key, ExEventLog.EventTuple tuple, string periodicKey, bool forceLog, params object[] args)
		{
			this.LogEvent(key, HealthState.Green, tuple, periodicKey, forceLog, args);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002988E File Offset: 0x00027A8E
		public void LogYellowEvent(string key, ExEventLog.EventTuple tuple, string periodicKey, bool forceLog, params object[] args)
		{
			this.LogEvent(key, HealthState.Red, tuple, periodicKey, forceLog, args);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002989E File Offset: 0x00027A9E
		public void LogRedEvent(string key, ExEventLog.EventTuple tuple, string periodicKey, bool forceLog, params object[] args)
		{
			this.LogEvent(key, HealthState.Red, tuple, periodicKey, forceLog, args);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000298B0 File Offset: 0x00027AB0
		private void LogEvent(string key, HealthState newState, ExEventLog.EventTuple tuple, string periodicKey, bool forceLog, params object[] args)
		{
			bool flag = false;
			lock (StatefulEventLog.lockObj)
			{
				HealthState healthState;
				if (!this.healthstate.TryGetValue(key, out healthState))
				{
					this.healthstate.Add(key, newState);
					flag = true;
				}
				else
				{
					this.healthstate[key] = newState;
					flag = (healthState != newState);
				}
			}
			if (flag || forceLog)
			{
				UmGlobals.ExEvent.LogEvent(tuple, periodicKey, args);
			}
		}

		// Token: 0x040005E4 RID: 1508
		private static object lockObj = new object();

		// Token: 0x040005E5 RID: 1509
		private static StatefulEventLog instance = new StatefulEventLog();

		// Token: 0x040005E6 RID: 1510
		private Dictionary<string, HealthState> healthstate = new Dictionary<string, HealthState>();
	}
}
