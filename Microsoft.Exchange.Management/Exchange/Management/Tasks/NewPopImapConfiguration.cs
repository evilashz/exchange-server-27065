using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200062B RID: 1579
	public abstract class NewPopImapConfiguration<TDataObject> : NewFixedNameSystemConfigurationObjectTask<TDataObject> where TDataObject : PopImapAdConfiguration, new()
	{
		// Token: 0x060037C9 RID: 14281 RVA: 0x000E79E4 File Offset: 0x000E5BE4
		public NewPopImapConfiguration()
		{
			TDataObject dataObject = this.DataObject;
			dataObject.Name = "1";
			TDataObject dataObject2 = this.DataObject;
			dataObject2.LogPerFileSizeQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.Zero);
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000E7A2E File Offset: 0x000E5C2E
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000E7A45 File Offset: 0x000E5C45
		[Parameter(Mandatory = true)]
		public string ExchangePath
		{
			get
			{
				return (string)base.Fields["ExchangePath"];
			}
			set
			{
				base.Fields["ExchangePath"] = value;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060037CC RID: 14284
		protected abstract string ProtocolName { get; }

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x000E7A58 File Offset: 0x000E5C58
		protected override ObjectId RootId
		{
			get
			{
				ServerIdParameter serverIdParameter = ServerIdParameter.Parse(Environment.MachineName);
				Server server = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				Server server2 = server;
				TDataObject dataObject = this.DataObject;
				return PopImapAdConfiguration.GetRootId(server2, dataObject.ProtocolName);
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000E7AC4 File Offset: 0x000E5CC4
		protected override IConfigurable PrepareDataObject()
		{
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			ADObjectId adobjectId = this.RootId as ADObjectId;
			tdataObject.SetId(adobjectId.GetChildId(tdataObject.Name));
			tdataObject.LogFileLocation = Path.Combine(this.GetLoggingPath(), this.ProtocolName);
			return tdataObject;
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000E7B30 File Offset: 0x000E5D30
		protected override void InternalProcessRecord()
		{
			IConfigDataProvider dataSession = base.DataSession;
			TDataObject dataObject = this.DataObject;
			if (dataSession.Read<TDataObject>(dataObject.Id) == null)
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000E7B64 File Offset: 0x000E5D64
		internal TDataObject DefaultConfiguration
		{
			get
			{
				return this.DataObject;
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000E7B6C File Offset: 0x000E5D6C
		protected string GetLoggingPath()
		{
			return Path.Combine(this.ExchangePath, "Logging");
		}

		// Token: 0x040025B6 RID: 9654
		private const string LoggingSubDirectory = "Logging";
	}
}
