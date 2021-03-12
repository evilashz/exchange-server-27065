using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	internal class WbxmlException : LocalizedException
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x0009174F File Offset: 0x0008F94F
		public WbxmlException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0009175D File Offset: 0x0008F95D
		public WbxmlException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0009176C File Offset: 0x0008F96C
		protected WbxmlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
