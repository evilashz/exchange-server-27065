using System;
using System.Collections.Generic;
using Microsoft.Ceres.ContentEngine.NlpOperators;
using Microsoft.Ceres.DataLossPrevention.Common;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Operators.Graphs;
using Microsoft.Ceres.Evaluation.Operators.PlugIns;
using Microsoft.Ceres.Evaluation.Operators.Utilities;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x0200001A RID: 26
	[Operator("DLPQuerySensitiveTypeTranslation")]
	[Serializable]
	public class DLPQuerySensitiveTypeTranslationOperator : ScopeIteratorOperatorBase<DLPQuerySensitiveTypeTranslationOperator>
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005271 File Offset: 0x00003471
		internal static FASTClassificationStore Store
		{
			get
			{
				return DLPQuerySensitiveTypeTranslationOperator.store;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005278 File Offset: 0x00003478
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005280 File Offset: 0x00003480
		[Property(Name = "selectPropertiesFieldName")]
		public string SelectPropertiesFieldName { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005289 File Offset: 0x00003489
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005291 File Offset: 0x00003491
		[Property(Name = "tenantIdFieldName")]
		public string TenantIdFieldName { get; set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x0000529C File Offset: 0x0000349C
		public DLPQuerySensitiveTypeTranslationOperator()
		{
			base.QueryTreeFieldName = "QueryTree";
			base.OperatorMode = "ScopeIteration";
			base.ScopeNodeProcessMode = "ScopeNode";
			base.ScopesProvider = "Property";
			this.SelectPropertiesFieldName = "SelectProperties";
			this.TenantIdFieldName = "TenantId";
			base.Scopes = new string[]
			{
				"SensitiveType",
				"SensitiveMatchCount",
				"SensitiveMatchConfidence"
			};
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005317 File Offset: 0x00003517
		protected override void ValidateProperties(OperatorStatus status, IList<Edge> inputEdges)
		{
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.SelectPropertiesFieldName, BuiltInTypes.ListType(BuiltInTypes.StringType), inputEdges);
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.TenantIdFieldName, BuiltInTypes.GuidType, inputEdges);
		}

		// Token: 0x04000077 RID: 119
		private static FASTClassificationStore store = new FASTClassificationStore();
	}
}
