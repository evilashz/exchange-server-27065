using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB3 RID: 3763
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedAuthMethodPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A855 RID: 43093 RVA: 0x00289D39 File Offset: 0x00287F39
		public UnsupportedAuthMethodPermanentException(string authMethod) : base(Strings.ErrorUnsupportedAuthMethodForMerges(authMethod))
		{
			this.authMethod = authMethod;
		}

		// Token: 0x0600A856 RID: 43094 RVA: 0x00289D4E File Offset: 0x00287F4E
		public UnsupportedAuthMethodPermanentException(string authMethod, Exception innerException) : base(Strings.ErrorUnsupportedAuthMethodForMerges(authMethod), innerException)
		{
			this.authMethod = authMethod;
		}

		// Token: 0x0600A857 RID: 43095 RVA: 0x00289D64 File Offset: 0x00287F64
		protected UnsupportedAuthMethodPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.authMethod = (string)info.GetValue("authMethod", typeof(string));
		}

		// Token: 0x0600A858 RID: 43096 RVA: 0x00289D8E File Offset: 0x00287F8E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("authMethod", this.authMethod);
		}

		// Token: 0x170036A6 RID: 13990
		// (get) Token: 0x0600A859 RID: 43097 RVA: 0x00289DA9 File Offset: 0x00287FA9
		public string AuthMethod
		{
			get
			{
				return this.authMethod;
			}
		}

		// Token: 0x0400600C RID: 24588
		private readonly string authMethod;
	}
}
