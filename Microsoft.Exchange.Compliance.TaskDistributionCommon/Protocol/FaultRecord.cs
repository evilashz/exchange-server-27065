using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000057 RID: 87
	public class FaultRecord
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000BC38 File Offset: 0x00009E38
		static FaultRecord()
		{
			ComplianceSerializationDescription<FaultRecord.MutableKeyValuePair> complianceSerializationDescription = new ComplianceSerializationDescription<FaultRecord.MutableKeyValuePair>();
			complianceSerializationDescription.ComplianceStructureId = 98;
			complianceSerializationDescription.RegisterStringPropertyGetterAndSetter(0, (FaultRecord.MutableKeyValuePair item) => item.Key, delegate(FaultRecord.MutableKeyValuePair item, string value)
			{
				item.Key = value;
			});
			complianceSerializationDescription.RegisterStringPropertyGetterAndSetter(1, (FaultRecord.MutableKeyValuePair item) => item.Value, delegate(FaultRecord.MutableKeyValuePair item, string value)
			{
				item.Value = value;
			});
			FaultRecord.description = new ComplianceSerializationDescription<FaultRecord>();
			FaultRecord.description.ComplianceStructureId = 11;
			FaultRecord.description.RegisterComplexCollectionAccessor<FaultRecord.MutableKeyValuePair>(0, (FaultRecord item) => item.Data.Count, (FaultRecord item, int index) => FaultRecord.MutableKeyValuePair.From(item.Data.ToList<KeyValuePair<string, string>>()[index]), delegate(FaultRecord item, FaultRecord.MutableKeyValuePair value, int index)
			{
				item.Data[value.Key] = value.Value;
			}, complianceSerializationDescription);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000BD4E File Offset: 0x00009F4E
		public static ComplianceSerializationDescription<FaultRecord> Description
		{
			get
			{
				return FaultRecord.description;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000BD55 File Offset: 0x00009F55
		public IDictionary<string, string> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x040001E5 RID: 485
		public const string CorrelationId = "CID";

		// Token: 0x040001E6 RID: 486
		public const string MessageId = "MID";

		// Token: 0x040001E7 RID: 487
		public const string MessageSourceId = "MSID";

		// Token: 0x040001E8 RID: 488
		public const string TargetId = "TID";

		// Token: 0x040001E9 RID: 489
		public const string TargetType = "TTYPE";

		// Token: 0x040001EA RID: 490
		public const string TargetDatabase = "TDB";

		// Token: 0x040001EB RID: 491
		public const string TargetMailbox = "TMBX";

		// Token: 0x040001EC RID: 492
		public const string SourceTargetId = "STID";

		// Token: 0x040001ED RID: 493
		public const string SourceTargetType = "STTYPE";

		// Token: 0x040001EE RID: 494
		public const string Tenant = "TENANT";

		// Token: 0x040001EF RID: 495
		public const string TenantGuid = "TGUID";

		// Token: 0x040001F0 RID: 496
		public const string Exception = "EX";

		// Token: 0x040001F1 RID: 497
		public const string ExceptionCode = "EXC";

		// Token: 0x040001F2 RID: 498
		public const string TransientException = "TEX";

		// Token: 0x040001F3 RID: 499
		public const string FatalException = "FEX";

		// Token: 0x040001F4 RID: 500
		public const string HandledException = "HEX";

		// Token: 0x040001F5 RID: 501
		public const string RetryCount = "RC";

		// Token: 0x040001F6 RID: 502
		public const string UserMessage = "UM";

		// Token: 0x040001F7 RID: 503
		public const string ExecutingFunction = "EFUNC";

		// Token: 0x040001F8 RID: 504
		public const string ExecutingFile = "EFILE";

		// Token: 0x040001F9 RID: 505
		public const string ExecutingLine = "ELINE";

		// Token: 0x040001FA RID: 506
		private static ComplianceSerializationDescription<FaultRecord> description;

		// Token: 0x040001FB RID: 507
		private ConcurrentDictionary<string, string> data = new ConcurrentDictionary<string, string>();

		// Token: 0x02000058 RID: 88
		private class MutableKeyValuePair
		{
			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x06000290 RID: 656 RVA: 0x0000BD70 File Offset: 0x00009F70
			// (set) Token: 0x06000291 RID: 657 RVA: 0x0000BD78 File Offset: 0x00009F78
			public string Key { get; set; }

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x06000292 RID: 658 RVA: 0x0000BD81 File Offset: 0x00009F81
			// (set) Token: 0x06000293 RID: 659 RVA: 0x0000BD89 File Offset: 0x00009F89
			public string Value { get; set; }

			// Token: 0x06000294 RID: 660 RVA: 0x0000BD94 File Offset: 0x00009F94
			public static FaultRecord.MutableKeyValuePair From(KeyValuePair<string, string> input)
			{
				return new FaultRecord.MutableKeyValuePair
				{
					Key = input.Key,
					Value = input.Value
				};
			}
		}
	}
}
