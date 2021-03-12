using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200006C RID: 108
	public abstract class MapiObjectActionTask<TIdentity, TDataObject> : ObjectActionTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : MapiObject, new()
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000F296 File Offset: 0x0000D496
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000F2AD File Offset: 0x0000D4AD
		[Parameter]
		public ServerIdParameter Server
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
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

		// Token: 0x06000449 RID: 1097 RVA: 0x0000F2D1 File Offset: 0x0000D4D1
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(MapiPermanentException).IsInstanceOfType(exception) || typeof(MapiRetryableException).IsInstanceOfType(exception);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000F307 File Offset: 0x0000D507
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.configurationSession;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000F310 File Offset: 0x0000D510
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.DataObject != null)
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Dispose();
				this.DataObject = default(TDataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000F360 File Offset: 0x0000D560
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (!MapiTaskHelper.IsDatacenter)
			{
				this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 120, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\MapiObjectActionTask.cs");
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.DataObject != null)
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Dispose();
				this.DataObject = default(TDataObject);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000109 RID: 265
		private IConfigurationSession configurationSession;
	}
}
