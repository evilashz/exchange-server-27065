using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	public class RemotePermanentException : MailboxReplicationPermanentException, IMRSRemoteException
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x000215D4 File Offset: 0x0001F7D4
		public RemotePermanentException(LocalizedString msg, Exception innerException) : base(msg, innerException)
		{
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000215DE File Offset: 0x0001F7DE
		protected RemotePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			RemotePermanentException.Deserialize(info, context, out this.originalFailureType, out this.mapiLowLevelError, out this.wkeClasses, out this.remoteStackTrace);
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00021607 File Offset: 0x0001F807
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x0002160F File Offset: 0x0001F80F
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

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00021618 File Offset: 0x0001F818
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00021620 File Offset: 0x0001F820
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00021629 File Offset: 0x0001F829
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x00021631 File Offset: 0x0001F831
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0002163A File Offset: 0x0001F83A
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00021642 File Offset: 0x0001F842
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

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002164B File Offset: 0x0001F84B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			RemotePermanentException.Serialize(info, context, this.originalFailureType, this.mapiLowLevelError, this.wkeClasses, this.remoteStackTrace);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00021674 File Offset: 0x0001F874
		internal static void Serialize(SerializationInfo info, StreamingContext context, string originalFailureType, int mapiLowLevelError, WellKnownException[] wkeClasses, string remoteStackTrace)
		{
			info.AddValue("originalFailureType", originalFailureType);
			info.AddValue("mapiLowLevelError", mapiLowLevelError);
			info.AddValue("wkeCount", wkeClasses.Length);
			for (int i = 0; i < wkeClasses.Length; i++)
			{
				info.AddValue(string.Format("wke{0}", i), (int)wkeClasses[i]);
			}
			info.AddValue("remoteStackTrace", remoteStackTrace);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000216E0 File Offset: 0x0001F8E0
		internal static void Deserialize(SerializationInfo info, StreamingContext context, out string originalFailureType, out int mapiLowLevelError, out WellKnownException[] wkeClasses, out string remoteStackTrace)
		{
			originalFailureType = info.GetString("originalFailureType");
			mapiLowLevelError = info.GetInt32("mapiLowLevelError");
			int @int = info.GetInt32("wkeCount");
			wkeClasses = new WellKnownException[@int];
			for (int i = 0; i < @int; i++)
			{
				wkeClasses[i] = (WellKnownException)info.GetInt32(string.Format("wke{0}", i));
			}
			try
			{
				remoteStackTrace = info.GetString("remoteStackTrace");
			}
			catch (SerializationException)
			{
				remoteStackTrace = null;
			}
		}

		// Token: 0x0400083A RID: 2106
		private string originalFailureType;

		// Token: 0x0400083B RID: 2107
		private WellKnownException[] wkeClasses;

		// Token: 0x0400083C RID: 2108
		private int mapiLowLevelError;

		// Token: 0x0400083D RID: 2109
		private string remoteStackTrace;
	}
}
