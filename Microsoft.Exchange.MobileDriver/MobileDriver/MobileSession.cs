using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000038 RID: 56
	internal class MobileSession : IMobileActionProvider
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00006D74 File Offset: 0x00004F74
		public MobileSession(IMobileServiceSelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			this.SendMode = MobileSessionSendMode.SynchronousSend;
			this.Services = new ReadOnlyCollection<IMobileService>(new IMobileService[]
			{
				MobileServiceCreator.Create(selector)
			});
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public MobileSession(ExchangePrincipal principal, IList<DeliveryPoint> deliveryPoints)
		{
			if (deliveryPoints == null)
			{
				throw new ArgumentNullException("deliveryPoints");
			}
			if (deliveryPoints.Count == 0)
			{
				throw new ArgumentException("deliveryPoints");
			}
			this.SendMode = MobileSessionSendMode.SynchronousSend;
			this.Principal = principal;
			List<IMobileService> list = new List<IMobileService>();
			foreach (DeliveryPoint dp in DeliveryPoint.GetPersonToPersonPreferences(deliveryPoints))
			{
				list.Add(MobileServiceCreator.Create(principal, dp));
			}
			this.Services = list.AsReadOnly();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006E54 File Offset: 0x00005054
		public MobileSession()
		{
			this.SendMode = MobileSessionSendMode.AsynchronousSend;
			MobileSession.InitializeBackEnd();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006E68 File Offset: 0x00005068
		public IList<IMobileServiceManager> ServiceManagers
		{
			get
			{
				if (MobileSessionSendMode.SynchronousSend != this.SendMode)
				{
					throw new MobileDriverStateException(Strings.ErrorInvalidState("SendMode", this.SendMode.ToString()));
				}
				List<IMobileServiceManager> list = new List<IMobileServiceManager>();
				foreach (IMobileService mobileService in this.Services)
				{
					list.Add(mobileService.Manager);
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006EF0 File Offset: 0x000050F0
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006EF8 File Offset: 0x000050F8
		private ExchangePrincipal Principal { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006F01 File Offset: 0x00005101
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006F09 File Offset: 0x00005109
		private IList<IMobileService> Services { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006F12 File Offset: 0x00005112
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006F1A File Offset: 0x0000511A
		private MobileSessionSendMode SendMode { get; set; }

		// Token: 0x06000123 RID: 291 RVA: 0x00006F24 File Offset: 0x00005124
		public void Send(Message message, MobileRecipient sender, ICollection<MobileRecipient> recipients, int maxSegmentsPerRecipient)
		{
			if (MobileSessionSendMode.SynchronousSend != this.SendMode)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("SendMode", this.SendMode.ToString()));
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			List<MobileRecipient> list = new List<MobileRecipient>(recipients);
			if (list.Count == 0)
			{
				throw new ArgumentOutOfRangeException("recipients");
			}
			IMobileService mobileService = null;
			foreach (IMobileService mobileService2 in this.Services)
			{
				if (-1 != mobileService2.Manager.Selector.PersonToPersonMessagingPriority)
				{
					if (mobileService == null)
					{
						mobileService = mobileService2;
					}
					else if (mobileService2.Manager.Selector.PersonToPersonMessagingPriority < mobileService.Manager.Selector.PersonToPersonMessagingPriority)
					{
						mobileService = mobileService2;
					}
				}
			}
			if (mobileService == null)
			{
				throw new ArgumentNullException("recipients");
			}
			new TextMessageDeliverer(new TextMessageDeliveryContext
			{
				MobileService = this.Services[0],
				Message = new MessageItem(message, sender, recipients, maxSegmentsPerRecipient)
			}).Deliver();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007060 File Offset: 0x00005260
		internal void Send(TransportAgentWrapper agentWrapper, QueueDataAvailableEventHandler<TextMessageDeliveryContext> cleanerEventHandler)
		{
			if (this.SendMode != MobileSessionSendMode.AsynchronousSend)
			{
				throw new MobileDriverStateException(Strings.ErrorInvalidState("SendMode", this.SendMode.ToString()));
			}
			TextMessageDeliveryContext textMessageDeliveryContext = new TextMessageDeliveryContext();
			textMessageDeliveryContext.AgentWrapper = agentWrapper;
			textMessageDeliveryContext.CleanerEventHandler = cleanerEventHandler;
			MobileSession.deliveringPipeline.Deliver(textMessageDeliveryContext);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000070B4 File Offset: 0x000052B4
		private static void InitializeBackEnd()
		{
			if (MobileSession.backEndInitialized)
			{
				return;
			}
			lock (typeof(MobileSession))
			{
				if (!MobileSession.backEndInitialized)
				{
					MobileSession.deliveringPipeline = new TextMessageDeliveringPipeline();
					MobileSession.deliveringPipeline.Start();
					MobileSession.backEndInitialized = true;
				}
			}
		}

		// Token: 0x040000B3 RID: 179
		private static bool backEndInitialized;

		// Token: 0x040000B4 RID: 180
		private static TextMessageDeliveringPipeline deliveringPipeline;
	}
}
