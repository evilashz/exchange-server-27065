using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000499 RID: 1177
	public static class TransportGlobalSettingsService
	{
		// Token: 0x06003A76 RID: 14966 RVA: 0x000B1748 File Offset: 0x000AF948
		public static void OnPostGlobalSettings(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			dataRow["MaxRecipientEnvelopeLimit"] = DDIUtil.ConvertUnlimitedToString<int>(dataRow["MaxRecipientEnvelopeLimit"], (int t) => t.ToString());
			dataRow["MaxReceiveSize"] = DDIUtil.ConvertUnlimitedToString<ByteQuantifiedSize>(dataRow["MaxReceiveSize"], (ByteQuantifiedSize s) => s.ToMB(3));
			dataRow["MaxSendSize"] = DDIUtil.ConvertUnlimitedToString<ByteQuantifiedSize>(dataRow["MaxSendSize"], (ByteQuantifiedSize s) => s.ToMB(3));
			if (!DDIHelper.IsEmptyValue(dataRow["SafetyNetHoldTime"]))
			{
				dataRow["SafetyNetHoldTime"] = ((EnhancedTimeSpan)dataRow["SafetyNetHoldTime"]).ToString(TimeUnit.Day, 2);
			}
			if (!DDIHelper.IsEmptyValue(dataRow["MaxDumpsterTime"]))
			{
				dataRow["MaxDumpsterTime"] = ((EnhancedTimeSpan)dataRow["MaxDumpsterTime"]).ToString(TimeUnit.Day, 2);
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000B1884 File Offset: 0x000AFA84
		public static void OnPreGlobalSettings(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["MaxReceiveSize"]))
			{
				dataRow["MaxReceiveSize"] = DDIUtil.ConvertStringToUnlimited((string)dataRow["MaxReceiveSize"], (string t) => t.FromMB());
			}
			if (!DBNull.Value.Equals(dataRow["MaxSendSize"]))
			{
				dataRow["MaxSendSize"] = DDIUtil.ConvertStringToUnlimited((string)dataRow["MaxSendSize"], (string t) => t.FromMB());
			}
			if (!DBNull.Value.Equals(dataRow["SafetyNetHoldTime"]))
			{
				dataRow["SafetyNetHoldTime"] = ((string)dataRow["SafetyNetHoldTime"]).FromTimeSpan(TimeUnit.Day).ToString();
			}
			if (!DBNull.Value.Equals(dataRow["MaxDumpsterTime"]))
			{
				dataRow["MaxDumpsterTime"] = ((string)dataRow["MaxDumpsterTime"]).FromTimeSpan(TimeUnit.Day).ToString();
			}
		}

		// Token: 0x04002716 RID: 10006
		private const string SafetyNetHoldTimeName = "SafetyNetHoldTime";

		// Token: 0x04002717 RID: 10007
		private const string MaxDumpsterTimeName = "MaxDumpsterTime";

		// Token: 0x04002718 RID: 10008
		private const string MaxReceiveSizeName = "MaxReceiveSize";

		// Token: 0x04002719 RID: 10009
		private const string MaxRecipientEnvelopeLimitName = "MaxRecipientEnvelopeLimit";

		// Token: 0x0400271A RID: 10010
		private const string MaxSendSizeName = "MaxSendSize";

		// Token: 0x0400271B RID: 10011
		private const string ExternalPostmasterAddressName = "ExternalPostmasterAddress";
	}
}
