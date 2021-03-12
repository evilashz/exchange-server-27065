using System;
using System.DirectoryServices;
using System.Management.Automation;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B7 RID: 695
	[Cmdlet("Add", "DelegatedGroupsToLocalAdminGroup")]
	public class AddDelegatedGroupsToLocalAdminGroup : SetupTaskBase
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00067451 File Offset: 0x00065651
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x00067468 File Offset: 0x00065668
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0006747B File Offset: 0x0006567B
		protected ADGroup ExchangeOrgAdminGroup
		{
			get
			{
				return this.eoa;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00067483 File Offset: 0x00065683
		protected Server ServerObject
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0006748C File Offset: 0x0006568C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.eoa = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EoaWkGuid);
			if (this.eoa == null)
			{
				base.ThrowTerminatingError(new ExOrgAdminSGroupNotFoundException(WellKnownGuid.EoaWkGuid), ErrorCategory.ObjectNotFound, null);
			}
			base.LogReadObject(this.eoa);
			this.ets = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EtsWkGuid);
			if (this.ets == null)
			{
				base.ThrowTerminatingError(new ExOrgAdminSGroupNotFoundException(WellKnownGuid.EtsWkGuid), ErrorCategory.ObjectNotFound, null);
			}
			base.LogReadObject(this.ets);
			TaskLogger.LogExit();
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0006751C File Offset: 0x0006571C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.ServerName != null)
			{
				this.server = ((ITopologyConfigurationSession)this.configurationSession).FindServerByName(this.ServerName);
			}
			else
			{
				try
				{
					this.server = ((ITopologyConfigurationSession)this.configurationSession).FindLocalServer();
				}
				catch (LocalServerNotFoundException)
				{
					this.server = null;
				}
			}
			if (this.server == null)
			{
				base.WriteError(new DirectoryObjectNotFoundException(string.IsNullOrEmpty(this.ServerName) ? NativeHelpers.GetLocalComputerFqdn(false) : this.ServerName), ErrorCategory.ObjectNotFound, null);
			}
			else
			{
				base.LogReadObject(this.ServerObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000675CC File Offset: 0x000657CC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			DirectoryEntry localAdminGroup = AddDelegatedGroupsToLocalAdminGroup.GetLocalAdminGroup(Environment.MachineName);
			if (localAdminGroup != null)
			{
				base.WriteVerbose(Strings.AboutToAddLocalOrgUSGToLocalAdminGroup);
				AddDelegatedGroupsToLocalAdminGroup.AddToLocalAdminGroup(this.eoa.Sid, localAdminGroup, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null);
				base.WriteVerbose(Strings.AboutToAddLocalEtsUSGToLocalAdminGroup);
				AddDelegatedGroupsToLocalAdminGroup.AddToLocalAdminGroup(this.ets.Sid, localAdminGroup, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null);
			}
			else
			{
				base.WriteError(new CannotFindLocalAdminGroupException(Environment.MachineName), ErrorCategory.ObjectNotFound, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00067658 File Offset: 0x00065858
		internal static DirectoryEntry GetLocalAdminGroup(string serverName)
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			NTAccount ntaccount = (NTAccount)securityIdentifier.Translate(typeof(NTAccount));
			string[] array = ntaccount.Value.Split(new char[]
			{
				'\\'
			}, 2);
			string name = array[1];
			DirectoryEntry result;
			using (DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://" + serverName + ",computer"))
			{
				DirectoryEntry directoryEntry2 = directoryEntry.Children.Find(name, "group");
				result = directoryEntry2;
			}
			return result;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000676F4 File Offset: 0x000658F4
		internal static void AddToLocalAdminGroup(SecurityIdentifier sid, DirectoryEntry localAdminGroup, Task.TaskVerboseLoggingDelegate logVerbose, string user)
		{
			string user2 = string.IsNullOrEmpty(user) ? sid.ToString() : user;
			try
			{
				localAdminGroup.Invoke("Add", new object[]
				{
					"WinNT://" + sid.ToString()
				});
				localAdminGroup.CommitChanges();
			}
			catch (TargetInvocationException ex)
			{
				logVerbose(Strings.FailToAddServerAdminToLocalAdminGroup(user2, ex.ToString()));
			}
		}

		// Token: 0x04000AA2 RID: 2722
		private ADGroup eoa;

		// Token: 0x04000AA3 RID: 2723
		private ADGroup ets;

		// Token: 0x04000AA4 RID: 2724
		private Server server;
	}
}
