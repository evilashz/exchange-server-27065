using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200003B RID: 59
	// (Invoke) Token: 0x06000285 RID: 645
	internal delegate AnchorRowSelectorResult AnchorRowSelector(IDictionary<PropertyDefinition, object> rowData);
}
