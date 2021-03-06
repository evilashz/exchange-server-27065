using System;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B8 RID: 2232
	[Cmdlet("New", "StampGroup", SupportsShouldProcess = true)]
	public sealed class NewStampGroup : NewSystemConfigurationObjectTask<StampGroup>
	{
		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x00148178 File Offset: 0x00146378
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewDatabaseAvailabilityGroup(base.Name.ToString());
			}
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x00148192 File Offset: 0x00146392
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 63, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\NewStampGroup.cs");
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x001481BC File Offset: 0x001463BC
		private void ValidateStampGroupNameIsValidComputerName(string stampGroupName)
		{
			bool flag = true;
			Regex regex = new Regex("^[-A-Za-z0-9]+$", RegexOptions.CultureInvariant);
			if (stampGroupName.Length > 15)
			{
				flag = false;
			}
			else
			{
				Match match = regex.Match(stampGroupName);
				if (!match.Success)
				{
					flag = false;
				}
			}
			if (!flag)
			{
				this.m_output.WriteError(new DagTaskDagNameMustBeComputerNameExceptionM1(stampGroupName), ErrorCategory.InvalidArgument, null);
			}
			ITopologyConfigurationSession configSession = (ITopologyConfigurationSession)base.DataSession;
			bool flag3;
			bool flag2 = DagTaskHelper.DoesComputerAccountExist(configSession, stampGroupName, out flag3);
			if (!flag2)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.GetHashCode(), "The computer account {0} does not exist.", stampGroupName);
				return;
			}
			if (flag3)
			{
				this.m_output.WriteError(new DagTaskComputerAccountExistsAndIsEnabledException(stampGroupName), ErrorCategory.InvalidArgument, null);
				return;
			}
			ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.GetHashCode(), "The computer account {0} exists, but is disabled.", stampGroupName);
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0014827C File Offset: 0x0014647C
		private void LogCommandLineParameters()
		{
			this.m_output.AppendLogMessage("commandline: {0}", new object[]
			{
				base.MyInvocation.Line
			});
			string[] array = new string[]
			{
				"Name",
				"WhatIf"
			};
			foreach (string text in array)
			{
				this.m_output.AppendLogMessage("Option '{0}' = '{1}'.", new object[]
				{
					text,
					base.Fields[text]
				});
			}
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x00148314 File Offset: 0x00146514
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.m_output = new HaTaskOutputHelper("new-stampgroup", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_output.CreateTempLogFile();
			this.m_output.AppendLogMessage("new-stampgroup started", new object[0]);
			this.LogCommandLineParameters();
			this.m_stampGroupName = base.Name;
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, StampGroupSchema.Name, this.m_stampGroupName);
			StampGroup[] array = this.ConfigurationSession.Find<StampGroup>(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				base.WriteError(new ADObjectAlreadyExistsException(Strings.NewDagErrorDuplicateName(this.m_stampGroupName)), ErrorCategory.InvalidArgument, this.m_stampGroupName);
			}
			this.ValidateStampGroupNameIsValidComputerName(this.m_stampGroupName);
			base.InternalValidate();
			this.m_output.WriteVerbose(Strings.NewDagPassedChecks);
			TaskLogger.LogExit();
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x00148410 File Offset: 0x00146610
		private void InitializeDagAdObject()
		{
			foreach (ADObjectId identity in this.m_newStampGroup.Servers)
			{
				Server dagMemberServer = (Server)base.DataSession.Read<Server>(identity);
				DagTaskHelper.RevertDagServersDatabasesToStandalone(this.ConfigurationSession, this.m_output, dagMemberServer);
			}
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x00148488 File Offset: 0x00146688
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.InitializeDagAdObject();
			base.InternalProcessRecord();
			this.m_output.WriteVerbose(Strings.NewDagCompletedSuccessfully);
			this.m_output.CloseTempLogFile();
			TaskLogger.LogExit();
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x001484BC File Offset: 0x001466BC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			StampGroup stampGroup = new StampGroup();
			stampGroup.SetId(((ITopologyConfigurationSession)this.ConfigurationSession).GetStampGroupContainerId().GetChildId(base.Name));
			stampGroup.Name = this.m_stampGroupName;
			this.m_newStampGroup = stampGroup;
			TaskLogger.LogExit();
			return stampGroup;
		}

		// Token: 0x04002EE2 RID: 12002
		private StampGroup m_newStampGroup;

		// Token: 0x04002EE3 RID: 12003
		private string m_stampGroupName;

		// Token: 0x04002EE4 RID: 12004
		private HaTaskOutputHelper m_output;
	}
}
