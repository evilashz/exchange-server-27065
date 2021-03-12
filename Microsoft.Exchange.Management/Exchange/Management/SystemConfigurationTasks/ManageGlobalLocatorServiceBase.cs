using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A03 RID: 2563
	public class ManageGlobalLocatorServiceBase : Task
	{
		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06005BE6 RID: 23526 RVA: 0x00183F69 File Offset: 0x00182169
		// (set) Token: 0x06005BE7 RID: 23527 RVA: 0x00183F80 File Offset: 0x00182180
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "MsaUserNetIDParameterSet")]
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			}
			set
			{
				base.Fields["ExternalDirectoryOrganizationId"] = value;
			}
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00183F98 File Offset: 0x00182198
		internal void WriteGlsTenantNotFoundError(Guid orgGuid)
		{
			this.WriteGlsTenantNotFoundError(orgGuid.ToString());
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x00183FAD File Offset: 0x001821AD
		internal void WriteGlsTenantNotFoundError(string orgGuidOrDomain)
		{
			base.WriteError(new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(orgGuidOrDomain)), ExchangeErrorCategory.Client, null);
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x00183FC6 File Offset: 0x001821C6
		internal void WriteGlsDomainNotFoundError(string domain)
		{
			base.WriteError(new GlsDomainNotFoundException(DirectoryStrings.DomainNotFoundInGlsError(domain)), ExchangeErrorCategory.Client, null);
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x00183FDF File Offset: 0x001821DF
		internal void WriteGlsMsaUserNotFoundError(string msaUserNetId)
		{
			base.WriteError(new GlsMsaUserNotFoundException(DirectoryStrings.MsaUserNotFoundInGlsError(msaUserNetId)), ExchangeErrorCategory.Client, null);
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x00183FF8 File Offset: 0x001821F8
		internal void WriteGlsMsaUserAlreadyExistsError(string msaUserNetId)
		{
			base.WriteError(new GlsMsaUserAlreadyExistsException(DirectoryStrings.MsaUserAlreadyExistsInGlsError(msaUserNetId)), ExchangeErrorCategory.Client, null);
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x00184011 File Offset: 0x00182211
		internal void WriteInvalidFqdnError(string fqdn)
		{
			base.WriteError(new ArgumentException(DirectoryStrings.InvalidPartitionFqdn(fqdn)), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x04003446 RID: 13382
		internal const string ExternalDirectoryOrganizationIdParameterName = "ExternalDirectoryOrganizationId";

		// Token: 0x04003447 RID: 13383
		internal const string ExternalDirectoryOrganizationIdParameterSetName = "ExternalDirectoryOrganizationIdParameterSet";

		// Token: 0x04003448 RID: 13384
		internal const string DomainNameParameterName = "DomainName";

		// Token: 0x04003449 RID: 13385
		internal const string DomainNameParameterSetName = "DomainNameParameterSet";

		// Token: 0x0400344A RID: 13386
		internal const string ResourceForestParameterName = "ResourceForest";

		// Token: 0x0400344B RID: 13387
		internal const string AccountForestParameterName = "AccountForest";

		// Token: 0x0400344C RID: 13388
		internal const string PrimarySiteParameterName = "PrimarySite";

		// Token: 0x0400344D RID: 13389
		internal const string SmtpNextHopDomainParameterName = "SmtpNextHopDomain";

		// Token: 0x0400344E RID: 13390
		internal const string TenantFlagsParameterName = "TenantFlags";

		// Token: 0x0400344F RID: 13391
		internal const string TenantContainerCNParameterName = "TenantContainerCN";

		// Token: 0x04003450 RID: 13392
		internal const string DomainFlagsParameterName = "DomainFlags";

		// Token: 0x04003451 RID: 13393
		internal const string DomainTypeParameterName = "DomainType";

		// Token: 0x04003452 RID: 13394
		internal const string DomainInUseParameterName = "DomainInUse";

		// Token: 0x04003453 RID: 13395
		internal const string ShowDomainNamesParameterName = "ShowDomainNames";

		// Token: 0x04003454 RID: 13396
		internal const string UseOfflineGLSParameterName = "UseOfflineGLS";

		// Token: 0x04003455 RID: 13397
		internal const string MsaUserMemberNameParameterName = "MsaUserMemberName";

		// Token: 0x04003456 RID: 13398
		internal const string MsaUserNetIdParameterName = "MsaUserNetID";

		// Token: 0x04003457 RID: 13399
		internal const string MsaUserNetIdParameterSetName = "MsaUserNetIDParameterSet";

		// Token: 0x04003458 RID: 13400
		internal const string DomainNameSuffix = "{0}:{1}";

		// Token: 0x04003459 RID: 13401
		internal const string ADOnlySuffix = "ADOnly";

		// Token: 0x0400345A RID: 13402
		internal const string ADAndGLSSuffix = "ADAndGLS";

		// Token: 0x0400345B RID: 13403
		internal const string GlsOnlySuffix = "GlsOnly";
	}
}
