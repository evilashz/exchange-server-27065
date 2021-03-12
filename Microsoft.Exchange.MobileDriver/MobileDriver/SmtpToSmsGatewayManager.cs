using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000041 RID: 65
	internal class SmtpToSmsGatewayManager : IMobileServiceManager
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public SmtpToSmsGatewayManager(SmtpToSmsGatewaySelector selector)
		{
			this.Selector = selector;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007E03 File Offset: 0x00006003
		IMobileServiceSelector IMobileServiceManager.Selector
		{
			get
			{
				return this.Selector;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007E0B File Offset: 0x0000600B
		public bool CapabilityPerRecipientSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007E0E File Offset: 0x0000600E
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00007E16 File Offset: 0x00006016
		public SmtpToSmsGatewaySelector Selector { get; private set; }

		// Token: 0x0600015F RID: 351 RVA: 0x00007E20 File Offset: 0x00006020
		public TextMessagingHostingDataServicesServiceSmtpToSmsGateway GetParameters(MobileRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (recipient.Region == null)
			{
				throw new ArgumentException("recipient");
			}
			TextMessagingHostingDataCache instance = TextMessagingHostingDataCache.Instance;
			TextMessagingHostingDataServicesService service = TextMessagingHostingDataCache.Instance.GetService(recipient.Region.TwoLetterISORegionName, recipient.Carrier.ToString("00000"), TextMessagingHostingDataServicesServiceType.SmtpToSmsGateway);
			if (service == null)
			{
				return null;
			}
			return service.SmtpToSmsGateway;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007E8C File Offset: 0x0000608C
		public SmtpToSmsGatewayCapability GetCapabilityForRecipient(MobileRecipient recipient)
		{
			TextMessagingHostingDataServicesServiceSmtpToSmsGateway parameters = this.GetParameters(recipient);
			if (parameters == null)
			{
				return null;
			}
			List<CodingSupportability> list = new List<CodingSupportability>(parameters.MessageRendering.Capacity.Length);
			TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity[] capacity = parameters.MessageRendering.Capacity;
			int i = 0;
			while (i < capacity.Length)
			{
				TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity = capacity[i];
				CodingScheme codingScheme = CodingScheme.Neutral;
				try
				{
					codingScheme = (CodingScheme)Enum.Parse(typeof(CodingScheme), textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity.CodingScheme.ToString());
				}
				catch (ArgumentException)
				{
					goto IL_85;
				}
				goto IL_64;
				IL_85:
				i++;
				continue;
				IL_64:
				if (0 < textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity.Value)
				{
					list.Add(new CodingSupportability(codingScheme, textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity.Value, textMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity.Value));
					goto IL_85;
				}
				goto IL_85;
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new SmtpToSmsGatewayCapability(PartType.Short, 1, list.ToArray(), FeatureSupportability.None, parameters);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007F58 File Offset: 0x00006158
		MobileServiceCapability IMobileServiceManager.GetCapabilityForRecipient(MobileRecipient recipient)
		{
			return this.GetCapabilityForRecipient(recipient);
		}

		// Token: 0x040000D9 RID: 217
		private const string ServiceType = "SmtpToSmsGateway";

		// Token: 0x040000DA RID: 218
		private const string MessageRenderingContainerBody = "Body";
	}
}
