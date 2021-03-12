using System;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002C8 RID: 712
	[Cmdlet("initialize", "SystemAttendantPermissions", SupportsShouldProcess = true)]
	public sealed class InitializeSystemAttendantPermissions : SetupTaskBase
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0006FF04 File Offset: 0x0006E104
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x0006FF1B File Offset: 0x0006E11B
		[Parameter(Mandatory = false)]
		public string ServerName
		{
			get
			{
				return (string)base.Fields["ServerName"];
			}
			set
			{
				base.Fields["ServerName"] = value;
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0006FF30 File Offset: 0x0006E130
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADSystemAttendantMailbox adsystemAttendantMailbox = null;
			Server server = null;
			SecurityIdentifier sid = this.exs.Sid;
			SecurityIdentifier securityIdentifier = new SecurityIdentifier("SY");
			try
			{
				server = ((ITopologyConfigurationSession)this.configurationSession).FindLocalServer();
			}
			catch (LocalServerNotFoundException ex)
			{
				base.WriteError(new CouldNotFindExchangeServerDirectoryEntryException(ex.Fqdn), ErrorCategory.InvalidData, null);
			}
			if (server != null)
			{
				base.LogReadObject(server);
				this.recipientSession.DomainController = server.OriginatingServer;
				ADRecipient[] array = this.recipientSession.Find(server.Id.GetChildId("Microsoft System Attendant"), QueryScope.Base, null, null, 1);
				if (array.Length > 0)
				{
					adsystemAttendantMailbox = (array[0] as ADSystemAttendantMailbox);
				}
			}
			if (adsystemAttendantMailbox != null)
			{
				base.LogReadObject(adsystemAttendantMailbox);
				GenericAce[] aces = new GenericAce[]
				{
					new CommonAce(AceFlags.None, AceQualifier.AccessAllowed, 131073, securityIdentifier, false, null)
				};
				DirectoryCommon.SetAclOnAlternateProperty(adsystemAttendantMailbox, aces, ADSystemAttendantMailboxSchema.ExchangeSecurityDescriptor);
				if (base.ShouldProcess(adsystemAttendantMailbox.DistinguishedName, Strings.InfoProcessAction(securityIdentifier.ToString()), null))
				{
					this.recipientSession.Save(adsystemAttendantMailbox);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0007004C File Offset: 0x0006E24C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.exs = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.ExSWkGuid);
			if (this.exs == null)
			{
				base.ThrowTerminatingError(new ExSGroupNotFoundException(WellKnownGuid.ExSWkGuid), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.exs);
			TaskLogger.LogExit();
		}

		// Token: 0x04000AF3 RID: 2803
		private ADGroup exs;
	}
}
