using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000365 RID: 869
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnknownSecurityPropException : MailboxReplicationPermanentException
	{
		// Token: 0x060026B6 RID: 9910 RVA: 0x000538DA File Offset: 0x00051ADA
		public UnknownSecurityPropException(int securityProp) : base(MrsStrings.UnknownSecurityProp(securityProp))
		{
			this.securityProp = securityProp;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000538EF File Offset: 0x00051AEF
		public UnknownSecurityPropException(int securityProp, Exception innerException) : base(MrsStrings.UnknownSecurityProp(securityProp), innerException)
		{
			this.securityProp = securityProp;
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x00053905 File Offset: 0x00051B05
		protected UnknownSecurityPropException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.securityProp = (int)info.GetValue("securityProp", typeof(int));
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0005392F File Offset: 0x00051B2F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("securityProp", this.securityProp);
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0005394A File Offset: 0x00051B4A
		public int SecurityProp
		{
			get
			{
				return this.securityProp;
			}
		}

		// Token: 0x0400106B RID: 4203
		private readonly int securityProp;
	}
}
