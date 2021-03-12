using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000072 RID: 114
	internal sealed class HealthCheckResponder
	{
		// Token: 0x06000375 RID: 885 RVA: 0x000144B8 File Offset: 0x000126B8
		private HealthCheckResponder()
		{
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000144CC File Offset: 0x000126CC
		public static HealthCheckResponder Instance
		{
			get
			{
				if (HealthCheckResponder.instance == null)
				{
					lock (HealthCheckResponder.staticLock)
					{
						if (HealthCheckResponder.instance == null)
						{
							HealthCheckResponder.instance = new HealthCheckResponder();
						}
					}
				}
				return HealthCheckResponder.instance;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014524 File Offset: 0x00012724
		public bool IsHealthCheckRequest(HttpContext httpContext)
		{
			return httpContext.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) && httpContext.Request.Url.AbsolutePath.EndsWith(Constants.HealthCheckPage, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001455C File Offset: 0x0001275C
		public void CheckHealthStateAndRespond(HttpContext httpContext)
		{
			if (!HealthCheckResponder.HealthCheckResponderEnabled.Value)
			{
				this.RespondSuccess(httpContext);
			}
			else
			{
				ServerComponentEnum serverComponent = ServerComponentEnum.None;
				if (!HealthCheckResponder.ProtocolServerComponentMap.TryGetValue(HttpProxyGlobals.ProtocolType, out serverComponent))
				{
					throw new InvalidOperationException("Unknown protocol type " + HttpProxyGlobals.ProtocolType);
				}
				if (HealthCheckResponder.HealthCheckResponderServerComponentOverride.Value != ServerComponentEnum.None)
				{
					serverComponent = HealthCheckResponder.HealthCheckResponderServerComponentOverride.Value;
				}
				DateTime utcNow = DateTime.UtcNow;
				if (this.componentStateNextLookupTime <= utcNow)
				{
					this.isComponentOnline = ServerComponentStateManager.IsOnline(serverComponent);
					this.componentStateNextLookupTime = utcNow.AddSeconds(15.0);
				}
				if (!this.isComponentOnline)
				{
					this.RespondFailure(httpContext);
				}
				else
				{
					this.RespondSuccess(httpContext);
				}
			}
			httpContext.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00014620 File Offset: 0x00012820
		private void RespondSuccess(HttpContext httpContext)
		{
			PerfCounters.HttpProxyCountersInstance.LoadBalancerHealthChecks.RawValue = 1L;
			httpContext.Response.StatusCode = 200;
			httpContext.Response.Write(Constants.HealthCheckPageResponse);
			httpContext.Response.Write("<br/>");
			httpContext.Response.Write(HttpProxyGlobals.LocalMachineFqdn.Member);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00014683 File Offset: 0x00012883
		private void RespondFailure(HttpContext httpContext)
		{
			PerfCounters.HttpProxyCountersInstance.LoadBalancerHealthChecks.RawValue = 0L;
			httpContext.Response.Close();
		}

		// Token: 0x04000271 RID: 625
		private const int ComponentStateLookupTimeIntervalInSeconds = 15;

		// Token: 0x04000272 RID: 626
		private static readonly BoolAppSettingsEntry HealthCheckResponderEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("HealthCheckResponderEnabled"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000273 RID: 627
		private static readonly EnumAppSettingsEntry<ServerComponentEnum> HealthCheckResponderServerComponentOverride = new EnumAppSettingsEntry<ServerComponentEnum>(HttpProxySettings.Prefix("HealthCheckResponderServerComponentOverride"), ServerComponentEnum.None, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000274 RID: 628
		private static readonly Dictionary<ProtocolType, ServerComponentEnum> ProtocolServerComponentMap = new Dictionary<ProtocolType, ServerComponentEnum>
		{
			{
				ProtocolType.Autodiscover,
				ServerComponentEnum.AutoDiscoverProxy
			},
			{
				ProtocolType.Eas,
				ServerComponentEnum.ActiveSyncProxy
			},
			{
				ProtocolType.Ecp,
				ServerComponentEnum.EcpProxy
			},
			{
				ProtocolType.Ews,
				ServerComponentEnum.EwsProxy
			},
			{
				ProtocolType.Mapi,
				ServerComponentEnum.MapiProxy
			},
			{
				ProtocolType.Oab,
				ServerComponentEnum.OabProxy
			},
			{
				ProtocolType.Owa,
				ServerComponentEnum.OwaProxy
			},
			{
				ProtocolType.OwaCalendar,
				ServerComponentEnum.OwaProxy
			},
			{
				ProtocolType.PushNotifications,
				ServerComponentEnum.PushNotificationsProxy
			},
			{
				ProtocolType.PowerShell,
				ServerComponentEnum.RpsProxy
			},
			{
				ProtocolType.PowerShellLiveId,
				ServerComponentEnum.RpsProxy
			},
			{
				ProtocolType.ReportingWebService,
				ServerComponentEnum.RwsProxy
			},
			{
				ProtocolType.RpcHttp,
				ServerComponentEnum.RpcProxy
			},
			{
				ProtocolType.Xrop,
				ServerComponentEnum.XropProxy
			}
		};

		// Token: 0x04000275 RID: 629
		private static HealthCheckResponder instance = null;

		// Token: 0x04000276 RID: 630
		private static object staticLock = new object();

		// Token: 0x04000277 RID: 631
		private bool isComponentOnline;

		// Token: 0x04000278 RID: 632
		private DateTime componentStateNextLookupTime = DateTime.UtcNow;
	}
}
