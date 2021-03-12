using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C7 RID: 199
	public class JetTableFunction : TableFunction
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x000297CD File Offset: 0x000279CD
		public JetTableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns) : base(name, getTableContents, getColumnFromRow, visibility, parameterTypes, indexes, columns)
		{
		}
	}
}
