using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001134 RID: 4404
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BackSyncUrlInvalidProtcolFormatException : LocalizedException
	{
		// Token: 0x0600B4EE RID: 46318 RVA: 0x0029D7C2 File Offset: 0x0029B9C2
		public BackSyncUrlInvalidProtcolFormatException() : base(Strings.BackSyncUrlInvalidProtcolFormat)
		{
		}

		// Token: 0x0600B4EF RID: 46319 RVA: 0x0029D7CF File Offset: 0x0029B9CF
		public BackSyncUrlInvalidProtcolFormatException(Exception innerException) : base(Strings.BackSyncUrlInvalidProtcolFormat, innerException)
		{
		}

		// Token: 0x0600B4F0 RID: 46320 RVA: 0x0029D7DD File Offset: 0x0029B9DD
		protected BackSyncUrlInvalidProtcolFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4F1 RID: 46321 RVA: 0x0029D7E7 File Offset: 0x0029B9E7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
