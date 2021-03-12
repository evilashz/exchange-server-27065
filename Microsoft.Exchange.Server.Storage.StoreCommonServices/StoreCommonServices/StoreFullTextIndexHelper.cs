using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200015C RID: 348
	public static class StoreFullTextIndexHelper
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00044393 File Offset: 0x00042593
		internal static IDisposable SetFullTextIndexQueryTestHook(Func<IFullTextIndexQuery> testHook)
		{
			return StoreFullTextIndexHelper.FtiQueryCreator.SetTestHook(testHook);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00044410 File Offset: 0x00042610
		internal static IList<StoreFullTextIndexQuery> CollectAllFullTextQueries(DataAccessOperator.DataAccessOperatorDefinition dataAccessOperatorDefinition)
		{
			List<StoreFullTextIndexQuery> queries = null;
			if (dataAccessOperatorDefinition != null)
			{
				dataAccessOperatorDefinition.EnumerateDescendants(delegate(DataAccessOperator.DataAccessOperatorDefinition operatorDefinition)
				{
					TableFunctionOperator.TableFunctionOperatorDefinition tableFunctionOperatorDefinition = operatorDefinition as TableFunctionOperator.TableFunctionOperatorDefinition;
					if (tableFunctionOperatorDefinition != null && tableFunctionOperatorDefinition.Parameters != null && tableFunctionOperatorDefinition.Parameters.Length == 1 && tableFunctionOperatorDefinition.Parameters[0] is StoreFullTextIndexQuery)
					{
						if (queries == null)
						{
							queries = new List<StoreFullTextIndexQuery>(3);
						}
						queries.Add((StoreFullTextIndexQuery)tableFunctionOperatorDefinition.Parameters[0]);
					}
				});
			}
			return queries;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0004444C File Offset: 0x0004264C
		internal static bool IsFullTextIndexTableFunctionOperatorDefinition(DataAccessOperator.DataAccessOperatorDefinition dataAccessOperatorDefinition)
		{
			TableFunctionOperator.TableFunctionOperatorDefinition tableFunctionOperatorDefinition = dataAccessOperatorDefinition as TableFunctionOperator.TableFunctionOperatorDefinition;
			return tableFunctionOperatorDefinition != null && tableFunctionOperatorDefinition.Parameters != null && tableFunctionOperatorDefinition.Parameters.Length == 1 && tableFunctionOperatorDefinition.Parameters[0] is StoreFullTextIndexQuery;
		}

		// Token: 0x04000780 RID: 1920
		internal static readonly Hookable<Func<IFullTextIndexQuery>> FtiQueryCreator = Hookable<Func<IFullTextIndexQuery>>.Create(true, () => new FullTextIndexQuery());
	}
}
