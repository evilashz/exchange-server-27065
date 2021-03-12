using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007D5 RID: 2005
	[Cmdlet("Get", "AddressList", DefaultParameterSetName = "Identity")]
	public sealed class GetAddressList : GetMultitenancySystemConfigurationObjectTask<AddressListIdParameter, AddressBookBase>
	{
		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06004625 RID: 17957 RVA: 0x001201F4 File Offset: 0x0011E3F4
		// (set) Token: 0x06004626 RID: 17958 RVA: 0x0012020B File Offset: 0x0011E40B
		[Parameter(Mandatory = true, ParameterSetName = "Container")]
		public AddressListIdParameter Container
		{
			get
			{
				return (AddressListIdParameter)base.Fields["Container"];
			}
			set
			{
				base.Fields["Container"] = value;
			}
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x0012021E File Offset: 0x0011E41E
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x00120226 File Offset: 0x0011E426
		[Parameter(ParameterSetName = "SearchSet")]
		public string SearchText { get; set; }

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x0012022F File Offset: 0x0011E42F
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity == null)
				{
					return this.rootId ?? base.RootId;
				}
				return null;
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x0012024B File Offset: 0x0011E44B
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.Identity == null)
				{
					return new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, base.CurrentOrgContainerId.GetDescendantId(AddressList.RdnAlContainerToOrganization));
				}
				return null;
			}
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x00120274 File Offset: 0x0011E474
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.Container != null)
			{
				this.containerAddressBook = (AddressBookBase)base.GetDataObject<AddressBookBase>(this.Container, this.ConfigurationSession, this.RootId, new LocalizedString?(Strings.ErrorAddressListNotFound(this.Container.ToString())), new LocalizedString?(Strings.ErrorAddressListNotUnique(this.Container.ToString())));
				this.rootId = this.containerAddressBook.Id;
			}
			else
			{
				this.rootId = AddressListIdParameter.GetRootContainerId(this.ConfigurationSession, base.CurrentOrganizationId);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x00120310 File Offset: 0x0011E510
		protected override void InternalProcessRecord()
		{
			if (!base.Stopping && this.Container != null)
			{
				this.WriteResult(this.containerAddressBook);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x00120334 File Offset: 0x0011E534
		protected override bool DeepSearch
		{
			get
			{
				return this.Container == null;
			}
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x00120340 File Offset: 0x0011E540
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			AddressBookBase addressBookBase = (AddressBookBase)dataObject;
			bool flag = this.SearchText == null || addressBookBase.DisplayName.IndexOf(this.SearchText, StringComparison.OrdinalIgnoreCase) > 0 || addressBookBase.Path.IndexOf(this.SearchText, StringComparison.OrdinalIgnoreCase) > 0;
			if (flag)
			{
				base.WriteResult(new AddressList(addressBookBase));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002AFE RID: 11006
		private ADObjectId rootId;

		// Token: 0x04002AFF RID: 11007
		private AddressBookBase containerAddressBook;
	}
}
