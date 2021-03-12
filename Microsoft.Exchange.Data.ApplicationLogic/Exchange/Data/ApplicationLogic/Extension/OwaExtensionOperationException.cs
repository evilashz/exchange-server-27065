using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class OwaExtensionOperationException : LocalizedException
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003410 File Offset: 0x00001610
		public OwaExtensionOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003419 File Offset: 0x00001619
		public OwaExtensionOperationException(Exception innerException) : base(new LocalizedString(innerException.Message), innerException)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000342D File Offset: 0x0000162D
		public OwaExtensionOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003437 File Offset: 0x00001637
		protected OwaExtensionOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
