using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001114 RID: 4372
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToDiscoverMailboxPermissionException : LocalizedException
	{
		// Token: 0x0600B454 RID: 46164 RVA: 0x0029CA22 File Offset: 0x0029AC22
		public UnableToDiscoverMailboxPermissionException() : base(Strings.UnableToDiscoverMailboxPermission)
		{
		}

		// Token: 0x0600B455 RID: 46165 RVA: 0x0029CA2F File Offset: 0x0029AC2F
		public UnableToDiscoverMailboxPermissionException(Exception innerException) : base(Strings.UnableToDiscoverMailboxPermission, innerException)
		{
		}

		// Token: 0x0600B456 RID: 46166 RVA: 0x0029CA3D File Offset: 0x0029AC3D
		protected UnableToDiscoverMailboxPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B457 RID: 46167 RVA: 0x0029CA47 File Offset: 0x0029AC47
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
