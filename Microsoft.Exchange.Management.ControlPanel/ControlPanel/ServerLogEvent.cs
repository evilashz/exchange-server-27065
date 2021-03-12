using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000219 RID: 537
	internal sealed class ServerLogEvent : ILogEvent
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x0007AE04 File Offset: 0x00079004
		internal ServerLogEvent(PSCommand psCommand, IEnumerable pipelineInput, int requestLatency, string requestId, string exception, int resultSize)
		{
			string value = ActivityContext.ActivityId.FormatForLog();
			this.datapointProperties = new Dictionary<string, object>
			{
				{
					"TIME",
					requestLatency.ToString()
				},
				{
					"SID",
					HttpContext.Current.GetSessionID()
				},
				{
					"CMD",
					PSCommandExtension.ToLogString(psCommand, pipelineInput)
				},
				{
					"REQID",
					requestId
				},
				{
					"URL",
					HttpContext.Current.Request.RawUrl
				},
				{
					"EX",
					exception
				},
				{
					"ACTID",
					value
				},
				{
					"RS",
					resultSize.ToString()
				},
				{
					"BLD",
					ExchangeSetupContext.InstalledVersion.ToString()
				}
			};
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0007AEE4 File Offset: 0x000790E4
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

		// Token: 0x06002744 RID: 10052 RVA: 0x0007AF2E File Offset: 0x0007912E
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return this.datapointProperties;
		}

		// Token: 0x04001FD9 RID: 8153
		private Dictionary<string, object> datapointProperties;

		// Token: 0x04001FDA RID: 8154
		private readonly string clientAppId = RbacContext.ClientAppId.ToString();
	}
}
