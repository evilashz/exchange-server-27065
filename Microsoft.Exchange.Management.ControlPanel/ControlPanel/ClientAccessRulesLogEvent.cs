using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000215 RID: 533
	internal sealed class ClientAccessRulesLogEvent : ILogEvent
	{
		// Token: 0x06002728 RID: 10024 RVA: 0x0007A9FC File Offset: 0x00078BFC
		internal ClientAccessRulesLogEvent(OrganizationId orgId, string username, IPEndPoint endpoint, ClientAccessAuthenticationMethod authenticationType, string blockingRuleName, double latency, bool blocked)
		{
			ActivityContext.ActivityId.FormatForLog();
			this.datapointProperties = new Dictionary<string, object>
			{
				{
					"ORGID",
					orgId.ToString()
				},
				{
					"USER",
					username
				},
				{
					"IP",
					endpoint.Address.ToString()
				},
				{
					"PORT",
					endpoint.Port.ToString()
				},
				{
					"AUTHTYPE",
					authenticationType.ToString()
				},
				{
					"RULE",
					blockingRuleName
				},
				{
					"LATENCY",
					latency.ToString()
				},
				{
					"BLOCKED",
					blocked.ToString()
				}
			};
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x0007AAD0 File Offset: 0x00078CD0
		public string EventId
		{
			get
			{
				string str = ActivityContextLoggerId.Request.ToString();
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					string property = currentActivityScope.GetProperty(ExtensibleLoggerMetadata.EventId);
					if (!string.IsNullOrEmpty(property))
					{
						str = property;
					}
				}
				return this.clientAppId + "." + str;
			}
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x0007AB1A File Offset: 0x00078D1A
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return this.datapointProperties;
		}

		// Token: 0x04001FCA RID: 8138
		private Dictionary<string, object> datapointProperties;

		// Token: 0x04001FCB RID: 8139
		private readonly string clientAppId = RbacContext.ClientAppId.ToString();
	}
}
