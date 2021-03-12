using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB0 RID: 2736
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCountryOrRegionException : LocalizedException
	{
		// Token: 0x06008034 RID: 32820 RVA: 0x001A4F91 File Offset: 0x001A3191
		public InvalidCountryOrRegionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x001A4F9A File Offset: 0x001A319A
		public InvalidCountryOrRegionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x001A4FA4 File Offset: 0x001A31A4
		protected InvalidCountryOrRegionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x001A4FAE File Offset: 0x001A31AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
