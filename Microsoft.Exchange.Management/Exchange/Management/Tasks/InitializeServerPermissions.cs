using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
	// Token: 0x020002C7 RID: 711
	[Cmdlet("initialize", "ServerPermissions", SupportsShouldProcess = true)]
	public sealed class InitializeServerPermissions : SetupTaskBase
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0006FBD8 File Offset: 0x0006DDD8
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0006FBEF File Offset: 0x0006DDEF
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

		// Token: 0x0600190B RID: 6411 RVA: 0x0006FC3C File Offset: 0x0006DE3C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			List<Server> list = new List<Server>();
			List<SecurityIdentifier> list2 = new List<SecurityIdentifier>();
			Server server = null;
			try
			{
				server = ((ITopologyConfigurationSession)this.configurationSession).FindLocalServer();
			}
			catch (LocalServerNotFoundException ex)
			{
				base.WriteError(new CouldNotFindExchangeServerDirectoryEntryException(ex.Fqdn), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(server);
			list.Add(server);
			ADComputer adcomputer = ((ITopologyConfigurationSession)this.domainConfigurationSession).FindLocalComputer();
			if (adcomputer == null)
			{
				base.WriteError(new CouldNotFindLocalhostDirectoryEntryException(), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(adcomputer);
			list2.Add(adcomputer.Sid);
			if (list.Count > 0)
			{
				List<ActiveDirectoryAccessRule> list3 = list2.ConvertAll<ActiveDirectoryAccessRule>((SecurityIdentifier machineSid) => new ActiveDirectoryAccessRule(machineSid, ActiveDirectoryRights.GenericRead, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All));
				using (IEnumerator<string> enumerator = DirectoryCommon.ServerWriteAttrs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string attr = enumerator.Current;
						list3.AddRange(list2.ConvertAll<ActiveDirectoryAccessRule>((SecurityIdentifier machineSid) => new ActiveDirectoryAccessRule(machineSid, ActiveDirectoryRights.WriteProperty, AccessControlType.Allow, DirectoryCommon.GetSchemaPropertyGuid(this.configurationSession, attr), ActiveDirectorySecurityInheritance.None)));
					}
				}
				foreach (Server obj in list)
				{
					DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, obj, list3.ToArray());
				}
				SecurityIdentifier sid = this.exs.Sid;
				List<ActiveDirectoryAccessRule> list4 = new List<ActiveDirectoryAccessRule>();
				list4.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, WellKnownGuid.StoreTransportAccessExtendedRightGuid, ActiveDirectorySecurityInheritance.All));
				list4.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, WellKnownGuid.StoreConstrainedDelegationExtendedRightGuid, ActiveDirectorySecurityInheritance.All));
				list4.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, WellKnownGuid.StoreReadAccessExtendedRightGuid, ActiveDirectorySecurityInheritance.All));
				list4.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, WellKnownGuid.StoreReadWriteAccessExtendedRightGuid, ActiveDirectorySecurityInheritance.All));
				foreach (Server obj2 in list)
				{
					DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, obj2, list4.ToArray());
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0006FEA8 File Offset: 0x0006E0A8
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

		// Token: 0x04000AF1 RID: 2801
		private ADGroup exs;
	}
}
