using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationSerializable
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D5 RID: 469
		PropertyDefinition[] PropertyDefinitions { get; }

		// Token: 0x060001D6 RID: 470
		bool ReadFromMessageItem(IMigrationStoreObject message);

		// Token: 0x060001D7 RID: 471
		void WriteToMessageItem(IMigrationStoreObject message, bool loaded);

		// Token: 0x060001D8 RID: 472
		XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument);
	}
}
