using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200003E RID: 62
	internal sealed class ADNotificationEventArgs : EventArgs
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00012D8A File Offset: 0x00010F8A
		public ADNotificationChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00012D92 File Offset: 0x00010F92
		public object Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00012D9A File Offset: 0x00010F9A
		public ADObjectId Id
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00012DA2 File Offset: 0x00010FA2
		public ADObjectId LastKnownParent
		{
			get
			{
				return this.lastKnownParent;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00012DAA File Offset: 0x00010FAA
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012DB2 File Offset: 0x00010FB2
		internal ADNotificationEventArgs(ADNotificationChangeType changeType, object context, ADObjectId id, ADObjectId lastKnownParent, Type type)
		{
			this.changeType = changeType;
			this.context = context;
			this.objectId = id;
			this.lastKnownParent = lastKnownParent;
			this.type = type;
		}

		// Token: 0x040000FB RID: 251
		private ADNotificationChangeType changeType;

		// Token: 0x040000FC RID: 252
		private object context;

		// Token: 0x040000FD RID: 253
		private ADObjectId objectId;

		// Token: 0x040000FE RID: 254
		private ADObjectId lastKnownParent;

		// Token: 0x040000FF RID: 255
		private Type type;
	}
}
