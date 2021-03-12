using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C1F RID: 3103
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BodySchema : Schema
	{
		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x06006E70 RID: 28272 RVA: 0x001DACE5 File Offset: 0x001D8EE5
		public new static BodySchema Instance
		{
			get
			{
				if (BodySchema.instance == null)
				{
					BodySchema.instance = new BodySchema();
				}
				return BodySchema.instance;
			}
		}

		// Token: 0x040040BC RID: 16572
		internal static readonly StorePropertyDefinition BodyContentBase = InternalSchema.BodyContentBase;

		// Token: 0x040040BD RID: 16573
		internal static readonly StorePropertyDefinition BodyContentLocation = InternalSchema.BodyContentLocation;

		// Token: 0x040040BE RID: 16574
		public static readonly StorePropertyDefinition Codepage = InternalSchema.Codepage;

		// Token: 0x040040BF RID: 16575
		public static readonly StorePropertyDefinition InternetCpid = InternalSchema.InternetCpid;

		// Token: 0x040040C0 RID: 16576
		internal static readonly StorePropertyDefinition RtfInSync = InternalSchema.RtfInSync;

		// Token: 0x040040C1 RID: 16577
		internal static readonly StorePropertyDefinition RtfBody = InternalSchema.RtfBody;

		// Token: 0x040040C2 RID: 16578
		internal static readonly StorePropertyDefinition HtmlBody = InternalSchema.HtmlBody;

		// Token: 0x040040C3 RID: 16579
		internal static readonly StorePropertyDefinition TextBody = InternalSchema.TextBody;

		// Token: 0x040040C4 RID: 16580
		private static BodySchema instance = null;
	}
}
