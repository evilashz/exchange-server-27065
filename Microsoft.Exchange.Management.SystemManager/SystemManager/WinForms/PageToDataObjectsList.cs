using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000BE RID: 190
	[DDIDictionaryDecorate(UseKeys = false, ExpandInnerCollection = true, AttributeType = typeof(DDIDataObjectNameExistAttribute))]
	[DDIDictionaryDecorate(UseKeys = true, AttributeType = typeof(DDIValidPropertyPageNameAttribute))]
	public class PageToDataObjectsList : Dictionary<string, string[]>
	{
	}
}
