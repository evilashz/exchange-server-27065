using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B2 RID: 4530
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMMailboxPolicyNotPresentException : UMMailboxPolicyNotFoundException
	{
		// Token: 0x0600B864 RID: 47204 RVA: 0x002A482C File Offset: 0x002A2A2C
		public UMMailboxPolicyNotPresentException(string user) : base(Strings.UMMailboxPolicyNotPresent(user))
		{
			this.user = user;
		}

		// Token: 0x0600B865 RID: 47205 RVA: 0x002A4841 File Offset: 0x002A2A41
		public UMMailboxPolicyNotPresentException(string user, Exception innerException) : base(Strings.UMMailboxPolicyNotPresent(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x0600B866 RID: 47206 RVA: 0x002A4857 File Offset: 0x002A2A57
		protected UMMailboxPolicyNotPresentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x0600B867 RID: 47207 RVA: 0x002A4881 File Offset: 0x002A2A81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17003A19 RID: 14873
		// (get) Token: 0x0600B868 RID: 47208 RVA: 0x002A489C File Offset: 0x002A2A9C
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04006434 RID: 25652
		private readonly string user;
	}
}
