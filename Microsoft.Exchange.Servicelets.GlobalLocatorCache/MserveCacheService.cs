using System;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.GlobalLocatorCache;
using Microsoft.Exchange.Net.Mserve;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000018 RID: 24
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
	internal class MserveCacheService : GlobalLocatorCache, IMserveCacheService
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003D2C File Offset: 0x00001F2C
		public string ReadMserveData(string requestName)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Processing ReadMserveData() for request {0}", requestName);
			string text = MserveCacheService.invalidPartnerIdStr;
			string text2 = string.Empty;
			int tickCount = Environment.TickCount;
			string text3 = string.Empty;
			if (this.IsValidRequest(requestName, "Read", ref text3, ref text2))
			{
				string domainOrTenantId = this.ExtractTenantNameOrGuidFromSmtpAddress(requestName);
				text2 = (this.ShouldTreatPartnerIdValueAsMinorPartnerId(requestName) ? "MinorPartnerId" : "PartnerId");
				OfflineTenantInfo offlineTenantInfo;
				if (base.TryFindTenantInfoInCache(domainOrTenantId, out offlineTenantInfo, out text3))
				{
					text = ((text2 == "MinorPartnerId") ? offlineTenantInfo.MinorPartnerId.ToString() : offlineTenantInfo.PartnerId.ToString());
					text2 = "Read " + text2;
				}
				else
				{
					text3 = "Entry not found in GlobalLocatorCache: " + text3;
					text2 = "Failed to Read " + text2;
				}
			}
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "ReadMserveData from GlobalLocatorCache for {0}, {1} = {2}. Failure : {3}", new object[]
			{
				requestName,
				text2,
				text,
				text3
			});
			int ticksElapsed = Environment.TickCount - tickCount;
			GlobalLocatorCache.LogMserveInfo(string.Format("ReadMserveData from GlobalLocatorCache - {0}", text2), text3, ticksElapsed, requestName, text);
			return text;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E58 File Offset: 0x00002058
		public int GetChunkSize()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "Processing GetChunkSize(), default value is {0}", 100);
			return 100;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E74 File Offset: 0x00002074
		private bool ShouldTreatPartnerIdValueAsMinorPartnerId(string address)
		{
			return address.StartsWith(MserveCacheService.prefixDomainEntryAddressFormatMinorPartnerId) || address.StartsWith(MserveCacheService.prefixDomainEntryAddressFormatMinorPartnerIdForOrgGuid);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E94 File Offset: 0x00002094
		private string ExtractTenantNameOrGuidFromSmtpAddress(string address)
		{
			string text = address.Split(new char[]
			{
				'@'
			})[1];
			if (text.EndsWith("exchangereserved"))
			{
				text = text.Split(new char[]
				{
					'.'
				})[0];
			}
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003EDC File Offset: 0x000020DC
		private bool IsValidRequest(string address, string operation, ref string failure, ref string entryType)
		{
			if (!this.IsValidAddress(address))
			{
				failure = operation + " request is not a valid request";
				entryType = "Failed to " + operation;
				return false;
			}
			if (!GlobalLocatorCache.IsCacheReady())
			{
				failure = "Mserve Cache is not ready";
				entryType = "Failed to " + operation;
				return false;
			}
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003F2E File Offset: 0x0000212E
		private bool IsValidAddress(string address)
		{
			if (!SmtpAddress.IsValidSmtpAddress(address))
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "The request {0} is not in a valid Smtp address format.", address);
				return false;
			}
			return true;
		}

		// Token: 0x04000051 RID: 81
		private const int ChunkSize = 100;

		// Token: 0x04000052 RID: 82
		private static readonly string prefixDomainEntryAddressFormatMinorPartnerId = "7f66cd009b304aeda37ffdeea1733ff6@{0}".Split(new char[]
		{
			'@'
		})[0];

		// Token: 0x04000053 RID: 83
		private static readonly string prefixDomainEntryAddressFormatMinorPartnerIdForOrgGuid = "3da19c7b44a74bd3896daaf008594b6c@{0}.exchangereserved".Split(new char[]
		{
			'@'
		})[0];

		// Token: 0x04000054 RID: 84
		private static readonly string invalidPartnerIdStr = -1.ToString();
	}
}
