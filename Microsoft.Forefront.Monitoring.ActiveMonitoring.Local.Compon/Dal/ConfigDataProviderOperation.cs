using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200005D RID: 93
	public abstract class ConfigDataProviderOperation : DalProbeOperation
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000F30A File Offset: 0x0000D50A
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000F312 File Offset: 0x0000D512
		[XmlAttribute]
		public DalType Database { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000F31B File Offset: 0x0000D51B
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000F323 File Offset: 0x0000D523
		[XmlAttribute]
		public string DataType { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000F32C File Offset: 0x0000D52C
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000F334 File Offset: 0x0000D534
		[XmlAttribute]
		public string OrganizationTag { get; set; }

		// Token: 0x06000268 RID: 616 RVA: 0x0000F340 File Offset: 0x0000D540
		public sealed override void Execute(IDictionary<string, object> variables)
		{
			GlobalConfigSession globalConfigSession = new GlobalConfigSession();
			IEnumerable<ProbeOrganizationInfo> probeOrganizations = globalConfigSession.GetProbeOrganizations(this.OrganizationTag);
			foreach (ProbeOrganizationInfo probeOrganizationInfo in probeOrganizations)
			{
				IConfigDataProvider configDataProvider = this.CreateDataProvider(probeOrganizationInfo);
				this.ExecuteConfigDataProviderOperation(configDataProvider, variables);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		protected static IEnumerable<PropertyDefinition> GetPropertyDefinitions(IConfigurable iconfigObj)
		{
			ConfigurableObject configurableObject = iconfigObj as ConfigurableObject;
			if (configurableObject != null)
			{
				return configurableObject.ObjectSchema.AllProperties;
			}
			ConfigurablePropertyBag configurablePropertyBag = (ConfigurablePropertyBag)iconfigObj;
			return configurablePropertyBag.GetPropertyDefinitions(false);
		}

		// Token: 0x0600026A RID: 618
		protected abstract void ExecuteConfigDataProviderOperation(IConfigDataProvider configDataProvider, IDictionary<string, object> variables);

		// Token: 0x0600026B RID: 619 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		private IConfigDataProvider CreateDataProvider(ProbeOrganizationInfo probeOrganizationInfo)
		{
			switch (this.Database)
			{
			case DalType.Global:
				throw new NotSupportedException(string.Format("DalType {0} is not supported.", this.Database));
			case DalType.Tenant:
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromTenantCUName(this.OrganizationTag);
				return DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, probeOrganizationInfo.ProbeOrganizationId.ObjectGuid, 115, "CreateDataProvider", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DAL\\Probes\\ConfigDataProviderOperation.cs");
			}
			case DalType.Recipient:
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(probeOrganizationInfo.ProbeOrganizationId.ObjectGuid);
				return DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 118, "CreateDataProvider", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DAL\\Probes\\ConfigDataProviderOperation.cs");
			}
			default:
				return ConfigDataProviderFactory.Default.Create((DatabaseType)this.Database);
			}
		}
	}
}
