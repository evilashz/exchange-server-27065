using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DEB RID: 3563
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDeleteAssociatedMailboxPolicyException : LocalizedException
	{
		// Token: 0x0600A48B RID: 42123 RVA: 0x00284726 File Offset: 0x00282926
		public CannotDeleteAssociatedMailboxPolicyException(string dn) : base(Strings.CannotDeleteAssociatedMailboxPolicyException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600A48C RID: 42124 RVA: 0x0028473B File Offset: 0x0028293B
		public CannotDeleteAssociatedMailboxPolicyException(string dn, Exception innerException) : base(Strings.CannotDeleteAssociatedMailboxPolicyException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600A48D RID: 42125 RVA: 0x00284751 File Offset: 0x00282951
		protected CannotDeleteAssociatedMailboxPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600A48E RID: 42126 RVA: 0x0028477B File Offset: 0x0028297B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x170035FC RID: 13820
		// (get) Token: 0x0600A48F RID: 42127 RVA: 0x00284796 File Offset: 0x00282996
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x04005F62 RID: 24418
		private readonly string dn;
	}
}
