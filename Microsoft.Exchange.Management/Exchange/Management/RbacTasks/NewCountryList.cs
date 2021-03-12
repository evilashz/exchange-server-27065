using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200065C RID: 1628
	[Cmdlet("New", "CountryList", SupportsShouldProcess = true)]
	public sealed class NewCountryList : NewFixedNameSystemConfigurationObjectTask<CountryList>
	{
		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000EEAE5 File Offset: 0x000ECCE5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewCountryList(this.Name);
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000EEAF2 File Offset: 0x000ECCF2
		// (set) Token: 0x060038F9 RID: 14585 RVA: 0x000EEAFF File Offset: 0x000ECCFF
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000EEB0D File Offset: 0x000ECD0D
		// (set) Token: 0x060038FB RID: 14587 RVA: 0x000EEB1A File Offset: 0x000ECD1A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CountryInfo> Countries
		{
			get
			{
				return this.DataObject.Countries;
			}
			set
			{
				this.DataObject.Countries = value;
			}
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000EEB28 File Offset: 0x000ECD28
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (CountryList)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			this.DataObject.SetId(this.ConfigurationSession, CountryList.RdnContainer, this.DataObject.Name);
			TaskLogger.LogExit();
			return this.DataObject;
		}
	}
}
