using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F4 RID: 756
	internal sealed class ClientLogEvent : ILogEvent
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x00058B9C File Offset: 0x00056D9C
		internal ClientLogEvent(Datapoint datapoint, string userContext) : this(datapoint, userContext, string.Empty, string.Empty, string.Empty, string.Empty, false, null)
		{
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00058BC8 File Offset: 0x00056DC8
		internal ClientLogEvent(Datapoint datapoint, string userContext, string ipAddress, string userName, string serverVersion, bool isMowa, string clientIdCookieValue = null) : this(datapoint, userContext, ipAddress, userName, string.Empty, serverVersion, isMowa, clientIdCookieValue)
		{
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00058BEC File Offset: 0x00056DEC
		internal ClientLogEvent(Datapoint datapoint, string userContext, string ipAddress, string userName, string clientVersion, string serverVersion, bool isMowa, string clientIdCookieValue = null)
		{
			this.datapoint = datapoint;
			this.userContext = userContext;
			this.ipAddress = ExtensibleLogger.FormatPIIValue(ipAddress);
			this.userName = ExtensibleLogger.FormatPIIValue(userName);
			this.clientVersion = clientVersion;
			this.serverVersion = serverVersion;
			this.isMowa = isMowa;
			this.clientIdCookieValue = clientIdCookieValue;
			this.BuildDictionary();
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00058C4C File Offset: 0x00056E4C
		public string EventId
		{
			get
			{
				return this.datapoint.Id;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00058C59 File Offset: 0x00056E59
		public string Time
		{
			get
			{
				return this.datapoint.Time;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00058C66 File Offset: 0x00056E66
		internal Datapoint InnerDatapoint
		{
			get
			{
				return this.datapoint;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00058C6E File Offset: 0x00056E6E
		internal Dictionary<string, object> DatapointProperties
		{
			get
			{
				return this.datapointProperties;
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00058C76 File Offset: 0x00056E76
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return this.datapointProperties;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00058C7E File Offset: 0x00056E7E
		public bool IsForConsumer(DatapointConsumer consumer)
		{
			return this.datapoint.IsForConsumer(consumer);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00058C8C File Offset: 0x00056E8C
		public void UpdatePerfTraceDatapoint(UserContext userContext)
		{
			if (userContext != null)
			{
				LogEventCommonData logEventCommonData = userContext.LogEventCommonData;
				this.UpdateDeviceInfo(logEventCommonData);
				this.UpdateClientInfo(logEventCommonData);
				this.UpdateUserAgent(userContext.UserAgent);
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00058CC0 File Offset: 0x00056EC0
		public void UpdateActionRecordDataPoint(UserContext userContext)
		{
			LogEventCommonData logEventCommonData = userContext.LogEventCommonData;
			this.UpdateDeviceInfo(logEventCommonData);
			if (!string.IsNullOrEmpty(logEventCommonData.OfflineEnabled))
			{
				this.CheckAndAddKeyValue("oe", logEventCommonData.OfflineEnabled);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.Layout))
			{
				this.CheckAndAddKeyValue("l", logEventCommonData.Layout);
			}
			this.UpdateTenantGuid(userContext.ExchangePrincipal);
			this.UpdateUserAgent(userContext.UserAgent);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00058D30 File Offset: 0x00056F30
		public void UpdatePerformanceNavigationDatapoint(UserContext userContext)
		{
			LogEventCommonData logEventCommonData = userContext.LogEventCommonData;
			this.UpdateDeviceInfo(logEventCommonData);
			this.UpdateFlightInfo(logEventCommonData);
			this.UpdateTenantGuid(userContext.ExchangePrincipal);
			this.UpdateMailboxGuid(userContext.ExchangePrincipal);
			this.UpdateUserAgent(userContext.UserAgent);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00058D78 File Offset: 0x00056F78
		public void UpdateTenantGuid(ExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal != null && exchangePrincipal.MailboxInfo != null && exchangePrincipal.MailboxInfo.OrganizationId != null)
			{
				string text = exchangePrincipal.MailboxInfo.OrganizationId.ToExternalDirectoryOrganizationId();
				if (text != string.Empty)
				{
					this.CheckAndAddKeyValue("tg", text);
				}
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00058DCD File Offset: 0x00056FCD
		public void UpdateMailboxGuid(ExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal != null && exchangePrincipal.MailboxInfo != null)
			{
				this.CheckAndAddKeyValue("mg", exchangePrincipal.MailboxInfo.MailboxGuid);
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00058DF5 File Offset: 0x00056FF5
		public void UpdateClientBuildVersion(LogEventCommonData logEventCommonData)
		{
			if (!string.IsNullOrEmpty(logEventCommonData.ClientBuild))
			{
				this.CheckAndAddKeyValue("cbld", logEventCommonData.ClientBuild);
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00058E18 File Offset: 0x00057018
		public void UpdateDeviceInfo(LogEventCommonData logEventCommonData)
		{
			if (!string.IsNullOrEmpty(logEventCommonData.Platform))
			{
				this.CheckAndAddKeyValue("pl", logEventCommonData.Platform);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.Browser))
			{
				this.CheckAndAddKeyValue("brn", logEventCommonData.Browser);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.BrowserVersion))
			{
				this.CheckAndAddKeyValue("brv", logEventCommonData.BrowserVersion);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.OperatingSystem))
			{
				this.CheckAndAddKeyValue("osn", logEventCommonData.OperatingSystem);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.OperatingSystemVersion))
			{
				this.CheckAndAddKeyValue("osv", logEventCommonData.OperatingSystemVersion);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.DeviceModel))
			{
				this.CheckAndAddKeyValue("dm", logEventCommonData.DeviceModel);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.PalBuild))
			{
				this.CheckAndAddKeyValue("pbld", logEventCommonData.PalBuild);
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00058EF7 File Offset: 0x000570F7
		public void UpdateUserAgent(string userAgentString)
		{
			this.CheckAndAddKeyValue("UA", userAgentString);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00058F05 File Offset: 0x00057105
		public void UpdateFlightInfo(LogEventCommonData logEventCommonData)
		{
			if (!string.IsNullOrEmpty(logEventCommonData.Flights))
			{
				this.CheckAndAddKeyValue("flt", logEventCommonData.Flights);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.Features))
			{
				this.CheckAndAddKeyValue("ftr", logEventCommonData.Features);
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00058F44 File Offset: 0x00057144
		public void UpdateClientInfo(LogEventCommonData logEventCommonData)
		{
			this.UpdateClientLocaleInfo(logEventCommonData);
			if (!string.IsNullOrEmpty(logEventCommonData.Layout))
			{
				this.CheckAndAddKeyValue("l", logEventCommonData.Layout);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.OfflineEnabled))
			{
				this.CheckAndAddKeyValue("oe", logEventCommonData.OfflineEnabled);
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00058F94 File Offset: 0x00057194
		public void UpdateClientLocaleInfo(LogEventCommonData logEventCommonData)
		{
			if (!string.IsNullOrEmpty(logEventCommonData.Culture))
			{
				this.CheckAndAddKeyValue("clg", logEventCommonData.Culture);
			}
			if (!string.IsNullOrEmpty(logEventCommonData.TimeZone))
			{
				this.CheckAndAddKeyValue("tz", logEventCommonData.TimeZone);
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00058FD4 File Offset: 0x000571D4
		public void UpdateTenantInfo(UserContext userContext)
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.LogTenantInfo.Enabled)
			{
				if (userContext.LogEventCommonData != null && !string.IsNullOrEmpty(userContext.LogEventCommonData.TenantDomain))
				{
					this.CheckAndAddKeyValue("dom", userContext.LogEventCommonData.TenantDomain);
				}
				if (!string.IsNullOrEmpty(userContext.BposSkuCapability))
				{
					this.CheckAndAddKeyValue("sku", userContext.BposSkuCapability);
				}
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00059050 File Offset: 0x00057250
		public void UpdateNetid(UserContext userContext)
		{
			if (userContext.MailboxIdentity != null && userContext.MailboxIdentity.GetOWAMiniRecipient().NetID != null)
			{
				this.CheckAndAddKeyValue("NetId", userContext.MailboxIdentity.GetOWAMiniRecipient().NetID.ToString());
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005909D File Offset: 0x0005729D
		public void UpdateDatabaseInfo(UserContext userContext)
		{
			if (userContext.LogEventCommonData.DatabaseGuid != Guid.Empty)
			{
				this.CheckAndAddKeyValue("db", userContext.LogEventCommonData.DatabaseGuid);
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000590D1 File Offset: 0x000572D1
		public void UpdatePassThroughProxyInfo(bool isPassThroughProxy)
		{
			this.CheckAndAddKeyValue("PTP", isPassThroughProxy ? "1" : "0");
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000590ED File Offset: 0x000572ED
		public string GetValue(string keyName)
		{
			return (string)this.datapointProperties[keyName];
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00059100 File Offset: 0x00057300
		public bool TryGetValue<T>(string keyName, out T result) where T : class
		{
			object obj;
			if (!this.datapointProperties.TryGetValue(keyName, out obj))
			{
				result = default(T);
				return false;
			}
			if (obj is T)
			{
				result = (T)((object)obj);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00059144 File Offset: 0x00057344
		public override string ToString()
		{
			return this.datapoint.Consumers.ToString() + ": " + this.EventId;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005916C File Offset: 0x0005736C
		private int GetCustomKeyCount()
		{
			if (this.datapoint.Keys != null && this.datapoint.Values != null && this.datapoint.Keys.Length == this.datapoint.Values.Length)
			{
				return this.datapoint.Keys.Length;
			}
			return 0;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000591C0 File Offset: 0x000573C0
		private void BuildDictionary()
		{
			this.datapointProperties = new Dictionary<string, object>
			{
				{
					"ts",
					this.datapoint.Time
				},
				{
					ClientLogEvent.UserContextKey,
					this.userContext
				},
				{
					"ds",
					this.datapoint.Size
				},
				{
					"DC",
					(int)this.datapoint.Consumers
				},
				{
					"Mowa",
					this.isMowa ? "1" : "0"
				}
			};
			if (!string.IsNullOrEmpty(this.ipAddress))
			{
				this.datapointProperties.Add("ip", this.ipAddress);
			}
			if (!string.IsNullOrEmpty(this.userName))
			{
				this.datapointProperties.Add("user", this.userName);
			}
			if (!string.IsNullOrEmpty(this.clientVersion))
			{
				this.datapointProperties.Add("cbld", this.clientVersion);
			}
			if (!string.IsNullOrEmpty(this.serverVersion))
			{
				this.datapointProperties.Add("Bld", this.serverVersion);
			}
			if (!string.IsNullOrEmpty(this.clientIdCookieValue))
			{
				this.datapointProperties.Add("ClientId", this.clientIdCookieValue);
			}
			for (int i = 0; i < this.GetCustomKeyCount(); i++)
			{
				this.CheckAndAddKeyValue(this.datapoint.Keys[i], this.datapoint.Values[i]);
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00059338 File Offset: 0x00057538
		private void CheckAndAddKeyValue(string key, object value)
		{
			if (!this.datapointProperties.ContainsKey(key))
			{
				this.datapointProperties.Add(key, value);
				return;
			}
			if (this.datapointProperties.ContainsKey("dk"))
			{
				Dictionary<string, object> dictionary;
				(dictionary = this.datapointProperties)["dk"] = dictionary["dk"] + "," + key;
				return;
			}
			this.datapointProperties.Add("dk", key);
		}

		// Token: 0x04000DFD RID: 3581
		public const string TimeStampKey = "ts";

		// Token: 0x04000DFE RID: 3582
		public const string IpAddressKey = "ip";

		// Token: 0x04000DFF RID: 3583
		public const string UserNameKey = "user";

		// Token: 0x04000E00 RID: 3584
		public const string DatapointSizeKey = "ds";

		// Token: 0x04000E01 RID: 3585
		public const string ClientVersionKey = "cbld";

		// Token: 0x04000E02 RID: 3586
		public const string ServerVersionKey = "Bld";

		// Token: 0x04000E03 RID: 3587
		public const string ConsumersKey = "DC";

		// Token: 0x04000E04 RID: 3588
		public const string IsMowaKey = "Mowa";

		// Token: 0x04000E05 RID: 3589
		public const string TenantGuidKey = "tg";

		// Token: 0x04000E06 RID: 3590
		public const string MailboxGuidKey = "mg";

		// Token: 0x04000E07 RID: 3591
		public const string NetIdKey = "NetId";

		// Token: 0x04000E08 RID: 3592
		public const string IsPassThroughProxyKey = "PTP";

		// Token: 0x04000E09 RID: 3593
		public const string DuplicatedKeysKey = "dk";

		// Token: 0x04000E0A RID: 3594
		public const string UserAgentKey = "UA";

		// Token: 0x04000E0B RID: 3595
		public const string ClientIdCookieKey = "ClientId";

		// Token: 0x04000E0C RID: 3596
		public static readonly string UserContextKey = UserContextCookie.UserContextCookiePrefix;

		// Token: 0x04000E0D RID: 3597
		private readonly Datapoint datapoint;

		// Token: 0x04000E0E RID: 3598
		private readonly string userContext;

		// Token: 0x04000E0F RID: 3599
		private readonly string ipAddress;

		// Token: 0x04000E10 RID: 3600
		private readonly string userName;

		// Token: 0x04000E11 RID: 3601
		private readonly string clientVersion;

		// Token: 0x04000E12 RID: 3602
		private readonly string serverVersion;

		// Token: 0x04000E13 RID: 3603
		private readonly bool isMowa;

		// Token: 0x04000E14 RID: 3604
		public readonly string clientIdCookieValue;

		// Token: 0x04000E15 RID: 3605
		private Dictionary<string, object> datapointProperties;
	}
}
