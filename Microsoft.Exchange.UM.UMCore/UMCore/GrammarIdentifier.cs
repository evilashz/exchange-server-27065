using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000273 RID: 627
	internal class GrammarIdentifier : IEquatable<GrammarIdentifier>
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00052CA7 File Offset: 0x00050EA7
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x00052CAF File Offset: 0x00050EAF
		internal OrganizationId OrgId { get; private set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00052CB8 File Offset: 0x00050EB8
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00052CC0 File Offset: 0x00050EC0
		internal string GrammarName { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00052CC9 File Offset: 0x00050EC9
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00052CD1 File Offset: 0x00050ED1
		internal CultureInfo Culture { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00052CDA File Offset: 0x00050EDA
		internal string TenantTopLevelGrammarDirPath
		{
			get
			{
				if (this.tenantTopLevelGrammarDirPath == null)
				{
					this.tenantTopLevelGrammarDirPath = this.GetTenantTopLevelGrammarDirPath();
				}
				return this.tenantTopLevelGrammarDirPath;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00052CF6 File Offset: 0x00050EF6
		public GrammarIdentifier(OrganizationId tenantId, CultureInfo culture, string grammarFileName)
		{
			ValidateArgument.NotNull(culture, "Culture");
			ValidateArgument.NotNullOrEmpty(grammarFileName, "GrammarFileName");
			ValidateArgument.NotNull(tenantId, "OrganizationId");
			this.OrgId = tenantId;
			this.Culture = culture;
			this.GrammarName = grammarFileName;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00052D34 File Offset: 0x00050F34
		private string GetTenantTopLevelGrammarDirPath()
		{
			string text = Utils.GrammarPathFromCulture(this.Culture);
			text = Path.Combine(text, "Cache");
			if (this.OrgId.OrganizationalUnit == null)
			{
				text = Path.Combine(text, "Enterprise");
			}
			else
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.OrgId);
				string path = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId().ToString();
				text = Path.Combine(text, path);
			}
			return text;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00052DA0 File Offset: 0x00050FA0
		public static Guid GetSystemMailboxGuid(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarIdentifier.GetSystemMailboxGuid, orgId='{0}'", new object[]
			{
				orgId
			});
			Guid guid = Guid.Empty;
			ADUser localOrganizationMailboxByCapability = OrganizationMailbox.GetLocalOrganizationMailboxByCapability(orgId, OrganizationCapability.UMGrammarReady, true);
			if (localOrganizationMailboxByCapability == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarIdentifier.GetSystemMailboxGuid, orgId='{0}', No UMGrammarReady mailbox", new object[]
				{
					orgId
				});
				localOrganizationMailboxByCapability = OrganizationMailbox.GetLocalOrganizationMailboxByCapability(orgId, OrganizationCapability.UMGrammar, true);
			}
			if (localOrganizationMailboxByCapability != null)
			{
				guid = localOrganizationMailboxByCapability.ExchangeGuid;
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarIdentifier.GetSystemMailboxGuid, orgId='{0}', mbxGuid='{1}'", new object[]
				{
					orgId,
					guid
				});
				UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMGrammarUsage.ToString());
			}
			else
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarMailboxNotFound, null, new object[]
				{
					orgId
				});
				UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMGrammarUsage.ToString());
			}
			return guid;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00052E8D File Offset: 0x0005108D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GrammarIdentifier);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00052E9C File Offset: 0x0005109C
		public bool Equals(GrammarIdentifier other)
		{
			return other != null && (this.OrgId.Equals(other.OrgId) && this.Culture.Equals(other.Culture)) && this.GrammarName.Equals(other.GrammarName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00052EE8 File Offset: 0x000510E8
		public override int GetHashCode()
		{
			return this.OrgId.GetHashCode() ^ this.Culture.GetHashCode() ^ this.GrammarName.GetHashCode();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00052F0D File Offset: 0x0005110D
		public override string ToString()
		{
			return Path.Combine(this.TenantTopLevelGrammarDirPath, this.GrammarName);
		}

		// Token: 0x04000C28 RID: 3112
		private const string Enterprise = "Enterprise";

		// Token: 0x04000C29 RID: 3113
		private string tenantTopLevelGrammarDirPath;
	}
}
