using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EventLogTasks
{
	// Token: 0x02000324 RID: 804
	[Cmdlet("Get", "eventloglevel", DefaultParameterSetName = "Identity")]
	public class GetEventLogLevel : GetObjectWithIdentityTaskBase<ECIdParameter, EventCategoryObject>
	{
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0007A1DD File Offset: 0x000783DD
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x0007A1E5 File Offset: 0x000783E5
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0007A1EE File Offset: 0x000783EE
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0007A205 File Offset: 0x00078405
		[Parameter(Mandatory = true, ParameterSetName = "Server")]
		public virtual ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0007A218 File Offset: 0x00078418
		protected override IConfigDataProvider CreateSession()
		{
			return new EventCategorySession((this.Server != null) ? this.Server.ToString() : null);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0007A238 File Offset: 0x00078438
		protected override void InternalProcessRecord()
		{
			if (base.ParameterSetName != "Identity")
			{
				ECIdParameter ecidParameter = null;
				TaskLogger.LogEnter(new object[]
				{
					this.Server
				});
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.FindingServer(this.Server.ToString()));
				}
				IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 89, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\EventLog\\GetEventLogLevel.cs");
				IEnumerable<Server> objects = this.Server.GetObjects<Server>(null, session);
				IEnumerator<Server> enumerator = objects.GetEnumerator();
				Server server = null;
				if (enumerator.MoveNext())
				{
					server = enumerator.Current;
					if (enumerator.MoveNext())
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorServerNotUnique(this.Server.ToString())), ErrorCategory.InvalidData, this.Server);
					}
				}
				else
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorServerNotFound(this.Server.ToString())), ErrorCategory.InvalidData, this.Server);
				}
				base.WriteVerbose(Strings.FindingEventSources);
				try
				{
					try
					{
						ecidParameter = ECIdParameter.Parse(server.AdminDisplayName + "\\*");
						IEnumerable<EventCategoryObject> dataObjects = base.GetDataObjects(ecidParameter);
						this.WriteResult<EventCategoryObject>(dataObjects);
					}
					catch (SecurityException exception)
					{
						base.WriteError(exception, ErrorCategory.InvalidData, ecidParameter);
					}
					catch (IOException exception2)
					{
						base.WriteError(exception2, ErrorCategory.InvalidData, ecidParameter);
					}
					catch (DataSourceOperationException exception3)
					{
						base.WriteError(exception3, ErrorCategory.InvalidData, this.Identity);
					}
					return;
				}
				finally
				{
					TaskLogger.LogExit();
				}
			}
			try
			{
				base.InternalProcessRecord();
			}
			catch (SecurityException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidData, this.Identity);
			}
			catch (IOException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidData, this.Identity);
			}
			catch (DataSourceOperationException exception6)
			{
				base.WriteError(exception6, ErrorCategory.InvalidData, this.Identity);
			}
		}
	}
}
