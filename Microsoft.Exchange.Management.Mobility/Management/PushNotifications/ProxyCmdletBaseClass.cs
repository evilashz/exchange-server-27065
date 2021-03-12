using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000048 RID: 72
	public class ProxyCmdletBaseClass : SetSystemConfigurationObjectTask<PushNotificationAppIdParameter, PushNotificationApp>
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				return configurationSession.GetOrgContainerId().GetDescendantId(PushNotificationApp.RdnContainer);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000C301 File Offset: 0x0000A501
		public override PushNotificationAppIdParameter Identity
		{
			get
			{
				return ProxyCmdletBaseClass.ProxyIdentity;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000C308 File Offset: 0x0000A508
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000C310 File Offset: 0x0000A510
		private protected string CurrentProxyStatus { protected get; private set; }

		// Token: 0x060002B0 RID: 688 RVA: 0x0000C31C File Offset: 0x0000A51C
		protected override IConfigurable PrepareDataObject()
		{
			PushNotificationApp pushNotificationApp = (PushNotificationApp)base.PrepareDataObject();
			this.CurrentProxyStatus = ((pushNotificationApp != null && pushNotificationApp.Enabled != null) ? pushNotificationApp.Enabled.Value.ToString() : false.ToString());
			return pushNotificationApp;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C371 File Offset: 0x0000A571
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			this.WriteResult(this.DataObject);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C388 File Offset: 0x0000A588
		protected void WriteResult(IConfigurable dataObject)
		{
			PushNotificationApp pushNotificationApp = dataObject as PushNotificationApp;
			base.WriteObject(new PushNotificationProxyPresentationObject(pushNotificationApp));
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 103, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\PushNotifications\\ProxyCmdletBaseClass.cs");
		}

		// Token: 0x040000BD RID: 189
		private static readonly PushNotificationAppIdParameter ProxyIdentity = new PushNotificationAppIdParameter(PushNotificationCannedApp.OnPremProxy.Name);
	}
}
