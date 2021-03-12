using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED8 RID: 3800
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BadItemLimitAlreadyExceededPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A910 RID: 43280 RVA: 0x0028AF93 File Offset: 0x00289193
		public BadItemLimitAlreadyExceededPermanentException(string name, int encountered, string newlimit) : base(Strings.ErrorBadItemLimitAlreadyExceededNew(name, encountered, newlimit))
		{
			this.name = name;
			this.encountered = encountered;
			this.newlimit = newlimit;
		}

		// Token: 0x0600A911 RID: 43281 RVA: 0x0028AFB8 File Offset: 0x002891B8
		public BadItemLimitAlreadyExceededPermanentException(string name, int encountered, string newlimit, Exception innerException) : base(Strings.ErrorBadItemLimitAlreadyExceededNew(name, encountered, newlimit), innerException)
		{
			this.name = name;
			this.encountered = encountered;
			this.newlimit = newlimit;
		}

		// Token: 0x0600A912 RID: 43282 RVA: 0x0028AFE0 File Offset: 0x002891E0
		protected BadItemLimitAlreadyExceededPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.encountered = (int)info.GetValue("encountered", typeof(int));
			this.newlimit = (string)info.GetValue("newlimit", typeof(string));
		}

		// Token: 0x0600A913 RID: 43283 RVA: 0x0028B055 File Offset: 0x00289255
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("encountered", this.encountered);
			info.AddValue("newlimit", this.newlimit);
		}

		// Token: 0x170036CD RID: 14029
		// (get) Token: 0x0600A914 RID: 43284 RVA: 0x0028B092 File Offset: 0x00289292
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170036CE RID: 14030
		// (get) Token: 0x0600A915 RID: 43285 RVA: 0x0028B09A File Offset: 0x0028929A
		public int Encountered
		{
			get
			{
				return this.encountered;
			}
		}

		// Token: 0x170036CF RID: 14031
		// (get) Token: 0x0600A916 RID: 43286 RVA: 0x0028B0A2 File Offset: 0x002892A2
		public string Newlimit
		{
			get
			{
				return this.newlimit;
			}
		}

		// Token: 0x04006033 RID: 24627
		private readonly string name;

		// Token: 0x04006034 RID: 24628
		private readonly int encountered;

		// Token: 0x04006035 RID: 24629
		private readonly string newlimit;
	}
}
