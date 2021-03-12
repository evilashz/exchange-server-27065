using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F40 RID: 3904
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxSearchScopeException : LocalizedException
	{
		// Token: 0x0600AB3A RID: 43834 RVA: 0x0028EC18 File Offset: 0x0028CE18
		public MailboxSearchScopeException(string identity) : base(Strings.MailboxSearchScopeException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600AB3B RID: 43835 RVA: 0x0028EC2D File Offset: 0x0028CE2D
		public MailboxSearchScopeException(string identity, Exception innerException) : base(Strings.MailboxSearchScopeException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600AB3C RID: 43836 RVA: 0x0028EC43 File Offset: 0x0028CE43
		protected MailboxSearchScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600AB3D RID: 43837 RVA: 0x0028EC6D File Offset: 0x0028CE6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17003757 RID: 14167
		// (get) Token: 0x0600AB3E RID: 43838 RVA: 0x0028EC88 File Offset: 0x0028CE88
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x040060BD RID: 24765
		private readonly string identity;
	}
}
