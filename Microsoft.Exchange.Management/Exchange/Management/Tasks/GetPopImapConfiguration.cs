using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000629 RID: 1577
	public abstract class GetPopImapConfiguration<TDataObject> : GetSingletonSystemConfigurationObjectTask<TDataObject> where TDataObject : PopImapAdConfiguration, new()
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x000E7328 File Offset: 0x000E5528
		public GetPopImapConfiguration()
		{
			TDataObject tdataObject = Activator.CreateInstance<TDataObject>();
			this.protocolName = tdataObject.ProtocolName;
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000E7354 File Offset: 0x000E5554
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x000E736B File Offset: 0x000E556B
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000E7380 File Offset: 0x000E5580
		protected override ObjectId RootId
		{
			get
			{
				ServerIdParameter serverIdParameter = this.Server ?? ServerIdParameter.Parse(Environment.MachineName);
				Server server = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				return PopImapAdConfiguration.GetRootId(server, this.protocolName);
			}
		}

		// Token: 0x040025B1 RID: 9649
		private readonly string protocolName;
	}
}
