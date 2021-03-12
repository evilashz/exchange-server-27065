using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000004 RID: 4
	public abstract class DirectoryBase : IDirectory
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002101 File Offset: 0x00000301
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002109 File Offset: 0x00000309
		public bool BypassContextADAccessValidation
		{
			get
			{
				return this.bypassContextADAccessValidation;
			}
			set
			{
				this.bypassContextADAccessValidation = value;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002114 File Offset: 0x00000314
		public ErrorCode PrimeDirectoryCaches(IExecutionContext context)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.PrimeDirectoryCachesImpl(context).Propagate((LID)29856U);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002141 File Offset: 0x00000341
		public ServerInfo GetServerInfo(IExecutionContext context)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetServerInfoImpl(context);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002151 File Offset: 0x00000351
		public void RefreshServerInfo(IExecutionContext context)
		{
			this.ValidateADAccessIsAllowed(context);
			this.RefreshServerInfoImpl(context);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002161 File Offset: 0x00000361
		public void RefreshDatabaseInfo(IExecutionContext context, Guid databaseGuid)
		{
			this.ValidateADAccessIsAllowed(context);
			this.RefreshDatabaseInfoImpl(context, databaseGuid);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002172 File Offset: 0x00000372
		public void RefreshMailboxInfo(IExecutionContext context, Guid mailboxGuid)
		{
			this.ValidateADAccessIsAllowed(context);
			this.RefreshMailboxInfoImpl(context, mailboxGuid);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002183 File Offset: 0x00000383
		public void RefreshOrganizationContainer(IExecutionContext context, Guid organizationGuid)
		{
			this.ValidateADAccessIsAllowed(context);
			this.RefreshOrganizationContainerImpl(context, organizationGuid);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002194 File Offset: 0x00000394
		public DatabaseInfo GetDatabaseInfo(IExecutionContext context, Guid databaseGuid)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetDatabaseInfoImpl(context, databaseGuid);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021A5 File Offset: 0x000003A5
		public MailboxInfo GetMailboxInfo(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, GetMailboxInfoFlags flags)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetMailboxInfoImpl(context, tenantHint, mailboxGuid, flags);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021B9 File Offset: 0x000003B9
		public MailboxInfo GetMailboxInfo(IExecutionContext context, TenantHint tenantHint, string legacyDN)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetMailboxInfoImpl(context, tenantHint, legacyDN);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021CB File Offset: 0x000003CB
		public AddressInfo GetAddressInfoByMailboxGuid(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, GetAddressInfoFlags flags)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetAddressInfoByMailboxGuidImpl(context, tenantHint, mailboxGuid, flags);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000021DF File Offset: 0x000003DF
		public AddressInfo GetAddressInfoByObjectId(IExecutionContext context, TenantHint tenantHint, Guid objectId)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetAddressInfoByObjectIdImpl(context, tenantHint, objectId);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000021F1 File Offset: 0x000003F1
		public AddressInfo GetAddressInfo(IExecutionContext context, TenantHint tenantHint, string legacyDN, bool loadPublicDelegates)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.GetAddressInfoImpl(context, tenantHint, legacyDN, loadPublicDelegates);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002205 File Offset: 0x00000405
		public TenantHint ResolveTenantHint(IExecutionContext context, byte[] tenantHintBlob)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.ResolveTenantHintImpl(context, tenantHintBlob);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002216 File Offset: 0x00000416
		public void PrePopulateCachesForMailbox(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, string domainController)
		{
			this.ValidateADAccessIsAllowed(context);
			this.PrePopulateCachesForMailboxImpl(context, tenantHint, mailboxGuid, domainController);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000222A File Offset: 0x0000042A
		public bool IsMemberOfDistributionList(IExecutionContext context, TenantHint tenantHint, AddressInfo addressInfo, Guid distributionListObjectId)
		{
			this.ValidateADAccessIsAllowed(context);
			return this.IsMemberOfDistributionListImpl(context, tenantHint, addressInfo, distributionListObjectId);
		}

		// Token: 0x06000026 RID: 38
		protected abstract ErrorCode PrimeDirectoryCachesImpl(IExecutionContext context);

		// Token: 0x06000027 RID: 39
		protected abstract ServerInfo GetServerInfoImpl(IExecutionContext context);

		// Token: 0x06000028 RID: 40
		protected abstract void RefreshServerInfoImpl(IExecutionContext context);

		// Token: 0x06000029 RID: 41
		protected abstract void RefreshDatabaseInfoImpl(IExecutionContext context, Guid databaseGuid);

		// Token: 0x0600002A RID: 42
		protected abstract void RefreshMailboxInfoImpl(IExecutionContext context, Guid mailboxGuid);

		// Token: 0x0600002B RID: 43
		protected abstract void RefreshOrganizationContainerImpl(IExecutionContext context, Guid organizationGuid);

		// Token: 0x0600002C RID: 44
		protected abstract DatabaseInfo GetDatabaseInfoImpl(IExecutionContext context, Guid databaseGuid);

		// Token: 0x0600002D RID: 45
		protected abstract MailboxInfo GetMailboxInfoImpl(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, GetMailboxInfoFlags flags);

		// Token: 0x0600002E RID: 46
		protected abstract MailboxInfo GetMailboxInfoImpl(IExecutionContext context, TenantHint tenantHint, string legacyDN);

		// Token: 0x0600002F RID: 47
		protected abstract AddressInfo GetAddressInfoByMailboxGuidImpl(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, GetAddressInfoFlags flags);

		// Token: 0x06000030 RID: 48
		protected abstract AddressInfo GetAddressInfoByObjectIdImpl(IExecutionContext context, TenantHint tenantHint, Guid objectId);

		// Token: 0x06000031 RID: 49
		protected abstract AddressInfo GetAddressInfoImpl(IExecutionContext context, TenantHint tenantHint, string legacyDN, bool loadPublicDelegates);

		// Token: 0x06000032 RID: 50
		protected abstract TenantHint ResolveTenantHintImpl(IExecutionContext context, byte[] tenantHintBlob);

		// Token: 0x06000033 RID: 51
		protected abstract void PrePopulateCachesForMailboxImpl(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, string domainController);

		// Token: 0x06000034 RID: 52
		protected abstract bool IsMemberOfDistributionListImpl(IExecutionContext context, TenantHint tenantHint, AddressInfo addressInfo, Guid distributionListObjectId);

		// Token: 0x06000035 RID: 53 RVA: 0x0000223E File Offset: 0x0000043E
		private void ValidateADAccessIsAllowed(IExecutionContext context)
		{
			if (this.bypassContextADAccessValidation)
			{
				return;
			}
			if (context.IsMailboxOperationStarted)
			{
				throw new InvalidOperationException("AD access is not allowed when we are in the middle of MAPI protocol request.");
			}
		}

		// Token: 0x04000002 RID: 2
		private bool bypassContextADAccessValidation;
	}
}
