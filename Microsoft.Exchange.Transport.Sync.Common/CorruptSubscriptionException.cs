using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CorruptSubscriptionException : LocalizedException
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00006A50 File Offset: 0x00004C50
		public CorruptSubscriptionException(Guid guid) : base(Strings.CorruptSubscriptionException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00006A65 File Offset: 0x00004C65
		public CorruptSubscriptionException(Guid guid, Exception innerException) : base(Strings.CorruptSubscriptionException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006A7B File Offset: 0x00004C7B
		protected CorruptSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006AA5 File Offset: 0x00004CA5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006AC5 File Offset: 0x00004CC5
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000109 RID: 265
		private readonly Guid guid;
	}
}
