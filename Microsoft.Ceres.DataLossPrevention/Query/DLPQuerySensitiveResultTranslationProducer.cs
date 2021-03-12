using System;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Ceres.Evaluation.Processing.Utilities;
using Microsoft.Ceres.Flighting;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000016 RID: 22
	internal class DLPQuerySensitiveResultTranslationProducer : TransformationBasedProducer<DLPQuerySensitiveResultTranslationOperator>
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000049B0 File Offset: 0x00002BB0
		protected override void Initialize(TransformationHandler handler)
		{
			this.otherPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.OtherFieldName].Position;
			this.partitionIdPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.PartitionIdFieldName].Position;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004A14 File Offset: 0x00002C14
		protected override void FirstRecord(IRecord record)
		{
			this.isIPClassificationQueryEnabled = VariantConfiguration.IsFeatureEnabled(39, this.GetTenantId(record).ToString());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004A44 File Offset: 0x00002C44
		private Guid GetTenantId(IRecord record)
		{
			Guid result = Guid.Empty;
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			IField field = record[this.partitionIdPosition];
			if (field != null)
			{
				result = (Guid)field.Value;
			}
			else
			{
				ULS.SendTraceTag(5833438U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPQuerySensitiveResultTranslationProducer.GetTenantId :: Could not extract the 'PartitionId' entry (which represents the tenant ID) from the record.  Using the empty Guid.");
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004A9C File Offset: 0x00002C9C
		public override void ProcessRecordCore(IRecord record)
		{
			Guid tenantId = this.GetTenantId(record);
			if (!this.isIPClassificationQueryEnabled)
			{
				base.SetNextRecord();
				return;
			}
			IUpdateableBucketField updateableBucketField = record[this.otherPosition] as IUpdateableBucketField;
			if (updateableBucketField != null)
			{
				for (int i = 0; i < updateableBucketField.FieldCount; i++)
				{
					string text = updateableBucketField.Name(i);
					if (string.Equals(text, "SensitiveType", StringComparison.OrdinalIgnoreCase))
					{
						IUpdateableListField<string> updateableListField = updateableBucketField[text] as IUpdateableListField<string>;
						if (updateableListField == null)
						{
							ULS.SendTraceTag(5833439U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySensitiveResultTranslationProducer.ProcessRecordCore :: tenantId={0}, fieldName={1}; Found a result entry for the field name (expected to represent the sensitive type list), but its type was not IUpdateableListField<string> or its value was null.  Cannot do any data-loss-prevention result-set translation.", new object[]
							{
								tenantId,
								text
							});
						}
						else
						{
							for (int j = 0; j < updateableListField.Count; j++)
							{
								string text2 = DLPQuerySensitiveResultTranslationOperator.Store.RuleIdToRuleName(updateableListField[j]);
								if (text2 == null)
								{
									ULS.SendTraceTag(5833440U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySensitiveResultTranslationProducer.ProcessRecordCore :: tenantId={0}, fieldIndex={1}, fieldValue={2}; Found a result entry for the field name (expected to represent the sensitive type list), but the value at this index was either null or an invalid rule ID.  Cannot translate this value.", new object[]
									{
										tenantId,
										j,
										updateableListField[j]
									});
								}
								else
								{
									updateableListField[j] = text2;
								}
							}
						}
					}
					else if (string.Equals(text, "SensitiveMatchCount", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "SensitiveMatchConfidence", StringComparison.OrdinalIgnoreCase))
					{
						IUpdateableListField<long?> updateableListField2 = updateableBucketField[text] as IUpdateableListField<long?>;
						long?[] array = updateableBucketField[text].Value as long?[];
						if (array == null || updateableListField2 == null)
						{
							ULS.SendTraceTag(5833441U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySensitiveResultTranslationProducer.ProcessRecordCore :: tenantId={0}, fieldName={1}; Found a result entry for the field name, but its value was null or not of type long?[] or the field was not of type IUpdateableListField<long?>.  Cannot do any data-loss-prevention result-set translation.", new object[]
							{
								tenantId,
								text
							});
						}
						else
						{
							for (int k = 0; k < array.Length; k++)
							{
								array[k] &= (long)((ulong)-1);
							}
							updateableListField2.Value = array;
						}
					}
				}
			}
			else
			{
				ULS.SendTraceTag(5833442U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySensitiveResultTranslationProducer.ProcessRecordCore :: tenantId={0}; The 'Other' field in the record was null, so we cannot do any data-loss-prevention result-set translation.", new object[]
				{
					tenantId
				});
			}
			base.SetNextRecord();
		}

		// Token: 0x0400006D RID: 109
		private int otherPosition = -1;

		// Token: 0x0400006E RID: 110
		private int partitionIdPosition = -1;

		// Token: 0x0400006F RID: 111
		private bool isIPClassificationQueryEnabled;
	}
}
