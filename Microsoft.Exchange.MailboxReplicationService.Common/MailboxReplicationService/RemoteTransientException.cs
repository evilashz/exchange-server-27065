using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200018A RID: 394
	[Serializable]
	public class RemoteTransientException : MailboxReplicationTransientException, IMRSRemoteException
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00021793 File Offset: 0x0001F993
		public RemoteTransientException(LocalizedString msg, Exception innerException) : base(msg, innerException)
		{
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002179D File Offset: 0x0001F99D
		protected RemoteTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			RemotePermanentException.Deserialize(info, context, out this.originalFailureType, out this.mapiLowLevelError, out this.wkeClasses, out this.remoteStackTrace);
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000217C6 File Offset: 0x0001F9C6
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x000217CE File Offset: 0x0001F9CE
		string IMRSRemoteException.OriginalFailureType
		{
			get
			{
				return this.originalFailureType;
			}
			set
			{
				this.originalFailureType = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x000217D7 File Offset: 0x0001F9D7
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x000217DF File Offset: 0x0001F9DF
		WellKnownException[] IMRSRemoteException.WKEClasses
		{
			get
			{
				return this.wkeClasses;
			}
			set
			{
				this.wkeClasses = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000217E8 File Offset: 0x0001F9E8
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x000217F0 File Offset: 0x0001F9F0
		int IMRSRemoteException.MapiLowLevelError
		{
			get
			{
				return this.mapiLowLevelError;
			}
			set
			{
				this.mapiLowLevelError = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x000217F9 File Offset: 0x0001F9F9
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00021801 File Offset: 0x0001FA01
		string IMRSRemoteException.RemoteStackTrace
		{
			get
			{
				return this.remoteStackTrace;
			}
			set
			{
				this.remoteStackTrace = value;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0002180A File Offset: 0x0001FA0A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			RemotePermanentException.Serialize(info, context, this.originalFailureType, this.mapiLowLevelError, this.wkeClasses, this.remoteStackTrace);
		}

		// Token: 0x0400083E RID: 2110
		private string originalFailureType;

		// Token: 0x0400083F RID: 2111
		private WellKnownException[] wkeClasses;

		// Token: 0x04000840 RID: 2112
		private int mapiLowLevelError;

		// Token: 0x04000841 RID: 2113
		private string remoteStackTrace;
	}
}
