using System;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A3 RID: 675
	internal class WbxmlBase
	{
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00091734 File Offset: 0x0008F934
		public static WbxmlSchema Schema
		{
			get
			{
				return WbxmlBase.wbxmlSchema;
			}
		}

		// Token: 0x04001179 RID: 4473
		protected const byte AttributeBit = 128;

		// Token: 0x0400117A RID: 4474
		protected const int AttributeBitMask = 255;

		// Token: 0x0400117B RID: 4475
		protected const byte ContentBit = 64;

		// Token: 0x0400117C RID: 4476
		protected const byte ContinuationBit = 128;

		// Token: 0x0400117D RID: 4477
		protected const byte MinimumTagValue = 5;

		// Token: 0x0400117E RID: 4478
		protected const int NamespaceBitMask = 65280;

		// Token: 0x0400117F RID: 4479
		protected const byte NumberBitMask = 127;

		// Token: 0x04001180 RID: 4480
		protected const byte ValidTagBitMask = 63;

		// Token: 0x04001181 RID: 4481
		private static WbxmlSchema wbxmlSchema = new WbxmlSchema30();
	}
}
