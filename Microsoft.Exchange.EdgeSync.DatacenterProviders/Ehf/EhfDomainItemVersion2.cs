using System;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200001F RID: 31
	internal class EhfDomainItemVersion2 : EhfDomainItem
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00009EED File Offset: 0x000080ED
		private EhfDomainItemVersion2(ExSearchResultEntry entry, int[] connectorIds, EdgeSyncDiag diagSession) : base(entry, diagSession)
		{
			this.InitializeDomainSettings(connectorIds);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009EFE File Offset: 0x000080FE
		private EhfDomainItemVersion2(ExSearchResultEntry entry, int ehfCompanyId, int[] connectorIds, EdgeSyncDiag diagSession) : base(entry, ehfCompanyId, diagSession)
		{
			this.InitializeDomainSettings(connectorIds);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009F11 File Offset: 0x00008111
		public static EhfDomainItemVersion2 CreateEhfDomainItem(ExSearchResultEntry entry, int[] connectorIds, EdgeSyncDiag diagSession)
		{
			return new EhfDomainItemVersion2(entry, connectorIds, diagSession);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009F1B File Offset: 0x0000811B
		public static EhfDomainItemVersion2 CreateEhfDomainItem(ExSearchResultEntry entry, int ehfCompanyId, int[] connectorIds, EdgeSyncDiag diagSession)
		{
			return new EhfDomainItemVersion2(entry, ehfCompanyId, connectorIds, diagSession);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009F28 File Offset: 0x00008128
		public static EhfDomainItem CreateForOutboundOnlyTombstone(ExSearchResultEntry entry, EdgeSyncDiag diagSession)
		{
			EhfDomainItem ehfDomainItem = EhfDomainItem.CreateForTombstone(entry, diagSession);
			ehfDomainItem.Domain.Name = AcceptedDomain.FormatEhfOutboundOnlyDomainName(ehfDomainItem.Domain.Name, ehfDomainItem.Domain.DomainGuid.Value);
			return ehfDomainItem;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009F6C File Offset: 0x0000816C
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00009F74 File Offset: 0x00008174
		public bool TriedToUpdateDomain
		{
			get
			{
				return this.triedToUpdateDomain;
			}
			set
			{
				this.triedToUpdateDomain = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009F7D File Offset: 0x0000817D
		public bool DomainCreatedWithGuid
		{
			get
			{
				return this.domainCreatedWithGuid;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009F88 File Offset: 0x00008188
		protected override string GetDomainName()
		{
			string domainName = base.GetDomainName();
			if (base.ADEntry.IsDeleted || !base.OutboundOnly)
			{
				return domainName;
			}
			return AcceptedDomain.FormatEhfOutboundOnlyDomainName(domainName, base.Domain.DomainGuid.Value);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009FCC File Offset: 0x000081CC
		private void InitializeDomainSettings(int[] connectorIds)
		{
			if (base.Domain.Settings == null)
			{
				base.Domain.Settings = new DomainConfigurationSettings();
			}
			base.Domain.Settings.DomainName = base.Domain.Name;
			base.Domain.Settings.DomainGuid = base.Domain.DomainGuid;
			base.Domain.Settings.CompanyId = base.Domain.CompanyId;
			base.Domain.Settings.MailFlowType = DomainMailFlowType.InboundOutbound;
			if (connectorIds != null)
			{
				base.Domain.Settings.ConnectorId = connectorIds;
			}
			this.domainCreatedWithGuid = (base.OutboundOnly && !base.IsDeleted);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A088 File Offset: 0x00008288
		public void SetAsGuidDomain()
		{
			if (this.domainCreatedWithGuid)
			{
				throw new InvalidOperationException(string.Format("Trying to set a Guid-Domain twice. DomainName: {0}", base.Domain.Name));
			}
			base.Domain.Name = AcceptedDomain.FormatEhfOutboundOnlyDomainName(base.Domain.Name, base.GetDomainGuid());
			base.Domain.Settings.DomainName = base.Domain.Name;
			this.domainCreatedWithGuid = true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A0FC File Offset: 0x000082FC
		public EhfADResultCode TrySetDuplicateDomain(EhfADAdapter ehfADAdapter, bool isDuplicate)
		{
			int flagsValue = base.GetFlagsValue("msExchTransportInboundSettings");
			int num;
			if (isDuplicate)
			{
				num = (flagsValue | 1);
			}
			else
			{
				num = (flagsValue & -2);
			}
			if (flagsValue == num)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "DuplicateDetected field is set to the expected value {0} for domain <{1}>:<{2}>. Not setting the value.", new object[]
				{
					flagsValue,
					base.DistinguishedName,
					base.GetDomainGuid()
				});
				return EhfADResultCode.Success;
			}
			EhfADResultCode result;
			try
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Setting DuplicateDetected field from {0} to {1} for domain <{2}>:<{3}>.", new object[]
				{
					flagsValue,
					num,
					base.DistinguishedName,
					base.GetDomainGuid()
				});
				ehfADAdapter.SetAttributeValue(base.GetDomainGuid(), "msExchTransportInboundSettings", num.ToString());
				result = EhfADResultCode.Success;
			}
			catch (ExDirectoryException ex)
			{
				if (ex.ResultCode == ResultCode.NoSuchObject)
				{
					base.DiagSession.LogAndTraceException(ex, "NoSuchObject error occurred while trying to set the DuplicateDetected flag for domain <{0}>:<{1}>", new object[]
					{
						base.DistinguishedName,
						base.GetDomainGuid()
					});
					result = EhfADResultCode.NoSuchObject;
				}
				else
				{
					base.DiagSession.LogAndTraceException(ex, "Exception occurred while trying to set the DuplicateDetected flag for domain <{0}>:<{1}>", new object[]
					{
						base.DistinguishedName,
						base.GetDomainGuid()
					});
					result = EhfADResultCode.Failure;
				}
			}
			return result;
		}

		// Token: 0x04000087 RID: 135
		private bool triedToUpdateDomain;

		// Token: 0x04000088 RID: 136
		private bool domainCreatedWithGuid;
	}
}
