using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000018 RID: 24
	internal class AsyncQueueOwnerInfo : ConfigurablePropertyBag
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003CDC File Offset: 0x00001EDC
		public AsyncQueueOwnerInfo(string ownerId) : this(ownerId, AsyncQueueFlags.None)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003CE6 File Offset: 0x00001EE6
		public AsyncQueueOwnerInfo(string ownerId, AsyncQueueFlags flags)
		{
			this.OwnerId = ownerId;
			this.Flags = flags;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003CFC File Offset: 0x00001EFC
		public AsyncQueueOwnerInfo()
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003D04 File Offset: 0x00001F04
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ObjectId.ToString());
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003D2A File Offset: 0x00001F2A
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003D53 File Offset: 0x00001F53
		public Guid ObjectId
		{
			get
			{
				this.identity = new Guid(DalHelper.GetMDHash(this.OwnerId) ?? new byte[16]);
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003D5C File Offset: 0x00001F5C
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003D6E File Offset: 0x00001F6E
		public AsyncQueueFlags Flags
		{
			get
			{
				return (AsyncQueueFlags)this[AsyncQueueOwnerInfoSchema.FlagsProperty];
			}
			set
			{
				this[AsyncQueueOwnerInfoSchema.FlagsProperty] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003D81 File Offset: 0x00001F81
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003D93 File Offset: 0x00001F93
		public string OwnerId
		{
			get
			{
				return (string)this[AsyncQueueOwnerInfoSchema.OwnerIdProperty];
			}
			set
			{
				this[AsyncQueueOwnerInfoSchema.OwnerIdProperty] = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DA1 File Offset: 0x00001FA1
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueOwnerInfoSchema);
		}

		// Token: 0x0400005B RID: 91
		private Guid identity;
	}
}
