using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Common;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000003 RID: 3
	internal static class ExchangeComponent
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000024A8 File Offset: 0x000006A8
		static ExchangeComponent()
		{
			bool flag = Datacenter.IsMicrosoftHostedOnly(false) || Datacenter.IsDatacenterDedicated(false);
			Dictionary<string, ServerComponentEnum> dictionary = new Dictionary<string, ServerComponentEnum>(StringComparer.InvariantCultureIgnoreCase);
			dictionary.Add("ECP.Proxy", ServerComponentEnum.EcpProxy);
			dictionary.Add("OAB.Proxy", ServerComponentEnum.OabProxy);
			dictionary.Add("OWA.Proxy", ServerComponentEnum.OwaProxy);
			dictionary.Add("Outlook.Proxy", ServerComponentEnum.RpcProxy);
			dictionary.Add("OutlookMapiHttp.Proxy", ServerComponentEnum.MapiProxy);
			dictionary.Add("EWS.Proxy", ServerComponentEnum.EwsProxy);
			dictionary.Add("ActiveSync.Proxy", ServerComponentEnum.ActiveSyncProxy);
			dictionary.Add("Autodiscover.Proxy", ServerComponentEnum.AutoDiscoverProxy);
			dictionary.Add("XRop.Proxy", ServerComponentEnum.XropProxy);
			dictionary.Add("RWS.Proxy", ServerComponentEnum.RwsProxy);
			dictionary.Add("RPS.Proxy", ServerComponentEnum.RpsProxy);
			dictionary.Add("PushNotifications.Proxy", ServerComponentEnum.PushNotificationsProxy);
			Dictionary<string, ServerComponentEnum> dictionary2 = new Dictionary<string, ServerComponentEnum>(StringComparer.InvariantCultureIgnoreCase);
			foreach (string key in dictionary.Keys)
			{
				dictionary2[key] = dictionary[key];
			}
			if (flag)
			{
				Dictionary<string, ServerComponentEnum> dictionary3 = ExchangeComponent.RetrieveHttpProxyServerComponentOverride();
				foreach (string key2 in dictionary3.Keys)
				{
					if (dictionary2.ContainsKey(key2))
					{
						dictionary2[key2] = dictionary3[key2];
					}
				}
			}
			ExchangeComponent.OwaProxy = new Component("OWA.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["OWA.Proxy"]);
			ExchangeComponent.OutlookProxy = new Component("Outlook.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["Outlook.Proxy"]);
			ExchangeComponent.OutlookMapiProxy = new Component("OutlookMapiHttp.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["OutlookMapiHttp.Proxy"]);
			ExchangeComponent.EwsProxy = new Component("EWS.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["EWS.Proxy"]);
			ExchangeComponent.ActiveSyncProxy = new Component("ActiveSync.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["ActiveSync.Proxy"]);
			ExchangeComponent.AutodiscoverProxy = new Component("Autodiscover.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["Autodiscover.Proxy"]);
			ExchangeComponent.PushNotificationsProxy = new Component("PushNotifications.Proxy", HealthGroup.ServiceComponents, "Push Notification Services", "Exchange", ManagedAvailabilityPriority.Low, dictionary2["PushNotifications.Proxy"]);
			ExchangeComponent.EcpProxy = new Component("ECP.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.High, dictionary2["ECP.Proxy"]);
			ExchangeComponent.RpsProxy = new Component("RPS.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["RPS.Proxy"]);
			ExchangeComponent.OabProxy = new Component("OAB.Proxy", HealthGroup.ServiceComponents, "People911", "Exchange", ManagedAvailabilityPriority.Low, dictionary2["OAB.Proxy"]);
			ExchangeComponent.RwsProxy = new Component("RWS.Proxy", HealthGroup.ServiceComponents, "Reporting Web Service", "Exchange", ManagedAvailabilityPriority.Low, dictionary2["RWS.Proxy"]);
			ExchangeComponent.XropProxy = new Component("XRop.Proxy", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, dictionary2["XRop.Proxy"]);
			ExchangeComponent.WellKnownComponents = (from field in typeof(ExchangeComponent).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			where field.FieldType == typeof(Component)
			select (Component)field.GetValue(null)).ToDictionary((Component component) => component.Name, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000049F0 File Offset: 0x00002BF0
		private static Dictionary<string, ServerComponentEnum> RetrieveHttpProxyServerComponentOverride()
		{
			Dictionary<string, ServerComponentEnum> dictionary = new Dictionary<string, ServerComponentEnum>(StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrEmpty(Settings.HttpProxyAvailabilityGroup))
			{
				string[] array = Settings.HttpProxyAvailabilityGroup.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						':'
					}, StringSplitOptions.RemoveEmptyEntries);
					if (array3 != null && array3.Length == 2)
					{
						dictionary.Add(array3[0], (ServerComponentEnum)Enum.Parse(typeof(ServerComponentEnum), array3[1], true));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04000003 RID: 3
		private const string RecoveryActionComponentName = "RecoveryAction";

		// Token: 0x04000004 RID: 4
		private const string OwaProxyComponentName = "OWA.Proxy";

		// Token: 0x04000005 RID: 5
		private const string EcpProxyComponentName = "ECP.Proxy";

		// Token: 0x04000006 RID: 6
		private const string OutlookProxyComponentName = "Outlook.Proxy";

		// Token: 0x04000007 RID: 7
		private const string OutlookMapiProxyComponentName = "OutlookMapiHttp.Proxy";

		// Token: 0x04000008 RID: 8
		private const string EwsProxyComponentName = "EWS.Proxy";

		// Token: 0x04000009 RID: 9
		private const string ActiveSyncProxyComponentName = "ActiveSync.Proxy";

		// Token: 0x0400000A RID: 10
		private const string AutodiscoverProxyComponentName = "Autodiscover.Proxy";

		// Token: 0x0400000B RID: 11
		private const string XropProxyComponentName = "XRop.Proxy";

		// Token: 0x0400000C RID: 12
		private const string RwsProxyComponentName = "RWS.Proxy";

		// Token: 0x0400000D RID: 13
		private const string RpsProxyComponentName = "RPS.Proxy";

		// Token: 0x0400000E RID: 14
		private const string PushNotificationsProxyComponentName = "PushNotifications.Proxy";

		// Token: 0x0400000F RID: 15
		private const string OabProxyComponentName = "OAB.Proxy";

		// Token: 0x04000010 RID: 16
		internal static readonly Component RecoveryAction = new Component("RecoveryAction", HealthGroup.ServiceComponents, "Monitoring", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000011 RID: 17
		internal static readonly Component Cafe = new Component("ClientAccess.Proxy", HealthGroup.ServiceComponents, "Client Access Front End", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000012 RID: 18
		internal static readonly Component Owa = new Component("OWA", HealthGroup.CustomerTouchPoints, "OWA", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000013 RID: 19
		internal static readonly Component OwaAttachments = new Component("OWA.Attachments", HealthGroup.CustomerTouchPoints, "OWA Attachments team", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000014 RID: 20
		internal static readonly Component OwaWebServices = new Component("OWA.WebServices", HealthGroup.CustomerTouchPoints, "Web Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000015 RID: 21
		internal static readonly Component OwaSuiteServices = new Component("OWA.SuiteServices", HealthGroup.CustomerTouchPoints, "Suite UX Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000016 RID: 22
		internal static readonly Component OwaProxy;

		// Token: 0x04000017 RID: 23
		internal static readonly Component OwaProtocol = new Component("OWA.Protocol", HealthGroup.ServiceComponents, "OWA", "Exchange", ManagedAvailabilityPriority.Critical);

		// Token: 0x04000018 RID: 24
		internal static readonly Component OwaDependency = new Component("OWA.Protocol.Dep", HealthGroup.KeyDependencies, "OWA", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000019 RID: 25
		internal static readonly Component Outlook = new Component("Outlook", HealthGroup.CustomerTouchPoints, "MOMT and XSO", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400001A RID: 26
		internal static readonly Component OutlookMapiHttp = new Component("OutlookMapiHttp", HealthGroup.CustomerTouchPoints, "MOMT and XSO", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400001B RID: 27
		internal static readonly Component HxServiceMail = new Component("HxService.Mail", HealthGroup.ServiceComponents, "HxService-Mail", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400001C RID: 28
		internal static readonly Component HxServiceCalendar = new Component("HxService.Calendar", HealthGroup.ServiceComponents, "HxService-Calendar", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400001D RID: 29
		internal static readonly Component OutlookProtocol = new Component("Outlook.Protocol", HealthGroup.ServiceComponents, ExchangeComponent.Outlook.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Critical);

		// Token: 0x0400001E RID: 30
		internal static readonly Component OutlookMapiHttpProtocol = new Component("OutlookMapiHttp.Protocol", HealthGroup.ServiceComponents, ExchangeComponent.Outlook.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Critical);

		// Token: 0x0400001F RID: 31
		internal static readonly Component UnifiedMailbox = new Component("UnifiedMailbox", HealthGroup.CustomerTouchPoints, "Web Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000020 RID: 32
		internal static readonly Component OutlookProxy;

		// Token: 0x04000021 RID: 33
		internal static readonly Component OutlookMapiProxy;

		// Token: 0x04000022 RID: 34
		internal static readonly Component Calendaring = new Component("Calendaring", HealthGroup.CustomerTouchPoints, "Calendaring/Sharing", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000023 RID: 35
		internal static readonly Component FreeBusy = new Component("FreeBusy", HealthGroup.ServiceComponents, "Calendaring/Sharing", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000024 RID: 36
		internal static readonly Component Elc = new Component("ELC", HealthGroup.ServiceComponents, "MRM/Archive", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000025 RID: 37
		internal static readonly Component Pop = new Component("POP", HealthGroup.CustomerTouchPoints, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000026 RID: 38
		internal static readonly Component PopProtocol = new Component("POP.Protocol", HealthGroup.ServiceComponents, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000027 RID: 39
		internal static readonly Component PopProxy = new Component("POP.Proxy", HealthGroup.ServiceComponents, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.PopProxy);

		// Token: 0x04000028 RID: 40
		internal static readonly Component PushNotifications = new Component("PushNotifications", HealthGroup.CustomerTouchPoints, "Push Notification Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000029 RID: 41
		internal static readonly Component PushNotificationsProxy;

		// Token: 0x0400002A RID: 42
		internal static readonly Component PushNotificationsProtocol = new Component("PushNotifications.Protocol", HealthGroup.ServiceComponents, "Push Notification Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400002B RID: 43
		internal static readonly Component Imap = new Component("IMAP", HealthGroup.CustomerTouchPoints, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400002C RID: 44
		internal static readonly Component ImapProtocol = new Component("IMAP.Protocol", HealthGroup.ServiceComponents, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400002D RID: 45
		internal static readonly Component ImapProxy = new Component("IMAP.Proxy", HealthGroup.ServiceComponents, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.ImapProxy);

		// Token: 0x0400002E RID: 46
		internal static readonly Component Eas = new Component("EAS", HealthGroup.CustomerTouchPoints, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400002F RID: 47
		internal static readonly Component ActiveSyncProxy;

		// Token: 0x04000030 RID: 48
		internal static readonly Component SharedCache = new Component("SharedCache", HealthGroup.ServiceComponents, ExchangeComponent.Cafe.EscalationTeam, "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.SharedCache);

		// Token: 0x04000031 RID: 49
		internal static readonly Component ActiveSync = new Component("ActiveSync", HealthGroup.CustomerTouchPoints, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000032 RID: 50
		internal static readonly Component ActiveSyncProtocol = new Component("ActiveSync.Protocol", HealthGroup.ServiceComponents, "Pop3, Imap4, ActiveSync", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x04000033 RID: 51
		internal static readonly Component Ews = new Component("EWS", HealthGroup.CustomerTouchPoints, "Web Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000034 RID: 52
		internal static readonly Component OnlineMeeting = new Component("OnlineMeeting", HealthGroup.CustomerTouchPoints, "Calendaring/Sharing", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000035 RID: 53
		internal static readonly Component EwsProtocol = new Component("EWS.Protocol", HealthGroup.ServiceComponents, "Web Services", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x04000036 RID: 54
		internal static readonly Component EwsProxy;

		// Token: 0x04000037 RID: 55
		internal static readonly Component Autodiscover = new Component("Autodiscover", HealthGroup.CustomerTouchPoints, "Web Services", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000038 RID: 56
		internal static readonly Component AutodiscoverProtocol = new Component("Autodiscover.Protocol", HealthGroup.ServiceComponents, "Web Services", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x04000039 RID: 57
		internal static readonly Component AutodiscoverProxy;

		// Token: 0x0400003A RID: 58
		internal static readonly Component UMCallRouter = new Component("UM.CallRouter", HealthGroup.ServiceComponents, "Unified Messaging", "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.UMCallRouter);

		// Token: 0x0400003B RID: 59
		internal static readonly Component UMProtocol = new Component("UM.Protocol", HealthGroup.ServiceComponents, "Unified Messaging", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400003C RID: 60
		internal static readonly Component UM = new Component("UM", HealthGroup.CustomerTouchPoints, "Unified Messaging", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400003D RID: 61
		internal static readonly Component Mailflow = new Component("Mailflow", HealthGroup.CustomerTouchPoints, "E15Transport", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400003E RID: 62
		internal static readonly Component OutsideInMailflow = new Component("Outside-In Mailflow", HealthGroup.CustomerTouchPoints, "E15Transport", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400003F RID: 63
		internal static readonly Component Provisioning = new Component("Provisioning", HealthGroup.CustomerTouchPoints, "Provisioning", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000040 RID: 64
		internal static readonly Component ForwardSync = new Component("ForwardSync", HealthGroup.CustomerTouchPoints, "Provisioning", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000041 RID: 65
		internal static readonly Component Umc = new Component("UMC", HealthGroup.CustomerTouchPoints, "Monitoring", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000042 RID: 66
		internal static readonly Component Ecp = new Component("ECP", HealthGroup.CustomerTouchPoints, "ECP", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000043 RID: 67
		internal static readonly Component InstantMessaging = new Component("Owa.InstantMessaging", HealthGroup.KeyDependencies, "OWA", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000044 RID: 68
		internal static readonly Component EcpProxy;

		// Token: 0x04000045 RID: 69
		internal static readonly Component Osp = new Component("OSP", HealthGroup.CustomerTouchPoints, "OSP", "OBD", ManagedAvailabilityPriority.Low);

		// Token: 0x04000046 RID: 70
		internal static readonly Component SmartAlerts = new Component("SmartAlerts", HealthGroup.ServiceComponents, "SmartAlerts Team", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000047 RID: 71
		internal static readonly Component Rps = new Component("RPS", HealthGroup.CustomerTouchPoints, "Cmdlet Infra/Recipients/RPS", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000048 RID: 72
		internal static readonly Component RpsProtocol = new Component("RPS.Protocol", HealthGroup.ServiceComponents, "Cmdlet Infra/Recipients/RPS", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000049 RID: 73
		internal static readonly Component RpsProxy;

		// Token: 0x0400004A RID: 74
		internal static readonly Component Store = new Component("Store", HealthGroup.ServiceComponents, "Store", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400004B RID: 75
		internal static readonly Component RemoteStore = new Component("RemoteStore", HealthGroup.ServiceComponents, "Store", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400004C RID: 76
		internal static readonly Component MailboxSpace = new Component("MailboxSpace", HealthGroup.ServerResources, "Store", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400004D RID: 77
		internal static readonly Component EventAssistants = new Component("EventAssistants", HealthGroup.ServiceComponents, "Store", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400004E RID: 78
		internal static readonly Component Transport = new Component("Transport", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.HubTransport);

		// Token: 0x0400004F RID: 79
		internal static readonly Component Eds = new Component("EDS", HealthGroup.ServiceComponents, "Performance", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000050 RID: 80
		internal static readonly Component Pum = new Component("Pum", HealthGroup.ServiceComponents, "Performance", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000051 RID: 81
		internal static readonly Component DataProtection = new Component("DataProtection", HealthGroup.ServiceComponents, "High Availability", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x04000052 RID: 82
		internal static readonly Component BitlockerDeployment = new Component("BitlockerDeployment", HealthGroup.ServiceComponents, "High Availability", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x04000053 RID: 83
		internal static readonly Component Clustering = new Component("Clustering", HealthGroup.ServerResources, "High Availability", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000054 RID: 84
		internal static readonly Component FEP = new Component("FEP", HealthGroup.ServiceComponents, "Security", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000055 RID: 85
		internal static readonly Component DiskController = new Component("DiskController", HealthGroup.ServerResources, "High Availability", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000056 RID: 86
		internal static readonly Component Mrs = new Component("MRS", HealthGroup.ServiceComponents, "Mailbox Migration", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000057 RID: 87
		internal static readonly Component Oab = new Component("OAB", HealthGroup.ServiceComponents, "People911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000058 RID: 88
		internal static readonly Component OabProxy;

		// Token: 0x04000059 RID: 89
		internal static readonly Component Search = new Component("Search", HealthGroup.ServiceComponents, "Search", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x0400005A RID: 90
		internal static readonly Component Inference = new Component("Inference", HealthGroup.ServiceComponents, "Inference", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400005B RID: 91
		internal static readonly Component Monitoring = new Component("Monitoring", HealthGroup.ServiceComponents, "Monitoring", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400005C RID: 92
		internal static readonly Component RemoteMonitoring = new Component("RemoteMonitoring", HealthGroup.ServiceComponents, "Monitoring", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400005D RID: 93
		internal static readonly Component LighthouseMonitoring = new Component("LighthouseMonitoring", HealthGroup.ServiceComponents, "Monitoring", "Exchange", ManagedAvailabilityPriority.Normal, ServerComponentEnum.Monitoring);

		// Token: 0x0400005E RID: 94
		internal static readonly Component Security = new Component("Security", HealthGroup.ServiceComponents, "Security", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400005F RID: 95
		internal static readonly Component ProcessIsolation = new Component("ProcessIsolation", HealthGroup.ServiceComponents, "Performance", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000060 RID: 96
		internal static readonly Component UserThrottling = new Component("UserThrottling", HealthGroup.ServiceComponents, "Workload Management", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000061 RID: 97
		internal static readonly Component Datamining = new Component("Datamining", HealthGroup.ServiceComponents, "Optics", "OBD", ManagedAvailabilityPriority.Low);

		// Token: 0x04000062 RID: 98
		internal static readonly Component ServiceAvailability = new Component("ServiceAvailability", HealthGroup.ServiceComponents, "Service Availability", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000063 RID: 99
		internal static readonly Component RedAlert = new Component("RedAlert", HealthGroup.ServiceComponents, "Service Availability", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000064 RID: 100
		internal static readonly Component STX = new Component("STX", HealthGroup.ServiceComponents, "Service Availability", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000065 RID: 101
		internal static readonly Component FfoWebService = new Component("FfoWebService", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000066 RID: 102
		internal static readonly Component FfoBackground = new Component("FfoBackground", HealthGroup.ServiceComponents, "Service Automation & Monitoring", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000067 RID: 103
		internal static readonly Component AnalystAlertingUntriagedFpExceededThreshold = new Component("AnalystAlertingUntriagedFpExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000068 RID: 104
		internal static readonly Component AnalystAlertingUntriagedFnExceededThreshold = new Component("AnalystAlertingUntriagedFnExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000069 RID: 105
		internal static readonly Component AnalystAlertingUntriagedSenExceededThreshold = new Component("AnalystAlertingUntriagedSenExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400006A RID: 106
		internal static readonly Component AnalystAlertingUntriagedSewrExceededThreshold = new Component("AnalystAlertingUntriagedSewrExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400006B RID: 107
		internal static readonly Component AnalystAlertingUntriagedFingerprintsExceededThreshold = new Component("AnalystAlertingUntriagedFingerprintsExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400006C RID: 108
		internal static readonly Component AnalystAlertingUntriagedFpRulesClusterThreshold = new Component("AnalystAlertingUntriagedFpRulesClusterThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400006D RID: 109
		internal static readonly Component AnalystAlertingTopSubjectsExceededThreshold = new Component("AnalystAlertingTopSubjectsExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400006E RID: 110
		internal static readonly Component AnalystAlertingFpUriRulesExceededThreshold = new Component("AnalystAlertingFpUriRulesExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400006F RID: 111
		internal static readonly Component AnalystAlertingFpSpamRulesExceededThreshold = new Component("AnalystAlertingFpSpamRulesExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000070 RID: 112
		internal static readonly Component AnalystAlertingSenderIpFpExceededThreshold = new Component("AnalystAlertingSenderIpFpExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000071 RID: 113
		internal static readonly Component AnalystAlertingSenderIpFnExceededThreshold = new Component("AnalystAlertingSenderIpFnExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000072 RID: 114
		internal static readonly Component AnalystAlertingUntriagedSubmitterExceededThreshold = new Component("AnalystAlertingUntriagedSubmitterExceededThreshold", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000073 RID: 115
		internal static readonly Component AnalystAlertingNoFpTraffic = new Component("AnalystAlertingNoFpTraffic", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000074 RID: 116
		internal static readonly Component AnalystAlertingNoFnTraffic = new Component("AnalystAlertingNoFnTraffic", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000075 RID: 117
		internal static readonly Component AnalystAlertingNoSenTraffic = new Component("AnalystAlertingNoSenTraffic", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000076 RID: 118
		internal static readonly Component AnalystAlertingNoSewrTraffic = new Component("AnalystAlertingNoSewrTraffic", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000077 RID: 119
		internal static readonly Component AnalystAlertingNoHpTraffic = new Component("AnalystAlertingNoHpTraffic", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000078 RID: 120
		internal static readonly Component FfoBackgroundFwdSync = new Component("FfoBackgroundFwdSync", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000079 RID: 121
		internal static readonly Component FfoBackgroundIpListGen = new Component("FfoBackgroundIpListGen", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400007A RID: 122
		internal static readonly Component FfoBackgroundUriPuller = new Component("FfoBackgroundUriPuller", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400007B RID: 123
		internal static readonly Component FfoBackgroundAntiSpamDataPumper = new Component("FfoBackgroundAntiSpamDataPumper", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400007C RID: 124
		internal static readonly Component FfoBackgroundAntiSpamScheduler = new Component("FfoBackgroundAntiSpamScheduler", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400007D RID: 125
		internal static readonly Component FfoBackgroundOutSpamAlert = new Component("FfoBackgroundOutSpamAlert", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400007E RID: 126
		internal static readonly Component FfoBackgroundRulesPub = new Component("FfoBackgroundRulesPub", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400007F RID: 127
		internal static readonly Component FfoBackgroundPackageManager = new Component("FfoBackgroundPackageManager", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000080 RID: 128
		internal static readonly Component FfoBackgroundInterServiceSpamDataSync = new Component("FfoBackgroundInterServiceSpamDataSync", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000081 RID: 129
		internal static readonly Component FfoBackgroundUriGen = new Component("FfoBackgroundUriGen", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000082 RID: 130
		internal static readonly Component FfoBackgroundAsyncQueue = new Component("FfoBackgroundAsyncQueue", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000083 RID: 131
		internal static readonly Component FfoBackgroundQuarMgr = new Component("FfoBackgroundQuarMgr", HealthGroup.ServiceComponents, "UI and Manageability", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000084 RID: 132
		internal static readonly Component FfoBackgroundDataPump = new Component("FfoBackgroundDataPump", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000085 RID: 133
		internal static readonly Component FfoBackgroundJobSched = new Component("FfoBackgroundJobSched", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000086 RID: 134
		internal static readonly Component FfoBackgroundLatencyBucket = new Component("FfoBackgroundLatencyBucket", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000087 RID: 135
		internal static readonly Component FfoBackgroundQueueDigestMon = new Component("FfoBackgroundQueueDigestMon", HealthGroup.ServiceComponents, "E15Transport", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000088 RID: 136
		internal static readonly Component FfoBackgroundCacheGen = new Component("FfoBackgroundCacheGen", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000089 RID: 137
		internal static readonly Component FfoBackgroundIpRepCheck = new Component("FfoBackgroundIpRepCheck", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400008A RID: 138
		internal static readonly Component FfoBackgroundRulesDataBlobGen = new Component("FfoBackgroundRulesDataBlobGen", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400008B RID: 139
		internal static readonly Component FfoDatabase = new Component("FfoDatabase", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400008C RID: 140
		internal static readonly Component DiskSpace = new Component("DiskSpace", HealthGroup.ServerResources, "Ops support", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400008D RID: 141
		internal static readonly Component Cpu = new Component("CPU", HealthGroup.ServerResources, "Performance", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400008E RID: 142
		internal static readonly Component Memory = new Component("Memory", HealthGroup.ServerResources, "Performance", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400008F RID: 143
		internal static readonly Component AD = new Component("AD", HealthGroup.KeyDependencies, "Directory and LiveId Auth", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000090 RID: 144
		internal static readonly Component Network = new Component("Network", HealthGroup.KeyDependencies, "Networking", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000091 RID: 145
		internal static readonly Component LiveId = new Component("LiveId", HealthGroup.KeyDependencies, "Directory and LiveId Auth", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000092 RID: 146
		internal static readonly Component OrgId = new Component("OrgId", HealthGroup.KeyDependencies, "Directory and LiveId Auth", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000093 RID: 147
		internal static readonly Component Mserv = new Component("Mserv", HealthGroup.KeyDependencies, "Directory and LiveId Auth", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000094 RID: 148
		internal static readonly Component AntiSpam = new Component("AntiSpam", HealthGroup.ServiceComponents, "AntiSpam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000095 RID: 149
		internal static readonly Component Capacity = new Component("Capacity", HealthGroup.ServiceComponents, "Capacity", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000096 RID: 150
		internal static readonly Component Wascl = new Component("Wascl", HealthGroup.ServiceComponents, "Wascl", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000097 RID: 151
		internal static readonly Component Classification = new Component("Classification", HealthGroup.ServiceComponents, "Classification", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000098 RID: 152
		internal static readonly Component CentralAdmin = new Component("CentralAdmin", HealthGroup.ServiceComponents, "Central Admin", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000099 RID: 153
		internal static readonly Component Dal = new Component("DAL", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400009A RID: 154
		internal static readonly Component Deployment = new Component("Deployment", HealthGroup.ServiceComponents, "Deployment", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400009B RID: 155
		internal static readonly Component Dns = new Component("DNS", HealthGroup.CustomerTouchPoints, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400009C RID: 156
		internal static readonly Component Fips = new Component("FIPS", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400009D RID: 157
		internal static readonly Component Antimalware = new Component("Antimalware", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400009E RID: 158
		internal static readonly Component AMEUS = new Component("AMEUS", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400009F RID: 159
		internal static readonly Component AMFailedUpdate = new Component("AMFailedUpdate", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A0 RID: 160
		internal static readonly Component RusPublisherWeb = new Component("RusPublisherWeb", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A1 RID: 161
		internal static readonly Component EngineUpdatePublisher = new Component("EngineUpdatePublisher", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A2 RID: 162
		internal static readonly Component GenericRusClient = new Component("GenericRusClient", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A3 RID: 163
		internal static readonly Component GenericRusServer = new Component("GenericRusServer", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A4 RID: 164
		internal static readonly Component GenericUpdateService = new Component("GenericUpdateService", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A5 RID: 165
		internal static readonly Component AMService = new Component("AMService", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A6 RID: 166
		internal static readonly Component AMFMSService = new Component("AMFMSService", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A7 RID: 167
		internal static readonly Component AMUnifiedContentError = new Component("AMUnifiedContentError", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A8 RID: 168
		internal static readonly Component AMScannerCrash = new Component("AMScannerCrash", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000A9 RID: 169
		internal static readonly Component AMScanners = new Component("AMScanners", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000AA RID: 170
		internal static readonly Component AMScanTimeout = new Component("AMScanTimeout", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000AB RID: 171
		internal static readonly Component AMMessagesDeferred = new Component("AMMessagesDeferred", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000AC RID: 172
		internal static readonly Component AMScanError = new Component("AMScanError", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000AD RID: 173
		internal static readonly Component AMSMTPProbe = new Component("AMSMTPProbe", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000AE RID: 174
		internal static readonly Component AMADError = new Component("AMADError", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000AF RID: 175
		internal static readonly Component AMTenantConfigError = new Component("AMTenantConfigError", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B0 RID: 176
		internal static readonly Component FfoRps = new Component("FfoRps", HealthGroup.ServiceComponents, "FIPS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B1 RID: 177
		internal static readonly Component FfoRws = new Component("FfoRws", HealthGroup.ServiceComponents, "UI and Manageability", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B2 RID: 178
		internal static readonly Component FfoMobileDevices = new Component("FfoMobileDevices", HealthGroup.ServiceComponents, "Mobile Devices", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B3 RID: 179
		internal static readonly Component FfoDatamining = new Component("FfoDatamining", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B4 RID: 180
		internal static readonly Component FfoDatamining_UrgentInTraining = new Component("FfoDatamining_UrgentInTraining", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B5 RID: 181
		internal static readonly Component FfoDatamining_Urgent = new Component("FfoDatamining_Urgent", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B6 RID: 182
		internal static readonly Component EmailManagement = new Component("EmailManagement", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B7 RID: 183
		internal static readonly Component FfoMonitoring = new Component("FfoMonitoring", HealthGroup.ServiceComponents, "Service Automation & Monitoring", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B8 RID: 184
		internal static readonly Component FfoProvisioning = new Component("FfoProvisioning", HealthGroup.ServiceComponents, "Directory and Database Infrastructure", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000B9 RID: 185
		internal static readonly Component FfoQuarantine = new Component("FfoQuarantine", HealthGroup.ServiceComponents, "UI and Manageability", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000BA RID: 186
		internal static readonly Component FfoTransport = new Component("FfoTransport", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x040000BB RID: 187
		internal static readonly Component FfoWebstore = new Component("FfoWebstore", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000BC RID: 188
		internal static readonly Component FfoUmc = new Component("FfoUmc", HealthGroup.CustomerTouchPoints, "UI and Manageability", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000BD RID: 189
		internal static readonly Component FfoUcc = new Component("FfoUcc", HealthGroup.CustomerTouchPoints, "UI and Manageability", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000BE RID: 190
		internal static readonly Component Gls = new Component("GLS", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000BF RID: 191
		internal static readonly Component Gls_Local_Urgent = new Component("GLS_Local_Urgent", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C0 RID: 192
		internal static readonly Component Gls_Local_UrgentInTraining = new Component("GLS_Local_UrgentInTraining", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C1 RID: 193
		internal static readonly Component Gls_External_Urgent = new Component("GLS_External_Urgent", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C2 RID: 194
		internal static readonly Component Gls_External_UrgentInTraining = new Component("GLS_External_UrgentInTraining", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C3 RID: 195
		internal static readonly Component Smtp = new Component("SMTP", HealthGroup.CustomerTouchPoints, "E15Transport", "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.HubTransport);

		// Token: 0x040000C4 RID: 196
		internal static readonly Component TransportSync = new Component("TransportSync", HealthGroup.ServiceComponents, "Mailbox Migration", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x040000C5 RID: 197
		internal static readonly Component HDPhoto = new Component("HDPhoto", HealthGroup.ServiceComponents, "People911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C6 RID: 198
		internal static readonly Component PeopleConnect = new Component("PeopleConnect", HealthGroup.ServiceComponents, "People911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C7 RID: 199
		internal static readonly Component Rws = new Component("RWS", HealthGroup.CustomerTouchPoints, "Reporting Web Service", "OBD", ManagedAvailabilityPriority.Low);

		// Token: 0x040000C8 RID: 200
		internal static readonly Component RwsProxy;

		// Token: 0x040000C9 RID: 201
		internal static readonly Component Compliance = new Component("Compliance", HealthGroup.CustomerTouchPoints, "MRM/Archive", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000CA RID: 202
		internal static readonly Component ExtendedReportWeb = new Component("ExtendedReportWeb", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000CB RID: 203
		internal static readonly Component MessageTracing = new Component("MessageTracing", HealthGroup.ServiceComponents, "Intelligence and Reporting", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000CC RID: 204
		internal static readonly Component MSExchangeCertificateDeployment = new Component("MSExchangeCertificateDeployment", HealthGroup.ServiceComponents, "MSExchangeCertificateDeployment", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000CD RID: 205
		internal static readonly Component Rms = new Component("RMS", HealthGroup.ServiceComponents, "RMS", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000CE RID: 206
		internal static readonly Component MailboxMigration = new Component("MailboxMigration", HealthGroup.ServiceComponents, "Mailbox Migration", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x040000CF RID: 207
		internal static readonly Component FfoSelfRecoveryFx = new Component("FfoSelfRecoveryFx", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D0 RID: 208
		internal static readonly Component FfoRaaNetworkValidator = new Component("FfoRaaNetworkValidator", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D1 RID: 209
		internal static readonly Component FfoRaaService = new Component("FfoRaaService", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D2 RID: 210
		internal static readonly Component FfoWebserviceF5Threshold = new Component("FfoWebserviceF5Threshold", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D3 RID: 211
		internal static readonly Component FfoHubTransportF5Threshold = new Component("FfoHubTransportF5Threshold", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D4 RID: 212
		internal static readonly Component FfoFrontendTransportF5Threshold = new Component("FfoFrontendTransportF5Threshold", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D5 RID: 213
		internal static readonly Component FfoDomainNameServerF5Threshold = new Component("FfoDomainNameServerF5Threshold", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D6 RID: 214
		internal static readonly Component StreamingOptics = new Component("StreamingOptics", HealthGroup.ServiceComponents, "Antispam", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D7 RID: 215
		internal static readonly Component SyslogListener = new Component("SyslogListener", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D8 RID: 216
		internal static readonly Component SyslogListenerServiceError = new Component("SyslogListenerServiceError", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000D9 RID: 217
		internal static readonly Component SyslogListenerServiceParseError = new Component("SyslogListenerServiceParseError", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000DA RID: 218
		internal static readonly Component SyslogListener_F5AuditLoginFailure = new Component("SyslogListener_F5AuditLoginFailure", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000DB RID: 219
		internal static readonly Component SyslogListener_F5LTMFailover = new Component("SyslogListener_F5LTMFailover", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000DC RID: 220
		internal static readonly Component SyslogListener_F5DriveError = new Component("SyslogListener_F5DriveError", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000DD RID: 221
		internal static readonly Component SyslogListener_F5GTMWideIPDown = new Component("SyslogListener_F5GTMWideIPDown", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000DE RID: 222
		internal static readonly Component SyslogListener_F5GTMVirtualServerDown = new Component("SyslogListener_F5GTMVirtualServerDown", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000DF RID: 223
		internal static readonly Component SyslogListener_F5GTMPoolDown = new Component("SyslogListener_F5GTMPoolDown", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E0 RID: 224
		internal static readonly Component SyslogListener_F5GTMPoolMemberDown = new Component("SyslogListener_F5GTMPoolMemberDown", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E1 RID: 225
		internal static readonly Component SyslogListener_F5LTMPoolDown = new Component("SyslogListener_F5LTMPoolDown", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E2 RID: 226
		internal static readonly Component SyslogListener_F5ExcessiveResets = new Component("SyslogListener_F5ExcessiveResets", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E3 RID: 227
		internal static readonly Component SyslogListener_F5LTMExcessiveMemberReselects = new Component("SyslogListener_F5LTMExcessiveMemberReselects", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E4 RID: 228
		internal static readonly Component SyslogListener_F5Big3dCertErrors = new Component("SyslogListener_F5Big3dCertErrors", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E5 RID: 229
		internal static readonly Component SyslogListener_F5PortExhaustion = new Component("SyslogListener_F5PortExhaustion", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E6 RID: 230
		internal static readonly Component SyslogListener_F5EmergencyEvent = new Component("SyslogListener_F5EmergencyEvent", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E7 RID: 231
		internal static readonly Component SyslogListener_F5NTPError = new Component("SyslogListener_F5NTPError", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E8 RID: 232
		internal static readonly Component PowershellDataProvider = new Component("PowershellDataProvider", HealthGroup.ServiceComponents, "PowershellDataProvider", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000E9 RID: 233
		internal static readonly Component AsyncQueueDaemon = new Component("AsyncQueueDaemon", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000EA RID: 234
		internal static readonly Component AsyncQueue = new Component("AsyncQueue", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000EB RID: 235
		internal static readonly Component UnifiedGroups = new Component("UnifiedGroups", HealthGroup.CustomerTouchPoints, "Groups911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000EC RID: 236
		internal static readonly Component PublicFolders = new Component("PublicFolders", HealthGroup.CustomerTouchPoints, "Groups911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000ED RID: 237
		internal static readonly Component SiteMailbox = new Component("SiteMailbox", HealthGroup.CustomerTouchPoints, "Groups911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000EE RID: 238
		internal static readonly Component NotificationsBroker = new Component("NotificationsBroker", HealthGroup.CustomerTouchPoints, "Groups911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000EF RID: 239
		internal static readonly Component HubTransport = new Component("HubTransport", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.High, ServerComponentEnum.HubTransport);

		// Token: 0x040000F0 RID: 240
		internal static readonly Component FrontendTransport = new Component("FrontendTransport", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.Low, ServerComponentEnum.FrontendTransport);

		// Token: 0x040000F1 RID: 241
		internal static readonly Component EdgeTransport = new Component("EdgeTransport", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.High, ServerComponentEnum.EdgeTransport);

		// Token: 0x040000F2 RID: 242
		internal static readonly Component MailboxTransport = new Component("MailboxTransport", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x040000F3 RID: 243
		internal static readonly Component TransportExtensibility = new Component("TransportExtensibility", HealthGroup.ServiceComponents, "E15Transport", "Exchange", ManagedAvailabilityPriority.High);

		// Token: 0x040000F4 RID: 244
		internal static readonly Component Odc = new Component("ODC", HealthGroup.ServiceComponents, "ODC Office 15 Alerts", "ODC", ManagedAvailabilityPriority.Low);

		// Token: 0x040000F5 RID: 245
		internal static readonly Component FFOMigration1415 = new Component("FFOMigration1415", HealthGroup.ServiceComponents, "GLS", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000F6 RID: 246
		internal static readonly Component O365SuiteExp = new Component("O365SuiteExp", HealthGroup.ServiceComponents, "OSE on-call", "OSE", ManagedAvailabilityPriority.Low);

		// Token: 0x040000F7 RID: 247
		internal static readonly Component CustomerConnection = new Component("CustomerConnection", HealthGroup.ServiceComponents, "CC Service Management on-call", "OSE", ManagedAvailabilityPriority.Low);

		// Token: 0x040000F8 RID: 248
		internal static readonly Component ExchangeDatacenterDeployment = new Component("ExDcDeployment", HealthGroup.ServiceComponents, "Deployment", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x040000F9 RID: 249
		internal static readonly Component FfoDeployment = new Component("FfoDeployment", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000FA RID: 250
		internal static readonly Component FfoCentralAdmin = new Component("FfoCentralAdmin", HealthGroup.ServiceComponents, "Deployment and Configuration Management", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000FB RID: 251
		internal static readonly Component FfoGtmValidation = new Component("FfoGtmValidation", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000FC RID: 252
		internal static readonly Component FfoLoadBalancerValidation = new Component("FfoLoadBalancerValidation", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000FD RID: 253
		internal static readonly Component FfoFailureStatisticValidation = new Component("FfoFailureStatisticValidation", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000FE RID: 254
		internal static readonly Component FfoGatewayConnectivityValidation = new Component("FfoGatewayConnectivityValidation", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x040000FF RID: 255
		internal static readonly Component FfoVipConnectivityValidation = new Component("FfoVipConnectivityValidation", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000100 RID: 256
		internal static readonly Component FfoLtmOpticsProvider = new Component("FfoLtmOpticsProvider", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000101 RID: 257
		internal static readonly Component FfoGtmOpticsProvider = new Component("FfoGtmOpticsProvider", HealthGroup.ServiceComponents, "Network", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000102 RID: 258
		internal static readonly Component Psws = new Component("Psws", HealthGroup.CustomerTouchPoints, "Cmdlet Infra/Recipients/RPS", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000103 RID: 259
		internal static readonly Component XropProxy;

		// Token: 0x04000104 RID: 260
		internal static readonly Component Places = new Component("Places", HealthGroup.CustomerTouchPoints, "Calendaring/Sharing", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000105 RID: 261
		internal static readonly Component EdiscoveryProtocol = new Component("Ediscovery.Protocol", HealthGroup.ServiceComponents, "EDiscovery and Auditing", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000106 RID: 262
		internal static readonly Component UnifiedComplianceSourceValidation = new Component("UnifiedComplianceSourceValidation", HealthGroup.ServiceComponents, "EDiscovery and Auditing", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x04000107 RID: 263
		internal static readonly Component Auditing = new Component("Auditing", HealthGroup.ServiceComponents, "EDiscovery and Auditing", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000108 RID: 264
		internal static readonly Component DLExpansion = new Component("DLExpansion", HealthGroup.ServiceComponents, "MRM/Archive", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000109 RID: 265
		internal static readonly Component JournalArchive = new Component("JournalArchive", HealthGroup.ServiceComponents, "MRM/Archive", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400010A RID: 266
		internal static readonly Component BPOSDMonitoring = new Component("BPOS-D.Monitoring", HealthGroup.ServiceComponents, "Monitoring", "BPOS-D", ManagedAvailabilityPriority.Low);

		// Token: 0x0400010B RID: 267
		internal static readonly Component E4E = new Component("E4E", HealthGroup.CustomerTouchPoints, "E4E", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400010C RID: 268
		internal static readonly Component UnifiedPolicy = new Component("UnifiedPolicy", HealthGroup.ServiceComponents, "ETR and DLP", "EOP", ManagedAvailabilityPriority.Low);

		// Token: 0x0400010D RID: 269
		internal static readonly Component DataInsights = new Component("DataInsights", HealthGroup.ServiceComponents, "Data Insights", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400010E RID: 270
		internal static readonly Component Horus = new Component("Horus", HealthGroup.CustomerTouchPoints, "Groups911", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400010F RID: 271
		internal static readonly Component AddExternalUserProbe = new Component("AddExternalUserProbe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000110 RID: 272
		internal static readonly Component AppIsolationProbe = new Component("App Isolation Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000111 RID: 273
		internal static readonly Component AppManagementServiceGetDeploymentIdProbe = new Component("App Management Service GetDeploymentId Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000112 RID: 274
		internal static readonly Component BcsSecureStore = new Component("BcsSecureStore", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000113 RID: 275
		internal static readonly Component CDNValidator = new Component("CDNValidator", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000114 RID: 276
		internal static readonly Component DAVGET_100MB_WMV = new Component("DAVGET_100MB_WMV", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000115 RID: 277
		internal static readonly Component DAVGET_10MB_WMV = new Component("DAVGET_10MB_WMV", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000116 RID: 278
		internal static readonly Component DAVGET_1MB_TXT = new Component("DAVGET_1MB_TXT", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000117 RID: 279
		internal static readonly Component DAVGET_200KB_TXT = new Component("DAVGET_200KB_TXT", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000118 RID: 280
		internal static readonly Component DAVGET_50MB_WMV = new Component("DAVGET_50MB_WMV", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000119 RID: 281
		internal static readonly Component DAVGET_SpeedOfLight = new Component("DAVGET_SpeedOfLight", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400011A RID: 282
		internal static readonly Component DAVGET_TypicalSmall_XLSX = new Component("DAVGET_TypicalSmall_XLSX", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400011B RID: 283
		internal static readonly Component DAVPUT_100MB_WMV = new Component("DAVPUT_100MB_WMV", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400011C RID: 284
		internal static readonly Component DAVPUT_10MB_WMV = new Component("DAVPUT_10MB_WMV", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400011D RID: 285
		internal static readonly Component DAVPUT_1MB_TXT = new Component("DAVPUT_1MB_TXT", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400011E RID: 286
		internal static readonly Component DAVPUT_200KB_TXT = new Component("DAVPUT_200KB_TXT", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400011F RID: 287
		internal static readonly Component DAVPUT_50MB_WMV = new Component("DAVPUT_50MB_WMV", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000120 RID: 288
		internal static readonly Component DAVPUT_SpeedOfLight = new Component("DAVPUT_SpeedOfLight", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000121 RID: 289
		internal static readonly Component DAVPUT_TypicalSmall_XLSX = new Component("DAVPUT_TypicalSmall_XLSX", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000122 RID: 290
		internal static readonly Component HomePage = new Component("HomePage", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000123 RID: 291
		internal static readonly Component InstallSPOnlyApp = new Component("Install SP Only App", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000124 RID: 292
		internal static readonly Component InstallUninstallAnataresApp = new Component("Install Uninstall Anatares App", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000125 RID: 293
		internal static readonly Component InstallUninstallSPOnlyApp = new Component("Install Uninstall SP Only App", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000126 RID: 294
		internal static readonly Component LaunchAntaresSqlAzureApp = new Component("Launch Antares SqlAzure App", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000127 RID: 295
		internal static readonly Component MDSProbe = new Component("MDSProbe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000128 RID: 296
		internal static readonly Component MobilePage = new Component("MobilePage", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000129 RID: 297
		internal static readonly Component MySiteHomePage = new Component("My Site Home Page", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400012A RID: 298
		internal static readonly Component PingCCT = new Component("Ping CCT", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400012B RID: 299
		internal static readonly Component ProjectServerBICenterPage = new Component("Project Server BI Center Page", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400012C RID: 300
		internal static readonly Component ProjectServerProjectCenterPage = new Component("Project Server Project Center Page", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400012D RID: 301
		internal static readonly Component ProjectServerQueueServiceAvailability = new Component("Project Server Queue Service Availability", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400012E RID: 302
		internal static readonly Component ProjectServerWorkflow = new Component("Project Server Workflow", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400012F RID: 303
		internal static readonly Component PublicSiteBlog = new Component("PublicSiteBlog", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000130 RID: 304
		internal static readonly Component PublicSiteDefaultSettings = new Component("PublicSiteDefaultSettings", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000131 RID: 305
		internal static readonly Component PublicSiteHomePage = new Component("PublicSiteHomePage", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000132 RID: 306
		internal static readonly Component PublicSiteNavigationTerms = new Component("PublicSiteNavigationTerms", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000133 RID: 307
		internal static readonly Component PublicSiteSearch = new Component("PublicSiteSearch", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000134 RID: 308
		internal static readonly Component SandboxProbe = new Component("Sandbox Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000135 RID: 309
		internal static readonly Component SearchSPO = new Component("SearchSPO", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000136 RID: 310
		internal static readonly Component Search14 = new Component("Search14", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000137 RID: 311
		internal static readonly Component SearchAnalyticsAnchor = new Component("SearchAnalyticsAnchor", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000138 RID: 312
		internal static readonly Component SearchAnalyticsIsIndexed = new Component("SearchAnalyticsIsIndexed", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000139 RID: 313
		internal static readonly Component SearchAnalyticsRecycle = new Component("SearchAnalyticsRecycle", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400013A RID: 314
		internal static readonly Component SearchAnalyticsSetup = new Component("SearchAnalyticsSetup", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400013B RID: 315
		internal static readonly Component SearchAnalyticsView = new Component("SearchAnalyticsView", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400013C RID: 316
		internal static readonly Component SearchFreshness = new Component("SearchFreshness", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400013D RID: 317
		internal static readonly Component TSAProbe = new Component("TSA Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400013E RID: 318
		internal static readonly Component UPAEditProfileProbe = new Component("UPA Edit Profile Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x0400013F RID: 319
		internal static readonly Component UPAGetCommonManagerProbe = new Component("UPA Get Common Manager Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000140 RID: 320
		internal static readonly Component UPAGetProfileProbe = new Component("UPA Get Profile Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000141 RID: 321
		internal static readonly Component UPAProbe = new Component("UPA Probe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000142 RID: 322
		internal static readonly Component UploadDoc = new Component("UploadDoc", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000143 RID: 323
		internal static readonly Component YammerNTaxonomySvcGetDefaultSiteCollectionTermStore = new Component("YammerN Taxonomy Svc GetDefaultSiteCollectionTermStore", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000144 RID: 324
		internal static readonly Component YammerNTaxonomySvcGetTermsByLabel = new Component("YammerN Taxonomy Svc GetTermsByLabel", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000145 RID: 325
		internal static readonly Component YammerNTaxonomySvcTermStoreManagementPageProbe = new Component("YammerN Taxonomy Svc TermStoreManagementPageProbe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000146 RID: 326
		internal static readonly Component YammerNTaxonomySvcTermStoreTaggingProbe = new Component("YammerN Taxonomy Svc TermStoreTaggingProbe", HealthGroup.CustomerTouchPoints, "spo-crackle", "spo", ManagedAvailabilityPriority.Low);

		// Token: 0x04000147 RID: 327
		internal static readonly Component MrmArchive = new Component("MRMArchive", HealthGroup.CustomerTouchPoints, "MRM/Archive", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x04000148 RID: 328
		internal static readonly Component TimeBasedAssistants = new Component("TimeBasedAssistants", HealthGroup.ServiceComponents, "Resource Throttling, Time Based Assistants", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x04000149 RID: 329
		internal static readonly Component DedicatedIAP = new Component("DedicatedIAP", HealthGroup.ServiceComponents, "Dedicated IAP", "Exchange", ManagedAvailabilityPriority.Low);

		// Token: 0x0400014A RID: 330
		internal static readonly Component OfficeGraph = new Component("OfficeGraph", HealthGroup.ServiceComponents, "OfficeGraph", "Exchange", ManagedAvailabilityPriority.Normal);

		// Token: 0x0400014B RID: 331
		internal static readonly Dictionary<string, Component> WellKnownComponents;
	}
}
