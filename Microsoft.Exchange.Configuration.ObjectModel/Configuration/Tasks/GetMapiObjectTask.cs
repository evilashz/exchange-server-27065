using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000061 RID: 97
	public abstract class GetMapiObjectTask<TIdentity, TDataObject> : GetObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000EFDD File Offset: 0x0000D1DD
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		[Parameter(ValueFromPipeline = true)]
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000F007 File Offset: 0x0000D207
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000F00F File Offset: 0x0000D20F
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

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F018 File Offset: 0x0000D218
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(MapiPermanentException).IsInstanceOfType(exception) || typeof(MapiRetryableException).IsInstanceOfType(exception);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000F04E File Offset: 0x0000D24E
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.configurationSession;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F058 File Offset: 0x0000D258
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 104, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetMapiObjectTask.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x040000FD RID: 253
		private IConfigurationSession configurationSession;
	}
}
