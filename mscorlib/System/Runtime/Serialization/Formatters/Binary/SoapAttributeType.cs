using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200074B RID: 1867
	[Serializable]
	internal enum SoapAttributeType
	{
		// Token: 0x040024BA RID: 9402
		None,
		// Token: 0x040024BB RID: 9403
		SchemaType,
		// Token: 0x040024BC RID: 9404
		Embedded,
		// Token: 0x040024BD RID: 9405
		XmlElement = 4,
		// Token: 0x040024BE RID: 9406
		XmlAttribute = 8
	}
}
