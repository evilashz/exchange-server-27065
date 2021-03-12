using System;
using System.Management.Automation;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200000E RID: 14
	[Cmdlet("Get", "ExchangeDiagnosticInfo")]
	public sealed class GetExchangeDiagnosticInfo : GetTaskBase<ExchangeDiagnosticInfoResult>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004212 File Offset: 0x00002412
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00004229 File Offset: 0x00002429
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000423C File Offset: 0x0000243C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004253 File Offset: 0x00002453
		[Parameter(Mandatory = false)]
		[ValidateLength(0, 256)]
		public string Process
		{
			get
			{
				return (string)base.Fields["Process"];
			}
			set
			{
				base.Fields["Process"] = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004266 File Offset: 0x00002466
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000427D File Offset: 0x0000247D
		[Parameter(Mandatory = false)]
		[ValidateLength(0, 256)]
		public string Component
		{
			get
			{
				return (string)base.Fields["Component"];
			}
			set
			{
				base.Fields["Component"] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004290 File Offset: 0x00002490
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000042A7 File Offset: 0x000024A7
		[Parameter(Mandatory = false)]
		[ValidateLength(0, 1048576)]
		public string Argument
		{
			get
			{
				return (string)base.Fields["Argument"];
			}
			set
			{
				base.Fields["Argument"] = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000042BA File Offset: 0x000024BA
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000042E0 File Offset: 0x000024E0
		[Parameter(Mandatory = false)]
		public SwitchParameter Unlimited
		{
			get
			{
				return (SwitchParameter)(base.Fields["Unlimited"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Unlimited"] = value;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000042F8 File Offset: 0x000024F8
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 112, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Support\\DiagnosticTasks\\GetExchangeDiagnosticInfo.cs");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004318 File Offset: 0x00002518
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.Server == null)
			{
				this.Server = new ServerIdParameter();
			}
			Server server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			if (string.IsNullOrEmpty(server.Fqdn))
			{
				base.WriteError(new LocalizedException(Strings.ErrorMissingServerFqdn(this.Server.ToString())), ErrorCategory.InvalidOperation, this.Server);
				return;
			}
			this.serverFqdn = server.Fqdn;
			this.serverVersion = server.AdminDisplayVersion;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000043D8 File Offset: 0x000025D8
		protected override void InternalProcessRecord()
		{
			string userIdentity = (base.ExchangeRunspaceConfig == null) ? string.Empty : base.ExchangeRunspaceConfig.IdentityName;
			bool componentRestrictedData = GetExchangeDiagnosticInfo.CheckDataRedactionEnabled(this.serverVersion) && !base.NeedSuppressingPiiData;
			string xml = ProcessAccessManager.ClientRunProcessCommand(this.serverFqdn, this.Process, this.Component, this.Argument, componentRestrictedData, this.Unlimited, userIdentity);
			string result = GetExchangeDiagnosticInfo.ReformatXml(xml);
			this.WriteResult(new ExchangeDiagnosticInfoResult(result));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000445C File Offset: 0x0000265C
		private static string ReformatXml(string xml)
		{
			string result;
			try
			{
				XDocument xdocument = XDocument.Parse(xml);
				result = xdocument.ToString(SaveOptions.None);
			}
			catch (XmlException)
			{
				result = xml;
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004490 File Offset: 0x00002690
		public static bool CheckDataRedactionEnabled(ServerVersion target)
		{
			ServerVersion b = new ServerVersion(15, 0, 586, 0);
			return !(target == null) && ServerVersion.Compare(target, b) >= 0;
		}

		// Token: 0x0400004C RID: 76
		private string serverFqdn;

		// Token: 0x0400004D RID: 77
		private ServerVersion serverVersion;
	}
}
