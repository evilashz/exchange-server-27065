using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000216 RID: 534
	internal sealed class ClientLogEvent : ILogEvent
	{
		// Token: 0x0600272B RID: 10027 RVA: 0x0007AB24 File Offset: 0x00078D24
		internal ClientLogEvent(Datapoint datapoint)
		{
			this.datapoint = datapoint;
			LocalSession localSession = RbacPrincipal.GetCurrent(false) as LocalSession;
			string value = (localSession == null) ? string.Empty : localSession.LogonTypeFlag;
			this.datapointProperties = new Dictionary<string, object>
			{
				{
					"TIME",
					this.datapoint.Time
				},
				{
					"SID",
					HttpContext.Current.GetSessionID()
				},
				{
					"USID",
					(HttpContext.Current.User is RbacSession) ? HttpContext.Current.GetCachedUserUniqueKey() : string.Empty
				},
				{
					"SRC",
					this.datapoint.Src
				},
				{
					"REQID",
					this.datapoint.ReqId
				},
				{
					"IP",
					this.GetClientIP()
				},
				{
					"UA",
					HttpUtility.UrlEncode(HttpContext.Current.Request.UserAgent)
				},
				{
					"BLD",
					ExchangeSetupContext.InstalledVersion.ToString()
				},
				{
					"LTYPE",
					value
				}
			};
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x0007AC3D File Offset: 0x00078E3D
		public string EventId
		{
			get
			{
				return RbacContext.ClientAppId.ToString() + "." + this.datapoint.Name;
			}
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0007AC63 File Offset: 0x00078E63
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return this.datapointProperties;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x0007AC6C File Offset: 0x00078E6C
		private string GetClientIP()
		{
			string text = HttpContext.Current.Request.Headers["X-Forwarded-For"];
			if (string.IsNullOrEmpty(text))
			{
				text = HttpContext.Current.Request.UserHostAddress;
			}
			return text;
		}

		// Token: 0x04001FCC RID: 8140
		private readonly Datapoint datapoint;

		// Token: 0x04001FCD RID: 8141
		private Dictionary<string, object> datapointProperties;
	}
}
