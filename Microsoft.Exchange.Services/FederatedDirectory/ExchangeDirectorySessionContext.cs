using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000071 RID: 113
	internal sealed class ExchangeDirectorySessionContext : IDirectorySessionContext
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000E970 File Offset: 0x0000CB70
		public ExchangeDirectorySessionContext(ADUser accessingUser, ExchangePrincipal accessingPrincipal)
		{
			ArgumentValidator.ThrowIfNull("accessingUser", accessingUser);
			ArgumentValidator.ThrowIfNull("accessingPrincipal", accessingPrincipal);
			this.AccessingUser = accessingUser;
			this.AccessingPrincipal = accessingPrincipal;
			this.TenantContextId = ((accessingUser.OrganizationId == OrganizationId.ForestWideOrgId) ? Guid.Empty : new Guid(accessingUser.OrganizationId.ToExternalDirectoryOrganizationId()));
			this.UserId = new Guid(accessingUser.ExternalDirectoryObjectId);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000E9E7 File Offset: 0x0000CBE7
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000E9EF File Offset: 0x0000CBEF
		internal ADUser AccessingUser { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000EA00 File Offset: 0x0000CC00
		internal ExchangePrincipal AccessingPrincipal { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000EA09 File Offset: 0x0000CC09
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000EA11 File Offset: 0x0000CC11
		public Guid TenantContextId { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000EA1A File Offset: 0x0000CC1A
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000EA22 File Offset: 0x0000CC22
		public bool MockEnternalDirectoryObjectId { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000EA2B File Offset: 0x0000CC2B
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000EA33 File Offset: 0x0000CC33
		public Guid UserId { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public string UserPrincipalName
		{
			get
			{
				return this.AccessingUser.UserPrincipalName;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public ICredentials ActAsUserCredentials
		{
			get
			{
				if (this.actAsUserCredentials == null)
				{
					this.actAsUserCredentials = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(this.AccessingUser.OrganizationId, this.AccessingUser, null);
					LogWriter.TraceAndLog(new LogWriter.TraceMethod(ExchangeDirectorySessionContext.Tracer.TraceDebug), 4, this.GetHashCode(), "Created user credentials for {0}: {1}", new object[]
					{
						this.AccessingUser.UserPrincipalName,
						this.actAsUserCredentials
					});
				}
				return this.actAsUserCredentials;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		public ICredentials AppCredentials
		{
			get
			{
				if (this.appCredentials == null)
				{
					this.appCredentials = OAuthCredentials.GetOAuthCredentialsForAppToken(this.AccessingUser.OrganizationId, "dummyRealm");
					LogWriter.TraceAndLog(new LogWriter.TraceMethod(ExchangeDirectorySessionContext.Tracer.TraceDebug), 4, this.GetHashCode(), "Created app credentials for {0}: {1}", new object[]
					{
						this.AccessingUser.OrganizationId,
						this.appCredentials
					});
				}
				return this.appCredentials;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000EB3A File Offset: 0x0000CD3A
		public bool IsInTenantAdministrationRole
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000EB3D File Offset: 0x0000CD3D
		public ExchangeDirectorySessionContext.NewGroupDiagnostics CreationDiagnostics
		{
			get
			{
				if (this.creationDiagnostics == null)
				{
					this.creationDiagnostics = new ExchangeDirectorySessionContext.NewGroupDiagnostics();
				}
				return this.creationDiagnostics;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000EB58 File Offset: 0x0000CD58
		public override string ToString()
		{
			return "ExchangeDirectorySessionContext: " + this.AccessingUser.UserPrincipalName;
		}

		// Token: 0x0400058A RID: 1418
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;

		// Token: 0x0400058B RID: 1419
		private ICredentials appCredentials;

		// Token: 0x0400058C RID: 1420
		private ICredentials actAsUserCredentials;

		// Token: 0x0400058D RID: 1421
		private ExchangeDirectorySessionContext.NewGroupDiagnostics creationDiagnostics;

		// Token: 0x02000072 RID: 114
		public class NewGroupDiagnostics
		{
			// Token: 0x060002AD RID: 685 RVA: 0x0000EB7B File Offset: 0x0000CD7B
			public NewGroupDiagnostics()
			{
				this.stopWatch = new Stopwatch();
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060002AE RID: 686 RVA: 0x0000EB8E File Offset: 0x0000CD8E
			// (set) Token: 0x060002AF RID: 687 RVA: 0x0000EB96 File Offset: 0x0000CD96
			public TimeSpan? MailboxCreationTime { get; private set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000EB9F File Offset: 0x0000CD9F
			// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000EBA7 File Offset: 0x0000CDA7
			public TimeSpan? AADIdentityCreationTime { get; private set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
			// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
			public TimeSpan? GroupCreationTime { get; private set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000EBC1 File Offset: 0x0000CDC1
			// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
			public bool MailboxCreatedSuccessfully { get; set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000EBD2 File Offset: 0x0000CDD2
			// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000EBDA File Offset: 0x0000CDDA
			public Guid CmdletLogCorrelationId { get; set; }

			// Token: 0x060002B8 RID: 696 RVA: 0x0000EBE3 File Offset: 0x0000CDE3
			public void Start()
			{
				this.stopWatch.Start();
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
			public void Stop()
			{
				this.GroupCreationTime = new TimeSpan?(this.stopWatch.Elapsed);
				this.stopWatch.Stop();
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000EC13 File Offset: 0x0000CE13
			public void RecordAADTime()
			{
				this.AADIdentityCreationTime = new TimeSpan?(this.stopWatch.Elapsed);
			}

			// Token: 0x060002BB RID: 699 RVA: 0x0000EC2C File Offset: 0x0000CE2C
			public void RecordMailboxTime()
			{
				this.MailboxCreationTime = new TimeSpan?(this.stopWatch.Elapsed.Subtract(this.AADIdentityCreationTime.Value));
			}

			// Token: 0x04000593 RID: 1427
			private readonly Stopwatch stopWatch;
		}
	}
}
