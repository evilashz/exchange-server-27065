using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000057 RID: 87
	[Cmdlet("Remove", "PushNotificationApp", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePushNotificationApp : RemoveSystemConfigurationObjectTask<PushNotificationAppIdParameter, PushNotificationApp>
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000E691 File Offset: 0x0000C891
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveApp(this.Identity.ToString());
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000E6A3 File Offset: 0x0000C8A3
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 41, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\PushNotifications\\RemovePushNotificationApp.cs");
		}
	}
}
