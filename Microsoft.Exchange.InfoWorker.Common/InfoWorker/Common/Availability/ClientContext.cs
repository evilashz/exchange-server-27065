using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Net.WSSecurity;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000073 RID: 115
	internal abstract class ClientContext : IDisposable
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000D781 File Offset: 0x0000B981
		public static ClientContext Create(ClientSecurityContext clientSecurityContext, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture)
		{
			return ClientContext.Create(clientSecurityContext, budget, timeZone, clientCulture, null, null);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D78E File Offset: 0x0000B98E
		public static ClientContext Create(ClientSecurityContext clientSecurityContext, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId)
		{
			return ClientContext.Create(clientSecurityContext, budget, timeZone, clientCulture, messageId, null);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D79C File Offset: 0x0000B99C
		public static ClientContext Create(ClientSecurityContext clientSecurityContext, OrganizationId organizationId, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId)
		{
			return new InternalClientContext(clientSecurityContext, organizationId, budget, timeZone, clientCulture, messageId);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		public static ClientContext Create(ClientSecurityContext clientSecurityContext, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId, ADUser adUser)
		{
			return new InternalClientContext(clientSecurityContext, budget, timeZone, clientCulture, messageId, adUser);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public static ClientContext Create(SmtpAddress emailAddress, SmtpAddress externalId, WSSecurityHeader wsSecurityHeader, SharingSecurityHeader sharingSecurityHeader, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture)
		{
			return ClientContext.Create(emailAddress, externalId, wsSecurityHeader, sharingSecurityHeader, budget, timeZone, clientCulture, null);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		public static ClientContext Create(SmtpAddress emailAddress, SmtpAddress externalId, WSSecurityHeader wsSecurityHeader, SharingSecurityHeader sharingSecurityHeader, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId)
		{
			if (externalId != SmtpAddress.Empty)
			{
				return new PersonalClientContext(emailAddress, externalId, wsSecurityHeader, sharingSecurityHeader, budget, timeZone, clientCulture, messageId);
			}
			return new OrganizationalClientContext(emailAddress, emailAddress.Domain, wsSecurityHeader, budget, timeZone, clientCulture, messageId);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000D81D File Offset: 0x0000BA1D
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000D825 File Offset: 0x0000BA25
		public ExTimeZone TimeZone { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000D82E File Offset: 0x0000BA2E
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000D836 File Offset: 0x0000BA36
		public CultureInfo ClientCulture { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000D83F File Offset: 0x0000BA3F
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000D847 File Offset: 0x0000BA47
		public string MessageId { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000D850 File Offset: 0x0000BA50
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000D858 File Offset: 0x0000BA58
		public IBudget Budget { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000D861 File Offset: 0x0000BA61
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000D869 File Offset: 0x0000BA69
		public string RequestId { get; set; }

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D872 File Offset: 0x0000BA72
		public void CheckOverBudget()
		{
			if (this.Budget != null)
			{
				this.Budget.CheckOverBudget();
			}
		}

		// Token: 0x060002E1 RID: 737
		public abstract void Dispose();

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002E2 RID: 738
		public abstract string IdentityForFilteredTracing { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E3 RID: 739
		public abstract OrganizationId OrganizationId { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		public abstract ADObjectId QueryBaseDN { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		public abstract ExchangeVersionType RequestSchemaVersion { get; set; }

		// Token: 0x060002E8 RID: 744
		public abstract void ValidateContext();

		// Token: 0x060002E9 RID: 745 RVA: 0x0000D887 File Offset: 0x0000BA87
		protected ClientContext(IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId)
		{
			this.Budget = budget;
			this.TimeZone = timeZone;
			this.ClientCulture = clientCulture;
			this.MessageId = messageId;
		}
	}
}
