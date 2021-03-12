using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x0200010D RID: 269
	internal class SqlCategorizedTableOperator : CategorizedTableOperator
	{
		// Token: 0x06000B40 RID: 2880 RVA: 0x00037E84 File Offset: 0x00036084
		internal SqlCategorizedTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, CategorizedTableParams categorizedTableParams, CategorizedTableCollapseState collapseState, IList<Column> columnsToFetch, IReadOnlyDictionary<Column, Column> additionalHeaderRenameDictionary, IReadOnlyDictionary<Column, Column> additionalLeafRenameDictionary, SearchCriteria restriction, int skipTo, int maxRows, KeyRange keyRange, bool backwards, bool frequentOperation) : this(connectionProvider, new CategorizedTableOperator.CategorizedTableOperatorDefinition(culture, table, categorizedTableParams, collapseState, columnsToFetch, additionalHeaderRenameDictionary, additionalLeafRenameDictionary, (restriction is SearchCriteriaTrue) ? null : restriction, skipTo, maxRows, keyRange, backwards, frequentOperation))
		{
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00037EC1 File Offset: 0x000360C1
		internal SqlCategorizedTableOperator(IConnectionProvider connectionProvider, CategorizedTableOperator.CategorizedTableOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}
	}
}
