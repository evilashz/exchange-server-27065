using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C0 RID: 192
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationStoreObject : IDisposable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A32 RID: 2610
		StoreObjectId Id { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A33 RID: 2611
		ExDateTime CreationTime { get; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A34 RID: 2612
		string Name { get; }

		// Token: 0x06000A35 RID: 2613
		void Delete(PropertyDefinition propertyDefinition);

		// Token: 0x06000A36 RID: 2614
		T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue);

		// Token: 0x06000A37 RID: 2615
		void Load(ICollection<PropertyDefinition> properties);

		// Token: 0x06000A38 RID: 2616
		void OpenAsReadWrite();

		// Token: 0x06000A39 RID: 2617
		void LoadMessageIdProperties();

		// Token: 0x06000A3A RID: 2618
		void Save(SaveMode saveMode);

		// Token: 0x06000A3B RID: 2619
		XElement GetDiagnosticInfo(ICollection<PropertyDefinition> properties, MigrationDiagnosticArgument argument);
	}
}
