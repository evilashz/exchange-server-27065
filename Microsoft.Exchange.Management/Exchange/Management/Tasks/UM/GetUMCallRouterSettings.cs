using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D1E RID: 3358
	[Cmdlet("Get", "UMCallRouterSettings")]
	public sealed class GetUMCallRouterSettings : GetSingletonSystemConfigurationObjectTask<SIPFEServerConfiguration>
	{
		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x060080E9 RID: 33001 RVA: 0x0020F9D5 File Offset: 0x0020DBD5
		// (set) Token: 0x060080EA RID: 33002 RVA: 0x0020F9EC File Offset: 0x0020DBEC
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x060080EB RID: 33003 RVA: 0x0020FA00 File Offset: 0x0020DC00
		protected override ObjectId RootId
		{
			get
			{
				ServerIdParameter serverIdParameter = this.Server ?? ServerIdParameter.Parse(Environment.MachineName);
				Server server = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				return SIPFEServerConfiguration.GetRootId(server);
			}
		}
	}
}
