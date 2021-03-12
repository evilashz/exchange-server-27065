using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200007A RID: 122
	internal class MapiSessionPerUserCounter
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0001CC1E File Offset: 0x0001AE1E
		private MapiSessionPerUserCounter()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001CC26 File Offset: 0x0001AE26
		internal static void Initialize()
		{
			if (MapiSessionPerUserCounter.lockObject == null)
			{
				MapiSessionPerUserCounter.userSessionCounters = new Dictionary<SecurityIdentifier, MapiSessionPerUserCounter.UserSessionCounter>();
				MapiSessionPerUserCounter.lockObject = new object();
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001CC43 File Offset: 0x0001AE43
		internal static IMapiObjectCounter GetObjectCounter(string userDN, SecurityIdentifier sid, ClientType clientType)
		{
			return new MapiSessionPerUserAndClientTypeCounter(userDN, sid, clientType);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001CC50 File Offset: 0x0001AE50
		internal static IList<SecurityIdentifier> GetUsersSnapshot()
		{
			IList<SecurityIdentifier> result;
			using (LockManager.Lock(MapiSessionPerUserCounter.lockObject))
			{
				result = new List<SecurityIdentifier>(MapiSessionPerUserCounter.userSessionCounters.Keys);
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		internal static long GetCount(SecurityIdentifier userSid, ClientType clientType)
		{
			using (LockManager.Lock(MapiSessionPerUserCounter.lockObject))
			{
				MapiSessionPerUserCounter.UserSessionCounter userSessionCounter;
				if (MapiSessionPerUserCounter.userSessionCounters.TryGetValue(userSid, out userSessionCounter))
				{
					return userSessionCounter.GetCount(clientType);
				}
			}
			return 0L;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		internal static void IncrementCount(SecurityIdentifier userSid, ClientType clientType)
		{
			using (LockManager.Lock(MapiSessionPerUserCounter.lockObject))
			{
				MapiSessionPerUserCounter.UserSessionCounter userSessionCounter;
				if (MapiSessionPerUserCounter.userSessionCounters.TryGetValue(userSid, out userSessionCounter))
				{
					userSessionCounter.IncrementCount(clientType);
				}
				else
				{
					userSessionCounter = new MapiSessionPerUserCounter.UserSessionCounter();
					userSessionCounter.IncrementCount(clientType);
					MapiSessionPerUserCounter.userSessionCounters.Add(userSid, userSessionCounter);
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001CD60 File Offset: 0x0001AF60
		internal static void DecrementCount(SecurityIdentifier userSid, ClientType clientType)
		{
			using (LockManager.Lock(MapiSessionPerUserCounter.lockObject))
			{
				MapiSessionPerUserCounter.UserSessionCounter userSessionCounter;
				if (MapiSessionPerUserCounter.userSessionCounters.TryGetValue(userSid, out userSessionCounter))
				{
					userSessionCounter.DecrementCount(clientType);
					if (userSessionCounter.IsEmpty())
					{
						MapiSessionPerUserCounter.userSessionCounters.Remove(userSid);
					}
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001CDC4 File Offset: 0x0001AFC4
		internal static bool IsClientOverQuota(SecurityIdentifier userSid, ClientType clientType, long effectiveLimitation, bool mustBeStrictlyUnderQuota, out bool needLogEvent)
		{
			needLogEvent = false;
			using (LockManager.Lock(MapiSessionPerUserCounter.lockObject))
			{
				MapiSessionPerUserCounter.UserSessionCounter userSessionCounter;
				if (MapiSessionPerUserCounter.userSessionCounters.TryGetValue(userSid, out userSessionCounter))
				{
					return userSessionCounter.IsClientOverQuota(clientType, effectiveLimitation, mustBeStrictlyUnderQuota, out needLogEvent);
				}
			}
			return false;
		}

		// Token: 0x04000264 RID: 612
		private static Dictionary<SecurityIdentifier, MapiSessionPerUserCounter.UserSessionCounter> userSessionCounters;

		// Token: 0x04000265 RID: 613
		private static object lockObject;

		// Token: 0x0200007B RID: 123
		private class UserSessionCounter
		{
			// Token: 0x060003D4 RID: 980 RVA: 0x0001CE20 File Offset: 0x0001B020
			internal UserSessionCounter()
			{
				this.lastReportTime = DateTime.UtcNow.AddMonths(-1);
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x0001CE54 File Offset: 0x0001B054
			internal bool IsEmpty()
			{
				for (int i = 0; i < this.counters.Length; i++)
				{
					if (this.counters[i] != 0L)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x0001CE83 File Offset: 0x0001B083
			internal long GetCount(ClientType clientType)
			{
				return this.counters[(int)this.CountTypeFromClientType(clientType)];
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0001CE93 File Offset: 0x0001B093
			internal void IncrementCount(ClientType clientType)
			{
				this.counters[(int)this.CountTypeFromClientType(clientType)] += 1L;
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0001CEB5 File Offset: 0x0001B0B5
			internal void DecrementCount(ClientType clientType)
			{
				this.counters[(int)this.CountTypeFromClientType(clientType)] -= 1L;
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x0001CED8 File Offset: 0x0001B0D8
			internal bool IsClientOverQuota(ClientType clientType, long effectiveLimitation, bool mustBeStrictlyUnderQuota, out bool needLogEvent)
			{
				needLogEvent = false;
				long num = this.counters[(int)this.CountTypeFromClientType(clientType)];
				bool flag = mustBeStrictlyUnderQuota ? (num >= effectiveLimitation) : (num > effectiveLimitation);
				if (flag)
				{
					if (DateTime.UtcNow - this.lastReportTime > MapiSessionPerUserCounter.UserSessionCounter.eventLogInterval)
					{
						needLogEvent = true;
						this.lastReportTime = DateTime.UtcNow;
					}
					DiagnosticContext.TraceDwordAndString((LID)57936U, (uint)(effectiveLimitation & (long)((ulong)-1)), clientType.ToString());
					DiagnosticContext.TraceDword((LID)33360U, (uint)num);
				}
				return flag;
			}

			// Token: 0x060003DA RID: 986 RVA: 0x0001CF6C File Offset: 0x0001B16C
			private MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType CountTypeFromClientType(ClientType clientType)
			{
				switch (clientType)
				{
				case ClientType.Administrator:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.Administrator;
				case ClientType.User:
				case ClientType.Transport:
				case ClientType.EventBasedAssistants:
				case ClientType.RpcHttp:
				case ClientType.ContentIndexing:
				case ClientType.Monitoring:
					break;
				case ClientType.AirSync:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.AirSync;
				case ClientType.OWA:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.OWA;
				case ClientType.Imap:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.Imap;
				case ClientType.AvailabilityService:
				case ClientType.Management:
				case ClientType.WebServices:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.WebServices;
				case ClientType.ELC:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.ELC;
				case ClientType.UnifiedMessaging:
					return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.UnifiedMessaging;
				default:
					if (clientType == ClientType.Pop)
					{
						return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.Pop;
					}
					break;
				}
				return MapiSessionPerUserCounter.UserSessionCounter.MapiSessionPerUserCountType.MoMT;
			}

			// Token: 0x04000266 RID: 614
			private static TimeSpan eventLogInterval = TimeSpan.FromMinutes(30.0);

			// Token: 0x04000267 RID: 615
			private DateTime lastReportTime;

			// Token: 0x04000268 RID: 616
			private long[] counters = new long[9];

			// Token: 0x0200007C RID: 124
			private enum MapiSessionPerUserCountType
			{
				// Token: 0x0400026A RID: 618
				MoMT,
				// Token: 0x0400026B RID: 619
				AirSync,
				// Token: 0x0400026C RID: 620
				OWA,
				// Token: 0x0400026D RID: 621
				Pop,
				// Token: 0x0400026E RID: 622
				Imap,
				// Token: 0x0400026F RID: 623
				UnifiedMessaging,
				// Token: 0x04000270 RID: 624
				WebServices,
				// Token: 0x04000271 RID: 625
				ELC,
				// Token: 0x04000272 RID: 626
				Administrator,
				// Token: 0x04000273 RID: 627
				MaxValue
			}
		}
	}
}
