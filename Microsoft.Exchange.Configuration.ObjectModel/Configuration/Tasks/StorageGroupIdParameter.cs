using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class StorageGroupIdParameter : ServerBasedIdParameter
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x00025173 File Offset: 0x00023373
		public StorageGroupIdParameter()
		{
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002517B File Offset: 0x0002337B
		public StorageGroupIdParameter(StorageGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00025189 File Offset: 0x00023389
		public StorageGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00025192 File Offset: 0x00023392
		public StorageGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002519B File Offset: 0x0002339B
		protected StorageGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000251A4 File Offset: 0x000233A4
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.Mailbox;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000251A7 File Offset: 0x000233A7
		public static StorageGroupIdParameter Parse(string identity)
		{
			return new StorageGroupIdParameter(identity);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000251AF File Offset: 0x000233AF
		protected override void Initialize(string identity)
		{
			base.Initialize(identity);
			if (!string.IsNullOrEmpty(base.ServerName) && base.ServerId == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
		}
	}
}
