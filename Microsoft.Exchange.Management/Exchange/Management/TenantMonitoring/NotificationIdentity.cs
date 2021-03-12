using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CF7 RID: 3319
	[Serializable]
	public sealed class NotificationIdentity : ObjectId, IEquatable<NotificationIdentity>
	{
		// Token: 0x06007F9F RID: 32671 RVA: 0x00209A59 File Offset: 0x00207C59
		public NotificationIdentity(byte[] id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = new Guid(id);
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x00209A7B File Offset: 0x00207C7B
		public NotificationIdentity() : this(Guid.NewGuid())
		{
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x00209A88 File Offset: 0x00207C88
		private NotificationIdentity(Guid id)
		{
			this.id = id;
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x00209A97 File Offset: 0x00207C97
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NotificationIdentity);
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x00209AA8 File Offset: 0x00207CA8
		public bool Equals(NotificationIdentity other)
		{
			return other != null && this.id.Equals(other.id);
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x00209AD0 File Offset: 0x00207CD0
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x00209AF4 File Offset: 0x00207CF4
		public override byte[] GetBytes()
		{
			return this.id.ToByteArray();
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x00209B10 File Offset: 0x00207D10
		public override string ToString()
		{
			return this.id.ToString("D");
		}

		// Token: 0x1700279E RID: 10142
		// (get) Token: 0x06007FA7 RID: 32679 RVA: 0x00209B30 File Offset: 0x00207D30
		// (set) Token: 0x06007FA8 RID: 32680 RVA: 0x00209B38 File Offset: 0x00207D38
		internal string EventSource
		{
			get
			{
				return this.eventSource;
			}
			set
			{
				this.eventSource = value;
			}
		}

		// Token: 0x04003E9E RID: 16030
		private readonly Guid id;

		// Token: 0x04003E9F RID: 16031
		[NonSerialized]
		private string eventSource;
	}
}
