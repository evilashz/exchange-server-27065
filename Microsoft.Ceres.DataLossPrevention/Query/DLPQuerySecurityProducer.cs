using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Threading;
using Microsoft.Ceres.ContentEngine.NlpEvaluators;
using Microsoft.Ceres.DataLossPrevention.Common;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Flighting;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000013 RID: 19
	internal class DLPQuerySecurityProducer : ScopeIteratorProducerBase<DLPQuerySecurityOperator>
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000043CB File Offset: 0x000025CB
		public DLPQuerySecurityProducer(IEvaluationContext context) : base(context)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000043F4 File Offset: 0x000025F4
		protected override void Initialize()
		{
			base.Initialize();
			this.tenantIdPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.TenantIdFieldName].Position;
			this.selectPropertiesPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.SelectPropertiesFieldName].Position;
			this.propertyBagPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.PropertyBagFieldName].Position;
			this.classificationScopes = base.Operator.Scopes;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000449C File Offset: 0x0000269C
		protected override void ProcessRecordInScopeIterationMode()
		{
			ULS.SendTraceTag(5256286U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPSecurityTrimmerProducer.ProcessRecordInScopeIterationMode :: record={0}", new object[]
			{
				this.holder
			});
			IField field = this.holder[this.tenantIdPosition];
			if (field != null)
			{
				this.tenantId = (Guid)field.Value;
			}
			else
			{
				ULS.SendTraceTag(5256287U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPSecurityTrimmerProducer.ProcessRecordInScopeIterationMode :: Could not extract the tenant ID from the record.  Using the empty Guid.");
			}
			if (!VariantConfiguration.IsFeatureEnabled(62, this.tenantId.ToString()))
			{
				return;
			}
			try
			{
				this.isDLPAuthorized = this.CanAccessDataLossPreventionProperties(this.holder);
				if (!this.isDLPAuthorized)
				{
					IListField<string> listField = this.holder[this.selectPropertiesPosition] as IListField<string>;
					if (listField != null)
					{
						for (int i = 0; i < listField.Count; i++)
						{
							string text = listField[i];
							if (this.classificationScopes.Contains(text, StringComparer.OrdinalIgnoreCase))
							{
								ULS.SendTraceTag(5256288U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPSecurityTrimmerProducer.ProcessRecordInScopeIterationMode :: tenantId={0}; selectProperty={1}; User is not authorized for data-loss-prevention properties, but attempted to access at least one as a select property.", new object[]
								{
									this.tenantId,
									text
								});
								throw new SecurityAccessDeniedException("You do not have access to this property: " + text);
							}
						}
					}
					base.ProcessRecordInScopeIterationMode();
				}
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
			catch (Exception ex)
			{
				ULS.SendTraceTag(5256289U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 10, "DLPSecurityTrimmerProducer.ProcessRecordInScopeIterationMode :: tenantId={0}; exception={1}", new object[]
				{
					this.tenantId,
					ex
				});
				throw;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004654 File Offset: 0x00002854
		protected override TreeNode ProcessScopeSubQueryTree(TreeNode scopeSubQueryTree)
		{
			if (this.isDLPAuthorized)
			{
				return scopeSubQueryTree;
			}
			ScopeNode scopeNode = scopeSubQueryTree as ScopeNode;
			if (scopeNode == null || scopeNode.Scope == null)
			{
				ULS.SendTraceTag(5578898U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySecurityProducer.ProcessScopeSubQueryTree ::tenantId={0};  Argument is null or is not a ScopeNode.", new object[]
				{
					this.tenantId
				});
				return scopeSubQueryTree;
			}
			string scope = scopeNode.Scope;
			if (this.classificationScopes.Contains(scope))
			{
				ULS.SendTraceTag(5256290U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPQuerySecurityProducer.ProcessScopeSubQueryTree :: tenantId={0}; property={1}; Attempt by unauthorized user to access data-loss-prevention properties.", new object[]
				{
					this.tenantId,
					scope
				});
				throw new SecurityAccessDeniedException("You do not have access to the property identified in this scope: " + scopeSubQueryTree);
			}
			ULS.SendTraceTag(5578899U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPQuerySecurityProducer.ProcessScopeSubQueryTree :: tenantId={0}; scopeSubQueryTree={1}; Erroneously entered ProcessScopeSubQueryTree with an argument that is not a classification property.  This should not have happened because the scopes of the associated operator should only contain classification properties.", new object[]
			{
				this.tenantId,
				scopeSubQueryTree.ToString()
			});
			return scopeSubQueryTree;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004740 File Offset: 0x00002940
		private bool CanAccessDataLossPreventionProperties(IRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			IUpdateableBucketField updateableBucketField = record[this.propertyBagPosition] as IUpdateableBucketField;
			if (updateableBucketField == null)
			{
				ULS.SendTraceTag(5256291U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySecurityProducer.CanAccessDataLossPreventionProperties :: tenantId={0}; The record did not have a 'PropertyBag' entry.   We cannot determine if the user has permission, so we assume not.", new object[]
				{
					this.tenantId
				});
				return false;
			}
			IStringField stringField = updateableBucketField["RootWebTemplate"] as IStringField;
			if (stringField == null)
			{
				ULS.SendTraceTag(5256320U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySecurityProducer.CanAccessDataLossPreventionProperties :: tenantId={0}; The record did not have a 'RootWebTemplate' entry in its 'PropertyBag' or the value was not of type IStringField.   We cannot determine if the user has permission, so we assume not.", new object[]
				{
					this.tenantId
				});
				return false;
			}
			if (DLPQuerySecurityProducer.AuthorizedWebTemplateIdentifiers.Contains(stringField.StringValue, StringComparer.Ordinal))
			{
				ULS.SendTraceTag(5256321U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySecurityProducer.CanAccessDataLossPreventionProperties :: tenantId={0}; RootWebTemplate={1}; The query comes from an authorized template.  The user, by virtue of having access to an authorized template, has permission to query for data-loss-prevention properties.", new object[]
				{
					this.tenantId,
					stringField.StringValue
				});
				return true;
			}
			ULS.SendTraceTag(5256322U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPQuerySecurityProducer.CanAccessDataLossPreventionProperties :: tenantId={0}; RootWebTemplate={1}; The query does not come from an authorized template.  If the query contains data-loss-prevention properties, it will be rejected.  Identifier for the web template where the query originated: ", new object[]
			{
				this.tenantId,
				stringField.StringValue
			});
			return false;
		}

		// Token: 0x04000061 RID: 97
		private static HashSet<string> AuthorizedWebTemplateIdentifiers = new HashSet<string>
		{
			"EDISC",
			"POLICYCTR"
		};

		// Token: 0x04000062 RID: 98
		private static FASTClassificationStore ruleStore = new FASTClassificationStore();

		// Token: 0x04000063 RID: 99
		private bool isDLPAuthorized;

		// Token: 0x04000064 RID: 100
		private int propertyBagPosition = -1;

		// Token: 0x04000065 RID: 101
		private int selectPropertiesPosition = -1;

		// Token: 0x04000066 RID: 102
		private Guid tenantId = Guid.Empty;

		// Token: 0x04000067 RID: 103
		private int tenantIdPosition = -1;

		// Token: 0x04000068 RID: 104
		private IList<string> classificationScopes;
	}
}
