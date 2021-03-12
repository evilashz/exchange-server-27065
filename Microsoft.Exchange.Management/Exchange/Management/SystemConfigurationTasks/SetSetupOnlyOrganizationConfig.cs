using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AF0 RID: 2800
	[Cmdlet("Set", "SetupOnlyOrganizationConfig", DefaultParameterSetName = "Identity")]
	public sealed class SetSetupOnlyOrganizationConfig : BaseOrganization
	{
		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x0600636F RID: 25455 RVA: 0x001A027C File Offset: 0x0019E47C
		// (set) Token: 0x06006370 RID: 25456 RVA: 0x001A0293 File Offset: 0x0019E493
		[Parameter(Mandatory = false)]
		public int ObjectVersion
		{
			get
			{
				return (int)base.Fields[OrganizationSchema.ObjectVersion];
			}
			set
			{
				base.Fields[OrganizationSchema.ObjectVersion] = value;
			}
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x06006371 RID: 25457 RVA: 0x001A02AB File Offset: 0x0019E4AB
		// (set) Token: 0x06006372 RID: 25458 RVA: 0x001A02C2 File Offset: 0x0019E4C2
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)base.Fields[ADObjectSchema.Name];
			}
			set
			{
				base.Fields[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x001A02D8 File Offset: 0x0019E4D8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable result;
			try
			{
				Organization organization = (Organization)base.PrepareDataObject();
				if (base.Fields.IsModified(OrganizationSchema.ObjectVersion))
				{
					organization.ObjectVersion = this.ObjectVersion;
				}
				if (base.Fields.IsModified(ADObjectSchema.Name))
				{
					organization.Name = this.Name;
				}
				organization.BuildNumber = FileVersionInfo.GetVersionInfo(Path.Combine(ExchangeSetupContext.AssemblyPath, "ExSetup.exe")).ProductVersion;
				result = organization;
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}
	}
}
