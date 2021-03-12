using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSSecurity;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F5 RID: 245
	internal abstract class ExternalClientContext : ClientContext
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001D060 File Offset: 0x0001B260
		internal ExternalClientContext(SmtpAddress emailAddress, WSSecurityHeader wsSecurityHeader, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId) : base(budget, timeZone, clientCulture, messageId)
		{
			this.emailAddress = emailAddress;
			this.wsSecurityHeader = wsSecurityHeader;
		}

		// Token: 0x06000689 RID: 1673
		public abstract ProxyAuthenticator CreateInternalProxyAuthenticator();

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001D084 File Offset: 0x0001B284
		public SmtpAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001D08C File Offset: 0x0001B28C
		public WSSecurityHeader WSSecurityHeader
		{
			get
			{
				return this.wsSecurityHeader;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001D094 File Offset: 0x0001B294
		public override OrganizationId OrganizationId
		{
			get
			{
				return OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001D09B File Offset: 0x0001B29B
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001D09E File Offset: 0x0001B29E
		public override ADObjectId QueryBaseDN
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException("Cannot set QueryBaseDN on ExternalClientContext.");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001D0AA File Offset: 0x0001B2AA
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001D0B2 File Offset: 0x0001B2B2
		public override ExchangeVersionType RequestSchemaVersion
		{
			get
			{
				return this.requestSchemaVersion;
			}
			set
			{
				this.requestSchemaVersion = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001D0BB File Offset: 0x0001B2BB
		public override string IdentityForFilteredTracing
		{
			get
			{
				return this.emailAddress.ToString();
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001D0CE File Offset: 0x0001B2CE
		public override void ValidateContext()
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		public override void Dispose()
		{
		}

		// Token: 0x040003E9 RID: 1001
		private SmtpAddress emailAddress;

		// Token: 0x040003EA RID: 1002
		private WSSecurityHeader wsSecurityHeader;

		// Token: 0x040003EB RID: 1003
		private ExchangeVersionType requestSchemaVersion = ExchangeVersionType.Exchange2010;
	}
}
