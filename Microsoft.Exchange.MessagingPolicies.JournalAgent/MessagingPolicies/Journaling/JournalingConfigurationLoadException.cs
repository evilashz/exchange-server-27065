using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000021 RID: 33
	internal class JournalingConfigurationLoadException : LocalizedException
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000B49E File Offset: 0x0000969E
		internal JournalingConfigurationLoadException(string errorString) : base(new LocalizedString(errorString))
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000B4AC File Offset: 0x000096AC
		internal JournalingConfigurationLoadException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
