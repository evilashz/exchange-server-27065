using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001ED RID: 493
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Pop3AutoProvision : IAutoProvision
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x00032E9C File Offset: 0x0003109C
		public Pop3AutoProvision(SmtpAddress emailAddress, SecureString password, string userLegacyDN) : this(emailAddress, password, userLegacyDN, new AutoProvisionOverrideProvider())
		{
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00032EAC File Offset: 0x000310AC
		internal Pop3AutoProvision(SmtpAddress emailAddress, SecureString password, string userLegacyDN, IAutoProvisionOverrideProvider overrideProvider)
		{
			AuthenticationMechanism[] array = new AuthenticationMechanism[2];
			array[0] = AuthenticationMechanism.Spa;
			this.authMechanisms = array;
			base..ctor();
			if (emailAddress == SmtpAddress.Empty)
			{
				throw new ArgumentException("cannot be an empty address", "emailAddress");
			}
			SyncUtilities.ThrowIfArgumentNull("password", password);
			SyncUtilities.ThrowIfArgumentNull("overrideProvider", overrideProvider);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userLegacyDN", userLegacyDN);
			string domain = emailAddress.Domain;
			this.password = password;
			this.userNames = new string[]
			{
				emailAddress.ToString(),
				emailAddress.Local
			};
			this.userLegacyDN = userLegacyDN;
			bool flag;
			if (!overrideProvider.TryGetOverrides(domain, AggregationSubscriptionType.Pop, out this.hostNames, out flag))
			{
				this.hostNames = new string[]
				{
					"pop." + domain,
					"mail." + domain,
					"pop3." + domain,
					domain
				};
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00032FFA File Offset: 0x000311FA
		public string[] Hostnames
		{
			get
			{
				return this.hostNames;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00033002 File Offset: 0x00031202
		public int[] ConnectivePorts
		{
			get
			{
				return this.connectivePorts;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003300A File Offset: 0x0003120A
		private int[] SecurePorts
		{
			get
			{
				return this.securePorts;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00033012 File Offset: 0x00031212
		private int[] InsecurePorts
		{
			get
			{
				return this.insecurePorts;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0003301A File Offset: 0x0003121A
		private string[] UserNames
		{
			get
			{
				return this.userNames;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00033022 File Offset: 0x00031222
		private AuthenticationMechanism[] AuthMechanisms
		{
			get
			{
				return this.authMechanisms;
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003302C File Offset: 0x0003122C
		public static bool ValidatePopSettings(bool leaveOnServer, bool mirrored, string host, int port, string username, SecureString password, AuthenticationMechanism authentication, SecurityMechanism security, string userLegacyDN, SyncLogSession syncLogSession, out LocalizedException validationException)
		{
			validationException = null;
			bool result;
			using (Pop3Client pop3Client = new Pop3Client(Guid.NewGuid(), host, port, username, password, authentication, security, userLegacyDN, Pop3AutoProvision.ConnectionTimeout, syncLogSession, null, null))
			{
				AsyncResult<Pop3Client, Pop3ResultData> asyncResult = Pop3Client.BeginVerifyAccount(pop3Client, null, null, null);
				AsyncOperationResult<Pop3ResultData> asyncOperationResult = Pop3Client.EndVerifyAccount(asyncResult);
				if (!asyncOperationResult.IsSucceeded)
				{
					if (asyncOperationResult.Exception != null && asyncOperationResult.Exception.InnerException != null)
					{
						validationException = (asyncOperationResult.Exception.InnerException as LocalizedException);
					}
					result = false;
				}
				else if (mirrored && !Pop3AutoProvision.SupportsMirroredSubscription(asyncOperationResult))
				{
					validationException = new Pop3MirroredAccountNotPossibleException();
					result = false;
				}
				else if (leaveOnServer && !Pop3AutoProvision.SupportsLeaveOnServer(asyncOperationResult))
				{
					validationException = new Pop3LeaveOnServerNotPossibleException();
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000330F0 File Offset: 0x000312F0
		public DiscoverSettingsResult DiscoverSetting(SyncLogSession syncLogSession, bool testOnlyInsecure, Dictionary<Authority, bool> connectiveAuthority, AutoProvisionProgress progressCallback, out PimSubscriptionProxy sub)
		{
			sub = null;
			SecurityMechanism[] array2;
			int[] array3;
			if (testOnlyInsecure)
			{
				SecurityMechanism[] array = new SecurityMechanism[1];
				array2 = array;
				array3 = this.InsecurePorts;
			}
			else
			{
				array2 = new SecurityMechanism[]
				{
					SecurityMechanism.Ssl,
					SecurityMechanism.Tls
				};
				array3 = this.SecurePorts;
			}
			List<Pop3Client> list = new List<Pop3Client>();
			DiscoverSettingsResult result = DiscoverSettingsResult.SettingsNotFound;
			try
			{
				bool flag = SyncUtilities.HasUnicodeCharacters(this.password);
				foreach (string text in this.UserNames)
				{
					List<IAsyncResult> list2 = new List<IAsyncResult>();
					List<WaitHandle> list3 = new List<WaitHandle>();
					bool flag2 = flag || SyncUtilities.HasUnicodeCharacters(text);
					foreach (string text2 in this.Hostnames)
					{
						foreach (int num in array3)
						{
							Authority authority = new Authority(text2, num);
							if (connectiveAuthority[authority])
							{
								foreach (SecurityMechanism securityMechanism in array2)
								{
									if ((securityMechanism != SecurityMechanism.Tls || num != 995) && (securityMechanism != SecurityMechanism.Ssl || num != 110))
									{
										foreach (AuthenticationMechanism authenticationMechanism in this.AuthMechanisms)
										{
											if (authenticationMechanism != AuthenticationMechanism.Basic || !flag2)
											{
												progressCallback(Strings.AutoProvisionTestPop3, Strings.AutoProvisionStatus(authority.ToString(), text, securityMechanism.ToString(), authenticationMechanism.ToString()));
												PopSubscriptionProxy popSubscriptionProxy = new PopSubscriptionProxy();
												popSubscriptionProxy.IncomingServer = new Fqdn(text2);
												popSubscriptionProxy.IncomingUserName = text;
												popSubscriptionProxy.IncomingPort = num;
												popSubscriptionProxy.IncomingSecurity = securityMechanism;
												popSubscriptionProxy.IncomingAuthentication = authenticationMechanism;
												popSubscriptionProxy.SetPassword(this.password);
												Pop3Client pop3Client = new Pop3Client(Guid.NewGuid(), text2, num, text, this.password, authenticationMechanism, securityMechanism, this.userLegacyDN, Pop3AutoProvision.ConnectionTimeout, syncLogSession, null, null);
												AsyncResult<Pop3Client, Pop3ResultData> asyncResult = Pop3Client.BeginVerifyAccount(pop3Client, null, popSubscriptionProxy, null);
												list.Add(pop3Client);
												list2.Add(asyncResult);
												list3.Add(asyncResult.AsyncWaitHandle);
											}
										}
									}
								}
							}
						}
					}
					if (list3.Count != 0)
					{
						progressCallback(Strings.AutoProvisionTestPop3, Strings.AutoProvisionResults);
						WaitHandle.WaitAll(list3.ToArray());
						foreach (IAsyncResult asyncResult2 in list2)
						{
							AsyncResult<Pop3Client, Pop3ResultData> asyncResult3 = (AsyncResult<Pop3Client, Pop3ResultData>)asyncResult2;
							AsyncOperationResult<Pop3ResultData> asyncOperationResult = Pop3Client.EndVerifyAccount(asyncResult3);
							if (asyncOperationResult.IsSucceeded)
							{
								PopSubscriptionProxy popSubscriptionProxy2 = asyncResult3.AsyncState as PopSubscriptionProxy;
								popSubscriptionProxy2.LeaveOnServer = Pop3AutoProvision.SupportsLeaveOnServer(asyncOperationResult);
								popSubscriptionProxy2.ServerSupportsMirroring = Pop3AutoProvision.SupportsMirroredSubscription(asyncOperationResult);
								sub = popSubscriptionProxy2;
								return DiscoverSettingsResult.Succeeded;
							}
							SyncTransientException ex = asyncResult3.Exception as SyncTransientException;
							if (ex != null && ex.DetailedAggregationStatus == DetailedAggregationStatus.AuthenticationError)
							{
								result = DiscoverSettingsResult.AuthenticationError;
							}
						}
					}
				}
			}
			finally
			{
				foreach (Pop3Client pop3Client2 in list)
				{
					pop3Client2.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00033480 File Offset: 0x00031680
		private static bool SupportsLeaveOnServer(AsyncOperationResult<Pop3ResultData> result)
		{
			return result != null && result.Data.UidlCommandSupported && (result.Data.RetentionDays == null || result.Data.RetentionDays.Value > 0);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000334D0 File Offset: 0x000316D0
		private static bool SupportsMirroredSubscription(AsyncOperationResult<Pop3ResultData> result)
		{
			return result != null && (result.Data.RetentionDays == null || !(result.Data.RetentionDays != int.MaxValue));
		}

		// Token: 0x040008D7 RID: 2263
		internal static readonly int ConnectionTimeout = 20000;

		// Token: 0x040008D8 RID: 2264
		private readonly int[] connectivePorts = new int[]
		{
			995,
			110
		};

		// Token: 0x040008D9 RID: 2265
		private readonly int[] securePorts = new int[]
		{
			995,
			110
		};

		// Token: 0x040008DA RID: 2266
		private readonly int[] insecurePorts = new int[]
		{
			110
		};

		// Token: 0x040008DB RID: 2267
		private readonly AuthenticationMechanism[] authMechanisms;

		// Token: 0x040008DC RID: 2268
		private readonly string[] hostNames;

		// Token: 0x040008DD RID: 2269
		private readonly string[] userNames;

		// Token: 0x040008DE RID: 2270
		private readonly SecureString password;

		// Token: 0x040008DF RID: 2271
		private readonly string userLegacyDN;
	}
}
