using System;
using System.ServiceModel.Security;
using System.Threading;
using Microsoft.Ceres.ContentEngine.NlpEvaluators;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Flighting;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000019 RID: 25
	internal class DLPQuerySensitiveTypeTranslationProducer : ScopeIteratorProducerBase<DLPQuerySensitiveTypeTranslationOperator>
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public DLPQuerySensitiveTypeTranslationProducer(IEvaluationContext context) : base(context)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004E08 File Offset: 0x00003008
		protected override void Initialize()
		{
			base.Initialize();
			this.selectPropertiesPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.SelectPropertiesFieldName].Position;
			this.tenantIdPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.TenantIdFieldName].Position;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004E74 File Offset: 0x00003074
		protected override void ProcessRecordInScopeIterationMode()
		{
			IField field = this.holder[this.tenantIdPosition];
			if (field != null)
			{
				this.tenantId = (Guid)field.Value;
			}
			else
			{
				ULS.SendTraceTag(5833443U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPQuerySensitiveTypeTranslationProducer.ProcessRecordInScopeIterationMode :: Could not extract the tenant ID from the record.  Using the empty Guid.");
			}
			if (!VariantConfiguration.IsFeatureEnabled(39, this.tenantId.ToString()))
			{
				return;
			}
			try
			{
				IUpdateableListField<string> updateableListField = this.holder[this.selectPropertiesPosition] as IUpdateableListField<string>;
				if (updateableListField != null)
				{
					bool flag = false;
					bool flag2 = false;
					int num = 0;
					while (num < updateableListField.Count && !flag2)
					{
						string b = updateableListField[num];
						if (string.Equals("SensitiveType", b, StringComparison.OrdinalIgnoreCase))
						{
							flag2 = true;
						}
						else if (string.Equals("SensitiveMatchCount", b, StringComparison.OrdinalIgnoreCase) || string.Equals("SensitiveMatchConfidence", b, StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
						num++;
					}
					if (flag && !flag2)
					{
						updateableListField.Add("SensitiveType");
					}
				}
				base.ProcessRecordInScopeIterationMode();
			}
			catch (ThreadAbortException)
			{
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (SecurityAccessDeniedException)
			{
				throw;
			}
			catch (ArgumentException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ULS.SendTraceTag(5833472U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 10, "DLPQuerySensitiveTypeTranslationProducer.ProcessRecordInScopeIterationMode :: tenantId={0}; exception={1}", new object[]
				{
					this.tenantId,
					ex
				});
				throw;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004FEC File Offset: 0x000031EC
		protected override TreeNode ProcessScopeSubQueryTree(TreeNode scopeSubQueryTree)
		{
			if (scopeSubQueryTree == null)
			{
				throw new ArgumentNullException("scopeSubQueryTree");
			}
			ScopeNode scopeNode = scopeSubQueryTree as ScopeNode;
			if (scopeNode == null || scopeNode.Scope == null)
			{
				ULS.SendTraceTag(5884049U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: argument={0}; Erroneously entered ProcessScopeSubQueryTree with an argument that is not a ScopeNode.  This should not have happened because the scopes of the associated operator should prevent it.", new object[]
				{
					scopeSubQueryTree.ToString()
				});
				return base.ProcessScopeSubQueryTree(scopeSubQueryTree);
			}
			if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
			{
				ULS.SendTraceTag(5884050U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: scope={0}; Processing a tree (or subtree) for data loss prevention translation from a virtual property to the actual index property.", new object[]
				{
					scopeNode.Scope
				});
			}
			string scope = scopeNode.Scope;
			if (string.Equals("SensitiveType", scope, StringComparison.OrdinalIgnoreCase))
			{
				BoundaryNode boundaryNode = scopeNode.Node as BoundaryNode;
				StringNode stringNode;
				TokenNode tokenNode;
				if (boundaryNode != null)
				{
					if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
					{
						ULS.SendTraceTag(6038296U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: Boundary operator mode:{0}; Processing a boundary node in the query tree." + boundaryNode.BoundaryMode);
					}
					if (boundaryNode.BoundaryMode != 2 && boundaryNode.BoundaryMode != null)
					{
						ULS.SendTraceTag(5884051U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: Unexpected boundary operator mode.  For 'SensitiveType' queries, only equality is legal.  Boundary operator mode: " + boundaryNode.BoundaryMode);
						return base.ProcessScopeSubQueryTree(scopeSubQueryTree);
					}
					stringNode = (boundaryNode.Node as StringNode);
					tokenNode = (boundaryNode.Node as TokenNode);
				}
				else
				{
					stringNode = (scopeNode.Node as StringNode);
					tokenNode = (scopeNode.Node as TokenNode);
				}
				string text;
				if (stringNode != null)
				{
					text = stringNode.Text;
				}
				else
				{
					if (tokenNode == null)
					{
						ULS.SendTraceTag(5884052U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: Unexpected node type.  The query looks like it is a 'SensitiveType' style query, but the value portion is not a StringNode or a TokenNode.  Node type: " + boundaryNode.Node.GetType());
						return base.ProcessScopeSubQueryTree(scopeSubQueryTree);
					}
					text = tokenNode.Token;
				}
				if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
				{
					ULS.SendTraceTag(5884053U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: encoding={0}; About to parse the encoding.", new object[]
					{
						text
					});
				}
				SensitiveTypeWildcardExpander sensitiveTypeWildcardExpander = new SensitiveTypeWildcardExpander(text, DLPQuerySensitiveTypeTranslationOperator.Store);
				if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
				{
					ULS.SendTraceTag(5884054U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: About to create the new subtree.");
				}
				return sensitiveTypeWildcardExpander.CreateSubtree();
			}
			if (string.Equals("SensitiveMatchConfidence", scope, StringComparison.OrdinalIgnoreCase) || string.Equals("SensitiveMatchCount", scope, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(scope + " is illegal to use in a query.  You may use it as a select property.  In queries, use SensitiveType");
			}
			ULS.SendTraceTag(5884055U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySensitiveTypeTranslationProducer.ProcessScopeSubQueryTree :: scope={0}; This is not a legal scope for this producer.  There must be some error in which the operator has the wrong scopes or a code change is incorrect.  Ignoring and continuing.", new object[]
			{
				scope
			});
			return base.ProcessScopeSubQueryTree(scopeSubQueryTree);
		}

		// Token: 0x04000074 RID: 116
		private int selectPropertiesPosition = -1;

		// Token: 0x04000075 RID: 117
		private Guid tenantId = Guid.Empty;

		// Token: 0x04000076 RID: 118
		private int tenantIdPosition = -1;
	}
}
