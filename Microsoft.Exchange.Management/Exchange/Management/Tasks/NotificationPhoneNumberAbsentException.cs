using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FB9 RID: 4025
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationPhoneNumberAbsentException : LocalizedException
	{
		// Token: 0x0600AD74 RID: 44404 RVA: 0x00291B8F File Offset: 0x0028FD8F
		public NotificationPhoneNumberAbsentException(string identity) : base(Strings.ErrorNotificationPhoneNumberAbsent(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600AD75 RID: 44405 RVA: 0x00291BA4 File Offset: 0x0028FDA4
		public NotificationPhoneNumberAbsentException(string identity, Exception innerException) : base(Strings.ErrorNotificationPhoneNumberAbsent(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600AD76 RID: 44406 RVA: 0x00291BBA File Offset: 0x0028FDBA
		protected NotificationPhoneNumberAbsentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600AD77 RID: 44407 RVA: 0x00291BE4 File Offset: 0x0028FDE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170037AD RID: 14253
		// (get) Token: 0x0600AD78 RID: 44408 RVA: 0x00291BFF File Offset: 0x0028FDFF
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006113 RID: 24851
		private readonly string identity;
	}
}
