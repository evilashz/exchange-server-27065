using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200036B RID: 875
	[Serializable]
	public class MsoTenantSyncRequest : ConfigurableObject
	{
		// Token: 0x06001E91 RID: 7825 RVA: 0x00084BE0 File Offset: 0x00082DE0
		public MsoTenantSyncRequest() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00084BF0 File Offset: 0x00082DF0
		internal MsoTenantSyncRequest(MsoTenantCookieContainer organization, MsoFullSyncCookie recipientCookie, MsoFullSyncCookie companyCookie) : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
			this.propertyBag.SetField(this.propertyBag.ObjectIdentityPropertyDefinition, organization.Identity);
			this.ExternalDirectoryOrganizationId = organization.ExternalDirectoryOrganizationId;
			this.ServiceInstanceId = organization.DirSyncServiceInstance;
			MsoFullSyncCookie msoFullSyncCookie = recipientCookie ?? companyCookie;
			if (msoFullSyncCookie != null)
			{
				this.SyncType = msoFullSyncCookie.SyncType;
				this.Requestor = msoFullSyncCookie.SyncRequestor;
				this.WhenCreated = ((msoFullSyncCookie.WhenSyncRequested != DateTime.MinValue) ? new DateTime?(msoFullSyncCookie.WhenSyncRequested) : null);
				this.WhenSyncStarted = ((msoFullSyncCookie.WhenSyncStarted != DateTime.MinValue) ? new DateTime?(msoFullSyncCookie.WhenSyncStarted) : null);
				if (recipientCookie != null)
				{
					this.WhenLastRecipientCookieCommitted = ((recipientCookie.Timestamp != DateTime.MinValue) ? new DateTime?(recipientCookie.Timestamp) : null);
				}
				if (companyCookie != null)
				{
					this.WhenLastCompanyCookieCommitted = ((companyCookie.Timestamp != DateTime.MinValue) ? new DateTime?(companyCookie.Timestamp) : null);
				}
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00084D3D File Offset: 0x00082F3D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MsoTenantSyncRequest.Schema;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x00084D44 File Offset: 0x00082F44
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x00084D56 File Offset: 0x00082F56
		public TenantSyncType SyncType
		{
			get
			{
				return (TenantSyncType)base[MsoTenantSyncRequestSchema.TenantSyncType];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.TenantSyncType] = value;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x00084D69 File Offset: 0x00082F69
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x00084D7B File Offset: 0x00082F7B
		public string Requestor
		{
			get
			{
				return (string)base[MsoTenantSyncRequestSchema.Requestor];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.Requestor] = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00084D89 File Offset: 0x00082F89
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x00084D9B File Offset: 0x00082F9B
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)base[MsoTenantSyncRequestSchema.ExternalDirectoryOrganizationId];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00084DA9 File Offset: 0x00082FA9
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x00084DBB File Offset: 0x00082FBB
		public string ServiceInstanceId
		{
			get
			{
				return (string)base[MsoTenantSyncRequestSchema.ServiceInstanceId];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.ServiceInstanceId] = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x00084DC9 File Offset: 0x00082FC9
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x00084DDB File Offset: 0x00082FDB
		public DateTime? WhenSyncStarted
		{
			get
			{
				return (DateTime?)base[MsoTenantSyncRequestSchema.WhenSyncStarted];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.WhenSyncStarted] = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x00084DEE File Offset: 0x00082FEE
		// (set) Token: 0x06001E9F RID: 7839 RVA: 0x00084E00 File Offset: 0x00083000
		public DateTime? WhenLastRecipientCookieCommitted
		{
			get
			{
				return (DateTime?)base[MsoTenantSyncRequestSchema.WhenLastRecipientCookieCommitted];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.WhenLastRecipientCookieCommitted] = value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x00084E13 File Offset: 0x00083013
		// (set) Token: 0x06001EA1 RID: 7841 RVA: 0x00084E25 File Offset: 0x00083025
		public DateTime? WhenLastCompanyCookieCommitted
		{
			get
			{
				return (DateTime?)base[MsoTenantSyncRequestSchema.WhenLastCompanyCookieCommitted];
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.WhenLastCompanyCookieCommitted] = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x00084E38 File Offset: 0x00083038
		// (set) Token: 0x06001EA3 RID: 7843 RVA: 0x00084E4F File Offset: 0x0008304F
		public DateTime? WhenCreated
		{
			get
			{
				return new DateTime?((DateTime)base[MsoTenantSyncRequestSchema.WhenCreated]);
			}
			private set
			{
				base[MsoTenantSyncRequestSchema.WhenCreated] = value;
			}
		}

		// Token: 0x04001934 RID: 6452
		private static readonly MsoTenantSyncRequestSchema Schema = ObjectSchema.GetInstance<MsoTenantSyncRequestSchema>();
	}
}
