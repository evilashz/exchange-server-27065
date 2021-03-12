using System;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Win32;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200005D RID: 93
	internal class AuthServiceHelper
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00017557 File Offset: 0x00015757
		internal static ExEventLog EventLogger
		{
			get
			{
				return AuthServiceHelper.eventLogger;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0001755E File Offset: 0x0001575E
		internal static LiveIdBasicAuthenticationCountersInstance PerformanceCounters
		{
			get
			{
				return AuthServiceHelper.counters;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00017568 File Offset: 0x00015768
		internal static bool IsMailboxRole
		{
			get
			{
				if (AuthServiceHelper.isMailboxRole == null)
				{
					lock (AuthServiceHelper.lockObj)
					{
						if (AuthServiceHelper.isMailboxRole == null)
						{
							using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole"))
							{
								AuthServiceHelper.isMailboxRole = new bool?(registryKey != null);
							}
						}
					}
				}
				return AuthServiceHelper.isMailboxRole.Value;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000175FC File Offset: 0x000157FC
		public static void InvalidateDuplicateUPNs(ITenantRecipientSession ads, ADRawEntry validEntry, ADRawEntry[] entries, LogWarning logWarning)
		{
			string text = validEntry[ADUserSchema.UserPrincipalName].ToString();
			SmtpAddress smtpAddress = SmtpAddress.Parse(text);
			foreach (ADRawEntry adrawEntry in entries)
			{
				if (!ADObjectId.Equals(validEntry.Id, adrawEntry.Id) && string.Equals(text, adrawEntry[ADUserSchema.UserPrincipalName].ToString(), StringComparison.OrdinalIgnoreCase))
				{
					logWarning("Entry {0} has duplicate UPN {1}", new object[]
					{
						adrawEntry.Id,
						text
					});
					string text2 = string.Format("{0}@{1}", ((Guid)adrawEntry[ADObjectSchema.Guid]).ToString("N"), smtpAddress.Domain);
					ADUser aduser = (ADUser)ads.Read(adrawEntry.Id);
					if (aduser == null)
					{
						logWarning("Cannot find entry {0} using passed ADSession", new object[]
						{
							adrawEntry.Id
						});
					}
					else
					{
						aduser.UserPrincipalName = text2;
						logWarning("Entry {0} has duplicate UPN {1} setting to {2}", new object[]
						{
							adrawEntry.Id,
							text,
							text2
						});
						ads.Save(aduser);
					}
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001773C File Offset: 0x0001593C
		public static string GetAuthType(string authorizationHeader)
		{
			string result;
			if (string.IsNullOrEmpty(authorizationHeader))
			{
				result = "Anonymous";
			}
			else
			{
				int num = authorizationHeader.IndexOf(" ");
				if (num == -1)
				{
					result = "Unknown";
				}
				else
				{
					string text = authorizationHeader.Substring(0, num);
					string s = authorizationHeader.Substring(num + 1);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(text);
					if (string.CompareOrdinal(text, "Nego2") == 0 || string.CompareOrdinal(text, "Negotiate") == 0)
					{
						byte[] array = null;
						try
						{
							array = Convert.FromBase64String(s);
						}
						catch (FormatException)
						{
						}
						if (array != null)
						{
							if (AuthServiceHelper.IsPatternInArray(array, AuthServiceHelper.NegoextsBinaryArray))
							{
								stringBuilder.Append("+NegoEx");
							}
							if (AuthServiceHelper.IsPatternInArray(array, AuthServiceHelper.NtlmBinaryArray))
							{
								stringBuilder.Append("+Ntlm");
							}
							if (AuthServiceHelper.IsPatternInArray(array, AuthServiceHelper.WlidtokenBinaryArray))
							{
								stringBuilder.Append("+LiveId");
							}
						}
						result = stringBuilder.ToString();
					}
					else
					{
						result = text;
					}
				}
			}
			return result;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00017838 File Offset: 0x00015A38
		private static bool IsPatternInArray(byte[] array, byte[] pattern)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (pattern.Length <= 0)
			{
				throw new ArgumentException("Pattern must not be empty");
			}
			for (int i = 0; i < array.Length - pattern.Length + 1; i++)
			{
				bool flag = false;
				for (int j = 0; j < pattern.Length; j++)
				{
					if (pattern[j] != array[i + j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000178AC File Offset: 0x00015AAC
		public static long ExecuteAndRecordLatency(Action action)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			action();
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000178D4 File Offset: 0x00015AD4
		public static bool IsTenantInAccountForest(string acceptedDomain, IActivityScope scope)
		{
			bool result;
			try
			{
				using (new ActivityScopeThreadGuard(scope))
				{
					PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(acceptedDomain);
					string resourceForestFqdnByAcceptedDomainName = ADAccountPartitionLocator.GetResourceForestFqdnByAcceptedDomainName(acceptedDomain);
					result = (!string.IsNullOrEmpty(partitionIdByAcceptedDomainName.ForestFQDN) && !string.Equals(resourceForestFqdnByAcceptedDomainName, partitionIdByAcceptedDomainName.ForestFQDN, StringComparison.OrdinalIgnoreCase) && !string.Equals(PartitionId.LocalForest.ForestFQDN, partitionIdByAcceptedDomainName.ForestFQDN, StringComparison.OrdinalIgnoreCase) && !partitionIdByAcceptedDomainName.ForestFQDN.EndsWith(resourceForestFqdnByAcceptedDomainName, StringComparison.OrdinalIgnoreCase));
				}
			}
			catch (CannotResolveTenantNameException ex)
			{
				result = true;
				AuthServiceHelper.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_GeneralException, acceptedDomain, new object[]
				{
					acceptedDomain,
					ex.ToString()
				});
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001799C File Offset: 0x00015B9C
		public static string GetImplicitUpn(ADRawEntry entry)
		{
			OrganizationId organizationId = (OrganizationId)entry[ADObjectSchema.OrganizationId];
			string arg = (string)entry[ADMailboxRecipientSchema.SamAccountName];
			return string.Format("{0}@{1}", arg, organizationId.PartitionId.ForestFQDN);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000179E4 File Offset: 0x00015BE4
		public static HttpWebRequest CreateHttpWebRequest(string uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.Timeout = 1000 * AuthServiceStaticConfig.Config.LiveIdStsTimeoutInSeconds;
			httpWebRequest.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AuthService.CertificateValidationCallBack);
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = "POST";
			return httpWebRequest;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00017A3D File Offset: 0x00015C3D
		public static void UpdateConnectionSettingsInRequest(ref HttpWebRequest request, string connectionGroupName)
		{
			request.KeepAlive = true;
			request.ServicePoint.ConnectionLeaseTimeout = 1000 * AuthServiceStaticConfig.Config.ConnectionLeaseTimeoutInSeconds;
			request.ConnectionGroupName = connectionGroupName;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00017A6C File Offset: 0x00015C6C
		public static bool CloseConnectionGroupIfNeeded(bool closeConnectionGroup, string uri, string connectionGroupName, int traceId)
		{
			if (AuthServiceStaticConfig.Config.MsoSSLEndpointType != MsoEndpointType.OLD && closeConnectionGroup)
			{
				ServicePoint servicePoint = ServicePointManager.FindServicePoint(new Uri(uri));
				if (servicePoint != null)
				{
					ExTraceGlobals.AuthenticationTracer.Information<string>((long)traceId, "Closing connection group '{0}'", connectionGroupName);
					return servicePoint.CloseConnectionGroup(connectionGroupName);
				}
			}
			return false;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00017ACC File Offset: 0x00015CCC
		public static string GetConnectionGroup(int traceId)
		{
			int num = (AuthServiceStaticConfig.Config.MaxConnectionGroups > 0) ? (Thread.CurrentThread.ManagedThreadId % AuthServiceStaticConfig.Config.MaxConnectionGroups) : Thread.CurrentThread.ManagedThreadId;
			string text = AuthServiceStaticConfig.Config.ConnectionGroupPrefix + num;
			ExTraceGlobals.AuthenticationTracer.Information<string>((long)traceId, "ConnectionGroupName = '{0}'", text);
			return text;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00017B34 File Offset: 0x00015D34
		public static bool IsOutlookComUser(string userName)
		{
			bool flag = ConsumerIdentityHelper.IsConsumerMailbox(userName);
			if (!flag && AuthServiceStaticConfig.Config.outlookComRegex != null)
			{
				flag = AuthServiceStaticConfig.Config.outlookComRegex.IsMatch(userName);
			}
			return flag;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00017B69 File Offset: 0x00015D69
		public static UserType GetUserType(DomainConfig domainConfig)
		{
			if (domainConfig.IsOutlookCom)
			{
				return UserType.OutlookCom;
			}
			if (domainConfig.IsFederated)
			{
				return UserType.Federated;
			}
			if (domainConfig.Instance != LiveIdInstanceType.Business)
			{
				return UserType.ManagedConsumer;
			}
			return UserType.ManagedBusiness;
		}

		// Token: 0x0400029F RID: 671
		public const string LiveIdComponent = "MSExchange LiveIdBasicAuthentication";

		// Token: 0x040002A0 RID: 672
		internal const string AuthenticatedByOfflineOrgId = "AuthenticatedBy:OfflineOrgId.";

		// Token: 0x040002A1 RID: 673
		internal const string AuthenticatedByCache = "AuthenticatedBy:Cache.";

		// Token: 0x040002A2 RID: 674
		internal const string FederatedTag = "<FEDERATED>";

		// Token: 0x040002A3 RID: 675
		internal const string UserTypeTag = "<UserType:{0}>";

		// Token: 0x040002A4 RID: 676
		internal const string CheckPwdConfidenceTag = "CheckPwdConfidence";

		// Token: 0x040002A5 RID: 677
		internal const string GetLastLogonTimeFromMailboxTag = "GetLastLogonTimeFromMailbox";

		// Token: 0x040002A6 RID: 678
		internal static readonly byte[] NegoextsBinaryArray = new byte[]
		{
			78,
			69,
			71,
			79,
			69,
			88,
			84,
			83
		};

		// Token: 0x040002A7 RID: 679
		internal static readonly byte[] NtlmBinaryArray = new byte[]
		{
			78,
			84,
			76,
			77,
			83,
			83,
			80
		};

		// Token: 0x040002A8 RID: 680
		internal static readonly byte[] WlidtokenBinaryArray = new byte[]
		{
			87,
			108,
			105,
			100,
			84,
			111,
			107,
			101,
			110
		};

		// Token: 0x040002A9 RID: 681
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.AuthenticationTracer.Category, "MSExchange LiveIdBasicAuthentication");

		// Token: 0x040002AA RID: 682
		private static readonly LiveIdBasicAuthenticationCountersInstance counters = LiveIdBasicAuthenticationCounters.GetInstance(Process.GetCurrentProcess().ProcessName);

		// Token: 0x040002AB RID: 683
		private static bool? isMailboxRole = null;

		// Token: 0x040002AC RID: 684
		private static object lockObj = new object();
	}
}
