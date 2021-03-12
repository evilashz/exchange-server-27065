using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000217 RID: 535
	internal sealed class SubscriptionDiagnosticMethods
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x00044B64 File Offset: 0x00042D64
		internal static XmlNode ClearSubscriptions(XmlNode param)
		{
			foreach (SubscriptionBase subscriptionBase in Subscriptions.Singleton.SubscriptionsList)
			{
				Subscriptions.Singleton.Delete(subscriptionBase.SubscriptionId);
			}
			return null;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00044BC8 File Offset: 0x00042DC8
		[DiagnosticStrideJustification(DenialOfServiceJustification = "Enumerating subscriptions causes little load, and having a valid subscription id which you do not own gains you nothing over using a random id.", ElevationOfPrivligesJustification = "Each subscription keeps track of its owner; an attacker who does not own a subscription may not affect it.", InformationDisclosureJustification = "A subscription id contains the date/time created, the server the subscription is hosted on, and a random GUID.  None of this is privliged\r\n                information that could grant an attacker any advantage.", RepudiationJustification = "Common logging and AuthZ mechanisms apply here that apply to all EWS web methods.", SpoofingJustification = "Each subscription keeps track of its owner; an attacker may not spoof the owner using only a subscription id.", TamperingJustification = "Each subscription keeps track of its owner; an attacker who does not own a subscription may not affect it.  \r\n                Furthermore, this method effects no side-effect on the process.")]
		internal static XmlNode GetActiveSubscriptionIds(XmlNode param)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("Subscriptions", "http://schemas.microsoft.com/exchange/services/2006/types");
			foreach (SubscriptionBase subscriptionBase in Subscriptions.Singleton.SubscriptionsList)
			{
				if (!subscriptionBase.IsExpired)
				{
					XmlNode xmlNode2 = safeXmlDocument.CreateElement("Subscription", "http://schemas.microsoft.com/exchange/services/2006/types");
					XmlNode xmlNode3 = safeXmlDocument.CreateElement("SubscriptionId", "http://schemas.microsoft.com/exchange/services/2006/types");
					xmlNode3.InnerText = subscriptionBase.SubscriptionId;
					xmlNode2.AppendChild(xmlNode3);
					XmlNode xmlNode4 = safeXmlDocument.CreateElement("CreatorSmtpAddress", "http://schemas.microsoft.com/exchange/services/2006/types");
					xmlNode4.InnerText = subscriptionBase.CreatorSmtpAddress;
					xmlNode2.AppendChild(xmlNode4);
					xmlNode.AppendChild(xmlNode2);
				}
			}
			return xmlNode;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00044CA8 File Offset: 0x00042EA8
		internal static XmlNode GetHangingSubscriptionConnections(XmlNode param)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("Connections", "http://schemas.microsoft.com/exchange/services/2006/types");
			foreach (StreamingConnection streamingConnection in StreamingConnection.OpenConnections)
			{
				if (!streamingConnection.IsDisposed)
				{
					List<StreamingSubscription> subscriptions = streamingConnection.Subscriptions;
					string creatorSmtpAddress = streamingConnection.CreatorSmtpAddress;
					if (subscriptions != null && subscriptions.Count != 0 && !string.IsNullOrEmpty(creatorSmtpAddress))
					{
						XmlNode xmlNode2 = safeXmlDocument.CreateElement("Connection", "http://schemas.microsoft.com/exchange/services/2006/types");
						XmlNode xmlNode3 = safeXmlDocument.CreateElement("CreatorSmtpAddress", "http://schemas.microsoft.com/exchange/services/2006/types");
						xmlNode3.InnerText = streamingConnection.CreatorSmtpAddress;
						xmlNode2.AppendChild(xmlNode3);
						XmlNode xmlNode4 = safeXmlDocument.CreateElement("Subscriptions", "http://schemas.microsoft.com/exchange/services/2006/types");
						xmlNode2.AppendChild(xmlNode4);
						foreach (StreamingSubscription streamingSubscription in subscriptions)
						{
							XmlNode xmlNode5 = safeXmlDocument.CreateElement("SubscriptionId", "http://schemas.microsoft.com/exchange/services/2006/types");
							xmlNode5.InnerText = streamingSubscription.SubscriptionId;
							xmlNode4.AppendChild(xmlNode5);
						}
						xmlNode.AppendChild(xmlNode2);
					}
				}
			}
			return xmlNode;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00044E24 File Offset: 0x00043024
		internal static XmlNode SetStreamingSubscriptionTimeToLiveDefault(XmlNode param)
		{
			int timeToLiveDefault = StreamingSubscription.TimeToLiveDefault;
			bool flag = param != null && int.TryParse(param.InnerText, out timeToLiveDefault) && timeToLiveDefault > 0;
			if (flag)
			{
				StreamingSubscription.TimeToLiveDefault = timeToLiveDefault;
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("Success", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlNode.InnerText = flag.ToString();
			return xmlNode;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00044E80 File Offset: 0x00043080
		internal static XmlNode SetStreamingSubscriptionNewEventQueueSize(XmlNode param)
		{
			int num = 500;
			bool flag = param != null && int.TryParse(param.InnerText, out num) && num >= 10 && num <= 200;
			if (flag)
			{
				StreamingSubscription.NewEventQueueSize = num;
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("Success", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlNode.InnerText = flag.ToString();
			return xmlNode;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00044EE8 File Offset: 0x000430E8
		internal static XmlNode SetStreamingConnectionHeartbeatDefault(XmlNode param)
		{
			int periodicConnectionCheckInterval = StreamingConnection.PeriodicConnectionCheckInterval;
			bool flag = param != null && int.TryParse(param.InnerText, out periodicConnectionCheckInterval) && periodicConnectionCheckInterval > 0;
			if (flag)
			{
				StreamingConnection.PeriodicConnectionCheckInterval = periodicConnectionCheckInterval * 1000;
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("Success", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlNode.InnerText = flag.ToString();
			return xmlNode;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00044F4C File Offset: 0x0004314C
		internal static XmlNode GetStreamingSubscriptionExpirationTime(XmlNode param)
		{
			string innerText = param.InnerText;
			StreamingSubscription streamingSubscription = Subscriptions.Singleton.Get(innerText) as StreamingSubscription;
			if (streamingSubscription == null)
			{
				throw new SubscriptionNotFoundException();
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("ExpirationTime", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlNode.InnerText = streamingSubscription.ExpirationDateTime.UtcTicks.ToString();
			return xmlNode;
		}

		// Token: 0x04000AD7 RID: 2775
		private const string ConnectionXmlElement = "Connection";

		// Token: 0x04000AD8 RID: 2776
		private const string ConnectionsXmlElement = "Connections";

		// Token: 0x04000AD9 RID: 2777
		private const string CreatorSmtpAddressXmlElement = "CreatorSmtpAddress";

		// Token: 0x04000ADA RID: 2778
		private const string ExpirationTime = "ExpirationTime";

		// Token: 0x04000ADB RID: 2779
		private const string NewDefaultValueXmlElement = "NewDefaultValue";

		// Token: 0x04000ADC RID: 2780
		private const string SubscriptionXmlElement = "Subscription";

		// Token: 0x04000ADD RID: 2781
		private const string SubscriptionIdXmlElement = "SubscriptionId";

		// Token: 0x04000ADE RID: 2782
		private const string SubscriptionsXmlElement = "Subscriptions";

		// Token: 0x04000ADF RID: 2783
		private const string SuccessXmlElement = "Success";
	}
}
