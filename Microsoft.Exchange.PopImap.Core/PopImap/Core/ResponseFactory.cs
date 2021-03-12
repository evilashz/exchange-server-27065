using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000032 RID: 50
	internal abstract class ResponseFactory : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000B160 File Offset: 0x00009360
		protected ResponseFactory(ProtocolSession session)
		{
			this.session = session;
			this.disposeTracker = this.GetDisposeTracker();
			this.stopwatch = Stopwatch.StartNew();
			this.ActivityId = Guid.Empty;
			if (ResponseFactory.latencyDetectionContextFactory == null)
			{
				lock (ResponseFactory.lockObject)
				{
					if (ResponseFactory.latencyDetectionContextFactory == null)
					{
						ResponseFactory.latencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory(ProtocolBaseServices.ServiceName, ResponseFactory.DefaultMinPopImapThreshold, ResponseFactory.DefaultMinPopImapThreshold);
					}
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000B1F8 File Offset: 0x000093F8
		public static ExTimeZone CurrentExTimeZone
		{
			get
			{
				if (ResponseFactory.currentExTimeZone == null)
				{
					ResponseFactory.currentExTimeZone = ExTimeZone.CurrentTimeZone;
				}
				return ResponseFactory.currentExTimeZone;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B210 File Offset: 0x00009410
		public static char[] WordDelimiter
		{
			get
			{
				return ResponseFactory.wordDelimiter;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000B217 File Offset: 0x00009417
		public static RefCountTable<string> ConnectionsPerUser
		{
			get
			{
				return ResponseFactory.connectionsPerUser;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B21E File Offset: 0x0000941E
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000B225 File Offset: 0x00009425
		public static bool CheckOnlyAuthenticationStatusEnabled { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000B22D File Offset: 0x0000942D
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000B234 File Offset: 0x00009434
		public static bool EnforceLogsRetentionPolicyEnabled { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000B23C File Offset: 0x0000943C
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000B243 File Offset: 0x00009443
		public static bool UsePrimarySmtpAddressEnabled { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000B24B File Offset: 0x0000944B
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000B252 File Offset: 0x00009452
		public static bool IgnoreNonProvisionedServersEnabled { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000B25A File Offset: 0x0000945A
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000B261 File Offset: 0x00009461
		public static bool AppendServerNameInBannerEnabled { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000B269 File Offset: 0x00009469
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000B270 File Offset: 0x00009470
		public static bool GlobalCriminalComplianceEnabled { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000B278 File Offset: 0x00009478
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000B27F File Offset: 0x0000947F
		public static Func<bool> GetClientAccessRulesEnabled { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000B287 File Offset: 0x00009487
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000B28E File Offset: 0x0000948E
		public static bool LrsLoggingEnabled { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000B296 File Offset: 0x00009496
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000B29D File Offset: 0x0000949D
		public static bool KerberosAuthEnabled { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B2A5 File Offset: 0x000094A5
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000B2AC File Offset: 0x000094AC
		public static ClientAccessProtocol ClientAccessRulesProtocol { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000B2B4 File Offset: 0x000094B4
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000B2BC File Offset: 0x000094BC
		public string CommandName { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000B2C5 File Offset: 0x000094C5
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000B2CD File Offset: 0x000094CD
		public string Parameters { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B2D6 File Offset: 0x000094D6
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000B2DE File Offset: 0x000094DE
		public bool SkipAuthOnCafeEnabled { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B2E7 File Offset: 0x000094E7
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000B2EF File Offset: 0x000094EF
		public bool UseSamAccountNameAsUsername { get; protected set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public string DefaultAcceptedDomainName
		{
			get
			{
				string text;
				if (ResponseFactory.defaultAcceptedDomainTable.TryGetValue(this.protocolUser.OrganizationId, out text))
				{
					return text;
				}
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.protocolUser.OrganizationId), 531, "DefaultAcceptedDomainName", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs");
				tenantOrTopologyConfigurationSession.SessionSettings.AccountingObject = this.Session.Budget;
				AcceptedDomain defaultAcceptedDomain = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain();
				if (defaultAcceptedDomain == null)
				{
					ProtocolBaseServices.ServerTracer.TraceError(0L, "No default accepted domain found.");
					ProtocolBaseServices.LogEvent(this.NoDefaultAcceptedDomainFoundEventTuple, null, new string[0]);
					return null;
				}
				text = defaultAcceptedDomain.DomainName.ToString();
				ResponseFactory.defaultAcceptedDomainTable.Add(this.protocolUser.OrganizationId, text);
				return text;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B3B4 File Offset: 0x000095B4
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000B3BC File Offset: 0x000095BC
		public uint InvalidCommands
		{
			get
			{
				return this.invalidCommands;
			}
			set
			{
				this.invalidCommands = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000323 RID: 803
		public abstract bool IsAuthenticated { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000324 RID: 804
		public abstract bool IsDisconnected { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000325 RID: 805
		public abstract string TimeoutErrorString { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000326 RID: 806
		public abstract string FirstAuthenticateResponse { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B3C5 File Offset: 0x000095C5
		public bool IsInAuthenticationMode
		{
			get
			{
				return this.serverContext != null;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B3D3 File Offset: 0x000095D3
		public bool ExactRFC822SizeEnabled
		{
			get
			{
				if (!this.ProtocolUser.UseProtocolDefaults)
				{
					return this.ProtocolUser.EnableExactRFC822Size;
				}
				return this.Session.Server.EnableExactRFC822Size;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B3FE File Offset: 0x000095FE
		public bool SuppressReadReceipt
		{
			get
			{
				if (!this.ProtocolUser.UseProtocolDefaults)
				{
					return this.ProtocolUser.SuppressReadReceipt;
				}
				return this.Session.Server.SuppressReadReceipt;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B429 File Offset: 0x00009629
		public bool ForceICalForCalendarRetrievalOption
		{
			get
			{
				return !this.ProtocolUser.UseProtocolDefaults && this.ProtocolUser.ForceICalForCalendarRetrievalOption;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000B445 File Offset: 0x00009645
		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B44D File Offset: 0x0000964D
		public AutoResetEvent ConnectionCreated
		{
			get
			{
				return this.connectionCreated;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000B455 File Offset: 0x00009655
		public EncryptionType? ProxyEncryptionType
		{
			get
			{
				return this.proxyEncryptionType;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B45D File Offset: 0x0000965D
		public ProtocolSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000B465 File Offset: 0x00009665
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000B46D File Offset: 0x0000966D
		public ProtocolRequest IncompleteRequest
		{
			get
			{
				return this.incompleteRequest;
			}
			set
			{
				this.incompleteRequest = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000B476 File Offset: 0x00009676
		public bool ProxyMode
		{
			get
			{
				return this.session.ProxySession != null;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B489 File Offset: 0x00009689
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000B491 File Offset: 0x00009691
		public string Mailbox { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B49A File Offset: 0x0000969A
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000B4A2 File Offset: 0x000096A2
		public Guid ActivityId { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B4AB File Offset: 0x000096AB
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000B4B4 File Offset: 0x000096B4
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.userName = value.Replace('\\', '/');
					if (this.Session.LightLogSession != null)
					{
						this.Session.LightLogSession.User = this.userName;
						return;
					}
				}
				else
				{
					this.userName = null;
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000B504 File Offset: 0x00009704
		public string PrimarySmtpAddress
		{
			get
			{
				return this.protocolUser.PrimarySmtpAddress;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B511 File Offset: 0x00009711
		public MailboxSession Store
		{
			get
			{
				if (this.store != null)
				{
					return this.store;
				}
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Session has been disconnected, store object in not available.");
				throw new ResponseFactory.SessionDisconnectedException();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000B544 File Offset: 0x00009744
		public bool IsStoreConnected
		{
			get
			{
				bool result;
				try
				{
					result = (this.store != null && this.store.IsConnected);
				}
				catch (ObjectDisposedException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000B580 File Offset: 0x00009780
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000B588 File Offset: 0x00009788
		public bool NeedToReloadStoreStates { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000B591 File Offset: 0x00009791
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000B599 File Offset: 0x00009799
		public bool OkToResetRecipientCache
		{
			get
			{
				return this.okToResetRecipientCache;
			}
			set
			{
				this.okToResetRecipientCache = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000B5A4 File Offset: 0x000097A4
		public UserConfigurationManager CustomStorageManager
		{
			get
			{
				if (this.customStorageManager == null)
				{
					lock (this)
					{
						if (this.customStorageManager == null)
						{
							this.customStorageManager = new UserConfigurationManager(this.Store);
						}
					}
				}
				return this.customStorageManager;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000340 RID: 832
		public abstract string AuthenticationFailureString { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000B600 File Offset: 0x00009800
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000B607 File Offset: 0x00009807
		internal static int PodSiteStartRange { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000B60F File Offset: 0x0000980F
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000B616 File Offset: 0x00009816
		internal static int PodSiteEndRange { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000B61E File Offset: 0x0000981E
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000B625 File Offset: 0x00009825
		internal static string PodRedirectTemplate { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000B62D File Offset: 0x0000982D
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000B635 File Offset: 0x00009835
		internal ProtocolUser ProtocolUser
		{
			get
			{
				return this.protocolUser;
			}
			set
			{
				this.protocolUser = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000B63E File Offset: 0x0000983E
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000B646 File Offset: 0x00009846
		internal ResourceKey[] ResourceKeys { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600034B RID: 843
		protected abstract string ClientStringForMailboxSession { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600034C RID: 844
		protected abstract ExEventLog.EventTuple NoDefaultAcceptedDomainFoundEventTuple { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600034D RID: 845
		protected abstract BudgetType BudgetType { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000B64F File Offset: 0x0000984F
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000B657 File Offset: 0x00009857
		protected uint LoginAttempts
		{
			get
			{
				return this.loginAttempts;
			}
			set
			{
				this.loginAttempts = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000350 RID: 848
		protected abstract string AccountInvalidatedString { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000B660 File Offset: 0x00009860
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000B668 File Offset: 0x00009868
		protected bool ServerToServerAuthEnabled { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000B671 File Offset: 0x00009871
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000B679 File Offset: 0x00009879
		private protected bool ActualCafeAuthDone { protected get; private set; }

		// Token: 0x06000355 RID: 853 RVA: 0x0000B684 File Offset: 0x00009884
		public static bool IsXsoVersionChanged(int[] previousVersion)
		{
			return previousVersion == null || (previousVersion[0] < ResponseFactory.currentXsoVersion.FileMajorPart || (previousVersion[0] == ResponseFactory.currentXsoVersion.FileMajorPart && previousVersion[1] < ResponseFactory.currentXsoVersion.FileMinorPart) || (previousVersion[0] == ResponseFactory.currentXsoVersion.FileMajorPart && previousVersion[1] == ResponseFactory.currentXsoVersion.FileMinorPart && previousVersion[2] < ResponseFactory.currentXsoVersion.FileBuildPart) || (previousVersion[0] == ResponseFactory.currentXsoVersion.FileMajorPart && previousVersion[1] == ResponseFactory.currentXsoVersion.FileMinorPart && previousVersion[2] == ResponseFactory.currentXsoVersion.FileBuildPart && previousVersion[3] < ResponseFactory.currentXsoVersion.FilePrivatePart));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B734 File Offset: 0x00009934
		public static void RecordCurrentXsoVersion(Folder folder)
		{
			try
			{
				folder[FolderSchema.PopImapConversionVersion] = "15.00.1497.010";
				folder.Save();
			}
			catch (LocalizedException)
			{
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B770 File Offset: 0x00009970
		public static int[] GetPreviousXsoVersion(string fileVersion)
		{
			if (fileVersion == null)
			{
				return null;
			}
			string[] array = fileVersion.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				return null;
			}
			int[] array2 = new int[4];
			for (int i = 0; i < array2.Length; i++)
			{
				if (!int.TryParse(array[i], out array2[i]))
				{
					return null;
				}
			}
			return array2;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B7C8 File Offset: 0x000099C8
		public static AuthenticationMechanism GetAuthenticationMechanism(string authenticationMechanismString)
		{
			if (string.Compare(authenticationMechanismString, "ntlm", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!ProtocolBaseServices.Service.GSSAPIAndNTLMAuthDisabled)
				{
					return AuthenticationMechanism.Ntlm;
				}
				return AuthenticationMechanism.None;
			}
			else if (string.Compare(authenticationMechanismString, "gssapi", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!ProtocolBaseServices.Service.GSSAPIAndNTLMAuthDisabled)
				{
					return AuthenticationMechanism.Gssapi;
				}
				return AuthenticationMechanism.None;
			}
			else if (string.Compare(authenticationMechanismString, "kerberos", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!ProtocolBaseServices.Service.GSSAPIAndNTLMAuthDisabled)
				{
					return AuthenticationMechanism.Kerberos;
				}
				return AuthenticationMechanism.None;
			}
			else
			{
				if (string.Compare(authenticationMechanismString, "plain", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return AuthenticationMechanism.Plain;
				}
				return AuthenticationMechanism.None;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B840 File Offset: 0x00009A40
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
				this.disposed = true;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B85E File Offset: 0x00009A5E
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ResponseFactory>(this);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B866 File Offset: 0x00009A66
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B884 File Offset: 0x00009A84
		public ProtocolResponse ProcessCommand(byte[] buf, int offset, int size)
		{
			ProtocolResponse protocolResponse = null;
			if (!this.IsSessionValid(out protocolResponse))
			{
				return protocolResponse;
			}
			ProtocolRequest request = this.GenerateRequest(buf, offset, size);
			this.MarkBudgetForRequest(request);
			protocolResponse = this.ProcessRequest(request);
			if (!this.IsAuthenticated && ++this.preAuthCommands >= 9)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Session disconnected after 9 pre-auth commands.");
				protocolResponse.IsDisconnectResponse = true;
			}
			if (protocolResponse != null && protocolResponse.IsCommandFailedResponse && ++this.failedCommands >= 10)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Session disconnected after 10 failed commands.");
				protocolResponse.IsDisconnectResponse = true;
			}
			return protocolResponse;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B938 File Offset: 0x00009B38
		public void RecordCommandStart()
		{
			this.stopwatch.Start();
			if (this.ActivityId != Guid.Empty)
			{
				ActivityContextState activityContextState = new ActivityContextState(new Guid?(this.ActivityId), new ConcurrentDictionary<Enum, object>());
				this.Session.ActivityScope = ActivityContext.Resume(activityContextState, null);
			}
			else
			{
				this.Session.ActivityScope = ActivityContext.Start(null);
			}
			if (this.Session.ActivityScope != null)
			{
				this.Session.ActivityScope.Component = ProtocolBaseServices.ServiceName;
				this.Session.ActivityScope.Action = this.CommandName;
			}
			this.Session.SetBudgetDiagnosticValues(true);
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ActivityScope = this.Session.ActivityScope;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public void RecordCommandEnd()
		{
			if (!this.stopwatch.IsRunning)
			{
				return;
			}
			this.stopwatch.Stop();
			int num = (int)this.stopwatch.ElapsedMilliseconds;
			if (num == 0 && this.stopwatch.ElapsedTicks > 0L)
			{
				num = 1;
			}
			this.stopwatch.Reset();
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ProcessingTime = num;
			}
			this.session.SetDiagnosticValue(ConditionalHandlerSchema.ElapsedTime, TimeSpan.FromMilliseconds((double)num));
			double num2;
			if (this.TryGetActivityContextStat(ActivityOperationType.UserDelay, out num2))
			{
				this.session.SetDiagnosticValue(ConditionalHandlerSchema.BudgetDelay, num2);
			}
			if (this.TryGetActivityContextStat(ActivityOperationType.BudgetUsed, out num2))
			{
				this.session.SetDiagnosticValue(ConditionalHandlerSchema.BudgetUsed, num2);
			}
			this.Session.SetBudgetDiagnosticValues(false);
			if (this.Session.ActivityScope != null)
			{
				this.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.RequestId, this.Session.ActivityScope.ActivityId.ToString());
			}
			ExPerformanceCounter averageCommandProcessingTime = this.session.VirtualServer.AverageCommandProcessingTime;
			bool flag = this.session.VerifyMailboxLogEnabled();
			if (this.session.ActivityScope != null)
			{
				List<KeyValuePair<string, object>> formattableStatistics = this.Session.ActivityScope.GetFormattableStatistics();
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				float num6 = 0f;
				foreach (KeyValuePair<string, object> keyValuePair in formattableStatistics)
				{
					if (keyValuePair.Key.StartsWith("MailboxCall.AvgLatency"))
					{
						num3 = (float)keyValuePair.Value;
					}
					if (keyValuePair.Key.StartsWith("ADRead.AvgLatency"))
					{
						num4 = (float)keyValuePair.Value;
					}
					if (keyValuePair.Key.StartsWith("ADWrite.AvgLatency"))
					{
						num5 = (float)keyValuePair.Value;
					}
					if (keyValuePair.Key.StartsWith("ADSearch.AvgLatency"))
					{
						num6 = (float)keyValuePair.Value;
					}
				}
				if (num3 > 0f)
				{
					RatePerfCounters.IncrementLatencyPerfCounter(this.session.VirtualServer.RpcLatencyCounterIndex, (long)num3);
				}
				if (num4 + num5 + num6 > 0f)
				{
					RatePerfCounters.IncrementLatencyPerfCounter(this.session.VirtualServer.LdapLatencyCounterIndex, (long)(num4 + num5 + num6));
				}
				if (flag && this.session.ActivityScope != null)
				{
					this.session.LogInformation(LogRowFormatter.FormatCollection(this.Session.ActivityScope.GetFormattableStatistics()), null);
				}
				PopImapRequestData popImapRequestData = PopImapRequestCache.Instance.Get(this.Session.ActivityScope.ActivityId);
				popImapRequestData.CommandName = this.Session.ResponseFactory.CommandName;
				popImapRequestData.LdapLatency = (double)(num4 + num5 + num6);
				popImapRequestData.RpcLatency = (double)num3;
				popImapRequestData.ServerName = Environment.MachineName;
				popImapRequestData.UserEmail = this.Session.ResponseFactory.UserName;
				popImapRequestData.Parameters = this.Session.ResponseFactory.Parameters;
				if (this.Session.ActivityScope != null)
				{
					this.Session.ActivityScope.Component = null;
					this.Session.ActivityScope.Action = null;
				}
				this.Session.ActivityScope.End();
				this.Session.ActivityScope = null;
			}
			lock (ResponseFactory.commandProcessingTimeSamples)
			{
				int num7 = ResponseFactory.commandProcessingTimeSamples[ResponseFactory.insertionIndex];
				ResponseFactory.commandProcessingTimeSamples[ResponseFactory.insertionIndex] = num;
				ResponseFactory.insertionIndex = (ResponseFactory.insertionIndex + 1) % ResponseFactory.commandProcessingTimeSamples.Length;
				if (ResponseFactory.numSamples < ResponseFactory.commandProcessingTimeSamples.Length)
				{
					ResponseFactory.numSamples++;
				}
				ResponseFactory.latencySum += num;
				ResponseFactory.latencySum -= num7;
				averageCommandProcessingTime.RawValue = (long)(ResponseFactory.latencySum / ResponseFactory.numSamples);
			}
		}

		// Token: 0x0600035F RID: 863
		public abstract ProtocolRequest GenerateRequest(byte[] buf, int offset, int size);

		// Token: 0x06000360 RID: 864 RVA: 0x0000BE34 File Offset: 0x0000A034
		public ProtocolResponse ProcessRequest(ProtocolRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (this.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Session disposed");
				return null;
			}
			ProtocolSession protocolSession = this.Session;
			if (protocolSession == null)
			{
				return null;
			}
			VirtualServer virtualServer = protocolSession.VirtualServer;
			if (virtualServer == null)
			{
				return null;
			}
			virtualServer.Requests_Total.Increment();
			if (request.PerfCounterTotal != null)
			{
				request.PerfCounterTotal.Increment();
			}
			if (!request.VerifyState())
			{
				if (request.PerfCounterFailures != null)
				{
					request.PerfCounterFailures.Increment();
				}
				virtualServer.Requests_Failure.Increment();
				protocolSession.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "Err");
				return this.ProcessInvalidState(request);
			}
			request.ParseArguments();
			if (request.ParseResult != ParseResult.notYetParsed && request.ParseResult != ParseResult.success)
			{
				protocolSession.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "Err");
				return this.ProcessParseError(request);
			}
			if (!request.IsComplete)
			{
				protocolSession.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "Err");
				return this.ProcessIncompleteRequest(request);
			}
			ProtocolBaseServices.Assert(request.ParseResult == ParseResult.success, "Unexpected parse result {0}.", new object[]
			{
				request.ParseResult
			});
			return this.ConnectToTheStoreAndProcessTheRequest(request);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000BF6A File Offset: 0x0000A16A
		public virtual void PreProcessRequest(ProtocolRequest request)
		{
			if (this.NeedToReloadStoreStates)
			{
				this.ReloadStoreStates();
				this.NeedToReloadStoreStates = false;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000BF84 File Offset: 0x0000A184
		public ProtocolResponse ConnectToTheStoreAndProcessTheRequest(ProtocolRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			ProtocolBaseServices.SessionTracer.TraceDebug<ProtocolRequest>(this.session.SessionId, "Calling Process: {0}", request);
			ProtocolResponse protocolResponse = null;
			ProtocolResponse result;
			try
			{
				if (this.store != null)
				{
					bool flag = request.NeedsStoreConnection || this.NeedToReloadStoreStates;
					try
					{
						if (flag)
						{
							Monitor.Enter(this.store);
						}
						if (flag)
						{
							this.ConnectToTheStore();
						}
						this.PreProcessRequest(request);
						protocolResponse = request.Process();
					}
					finally
					{
						if (flag)
						{
							this.DisconnectFromTheStore();
							if (Monitor.IsEntered(this.store))
							{
								Monitor.Exit(this.store);
							}
						}
					}
					result = protocolResponse;
				}
				else
				{
					this.session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "OK");
					result = request.Process();
				}
			}
			catch (Exception ex)
			{
				this.session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "Err");
				if (protocolResponse != null)
				{
					protocolResponse.Dispose();
				}
				IProxyLogin proxyLogin = request as IProxyLogin;
				if (proxyLogin != null)
				{
					proxyLogin.AuthenticationError = ex.GetType().ToString() + " " + ex.Message;
				}
				if (!this.session.CheckNonCriticalException(ex))
				{
					throw;
				}
				result = this.ProcessException(request, ex);
			}
			return result;
		}

		// Token: 0x06000363 RID: 867
		public abstract ProtocolResponse ProcessInvalidState(ProtocolRequest request);

		// Token: 0x06000364 RID: 868
		public abstract ProtocolResponse ProcessParseError(ProtocolRequest request);

		// Token: 0x06000365 RID: 869
		public abstract ProtocolResponse CommandIsNotAllASCII(byte[] buf, int offset, int size);

		// Token: 0x06000366 RID: 870 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public virtual ProtocolResponse ProcessIncompleteRequest(ProtocolRequest request)
		{
			ProtocolBaseServices.Assert(false, "ProcessIncompleteRequest is not implemented.", new object[0]);
			return null;
		}

		// Token: 0x06000367 RID: 871
		public abstract ProtocolResponse ProcessException(ProtocolRequest request, Exception exception);

		// Token: 0x06000368 RID: 872
		public abstract ProtocolResponse ProcessException(ProtocolRequest request, Exception exception, string responseString);

		// Token: 0x06000369 RID: 873 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public ProtocolResponse DoAuthenticate(ProtocolRequest request, AuthenticationMechanism authenticationMechanism)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request is null");
			}
			this.incompleteRequest = request;
			this.authenticationMechanism = authenticationMechanism;
			this.serverContext = this.CreateServerAuthenticationContext();
			SecurityStatus securityStatus;
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox && this.authenticationMechanism == AuthenticationMechanism.Kerberos)
			{
				securityStatus = SecurityStatus.OK;
			}
			else
			{
				securityStatus = this.serverContext.InitializeForInboundNegotiate(authenticationMechanism);
			}
			if (securityStatus != SecurityStatus.OK)
			{
				throw new LocalizedException(new LocalizedString("InitializeForInboundNegotiate failed with " + securityStatus));
			}
			this.Session.Connection.MaxLineLength = 4096;
			ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "StartAuthentication completed");
			return new ProtocolResponse(this.FirstAuthenticateResponse);
		}

		// Token: 0x0600036A RID: 874
		public abstract ProtocolResponse AuthenticationDone(ProtocolRequest authenticateRequest, ResponseFactory.AuthenticationResult authenticationResult);

		// Token: 0x0600036B RID: 875 RVA: 0x0000C190 File Offset: 0x0000A390
		public ProtocolResponse ProcessAuthentication(byte[] buf, int offset, int size)
		{
			IProxyLogin proxyLogin = (IProxyLogin)this.IncompleteRequest;
			if (size > 0 && buf[offset] == 42)
			{
				this.SetSessionError("AuthCancelled");
				return this.AuthenticationDone(ResponseFactory.AuthenticationResult.cancel);
			}
			int inputLength = size;
			SecurityStatus securityStatus;
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox && this.authenticationMechanism == AuthenticationMechanism.Kerberos)
			{
				string text;
				if (!this.ExtractCafeHostname(buf, offset, size, out inputLength, out text))
				{
					return this.AuthenticationDone(ResponseFactory.AuthenticationResult.failure);
				}
				securityStatus = this.serverContext.InitializeForInboundExchangeAuth("SHA256", "IMAP/" + text, this.Session.VirtualServer.Certificate.GetPublicKey(), this.Session.Connection.TlsEapKey);
				if (securityStatus != SecurityStatus.OK)
				{
					this.Session.LogInformation("ExchangeAuth initialization failed for host {0} with status {1}", new object[]
					{
						text,
						securityStatus
					});
					return this.AuthenticationDone(ResponseFactory.AuthenticationResult.failure);
				}
			}
			else if (!this.ExtractClientIP(buf, offset, size, out inputLength))
			{
				return this.AuthenticationDone(ResponseFactory.AuthenticationResult.failure);
			}
			byte[] array;
			securityStatus = this.serverContext.NegotiateSecurityContext(buf, offset, inputLength, out array);
			if (!string.IsNullOrEmpty(this.serverContext.UserName))
			{
				this.UserName = this.serverContext.UserName;
			}
			SecurityStatus securityStatus2 = securityStatus;
			if (securityStatus2 <= SecurityStatus.LogonDenied)
			{
				if (securityStatus2 != SecurityStatus.InvalidToken && securityStatus2 != SecurityStatus.LogonDenied)
				{
				}
			}
			else if (securityStatus2 != SecurityStatus.IllegalMessage)
			{
				if (securityStatus2 != SecurityStatus.OK)
				{
					if (securityStatus2 == SecurityStatus.ContinueNeeded)
					{
						if (array == null)
						{
							throw new InvalidOperationException("No AUTH blob to send");
						}
						ProtocolResponse protocolResponse = new ProtocolResponse("+ ");
						protocolResponse.Append(Encoding.ASCII.GetString(array));
						return protocolResponse;
					}
				}
				else
				{
					if (array != null && array.Length > 0)
					{
						ProtocolResponse protocolResponse = new ProtocolResponse("+ ");
						protocolResponse.Append(Encoding.ASCII.GetString(array));
						if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox && this.authenticationMechanism == AuthenticationMechanism.Kerberos)
						{
							protocolResponse.Append("\r\n");
							protocolResponse.Append(this.AuthenticationDone(ResponseFactory.AuthenticationResult.authenticatedAsCafe).DataToSend);
							if (this.Session.LightLogSession != null)
							{
								this.Session.LightLogSession.LiveIdAuthResult = "AuthenticatedAsCafe";
							}
						}
						return protocolResponse;
					}
					if (this.Session.LightLogSession != null)
					{
						if ((this.authenticationMechanism == AuthenticationMechanism.Plain || this.authenticationMechanism == AuthenticationMechanism.Login) && proxyLogin != null && proxyLogin.LiveIdBasicAuth != null)
						{
							this.Session.LightLogSession.LiveIdAuthResult = proxyLogin.LiveIdBasicAuth.LastAuthResult.ToString();
						}
						else
						{
							this.Session.LightLogSession.LiveIdAuthResult = SecurityStatus.OK.ToString();
						}
					}
					return this.AuthenticationDone(ResponseFactory.AuthenticationResult.success);
				}
			}
			string text2 = securityStatus.ToString();
			string text3 = text2;
			string text4 = string.Empty;
			if (proxyLogin != null && proxyLogin.LiveIdBasicAuth != null)
			{
				text4 = proxyLogin.LiveIdBasicAuth.LastAuthResult.ToString();
				if (proxyLogin.LiveIdBasicAuth.LastAuthResult != LiveIdAuthResult.Success)
				{
					text3 = text3 + "-" + text4;
				}
				if (!string.IsNullOrEmpty(proxyLogin.LiveIdBasicAuth.LastRequestErrorMessage))
				{
					text2 = text2 + "-" + proxyLogin.LiveIdBasicAuth.LastRequestErrorMessage.Replace('"', '\'').Replace("\r\n", " ");
				}
			}
			this.Session.LogInformation("{0} authentication failed, {1}", new object[]
			{
				this.authenticationMechanism,
				text3
			});
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ErrorMessage = "AuthFailed:" + text3;
				this.Session.LightLogSession.LiveIdAuthResult = text4;
			}
			this.SetSessionError(string.Format("AuthFailed:{0},User: {1}", text2, this.serverContext.UserName ?? "not found"));
			return this.AuthenticationDone(ResponseFactory.AuthenticationResult.failure);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C554 File Offset: 0x0000A754
		public void AddExceptionToCache(Exception exception)
		{
			if (this.Session.ActivityScope != null)
			{
				Guid activityId = this.Session.ActivityScope.ActivityId;
				PopImapRequestData popImapRequestData = PopImapRequestCache.Instance.Get(this.Session.ActivityScope.ActivityId);
				popImapRequestData.HasErrors = true;
				if (popImapRequestData.ErrorDetails == null)
				{
					popImapRequestData.ErrorDetails = new List<ErrorDetail>();
				}
				popImapRequestData.ErrorDetails.Add(new ErrorDetail
				{
					UserEmail = this.Session.ResponseFactory.UserName,
					StackTrace = exception.StackTrace,
					ErrorMessage = exception.Message,
					ErrorType = exception.GetType().ToString()
				});
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C608 File Offset: 0x0000A808
		public void LogHandledException(Exception exception)
		{
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ExceptionCaught = exception;
			}
			this.SetSessionError(exception);
			if (exception is ObjectNotFoundException || exception is OverBudgetException)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<Type, string, Exception>(this.session.SessionId, "Exception {0} caught for user {1}: {2}", exception.GetType(), this.UserName, exception);
				return;
			}
			ProtocolBaseServices.SessionTracer.TraceError<string, Exception>(this.session.SessionId, "Exception caught for user {0}: {1}", this.UserName, exception);
			if (exception is ConnectionFailedTransientException || exception is ConnectionFailedPermanentException)
			{
				this.session.BeginShutdown(this.ProcessException(null, exception).DataToSend);
			}
		}

		// Token: 0x0600036E RID: 878
		public abstract void DoPostLoginTasks();

		// Token: 0x0600036F RID: 879
		public abstract bool DoProxyConnect(byte[] buf, int offset, int size, ProxySession proxySession);

		// Token: 0x06000370 RID: 880 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public OutboundConversionOptions GetOutboundConversionOptions()
		{
			if (this.options == null)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.protocolUser.OrganizationId), 1858, "GetOutboundConversionOptions", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs");
				tenantOrRootOrgRecipientSession.SessionSettings.AccountingObject = this.Session.Budget;
				this.options = new OutboundConversionOptions(this.DefaultAcceptedDomainName);
				this.options.UserADSession = tenantOrRootOrgRecipientSession;
				this.options.LoadPerOrganizationCharsetDetectionOptions(this.protocolUser.OrganizationId);
				if (this.options.ByteEncoderTypeFor7BitCharsets.Equals(ByteEncoderTypeFor7BitCharsetsEnum.Undefined))
				{
					string value = ConfigurationManager.AppSettings["ByteEncoderTypeFor7BitCharsets"];
					ByteEncoderTypeFor7BitCharsets byteEncoderTypeFor7BitCharsets;
					if (!string.IsNullOrEmpty(value) && EnumValidator.TryParse<ByteEncoderTypeFor7BitCharsets>(value, EnumParseOptions.AllowNumericConstants | EnumParseOptions.IgnoreCase, out byteEncoderTypeFor7BitCharsets))
					{
						this.options.ByteEncoderTypeFor7BitCharsets = byteEncoderTypeFor7BitCharsets;
					}
				}
				MimeTextFormat mimeTextFormat = this.protocolUser.UseProtocolDefaults ? this.session.Server.MessagesRetrievalMimeTextFormat : this.protocolUser.MessagesRetrievalMimeTextFormat;
				this.options.IsSenderTrusted = false;
				this.options.DemoteBcc = true;
				this.options.InternetMessageFormat = InternetMessageFormat.Mime;
				switch (mimeTextFormat)
				{
				case MimeTextFormat.TextOnly:
					this.options.InternetTextFormat = InternetTextFormat.TextOnly;
					break;
				case MimeTextFormat.HtmlOnly:
					this.options.InternetTextFormat = InternetTextFormat.HtmlOnly;
					break;
				case MimeTextFormat.HtmlAndTextAlternative:
					this.options.InternetTextFormat = InternetTextFormat.HtmlAndTextAlternative;
					break;
				case MimeTextFormat.TextEnrichedOnly:
					this.options.InternetTextFormat = InternetTextFormat.TextEnrichedOnly;
					break;
				case MimeTextFormat.TextEnrichedAndTextAlternative:
					this.options.InternetTextFormat = InternetTextFormat.TextEnrichedAndTextAlternative;
					break;
				case MimeTextFormat.BestBodyFormat:
					this.options.InternetTextFormat = InternetTextFormat.BestBody;
					break;
				case MimeTextFormat.Tnef:
					this.options.InternetMessageFormat = InternetMessageFormat.Tnef;
					this.options.InternetTextFormat = InternetTextFormat.TextOnly;
					break;
				}
				if (!string.IsNullOrEmpty(this.owaServer))
				{
					try
					{
						this.options.OwaServer = this.owaServer;
					}
					catch (ArgumentException ex)
					{
						ProtocolBaseServices.SessionTracer.TraceError<string, string>(this.session.SessionId, "Unable to set OWA server url for user {0}.\r\n{1}", this.UserName, ex.Message);
						ProtocolBaseServices.LogEvent(this.protocolUser.OwaServerInvalidEventTuple, this.UserName, new string[]
						{
							this.UserName,
							ex.Message
						});
					}
				}
				this.options.ClearCategories = false;
				string value2 = ConfigurationManager.AppSettings["QuoteDisplayNameBeforeRfc2047Encoding"];
				bool quoteDisplayNameBeforeRfc2047Encoding = false;
				bool.TryParse(value2, out quoteDisplayNameBeforeRfc2047Encoding);
				this.options.QuoteDisplayNameBeforeRfc2047Encoding = quoteDisplayNameBeforeRfc2047Encoding;
				string text = ConfigurationManager.AppSettings["OverrideCalendarMessageCharset"];
				Charset charset;
				if (!string.IsNullOrEmpty(text) && Charset.TryGetCharset(text, out charset) && charset.IsAvailable)
				{
					this.options.DetectionOptions.PreferredCharset = charset;
				}
				if (this.recipientCacheResetTimer == null)
				{
					this.recipientCacheResetTimer = new Timer(new TimerCallback(this.ResetRecipientCache), this.options, ResponseFactory.DefaultRecipientCacheResetInterval, ResponseFactory.DefaultRecipientCacheResetInterval);
				}
			}
			return this.options;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		public InboundConversionOptions GetInboundConversionOptions()
		{
			if (this.inboundOptions == null)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.protocolUser.OrganizationId), 1976, "GetInboundConversionOptions", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs");
				tenantOrRootOrgRecipientSession.SessionSettings.AccountingObject = this.Session.Budget;
				this.inboundOptions = new InboundConversionOptions(this.DefaultAcceptedDomainName);
				this.inboundOptions.UserADSession = tenantOrRootOrgRecipientSession;
				this.inboundOptions.LoadPerOrganizationCharsetDetectionOptions(this.protocolUser.OrganizationId);
			}
			return this.inboundOptions;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public bool ConnectToTheStore()
		{
			bool flag = false;
			if (this.store != null)
			{
				flag = this.Store.ConnectWithStatus();
				if (flag)
				{
					this.NeedToReloadStoreStates = true;
				}
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Store.Connected");
			}
			return flag;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public void DisconnectFromTheStore()
		{
			if (this.IsStoreConnected)
			{
				this.Store.Disconnect();
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Store.Disconnected");
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		internal bool DeleteMessages(StoreObjectId[] messages)
		{
			if (messages.Length == 0)
			{
				return true;
			}
			List<StoreObjectId> list;
			if (messages.Length <= 256)
			{
				list = new List<StoreObjectId>(messages.Length);
				for (int i = 0; i < messages.Length; i++)
				{
					if (messages[i] != null)
					{
						list.Add(messages[i]);
					}
				}
				if (list.Count > 0)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "Delete {0} items", list.Count);
					AggregateOperationResult aggregateOperationResult = this.store.Delete(DeleteItemFlags.SoftDelete, list.ToArray());
					return aggregateOperationResult.OperationResult == OperationResult.Succeeded;
				}
			}
			list = new List<StoreObjectId>(256);
			int num = 2;
			for (int j = 0; j < messages.Length; j++)
			{
				if (messages[j] != null)
				{
					list.Add(messages[j]);
				}
				if (list.Count >= 256 || j == messages.Length - 1)
				{
					this.BackOffFromStore(ref num);
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "Delete {0} items", list.Count);
					AggregateOperationResult aggregateOperationResult2 = this.store.Delete(DeleteItemFlags.SoftDelete, list.ToArray());
					if (aggregateOperationResult2.OperationResult != OperationResult.Succeeded)
					{
						return false;
					}
					list.Clear();
				}
			}
			return true;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		internal void MarkAsRead(StoreObjectId[] messages)
		{
			if (messages.Length == 0)
			{
				return;
			}
			List<StoreObjectId> list;
			if (messages.Length <= 256)
			{
				list = new List<StoreObjectId>(messages.Length);
				for (int i = 0; i < messages.Length; i++)
				{
					if (messages[i] != null)
					{
						list.Add(messages[i]);
					}
				}
				ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "MarkAsRead {0} items", list.Count);
				this.store.MarkAsRead(this.SuppressReadReceipt, this.SuppressReadReceipt, list.ToArray());
				return;
			}
			list = new List<StoreObjectId>(256);
			int num = 2;
			for (int j = 0; j < messages.Length; j++)
			{
				if (messages[j] != null)
				{
					list.Add(messages[j]);
				}
				if (list.Count >= 256 || j == messages.Length - 1)
				{
					this.BackOffFromStore(ref num);
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "MarkAsRead {0} items", list.Count);
					this.store.MarkAsRead(this.SuppressReadReceipt, this.SuppressReadReceipt, list.ToArray());
					list.Clear();
				}
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CCEF File Offset: 0x0000AEEF
		internal AggregateOperationResult MoveItems(Folder folder, StoreObjectId destinationUid, bool returnNewIds, StoreObjectId[] messages)
		{
			return this.CopyorMoveItems(folder, destinationUid, returnNewIds, messages, false);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CCFD File Offset: 0x0000AEFD
		internal AggregateOperationResult CopyItems(Folder folder, StoreObjectId destinationUid, bool returnNewIds, StoreObjectId[] messages)
		{
			return this.CopyorMoveItems(folder, destinationUid, returnNewIds, messages, true);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		internal OperationResult MoveItems(Folder folder, StoreObjectId destinationUid, StoreObjectId[] messages)
		{
			if (messages.Length == 0)
			{
				return OperationResult.Succeeded;
			}
			if (messages.Length <= 256)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "MoveItems {0} items", messages.Length);
				return folder.MoveItems(destinationUid, messages).OperationResult;
			}
			List<StoreObjectId> list = new List<StoreObjectId>(256);
			int num = 2;
			for (int i = 0; i < messages.Length; i++)
			{
				list.Add(messages[i]);
				if (list.Count >= 256 || i == messages.Length - 1)
				{
					this.BackOffFromStore(ref num);
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "MoveItems {0} items", list.Count);
					OperationResult operationResult = folder.MoveItems(destinationUid, list.ToArray()).OperationResult;
					if (operationResult != OperationResult.Succeeded)
					{
						return operationResult;
					}
					list.Clear();
				}
			}
			return OperationResult.Succeeded;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CDD6 File Offset: 0x0000AFD6
		internal void BackOffFromStore(ref int backoffDelay)
		{
			if (this.store.IsInBackoffState)
			{
				Thread.Sleep(backoffDelay);
				if (backoffDelay < 1024)
				{
					backoffDelay <<= 2;
					return;
				}
			}
			else
			{
				backoffDelay = 2;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000CE00 File Offset: 0x0000B000
		internal IStandardBudget AcquirePerCommandBudget()
		{
			if (!this.IsAuthenticated || this.Session.Budget == null)
			{
				return null;
			}
			IStandardBudget budget = this.Session.Budget;
			bool flag = false;
			IStandardBudget standardBudget = StandardBudget.Acquire(budget.Owner);
			IStandardBudget result;
			try
			{
				standardBudget.CheckOverBudget();
				ResourceLoadDelayInfo.CheckResourceHealth(standardBudget, this.Session.WorkloadSettings, this.ResourceKeys);
				standardBudget.StartLocal("ResponseFactory.AcquirePerCommandBudget", default(TimeSpan));
				flag = true;
				result = standardBudget;
			}
			finally
			{
				if (!flag)
				{
					standardBudget.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CE94 File Offset: 0x0000B094
		protected static bool CanProxyTo(Service service, ExchangePrincipal exchangePrincipal)
		{
			ServerVersion serverVersion = new ServerVersion(service.ServerVersionNumber);
			ServerVersion serverVersion2 = new ServerVersion(exchangePrincipal.MailboxInfo.Location.ServerVersion);
			return (serverVersion.Major == Server.Exchange2011MajorVersion && serverVersion2.Major == Server.Exchange2011MajorVersion) || (serverVersion.Major == Server.Exchange2009MajorVersion && serverVersion2.Major == Server.Exchange2009MajorVersion) || (serverVersion.Major == Server.Exchange2007MajorVersion && serverVersion2.Major == Server.Exchange2007MajorVersion);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000CF18 File Offset: 0x0000B118
		protected virtual void MarkBudgetForRequest(ProtocolRequest request)
		{
			if (this.Session.Budget != null && this.Session.Budget.LocalCostHandle != null)
			{
				this.Session.Budget.LocalCostHandle.MaxLiveTime = request.GetBudgetActionTimeout();
			}
		}

		// Token: 0x0600037D RID: 893
		protected abstract void ReloadStoreStates();

		// Token: 0x0600037E RID: 894
		protected abstract int GetE15MbxProxyPort(string e15MbxFqdn);

		// Token: 0x0600037F RID: 895
		protected abstract int GetE15MbxProxyPort(string e15MbxFqdn, bool isCrossForest, string userDomain);

		// Token: 0x06000380 RID: 896 RVA: 0x0000CF54 File Offset: 0x0000B154
		protected int GetE15MbxProxyPort<T>(string e15MbxFqdn, MruDictionaryCache<string, int> proxyPortCache, bool isCrossForest = false, string userDomain = "") where T : PopImapAdConfiguration, new()
		{
			int num = this.session.Server.ProxyPort;
			if (!proxyPortCache.TryGetValue(e15MbxFqdn, out num))
			{
				num = this.session.Server.ProxyPort;
				try
				{
					ITopologyConfigurationSession topologyConfigurationSession;
					if (isCrossForest && !string.IsNullOrEmpty(userDomain))
					{
						string resourceForestFqdnByAcceptedDomainName = ResponseFactory.GetResourceForestFqdnByAcceptedDomainName(userDomain);
						topologyConfigurationSession = ADSystemConfigurationSession.CreateRemoteForestSession(resourceForestFqdnByAcceptedDomainName, null);
					}
					else
					{
						topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2438, "GetE15MbxProxyPort", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs");
					}
					if (topologyConfigurationSession != null)
					{
						PopImapAdConfiguration popImapAdConfiguration = PopImapAdConfiguration.FindOne<T>(topologyConfigurationSession, e15MbxFqdn);
						if (popImapAdConfiguration != null)
						{
							num = popImapAdConfiguration.ProxyTargetPort;
						}
						if (num > 0 && num < 65535)
						{
							proxyPortCache[e15MbxFqdn] = num;
						}
					}
				}
				catch (Exception exception)
				{
					if (!this.Session.CheckNonCriticalException(exception))
					{
						throw;
					}
				}
			}
			if (num <= 0 || num >= 65535)
			{
				ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, "Proxy port could not be found.");
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.Message = "ProxyTargetPort from Config not found. Use Default port.";
				}
				num = (string.Equals(ProtocolBaseServices.ServiceName, "POP3", StringComparison.InvariantCultureIgnoreCase) ? 1995 : 1993);
			}
			ProtocolBaseServices.SessionTracer.TraceError<string, int>(this.session.SessionId, "Proxy port for {0} is {1}.", e15MbxFqdn, num);
			return num;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ReleaseResources();
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.recipientCacheResetTimer != null)
				{
					this.recipientCacheResetTimer.Dispose();
					this.recipientCacheResetTimer = null;
				}
			}
		}

		// Token: 0x06000382 RID: 898
		protected abstract ProxySession NewProxySession(NetworkConnection connection);

		// Token: 0x06000383 RID: 899 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		protected bool TryToConnect(SecureString password, out bool loginSucceeded)
		{
			loginSucceeded = false;
			if (string.IsNullOrEmpty(this.UserName))
			{
				ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, "Empty user name.");
				return false;
			}
			if (this.clientSecurityContext != null)
			{
				((IDisposable)this.clientSecurityContext).Dispose();
				this.clientSecurityContext = null;
			}
			bool result;
			try
			{
				loginSucceeded = this.Logon(password, out this.clientSecurityContext);
				if (!loginSucceeded)
				{
					result = false;
				}
				else
				{
					bool flag = this.TryToConnect(this.clientSecurityContext);
					loginSucceeded = string.IsNullOrEmpty(((IProxyLogin)this.incompleteRequest).AuthenticationError);
					result = flag;
				}
			}
			finally
			{
				using (this.clientSecurityContext)
				{
				}
			}
			return result;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		internal bool TryToConnect(string cat, string mailbox, string cafeActivityId)
		{
			if (this.clientSecurityContext != null)
			{
				((IDisposable)this.clientSecurityContext).Dispose();
				this.clientSecurityContext = null;
			}
			bool result;
			try
			{
				this.clientSecurityContext = this.GetClientSecurityContext(cat);
				if (!string.IsNullOrEmpty(mailbox) && !mailbox.Equals("\"\""))
				{
					this.Mailbox = mailbox;
				}
				if (this.ActivityId == Guid.Empty)
				{
					this.ActivityId = Guid.Parse(cafeActivityId);
					if (this.Session.LightLogSession != null)
					{
						this.Session.LightLogSession.CafeActivityId = cafeActivityId;
					}
				}
				result = this.TryToConnect(this.clientSecurityContext);
			}
			finally
			{
				this.Session.SetMaxCommandLength(this.Session.Server.MaxCommandLength);
				using (this.clientSecurityContext)
				{
				}
			}
			return result;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D298 File Offset: 0x0000B498
		protected bool TryToConnect(ClientSecurityContext clientSecurityContext)
		{
			if (!this.CheckConnectionLimit())
			{
				return false;
			}
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			bool flag = false;
			bool result;
			try
			{
				if (proxyLogin.AdUser == null)
				{
					proxyLogin.AdUser = this.protocolUser.FindAdUser(proxyLogin.UserName, this.userSid, this.userPuid);
				}
				if (proxyLogin.AdUser == null || !this.ConfigureUser(proxyLogin.AdUser))
				{
					ProtocolBaseServices.SessionTracer.TraceError<string>(this.session.SessionId, "User {0} could not be found in Active Directory.", this.Session.GetUserNameForLogging());
					result = false;
				}
				else
				{
					SmtpAddress primarySmtpAddress = proxyLogin.AdUser.PrimarySmtpAddress;
					if (string.IsNullOrEmpty(proxyLogin.AdUser.PrimarySmtpAddress.Domain))
					{
						ProtocolBaseServices.SessionTracer.TraceError<string>(this.session.SessionId, "User {0} has no PrimarySmtpAddress in AD.", proxyLogin.UserName);
						this.SetSessionError("NoPrimarySmtpAddress");
						result = false;
					}
					else
					{
						this.userSid = proxyLogin.AdUser.Sid;
						this.AcquireSessionBudget();
						if (ResponseFactory.UsePrimarySmtpAddressEnabled)
						{
							this.UserName = proxyLogin.AdUser.PrimarySmtpAddress.ToString();
						}
						else
						{
							this.UserName = (this.UseSamAccountNameAsUsername ? proxyLogin.AdUser.SamAccountName : proxyLogin.AdUser.Alias);
						}
						if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)
						{
							if (!this.OpenMailboxSession(clientSecurityContext, proxyLogin.AdUser))
							{
								return false;
							}
						}
						else
						{
							if (ProtocolBaseServices.ServerRoleService != ServerServiceRole.cafe)
							{
								this.SetSessionError("UnknownServerRole");
								return false;
							}
							if (!this.ProxyConnect(proxyLogin.AdUser))
							{
								return false;
							}
						}
						this.Session.RemoveFromUnauthenticatedConnectionsPerIp();
						flag = true;
						result = true;
					}
				}
			}
			catch (LocalizedException ex)
			{
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ExceptionCaught = ex;
				}
				this.SetSessionError(ex);
				result = false;
			}
			finally
			{
				if (!flag)
				{
					this.session.DisableUserTracing();
					this.ReleaseResources();
					this.protocolUser.Reset();
					if (string.IsNullOrEmpty(proxyLogin.AuthenticationError) && this.Session.LightLogSession != null)
					{
						proxyLogin.AuthenticationError = this.Session.LightLogSession.ErrorMessage;
					}
				}
			}
			return result;
		}

		// Token: 0x06000386 RID: 902
		protected abstract IEnumerable<EmailTransportService> GetProxyDestinations(ExchangePrincipal exchangePrincipal);

		// Token: 0x06000387 RID: 903 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		protected void TraceProxyResponse(byte[] buf, int offset, int size)
		{
			if (!this.disposed && ProtocolBaseServices.SessionTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				string @string = Encoding.ASCII.GetString(buf, offset, size);
				this.Session.LogInformation("<<< ProxyResponse:{0}<<<", new object[]
				{
					@string
				});
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D544 File Offset: 0x0000B744
		protected byte[] GetPlainAuthBlob()
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			if (proxyLogin == null || proxyLogin.Password == null)
			{
				return null;
			}
			byte[] result;
			using (AuthenticationContext authenticationContext = new AuthenticationContext())
			{
				SecurityStatus securityStatus = authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Plain, null, proxyLogin.UserName, null, proxyLogin.Password);
				if (securityStatus != SecurityStatus.OK)
				{
					throw new LocalizedException(new LocalizedString(string.Format("Unexpected response from InitializeForOutboundNegotiate: {0}", securityStatus)));
				}
				if (!string.IsNullOrEmpty(this.Mailbox))
				{
					authenticationContext.AuthorizationIdentity = Encoding.ASCII.GetBytes(this.Mailbox);
				}
				byte[] array;
				try
				{
					securityStatus = authenticationContext.NegotiateSecurityContext(null, out array);
				}
				catch (Exception exception)
				{
					if (!this.Session.CheckNonCriticalException(exception))
					{
						throw;
					}
					return null;
				}
				if (securityStatus != SecurityStatus.OK)
				{
					throw new LocalizedException(new LocalizedString(string.Format("Unexpected response from NegotiateSecurityContext: {0}", securityStatus)));
				}
				if (ProtocolBaseServices.GCCEnabledWithKeys || (ResponseFactory.GetClientAccessRulesEnabled() && this.clientAccessRulesSupportedByTargetServer))
				{
					string text = string.IsNullOrEmpty(proxyLogin.ClientIp) ? this.Session.Connection.RemoteEndPoint.Address.ToString() : proxyLogin.ClientIp;
					ProtocolBaseServices.FaultInjectionTracer.TraceTest<string>(2709925181U, ref text);
					if (!string.IsNullOrEmpty(text))
					{
						if (this.ActivityId == Guid.Empty && this.Session.ActivityScope != null)
						{
							this.ActivityId = this.Session.ActivityScope.ActivityId;
							if (this.Session.LightLogSession != null)
							{
								this.Session.LightLogSession.CafeActivityId = this.ActivityId.ToString();
							}
						}
						int num = array.Length;
						string text2 = this.ActivityId.ToString();
						string text3 = ProtocolBaseServices.GCCEnabledWithKeys ? GccUtils.GetAuthStringForThisServer() : string.Empty;
						string text4 = this.Session.Connection.RemoteEndPoint.Port.ToString();
						string s = (ResponseFactory.GetClientAccessRulesEnabled() && this.clientAccessRulesSupportedByTargetServer) ? string.Format("\0{0}\0{1}\0{2}\0{3}\0{4}\r\n", new object[]
						{
							text,
							text3,
							text2,
							text4,
							string.Empty
						}) : string.Format("\0{0}\0{1}\0{2}\r\n", text, text3, text2);
						byte[] bytes = Encoding.ASCII.GetBytes(s);
						Array.Resize<byte>(ref array, num + bytes.Length);
						Array.Copy(bytes, 0, array, num, bytes.Length);
						return array;
					}
				}
				Array.Resize<byte>(ref array, array.Length + 2);
				array[array.Length - 2] = 13;
				array[array.Length - 1] = 10;
				result = array;
			}
			return result;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D824 File Offset: 0x0000BA24
		protected string GetServerAuthXProxyCommand(string command)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			IProxyLogin proxyLogin = (IProxyLogin)this.incompleteRequest;
			stringBuilder.Append(command);
			stringBuilder.Append(" ");
			stringBuilder.Append(this.UserName);
			stringBuilder.Append(" ");
			stringBuilder.Append(this.catToken);
			stringBuilder.Append(" ");
			if (!string.IsNullOrEmpty(this.Mailbox))
			{
				stringBuilder.Append(this.Mailbox);
			}
			else
			{
				stringBuilder.Append("\"\"");
			}
			stringBuilder.Append(" ");
			if (this.ActivityId == Guid.Empty)
			{
				this.ActivityId = this.session.ActivityScope.ActivityId;
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.CafeActivityId = this.ActivityId.ToString();
				}
			}
			stringBuilder.Append(this.ActivityId);
			if (ProtocolBaseServices.GCCEnabledWithKeys)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(GccUtils.GetAuthStringForThisServer());
				stringBuilder.Append(" ");
				if (!string.IsNullOrEmpty(proxyLogin.ClientIp))
				{
					stringBuilder.Append(proxyLogin.ClientIp);
				}
				else if (this.Session.Connection.RemoteEndPoint.Address != null)
				{
					stringBuilder.Append(this.Session.Connection.RemoteEndPoint.Address.ToString());
				}
				stringBuilder.Append(" ");
				stringBuilder.Append(this.session.LocalEndPoint.Address);
			}
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		protected void ProxyConnectionEncryptionComplete(IAsyncResult iar)
		{
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.session.SessionId, "User {0} entering ResponseFactory.ProxyConnectionEncryptionComplete.", this.Session.GetUserNameForLogging());
				ProxySession proxySession = (ProxySession)iar.AsyncState;
				NetworkConnection connection = proxySession.Connection;
				if (connection == null)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "ProxyConnectionEncryptionComplete(): proxySession.Connection is null.");
					proxySession.State = ProxySession.ProxyState.failed;
				}
				else
				{
					object obj;
					connection.EndNegotiateTlsAsClient(iar, out obj);
					if (obj != null)
					{
						this.Session.LogInformation("ProxyConnectionEncryptionComplete(): EndNegotiateTlsAsClient error result: {0}", new object[]
						{
							obj
						});
						proxySession.State = ProxySession.ProxyState.failed;
					}
					else if (ProtocolBaseServices.EnforceCertificateErrors && !connection.RemoteCertificate.Verify())
					{
						this.Session.LogInformation("ProxyConnectionEncryptionComplete(): Invalid Certificate", new object[0]);
						proxySession.State = ProxySession.ProxyState.failed;
					}
					else if (this.Session.Disposed)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug(this.Session.SessionId, "Incoming session is disposed, nothing to do.");
						proxySession.State = ProxySession.ProxyState.failed;
					}
					else if (this.ProxyEncryptionType == EncryptionType.SSL)
					{
						proxySession.EnterReadLoop(connection);
					}
					else if (this.ProxyEncryptionType == EncryptionType.TLS)
					{
						proxySession.TransitProxyState(null, 0, 0);
					}
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted(this.session.SessionId);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000DB68 File Offset: 0x0000BD68
		protected void AcquireSessionBudget()
		{
			if (this.Session.Budget != null)
			{
				return;
			}
			IStandardBudget standardBudget = null;
			lock (this.Session)
			{
				if (this.Session.Budget != null)
				{
					return;
				}
				this.Session.Budget = ((this.userSid != null) ? StandardBudget.Acquire(this.userSid, this.BudgetType, this.protocolUser.GetSessionSettings()) : StandardBudget.AcquireFallback(this.UserName, this.BudgetType));
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.Budget = this.Session.Budget;
				}
				standardBudget = this.Session.Budget;
			}
			standardBudget.CheckOverBudget();
			ResourceLoadDelayInfo.CheckResourceHealth(standardBudget, this.Session.WorkloadSettings, this.ResourceKeys);
			standardBudget.StartConnection("ResponseFactory.AcquireSessionBudget");
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		protected void SetSessionError(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (exception != null)
			{
				stringBuilder.Append(exception.GetType());
				stringBuilder.Append(':');
				stringBuilder.Append(exception.Message);
				exception = exception.InnerException;
				if (exception != null)
				{
					stringBuilder.Append(" --> ");
				}
			}
			this.SetSessionError(stringBuilder.ToString());
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000DCCC File Offset: 0x0000BECC
		protected void SetSessionError(string error)
		{
			ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, error);
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.Message = error;
			}
			IProxyLogin proxyLogin = this.IncompleteRequest as IProxyLogin;
			if (proxyLogin != null)
			{
				proxyLogin.AuthenticationError = error;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000DD23 File Offset: 0x0000BF23
		private static int GetMajorVersionFromVersionNumber(int versionNumber)
		{
			return versionNumber >> 22 & 63;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		private static string GetResourceForestFqdnByAcceptedDomainName(string tenantAcceptedDomain)
		{
			return ADAccountPartitionLocator.GetResourceForestFqdnByAcceptedDomainName(tenantAcceptedDomain);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000DD34 File Offset: 0x0000BF34
		private AggregateOperationResult CopyorMoveItems(Folder folder, StoreObjectId destinationUid, bool returnNewIds, StoreObjectId[] messages, bool copy)
		{
			if (messages.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, null);
			}
			string arg = copy ? "Copy" : "Move";
			List<StoreObjectId> list;
			if (messages.Length > 256)
			{
				list = new List<StoreObjectId>(256);
				AggregateOperationResult aggregateOperationResult = null;
				int num = 2;
				for (int i = 0; i < messages.Length; i++)
				{
					if (messages[i] != null)
					{
						list.Add(messages[i]);
					}
					if (list.Count >= 256 || i == messages.Length - 1)
					{
						this.BackOffFromStore(ref num);
						ProtocolBaseServices.SessionTracer.TraceDebug<string, int>(this.session.SessionId, "{0}Items {1} items", arg, list.Count);
						AggregateOperationResult aggregateOperationResult2;
						if (copy)
						{
							aggregateOperationResult2 = folder.CopyObjects(folder.Session, destinationUid, returnNewIds, list.ToArray());
						}
						else
						{
							aggregateOperationResult2 = folder.MoveObjects(folder.Session, destinationUid, returnNewIds, list.ToArray());
						}
						ProtocolBaseServices.SessionTracer.TraceDebug<string, OperationResult>(this.session.SessionId, "{0} returned {1}.", arg, aggregateOperationResult2.OperationResult);
						if (aggregateOperationResult == null)
						{
							aggregateOperationResult = aggregateOperationResult2;
						}
						else
						{
							aggregateOperationResult = AggregateOperationResult.Merge(aggregateOperationResult, aggregateOperationResult2);
						}
						list.Clear();
					}
				}
				return aggregateOperationResult;
			}
			list = new List<StoreObjectId>(messages.Length);
			for (int j = 0; j < messages.Length; j++)
			{
				if (messages[j] != null)
				{
					list.Add(messages[j]);
				}
			}
			ProtocolBaseServices.SessionTracer.TraceDebug<string, int>(this.session.SessionId, "{0}Items {1} items", arg, list.Count);
			if (copy)
			{
				return folder.CopyObjects(folder.Session, destinationUid, returnNewIds, list.ToArray());
			}
			return folder.MoveObjects(folder.Session, destinationUid, returnNewIds, list.ToArray());
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		private SecurityStatus SkipAuthenticationOnCafe(byte[] userBytes, byte[] passBytes, Guid requestId, out string commonAccessToken, out IAccountValidationContext accountValidationContext)
		{
			IProxyLogin proxyLogin = (IProxyLogin)this.IncompleteRequest;
			string @string = Encoding.ASCII.GetString(userBytes);
			char[] trimChars = new char[1];
			string text = @string.Trim(trimChars);
			proxyLogin.AdUser = this.ProtocolUser.FindAdUser(text, null, null);
			if (proxyLogin.AdUser != null)
			{
				commonAccessToken = "SkipAuthenticationOnCafeToken";
				accountValidationContext = null;
				return SecurityStatus.OK;
			}
			this.session.LightLogSession.ErrorMessage = null;
			return proxyLogin.LiveIdBasicAuth.GetCommonAccessToken(userBytes, passBytes, requestId, out commonAccessToken, out accountValidationContext);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DF50 File Offset: 0x0000C150
		private bool IsSessionValid(out ProtocolResponse response)
		{
			if (this.IsAuthenticated && this.accountValidationContext != null)
			{
				AccountState accountState = this.accountValidationContext.CheckAccount();
				if (accountState != AccountState.AccountEnabled)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Session disconnected because account has been invalidated: " + accountState.ToString());
					response = new ProtocolResponse(string.Format(this.AccountInvalidatedString, accountState));
					response.IsDisconnectResponse = true;
					return false;
				}
			}
			response = null;
			return true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		private IPEndPoint GetRemoteEndPoint()
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			IPAddress address;
			int port;
			if (IPAddress.TryParse(proxyLogin.ClientIp, out address) && int.TryParse(proxyLogin.ClientPort, out port))
			{
				return new IPEndPoint(address, port);
			}
			if (string.IsNullOrEmpty(proxyLogin.ClientIp) && string.IsNullOrEmpty(proxyLogin.ClientPort))
			{
				return this.Session.Connection.RemoteEndPoint;
			}
			this.Session.LogInformation(string.Format("Error parsing FE->BE IP:Port ({0}:{1})", (proxyLogin.ClientIp == null) ? "<null>" : proxyLogin.ClientIp, (proxyLogin.ClientPort == null) ? "<null>" : proxyLogin.ClientPort), new object[0]);
			return this.Session.Connection.RemoteEndPoint;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		private bool BlockedByClientAccessRules(ADUser adUser)
		{
			if (ResponseFactory.GetClientAccessRulesEnabled() && ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)
			{
				IProxyLogin proxyLoginRequest = this.incompleteRequest as IProxyLogin;
				return ClientAccessRulesUtils.ShouldBlockConnection(adUser.OrganizationId, ClientAccessRulesUtils.GetUsernameFromIdInformation(adUser.WindowsLiveID, adUser.MasterAccountSid, adUser.Sid, adUser.Id), ResponseFactory.ClientAccessRulesProtocol, this.GetRemoteEndPoint(), ClientAccessAuthenticationMethod.BasicAuthentication, adUser, delegate(ClientAccessRulesEvaluationContext context)
				{
					this.Session.LogInformation(string.Format("{0}={1}", ClientAccessRulesConstants.ClientAccessRuleName, context.CurrentRule.Name), new object[0]);
					if (this.Session.LightLogSession != null)
					{
						this.Session.LightLogSession.Message = string.Format("{0}={1}", ClientAccessRulesConstants.ClientAccessRuleName, context.CurrentRule.Name);
						this.Session.LightLogSession.ErrorMessage = ClientAccessRulesConstants.ClientAccessRulesLogonFailed;
					}
					proxyLoginRequest.AuthenticationError = ClientAccessRulesConstants.ClientAccessRulesLogonFailed;
				}, delegate(double latency)
				{
					if (latency > 50.0)
					{
						this.Session.LogInformation(string.Format("{0}={1}", ClientAccessRulesConstants.ClientAccessRulesLatency, latency), new object[0]);
						if (this.Session.LightLogSession != null)
						{
							this.Session.LightLogSession.Message = string.Format("{0}={1}", ClientAccessRulesConstants.ClientAccessRulesLatency, latency);
						}
					}
				});
			}
			return false;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000E248 File Offset: 0x0000C448
		private void ConfigureResourceKeys()
		{
			this.ResourceKeys = new ResourceKey[]
			{
				ProcessorResourceKey.Local,
				new MdbResourceHealthMonitorKey(this.Store.MailboxOwner.MailboxInfo.GetDatabaseGuid()),
				new MdbReplicationResourceHealthMonitorKey(this.Store.MailboxOwner.MailboxInfo.GetDatabaseGuid())
			};
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		private bool IsWellKnownAccount(AuthenticationContext serverContext)
		{
			if (serverContext.IsWellKnownAdministrator || serverContext.IsGuest || serverContext.IsAnonymous)
			{
				string name = serverContext.Identity.Name;
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.session.SessionId, "User {0} is not enabled for protocol access", name);
				return true;
			}
			return false;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		private bool Logon(SecureString password, out ClientSecurityContext clientSecurityContext)
		{
			clientSecurityContext = null;
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			int num = this.UserName.IndexOf('/');
			int num2 = (num < this.UserName.Length - 1) ? this.UserName.IndexOf('/', num + 1) : -1;
			int num3 = this.UserName.IndexOf('@');
			if (num3 > 0)
			{
				if (num < num3 && num3 < num2)
				{
					this.SetSessionError("LogonFailed:UnsupportedLoginFormat1");
					return false;
				}
				if (num > num3)
				{
					num2 = num;
					num = 0;
				}
			}
			if (num2 == -1)
			{
				num2 = this.UserName.Length;
			}
			string domain;
			string text;
			if (num > 0)
			{
				domain = this.UserName.Substring(0, num);
				text = this.UserName.Substring(num + 1, num2 - num - 1);
			}
			else
			{
				domain = null;
				text = this.UserName.Substring(0, num2);
			}
			if (num2 < this.UserName.Length)
			{
				this.Mailbox = this.UserName.Substring(num2 + 1);
				if (string.IsNullOrEmpty(this.Mailbox))
				{
					this.SetSessionError("LogonFailed:UnsupportedLoginFormat2");
					return false;
				}
				proxyLogin.UserName = this.UserName.Remove(num2);
			}
			if (string.IsNullOrEmpty(text))
			{
				this.SetSessionError("LogonFailed:EmptyUserName");
				return false;
			}
			AuthenticationContext authenticationContext = null;
			AuthenticationContext authenticationContext2 = null;
			bool result;
			try
			{
				authenticationContext = new AuthenticationContext();
				authenticationContext2 = this.CreateServerAuthenticationContext();
				byte[] inputBuffer = null;
				SecurityStatus securityStatus = authenticationContext2.InitializeForInboundNegotiate(AuthenticationMechanism.Login);
				if (securityStatus != SecurityStatus.OK)
				{
					this.SetSessionError("LogonFailed:FailInitializeForInboundNegotiate");
					result = false;
				}
				else
				{
					SecurityStatus securityStatus2 = authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Login, null, text, domain, password);
					if (securityStatus2 != SecurityStatus.OK)
					{
						this.SetSessionError("LogonFailed:FailInitializeForOutboundNegotiate");
						result = false;
					}
					else
					{
						do
						{
							securityStatus2 = authenticationContext.NegotiateSecurityContext(inputBuffer, out inputBuffer);
							if (securityStatus2 != SecurityStatus.ContinueNeeded && securityStatus2 != SecurityStatus.OK)
							{
								break;
							}
							securityStatus = authenticationContext2.NegotiateSecurityContext(inputBuffer, out inputBuffer);
						}
						while ((securityStatus == SecurityStatus.ContinueNeeded || securityStatus == SecurityStatus.OK) && securityStatus2 == SecurityStatus.ContinueNeeded && securityStatus == SecurityStatus.ContinueNeeded);
						if (securityStatus2 == SecurityStatus.OK && securityStatus == SecurityStatus.OK && authenticationContext2.IsAuthenticated)
						{
							if (this.IsWellKnownAccount(authenticationContext2))
							{
								this.SetSessionError("LogonFailed:WellKnownAccount");
								result = false;
							}
							else
							{
								clientSecurityContext = this.GetClientSecurityContext(authenticationContext2);
								result = true;
							}
						}
						else
						{
							string text2 = securityStatus.ToString();
							string text3 = text2;
							string text4 = string.Empty;
							if (proxyLogin.LiveIdBasicAuth != null)
							{
								text4 = proxyLogin.LiveIdBasicAuth.LastAuthResult.ToString();
								if (proxyLogin.LiveIdBasicAuth.LastAuthResult != LiveIdAuthResult.Success)
								{
									text2 = text2 + "-" + text4;
								}
								if (!string.IsNullOrEmpty(proxyLogin.LiveIdBasicAuth.LastRequestErrorMessage))
								{
									text3 = text3 + "-" + proxyLogin.LiveIdBasicAuth.LastRequestErrorMessage.Replace('"', '\'').Replace("\r\n", " ");
								}
							}
							this.Session.LogInformation("User not found or invalid password. {0}", new object[]
							{
								text2
							});
							if (this.Session.LightLogSession != null)
							{
								this.Session.LightLogSession.ErrorMessage = "LogonFailed:" + text2;
								this.Session.LightLogSession.LiveIdAuthResult = text4.ToString();
							}
							this.SetSessionError("LogonFailed:" + text3);
							result = false;
						}
					}
				}
			}
			finally
			{
				if (authenticationContext != null)
				{
					authenticationContext.Dispose();
				}
				if (authenticationContext2 != null)
				{
					authenticationContext2.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000E658 File Offset: 0x0000C858
		private MiniRecipient FindPrincipalMailbox()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.protocolUser.OrganizationId), 3601, "FindPrincipalMailbox", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs");
			return this.FindPrincipalMailbox(tenantOrRootOrgRecipientSession);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000E698 File Offset: 0x0000C898
		private StorageMiniRecipient FindPrincipalMailbox(IRecipientSession session)
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			if (string.IsNullOrEmpty(this.Mailbox))
			{
				throw new ApplicationException("This call is only valid in delegate access scenario!");
			}
			QueryFilter filter;
			if (this.Mailbox.IndexOf('@') == -1)
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.Alias, this.Mailbox);
			}
			else
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, this.Mailbox);
			}
			ADPagedReader<StorageMiniRecipient> source = session.FindPagedMiniRecipient<StorageMiniRecipient>(null, QueryScope.SubTree, filter, null, 2, null);
			StorageMiniRecipient[] array = source.ToArray<StorageMiniRecipient>();
			if (array == null || array.Length == 0)
			{
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ErrorMessage = "NoADRecipient";
				}
				proxyLogin.AuthenticationError = "NoADRecipient";
				this.Session.LogInformation("ADRecipient {0} not found.", new object[]
				{
					this.Mailbox
				});
				return null;
			}
			if (array.Length > 1)
			{
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ErrorMessage = "TooManyADRecipients";
				}
				proxyLogin.AuthenticationError = "TooManyADRecipients";
				this.Session.LogInformation("Found too many recipients.", new object[0]);
				return null;
			}
			return array[0];
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		private ExchangePrincipal FindExchangePrincipal()
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			ExchangePrincipal exchangePrincipal = null;
			try
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.protocolUser.OrganizationId), 3684, "FindExchangePrincipal", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs");
				if (string.IsNullOrEmpty(this.Mailbox))
				{
					try
					{
						exchangePrincipal = ExchangePrincipal.FromUserSid(tenantOrRootOrgRecipientSession, this.userSid);
						goto IL_12D;
					}
					catch (ObjectNotFoundException)
					{
						if (this.Session.LightLogSession != null)
						{
							this.Session.LightLogSession.ErrorMessage = "NoExchangePrincipal";
						}
						proxyLogin.AuthenticationError = "NoExchangePrincipal";
						this.Session.LogInformation("ExchangePrincipal for {0} not found.", new object[]
						{
							this.protocolUser.LegacyDistinguishedName
						});
						return null;
					}
				}
				StorageMiniRecipient storageMiniRecipient = this.FindPrincipalMailbox(tenantOrRootOrgRecipientSession);
				if (storageMiniRecipient == null)
				{
					return null;
				}
				try
				{
					exchangePrincipal = ExchangePrincipal.FromMiniRecipient(storageMiniRecipient);
				}
				catch (ObjectNotFoundException)
				{
					if (this.Session.LightLogSession != null)
					{
						this.Session.LightLogSession.ErrorMessage = "NoExchangePrincipal2";
					}
					proxyLogin.AuthenticationError = "NoExchangePrincipal2";
					this.Session.LogInformation("ExchangePrincipal for mailbox {0} not found.", new object[]
					{
						this.Mailbox
					});
					return null;
				}
				IL_12D:;
			}
			catch (SystemException exception)
			{
				this.LogHandledException(exception);
				return null;
			}
			if (exchangePrincipal == null)
			{
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ErrorMessage = "NoExchangePrincipal3";
				}
				proxyLogin.AuthenticationError = "NoExchangePrincipal3";
				this.Session.LogInformation("Unable to find ExchangePrincipal", new object[0]);
				return null;
			}
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.Message = string.Format("User:{0}", exchangePrincipal.ToString());
			}
			this.Session.LogInformation("Mailbox: User \"{0}\" Server name \"{1}\", {2}, legacyId \"{3}\"", new object[]
			{
				exchangePrincipal.MailboxInfo.DisplayName,
				exchangePrincipal.MailboxInfo.Location.ServerFqdn,
				new ServerVersion(exchangePrincipal.MailboxInfo.Location.ServerVersion),
				exchangePrincipal.LegacyDn
			});
			return exchangePrincipal;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000EA30 File Offset: 0x0000CC30
		private bool OpenMailboxSession(ClientSecurityContext clientSecurityContext, ADUser adUser)
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			if (clientSecurityContext == null)
			{
				ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, "Empty clientSecurityContext.");
				return false;
			}
			ExchangePrincipal exchangePrincipal = this.FindExchangePrincipal();
			if (exchangePrincipal == null)
			{
				ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, "ExchangePrincipal not found.");
				return false;
			}
			if (this.Session.LightLogSession != null && ProtocolBaseServices.IsMultiTenancyEnabled)
			{
				this.Session.LightLogSession.OrganizationId = proxyLogin.AdUser.OrganizationId.ToString();
			}
			this.FindOwaServer(exchangePrincipal);
			this.store = MailboxSession.OpenWithBestAccess(exchangePrincipal, adUser, clientSecurityContext, CultureInfo.InvariantCulture, this.ClientStringForMailboxSession);
			if (!this.store.CanActAsOwner)
			{
				this.SetSessionError("NoPermissions");
				this.store.Dispose();
				this.store = null;
				return false;
			}
			this.Session.SetMailboxLogTimeout(this.protocolUser.MailboxLogTimeout);
			this.store.SetClientIPEndpoints(this.Session.Connection.RemoteEndPoint.Address, this.Session.Connection.LocalEndPoint.Address);
			this.store.AccountingObject = this.Session.Budget;
			this.store.ExTimeZone = (TimeZoneHelper.GetUserTimeZone(this.store) ?? ResponseFactory.CurrentExTimeZone);
			this.Session.Connection.Timeout = this.Session.Server.ConnectionTimeout;
			this.ConfigureResourceKeys();
			if (this.ProtocolUser.LrsEnabled)
			{
				this.Session.LrsSession = ProtocolBaseServices.LrsLog.OpenSession(exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), this.Session.Connection.RemoteEndPoint, this.Session.Connection.LocalEndPoint);
			}
			this.DoPostLoginTasks();
			this.DisconnectFromTheStore();
			return true;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		private bool ProxyConnect(ADUser adUser)
		{
			if (this.ActualCafeAuthDone)
			{
				this.catToken = this.GetCommonAccessToken(adUser);
			}
			BackEndServer backEndServer;
			if (string.IsNullOrEmpty(this.Mailbox))
			{
				backEndServer = BackEndLocator.GetBackEndServer(adUser);
			}
			else
			{
				MiniRecipient miniRecipient = this.FindPrincipalMailbox();
				if (miniRecipient == null)
				{
					ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, "Cross forest Cafe to Brick proxy principal mailbox could not be found.");
					if (this.Session.LightLogSession != null)
					{
						this.Session.LightLogSession.ErrorMessage = "NoCrossForestPrincipalMailbox";
					}
					return false;
				}
				backEndServer = BackEndLocator.GetBackEndServer(miniRecipient);
			}
			if (backEndServer != null)
			{
				this.session.SetDiagnosticValue(ConditionalHandlerSchema.MailboxServerVersion, backEndServer.Version);
				this.session.SetDiagnosticValue(ConditionalHandlerSchema.MailboxServer, backEndServer.Fqdn);
				int num = 0;
				if (backEndServer.Version >= Server.E15MinVersion)
				{
					num = 15;
				}
				else if (backEndServer.Version < Server.E15MinVersion && backEndServer.Version >= Server.E14MinVersion)
				{
					num = 14;
				}
				else if (backEndServer.Version < Server.E14MinVersion && backEndServer.Version >= Server.E2007MinVersion)
				{
					num = 12;
				}
				ProtocolBaseServices.SessionTracer.TraceDebug<string, int, int>(this.session.SessionId, "Proxy to external {0}, version {1}({2})", backEndServer.Fqdn, num, backEndServer.Version);
				switch (num)
				{
				case 12:
				case 14:
					return this.ProxyConnectToLegacyServer(num);
				case 15:
					return this.ProxyConnectTo15(backEndServer);
				}
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ErrorMessage = "UnsuportedBackendVersion" + backEndServer.Version;
				}
				return true;
			}
			if (this.Session.Server.IsPartnerHostedOnly && this.Session.Server.ExternalProxySettings != null)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Proxy to external proxy URL");
				this.Session.ProxyToLegacyServer = true;
				return this.ProxyConnect(this.Session.Server.ExternalProxySettings.Hostname.HostnameString, this.Session.Server.ExternalProxySettings.Port, this.Session.Server.ExternalProxySettings.EncryptionType);
			}
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ErrorMessage = "NoBackendFound";
			}
			return false;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000EE78 File Offset: 0x0000D078
		private bool ProxyConnectTo15(BackEndServer backEndServer)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(this.session.SessionId, "Proxy to E15 MBX using BackEndLocator");
			string localForestFqdn = TopologyProvider.LocalForestFqdn;
			bool flag = !ProtocolBaseServices.IsMultiTenancyEnabled || backEndServer.Fqdn.EndsWith(localForestFqdn, StringComparison.OrdinalIgnoreCase);
			string fqdn = backEndServer.Fqdn;
			EncryptionType value = EncryptionType.SSL;
			int e15MbxProxyPort = this.GetE15MbxProxyPort(fqdn, !flag, this.ProtocolUser.AcceptedDomain);
			return this.ProxyConnect(fqdn, e15MbxProxyPort, new EncryptionType?(value));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		private bool ProxyConnectToLegacyServer(int serverVersionMajor)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.session.SessionId, "Proxy to E{0} CAS using Service Discovery", serverVersionMajor);
			Dictionary<int, int> dictionary = new Dictionary<int, int>
			{
				{
					0,
					1
				},
				{
					1,
					2
				},
				{
					-1,
					this.Session.IsTls ? 3 : 0
				}
			};
			int num = 4;
			string proxyHostname = null;
			int proxyPort = 0;
			EncryptionType? encryptionType = null;
			ExchangePrincipal exchangePrincipal = this.FindExchangePrincipal();
			if (exchangePrincipal == null)
			{
				ProtocolBaseServices.SessionTracer.TraceError(this.session.SessionId, "ExchangePrincipal not found.");
				return false;
			}
			foreach (EmailTransportService emailTransportService in this.GetProxyDestinations(exchangePrincipal))
			{
				if (emailTransportService.PopImapTransport)
				{
					if (serverVersionMajor == 14)
					{
						using (IEnumerator<ProtocolConnectionSettings> enumerator2 = emailTransportService.InternalConnectionSettings.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ProtocolConnectionSettings protocolConnectionSettings = enumerator2.Current;
								if (!ResponseFactory.IgnoreNonProvisionedServersEnabled || !emailTransportService.ServerFullyQualifiedDomainName.Equals(protocolConnectionSettings.Hostname.HostnameString, StringComparison.OrdinalIgnoreCase))
								{
									Dictionary<int, int> dictionary2 = dictionary;
									EncryptionType? encryptionType2 = protocolConnectionSettings.EncryptionType;
									if (dictionary2[((encryptionType2 != null) ? new int?((int)encryptionType2.GetValueOrDefault()) : null) ?? -1] < num)
									{
										proxyHostname = protocolConnectionSettings.Hostname.HostnameString;
										proxyPort = protocolConnectionSettings.Port;
										encryptionType = protocolConnectionSettings.EncryptionType;
										Dictionary<int, int> dictionary3 = dictionary;
										EncryptionType? encryptionType3 = protocolConnectionSettings.EncryptionType;
										num = dictionary3[((encryptionType3 != null) ? new int?((int)encryptionType3.GetValueOrDefault()) : null) ?? -1];
									}
								}
							}
							continue;
						}
					}
					if (serverVersionMajor != 12)
					{
						throw new InvalidOperationException("Unsupported server verson " + serverVersionMajor);
					}
					proxyHostname = emailTransportService.ServerFullyQualifiedDomainName;
					if (emailTransportService.SSLPort != -1 && dictionary[0] < num)
					{
						proxyPort = emailTransportService.SSLPort;
						encryptionType = new EncryptionType?(EncryptionType.SSL);
						num = dictionary[0];
					}
					if (emailTransportService.UnencryptedOrTLSPort != -1 && emailTransportService.LoginType > LoginOptions.PlainTextLogin && dictionary[1] < num)
					{
						proxyPort = emailTransportService.UnencryptedOrTLSPort;
						encryptionType = new EncryptionType?(EncryptionType.TLS);
						num = dictionary[1];
					}
					if (emailTransportService.UnencryptedOrTLSPort != -1 && emailTransportService.LoginType == LoginOptions.PlainTextLogin && dictionary[-1] < num)
					{
						proxyPort = emailTransportService.UnencryptedOrTLSPort;
						encryptionType = null;
						num = dictionary[-1];
					}
				}
			}
			return this.ProxyConnect(proxyHostname, proxyPort, encryptionType);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		private bool ProxyConnect(string proxyHostname, int proxyPort, EncryptionType? proxyEncryptionType)
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			proxyLogin.AuthenticationError = string.Empty;
			proxyLogin.ProxyDestination = string.Concat(new object[]
			{
				proxyHostname,
				':',
				proxyPort,
				':',
				(proxyEncryptionType == null) ? "Plaintext" : proxyEncryptionType.ToString()
			});
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ProxyDestination = proxyLogin.ProxyDestination;
				this.Session.LightLogSession.Message = "Proxy:" + this.Session.LightLogSession.ProxyDestination;
			}
			try
			{
				this.Session.LogInformation("Start Proxy to {0}", new object[]
				{
					proxyLogin.ProxyDestination
				});
				if (string.IsNullOrEmpty(proxyHostname))
				{
					proxyLogin.AuthenticationError = "NoProxyServer";
					this.Session.LogInformation("Can't find a server to connect to for {0}", new object[]
					{
						this.protocolUser.UniqueName
					});
					return false;
				}
				if (proxyLogin.Password == null && (!this.ActualCafeAuthDone || this.catToken == null))
				{
					proxyLogin.AuthenticationError = "NoProxyPassword";
					this.Session.LogInformation("Can't make a proxy connection: Not enough info to auth on backend or skip auth.", new object[0]);
					return false;
				}
				this.connectionCreated = new AutoResetEvent(false);
				this.proxyEncryptionType = proxyEncryptionType;
				if (!this.StartProxyConnection(proxyHostname, proxyPort, AddressFamily.InterNetworkV6))
				{
					proxyLogin.AuthenticationError = "NoProxyConnection";
					this.Session.LogInformation("Can't start a proxy connection to {0}:{1}.", new object[]
					{
						proxyHostname,
						proxyPort
					});
					return false;
				}
				this.Session.Connection.Timeout = this.Session.Server.ConnectionTimeout;
				if (!this.connectionCreated.WaitOne(this.session.Server.PreAuthConnectionTimeout * 1000, true))
				{
					if (string.IsNullOrEmpty(proxyLogin.AuthenticationError))
					{
						IProxyLogin proxyLogin2 = proxyLogin;
						proxyLogin2.AuthenticationError += "ProxyTimeout";
					}
					this.Session.LogInformation("Timeout while connection to BE server.", new object[0]);
					return false;
				}
				if (this.session.ProxySession == null)
				{
					if (string.IsNullOrEmpty(proxyLogin.AuthenticationError))
					{
						proxyLogin.AuthenticationError = "ProxyFailed";
					}
					this.Session.LogInformation("Proxy connection failed", new object[0]);
					return false;
				}
				if (!this.session.ProxySession.IsConnected)
				{
					if (string.IsNullOrEmpty(proxyLogin.AuthenticationError))
					{
						proxyLogin.AuthenticationError = "ProxyNotAuthenticated";
					}
					this.session.ProxySession.Shutdown();
					this.session.ProxySession = null;
					this.Session.LogInformation("Unable to connect to BE server", new object[0]);
					return false;
				}
			}
			finally
			{
				if (this.Session.LightLogSession != null)
				{
					if (!string.IsNullOrEmpty(proxyLogin.AuthenticationError))
					{
						this.Session.LightLogSession.ErrorMessage = proxyLogin.AuthenticationError;
						this.Session.LightLogSession.ProxyDestination = null;
					}
					else
					{
						this.Session.LightLogSession.Message = "ProxySuccess";
					}
				}
			}
			return true;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000F53C File Offset: 0x0000D73C
		private bool ConfigureUser(ADUser adUser)
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			ProtocolBaseServices.Assert(this.protocolUser != null, "Derived ResponseFactory.DoConnect should have created a derived ProtocolUser object", new object[0]);
			this.protocolUser.Configure(adUser);
			this.protocolUser.LogonName = this.UserName;
			this.session.EnableUserTracing();
			if (this.accountValidationContext != null)
			{
				this.accountValidationContext.SetOrgId(adUser.OrganizationId);
			}
			ProtocolBaseServices.SessionTracer.TraceDebug<ProtocolUser>(this.session.SessionId, "UserConfiguration {0}", this.protocolUser);
			if (!this.protocolUser.IsEnabled)
			{
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ErrorMessage = "UserDisabled";
				}
				proxyLogin.AuthenticationError = "UserDisabled";
				this.Session.LogInformation("User {0} is not enabled for protocol access.", new object[]
				{
					this.UserName
				});
				return false;
			}
			return !this.BlockedByClientAccessRules(adUser);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000F63B File Offset: 0x0000D83B
		private string GetConnectionIdentity()
		{
			if (this.userSid != null)
			{
				return this.userSid.ToString();
			}
			if (!string.IsNullOrEmpty(this.userPuid))
			{
				return this.userPuid;
			}
			return this.UserName;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000F674 File Offset: 0x0000D874
		private bool CheckConnectionLimit()
		{
			IProxyLogin proxyLogin = this.incompleteRequest as IProxyLogin;
			string connectionIdentity = this.GetConnectionIdentity();
			this.protocolUser.ConnectionIdentity = connectionIdentity;
			int num = -1;
			if (!string.IsNullOrEmpty(connectionIdentity))
			{
				lock (ResponseFactory.connectionsPerUser)
				{
					if (ResponseFactory.connectionsPerUser.Add(connectionIdentity) > this.session.Server.MaxConnectionsPerUser)
					{
						num = this.Session.VirtualServer.DisposeExpiredSessions(connectionIdentity);
						ResponseFactory.connectionsPerUser.Counters[connectionIdentity] = num;
					}
				}
			}
			if (num == -1)
			{
				return true;
			}
			if (num <= this.session.Server.MaxConnectionsPerUser)
			{
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.Message = "DisposeExpiredSessions" + (this.session.Server.MaxConnectionsPerUser - num + 1);
				}
				this.Session.LogInformation("User {0} had {1} expired connections. Sessions cleaned up.", new object[]
				{
					this.UserName,
					this.session.Server.MaxConnectionsPerUser - num + 1
				});
				return true;
			}
			if (this.Session.LightLogSession != null)
			{
				this.Session.LightLogSession.ErrorMessage = "UserConnectionLimitReached";
			}
			proxyLogin.AuthenticationError = "UserConnectionLimitReached";
			ProtocolBaseServices.LogEvent(this.protocolUser.UserExceededNumberOfConnectionsEventTuple, this.UserName, new string[]
			{
				this.UserName,
				this.session.Server.MaxConnectionsPerUser.ToString()
			});
			this.Session.LogInformation("User {0} has more open connections than allowed.", new object[]
			{
				this.UserName
			});
			return false;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000F854 File Offset: 0x0000DA54
		private ProtocolResponse AuthenticationDone(ResponseFactory.AuthenticationResult authenticationResult)
		{
			IProxyLogin proxyLogin = (IProxyLogin)this.IncompleteRequest;
			ProtocolResponse result;
			try
			{
				if (authenticationResult == ResponseFactory.AuthenticationResult.success)
				{
					if (this.IsWellKnownAccount(this.serverContext))
					{
						this.SetSessionError("AuthFailed:WellKnownAccount");
						authenticationResult = ResponseFactory.AuthenticationResult.failure;
					}
					else
					{
						try
						{
							if (this.clientSecurityContext != null)
							{
								((IDisposable)this.clientSecurityContext).Dispose();
								this.clientSecurityContext = null;
							}
							this.clientSecurityContext = this.GetClientSecurityContext(this.serverContext);
							proxyLogin.UserName = this.serverContext.UserName;
							this.Session.LogInformation("After successful ProcessAuth, going to try to connect from {1} with username {0}", new object[]
							{
								proxyLogin.UserName,
								ProtocolBaseServices.ServerRoleService
							});
							if (this.serverContext.AuthorizationIdentity != null)
							{
								this.Mailbox = Encoding.ASCII.GetString(this.serverContext.AuthorizationIdentity);
								if (string.Equals(this.Mailbox, this.UserName, StringComparison.OrdinalIgnoreCase))
								{
									this.Mailbox = null;
								}
								if (!string.IsNullOrEmpty(this.Mailbox) && this.Session.LightLogSession != null)
								{
									this.Session.LightLogSession.Message = string.Format("Auth:User:{0},Mbx:{1}", this.UserName, this.Mailbox);
								}
							}
							if (this.authenticationMechanism == AuthenticationMechanism.Plain)
							{
								proxyLogin.Password = this.serverContext.Password;
							}
							else
							{
								this.ActualCafeAuthDone = true;
							}
							bool flag = false;
							try
							{
								flag = this.TryToConnect(this.clientSecurityContext);
							}
							finally
							{
								ResponseFactory.AuthenticationResult authenticationResult2 = ((ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox || this.ActualCafeAuthDone) && ResponseFactory.CheckOnlyAuthenticationStatusEnabled && !ProtocolBaseServices.AuthErrorReportEnabled(this.UserName)) ? ResponseFactory.AuthenticationResult.authenticatedButFailed : ResponseFactory.AuthenticationResult.failure;
								authenticationResult = (flag ? ResponseFactory.AuthenticationResult.success : authenticationResult2);
							}
						}
						catch (Exception ex)
						{
							if (!this.Session.CheckNonCriticalException(ex))
							{
								throw;
							}
							this.SetSessionError(ex);
							if (!ResponseFactory.CheckOnlyAuthenticationStatusEnabled || ProtocolBaseServices.AuthErrorReportEnabled(this.UserName))
							{
								return this.ProcessException(this.incompleteRequest, ex, this.AuthenticationFailureString);
							}
							authenticationResult = ResponseFactory.AuthenticationResult.authenticatedButFailed;
						}
						finally
						{
							using (this.clientSecurityContext)
							{
							}
						}
					}
				}
				result = this.AuthenticationDone(this.incompleteRequest, authenticationResult);
			}
			finally
			{
				if (this.serverContext != null)
				{
					this.serverContext.Dispose();
					this.serverContext = null;
				}
				this.incompleteRequest = null;
				if (authenticationResult != ResponseFactory.AuthenticationResult.authenticatedAsCafe)
				{
					this.Session.SetMaxCommandLength(this.Session.Server.MaxCommandLength);
				}
			}
			return result;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FB24 File Offset: 0x0000DD24
		private void ProxyConnectionComplete(IAsyncResult iar)
		{
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Session.SessionId, "User {0} entering ResponseFactory.ProxyConnectionComplete.", this.Session.GetUserNameForLogging());
				ResponseFactory.ProxyConnectioInfo proxyConnectioInfo = (ResponseFactory.ProxyConnectioInfo)iar.AsyncState;
				try
				{
					proxyConnectioInfo.Socket.EndConnect(iar);
				}
				catch (SocketException ex)
				{
					if (proxyConnectioInfo.Socket.AddressFamily == AddressFamily.InterNetworkV6)
					{
						this.Session.LogInformation("ConnectionComplete: try IPv4", new object[0]);
						this.StartProxyConnection(proxyConnectioInfo.Host, proxyConnectioInfo.Port, AddressFamily.InterNetwork);
					}
					else
					{
						if (this.Session.LightLogSession != null)
						{
							this.Session.LightLogSession.ExceptionCaught = ex;
						}
						this.Session.LogInformation("ConnectionComplete: SocketException: {0}", new object[]
						{
							ex
						});
						if (this.connectionCreated != null)
						{
							this.connectionCreated.Set();
						}
					}
					return;
				}
				catch (ArgumentException ex2)
				{
					if (proxyConnectioInfo.Socket.AddressFamily == AddressFamily.InterNetworkV6)
					{
						this.Session.LogInformation("ConnectionComplete: try IPv4", new object[0]);
						this.StartProxyConnection(proxyConnectioInfo.Host, proxyConnectioInfo.Port, AddressFamily.InterNetwork);
					}
					else
					{
						if (this.Session.LightLogSession != null)
						{
							this.Session.LightLogSession.ExceptionCaught = ex2;
						}
						this.Session.LogInformation("ConnectionComplete: ArgumentException: {0}", new object[]
						{
							ex2
						});
						if (this.connectionCreated != null)
						{
							this.connectionCreated.Set();
						}
					}
					return;
				}
				if (this.Session.Disposed)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(this.Session.SessionId, "Incoming session is disposed, nothing to do.");
					if (this.connectionCreated != null)
					{
						this.connectionCreated.Set();
					}
				}
				else
				{
					ProxySession proxySession = this.NewProxySession(new NetworkConnection(proxyConnectioInfo.Socket, 4096));
					ProtocolBaseServices.SessionTracer.TraceDebug<long, IPEndPoint, IPEndPoint>(this.Session.SessionId, "New proxy Tcp connection {0} opened from {1} to {2}.", proxySession.SessionId, proxySession.RemoteEndPoint, proxySession.LocalEndPoint);
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted(this.session.SessionId);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000FD80 File Offset: 0x0000DF80
		private bool StartProxyConnection(string host, int port, AddressFamily addressFamily)
		{
			Socket socket = new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
			ResponseFactory.ProxyConnectioInfo proxyConnectioInfo = default(ResponseFactory.ProxyConnectioInfo);
			proxyConnectioInfo.Socket = socket;
			proxyConnectioInfo.Host = host;
			proxyConnectioInfo.Port = port;
			try
			{
				socket.BeginConnect(host, port, new AsyncCallback(this.ProxyConnectionComplete), proxyConnectioInfo);
			}
			catch (SocketException ex)
			{
				if (addressFamily == AddressFamily.InterNetworkV6)
				{
					this.Session.LogInformation("StartProxyConnection: try IPv4", new object[0]);
					return this.StartProxyConnection(host, port, AddressFamily.InterNetwork);
				}
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ExceptionCaught = ex;
				}
				this.Session.LogInformation("StartProxyConnection: SocketException: {0}", new object[]
				{
					ex
				});
				return false;
			}
			catch (ArgumentException ex2)
			{
				if (addressFamily == AddressFamily.InterNetworkV6)
				{
					this.Session.LogInformation("StartProxyConnection: try IPv4", new object[0]);
					return this.StartProxyConnection(host, port, AddressFamily.InterNetwork);
				}
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.ExceptionCaught = ex2;
				}
				this.Session.LogInformation("StartProxyConnection: ArgumentException: {0}", new object[]
				{
					ex2
				});
				return false;
			}
			return true;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		private void FindOwaServer(ExchangePrincipal exchangePrincipal)
		{
			this.owaServer = null;
			if (this.ForceICalForCalendarRetrievalOption)
			{
				return;
			}
			switch (this.session.Server.CalendarItemRetrievalOption)
			{
			case CalendarItemRetrievalOptions.iCalendar:
				return;
			case CalendarItemRetrievalOptions.intranetUrl:
			case CalendarItemRetrievalOptions.InternetUrl:
			{
				ClientAccessType clientAccessType = (this.session.Server.CalendarItemRetrievalOption == CalendarItemRetrievalOptions.InternetUrl) ? ClientAccessType.External : ClientAccessType.Internal;
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs", "FindOwaServer", 4647);
				IList<OwaService> list = currentServiceTopology.FindAll<OwaService>(exchangePrincipal, clientAccessType, "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ResponseFactory.cs", "FindOwaServer", 4649);
				foreach (OwaService owaService in list)
				{
					if (!ResponseFactory.IgnoreNonProvisionedServersEnabled || !owaService.ServerFullyQualifiedDomainName.Equals(owaService.Url.Host, StringComparison.OrdinalIgnoreCase))
					{
						this.owaServer = owaService.Url.ToString();
						break;
					}
				}
				if (this.owaServer == null)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<ClientAccessType, string>(this.session.SessionId, "Unable to find {0} OWA server url for user {1}.", clientAccessType, exchangePrincipal.MailboxInfo.DisplayName);
					ProtocolBaseServices.LogEvent(this.protocolUser.OwaServerNotFoundEventTuple, exchangePrincipal.MailboxInfo.DisplayName, new string[]
					{
						clientAccessType.ToString(),
						exchangePrincipal.MailboxInfo.DisplayName
					});
				}
				break;
			}
			case CalendarItemRetrievalOptions.Custom:
				this.owaServer = this.session.Server.OwaServer;
				break;
			}
			if (this.Session.Server.LiveIdBasicAuthReplacement && !string.IsNullOrEmpty(this.owaServer))
			{
				this.owaServer = this.owaServer + (this.owaServer.EndsWith("/") ? string.Empty : "/") + this.DefaultAcceptedDomainName + "/";
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000100B4 File Offset: 0x0000E2B4
		private void ResetRecipientCache(object state)
		{
			OutboundConversionOptions outboundConversionOptions = state as OutboundConversionOptions;
			if (outboundConversionOptions != null && this.okToResetRecipientCache)
			{
				lock (this.options)
				{
					this.options.RecipientCache = null;
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001010C File Offset: 0x0000E30C
		private bool TryGetActivityContextStat(ActivityOperationType operationType, out double value)
		{
			value = 0.0;
			if (this.Session.ActivityScope != null)
			{
				IEnumerable<KeyValuePair<OperationKey, OperationStatistics>> statistics = this.Session.ActivityScope.Statistics;
				foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in statistics)
				{
					if (keyValuePair.Key.ActivityOperationType == operationType)
					{
						TotalOperationStatistics totalOperationStatistics = keyValuePair.Value as TotalOperationStatistics;
						if (totalOperationStatistics != null)
						{
							value = totalOperationStatistics.Total;
							return true;
						}
						AverageOperationStatistics averageOperationStatistics = keyValuePair.Value as AverageOperationStatistics;
						if (averageOperationStatistics != null)
						{
							value = (double)averageOperationStatistics.CumulativeAverage;
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000101CC File Offset: 0x0000E3CC
		private AuthenticationContext CreateServerAuthenticationContext()
		{
			IProxyLogin proxyLogin = (IProxyLogin)this.incompleteRequest;
			if (!this.Session.Server.LiveIdBasicAuthReplacement)
			{
				return new AuthenticationContext(this.Session.Server.ExtendedProtectionConfig, this.Session.Connection.ChannelBindingToken);
			}
			ProtocolBaseServices.FaultInjectionTracer.TraceTest<bool>(3515231549U, ref ResponseFactory.UseClientIpTestMocks);
			if (ResponseFactory.UseClientIpTestMocks)
			{
				proxyLogin.LiveIdBasicAuth = new LiveIdBasicAuthenticationMock(new LiveIdBasicAuthentication());
				ResponseFactory.CheckOnlyAuthenticationStatusEnabled = false;
			}
			else
			{
				proxyLogin.LiveIdBasicAuth = new LiveIdBasicAuthentication();
			}
			proxyLogin.LiveIdBasicAuth.AllowLiveIDOnlyAuth = true;
			proxyLogin.LiveIdBasicAuth.ApplicationName = "Microsoft.Exchange.PopImap";
			if (this.Session.Connection.RemoteEndPoint.Address != null)
			{
				proxyLogin.LiveIdBasicAuth.UserIpAddress = this.Session.Connection.RemoteEndPoint.Address.ToString();
			}
			else
			{
				proxyLogin.LiveIdBasicAuth.UserIpAddress = null;
			}
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe && this.SkipAuthOnCafeEnabled)
			{
				return new AuthenticationContext(new ExternalProxyAuthentication(this.SkipAuthenticationOnCafe));
			}
			return new AuthenticationContext(new ExternalProxyAuthentication(proxyLogin.LiveIdBasicAuth.GetCommonAccessToken));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00010300 File Offset: 0x0000E500
		private string GetAuthenticationFailureString(SecurityStatus status)
		{
			string text = status.ToString();
			IProxyLogin proxyLogin = (IProxyLogin)this.IncompleteRequest;
			if (proxyLogin != null && proxyLogin.LiveIdBasicAuth != null && !string.IsNullOrEmpty(proxyLogin.LiveIdBasicAuth.LastRequestErrorMessage))
			{
				text = text + "-" + proxyLogin.LiveIdBasicAuth.LastRequestErrorMessage.Replace('"', '\'').Replace("\r\n", " ");
			}
			return text;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00010374 File Offset: 0x0000E574
		private ClientSecurityContext GetClientSecurityContext(AuthenticationContext serverContext)
		{
			if (!this.Session.Server.LiveIdBasicAuthReplacement)
			{
				WindowsIdentity windowsIdentity = serverContext.DetachIdentity();
				this.userSid = windowsIdentity.User;
				return new ClientSecurityContext(windowsIdentity);
			}
			if (serverContext.CommonAccessToken != "SkipAuthenticationOnCafeToken")
			{
				if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)
				{
					this.accountValidationContext = (AccountValidationContextBase)serverContext.AccountValidationContext;
				}
				return this.GetClientSecurityContext(serverContext.CommonAccessToken);
			}
			return null;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000103E8 File Offset: 0x0000E5E8
		private ClientSecurityContext GetClientSecurityContext(string catToken)
		{
			CommonAccessToken commonAccessToken = CommonAccessToken.Deserialize(catToken);
			if (commonAccessToken.ExtensionData.ContainsKey("UserSid"))
			{
				this.userSid = new SecurityIdentifier(commonAccessToken.ExtensionData["UserSid"]);
			}
			if (commonAccessToken.ExtensionData.ContainsKey("Puid"))
			{
				this.userPuid = commonAccessToken.ExtensionData["Puid"];
			}
			if (commonAccessToken.ExtensionData.ContainsKey("MemberName"))
			{
				this.UserName = commonAccessToken.ExtensionData["MemberName"];
			}
			BackendAuthenticator backendAuthenticator = null;
			IPrincipal principal = null;
			try
			{
				string text;
				BackendAuthenticator.GetAuthIdentifier(commonAccessToken, ref backendAuthenticator, out text);
				bool wantAuthIdentifier = text != null;
				BackendAuthenticator.Rehydrate(commonAccessToken, ref backendAuthenticator, wantAuthIdentifier, out text, out principal);
			}
			catch (BackendRehydrationException sessionError)
			{
				this.SetSessionError(sessionError);
				return null;
			}
			IIdentity identity = principal.Identity;
			if (identity == null)
			{
				return null;
			}
			return identity.CreateClientSecurityContext(true);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000104D8 File Offset: 0x0000E6D8
		private string GetCommonAccessToken(ADUser adUser)
		{
			CommonAccessToken commonAccessToken = null;
			if (this.Session.Server.LiveIdBasicAuthReplacement)
			{
				LiveIdBasicTokenAccessor liveIdBasicTokenAccessor = LiveIdBasicTokenAccessor.Create(adUser);
				if (liveIdBasicTokenAccessor != null)
				{
					commonAccessToken = liveIdBasicTokenAccessor.GetToken();
				}
			}
			else
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity(adUser.UserPrincipalName);
				if (windowsIdentity == null || windowsIdentity.IsAnonymous)
				{
					throw new InvalidOperationException(string.Format("Unable to find windows Identity, {0}.", adUser.PrimarySmtpAddress));
				}
				WindowsTokenAccessor windowsTokenAccessor = WindowsTokenAccessor.Create(windowsIdentity);
				if (windowsTokenAccessor != null)
				{
					commonAccessToken = windowsTokenAccessor.GetToken();
				}
			}
			if (commonAccessToken != null)
			{
				return commonAccessToken.Serialize();
			}
			return null;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001055C File Offset: 0x0000E75C
		private bool ExtractCafeHostname(byte[] buf, int offset, int size, out int authBlobSize, out string cafeHost)
		{
			authBlobSize = 0;
			cafeHost = null;
			for (int i = 0; i < size; i++)
			{
				if (buf[offset + i] == 0)
				{
					authBlobSize = i;
					cafeHost = Encoding.ASCII.GetString(buf, offset + i + 1, size - i - 1);
					this.Session.LogInformation("Processing Exchange Auth from cafe server {0}", new object[]
					{
						cafeHost
					});
					return true;
				}
			}
			this.Session.LogInformation("Null character not found in Exchange Auth authblob", new object[0]);
			return false;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000105D8 File Offset: 0x0000E7D8
		private bool ExtractClientIP(byte[] buf, int offset, int size, out int authBlobSize)
		{
			authBlobSize = size;
			IProxyLogin proxyLogin = (IProxyLogin)this.IncompleteRequest;
			bool flag = ResponseFactory.GetClientAccessRulesEnabled() || ProtocolBaseServices.GCCEnabledWithKeys;
			if (flag)
			{
				int num = 0;
				List<string> list = new List<string>();
				for (int i = 0; i < size; i++)
				{
					if (buf[offset + i] == 0)
					{
						int num2 = i + 1;
						if (num == 0)
						{
							num = num2;
							authBlobSize = num - 1;
						}
						else
						{
							int num3 = num;
							num = num2;
							list.Add(Encoding.ASCII.GetString(buf, offset + num3, num - num3 - 1));
						}
					}
				}
				list.Add(Encoding.ASCII.GetString(buf, offset + num, size - num));
				if (list.Count >= 2)
				{
					string authString = list[1];
					if (ProtocolBaseServices.GCCEnabledWithKeys && !GccUtils.IsValidAuthString(authString))
					{
						this.SetSessionError("InvalidServerAuthString");
						return false;
					}
					proxyLogin.ClientIp = list[0];
					this.Session.LogInformation("Setting client IP {0}", new object[]
					{
						proxyLogin.ClientIp
					});
					if (this.Session.LightLogSession != null)
					{
						this.Session.LightLogSession.ClientIp = proxyLogin.ClientIp;
					}
					if (proxyLogin.LiveIdBasicAuth != null)
					{
						proxyLogin.LiveIdBasicAuth.UserIpAddress = proxyLogin.ClientIp;
					}
					if (list.Count >= 3)
					{
						string text = list[2];
						if (this.ActivityId == Guid.Empty && !string.IsNullOrWhiteSpace(text))
						{
							Guid empty = Guid.Empty;
							if (Guid.TryParse(text, out empty))
							{
								this.ActivityId = empty;
							}
							else
							{
								this.Session.LogInformation(string.Format("Ignore invalid CAFE Activity ID ({0})", text), new object[0]);
							}
							if (this.Session.LightLogSession != null)
							{
								this.Session.LightLogSession.CafeActivityId = text;
							}
						}
					}
					proxyLogin.ClientPort = ((list.Count >= 4) ? list[3] : string.Empty);
					proxyLogin.AuthenticationType = ((list.Count >= 5) ? list[4] : string.Empty);
				}
			}
			else
			{
				for (int j = 0; j < size; j++)
				{
					if (buf[offset + j] == 0)
					{
						this.Session.LogInformation("AuthBlob has unexpected extra tokens", new object[0]);
						if (this.Session.LightLogSession != null)
						{
							this.Session.LightLogSession.ErrorMessage = "InvalidAuthBlob";
						}
						proxyLogin.AuthenticationError = "InvalidAuthBlob";
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00010848 File Offset: 0x0000EA48
		private void ReleaseResources()
		{
			ProxySession proxySession = null;
			lock (this)
			{
				string connectionIdentity = this.protocolUser.ConnectionIdentity;
				if (!string.IsNullOrEmpty(connectionIdentity))
				{
					ResponseFactory.connectionsPerUser.Remove(connectionIdentity);
				}
				if (this.store != null)
				{
					try
					{
						((IDisposable)this.store).Dispose();
					}
					catch (LocalizedException ex)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Session.SessionId, "Exception caught while disposing store. {0}", ex.ToString());
					}
					this.store = null;
				}
				if (this.Session.Budget != null)
				{
					try
					{
						this.Session.Budget.Dispose();
					}
					catch (LocalizedException ex2)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Session.SessionId, "Exception caught while disposing budget. {0}", ex2.ToString());
					}
					this.Session.Budget = null;
				}
				if (this.Session.ActivityScope != null)
				{
					try
					{
						this.Session.ActivityScope.Dispose();
					}
					catch (LocalizedException ex3)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Session.SessionId, "Exception caught while disposing ActivityScope. {0}", ex3.ToString());
					}
					this.Session.ActivityScope = null;
				}
				if (this.serverContext != null)
				{
					this.serverContext.Dispose();
					this.serverContext = null;
				}
				if (this.connectionCreated != null)
				{
					this.connectionCreated.Close();
					this.connectionCreated = null;
				}
				if (this.Session.ProxySession != null)
				{
					proxySession = this.Session.ProxySession;
					this.Session.ProxySession = null;
				}
				if (this.Session.LightLogSession != null)
				{
					this.Session.LightLogSession.Budget = null;
					this.Session.LightLogSession.ActivityScope = null;
				}
			}
			if (proxySession != null)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.Session.SessionId, "Calling disposeSession.Dispose()");
				proxySession.Dispose();
			}
		}

		// Token: 0x04000198 RID: 408
		public const int InitialBackOffDelay = 2;

		// Token: 0x04000199 RID: 409
		public const string DefaultUserDomain = "";

		// Token: 0x0400019A RID: 410
		public const int BackOffShift = 2;

		// Token: 0x0400019B RID: 411
		public const int MaximumBackOffDelay = 1024;

		// Token: 0x0400019C RID: 412
		public const int MaxBulkSize = 256;

		// Token: 0x0400019D RID: 413
		public const string ClientAccessRulesCapability = "CLIENTACCESSRULES";

		// Token: 0x0400019E RID: 414
		public const string Xproxy3Capability = "XPROXY3";

		// Token: 0x0400019F RID: 415
		public const string SkipAuthenticationOnCafeToken = "SkipAuthenticationOnCafeToken";

		// Token: 0x040001A0 RID: 416
		private const string AssemblyVersion = "15.00.1497.010";

		// Token: 0x040001A1 RID: 417
		public static bool UseClientIpTestMocks = false;

		// Token: 0x040001A2 RID: 418
		protected static readonly Regex AuthErrorParser = new Regex("\\[Error=\"?(?<authError>[^\"]+)\"?( Proxy=(?<proxy>.+))?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040001A3 RID: 419
		private static readonly FileVersionInfo currentXsoVersion = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(MailboxSession)).Location);

		// Token: 0x040001A4 RID: 420
		private static readonly char[] wordDelimiter = new char[]
		{
			' '
		};

		// Token: 0x040001A5 RID: 421
		private static readonly TimeSpan DefaultRecipientCacheResetInterval = TimeSpan.FromMinutes(3.0);

		// Token: 0x040001A6 RID: 422
		private static readonly TimeSpan DefaultMinPopImapThreshold = TimeSpan.FromMinutes(10.0);

		// Token: 0x040001A7 RID: 423
		private static MruDictionaryCache<OrganizationId, string> defaultAcceptedDomainTable = new MruDictionaryCache<OrganizationId, string>(10, 50000, 5);

		// Token: 0x040001A8 RID: 424
		private static RefCountTable<string> connectionsPerUser = new RefCountTable<string>();

		// Token: 0x040001A9 RID: 425
		private static int[] commandProcessingTimeSamples = new int[1024];

		// Token: 0x040001AA RID: 426
		private static LatencyDetectionContextFactory latencyDetectionContextFactory;

		// Token: 0x040001AB RID: 427
		private static int insertionIndex;

		// Token: 0x040001AC RID: 428
		private static int latencySum;

		// Token: 0x040001AD RID: 429
		private static int numSamples;

		// Token: 0x040001AE RID: 430
		private static object lockObject = new object();

		// Token: 0x040001AF RID: 431
		private static ExTimeZone currentExTimeZone;

		// Token: 0x040001B0 RID: 432
		protected bool clientAccessRulesSupportedByTargetServer;

		// Token: 0x040001B1 RID: 433
		private ProtocolRequest incompleteRequest;

		// Token: 0x040001B2 RID: 434
		private bool disposed;

		// Token: 0x040001B3 RID: 435
		private MailboxSession store;

		// Token: 0x040001B4 RID: 436
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x040001B5 RID: 437
		private OutboundConversionOptions options;

		// Token: 0x040001B6 RID: 438
		private InboundConversionOptions inboundOptions;

		// Token: 0x040001B7 RID: 439
		private ProtocolSession session;

		// Token: 0x040001B8 RID: 440
		private string userName;

		// Token: 0x040001B9 RID: 441
		private SecurityIdentifier userSid;

		// Token: 0x040001BA RID: 442
		private string userPuid;

		// Token: 0x040001BB RID: 443
		private string catToken;

		// Token: 0x040001BC RID: 444
		private AutoResetEvent connectionCreated;

		// Token: 0x040001BD RID: 445
		private EncryptionType? proxyEncryptionType;

		// Token: 0x040001BE RID: 446
		private int preAuthCommands;

		// Token: 0x040001BF RID: 447
		private int failedCommands;

		// Token: 0x040001C0 RID: 448
		private uint loginAttempts;

		// Token: 0x040001C1 RID: 449
		private AuthenticationMechanism authenticationMechanism;

		// Token: 0x040001C2 RID: 450
		private uint invalidCommands;

		// Token: 0x040001C3 RID: 451
		private AuthenticationContext serverContext;

		// Token: 0x040001C4 RID: 452
		private AccountValidationContextBase accountValidationContext;

		// Token: 0x040001C5 RID: 453
		private ProtocolUser protocolUser;

		// Token: 0x040001C6 RID: 454
		private string owaServer;

		// Token: 0x040001C7 RID: 455
		private Timer recipientCacheResetTimer;

		// Token: 0x040001C8 RID: 456
		private bool okToResetRecipientCache = true;

		// Token: 0x040001C9 RID: 457
		private DisposeTracker disposeTracker;

		// Token: 0x040001CA RID: 458
		private Stopwatch stopwatch;

		// Token: 0x040001CB RID: 459
		private UserConfigurationManager customStorageManager;

		// Token: 0x02000033 RID: 51
		public enum AuthenticationResult
		{
			// Token: 0x040001E4 RID: 484
			success,
			// Token: 0x040001E5 RID: 485
			failure,
			// Token: 0x040001E6 RID: 486
			authenticatedButFailed,
			// Token: 0x040001E7 RID: 487
			authenticatedAsCafe,
			// Token: 0x040001E8 RID: 488
			cancel
		}

		// Token: 0x02000034 RID: 52
		protected struct ProxyConnectioInfo
		{
			// Token: 0x040001E9 RID: 489
			public Socket Socket;

			// Token: 0x040001EA RID: 490
			public string Host;

			// Token: 0x040001EB RID: 491
			public int Port;
		}

		// Token: 0x02000035 RID: 53
		protected class SessionDisconnectedException : LocalizedException
		{
			// Token: 0x060003B3 RID: 947 RVA: 0x00010B2F File Offset: 0x0000ED2F
			public SessionDisconnectedException() : base(LocalizedString.Empty)
			{
			}
		}
	}
}
