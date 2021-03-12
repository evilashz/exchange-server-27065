using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000063 RID: 99
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverDataException : MobilePermanentException
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public MobileDriverDataException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		public MobileDriverDataException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000CCBF File Offset: 0x0000AEBF
		protected MobileDriverDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000CCC9 File Offset: 0x0000AEC9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
