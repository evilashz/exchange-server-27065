using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000ACF RID: 2767
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTimeZoneNameException : LocalizedException
	{
		// Token: 0x060080D0 RID: 32976 RVA: 0x001A5ED9 File Offset: 0x001A40D9
		public InvalidTimeZoneNameException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x001A5EE2 File Offset: 0x001A40E2
		public InvalidTimeZoneNameException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080D2 RID: 32978 RVA: 0x001A5EEC File Offset: 0x001A40EC
		protected InvalidTimeZoneNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x001A5EF6 File Offset: 0x001A40F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
