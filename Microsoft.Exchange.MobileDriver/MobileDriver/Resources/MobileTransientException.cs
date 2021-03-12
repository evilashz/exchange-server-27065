using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileTransientException : TransientException
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000CAB1 File Offset: 0x0000ACB1
		public MobileTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000CABA File Offset: 0x0000ACBA
		public MobileTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		protected MobileTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000CACE File Offset: 0x0000ACCE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
