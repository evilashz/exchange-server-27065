using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200063C RID: 1596
	[Serializable]
	public class GenericSidIdentity : ClientSecurityContextIdentity
	{
		// Token: 0x06001CE7 RID: 7399 RVA: 0x000340FA File Offset: 0x000322FA
		public GenericSidIdentity(string name, string type, SecurityIdentifier sid) : this(name, type, sid, null)
		{
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00034106 File Offset: 0x00032306
		public GenericSidIdentity(string name, string type, SecurityIdentifier sid, string partitionId) : base(name, type)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.sid = sid;
			this.partitionId = partitionId;
			this.serializedSidValue = sid.Value;
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0003413F File Offset: 0x0003233F
		public override SecurityIdentifier Sid
		{
			get
			{
				if (this.sid == null)
				{
					this.sid = new SecurityIdentifier(this.serializedSidValue);
				}
				return this.sid;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00034166 File Offset: 0x00032366
		public string PartitionId
		{
			get
			{
				return this.partitionId;
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00034170 File Offset: 0x00032370
		internal override ClientSecurityContext CreateClientSecurityContext()
		{
			return new ClientSecurityContext(new SecurityAccessToken
			{
				UserSid = this.Sid.Value
			});
		}

		// Token: 0x04001D2E RID: 7470
		private string serializedSidValue;

		// Token: 0x04001D2F RID: 7471
		[NonSerialized]
		private SecurityIdentifier sid;

		// Token: 0x04001D30 RID: 7472
		[NonSerialized]
		private readonly string partitionId;
	}
}
