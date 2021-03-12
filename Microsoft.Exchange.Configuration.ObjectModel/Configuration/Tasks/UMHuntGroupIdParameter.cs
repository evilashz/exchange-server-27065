using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200015E RID: 350
	[Serializable]
	public class UMHuntGroupIdParameter : ADIdParameter
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x00027A01 File Offset: 0x00025C01
		public UMHuntGroupIdParameter()
		{
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00027A09 File Offset: 0x00025C09
		public UMHuntGroupIdParameter(string identity) : base(identity)
		{
			this.Initialize(identity);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00027A19 File Offset: 0x00025C19
		public UMHuntGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00027A22 File Offset: 0x00025C22
		public UMHuntGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00027A2B File Offset: 0x00025C2B
		public static UMHuntGroupIdParameter Parse(string identity)
		{
			return new UMHuntGroupIdParameter(identity);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00027A34 File Offset: 0x00025C34
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (base.InternalADObjectId != null)
			{
				return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			List<T> list = new List<T>(5);
			notFoundReason = null;
			if (this.gatewayIdentity != null)
			{
				UMIPGatewayIdParameter umipgatewayIdParameter = new UMIPGatewayIdParameter(this.gatewayIdentity);
				IEnumerable<UMIPGateway> objects = umipgatewayIdParameter.GetObjects<UMIPGateway>(rootId, session, subTreeSession, optionalData, out notFoundReason);
				if (objects != null)
				{
					foreach (UMIPGateway umipgateway in objects)
					{
						IEnumerable<T> objectsInOrganization = base.GetObjectsInOrganization<T>(this.huntGroupId, umipgateway.Id, subTreeSession, optionalData);
						if (objectsInOrganization != null)
						{
							foreach (T item in objectsInOrganization)
							{
								list.Add(item);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00027B24 File Offset: 0x00025D24
		private void Initialize(string identity)
		{
			if (base.InternalADObjectId != null)
			{
				if (!(base.InternalADObjectId.Rdn != null))
				{
					Guid objectGuid = base.InternalADObjectId.ObjectGuid;
				}
				return;
			}
			int num = identity.LastIndexOf('\\');
			if (num == -1 || num == 0 || num == identity.Length - 1)
			{
				throw new ArgumentException(Strings.ErrorInvalidUMHuntGroupIdentity(identity));
			}
			this.gatewayIdentity = identity.Substring(0, num);
			this.huntGroupId = identity.Substring(num + 1, identity.Length - num - 1);
		}

		// Token: 0x040002E2 RID: 738
		private string gatewayIdentity;

		// Token: 0x040002E3 RID: 739
		private string huntGroupId;
	}
}
