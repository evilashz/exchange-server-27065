using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x0200040C RID: 1036
	[Serializable]
	public class WebObjectNotFoundException : LocalizedException
	{
		// Token: 0x06002449 RID: 9289 RVA: 0x00090BC7 File Offset: 0x0008EDC7
		public WebObjectNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00090BD0 File Offset: 0x0008EDD0
		public WebObjectNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00090BDA File Offset: 0x0008EDDA
		protected WebObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
