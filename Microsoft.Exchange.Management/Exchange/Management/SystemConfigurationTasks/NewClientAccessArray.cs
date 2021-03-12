using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200099F RID: 2463
	[Cmdlet("New", "ClientAccessArray", SupportsShouldProcess = true)]
	public sealed class NewClientAccessArray : NewFixedNameSystemConfigurationObjectTask<ClientAccessArray>
	{
		// Token: 0x06005811 RID: 22545 RVA: 0x0016FC1F File Offset: 0x0016DE1F
		public NewClientAccessArray()
		{
			this.arrayTaskCommon = new ClientAccessArrayTaskHelper(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06005812 RID: 22546 RVA: 0x0016FC4A File Offset: 0x0016DE4A
		// (set) Token: 0x06005813 RID: 22547 RVA: 0x0016FC57 File Offset: 0x0016DE57
		[Parameter(Position = 0, Mandatory = true)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x06005814 RID: 22548 RVA: 0x0016FC65 File Offset: 0x0016DE65
		// (set) Token: 0x06005815 RID: 22549 RVA: 0x0016FC85 File Offset: 0x0016DE85
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public string ArrayDefinition
		{
			get
			{
				return (string)(base.Fields[ClientAccessArraySchema.ArrayDefinition] ?? string.Empty);
			}
			set
			{
				base.Fields[ClientAccessArraySchema.ArrayDefinition] = value;
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06005816 RID: 22550 RVA: 0x0016FC98 File Offset: 0x0016DE98
		// (set) Token: 0x06005817 RID: 22551 RVA: 0x0016FCB9 File Offset: 0x0016DEB9
		[ValidateRange(0, 2147483646)]
		[Parameter(Mandatory = false)]
		public int ServerCount
		{
			get
			{
				return (int)(base.Fields[ClientAccessArraySchema.ServerCount] ?? 0);
			}
			set
			{
				base.Fields[ClientAccessArraySchema.ServerCount] = value;
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06005818 RID: 22552 RVA: 0x0016FCD1 File Offset: 0x0016DED1
		// (set) Token: 0x06005819 RID: 22553 RVA: 0x0016FCE8 File Offset: 0x0016DEE8
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public AdSiteIdParameter Site
		{
			get
			{
				return (AdSiteIdParameter)base.Fields[ClientAccessArraySchema.Site];
			}
			set
			{
				base.Fields[ClientAccessArraySchema.Site] = value;
			}
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x0016FCFC File Offset: 0x0016DEFC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string site = (this.Site != null) ? this.Site.ToString() : ((ITopologyConfigurationSession)this.ConfigurationSession).GetLocalSite().Name;
				return Strings.ConfirmationMessageNewClientAccessArray(this.Name, this.ArrayDefinition, this.ServerCount, site);
			}
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0016FD4C File Offset: 0x0016DF4C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.Site != null)
			{
				this.siteObject = this.arrayTaskCommon.GetADSite(this.Site, (ITopologyConfigurationSession)this.ConfigurationSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADSite>));
			}
			else
			{
				this.siteObject = ((ITopologyConfigurationSession)this.ConfigurationSession).GetLocalSite();
				if (this.siteObject == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorSiteNotSpecifiedAndLocalSiteNotFound), ErrorCategory.ObjectNotFound, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x0016FDD2 File Offset: 0x0016DFD2
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.arrayTaskCommon.VerifyArrayUniqueness(base.DataSession, this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x0016FDFC File Offset: 0x0016DFFC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ClientAccessArray clientAccessArray = (ClientAccessArray)base.PrepareDataObject();
			ADObjectId childId = ((ADObjectId)this.RootId).GetChildId(this.DataObject.Name);
			clientAccessArray.SetId(childId);
			clientAccessArray.ArrayDefinition = this.ArrayDefinition;
			clientAccessArray.ServerCount = this.ServerCount;
			clientAccessArray.Site = this.siteObject.Id;
			TaskLogger.LogExit();
			return clientAccessArray;
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x0016FE6C File Offset: 0x0016E06C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ClientAccessArray clientAccessArray = (ClientAccessArray)dataObject;
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x0600581F RID: 22559 RVA: 0x0016FEA6 File Offset: 0x0016E0A6
		protected override ObjectId RootId
		{
			get
			{
				return ClientAccessArray.GetParentContainer((ITopologyConfigurationSession)this.ConfigurationSession);
			}
		}

		// Token: 0x040032B3 RID: 12979
		private ADSite siteObject;

		// Token: 0x040032B4 RID: 12980
		private readonly ClientAccessArrayTaskHelper arrayTaskCommon;
	}
}
