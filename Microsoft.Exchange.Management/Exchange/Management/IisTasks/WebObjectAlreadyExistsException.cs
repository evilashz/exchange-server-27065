using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x0200040D RID: 1037
	[Serializable]
	public class WebObjectAlreadyExistsException : LocalizedException
	{
		// Token: 0x0600244C RID: 9292 RVA: 0x00090BE4 File Offset: 0x0008EDE4
		public WebObjectAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00090BED File Offset: 0x0008EDED
		public WebObjectAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00090BF7 File Offset: 0x0008EDF7
		protected WebObjectAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
