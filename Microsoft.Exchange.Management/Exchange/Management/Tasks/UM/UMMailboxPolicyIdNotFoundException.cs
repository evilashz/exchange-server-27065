using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B1 RID: 4529
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMMailboxPolicyIdNotFoundException : UMMailboxPolicyNotFoundException
	{
		// Token: 0x0600B85F RID: 47199 RVA: 0x002A47B4 File Offset: 0x002A29B4
		public UMMailboxPolicyIdNotFoundException(string id) : base(Strings.UMMailboxPolicyIdNotFound(id))
		{
			this.id = id;
		}

		// Token: 0x0600B860 RID: 47200 RVA: 0x002A47C9 File Offset: 0x002A29C9
		public UMMailboxPolicyIdNotFoundException(string id, Exception innerException) : base(Strings.UMMailboxPolicyIdNotFound(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x0600B861 RID: 47201 RVA: 0x002A47DF File Offset: 0x002A29DF
		protected UMMailboxPolicyIdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600B862 RID: 47202 RVA: 0x002A4809 File Offset: 0x002A2A09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17003A18 RID: 14872
		// (get) Token: 0x0600B863 RID: 47203 RVA: 0x002A4824 File Offset: 0x002A2A24
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04006433 RID: 25651
		private readonly string id;
	}
}
