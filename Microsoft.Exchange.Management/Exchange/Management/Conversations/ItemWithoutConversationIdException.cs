using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Conversations
{
	// Token: 0x02000F3B RID: 3899
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ItemWithoutConversationIdException : LocalizedException
	{
		// Token: 0x0600AB21 RID: 43809 RVA: 0x0028E9BC File Offset: 0x0028CBBC
		public ItemWithoutConversationIdException(string user, string itemid) : base(Strings.ConversationsItemWithoutConversationId(user, itemid))
		{
			this.user = user;
			this.itemid = itemid;
		}

		// Token: 0x0600AB22 RID: 43810 RVA: 0x0028E9D9 File Offset: 0x0028CBD9
		public ItemWithoutConversationIdException(string user, string itemid, Exception innerException) : base(Strings.ConversationsItemWithoutConversationId(user, itemid), innerException)
		{
			this.user = user;
			this.itemid = itemid;
		}

		// Token: 0x0600AB23 RID: 43811 RVA: 0x0028E9F8 File Offset: 0x0028CBF8
		protected ItemWithoutConversationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.itemid = (string)info.GetValue("itemid", typeof(string));
		}

		// Token: 0x0600AB24 RID: 43812 RVA: 0x0028EA4D File Offset: 0x0028CC4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("itemid", this.itemid);
		}

		// Token: 0x17003752 RID: 14162
		// (get) Token: 0x0600AB25 RID: 43813 RVA: 0x0028EA79 File Offset: 0x0028CC79
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17003753 RID: 14163
		// (get) Token: 0x0600AB26 RID: 43814 RVA: 0x0028EA81 File Offset: 0x0028CC81
		public string Itemid
		{
			get
			{
				return this.itemid;
			}
		}

		// Token: 0x040060B8 RID: 24760
		private readonly string user;

		// Token: 0x040060B9 RID: 24761
		private readonly string itemid;
	}
}
