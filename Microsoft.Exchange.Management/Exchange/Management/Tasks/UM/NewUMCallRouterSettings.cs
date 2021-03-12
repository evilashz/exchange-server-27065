using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D32 RID: 3378
	[Cmdlet("New", "UMCallRouterSettings")]
	public sealed class NewUMCallRouterSettings : NewFixedNameSystemConfigurationObjectTask<SIPFEServerConfiguration>
	{
		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x0600816E RID: 33134 RVA: 0x0021146C File Offset: 0x0020F66C
		protected override ObjectId RootId
		{
			get
			{
				ServerIdParameter serverIdParameter = ServerIdParameter.Parse(Environment.MachineName);
				this.localServer = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				return SIPFEServerConfiguration.GetRootId(this.localServer);
			}
		}

		// Token: 0x0600816F RID: 33135 RVA: 0x002114D0 File Offset: 0x0020F6D0
		protected override IConfigurable PrepareDataObject()
		{
			SIPFEServerConfiguration sipfeserverConfiguration = (SIPFEServerConfiguration)base.PrepareDataObject();
			ADObjectId adobjectId = this.RootId as ADObjectId;
			sipfeserverConfiguration.SetId(adobjectId.GetChildId(sipfeserverConfiguration.Name));
			sipfeserverConfiguration.VersionNumber = this.localServer.VersionNumber;
			sipfeserverConfiguration.NetworkAddress = this.localServer.NetworkAddress;
			sipfeserverConfiguration.CurrentServerRole = this.localServer.CurrentServerRole;
			return sipfeserverConfiguration;
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x0021153B File Offset: 0x0020F73B
		protected override void InternalProcessRecord()
		{
			if (base.DataSession.Read<SIPFEServerConfiguration>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x06008171 RID: 33137 RVA: 0x0021155B File Offset: 0x0020F75B
		internal SIPFEServerConfiguration DefaultConfiguration
		{
			get
			{
				return this.DataObject;
			}
		}

		// Token: 0x04003F26 RID: 16166
		private Server localServer;
	}
}
