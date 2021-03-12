using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003BD RID: 957
	internal static class ClientAccessRulesUtils
	{
		// Token: 0x06002BEC RID: 11244 RVA: 0x000B56B4 File Offset: 0x000B38B4
		internal static bool ShouldBlockConnection(OrganizationId organizationId, string username, ClientAccessProtocol protocol, IPEndPoint remoteEndpoint, ClientAccessAuthenticationMethod authenticationType, Action<ClientAccessRulesEvaluationContext> blockLoggerDelegate, Action<double> latencyLoggerDelegate)
		{
			return ClientAccessRulesUtils.ShouldBlockConnection(organizationId, username, protocol, remoteEndpoint, authenticationType, null, blockLoggerDelegate, latencyLoggerDelegate);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000B56E4 File Offset: 0x000B38E4
		internal static bool ShouldBlockConnection(OrganizationId organizationId, string username, ClientAccessProtocol protocol, IPEndPoint remoteEndpoint, ClientAccessAuthenticationMethod authenticationType, IReadOnlyPropertyBag propertyBag, Action<ClientAccessRulesEvaluationContext> blockLoggerDelegate, Action<double> latencyLoggerDelegate)
		{
			DateTime utcNow = DateTime.UtcNow;
			bool shouldBlock = false;
			long ticks = utcNow.Ticks;
			if (organizationId == null)
			{
				ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(ticks, "[Client Access Rules] ShouldBlockConnection assuming OrganizationId.ForestWideOrgId for null OrganizationId");
				organizationId = OrganizationId.ForestWideOrgId;
			}
			if (remoteEndpoint != null)
			{
				ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(ticks, "[Client Access Rules] ShouldBlockConnection - Initializing context to run rules");
				ClientAccessRuleCollection collection = ClientAccessRulesCache.Instance.GetCollection(organizationId);
				ClientAccessRulesEvaluationContext context = new ClientAccessRulesEvaluationContext(collection, username, remoteEndpoint, protocol, authenticationType, propertyBag, ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>(), delegate(ClientAccessRulesEvaluationContext evaluationContext)
				{
					shouldBlock = true;
					blockLoggerDelegate(evaluationContext);
				}, null, ticks);
				collection.Run(context);
			}
			ClientAccessRulesPerformanceCounters.TotalClientAccessRulesEvaluationCalls.Increment();
			if (shouldBlock)
			{
				ClientAccessRulesPerformanceCounters.TotalConnectionsBlockedByClientAccessRules.Increment();
			}
			double totalMilliseconds = (DateTime.UtcNow - utcNow).TotalMilliseconds;
			latencyLoggerDelegate(totalMilliseconds);
			if (totalMilliseconds > 50.0)
			{
				ClientAccessRulesPerformanceCounters.TotalClientAccessRulesEvaluationCallsOver50ms.Increment();
			}
			if (totalMilliseconds > 10.0)
			{
				ClientAccessRulesPerformanceCounters.TotalClientAccessRulesEvaluationCallsOver10ms.Increment();
			}
			ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(ticks, string.Format("[Client Access Rules] ShouldBlockConnection - Evaluate - Org: {0} - Protocol: {1} - Username: {2} - IP: {3} - Port: {4} - Auth Type: {5} - Blocked: {6} - Latency: {7}", new object[]
			{
				organizationId.ToString(),
				protocol.ToString(),
				username.ToString(),
				remoteEndpoint.Address.ToString(),
				remoteEndpoint.Port.ToString(),
				authenticationType.ToString(),
				shouldBlock.ToString(),
				totalMilliseconds.ToString()
			}));
			return shouldBlock;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000B5890 File Offset: 0x000B3A90
		internal static IPEndPoint GetRemoteEndPointFromContext(HttpContext httpContext)
		{
			int remotePortFromContext = ClientAccessRulesUtils.GetRemotePortFromContext(httpContext);
			IPAddress remoteIPAddressFromContext = ClientAccessRulesUtils.GetRemoteIPAddressFromContext(httpContext);
			if (httpContext == null || remoteIPAddressFromContext == null || remotePortFromContext == 0)
			{
				return null;
			}
			return new IPEndPoint(remoteIPAddressFromContext, remotePortFromContext);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000B58C0 File Offset: 0x000B3AC0
		private static int GetRemotePortFromContext(HttpContext httpContext)
		{
			string text = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_PORT"];
			int result;
			if (text != null)
			{
				text = text.Split(new char[]
				{
					','
				}).Last<string>();
				if (int.TryParse(text, out result))
				{
					return result;
				}
			}
			if (int.TryParse(httpContext.Request.ServerVariables["REMOTE_PORT"], out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000B592C File Offset: 0x000B3B2C
		private static IPAddress GetRemoteIPAddressFromContext(HttpContext httpContext)
		{
			if (httpContext == null || httpContext.Request == null || httpContext.Request.ServerVariables == null)
			{
				return null;
			}
			string text = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			IPAddress result;
			if (text != null)
			{
				text = text.Split(new char[]
				{
					','
				}).Last<string>();
				if (IPAddress.TryParse(text, out result))
				{
					return result;
				}
			}
			if (IPAddress.TryParse(httpContext.Request.UserHostAddress, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000B59A8 File Offset: 0x000B3BA8
		internal static string GetUsernameFromContext(HttpContext httpContext)
		{
			if (httpContext.Request.Headers[WellKnownHeader.WLIDMemberName] != null)
			{
				string text = httpContext.Request.Headers[WellKnownHeader.WLIDMemberName].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					SmtpAddress smtpAddress = SmtpAddress.Parse(text);
					return string.Format("{0}\\{1}", smtpAddress.Domain, smtpAddress.Local);
				}
			}
			if (httpContext.Items.Contains("AuthenticatedUser") && httpContext.Items["AuthenticatedUser"] != null)
			{
				return httpContext.Items["AuthenticatedUser"].ToString();
			}
			return string.Empty;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000B5A50 File Offset: 0x000B3C50
		internal static string GetUsernameFromADRawEntry(ADRawEntry rawEntry)
		{
			SmtpAddress smtpAddress = SmtpAddress.Empty;
			if (rawEntry[ADRecipientSchema.WindowsLiveID] != null)
			{
				smtpAddress = (SmtpAddress)rawEntry[ADRecipientSchema.WindowsLiveID];
				if (smtpAddress.IsValidAddress)
				{
					return ClientAccessRulesUtils.GetUsernameFromWindowsLiveId(smtpAddress);
				}
			}
			return ClientAccessRulesUtils.GetUsernameFromIdInformation(smtpAddress, (SecurityIdentifier)rawEntry[ADRecipientSchema.MasterAccountSid], (SecurityIdentifier)rawEntry[ADMailboxRecipientSchema.Sid], rawEntry.Id);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000B5ABD File Offset: 0x000B3CBD
		internal static string GetUsernameFromADObjectId(ADObjectId adObjectId)
		{
			if (adObjectId == null || adObjectId.DomainId == null)
			{
				return string.Empty;
			}
			return string.Format("{0}\\{1}", adObjectId.DomainId.Name, adObjectId.Name);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000B5AEB File Offset: 0x000B3CEB
		internal static string GetUsernameFromWindowsLiveId(SmtpAddress smtpAddress)
		{
			return string.Format("{0}\\{1}", smtpAddress.Domain, smtpAddress.Local);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000B5B08 File Offset: 0x000B3D08
		internal static string GetUsernameFromIdInformation(SmtpAddress liveId, SecurityIdentifier masterAccountSid, SecurityIdentifier sid, ADObjectId adObjectId)
		{
			if (liveId.IsValidAddress)
			{
				return ClientAccessRulesUtils.GetUsernameFromWindowsLiveId(liveId);
			}
			if (masterAccountSid != null)
			{
				return SidToAccountMap.Singleton.Get(masterAccountSid);
			}
			if (sid != null)
			{
				return SidToAccountMap.Singleton.Get(sid);
			}
			return ClientAccessRulesUtils.GetUsernameFromADObjectId(adObjectId);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000B5B58 File Offset: 0x000B3D58
		internal static ClientAccessRule GetAllowLocalClientAccessRule()
		{
			return new ADClientAccessRule
			{
				Name = "[Allow Local Connections In-Memory Hardcoded Rule]",
				Priority = 1,
				Enabled = true,
				DatacenterAdminsOnly = true,
				Action = ClientAccessRulesAction.AllowAccess,
				AnyOfClientIPAddressesOrRanges = ClientAccessRulesUtils.GetAllLocalIPAddresses()
			}.GetClientAccessRule();
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000B5BC0 File Offset: 0x000B3DC0
		private static IPRange[] GetAllLocalIPAddresses()
		{
			List<IPRange> list = new List<IPRange>();
			try
			{
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				foreach (NetworkInterface networkInterface in from a in allNetworkInterfaces
				where a.OperationalStatus == OperationalStatus.Up
				select a)
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					UnicastIPAddressInformationCollection unicastAddresses = ipproperties.UnicastAddresses;
					foreach (IPAddressInformation ipaddressInformation in unicastAddresses.OrderBy((UnicastIPAddressInformation ua) => ua.Address.AddressFamily))
					{
						if (!ipaddressInformation.IsTransient)
						{
							if (ipaddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
							{
								list.Add(IPRange.Parse(ipaddressInformation.Address.ToString()));
							}
							else if (ipaddressInformation.Address.AddressFamily == AddressFamily.InterNetworkV6)
							{
								list.Add(IPRange.Parse(ipaddressInformation.Address.ToString()));
							}
						}
					}
				}
			}
			catch (NetworkInformationException ex)
			{
				ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(0L, string.Format("[Client Access Rules] GetAllLocalIPAddresses threw an unexpected exception ({0})", ex.ToString()));
			}
			return list.Distinct<IPRange>().ToArray<IPRange>();
		}

		// Token: 0x04001A58 RID: 6744
		private const string AuthenticatedUserItemName = "AuthenticatedUser";

		// Token: 0x04001A59 RID: 6745
		private const string UsernameFormatString = "{0}\\{1}";
	}
}
