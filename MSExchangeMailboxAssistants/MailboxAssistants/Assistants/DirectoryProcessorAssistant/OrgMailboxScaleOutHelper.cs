using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001AE RID: 430
	internal class OrgMailboxScaleOutHelper
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x000628F2 File Offset: 0x00060AF2
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x000628FA File Offset: 0x00060AFA
		private RunData RunData { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00062903 File Offset: 0x00060B03
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0006290B File Offset: 0x00060B0B
		private Logger Logger { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00062914 File Offset: 0x00060B14
		private OrganizationId OrgId
		{
			get
			{
				return this.RunData.OrgId;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00062924 File Offset: 0x00060B24
		public OrgMailboxScaleOutHelper(RunData runData, Logger logger)
		{
			ValidateArgument.NotNull(runData, "runData");
			ValidateArgument.NotNull(logger, "logger");
			this.RunData = runData;
			this.Logger = logger;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00062980 File Offset: 0x00060B80
		internal static bool IsFactoryDefaultMailbox(string mailboxName)
		{
			ValidateArgument.NotNull(mailboxName, "mailboxName");
			return string.Equals(mailboxName, "SystemMailbox{bb558c35-97f1-4cb9-8ff7-d53741dc928c}", StringComparison.Ordinal);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0006299C File Offset: 0x00060B9C
		public void CheckScaleRequirements()
		{
			this.Logger.TraceDebug(this, "Entering CheckScaleRequirements", new object[0]);
			if (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.OrgMailboxCheckScaleRequirements.Enabled)
			{
				try
				{
					IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(this.OrgId, null, null, false);
					ADUser aduser = iadrecipientLookup.LookupByExchangeGuid(this.RunData.MailboxGuid) as ADUser;
					if (aduser != null && aduser.PersistedCapabilities.Contains((Capability)this.CapabilityRequiringScaling))
					{
						if (OrgMailboxScaleOutHelper.IsFactoryDefaultMailbox(aduser.Name))
						{
							this.Logger.TraceDebug(this, "CheckOrgMailboxScaleRequirements - Checking tenant size, mailbox='{0}'", new object[]
							{
								aduser.Name
							});
							int mailboxCount = 0;
							int contactCount = 0;
							int mailUserCount = 0;
							this.GetTenantRecipientCount(out mailboxCount, out contactCount, out mailUserCount);
							int organizationMailboxCount = this.GetOrganizationMailboxCount();
							this.Logger.TraceDebug(this, "CheckOrgMailboxScaleRequirements - Scale out threshold='{0}'", new object[]
							{
								OrgMailboxScaleOutHelper.scaleOutThreshold
							});
							bool shouldCapabilityExist = this.ShouldStampScaleOutCapability(mailboxCount, contactCount, mailUserCount, organizationMailboxCount, OrgMailboxScaleOutHelper.scaleOutThreshold);
							this.UpdateOrganizationScaleOutCapability(aduser, shouldCapabilityExist);
						}
						this.UpdateOrganizationMailboxCapabilities(aduser);
					}
				}
				catch (LocalizedException obj)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SetScaleOutCapabilityFailed, null, new object[]
					{
						this.RunData.TenantId,
						this.RunData.MailboxGuid,
						this.RunData.RunId,
						CommonUtil.ToEventLogString(obj)
					});
				}
			}
			this.Logger.TraceDebug(this, "Exiting CheckScaleRequirements", new object[0]);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00062B48 File Offset: 0x00060D48
		internal void GetTenantRecipientCount(out int mailboxCount, out int contactCount, out int mailUserCount)
		{
			this.Logger.TraceDebug(this, "GetOrganizationRecipientCount", new object[0]);
			mailboxCount = 0;
			contactCount = 0;
			mailUserCount = 0;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), this.OrgId, null, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 227, "GetTenantRecipientCount", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\DirectoryProcessor\\OrgMailboxScaleOutHelper.cs");
			mailboxCount = SystemAddressListMemberCount.GetCount(tenantOrTopologyConfigurationSession, this.OrgId, "All Mailboxes(VLV)", true);
			contactCount = SystemAddressListMemberCount.GetCount(tenantOrTopologyConfigurationSession, this.OrgId, "All Contacts(VLV)", true);
			mailUserCount = SystemAddressListMemberCount.GetCount(tenantOrTopologyConfigurationSession, this.OrgId, "All Mail Users(VLV)", true);
			this.Logger.TraceDebug(this, "GetOrganizationRecipientCount - Mailbox count='{0}', Contact count='{1}', Mail user count='{2}'", new object[]
			{
				mailboxCount,
				contactCount,
				mailUserCount
			});
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00062C18 File Offset: 0x00060E18
		internal int GetOrganizationMailboxCount()
		{
			this.Logger.TraceDebug(this, "GetOrganizationMailboxCount", new object[0]);
			ADUser[] array = OrganizationMailbox.FindByOrganizationId(this.OrgId, this.CapabilityRequiringScaling);
			int num = array.Length;
			this.Logger.TraceDebug(this, "GetOrganizationMailboxCount - Count='{0}'", new object[]
			{
				num
			});
			return num;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00062C78 File Offset: 0x00060E78
		internal void UpdateOrganizationScaleOutCapability(ADUser orgMailbox, bool shouldCapabilityExist)
		{
			this.Logger.TraceDebug(this, "UpdateOrganizationScaleOutCapability - shouldCapabilityExist='{0}'", new object[]
			{
				shouldCapabilityExist
			});
			Capability item = Capability.OrganizationCapabilityScaleOut;
			if (shouldCapabilityExist)
			{
				if (!orgMailbox.PersistedCapabilities.Contains(item))
				{
					this.Logger.TraceDebug(this, "UpdateOrganizationScaleOutCapability - Adding capability ScaleOut to mailbox '{0}'", new object[]
					{
						orgMailbox.Name
					});
					orgMailbox.PersistedCapabilities.Add(item);
					orgMailbox.Session.Save(orgMailbox);
					return;
				}
			}
			else if (orgMailbox.PersistedCapabilities.Contains(item))
			{
				this.Logger.TraceDebug(this, "UpdateOrganizationScaleOutCapability - Removing capability ScaleOut from mailbox '{0}'", new object[]
				{
					orgMailbox.Name
				});
				orgMailbox.PersistedCapabilities.Remove(item);
				orgMailbox.Session.Save(orgMailbox);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00062D44 File Offset: 0x00060F44
		internal bool ShouldStampScaleOutCapability(int mailboxCount, int contactCount, int mailUserCount, int orgMailboxCount, int scaleOutThreshold)
		{
			this.Logger.TraceDebug(this, "ShouldStampScaleOutCapability - Mailbox count='{0}', Contact count='{1}', Mail user count='{2}', Org mailbox count='{3}', Scale-out threshold='{4}'", new object[]
			{
				mailboxCount,
				contactCount,
				mailUserCount,
				orgMailboxCount,
				scaleOutThreshold
			});
			bool flag = false;
			int num = mailboxCount + contactCount + mailUserCount;
			this.Logger.TraceDebug(this, "ShouldStampScaleOutCapability - Total recipient count='{0}'", new object[]
			{
				num
			});
			if (num > 0)
			{
				int num2 = (int)Math.Ceiling((double)num / (double)scaleOutThreshold);
				this.Logger.TraceDebug(this, "ShouldStampScaleOutCapability - Number of organization mailboxes needed='{0}'", new object[]
				{
					num2
				});
				flag = (orgMailboxCount < num2);
			}
			this.Logger.TraceDebug(this, "ShouldStampScaleOutCapability - stampCapability='{0}'", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00062E28 File Offset: 0x00061028
		internal void UpdateOrganizationMailboxCapabilities(ADUser orgMailbox)
		{
			this.Logger.TraceDebug(this, "UpdateOrganizationMailboxCapabilities - Mailbox='{0}'", new object[]
			{
				orgMailbox.Name
			});
			bool flag = false;
			foreach (Capability capability in this.CapabilitiesRequiringScaling)
			{
				if (!orgMailbox.PersistedCapabilities.Contains(capability))
				{
					this.Logger.TraceDebug(this, "UpdateOrganizationMailboxCapabilities - Adding capability {0}  to mailbox '{1}'", new object[]
					{
						capability,
						orgMailbox.Name
					});
					orgMailbox.PersistedCapabilities.Add(capability);
					flag = true;
				}
			}
			if (flag)
			{
				orgMailbox.Session.Save(orgMailbox);
			}
		}

		// Token: 0x04000A99 RID: 2713
		private static int scaleOutThreshold = 200000;

		// Token: 0x04000A9A RID: 2714
		private OrganizationCapability CapabilityRequiringScaling = OrganizationCapability.UMGrammar;

		// Token: 0x04000A9B RID: 2715
		private Capability[] CapabilitiesRequiringScaling = new Capability[]
		{
			Capability.OrganizationCapabilityUMGrammar,
			Capability.OrganizationCapabilityMailRouting,
			Capability.OrganizationCapabilityMessageTracking
		};
	}
}
