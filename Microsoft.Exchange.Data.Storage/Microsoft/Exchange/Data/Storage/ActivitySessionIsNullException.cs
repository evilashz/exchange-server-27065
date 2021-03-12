using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000130 RID: 304
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ActivitySessionIsNullException : StoragePermanentException
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x0006B066 File Offset: 0x00069266
		public ActivitySessionIsNullException() : base(ServerStrings.ActivitySessionIsNull)
		{
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0006B073 File Offset: 0x00069273
		public ActivitySessionIsNullException(Exception innerException) : base(ServerStrings.ActivitySessionIsNull, innerException)
		{
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0006B081 File Offset: 0x00069281
		protected ActivitySessionIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0006B08B File Offset: 0x0006928B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
