using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED9 RID: 3801
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LargeItemLimitAlreadyExceededPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A917 RID: 43287 RVA: 0x0028B0AA File Offset: 0x002892AA
		public LargeItemLimitAlreadyExceededPermanentException(string name, int encountered, string newlimit) : base(Strings.ErrorLargeItemLimitAlreadyExceeded(name, encountered, newlimit))
		{
			this.name = name;
			this.encountered = encountered;
			this.newlimit = newlimit;
		}

		// Token: 0x0600A918 RID: 43288 RVA: 0x0028B0CF File Offset: 0x002892CF
		public LargeItemLimitAlreadyExceededPermanentException(string name, int encountered, string newlimit, Exception innerException) : base(Strings.ErrorLargeItemLimitAlreadyExceeded(name, encountered, newlimit), innerException)
		{
			this.name = name;
			this.encountered = encountered;
			this.newlimit = newlimit;
		}

		// Token: 0x0600A919 RID: 43289 RVA: 0x0028B0F8 File Offset: 0x002892F8
		protected LargeItemLimitAlreadyExceededPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.encountered = (int)info.GetValue("encountered", typeof(int));
			this.newlimit = (string)info.GetValue("newlimit", typeof(string));
		}

		// Token: 0x0600A91A RID: 43290 RVA: 0x0028B16D File Offset: 0x0028936D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("encountered", this.encountered);
			info.AddValue("newlimit", this.newlimit);
		}

		// Token: 0x170036D0 RID: 14032
		// (get) Token: 0x0600A91B RID: 43291 RVA: 0x0028B1AA File Offset: 0x002893AA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170036D1 RID: 14033
		// (get) Token: 0x0600A91C RID: 43292 RVA: 0x0028B1B2 File Offset: 0x002893B2
		public int Encountered
		{
			get
			{
				return this.encountered;
			}
		}

		// Token: 0x170036D2 RID: 14034
		// (get) Token: 0x0600A91D RID: 43293 RVA: 0x0028B1BA File Offset: 0x002893BA
		public string Newlimit
		{
			get
			{
				return this.newlimit;
			}
		}

		// Token: 0x04006036 RID: 24630
		private readonly string name;

		// Token: 0x04006037 RID: 24631
		private readonly int encountered;

		// Token: 0x04006038 RID: 24632
		private readonly string newlimit;
	}
}
