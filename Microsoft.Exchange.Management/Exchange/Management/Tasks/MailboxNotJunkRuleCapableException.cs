using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010FE RID: 4350
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxNotJunkRuleCapableException : LocalizedException
	{
		// Token: 0x0600B3DE RID: 46046 RVA: 0x0029BD0D File Offset: 0x00299F0D
		public MailboxNotJunkRuleCapableException(string identity) : base(Strings.MailboxNotJunkRuleCapable(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600B3DF RID: 46047 RVA: 0x0029BD22 File Offset: 0x00299F22
		public MailboxNotJunkRuleCapableException(string identity, Exception innerException) : base(Strings.MailboxNotJunkRuleCapable(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600B3E0 RID: 46048 RVA: 0x0029BD38 File Offset: 0x00299F38
		protected MailboxNotJunkRuleCapableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600B3E1 RID: 46049 RVA: 0x0029BD62 File Offset: 0x00299F62
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17003903 RID: 14595
		// (get) Token: 0x0600B3E2 RID: 46050 RVA: 0x0029BD7D File Offset: 0x00299F7D
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006269 RID: 25193
		private readonly string identity;
	}
}
