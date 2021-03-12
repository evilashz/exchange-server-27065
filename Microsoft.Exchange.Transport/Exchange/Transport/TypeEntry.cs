using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000E4 RID: 228
	internal struct TypeEntry
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0002100D File Offset: 0x0001F20D
		public TypeEntry(Type type, StreamPropertyType identifier)
		{
			this.Type = type;
			this.Identifier = identifier;
		}

		// Token: 0x0400041F RID: 1055
		public readonly Type Type;

		// Token: 0x04000420 RID: 1056
		public readonly StreamPropertyType Identifier;
	}
}
