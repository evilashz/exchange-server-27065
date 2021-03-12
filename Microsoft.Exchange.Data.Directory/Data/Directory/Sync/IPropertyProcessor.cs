using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000817 RID: 2071
	internal interface IPropertyProcessor
	{
		// Token: 0x06006683 RID: 26243
		void Process<T>(SyncPropertyDefinition propertyDefinition, ref T values) where T : DirectoryProperty, new();
	}
}
