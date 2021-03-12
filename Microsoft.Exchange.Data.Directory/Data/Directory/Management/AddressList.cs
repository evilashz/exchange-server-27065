using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006D9 RID: 1753
	[Serializable]
	public sealed class AddressList : AddressListBase
	{
		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x0012C947 File Offset: 0x0012AB47
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return AddressList.schema;
			}
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x0012C94E File Offset: 0x0012AB4E
		public AddressList()
		{
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x0012C956 File Offset: 0x0012AB56
		public AddressList(AddressBookBase dataObject) : base(dataObject)
		{
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x0012C95F File Offset: 0x0012AB5F
		internal static AddressList FromDataObject(AddressBookBase dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new AddressList(dataObject);
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06005130 RID: 20784 RVA: 0x0012C96C File Offset: 0x0012AB6C
		public string Container
		{
			get
			{
				return (string)this[AddressListSchema.Container];
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x0012C97E File Offset: 0x0012AB7E
		public string Path
		{
			get
			{
				return (string)this[AddressListSchema.Path];
			}
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x06005132 RID: 20786 RVA: 0x0012C990 File Offset: 0x0012AB90
		// (set) Token: 0x06005133 RID: 20787 RVA: 0x0012C9A2 File Offset: 0x0012ABA2
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[AddressListSchema.DisplayName];
			}
			set
			{
				this[AddressListSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x0012C9B0 File Offset: 0x0012ABB0
		// (set) Token: 0x06005135 RID: 20789 RVA: 0x0012C9C2 File Offset: 0x0012ABC2
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return (string)this[AddressListSchema.Name];
			}
			set
			{
				this[AddressListSchema.Name] = value;
			}
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0012C9D0 File Offset: 0x0012ABD0
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			ADObjectNameCharacterConstraint adobjectNameCharacterConstraint = new ADObjectNameCharacterConstraint(new char[]
			{
				'\\'
			});
			ValidationError validationError = adobjectNameCharacterConstraint.Validate(this.Name, AddressBookBaseSchema.Name, this.propertyBag);
			if (validationError != null)
			{
				errors.Add(validationError);
			}
		}

		// Token: 0x04003706 RID: 14086
		private static AddressListSchema schema = ObjectSchema.GetInstance<AddressListSchema>();

		// Token: 0x04003707 RID: 14087
		public static readonly ADObjectId RdnAlContainerToOrganization = new ADObjectId("CN=All Address Lists,CN=Address Lists Container");
	}
}
