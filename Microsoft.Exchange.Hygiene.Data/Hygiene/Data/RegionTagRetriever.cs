using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HygieneData;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200009E RID: 158
	internal class RegionTagRetriever
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x00011B85 File Offset: 0x0000FD85
		static RegionTagRetriever()
		{
			RegionTagRetriever.regionTagCache = RegionTagCache.GetInstance();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public RegionTagRetriever(GlsCallerId callerId)
		{
			ArgumentValidator.ThrowIfNull("callerId", callerId);
			this.glsCallerId = callerId;
			lock (RegionTagRetriever.glsSessionMapLock)
			{
				if (RegionTagRetriever.callIdToGlsSessionMap.ContainsKey(callerId))
				{
					this.glsSession = RegionTagRetriever.callIdToGlsSessionMap[callerId];
				}
				else
				{
					this.glsSession = this.CreateGlsSession(callerId);
					RegionTagRetriever.callIdToGlsSessionMap[callerId] = this.glsSession;
				}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00011C3C File Offset: 0x0000FE3C
		public static void AddRegionTag(Guid tenantId, string regionTag)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("regionTag", regionTag);
			RegionTagRetriever.regionTagCache.AddGoodTenant(tenantId, regionTag);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00011C58 File Offset: 0x0000FE58
		public string GetRegionTag(Guid tenantId)
		{
			bool flag;
			return this.GetRegionTag(tenantId, out flag, false, false);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00011C70 File Offset: 0x0000FE70
		public string GetRegionTag(Guid tenantId, out bool cacheHit, bool throwOnError = false)
		{
			return this.GetRegionTag(tenantId, out cacheHit, throwOnError, false);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public string GetRegionTag(Guid tenantId, out bool cacheHit, bool throwOnError, bool useMachineRegionIfGLSTenantWithoutRegion)
		{
			cacheHit = true;
			string text = RegionTagRetriever.regionTagCache.Get(tenantId);
			if (text == null)
			{
				cacheHit = false;
				bool flag = false;
				text = this.QueryGls(tenantId, throwOnError, out flag);
				if (string.IsNullOrWhiteSpace(text) && useMachineRegionIfGLSTenantWithoutRegion)
				{
					text = DalHelper.RegionTag;
					RegionTagRetriever.regionTagCache.AddGoodTenant(tenantId, text);
				}
			}
			return text;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00011CCA File Offset: 0x0000FECA
		protected virtual string GetRegionTagFromGLS(Guid tenantId)
		{
			return this.glsSession.GetFfoTenantRegionByOrgGuid(tenantId);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00011CD8 File Offset: 0x0000FED8
		protected virtual GlsDirectorySession CreateGlsSession(GlsCallerId callerId)
		{
			return new GlsDirectorySession(callerId);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		private static void PublishRecoverEvent(string msg)
		{
			if (Interlocked.CompareExchange(ref RegionTagRetriever.isGlsOk, 1, 0) == 0)
			{
				EventNotificationItem.Publish(ExchangeComponent.MessageTracing.Name, "RegionTagNonUrgent", null, msg, ResultSeverityLevel.Informational, false);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011D08 File Offset: 0x0000FF08
		private string QueryGls(Guid tenantId, bool throwOnError, out bool tenantInGLS)
		{
			string text = string.Empty;
			string result = null;
			int i = 3;
			Exception ex = null;
			tenantInGLS = true;
			while (i > 0)
			{
				try
				{
					ex = null;
					result = null;
					string regionTagFromGLS = this.GetRegionTagFromGLS(tenantId);
					if (string.IsNullOrWhiteSpace(regionTagFromGLS))
					{
						EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_FailedToRetrieveRegionTagWhenQueryGLS, tenantId.ToString(), new object[]
						{
							tenantId,
							this.glsCallerId.CallerIdString,
							"Region property is not valid for the tenant."
						});
						result = string.Empty;
						RegionTagRetriever.regionTagCache.AddBadTenant(tenantId);
					}
					else
					{
						RegionTagRetriever.PublishRecoverEvent("Retrieving region property from GLS succeeded.");
						RegionTagRetriever.regionTagCache.AddGoodTenant(tenantId, regionTagFromGLS);
						result = regionTagFromGLS.Trim();
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
					text = string.Format("Exception from GLS when fetching region tag. Tenant:{1} Detail is:\n{0}", ex2, tenantId);
					ExTraceGlobals.GLSQueryTracer.TraceError((long)this.GetHashCode(), text);
					if (ex is GlsTenantNotFoundException)
					{
						EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_FailedToRetrieveRegionTagWhenQueryGLS, tenantId.ToString(), new object[]
						{
							tenantId,
							this.glsCallerId.CallerIdString,
							"Unable to find Tenant in GLS"
						});
						result = string.Empty;
						RegionTagRetriever.regionTagCache.AddBadTenant(tenantId);
						tenantInGLS = false;
						ex = null;
					}
					else if (ex is GlsTransientException)
					{
						EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_TransientExceptionWhenQueryGLS, ex.Message, new object[]
						{
							ex,
							tenantId
						});
						if (throwOnError && i == 0)
						{
							throw;
						}
					}
					else
					{
						if (!(ex is GlsPermanentException))
						{
							EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_UnknownExceptionWhenQueryGLS, ex.Message, new object[]
							{
								ex,
								tenantId
							});
							i = 0;
							throw;
						}
						EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_PermanentExceptionWhenQueryGLS, ex.Message, new object[]
						{
							ex,
							tenantId
						});
						i = 0;
						if (throwOnError)
						{
							throw;
						}
					}
				}
				finally
				{
					if (ex != null)
					{
						if (i == 0)
						{
							this.PublishErrorEvent(text);
						}
						i--;
					}
					else
					{
						i = 0;
					}
				}
			}
			return result;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00011F5C File Offset: 0x0001015C
		private void PublishErrorEvent(string error)
		{
			if (Interlocked.CompareExchange(ref RegionTagRetriever.isGlsOk, 0, 1) == 1)
			{
				EventNotificationItem.Publish(ExchangeComponent.MessageTracing.Name, "RegionTagNonUrgent", null, string.Format("{0}\nThe caller id is {1}", error, this.glsCallerId.CallerIdString), ResultSeverityLevel.Error, false);
			}
		}

		// Token: 0x0400035B RID: 859
		private const string NonUrgentComponentName = "RegionTagNonUrgent";

		// Token: 0x0400035C RID: 860
		private static RegionTagCache regionTagCache;

		// Token: 0x0400035D RID: 861
		private static int isGlsOk = 1;

		// Token: 0x0400035E RID: 862
		private static Dictionary<GlsCallerId, GlsDirectorySession> callIdToGlsSessionMap = new Dictionary<GlsCallerId, GlsDirectorySession>();

		// Token: 0x0400035F RID: 863
		private GlsCallerId glsCallerId;

		// Token: 0x04000360 RID: 864
		private GlsDirectorySession glsSession;

		// Token: 0x04000361 RID: 865
		private static object glsSessionMapLock = new object();
	}
}
