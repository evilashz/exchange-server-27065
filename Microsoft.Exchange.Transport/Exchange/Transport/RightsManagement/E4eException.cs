using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003DC RID: 988
	[Serializable]
	public class E4eException : LocalizedException
	{
		// Token: 0x06002D44 RID: 11588 RVA: 0x000B4ED8 File Offset: 0x000B30D8
		public E4eException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000B4EE6 File Offset: 0x000B30E6
		public E4eException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000B4EEF File Offset: 0x000B30EF
		public E4eException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000B4EFE File Offset: 0x000B30FE
		public E4eException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000B4F08 File Offset: 0x000B3108
		protected E4eException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
