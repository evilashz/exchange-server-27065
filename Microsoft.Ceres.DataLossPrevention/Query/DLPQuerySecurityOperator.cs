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
	// Token: 0x02000014 RID: 20
	[Operator("DLPQuerySecurity")]
	[Serializable]
	public class DLPQuerySecurityOperator : ScopeIteratorOperatorBase<DLPQuerySecurityOperator>
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000048A7 File Offset: 0x00002AA7
		internal static FASTClassificationStore Store
		{
			get
			{
				return DLPQuerySecurityOperator.store;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000048AE File Offset: 0x00002AAE
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000048B6 File Offset: 0x00002AB6
		[Property(Name = "propertyBagFieldName")]
		public string PropertyBagFieldName { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000048BF File Offset: 0x00002ABF
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000048C7 File Offset: 0x00002AC7
		[Property(Name = "selectPropertiesFieldName")]
		public string SelectPropertiesFieldName { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000048D0 File Offset: 0x00002AD0
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000048D8 File Offset: 0x00002AD8
		[Property(Name = "tenantIdFieldName")]
		public string TenantIdFieldName { get; set; }

		// Token: 0x060000A6 RID: 166 RVA: 0x000048E4 File Offset: 0x00002AE4
		public DLPQuerySecurityOperator()
		{
			base.QueryTreeFieldName = "QueryTree";
			base.OperatorMode = "ScopeIteration";
			base.ScopeNodeProcessMode = "ScopeNode";
			base.ScopesProvider = "Property";
			base.Scopes = DLPQuerySecurityOperator.Store.SecurityScopes();
			this.PropertyBagFieldName = "PropertyBag";
			this.SelectPropertiesFieldName = "SelectProperties";
			this.TenantIdFieldName = "TenantId";
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004954 File Offset: 0x00002B54
		protected override void ValidateProperties(OperatorStatus status, IList<Edge> inputEdges)
		{
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.PropertyBagFieldName, BuiltInTypes.BucketType, inputEdges);
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.SelectPropertiesFieldName, BuiltInTypes.ListType(BuiltInTypes.StringType), inputEdges);
			ValidationUtils.ValidateTypedInputFieldExistence(status, this.TenantIdFieldName, BuiltInTypes.GuidType, inputEdges);
		}

		// Token: 0x04000069 RID: 105
		private static FASTClassificationStore store = new FASTClassificationStore();
	}
}
