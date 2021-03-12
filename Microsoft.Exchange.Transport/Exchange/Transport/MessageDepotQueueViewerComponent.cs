using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000030 RID: 48
	internal sealed class MessageDepotQueueViewerComponent : IMessageDepotQueueViewerComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004B37 File Offset: 0x00002D37
		public IMessageDepotQueueViewer MessageDepotQueueViewer
		{
			get
			{
				return this.messageDepotQueueViewer;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004B3F File Offset: 0x00002D3F
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004B47 File Offset: 0x00002D47
		public void SetLoadTimeDependencies(IMessageDepotComponent messageDepotComponent, TransportAppConfig.ILegacyQueueConfig queueConfig)
		{
			ArgumentValidator.ThrowIfNull("messageDepotComponent", messageDepotComponent);
			this.messageDepotComponent = messageDepotComponent;
			this.queueConfig = queueConfig;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004B64 File Offset: 0x00002D64
		public void Load()
		{
			this.enabled = this.messageDepotComponent.Enabled;
			if (this.enabled)
			{
				this.messageDepotQueueViewer = (IMessageDepotQueueViewer)this.messageDepotComponent.MessageDepot;
				this.msgDepotLegacyPerfCounterWrapper = new MsgDepotLegacyPerfCounterWrapper(this.messageDepotComponent.MessageDepot, this.messageDepotQueueViewer, this.queueConfig);
				this.refreshTimer = new GuardedTimer(new TimerCallback(this.TimedUpdate), null, MessageDepotQueueViewerComponent.RefreshTimeInterval);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004BDF File Offset: 0x00002DDF
		public void Unload()
		{
			if (!this.enabled)
			{
				return;
			}
			this.refreshTimer.Dispose(false);
			this.msgDepotLegacyPerfCounterWrapper = null;
			this.messageDepotQueueViewer = null;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004C04 File Offset: 0x00002E04
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004C07 File Offset: 0x00002E07
		public string GetDiagnosticComponentName()
		{
			return "MessageDepotQueueViewer";
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004C10 File Offset: 0x00002E10
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			bool flag = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag3 && this.Enabled)
			{
				foreach (object obj in Enum.GetValues(typeof(MessageDepotItemStage)))
				{
					MessageDepotItemStage messageDepotItemStage = (MessageDepotItemStage)obj;
					foreach (object obj2 in Enum.GetValues(typeof(MessageDepotItemState)))
					{
						MessageDepotItemState messageDepotItemState = (MessageDepotItemState)obj2;
						long count = this.messageDepotQueueViewer.GetCount(messageDepotItemStage, messageDepotItemState);
						if (count > 0L)
						{
							XElement content = new XElement("Messages", new object[]
							{
								new XAttribute("Stage", messageDepotItemStage.ToString()),
								new XAttribute("State", messageDepotItemState.ToString()),
								new XAttribute("Count", count)
							});
							xelement.Add(content);
						}
					}
				}
			}
			if (flag2)
			{
				xelement.Add(new XElement("Enabled", this.Enabled));
			}
			if (flag)
			{
				xelement.Add(new XElement("help", "Supported arguments: verbose, config, help"));
			}
			return xelement;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004E14 File Offset: 0x00003014
		private void TimedUpdate(object state)
		{
			if (this.msgDepotLegacyPerfCounterWrapper != null)
			{
				this.msgDepotLegacyPerfCounterWrapper.TimedUpdate();
			}
		}

		// Token: 0x04000079 RID: 121
		private static readonly TimeSpan RefreshTimeInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400007A RID: 122
		private IMessageDepotQueueViewer messageDepotQueueViewer;

		// Token: 0x0400007B RID: 123
		private MsgDepotLegacyPerfCounterWrapper msgDepotLegacyPerfCounterWrapper;

		// Token: 0x0400007C RID: 124
		private GuardedTimer refreshTimer;

		// Token: 0x0400007D RID: 125
		private bool enabled;

		// Token: 0x0400007E RID: 126
		private IMessageDepotComponent messageDepotComponent;

		// Token: 0x0400007F RID: 127
		private TransportAppConfig.ILegacyQueueConfig queueConfig;
	}
}
