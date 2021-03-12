using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A3 RID: 419
	internal sealed class UnifiedPolicyForSaveDataSet : ConfigurablePropertyBag
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x0003608C File Offset: 0x0003428C
		static UnifiedPolicyForSaveDataSet()
		{
			foreach (TvpInfo tvpInfo in UnifiedPolicyForSaveDataSet.tvpPrototypeList)
			{
				UnifiedPolicyForSaveDataSet.mapTableToTvpColumnInfo.Add(tvpInfo.TableName, tvpInfo.Columns);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00036158 File Offset: 0x00034358
		public override ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00036160 File Offset: 0x00034360
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0003616D File Offset: 0x0003436D
		public object PhysicalPartionId
		{
			get
			{
				return this[UnifiedPolicyCommonSchema.PhysicalInstanceKeyProp];
			}
			set
			{
				this[UnifiedPolicyCommonSchema.PhysicalInstanceKeyProp] = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0003617B File Offset: 0x0003437B
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x00036188 File Offset: 0x00034388
		public object FssCopyId
		{
			get
			{
				return this[UnifiedPolicyCommonSchema.FssCopyIdProp];
			}
			set
			{
				this[UnifiedPolicyCommonSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00036198 File Offset: 0x00034398
		public static UnifiedPolicyForSaveDataSet CreateDataSet(object partitionId, IEnumerable<UnifiedPolicyTrace> policyTraceList, int? fssCopyId = null)
		{
			if (partitionId == null)
			{
				throw new ArgumentNullException("partitionId");
			}
			UnifiedPolicyForSaveDataSet unifiedPolicyForSaveDataSet = UnifiedPolicyForSaveDataSet.CreateSkeletonUnifiedPolicyForSaveDataSetObject();
			unifiedPolicyForSaveDataSet.PhysicalPartionId = (int)partitionId;
			if (fssCopyId != null)
			{
				unifiedPolicyForSaveDataSet.FssCopyId = fssCopyId;
			}
			foreach (UnifiedPolicyTrace unifiedPolicyTrace in policyTraceList)
			{
				UnifiedPolicyForSaveDataSet.SerializeObjectToDataTable<UnifiedPolicyTrace>(unifiedPolicyTrace, UnifiedPolicyDataSetSchema.UnifiedPolicyObjectTableProperty, ref unifiedPolicyForSaveDataSet);
				foreach (UnifiedPolicyRule unifiedPolicyRule in unifiedPolicyTrace.Rules)
				{
					UnifiedPolicyForSaveDataSet.SetCommonProperties(unifiedPolicyTrace, unifiedPolicyRule);
					UnifiedPolicyForSaveDataSet.SerializeObjectToDataTable<UnifiedPolicyRule>(unifiedPolicyRule, UnifiedPolicyDataSetSchema.UnifiedPolicyRuleTableProperty, ref unifiedPolicyForSaveDataSet);
					foreach (UnifiedPolicyRuleAction unifiedPolicyRuleAction in unifiedPolicyRule.Actions)
					{
						UnifiedPolicyForSaveDataSet.SetCommonProperties(unifiedPolicyTrace, unifiedPolicyRuleAction);
						UnifiedPolicyForSaveDataSet.SerializeObjectToDataTable<UnifiedPolicyRuleAction>(unifiedPolicyRuleAction, UnifiedPolicyDataSetSchema.UnifiedPolicyRuleActionTableProperty, ref unifiedPolicyForSaveDataSet);
					}
					foreach (UnifiedPolicyRuleClassification unifiedPolicyRuleClassification in unifiedPolicyRule.Classifications)
					{
						UnifiedPolicyForSaveDataSet.SetCommonProperties(unifiedPolicyTrace, unifiedPolicyRuleClassification);
						UnifiedPolicyForSaveDataSet.SerializeObjectToDataTable<UnifiedPolicyRuleClassification>(unifiedPolicyRuleClassification, UnifiedPolicyDataSetSchema.UnifiedPolicyRuleClassificationTableProperty, ref unifiedPolicyForSaveDataSet);
					}
				}
			}
			return unifiedPolicyForSaveDataSet;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00036328 File Offset: 0x00034528
		public override Type GetSchemaType()
		{
			return typeof(UnifiedPolicyDataSetSchema);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00036334 File Offset: 0x00034534
		public int GetDatasize()
		{
			int num = 0;
			foreach (HygienePropertyDefinition propertyDefinition in UnifiedPolicyForSaveDataSet.tvpDataTables)
			{
				DataTable dataTable = this[propertyDefinition] as DataTable;
				if (dataTable != null)
				{
					num += dataTable.Rows.Count;
				}
			}
			return num;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00036380 File Offset: 0x00034580
		private static void SetCommonProperties(UnifiedPolicyTrace trace, ConfigurablePropertyBag dataObject)
		{
			dataObject[UnifiedPolicyCommonSchema.OrganizationalUnitRootProperty] = trace.OrganizationalUnitRoot;
			dataObject[UnifiedPolicyCommonSchema.ObjectIdProperty] = trace.ObjectId;
			dataObject[UnifiedPolicyCommonSchema.DataSourceProperty] = trace.DataSource;
			dataObject[UnifiedPolicyCommonSchema.HashBucketProperty] = trace[UnifiedPolicyCommonSchema.HashBucketProperty];
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000363E0 File Offset: 0x000345E0
		private static TvpInfo CreateTvpInfoPrototype(HygienePropertyDefinition tableName, HygienePropertyDefinition[] columnDefinitions)
		{
			HygienePropertyDefinition[] array = new HygienePropertyDefinition[columnDefinitions.Length];
			DataTable dataTable = new DataTable();
			DataColumnCollection columns = dataTable.Columns;
			dataTable.TableName = tableName.Name;
			foreach (HygienePropertyDefinition hygienePropertyDefinition in columnDefinitions)
			{
				if (!hygienePropertyDefinition.IsCalculated)
				{
					DataColumn dataColumn = columns.Add(hygienePropertyDefinition.Name, (hygienePropertyDefinition.Type == typeof(byte[])) ? hygienePropertyDefinition.Type : DalHelper.ConvertToStoreType(hygienePropertyDefinition));
					array[dataColumn.Ordinal] = hygienePropertyDefinition;
				}
			}
			dataTable.BeginLoadData();
			return new TvpInfo(tableName, dataTable, array);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00036480 File Offset: 0x00034680
		private static UnifiedPolicyForSaveDataSet CreateSkeletonUnifiedPolicyForSaveDataSetObject()
		{
			UnifiedPolicyForSaveDataSet unifiedPolicyForSaveDataSet = new UnifiedPolicyForSaveDataSet();
			foreach (TvpInfo tvpInfo in UnifiedPolicyForSaveDataSet.tvpPrototypeList)
			{
				unifiedPolicyForSaveDataSet[tvpInfo.TableName] = tvpInfo.Tvp.Clone();
			}
			return unifiedPolicyForSaveDataSet;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000364C4 File Offset: 0x000346C4
		private static void SerializeObjectToDataTable<T>(T source, HygienePropertyDefinition tableDefinition, ref UnifiedPolicyForSaveDataSet saveDataSet) where T : ConfigurablePropertyBag
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			DataTable dataTable = saveDataSet[tableDefinition] as DataTable;
			if (dataTable == null)
			{
				throw new ArgumentNullException("table");
			}
			HygienePropertyDefinition[] columns = UnifiedPolicyForSaveDataSet.mapTableToTvpColumnInfo[tableDefinition];
			DataRow row = dataTable.NewRow();
			UnifiedPolicyForSaveDataSet.PopulateRow(row, columns, source);
			dataTable.Rows.Add(row);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003652C File Offset: 0x0003472C
		private static void PopulateRow(DataRow row, HygienePropertyDefinition[] columns, ConfigurablePropertyBag dataSource)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			for (int i = 0; i < columns.Length; i++)
			{
				HygienePropertyDefinition hygienePropertyDefinition = columns[i];
				if (hygienePropertyDefinition != null && !hygienePropertyDefinition.IsCalculated)
				{
					object obj = dataSource[hygienePropertyDefinition];
					if (obj != hygienePropertyDefinition.DefaultValue)
					{
						row[i] = obj;
					}
				}
			}
		}

		// Token: 0x04000872 RID: 2162
		private static TvpInfo[] tvpPrototypeList = new TvpInfo[]
		{
			UnifiedPolicyForSaveDataSet.CreateTvpInfoPrototype(UnifiedPolicyDataSetSchema.UnifiedPolicyObjectTableProperty, UnifiedPolicyTrace.Properties),
			UnifiedPolicyForSaveDataSet.CreateTvpInfoPrototype(UnifiedPolicyDataSetSchema.UnifiedPolicyRuleTableProperty, UnifiedPolicyRule.Properties),
			UnifiedPolicyForSaveDataSet.CreateTvpInfoPrototype(UnifiedPolicyDataSetSchema.UnifiedPolicyRuleActionTableProperty, UnifiedPolicyRuleAction.Properties),
			UnifiedPolicyForSaveDataSet.CreateTvpInfoPrototype(UnifiedPolicyDataSetSchema.UnifiedPolicyRuleClassificationTableProperty, UnifiedPolicyRuleClassification.Properties)
		};

		// Token: 0x04000873 RID: 2163
		private static HygienePropertyDefinition[] tvpDataTables = new HygienePropertyDefinition[]
		{
			UnifiedPolicyDataSetSchema.UnifiedPolicyObjectTableProperty,
			UnifiedPolicyDataSetSchema.UnifiedPolicyRuleTableProperty,
			UnifiedPolicyDataSetSchema.UnifiedPolicyRuleActionTableProperty,
			UnifiedPolicyDataSetSchema.UnifiedPolicyRuleClassificationTableProperty
		};

		// Token: 0x04000874 RID: 2164
		private static Dictionary<HygienePropertyDefinition, HygienePropertyDefinition[]> mapTableToTvpColumnInfo = new Dictionary<HygienePropertyDefinition, HygienePropertyDefinition[]>();

		// Token: 0x04000875 RID: 2165
		private ObjectId identity = new ConfigObjectId(CombGuidGenerator.NewGuid().ToString());
	}
}
