using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200114E RID: 4430
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIntegrityCheckJobIdentity : LocalizedException
	{
		// Token: 0x0600B570 RID: 46448 RVA: 0x0029E442 File Offset: 0x0029C642
		public InvalidIntegrityCheckJobIdentity(string identity) : base(Strings.InvalidIntegrityCheckJobIdentity(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600B571 RID: 46449 RVA: 0x0029E457 File Offset: 0x0029C657
		public InvalidIntegrityCheckJobIdentity(string identity, Exception innerException) : base(Strings.InvalidIntegrityCheckJobIdentity(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600B572 RID: 46450 RVA: 0x0029E46D File Offset: 0x0029C66D
		protected InvalidIntegrityCheckJobIdentity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600B573 RID: 46451 RVA: 0x0029E497 File Offset: 0x0029C697
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17003955 RID: 14677
		// (get) Token: 0x0600B574 RID: 46452 RVA: 0x0029E4B2 File Offset: 0x0029C6B2
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x040062BB RID: 25275
		private readonly string identity;
	}
}
