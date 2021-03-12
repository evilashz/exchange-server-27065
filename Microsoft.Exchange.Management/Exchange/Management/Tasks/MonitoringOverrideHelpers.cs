using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020005A6 RID: 1446
	public class MonitoringOverrideHelpers
	{
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000CF9DB File Offset: 0x000CDBDB
		// (set) Token: 0x060032E7 RID: 13031 RVA: 0x000CF9E3 File Offset: 0x000CDBE3
		internal string MonitoringItemIdentity { get; set; }

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000CF9EC File Offset: 0x000CDBEC
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x000CF9F4 File Offset: 0x000CDBF4
		internal string HealthSet { get; set; }

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000CF9FD File Offset: 0x000CDBFD
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x000CFA05 File Offset: 0x000CDC05
		internal string MonitoringItemName { get; set; }

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000CFA0E File Offset: 0x000CDC0E
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x000CFA16 File Offset: 0x000CDC16
		internal string TargetResource { get; set; }

		// Token: 0x060032EE RID: 13038 RVA: 0x000CFA20 File Offset: 0x000CDC20
		internal void ParseAndValidateIdentity(string monitoringItemIdentity, bool global)
		{
			this.MonitoringItemIdentity = monitoringItemIdentity;
			this.HealthSet = null;
			this.MonitoringItemName = null;
			this.TargetResource = null;
			string[] array = MonitoringOverrideHelpers.SplitMonitoringItemIdentity(monitoringItemIdentity, '\\');
			if (array.Length < 2 || (global && array.Length != 2) || array.Length > 3 || string.IsNullOrWhiteSpace(array[0]) || string.IsNullOrWhiteSpace(array[1]))
			{
				throw new InvalidIdentityException(this.MonitoringItemIdentity, global ? "<HealthSet>\\<MonitoringItemName>" : "<HealthSet>\\<MonitoringItemName>\\[TargetResource]");
			}
			this.HealthSet = array[0];
			this.MonitoringItemName = array[1];
			if (array.Length == 3 && !string.IsNullOrWhiteSpace(array[2]))
			{
				this.TargetResource = array[2];
			}
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000CFAC4 File Offset: 0x000CDCC4
		internal static string[] SplitMonitoringItemIdentity(string fullIdentity, char splitCharacter)
		{
			return fullIdentity.Split(new char[]
			{
				splitCharacter
			});
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000CFAE3 File Offset: 0x000CDCE3
		internal static void ValidateApplyVersion(Version version)
		{
			if (version.Major == -1 || version.Minor == -1 || version.Build == -1 || version.Revision == -1)
			{
				throw new InvalidVersionException(version.ToString(), "Major.Minor.Build.Revision");
			}
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000CFB1C File Offset: 0x000CDD1C
		internal static void ValidateOverrideDuration(EnhancedTimeSpan? duration)
		{
			EnhancedTimeSpan zero = EnhancedTimeSpan.Zero;
			EnhancedTimeSpan t = EnhancedTimeSpan.FromDays(365.0);
			if ((duration != null && duration.Value <= zero) || (duration != null && duration.Value > t))
			{
				throw new InvalidDurationException(zero.ToString(), t.ToString());
			}
		}

		// Token: 0x04002381 RID: 9089
		internal const string ParameterServer = "Server";

		// Token: 0x04002382 RID: 9090
		internal const string ParameterIdentity = "Identity";

		// Token: 0x04002383 RID: 9091
		internal const string ParameterItemType = "ItemType";

		// Token: 0x04002384 RID: 9092
		internal const string ParameterPropertyName = "PropertyName";

		// Token: 0x04002385 RID: 9093
		internal const string ParameterPropertyValue = "PropertyValue";

		// Token: 0x04002386 RID: 9094
		internal const string ParameterDuration = "Duration";

		// Token: 0x04002387 RID: 9095
		internal const string ParameterApplyVersion = "ApplyVersion";

		// Token: 0x04002388 RID: 9096
		internal const string PropertyValueString = "PropertyValue";

		// Token: 0x04002389 RID: 9097
		internal const string ExpirationTimeString = "ExpirationTime";

		// Token: 0x0400238A RID: 9098
		internal const string TimeUpdatedString = "TimeUpdated";

		// Token: 0x0400238B RID: 9099
		internal const string CreatedByString = "CreatedBy";

		// Token: 0x0400238C RID: 9100
		internal const string ApplyVersionString = "ApplyVersion";

		// Token: 0x0400238D RID: 9101
		internal const string WatermarkString = "Watermark";

		// Token: 0x0400238E RID: 9102
		internal const char BackslashSeparator = '\\';

		// Token: 0x0400238F RID: 9103
		internal const char TildaSeparator = '~';

		// Token: 0x04002390 RID: 9104
		internal const int MaximumOverrideDuration = 365;
	}
}
