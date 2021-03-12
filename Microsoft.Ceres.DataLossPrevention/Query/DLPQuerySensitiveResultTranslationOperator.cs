using System;
using System.Collections.Generic;
using Microsoft.Ceres.DataLossPrevention.Common;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Operators.Graphs;
using Microsoft.Ceres.Evaluation.Operators.PlugIns;
using Microsoft.Ceres.Evaluation.Operators.Utilities;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000017 RID: 23
	[Operator("DLPQuerySensitiveResultTranslation")]
	[Serializable]
	public class DLPQuerySensitiveResultTranslationOperator : TypedOperatorBase<DLPQuerySensitiveResultTranslationOperator>, ITransformationBasedOperator
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004CF0 File Offset: 0x00002EF0
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004CF8 File Offset: 0x00002EF8
		[Property(Name = "otherField")]
		public string OtherFieldName { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004D01 File Offset: 0x00002F01
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004D09 File Offset: 0x00002F09
		[Property(Name = "partitionIdField")]
		public string PartitionIdFieldName { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004D12 File Offset: 0x00002F12
		internal static FASTClassificationStore Store
		{
			get
			{
				return DLPQuerySensitiveResultTranslationOperator.store;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004D19 File Offset: 0x00002F19
		public DLPQuerySensitiveResultTranslationOperator()
		{
			this.PartitionIdFieldName = "PartitionId";
			this.OtherFieldName = "Other";
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004D38 File Offset: 0x00002F38
		protected override void ValidateAndType(OperatorStatus status, IList<Edge> inputEdges)
		{
			if (status == null)
			{
				throw new ArgumentNullException("status");
			}
			if (!ValidationUtils.ValidateInputExists(status, inputEdges))
			{
				return;
			}
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.OtherFieldName, BuiltInTypes.BucketType, inputEdges);
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.PartitionIdFieldName, BuiltInTypes.GuidType, inputEdges);
			this.Specification = new TransformationSpecification();
			this.Specification.AddAll(0, inputEdges[0].RecordSetType);
			status.SetSingleOutput(this.Specification.MakeOutputType());
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004DB7 File Offset: 0x00002FB7
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004DBF File Offset: 0x00002FBF
		public TransformationSpecification Specification { get; private set; }

		// Token: 0x04000070 RID: 112
		private static FASTClassificationStore store = new FASTClassificationStore();
	}
}
