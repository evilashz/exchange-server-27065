using System;
using System.Security;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Pop3AutoProvision : IAutoProvision
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000263B File Offset: 0x0000083B
		public Pop3AutoProvision(SmtpAddress emailAddress, SecureString password, string userLegacyDN) : this(emailAddress, password, userLegacyDN, new AutoProvisionOverrideProvider())
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000264C File Offset: 0x0000084C
		internal Pop3AutoProvision(SmtpAddress emailAddress, SecureString password, string userLegacyDN, IAutoProvisionOverrideProvider overrideProvider)
		{
			Pop3AuthenticationMechanism[] array = new Pop3AuthenticationMechanism[2];
			array[0] = Pop3AuthenticationMechanism.Spa;
			this.authMechanisms = array;
			base..ctor();
			if (emailAddress == SmtpAddress.Empty)
			{
				throw new ArgumentException("cannot be an empty address", "emailAddress");
			}
			string domain = emailAddress.Domain;
			this.userNames = new string[]
			{
				emailAddress.ToString(),
				emailAddress.Local
			};
			bool flag;
			if (!overrideProvider.TryGetOverrides(domain, ConnectionType.Pop, out this.hostNames, out flag))
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000276A File Offset: 0x0000096A
		public string[] Hostnames
		{
			get
			{
				return this.hostNames;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002772 File Offset: 0x00000972
		public int[] ConnectivePorts
		{
			get
			{
				return this.connectivePorts;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000277A File Offset: 0x0000097A
		private int[] SecurePorts
		{
			get
			{
				return this.securePorts;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002782 File Offset: 0x00000982
		private int[] InsecurePorts
		{
			get
			{
				return this.insecurePorts;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000278A File Offset: 0x0000098A
		private string[] UserNames
		{
			get
			{
				return this.userNames;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002792 File Offset: 0x00000992
		private Pop3AuthenticationMechanism[] AuthMechanisms
		{
			get
			{
				return this.authMechanisms;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000279C File Offset: 0x0000099C
		public static bool ValidatePopSettings(bool leaveOnServer, bool mirrored, string host, int port, string username, SecureString password, Pop3AuthenticationMechanism authentication, Pop3SecurityMechanism security, string userLegacyDN, ILog log, out LocalizedException validationException)
		{
			validationException = null;
			bool result2;
			using (IPop3Connection pop3Connection = Pop3Connection.CreateInstance(null))
			{
				Pop3ResultData result = pop3Connection.VerifyAccount();
				if (mirrored && !Pop3AutoProvision.SupportsMirroredSubscription(result))
				{
					validationException = new Pop3MirroredAccountNotPossibleException();
					result2 = false;
				}
				else if (leaveOnServer && !Pop3AutoProvision.SupportsLeaveOnServer(result))
				{
					validationException = new Pop3LeaveOnServerNotPossibleException();
					result2 = false;
				}
				else
				{
					result2 = true;
				}
			}
			return result2;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002808 File Offset: 0x00000A08
		private static bool SupportsLeaveOnServer(Pop3ResultData result)
		{
			return result.UidlCommandSupported && (result.RetentionDays == null || result.RetentionDays.Value > 0);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002844 File Offset: 0x00000A44
		private static bool SupportsMirroredSubscription(Pop3ResultData result)
		{
			return result.RetentionDays == null || !(result.RetentionDays != int.MaxValue);
		}

		// Token: 0x0400000E RID: 14
		internal static readonly int ConnectionTimeout = 20000;

		// Token: 0x0400000F RID: 15
		private readonly int[] connectivePorts = new int[]
		{
			995,
			110
		};

		// Token: 0x04000010 RID: 16
		private readonly int[] securePorts = new int[]
		{
			995,
			110
		};

		// Token: 0x04000011 RID: 17
		private readonly int[] insecurePorts = new int[]
		{
			110
		};

		// Token: 0x04000012 RID: 18
		private readonly Pop3AuthenticationMechanism[] authMechanisms;

		// Token: 0x04000013 RID: 19
		private readonly string[] hostNames;

		// Token: 0x04000014 RID: 20
		private readonly string[] userNames;
	}
}
