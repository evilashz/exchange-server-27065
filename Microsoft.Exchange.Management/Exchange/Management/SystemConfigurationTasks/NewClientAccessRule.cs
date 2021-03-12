using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200087E RID: 2174
	[Cmdlet("New", "ClientAccessRule", SupportsShouldProcess = true)]
	public sealed class NewClientAccessRule : NewMultitenancySystemConfigurationObjectTask<ADClientAccessRule>
	{
		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x00137FEC File Offset: 0x001361EC
		// (set) Token: 0x06004B47 RID: 19271 RVA: 0x00137FF9 File Offset: 0x001361F9
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return this.DataObject.Priority;
			}
			set
			{
				this.DataObject.Priority = value;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x00138007 File Offset: 0x00136207
		// (set) Token: 0x06004B49 RID: 19273 RVA: 0x00138014 File Offset: 0x00136214
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x00138022 File Offset: 0x00136222
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x0013802F File Offset: 0x0013622F
		[Parameter(Mandatory = false)]
		public bool DatacenterAdminsOnly
		{
			get
			{
				return this.DataObject.DatacenterAdminsOnly;
			}
			set
			{
				this.DataObject.DatacenterAdminsOnly = value;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x0013803D File Offset: 0x0013623D
		// (set) Token: 0x06004B4D RID: 19277 RVA: 0x0013804A File Offset: 0x0013624A
		[Parameter(Mandatory = true)]
		public ClientAccessRulesAction Action
		{
			get
			{
				return this.DataObject.Action;
			}
			set
			{
				this.DataObject.Action = value;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x00138058 File Offset: 0x00136258
		// (set) Token: 0x06004B4F RID: 19279 RVA: 0x00138065 File Offset: 0x00136265
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> AnyOfClientIPAddressesOrRanges
		{
			get
			{
				return this.DataObject.AnyOfClientIPAddressesOrRanges;
			}
			set
			{
				this.DataObject.AnyOfClientIPAddressesOrRanges = value;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x00138073 File Offset: 0x00136273
		// (set) Token: 0x06004B51 RID: 19281 RVA: 0x00138080 File Offset: 0x00136280
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> ExceptAnyOfClientIPAddressesOrRanges
		{
			get
			{
				return this.DataObject.ExceptAnyOfClientIPAddressesOrRanges;
			}
			set
			{
				this.DataObject.ExceptAnyOfClientIPAddressesOrRanges = value;
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x0013808E File Offset: 0x0013628E
		// (set) Token: 0x06004B53 RID: 19283 RVA: 0x0013809B File Offset: 0x0013629B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IntRange> AnyOfSourceTcpPortNumbers
		{
			get
			{
				return this.DataObject.AnyOfSourceTcpPortNumbers;
			}
			set
			{
				this.DataObject.AnyOfSourceTcpPortNumbers = value;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x001380A9 File Offset: 0x001362A9
		// (set) Token: 0x06004B55 RID: 19285 RVA: 0x001380B6 File Offset: 0x001362B6
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IntRange> ExceptAnyOfSourceTcpPortNumbers
		{
			get
			{
				return this.DataObject.ExceptAnyOfSourceTcpPortNumbers;
			}
			set
			{
				this.DataObject.ExceptAnyOfSourceTcpPortNumbers = value;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x001380C4 File Offset: 0x001362C4
		// (set) Token: 0x06004B57 RID: 19287 RVA: 0x001380D1 File Offset: 0x001362D1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UsernameMatchesAnyOfPatterns
		{
			get
			{
				return this.DataObject.UsernameMatchesAnyOfPatterns;
			}
			set
			{
				this.DataObject.UsernameMatchesAnyOfPatterns = value;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x001380DF File Offset: 0x001362DF
		// (set) Token: 0x06004B59 RID: 19289 RVA: 0x001380EC File Offset: 0x001362EC
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExceptUsernameMatchesAnyOfPatterns
		{
			get
			{
				return this.DataObject.ExceptUsernameMatchesAnyOfPatterns;
			}
			set
			{
				this.DataObject.ExceptUsernameMatchesAnyOfPatterns = value;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06004B5A RID: 19290 RVA: 0x001380FA File Offset: 0x001362FA
		// (set) Token: 0x06004B5B RID: 19291 RVA: 0x00138107 File Offset: 0x00136307
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UserIsMemberOf
		{
			get
			{
				return this.DataObject.UserIsMemberOf;
			}
			set
			{
				this.DataObject.UserIsMemberOf = value;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06004B5C RID: 19292 RVA: 0x00138115 File Offset: 0x00136315
		// (set) Token: 0x06004B5D RID: 19293 RVA: 0x00138122 File Offset: 0x00136322
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExceptUserIsMemberOf
		{
			get
			{
				return this.DataObject.ExceptUserIsMemberOf;
			}
			set
			{
				this.DataObject.ExceptUserIsMemberOf = value;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06004B5E RID: 19294 RVA: 0x00138130 File Offset: 0x00136330
		// (set) Token: 0x06004B5F RID: 19295 RVA: 0x0013813D File Offset: 0x0013633D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessAuthenticationMethod> AnyOfAuthenticationTypes
		{
			get
			{
				return this.DataObject.AnyOfAuthenticationTypes;
			}
			set
			{
				this.DataObject.AnyOfAuthenticationTypes = value;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06004B60 RID: 19296 RVA: 0x0013814B File Offset: 0x0013634B
		// (set) Token: 0x06004B61 RID: 19297 RVA: 0x00138158 File Offset: 0x00136358
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessAuthenticationMethod> ExceptAnyOfAuthenticationTypes
		{
			get
			{
				return this.DataObject.ExceptAnyOfAuthenticationTypes;
			}
			set
			{
				this.DataObject.ExceptAnyOfAuthenticationTypes = value;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x00138166 File Offset: 0x00136366
		// (set) Token: 0x06004B63 RID: 19299 RVA: 0x00138173 File Offset: 0x00136373
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessProtocol> AnyOfProtocols
		{
			get
			{
				return this.DataObject.AnyOfProtocols;
			}
			set
			{
				this.DataObject.AnyOfProtocols = value;
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x00138181 File Offset: 0x00136381
		// (set) Token: 0x06004B65 RID: 19301 RVA: 0x0013818E File Offset: 0x0013638E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessProtocol> ExceptAnyOfProtocols
		{
			get
			{
				return this.DataObject.ExceptAnyOfProtocols;
			}
			set
			{
				this.DataObject.ExceptAnyOfProtocols = value;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06004B66 RID: 19302 RVA: 0x0013819C File Offset: 0x0013639C
		// (set) Token: 0x06004B67 RID: 19303 RVA: 0x001381A9 File Offset: 0x001363A9
		[Parameter(Mandatory = false)]
		public string UserRecipientFilter
		{
			get
			{
				return this.DataObject.UserRecipientFilter;
			}
			set
			{
				this.DataObject.UserRecipientFilter = value;
			}
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x001381B7 File Offset: 0x001363B7
		private ADObjectId GetBaseContainer()
		{
			return base.CurrentOrgContainerId.GetChildId(ADClientAccessRuleCollection.ContainerName);
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x001381C9 File Offset: 0x001363C9
		private ADObjectId GetObjectId()
		{
			return this.GetBaseContainer().GetChildId(base.Name);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x001381DC File Offset: 0x001363DC
		protected override void WriteResult(IConfigurable result)
		{
			((ADClientAccessRule)result).Priority = this.DataObject.Priority;
			base.WriteResult(result);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x001381FC File Offset: 0x001363FC
		protected override void InternalProcessRecord()
		{
			int priority = 0;
			bool flag = false;
			int clientAccessRulesLimit = AppSettings.Current.ClientAccessRulesLimit;
			ClientAccessRulesPriorityManager clientAccessRulesPriorityManager = new ClientAccessRulesPriorityManager(ClientAccessRulesStorageManager.GetClientAccessRules((IConfigurationSession)base.DataSession));
			if (clientAccessRulesPriorityManager.ADClientAccessRules.Count >= clientAccessRulesLimit)
			{
				base.WriteError(new InvalidOperationException(RulesTasksStrings.ClientAccessRulesLimitError(clientAccessRulesLimit)), ErrorCategory.InvalidOperation, null);
			}
			if (!ClientAccessRulesStorageManager.IsADRuleValid(this.DataObject))
			{
				base.WriteError(new InvalidOperationException(RulesTasksStrings.ClientAccessRulesAuthenticationTypeInvalid), ErrorCategory.InvalidOperation, null);
			}
			this.DataObject.InternalPriority = clientAccessRulesPriorityManager.GetInternalPriority(this.Priority, this.DatacenterAdminsOnly, out priority, out flag);
			this.DataObject.RuleName = this.DataObject.Name;
			this.DataObject.Priority = priority;
			if (flag)
			{
				ClientAccessRulesStorageManager.SaveRules((IConfigurationSession)base.DataSession, clientAccessRulesPriorityManager.ADClientAccessRules);
			}
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.InternalProcessRecord();
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x001382E6 File Offset: 0x001364E6
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			this.DataObject.SetId(this.GetObjectId());
			return this.DataObject;
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06004B6D RID: 19309 RVA: 0x00138306 File Offset: 0x00136506
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return RulesTasksStrings.ConfirmationMessageNewClientAccessRule(base.Name);
			}
		}
	}
}
