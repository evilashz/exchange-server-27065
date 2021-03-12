using System;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000007 RID: 7
	public static class Constants
	{
		// Token: 0x0400000F RID: 15
		public const int CosmosRequestIDPadding = 1000000;

		// Token: 0x04000010 RID: 16
		public const string DateReplacement = "{DATE}";

		// Token: 0x04000011 RID: 17
		public const string HourReplacement = "{HOUR}";

		// Token: 0x04000012 RID: 18
		public const string SequenceReplacement = "{SN}";

		// Token: 0x04000013 RID: 19
		public const string RegionReplacement = "{REGION}";

		// Token: 0x04000014 RID: 20
		public const string BatchIDReplacement = "{BATCHID}";

		// Token: 0x04000015 RID: 21
		public const string OnDemandRequestIDReplacement = "{REQUEST_ID}";

		// Token: 0x04000016 RID: 22
		public const string ExchangeVCReplacement = "{EXO_VC}";

		// Token: 0x04000017 RID: 23
		public const string SharePointVCReplacement = "{SPO_VC}";

		// Token: 0x04000018 RID: 24
		public const string WorkDirctoryReplacement = "{WORK_DIR}";

		// Token: 0x04000019 RID: 25
		public const string DataMiningCosmosDll = "Microsoft.Datacenter.Datamining.Cosmos.dll";

		// Token: 0x0400001A RID: 26
		public const string EOPDataMiningDll = "Microsoft.Forefront.Reporting.Cosmos.dll";

		// Token: 0x0400001B RID: 27
		public const string ScopeLibDll = "Relevance.ScopeLib.dll";

		// Token: 0x0400001C RID: 28
		public const string EmptyStream = "Empty.log";

		// Token: 0x0400001D RID: 29
		public const string EmptyStream1 = "Empty1.log";

		// Token: 0x0400001E RID: 30
		public const bool CosmosCompression = true;

		// Token: 0x0400001F RID: 31
		public const char ParameterDelimiter = '\t';

		// Token: 0x04000020 RID: 32
		public const string StatusDelimiter = "__";

		// Token: 0x04000021 RID: 33
		public const char MsgStatusRecipientDelimiter = ';';

		// Token: 0x04000022 RID: 34
		public const char QueryDefinitionFieldSeperator = ',';

		// Token: 0x04000023 RID: 35
		public const string MsgStatusValueDemilter = "##";

		// Token: 0x04000024 RID: 36
		public static readonly Guid TenantIDForSystemCount = new Guid("ae3384e1-e9f9-4479-845b-a4c3ff006a66");
	}
}
