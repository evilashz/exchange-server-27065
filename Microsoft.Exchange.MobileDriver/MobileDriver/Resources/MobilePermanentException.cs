using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000055 RID: 85
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobilePermanentException : LocalizedException
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000CA8A File Offset: 0x0000AC8A
		public MobilePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000CA93 File Offset: 0x0000AC93
		public MobilePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		protected MobilePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
