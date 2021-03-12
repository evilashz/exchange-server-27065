using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000BC RID: 188
	// (Invoke) Token: 0x06000A16 RID: 2582
	internal delegate MigrationRowSelectorResult MigrationRowSelector(IDictionary<PropertyDefinition, object> rowData);
}
