using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF2 RID: 4082
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidNameOrDescriptionForDefaultLocaleException : LocalizedException
	{
		// Token: 0x0600AE7F RID: 44671 RVA: 0x0029311E File Offset: 0x0029131E
		public InvalidNameOrDescriptionForDefaultLocaleException() : base(Strings.InvalidNameOrDescriptionForDefaultLocale)
		{
		}

		// Token: 0x0600AE80 RID: 44672 RVA: 0x0029312B File Offset: 0x0029132B
		public InvalidNameOrDescriptionForDefaultLocaleException(Exception innerException) : base(Strings.InvalidNameOrDescriptionForDefaultLocale, innerException)
		{
		}

		// Token: 0x0600AE81 RID: 44673 RVA: 0x00293139 File Offset: 0x00291339
		protected InvalidNameOrDescriptionForDefaultLocaleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE82 RID: 44674 RVA: 0x00293143 File Offset: 0x00291343
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
