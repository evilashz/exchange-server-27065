using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000079 RID: 121
	public class MapiSessionPerUserAndClientTypeCounter : IMapiObjectCounter
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		public MapiSessionPerUserAndClientTypeCounter(string userDN, SecurityIdentifier userSid, ClientType clientType)
		{
			this.userDN = userDN;
			this.userSid = userSid;
			this.clientType = clientType;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001CAFD File Offset: 0x0001ACFD
		public long GetCount()
		{
			return MapiSessionPerUserCounter.GetCount(this.userSid, this.clientType);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001CB10 File Offset: 0x0001AD10
		public void IncrementCount()
		{
			MapiSessionPerUserCounter.IncrementCount(this.userSid, this.clientType);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001CB23 File Offset: 0x0001AD23
		public void DecrementCount()
		{
			MapiSessionPerUserCounter.DecrementCount(this.userSid, this.clientType);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001CB38 File Offset: 0x0001AD38
		public void CheckObjectQuota(bool mustBeStrictlyUnderQuota)
		{
			long num = ActiveObjectLimits.EffectiveLimitation(this.clientType);
			if (num == -1L)
			{
				return;
			}
			bool flag;
			if (MapiSessionPerUserCounter.IsClientOverQuota(this.userSid, this.clientType, num, mustBeStrictlyUnderQuota, out flag))
			{
				if (flag)
				{
					this.LogEvent(num);
				}
				throw new StoreException((LID)57384U, ErrorCodeValue.MaxObjectsExceeded, "Exceeded session size limitation, user sid=" + this.userSid.Value + ", client type=" + this.clientType.ToString());
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		private void LogEvent(long effectiveLimitation)
		{
			string periodicKey = this.userDN + this.clientType + MapiObjectTrackedType.Session;
			Microsoft.Exchange.Server.Storage.Common.Globals.LogPeriodicEvent(periodicKey, MSExchangeISEventLogConstants.Tuple_MaxObjectsExceeded, new object[]
			{
				this.userDN,
				this.clientType,
				effectiveLimitation,
				MapiObjectTrackedType.Session
			});
		}

		// Token: 0x04000261 RID: 609
		private readonly string userDN;

		// Token: 0x04000262 RID: 610
		private readonly SecurityIdentifier userSid;

		// Token: 0x04000263 RID: 611
		private readonly ClientType clientType;
	}
}
