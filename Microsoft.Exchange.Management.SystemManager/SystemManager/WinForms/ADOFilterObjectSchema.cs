using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A2 RID: 162
	internal class ADOFilterObjectSchema : ObjectSchema
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00014151 File Offset: 0x00012351
		public ADOFilterObjectSchema(IList<PropertyDefinition> definitions)
		{
			this.allFilterableProperties = new ReadOnlyCollection<PropertyDefinition>(definitions);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00014165 File Offset: 0x00012365
		public override ReadOnlyCollection<PropertyDefinition> AllFilterableProperties
		{
			get
			{
				return this.allFilterableProperties;
			}
		}

		// Token: 0x040001BC RID: 444
		private ReadOnlyCollection<PropertyDefinition> allFilterableProperties;
	}
}
