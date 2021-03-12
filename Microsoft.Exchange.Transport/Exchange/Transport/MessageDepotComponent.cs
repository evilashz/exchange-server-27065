using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200002F RID: 47
	internal sealed class MessageDepotComponent : IMessageDepotComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00004893 File Offset: 0x00002A93
		public void SetLoadTimeDependencies(MessageDepotConfig msgDepotConfig)
		{
			ArgumentValidator.ThrowIfNull("msgDepotConfig", msgDepotConfig);
			this.msgDepotConfig = msgDepotConfig;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000048A8 File Offset: 0x00002AA8
		public void Load()
		{
			if (!this.msgDepotConfig.IsMessageDepotEnabled)
			{
				return;
			}
			this.messageDepot = new MessageDepot(null, new TimeSpan?(this.msgDepotConfig.DelayNotificationTimeout));
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.TimedUpdate), null, MessageDepotComponent.RefreshTimeInterval);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000048FC File Offset: 0x00002AFC
		public void Unload()
		{
			if (!this.msgDepotConfig.IsMessageDepotEnabled)
			{
				return;
			}
			this.refreshTimer.Dispose(false);
			this.messageDepot = null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000491F File Offset: 0x00002B1F
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004922 File Offset: 0x00002B22
		public string GetDiagnosticComponentName()
		{
			return "MessageDepot";
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000492C File Offset: 0x00002B2C
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			bool flag = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("showCounts", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag2)
			{
				xelement.Add(TransportAppConfig.GetDiagnosticInfoForType(this.msgDepotConfig));
			}
			if (flag3)
			{
				foreach (object obj in Enum.GetValues(typeof(MessageDepotItemStage)))
				{
					int num = (int)obj;
					XElement xelement2 = new XElement(Enum.GetName(typeof(MessageDepotItemStage), num));
					foreach (object obj2 in Enum.GetValues(typeof(MessageDepotItemState)))
					{
						int num2 = (int)obj2;
						long count = this.messageDepot.GetCount((MessageDepotItemStage)num, (MessageDepotItemState)num2);
						if (count > 0L)
						{
							XElement content = new XElement(Enum.GetName(typeof(MessageDepotItemState), num2), count);
							xelement2.Add(content);
						}
					}
					xelement.Add(xelement2);
				}
			}
			if (flag)
			{
				xelement.Add(new XElement("help", "Supported arguments: config, showCounts, help"));
			}
			return xelement;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public IMessageDepot MessageDepot
		{
			get
			{
				return this.messageDepot;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public bool Enabled
		{
			get
			{
				return this.msgDepotConfig.IsMessageDepotEnabled;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B05 File Offset: 0x00002D05
		private void TimedUpdate(object state)
		{
			if (this.messageDepot != null)
			{
				this.messageDepot.TimedUpdate();
			}
		}

		// Token: 0x04000075 RID: 117
		private static readonly TimeSpan RefreshTimeInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000076 RID: 118
		private MessageDepot messageDepot;

		// Token: 0x04000077 RID: 119
		private GuardedTimer refreshTimer;

		// Token: 0x04000078 RID: 120
		private MessageDepotConfig msgDepotConfig;
	}
}
