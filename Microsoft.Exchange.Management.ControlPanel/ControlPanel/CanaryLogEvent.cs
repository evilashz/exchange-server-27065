using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001AD RID: 429
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CanaryLogEvent : ILogEvent
	{
		// Token: 0x0600239F RID: 9119 RVA: 0x0006D1B4 File Offset: 0x0006B3B4
		public CanaryLogEvent(string version, string activityContextId, string canaryName, string canaryPath, string logonUniqueKey, CanaryStatus canaryStatus, DateTime creationTime, string logData)
		{
			if (string.IsNullOrEmpty(canaryName))
			{
				throw new ArgumentNullException("canaryName");
			}
			if (string.IsNullOrEmpty(canaryPath))
			{
				throw new ArgumentNullException("canaryPath");
			}
			this.version = this.TranslateStringValueToLog(version);
			this.activityContextId = activityContextId;
			this.canaryName = canaryName;
			this.canaryPath = canaryPath;
			this.logonUniqueKey = this.TranslateStringValueToLog(logonUniqueKey);
			this.canaryStatus = canaryStatus;
			this.creationTime = creationTime;
			this.logData = this.TranslateStringValueToLog(logData);
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x0006D23D File Offset: 0x0006B43D
		public string EventId
		{
			get
			{
				return ActivityContextLoggerId.Canary.ToString();
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0006D24C File Offset: 0x0006B44C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("ACTID", this.activityContextId),
				new KeyValuePair<string, object>("V", this.version),
				new KeyValuePair<string, object>("LUK", ExtensibleLogger.FormatPIIValue(this.logonUniqueKey)),
				new KeyValuePair<string, object>("CN.N", this.canaryName),
				new KeyValuePair<string, object>("CN.P", this.canaryPath),
				new KeyValuePair<string, object>("CN.S", string.Format("0x{0:X}", (int)this.canaryStatus)),
				new KeyValuePair<string, object>("CN.T", this.creationTime),
				new KeyValuePair<string, object>("CN.L", this.logData)
			};
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0006D35A File Offset: 0x0006B55A
		private string TranslateStringValueToLog(string value)
		{
			if (value == null)
			{
				return "<null>";
			}
			if (value == string.Empty)
			{
				return "<empty>";
			}
			return value;
		}

		// Token: 0x04001E11 RID: 7697
		private readonly string activityContextId;

		// Token: 0x04001E12 RID: 7698
		private readonly string canaryName;

		// Token: 0x04001E13 RID: 7699
		private readonly string canaryPath;

		// Token: 0x04001E14 RID: 7700
		private readonly string logonUniqueKey;

		// Token: 0x04001E15 RID: 7701
		private readonly string version;

		// Token: 0x04001E16 RID: 7702
		private readonly CanaryStatus canaryStatus;

		// Token: 0x04001E17 RID: 7703
		private readonly DateTime creationTime;

		// Token: 0x04001E18 RID: 7704
		private readonly string logData;
	}
}
