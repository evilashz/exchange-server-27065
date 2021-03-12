using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess.ExtensionMethods
{
	// Token: 0x02000094 RID: 148
	public static class ExtensionMethods
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001D288 File Offset: 0x0001B488
		public static IEnumerable<SimpleQueryOperator> CreateOperators(this SimpleQueryOperator.SimpleQueryOperatorDefinition[] definitions, IConnectionProvider connectionProvider)
		{
			SimpleQueryOperator[] array = new SimpleQueryOperator[definitions.Length];
			for (int i = 0; i < definitions.Length; i++)
			{
				if (definitions[i] != null)
				{
					array[i] = definitions[i].CreateOperator(connectionProvider);
				}
			}
			return array;
		}
	}
}
