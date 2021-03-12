using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WBXmlBase
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000082AB File Offset: 0x000064AB
		internal static WBXmlSchema Schema
		{
			get
			{
				return WBXmlBase.WBXmlSchema;
			}
		}

		// Token: 0x04000404 RID: 1028
		protected const byte AttributeBit = 128;

		// Token: 0x04000405 RID: 1029
		protected const int AttributeBitMask = 255;

		// Token: 0x04000406 RID: 1030
		protected const byte ContentBit = 64;

		// Token: 0x04000407 RID: 1031
		protected const byte ContinuationBit = 128;

		// Token: 0x04000408 RID: 1032
		protected const byte MinimumTagValue = 5;

		// Token: 0x04000409 RID: 1033
		protected const int NamespaceBitMask = 65280;

		// Token: 0x0400040A RID: 1034
		protected const byte NumberBitMask = 127;

		// Token: 0x0400040B RID: 1035
		protected const byte ValidTagBitMask = 63;

		// Token: 0x0400040C RID: 1036
		private static readonly WBXmlSchema WBXmlSchema = new WBXmlSchema30();
	}
}
