using System;
using System.Globalization;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A4 RID: 164
	internal sealed class InternalClientContext : ClientContext
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000EA88 File Offset: 0x0000CC88
		internal InternalClientContext(ClientSecurityContext clientSecurityContext, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId, ADUser adUser) : base(budget, timeZone, clientCulture, messageId)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			this.clientSecurityContext = clientSecurityContext;
			this.ownsClientSecurityContext = false;
			if (adUser != null)
			{
				this.adUser = adUser;
				this.organizationId = adUser.OrganizationId;
				this.adUserInitialized = true;
			}
			else
			{
				this.adUser = null;
				this.adUserInitialized = false;
			}
			if (this.clientSecurityContext.UserSid != null)
			{
				this.identityForFilteredTracing = this.clientSecurityContext.UserSid.ToString();
			}
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			this.clientSecurityContext.SetSecurityAccessToken(securityAccessToken);
			this.serializedSecurityContext = new SerializedSecurityContext(securityAccessToken);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000EB34 File Offset: 0x0000CD34
		internal InternalClientContext(ClientSecurityContext clientSecurityContext, OrganizationId organizationId, IBudget budget, ExTimeZone timeZone, CultureInfo clientCulture, string messageId) : base(budget, timeZone, clientCulture, messageId)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			this.clientSecurityContext = clientSecurityContext;
			this.ownsClientSecurityContext = false;
			if (this.clientSecurityContext.UserSid != null)
			{
				this.identityForFilteredTracing = this.clientSecurityContext.UserSid.ToString();
			}
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			this.clientSecurityContext.SetSecurityAccessToken(securityAccessToken);
			this.serializedSecurityContext = new SerializedSecurityContext(securityAccessToken);
			this.organizationId = organizationId;
			this.adUser = null;
			this.adUserInitialized = false;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		private InternalClientContext(InternalClientContext clientContext, ClientSecurityContext clientSecurityContext, bool ownsClientSecurityContext, ExchangeVersionType requestSchemaVersion) : base(clientContext.Budget, clientContext.TimeZone, clientContext.ClientCulture, clientContext.MessageId)
		{
			this.clientSecurityContext = clientSecurityContext;
			this.ownsClientSecurityContext = ownsClientSecurityContext;
			this.adUser = clientContext.adUser;
			this.adUserInitialized = clientContext.adUserInitialized;
			this.organizationId = clientContext.OrganizationId;
			this.serializedSecurityContext = clientContext.serializedSecurityContext;
			this.identityForFilteredTracing = clientContext.identityForFilteredTracing;
			this.RequestSchemaVersion = requestSchemaVersion;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000EC45 File Offset: 0x0000CE45
		public ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.clientSecurityContext;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000EC4D File Offset: 0x0000CE4D
		public ADUser ADUser
		{
			get
			{
				this.TryInitializeADUserIfNeeded();
				return this.adUser;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public bool ADUserInitialized
		{
			get
			{
				return this.adUserInitialized;
			}
			set
			{
				this.adUserInitialized = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000EC6D File Offset: 0x0000CE6D
		public override OrganizationId OrganizationId
		{
			get
			{
				if (this.organizationId != null)
				{
					return this.organizationId;
				}
				if (this.ADUser != null)
				{
					return this.ADUser.OrganizationId;
				}
				return OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000EC9D File Offset: 0x0000CE9D
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000ECC3 File Offset: 0x0000CEC3
		public override ADObjectId QueryBaseDN
		{
			get
			{
				if (this.queryBaseDnSpecified)
				{
					return this.queryBaseDn;
				}
				if (this.ADUser != null)
				{
					return this.ADUser.QueryBaseDN;
				}
				return null;
			}
			set
			{
				this.queryBaseDn = value;
				this.queryBaseDnSpecified = true;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000ECD3 File Offset: 0x0000CED3
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000ECDB File Offset: 0x0000CEDB
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		public override string IdentityForFilteredTracing
		{
			get
			{
				return this.identityForFilteredTracing;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public SerializedSecurityContext SerializedSecurityContext
		{
			get
			{
				return this.serializedSecurityContext;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public override void ValidateContext()
		{
			if (this.clientSecurityContext.UserSid == null)
			{
				InternalClientContext.Tracer.TraceDebug<InternalClientContext>((long)this.GetHashCode(), "{0}: Internal caller sid is null", this);
				throw new InvalidClientSecurityContextException();
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000ED26 File Offset: 0x0000CF26
		public InternalClientContext Clone()
		{
			return new InternalClientContext(this, this.ClientSecurityContext.Clone(), true, this.RequestSchemaVersion);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public override void Dispose()
		{
			if (this.ownsClientSecurityContext && this.clientSecurityContext != null)
			{
				this.clientSecurityContext.Dispose();
				this.clientSecurityContext = null;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000ED64 File Offset: 0x0000CF64
		public override string ToString()
		{
			return "InternalClientContext(" + this.clientSecurityContext.ToString() + ")";
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000ED80 File Offset: 0x0000CF80
		private bool TryInitializeADUserIfNeeded()
		{
			Exception ex = null;
			try
			{
				this.InitializeADUserIfNeeded();
			}
			catch (LocalizedException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				ex = ex4;
			}
			catch (FormatException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				InternalClientContext.Tracer.TraceError((long)this.GetHashCode(), "{0}: {1}: unable to find internal caller by SID {2} in the AD. Exception: {3}", new object[]
				{
					TraceContext.Get(),
					this,
					this.clientSecurityContext.UserSid,
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000EE2C File Offset: 0x0000D02C
		private void InitializeADUserIfNeeded()
		{
			if (this.adUserInitialized)
			{
				return;
			}
			SecurityIdentifier userSid = this.clientSecurityContext.UserSid;
			base.CheckOverBudget();
			ADSessionSettings sessionSettings;
			if (this.organizationId != null)
			{
				sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId);
			}
			else
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, sessionSettings, 397, "InitializeADUserIfNeeded", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\RequestDispatch\\InternalClientContext.cs");
			if (base.Budget != null)
			{
				tenantOrRootOrgRecipientSession.SessionSettings.AccountingObject = base.Budget;
			}
			this.adUser = (tenantOrRootOrgRecipientSession.FindBySid(userSid) as ADUser);
			InternalClientContext.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1}: found internal caller by SID {2} in the AD. User is {3}", new object[]
			{
				TraceContext.Get(),
				this,
				userSid,
				this.adUser
			});
			this.adUserInitialized = true;
		}

		// Token: 0x0400021B RID: 539
		private readonly bool ownsClientSecurityContext;

		// Token: 0x0400021C RID: 540
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x0400021D RID: 541
		private SerializedSecurityContext serializedSecurityContext;

		// Token: 0x0400021E RID: 542
		private ADUser adUser;

		// Token: 0x0400021F RID: 543
		private OrganizationId organizationId;

		// Token: 0x04000220 RID: 544
		private bool adUserInitialized;

		// Token: 0x04000221 RID: 545
		private string identityForFilteredTracing;

		// Token: 0x04000222 RID: 546
		private bool queryBaseDnSpecified;

		// Token: 0x04000223 RID: 547
		private ADObjectId queryBaseDn;

		// Token: 0x04000224 RID: 548
		private ExchangeVersionType requestSchemaVersion;

		// Token: 0x04000225 RID: 549
		private static readonly Trace Tracer = ExTraceGlobals.RequestRoutingTracer;
	}
}
