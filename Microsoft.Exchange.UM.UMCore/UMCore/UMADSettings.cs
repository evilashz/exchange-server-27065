using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000264 RID: 612
	internal abstract class UMADSettings
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060011FB RID: 4603
		public abstract ProtocolConnectionSettings SIPAccessService { get; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060011FC RID: 4604
		public abstract UMStartupMode UMStartupMode { get; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060011FD RID: 4605
		public abstract string UMCertificateThumbprint { get; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060011FE RID: 4606
		public abstract string Fqdn { get; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060011FF RID: 4607
		public abstract UMSmartHost ExternalServiceFqdn { get; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001200 RID: 4608
		public abstract IPAddressFamily IPAddressFamily { get; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001201 RID: 4609
		public abstract bool IPAddressFamilyConfigurable { get; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001202 RID: 4610
		public abstract string UMPodRedirectTemplate { get; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001203 RID: 4611
		public abstract string UMForwardingAddressTemplate { get; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001204 RID: 4612
		public abstract int SipTcpListeningPort { get; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001205 RID: 4613
		public abstract int SipTlsListeningPort { get; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001206 RID: 4614
		public abstract ADObjectId Id { get; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001207 RID: 4615
		public abstract bool IsSIPAccessServiceNeeded { get; }

		// Token: 0x06001208 RID: 4616
		internal abstract void SubscribeToADNotifications(ADNotificationEvent notificationHandler);

		// Token: 0x06001209 RID: 4617
		internal abstract UMADSettings RefreshFromAD();

		// Token: 0x0600120A RID: 4618 RVA: 0x000502B4 File Offset: 0x0004E4B4
		protected static void ExecuteADOperation(Action function)
		{
			try
			{
				function();
			}
			catch (Exception ex)
			{
				if (ex is ADOperationException || ex is ADTransientException)
				{
					throw new ExchangeServerNotFoundException(ex.Message, ex);
				}
				throw;
			}
		}
	}
}
