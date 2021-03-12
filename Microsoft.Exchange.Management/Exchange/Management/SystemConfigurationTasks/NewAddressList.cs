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
	// Token: 0x020007DA RID: 2010
	[Cmdlet("New", "AddressList", SupportsShouldProcess = true, DefaultParameterSetName = "PrecannedFilter")]
	public sealed class NewAddressList : NewAddressBookBase
	{
		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x00120AEC File Offset: 0x0011ECEC
		protected override int MaxAddressLists
		{
			get
			{
				int result;
				try
				{
					result = checked(base.MaxAddressLists * 4);
				}
				catch (OverflowException)
				{
					result = int.MaxValue;
				}
				return result;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x00120B20 File Offset: 0x0011ED20
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAddressList(this.Name.ToString());
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x0600466F RID: 18031 RVA: 0x00120B32 File Offset: 0x0011ED32
		// (set) Token: 0x06004670 RID: 18032 RVA: 0x00120B3F File Offset: 0x0011ED3F
		[Parameter]
		public string DisplayName
		{
			get
			{
				return this.DataObject.DisplayName;
			}
			set
			{
				this.DataObject.DisplayName = value;
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06004671 RID: 18033 RVA: 0x00120B4D File Offset: 0x0011ED4D
		// (set) Token: 0x06004672 RID: 18034 RVA: 0x00120B64 File Offset: 0x0011ED64
		[Parameter(Mandatory = true, Position = 0)]
		public new string Name
		{
			get
			{
				return (string)this.DataObject[AddressBookBaseSchema.Name];
			}
			set
			{
				this.DataObject[AddressBookBaseSchema.Name] = value;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x00120B77 File Offset: 0x0011ED77
		// (set) Token: 0x06004674 RID: 18036 RVA: 0x00120B8E File Offset: 0x0011ED8E
		[Parameter]
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

		// Token: 0x06004675 RID: 18037 RVA: 0x00120BA4 File Offset: 0x0011EDA4
		protected override ADObjectId GetContainerId()
		{
			ADObjectId result = null;
			if (this.Container != null)
			{
				IConfigurable dataObject = base.GetDataObject<AddressBookBase>(this.Container, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorAddressListNotFound(this.Container.ToString())), new LocalizedString?(Strings.ErrorAddressListNotUnique(this.Container.ToString())));
				if (!base.HasErrors)
				{
					result = (ADObjectId)dataObject.Identity;
				}
				return result;
			}
			return base.CurrentOrgContainerId.GetDescendantId(AddressList.RdnAlContainerToOrganization);
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00120C28 File Offset: 0x0011EE28
		protected override void WriteResult(IConfigurable result)
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			base.WriteResult(new AddressList((AddressBookBase)result));
			TaskLogger.LogExit();
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x00120C66 File Offset: 0x0011EE66
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(AddressList).FullName;
			}
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x00120C77 File Offset: 0x0011EE77
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return AddressList.FromDataObject((AddressBookBase)dataObject);
		}
	}
}
