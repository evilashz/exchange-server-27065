using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200083D RID: 2109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ManifestFolderChange : ManifestChangeBase
	{
		// Token: 0x06004E62 RID: 20066 RVA: 0x001487B6 File Offset: 0x001469B6
		internal ManifestFolderChange(PropValue[] propertyValues)
		{
			this.propertyValues = propertyValues;
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x001487C5 File Offset: 0x001469C5
		public PropValue[] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x04002AC3 RID: 10947
		private readonly PropValue[] propertyValues;
	}
}
