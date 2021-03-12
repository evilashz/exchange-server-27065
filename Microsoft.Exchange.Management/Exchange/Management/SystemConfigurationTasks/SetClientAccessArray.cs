using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A3 RID: 2467
	[Cmdlet("Set", "ClientAccessArray", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetClientAccessArray : SetTopologySystemConfigurationObjectTask<ClientAccessArrayIdParameter, ClientAccessArray>
	{
		// Token: 0x06005838 RID: 22584 RVA: 0x0017028A File Offset: 0x0016E48A
		public SetClientAccessArray()
		{
			this.arrayTaskCommon = new ClientAccessArrayTaskHelper(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06005839 RID: 22585 RVA: 0x001702B5 File Offset: 0x0016E4B5
		// (set) Token: 0x0600583A RID: 22586 RVA: 0x001702CC File Offset: 0x0016E4CC
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public string ArrayDefinition
		{
			get
			{
				return (string)base.Fields[ClientAccessArraySchema.ArrayDefinition];
			}
			set
			{
				base.Fields[ClientAccessArraySchema.ArrayDefinition] = value;
			}
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x0600583B RID: 22587 RVA: 0x001702DF File Offset: 0x0016E4DF
		// (set) Token: 0x0600583C RID: 22588 RVA: 0x001702F6 File Offset: 0x0016E4F6
		[ValidateRange(0, 2147483646)]
		[Parameter(Mandatory = false)]
		public int ServerCount
		{
			get
			{
				return (int)base.Fields[ClientAccessArraySchema.ServerCount];
			}
			set
			{
				base.Fields[ClientAccessArraySchema.ServerCount] = value;
			}
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x0600583D RID: 22589 RVA: 0x0017030E File Offset: 0x0016E50E
		// (set) Token: 0x0600583E RID: 22590 RVA: 0x00170325 File Offset: 0x0016E525
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x00170338 File Offset: 0x0016E538
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetClientAccessArray(this.Identity.ToString());
			}
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x0017034C File Offset: 0x0016E54C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ClientAccessArray clientAccessArray = (ClientAccessArray)base.PrepareDataObject();
			if (this.siteObject != null)
			{
				clientAccessArray.Site = this.siteObject.Id;
			}
			if (base.Fields.IsModified(ClientAccessArraySchema.ArrayDefinition))
			{
				clientAccessArray.ArrayDefinition = this.ArrayDefinition;
			}
			if (base.Fields.IsModified(ClientAccessArraySchema.ServerCount))
			{
				clientAccessArray.ServerCount = this.ServerCount;
			}
			TaskLogger.LogExit();
			return clientAccessArray;
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x001703C8 File Offset: 0x0016E5C8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.Site != null)
			{
				this.siteObject = this.arrayTaskCommon.GetADSite(this.Site, (ITopologyConfigurationSession)this.ConfigurationSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADSite>));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0017041C File Offset: 0x0016E61C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.ExchangeVersion.IsOlderThan(ClientAccessArray.MinimumSupportedExchangeObjectVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorCannotChangeBecauseTooOld(this.DataObject.ExchangeVersion.ToString(), ClientAccessArray.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
			this.arrayTaskCommon.VerifyArrayUniqueness(base.DataSession, this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x040032B8 RID: 12984
		private ADSite siteObject;

		// Token: 0x040032B9 RID: 12985
		private readonly ClientAccessArrayTaskHelper arrayTaskCommon;
	}
}
