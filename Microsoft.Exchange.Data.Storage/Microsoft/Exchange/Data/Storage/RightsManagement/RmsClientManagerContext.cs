using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B50 RID: 2896
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RmsClientManagerContext
	{
		// Token: 0x06006908 RID: 26888 RVA: 0x001C277B File Offset: 0x001C097B
		public RmsClientManagerContext(OrganizationId orgId, IRmsLatencyTracker latencyTracker = null) : this(orgId, RmsClientManagerContext.ContextId.None, null, Guid.NewGuid(), null, latencyTracker)
		{
		}

		// Token: 0x06006909 RID: 26889 RVA: 0x001C278D File Offset: 0x001C098D
		public RmsClientManagerContext(OrganizationId orgId, RmsClientManagerContext.ContextId contextId, string contextValue, string publishingLicense = null) : this(orgId, contextId, contextValue, Guid.NewGuid(), null, null)
		{
			if (!string.IsNullOrEmpty(publishingLicense))
			{
				this.orgId = RmsClientManagerUtils.OrgIdFromPublishingLicenseOrDefault(publishingLicense, orgId, out this.externalDirectoryOrgId);
			}
		}

		// Token: 0x0600690A RID: 26890 RVA: 0x001C27BC File Offset: 0x001C09BC
		public RmsClientManagerContext(OrganizationId orgId, RmsClientManagerContext.ContextId contextId, string contextValue, Guid transactionId, Guid externalDirectoryOrgId) : this(orgId, contextId, contextValue, transactionId, null, null)
		{
			this.externalDirectoryOrgId = externalDirectoryOrgId;
		}

		// Token: 0x0600690B RID: 26891 RVA: 0x001C27D3 File Offset: 0x001C09D3
		public RmsClientManagerContext(OrganizationId orgId, RmsClientManagerContext.ContextId contextId, string contextValue, IADRecipientCache recipientCache, IRmsLatencyTracker latencyTracker, string publishingLicense, Guid externalDirectoryOrgId) : this(orgId, contextId, contextValue, recipientCache, latencyTracker, publishingLicense)
		{
			this.externalDirectoryOrgId = externalDirectoryOrgId;
		}

		// Token: 0x0600690C RID: 26892 RVA: 0x001C27EC File Offset: 0x001C09EC
		public RmsClientManagerContext(OrganizationId orgId, RmsClientManagerContext.ContextId contextId, string contextValue, IADRecipientCache recipientCache, IRmsLatencyTracker latencyTracker, string publishingLicense = null) : this(orgId, contextId, contextValue, Guid.NewGuid(), recipientCache, latencyTracker)
		{
			if (!string.IsNullOrEmpty(publishingLicense))
			{
				this.orgId = RmsClientManagerUtils.OrgIdFromPublishingLicenseOrDefault(publishingLicense, orgId, out this.externalDirectoryOrgId);
			}
		}

		// Token: 0x0600690D RID: 26893 RVA: 0x001C2820 File Offset: 0x001C0A20
		private RmsClientManagerContext(OrganizationId orgId, RmsClientManagerContext.ContextId contextId, string contextValue, Guid transactionId, IADRecipientCache recipientCache, IRmsLatencyTracker latencyTracker)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			this.orgId = orgId;
			this.tenantId = RmsClientManagerUtils.GetTenantGuidFromOrgId(this.orgId);
			this.contextId = contextId;
			this.contextValue = contextValue;
			this.transactionId = transactionId;
			this.recipientCache = recipientCache;
			this.latencyTracker = (latencyTracker ?? NoopRmsLatencyTracker.Instance);
			if (this.recipientCache != null && this.recipientCache.ADSession != null)
			{
				this.recipientSession = this.recipientCache.ADSession;
				return;
			}
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, this.orgId.Equals(OrganizationId.ForestWideOrgId) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 248, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\rightsmanagement\\RmsClientManagerContext.cs");
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x0600690E RID: 26894 RVA: 0x001C28EB File Offset: 0x001C0AEB
		// (set) Token: 0x0600690F RID: 26895 RVA: 0x001C28F3 File Offset: 0x001C0AF3
		public Guid SystemProbeId { get; set; }

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x06006910 RID: 26896 RVA: 0x001C28FC File Offset: 0x001C0AFC
		public OrganizationId OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x06006911 RID: 26897 RVA: 0x001C2904 File Offset: 0x001C0B04
		public Guid ExternalDirectoryOrgId
		{
			get
			{
				return this.externalDirectoryOrgId;
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x06006912 RID: 26898 RVA: 0x001C290C File Offset: 0x001C0B0C
		public Guid TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06006913 RID: 26899 RVA: 0x001C2914 File Offset: 0x001C0B14
		public IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x001C291C File Offset: 0x001C0B1C
		public RmsClientManagerContext.ContextId ContextID
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x06006915 RID: 26901 RVA: 0x001C2924 File Offset: 0x001C0B24
		public string ContextValue
		{
			get
			{
				return this.contextValue;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x06006916 RID: 26902 RVA: 0x001C292C File Offset: 0x001C0B2C
		public string ContextStringForm
		{
			get
			{
				if (this.contextId == RmsClientManagerContext.ContextId.None || string.IsNullOrEmpty(this.contextValue))
				{
					return null;
				}
				if (string.IsNullOrEmpty(this.contextStringForm))
				{
					this.contextStringForm = this.contextId.ToString() + ":" + this.contextValue;
				}
				return this.contextStringForm;
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x06006917 RID: 26903 RVA: 0x001C2989 File Offset: 0x001C0B89
		public Guid TransactionId
		{
			get
			{
				return this.transactionId;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x06006918 RID: 26904 RVA: 0x001C2991 File Offset: 0x001C0B91
		public IRmsLatencyTracker LatencyTracker
		{
			get
			{
				return this.latencyTracker;
			}
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x001C299C File Offset: 0x001C0B9C
		public ADRawEntry ResolveRecipient(string recipient)
		{
			ArgumentValidator.ThrowIfNull("recipient", recipient);
			if (this.recipientCache == null)
			{
				this.recipientCache = new ADRecipientCache<ADRawEntry>(RmsClientManagerContext.propertyDefinitions, 1, this.orgId);
			}
			return this.recipientCache.FindAndCacheRecipient(new SmtpProxyAddress(recipient, true)).Data;
		}

		// Token: 0x04003BB7 RID: 15287
		private static readonly ADPropertyDefinition[] propertyDefinitions = new ADPropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientTypeDetails
		};

		// Token: 0x04003BB8 RID: 15288
		private readonly RmsClientManagerContext.ContextId contextId;

		// Token: 0x04003BB9 RID: 15289
		private readonly string contextValue;

		// Token: 0x04003BBA RID: 15290
		private readonly Guid transactionId;

		// Token: 0x04003BBB RID: 15291
		private readonly Guid tenantId;

		// Token: 0x04003BBC RID: 15292
		private readonly IRecipientSession recipientSession;

		// Token: 0x04003BBD RID: 15293
		private readonly IRmsLatencyTracker latencyTracker;

		// Token: 0x04003BBE RID: 15294
		private readonly OrganizationId orgId;

		// Token: 0x04003BBF RID: 15295
		private readonly Guid externalDirectoryOrgId;

		// Token: 0x04003BC0 RID: 15296
		private IADRecipientCache recipientCache;

		// Token: 0x04003BC1 RID: 15297
		private string contextStringForm;

		// Token: 0x02000B51 RID: 2897
		internal enum ContextId
		{
			// Token: 0x04003BC4 RID: 15300
			None,
			// Token: 0x04003BC5 RID: 15301
			MessageId,
			// Token: 0x04003BC6 RID: 15302
			MailboxGuid,
			// Token: 0x04003BC7 RID: 15303
			AttachmentFileName
		}
	}
}
