using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200017E RID: 382
	[Serializable]
	internal sealed class MessageTraceObjectId : ObjectId
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x0002FBAF File Offset: 0x0002DDAF
		public MessageTraceObjectId(Guid organizationalUnitRoot, Guid exMessageId)
		{
			this.organizationalUnitRoot = organizationalUnitRoot;
			this.exMessageId = exMessageId;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0002FBC5 File Offset: 0x0002DDC5
		public Guid ExMessageId
		{
			get
			{
				return this.exMessageId;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0002FBCD File Offset: 0x0002DDCD
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return this.organizationalUnitRoot;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002FBD5 File Offset: 0x0002DDD5
		public override string ToString()
		{
			return string.Format("Tenant={0},Message={1}", this.OrganizationalUnitRoot, this.ExMessageId);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002FBF7 File Offset: 0x0002DDF7
		public override byte[] GetBytes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000714 RID: 1812
		private readonly Guid organizationalUnitRoot;

		// Token: 0x04000715 RID: 1813
		private readonly Guid exMessageId;
	}
}
