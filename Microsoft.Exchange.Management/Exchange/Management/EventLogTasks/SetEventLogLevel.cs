using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EventLogTasks
{
	// Token: 0x02000325 RID: 805
	[Cmdlet("Set", "eventloglevel", SupportsShouldProcess = true)]
	public class SetEventLogLevel : SetTaskBase<EventCategoryObject>
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0007A440 File Offset: 0x00078640
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetEventLogLevel(this.Identity.ToString(), this.Level.ToString());
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0007A462 File Offset: 0x00078662
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0007A479 File Offset: 0x00078679
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public ECIdParameter Identity
		{
			get
			{
				return (ECIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0007A48C File Offset: 0x0007868C
		// (set) Token: 0x06001B7B RID: 7035 RVA: 0x0007A4A3 File Offset: 0x000786A3
		[Parameter(Mandatory = true)]
		public ExEventLog.EventLevel Level
		{
			get
			{
				return (ExEventLog.EventLevel)base.Fields["Level"];
			}
			set
			{
				base.Fields["Level"] = value;
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0007A4BC File Offset: 0x000786BC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable configurable = null;
			try
			{
				configurable = base.GetDataObject(this.Identity);
				this.StampChangesOn(configurable);
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, this.Identity);
			}
			catch (IOException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidData, this.Identity);
			}
			catch (DataSourceOperationException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidData, this.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return configurable;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0007A558 File Offset: 0x00078758
		protected void StampChangesOn(IConfigurable dataObject)
		{
			EventCategoryObject eventCategoryObject = dataObject as EventCategoryObject;
			if (eventCategoryObject == null)
			{
				base.WriteError(new InvalidEventCategoryInputException(), ErrorCategory.InvalidData, this.Identity);
				return;
			}
			eventCategoryObject.EventLevel = this.Level;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0007A58E File Offset: 0x0007878E
		protected override IConfigDataProvider CreateSession()
		{
			return new EventCategorySession((this.Identity != null) ? this.Identity.ToString() : null);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0007A5AC File Offset: 0x000787AC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.Identity.IsUnique())
			{
				base.WriteError(new NonUniqueEventCategoryInputException(), ErrorCategory.InvalidData, this.Identity);
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 148, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\EventLog\\SetEventLogLevel.cs");
			EventCategoryIdentity eventCategoryIdentity = null;
			if (this.Identity.ToString() != null)
			{
				eventCategoryIdentity = EventCategoryIdentity.Parse(this.Identity.ToString());
			}
			Server obj;
			if (eventCategoryIdentity == null || string.IsNullOrEmpty(eventCategoryIdentity.Server))
			{
				obj = topologyConfigurationSession.FindLocalServer();
			}
			else
			{
				ServerIdParameter serverIdParameter = ServerIdParameter.Parse(eventCategoryIdentity.Server);
				obj = (Server)base.GetDataObject<Server>(serverIdParameter, topologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())));
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			MapiTaskHelper.VerifyIsWithinConfigWriteScope(sessionSettings, obj, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
		}
	}
}
