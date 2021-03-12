using System;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200001E RID: 30
	internal class EhfDomainItem : EhfSyncItem
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00009AF2 File Offset: 0x00007CF2
		protected EhfDomainItem(ExSearchResultEntry entry, EdgeSyncDiag diagSession) : this(entry, 0, diagSession)
		{
			this.domain.CompanyId = this.GetEntryCompanyId();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009B10 File Offset: 0x00007D10
		protected EhfDomainItem(ExSearchResultEntry entry, int ehfCompanyId, EdgeSyncDiag diagSession) : base(entry, diagSession)
		{
			if (!entry.Attributes.ContainsKey("msEdgeSyncEhfCompanyGuid"))
			{
				throw new InvalidOperationException(string.Format("DomainItem <{0}> does not contain the companyGuid", entry.DistinguishedName));
			}
			if (!entry.IsDeleted)
			{
				this.InitializeDomainType();
			}
			this.domain = new Domain();
			this.domain.CompanyId = ehfCompanyId;
			this.domain.DomainGuid = new Guid?(base.GetObjectGuid());
			this.domain.Name = this.GetDomainName();
			this.domain.InheritFromCompany = (InheritanceSettings.InheritRecipientLevelRoutingConfig | InheritanceSettings.InheritSkipListConfig | InheritanceSettings.InheritDirectoryEdgeBlockModeConfig);
			this.domain.IsEnabled = true;
			this.domain.IsValid = true;
			this.domain.MailServerType = MailServerType.Exchange2007;
			this.domain.SmtpProfileName = "InboundSmtpProfile";
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00009BDA File Offset: 0x00007DDA
		public AcceptedDomainType AcceptedDomainType
		{
			get
			{
				if (base.ADEntry.IsDeleted)
				{
					throw new InvalidOperationException("Cannot get domain type of a deleted domain");
				}
				return this.acceptedDomainType;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00009BFA File Offset: 0x00007DFA
		public bool OutboundOnly
		{
			get
			{
				return this.outboundOnly;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00009C02 File Offset: 0x00007E02
		public Domain Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00009C0A File Offset: 0x00007E0A
		public string Name
		{
			get
			{
				return this.domain.Name;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009C17 File Offset: 0x00007E17
		public static EhfDomainItem CreateForActive(ExSearchResultEntry entry, EdgeSyncDiag diagSession)
		{
			return new EhfDomainItem(entry, diagSession);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009C20 File Offset: 0x00007E20
		public static EhfDomainItem CreateForTombstone(ExSearchResultEntry entry, EdgeSyncDiag diagSession)
		{
			return new EhfDomainItem(entry, diagSession);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009C2C File Offset: 0x00007E2C
		public static EhfDomainItem CreateIfAuthoritative(ExSearchResultEntry entry, int ehfCompanyId, EdgeSyncDiag diagSession)
		{
			EhfDomainItem ehfDomainItem = new EhfDomainItem(entry, ehfCompanyId, diagSession);
			if (ehfDomainItem.AcceptedDomainType != AcceptedDomainType.Authoritative)
			{
				return null;
			}
			return ehfDomainItem;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009C50 File Offset: 0x00007E50
		public static void ClearForceDomainSyncFlagFromPerimeterConfig(EhfADAdapter adAdapter, Guid companyGuid)
		{
			adAdapter.SetAttributeValue(companyGuid, "msExchTransportInboundSettings", 0.ToString());
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009C74 File Offset: 0x00007E74
		public Guid GetDomainGuid()
		{
			return this.domain.DomainGuid.Value;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009C94 File Offset: 0x00007E94
		public EhfADResultCode TryClearForceDomainSyncFlagFromPerimeterConfig(EhfADAdapter adAdapter)
		{
			if (!this.clearForceDomainSyncFlag)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.High, "Domain <{0}> does not have [clearForceDomainSyncFlag] set. Not clearing the forceDomainSync flag", new object[]
				{
					base.DistinguishedName
				});
				return EhfADResultCode.Success;
			}
			Guid entryCompanyGuid = this.GetEntryCompanyGuid();
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Clearing the force-domainsync flag set on the perimeterconfig with Guid <{0}> for domain <{1}>.", new object[]
			{
				entryCompanyGuid,
				base.DistinguishedName
			});
			try
			{
				EhfDomainItem.ClearForceDomainSyncFlagFromPerimeterConfig(adAdapter, entryCompanyGuid);
			}
			catch (ExDirectoryException ex)
			{
				if (ex.ResultCode == ResultCode.NoSuchObject)
				{
					base.DiagSession.LogAndTraceException(ex, "NoSuchObject error occurred while trying to clear the ForceDomainSync flag in Perimeter Settings for domain <{0}>:<{1}>", new object[]
					{
						base.DistinguishedName,
						entryCompanyGuid
					});
					return EhfADResultCode.NoSuchObject;
				}
				base.DiagSession.LogAndTraceException(ex, "Exception occurred while trying to clear the ForceDomainSync flag in Perimeter Settings for domain <{0}>:<{1}>", new object[]
				{
					base.DistinguishedName,
					entryCompanyGuid
				});
				base.DiagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfFailedToClearForceDomainSyncFlagFromDomainSync, null, new object[]
				{
					base.DistinguishedName,
					ex.Message
				});
				return EhfADResultCode.Failure;
			}
			return EhfADResultCode.Success;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public bool SetForcedDomainSync(Guid ehfCompanyGuid)
		{
			if (this.GetEntryCompanyGuid() == ehfCompanyGuid)
			{
				this.clearForceDomainSyncFlag = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009DE4 File Offset: 0x00007FE4
		protected virtual string GetDomainName()
		{
			DirectoryAttribute attribute = base.ADEntry.GetAttribute("msExchAcceptedDomainName");
			if (attribute == null || attribute[0] == null)
			{
				throw new InvalidOperationException("Domain name attribute is not present in accepted domain AD entry");
			}
			return (string)attribute[0];
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009E28 File Offset: 0x00008028
		private int GetEntryCompanyId()
		{
			DirectoryAttribute attribute = base.ADEntry.GetAttribute("msExchTenantPerimeterSettingsOrgID");
			if (attribute == null || attribute[0] == null)
			{
				throw new InvalidOperationException("EHF company AD attribute is not present in accepted domain AD entry, but it was supposed to be added by PreDecorate delegate");
			}
			return int.Parse((string)attribute[0]);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009E70 File Offset: 0x00008070
		private Guid GetEntryCompanyGuid()
		{
			DirectoryAttribute attribute = base.ADEntry.GetAttribute("msEdgeSyncEhfCompanyGuid");
			if (attribute == null || attribute[0] == null)
			{
				throw new InvalidOperationException("EHF companyGuid is not present in accepted domain AD entry, but it was supposed to be added by PreDecorate delegate or during CreateDomainForNewCompany");
			}
			return new Guid((string)attribute[0]);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009EB8 File Offset: 0x000080B8
		private void InitializeDomainType()
		{
			int flagsValue = base.GetFlagsValue("msExchAcceptedDomainFlags");
			this.acceptedDomainType = (AcceptedDomainType)(flagsValue & 3);
			this.outboundOnly = ((flagsValue & 256) != 0);
		}

		// Token: 0x04000082 RID: 130
		private const InheritanceSettings DomainInheritanceSettings = InheritanceSettings.InheritRecipientLevelRoutingConfig | InheritanceSettings.InheritSkipListConfig | InheritanceSettings.InheritDirectoryEdgeBlockModeConfig;

		// Token: 0x04000083 RID: 131
		private Domain domain;

		// Token: 0x04000084 RID: 132
		private AcceptedDomainType acceptedDomainType;

		// Token: 0x04000085 RID: 133
		private bool outboundOnly;

		// Token: 0x04000086 RID: 134
		private bool clearForceDomainSyncFlag;
	}
}
