using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001E2 RID: 482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IMAPAutoProvision : IAutoProvision
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003088E File Offset: 0x0002EA8E
		public IMAPAutoProvision(SmtpAddress emailAddress, SecureString password) : this(emailAddress, password, new AutoProvisionOverrideProvider())
		{
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000308A0 File Offset: 0x0002EAA0
		internal IMAPAutoProvision(SmtpAddress emailAddress, SecureString password, IAutoProvisionOverrideProvider overrideProvider)
		{
			IMAPAuthenticationMechanism[] array = new IMAPAuthenticationMechanism[2];
			array[0] = IMAPAuthenticationMechanism.Ntlm;
			this.authMechanisms = array;
			base..ctor();
			if (emailAddress == SmtpAddress.Empty)
			{
				throw new ArgumentNullException("emailAddress");
			}
			SyncUtilities.ThrowIfArgumentNull("password", password);
			SyncUtilities.ThrowIfArgumentNull("overrideProvider", overrideProvider);
			string domain = emailAddress.Domain;
			this.password = password;
			this.userNames = new string[]
			{
				emailAddress.ToString(),
				emailAddress.Local
			};
			bool flag;
			if (!overrideProvider.TryGetOverrides(domain, AggregationSubscriptionType.IMAP, out this.hostNames, out flag))
			{
				this.hostNames = new string[]
				{
					"imap." + domain,
					"mail." + domain,
					domain
				};
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000309D0 File Offset: 0x0002EBD0
		public string[] Hostnames
		{
			get
			{
				return this.hostNames;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000309D8 File Offset: 0x0002EBD8
		public int[] ConnectivePorts
		{
			get
			{
				return this.connectivePorts;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000309E0 File Offset: 0x0002EBE0
		private int[] SecurePorts
		{
			get
			{
				return this.securePorts;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x000309E8 File Offset: 0x0002EBE8
		private int[] InsecurePorts
		{
			get
			{
				return this.insecurePorts;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000309F0 File Offset: 0x0002EBF0
		private IMAPAuthenticationMechanism[] AuthMechanisms
		{
			get
			{
				return this.authMechanisms;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000309F8 File Offset: 0x0002EBF8
		private string[] UserNames
		{
			get
			{
				return this.userNames;
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00030A00 File Offset: 0x0002EC00
		public static Exception VerifyAccount(string host, int port, string username, SecureString password, IMAPAuthenticationMechanism authMechanism, IMAPSecurityMechanism secMechanism, AggregationType aggregationType, SyncLogSession logSession)
		{
			Exception exception;
			using (IMAPAutoProvisionClient imapautoProvisionClient = new IMAPAutoProvisionClient(host, port, username, password, authMechanism, secMechanism, aggregationType, IMAPAutoProvision.ConnectionTimeout, 102400, logSession))
			{
				IAsyncResult asyncResult = imapautoProvisionClient.BeginVerifyAccount(null, null, null);
				AsyncOperationResult<DBNull> asyncOperationResult = imapautoProvisionClient.EndVerifyAccount(asyncResult);
				exception = asyncOperationResult.Exception;
			}
			return exception;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00030A60 File Offset: 0x0002EC60
		public DiscoverSettingsResult DiscoverSetting(SyncLogSession syncLogSession, bool testOnlyInsecure, Dictionary<Authority, bool> connectiveAuthority, AutoProvisionProgress progressCallback, out PimSubscriptionProxy sub)
		{
			sub = null;
			IMAPSecurityMechanism[] array2;
			int[] array3;
			if (testOnlyInsecure)
			{
				IMAPSecurityMechanism[] array = new IMAPSecurityMechanism[1];
				array2 = array;
				array3 = this.InsecurePorts;
			}
			else
			{
				array2 = new IMAPSecurityMechanism[]
				{
					IMAPSecurityMechanism.Ssl,
					IMAPSecurityMechanism.Tls
				};
				array3 = this.SecurePorts;
			}
			Dictionary<IMAPAutoProvisionClient, IMAPSubscriptionProxy> dictionary = new Dictionary<IMAPAutoProvisionClient, IMAPSubscriptionProxy>();
			DiscoverSettingsResult result = DiscoverSettingsResult.SettingsNotFound;
			try
			{
				bool flag = SyncUtilities.HasUnicodeCharacters(this.password);
				foreach (string text in this.UserNames)
				{
					List<IAsyncResult> list = new List<IAsyncResult>();
					List<WaitHandle> list2 = new List<WaitHandle>();
					bool flag2 = flag || SyncUtilities.HasUnicodeCharacters(text);
					foreach (string text2 in this.Hostnames)
					{
						foreach (int num in array3)
						{
							Authority authority = new Authority(text2, num);
							if (connectiveAuthority[authority])
							{
								foreach (IMAPSecurityMechanism imapsecurityMechanism in array2)
								{
									if ((143 == num || IMAPSecurityMechanism.Tls != imapsecurityMechanism) && (993 == num || IMAPSecurityMechanism.Ssl != imapsecurityMechanism))
									{
										foreach (IMAPAuthenticationMechanism imapauthenticationMechanism in this.AuthMechanisms)
										{
											if (imapauthenticationMechanism != IMAPAuthenticationMechanism.Basic || !flag2)
											{
												progressCallback(Strings.AutoProvisionTestImap, Strings.AutoProvisionStatus(authority.ToString(), text, imapsecurityMechanism.ToString(), imapauthenticationMechanism.ToString()));
												IMAPSubscriptionProxy imapsubscriptionProxy = new IMAPSubscriptionProxy();
												imapsubscriptionProxy.IncomingServer = new Fqdn(text2);
												imapsubscriptionProxy.IncomingUserName = text;
												imapsubscriptionProxy.IncomingPort = num;
												imapsubscriptionProxy.IncomingSecurity = imapsecurityMechanism;
												imapsubscriptionProxy.IncomingAuthentication = imapauthenticationMechanism;
												imapsubscriptionProxy.SetPassword(this.password);
												imapsubscriptionProxy.AggregationType = AggregationType.Aggregation;
												IMAPAutoProvisionClient imapautoProvisionClient = new IMAPAutoProvisionClient(text2, num, text, this.password, imapauthenticationMechanism, imapsecurityMechanism, imapsubscriptionProxy.AggregationType, IMAPAutoProvision.ConnectionTimeout, 102400, syncLogSession.OpenWithContext(imapsubscriptionProxy.Subscription.SubscriptionGuid));
												AsyncResult<IMAPClientState, DBNull> asyncResult = (AsyncResult<IMAPClientState, DBNull>)imapautoProvisionClient.BeginVerifyAccount(null, imapautoProvisionClient, null);
												dictionary[imapautoProvisionClient] = imapsubscriptionProxy;
												list.Add(asyncResult);
												list2.Add(asyncResult.AsyncWaitHandle);
											}
										}
									}
								}
							}
						}
					}
					if (list2.Count != 0)
					{
						progressCallback(Strings.AutoProvisionTestImap, Strings.AutoProvisionResults);
						WaitHandle.WaitAll(list2.ToArray());
						foreach (IAsyncResult asyncResult2 in list)
						{
							AsyncResult<IMAPClientState, DBNull> asyncResult3 = (AsyncResult<IMAPClientState, DBNull>)asyncResult2;
							IMAPAutoProvisionClient imapautoProvisionClient2 = asyncResult3.AsyncState as IMAPAutoProvisionClient;
							AsyncOperationResult<DBNull> asyncOperationResult = imapautoProvisionClient2.EndVerifyAccount(asyncResult3);
							if (asyncOperationResult.IsSucceeded)
							{
								sub = dictionary[imapautoProvisionClient2];
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
				foreach (KeyValuePair<IMAPAutoProvisionClient, IMAPSubscriptionProxy> keyValuePair in dictionary)
				{
					keyValuePair.Key.Dispose();
				}
			}
			return result;
		}

		// Token: 0x04000895 RID: 2197
		private const int MaxBytesToDownload = 102400;

		// Token: 0x04000896 RID: 2198
		private const int ImapSslPort = 993;

		// Token: 0x04000897 RID: 2199
		private const int ImapNonSslPort = 143;

		// Token: 0x04000898 RID: 2200
		internal static readonly int ConnectionTimeout = 20000;

		// Token: 0x04000899 RID: 2201
		private readonly SecureString password;

		// Token: 0x0400089A RID: 2202
		private readonly int[] connectivePorts = new int[]
		{
			993,
			143
		};

		// Token: 0x0400089B RID: 2203
		private readonly int[] securePorts = new int[]
		{
			993,
			143
		};

		// Token: 0x0400089C RID: 2204
		private readonly int[] insecurePorts = new int[]
		{
			143
		};

		// Token: 0x0400089D RID: 2205
		private readonly IMAPAuthenticationMechanism[] authMechanisms;

		// Token: 0x0400089E RID: 2206
		private readonly string[] hostNames;

		// Token: 0x0400089F RID: 2207
		private readonly string[] userNames;
	}
}
