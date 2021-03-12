using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000052 RID: 82
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncTooSlowException : NonPromotableTransientException
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00006462 File Offset: 0x00004662
		public SyncTooSlowException(TimeSpan syncDurationThreshold) : base(Strings.SyncTooSlowException(syncDurationThreshold))
		{
			this.syncDurationThreshold = syncDurationThreshold;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006477 File Offset: 0x00004677
		public SyncTooSlowException(TimeSpan syncDurationThreshold, Exception innerException) : base(Strings.SyncTooSlowException(syncDurationThreshold), innerException)
		{
			this.syncDurationThreshold = syncDurationThreshold;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000648D File Offset: 0x0000468D
		protected SyncTooSlowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.syncDurationThreshold = (TimeSpan)info.GetValue("syncDurationThreshold", typeof(TimeSpan));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000064B7 File Offset: 0x000046B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("syncDurationThreshold", this.syncDurationThreshold);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000064D7 File Offset: 0x000046D7
		public TimeSpan SyncDurationThreshold
		{
			get
			{
				return this.syncDurationThreshold;
			}
		}

		// Token: 0x040000FB RID: 251
		private readonly TimeSpan syncDurationThreshold;
	}
}
