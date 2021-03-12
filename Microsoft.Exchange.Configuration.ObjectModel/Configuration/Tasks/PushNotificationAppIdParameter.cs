using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	public class PushNotificationAppIdParameter : ADIdParameter
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x00024000 File Offset: 0x00022200
		public PushNotificationAppIdParameter()
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00024008 File Offset: 0x00022208
		public PushNotificationAppIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00024011 File Offset: 0x00022211
		public PushNotificationAppIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002401A File Offset: 0x0002221A
		public PushNotificationAppIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00024023 File Offset: 0x00022223
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00024026 File Offset: 0x00022226
		public static PushNotificationAppIdParameter Parse(string identity)
		{
			return new PushNotificationAppIdParameter(identity);
		}
	}
}
