using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200002C RID: 44
	internal sealed class ContactsListViewContents : ListViewContents
	{
		// Token: 0x06000120 RID: 288 RVA: 0x0000976C File Offset: 0x0000796C
		private static Dictionary<PropertyDefinition, ThemeFileId> CreatePropertyIconMap()
		{
			Dictionary<PropertyDefinition, ThemeFileId> dictionary = new Dictionary<PropertyDefinition, ThemeFileId>();
			dictionary[ContactSchema.PrimaryTelephoneNumber] = ThemeFileId.PrimaryPhone;
			dictionary[ContactSchema.BusinessPhoneNumber] = ThemeFileId.WorkPhone;
			dictionary[ContactSchema.BusinessPhoneNumber2] = ThemeFileId.WorkPhone;
			dictionary[ContactSchema.MobilePhone] = ThemeFileId.MobilePhone;
			dictionary[ContactSchema.HomePhone] = ThemeFileId.HomePhone;
			dictionary[ContactSchema.HomePhone2] = ThemeFileId.HomePhone;
			dictionary[ContactSchema.HomeFax] = ThemeFileId.Fax;
			dictionary[ContactSchema.WorkFax] = ThemeFileId.Fax;
			dictionary[ContactSchema.OtherFax] = ThemeFileId.Fax;
			dictionary[ContactSchema.AssistantPhoneNumber] = ThemeFileId.None;
			dictionary[ContactSchema.CallbackPhone] = ThemeFileId.None;
			dictionary[ContactSchema.CarPhone] = ThemeFileId.None;
			dictionary[ContactSchema.Pager] = ThemeFileId.None;
			dictionary[ContactSchema.OtherTelephone] = ThemeFileId.None;
			dictionary[ContactSchema.RadioPhone] = ThemeFileId.None;
			dictionary[ContactSchema.TtyTddPhoneNumber] = ThemeFileId.None;
			dictionary[ContactSchema.InternationalIsdnNumber] = ThemeFileId.None;
			dictionary[ContactSchema.OrganizationMainPhone] = ThemeFileId.None;
			return dictionary;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000987C File Offset: 0x00007A7C
		private static Dictionary<PropertyDefinition, Strings.IDs> CreatePropertyAltMap()
		{
			Dictionary<PropertyDefinition, Strings.IDs> dictionary = new Dictionary<PropertyDefinition, Strings.IDs>();
			dictionary[ContactSchema.PrimaryTelephoneNumber] = 1442239260;
			dictionary[ContactSchema.BusinessPhoneNumber] = 346027136;
			dictionary[ContactSchema.BusinessPhoneNumber2] = 873918106;
			dictionary[ContactSchema.MobilePhone] = 1158653436;
			dictionary[ContactSchema.HomePhone] = -1844864953;
			dictionary[ContactSchema.HomePhone2] = 1714659233;
			dictionary[ContactSchema.HomeFax] = 1180016964;
			dictionary[ContactSchema.WorkFax] = -11305699;
			dictionary[ContactSchema.OtherFax] = -679895069;
			return dictionary;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009920 File Offset: 0x00007B20
		public ContactsListViewContents(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, bool showFolderNameTooltip, UserContext userContext) : base(viewDescriptor, sortedColumn, sortOrder, showFolderNameTooltip, userContext)
		{
			base.AddProperty(StoreObjectSchema.DisplayName);
			base.AddProperty(StoreObjectSchema.ItemClass);
			base.AddProperty(ContactSchema.SelectedPreferredPhoneNumber);
			if (Utilities.IsJapanese)
			{
				base.AddProperty(ContactSchema.YomiFirstName);
				base.AddProperty(ContactSchema.YomiLastName);
			}
			base.AddProperty(ContactSchema.BusinessPhoneNumber);
			base.AddProperty(ContactSchema.HomePhone);
			base.AddProperty(ContactSchema.MobilePhone);
			foreach (ContactPropertyInfo contactPropertyInfo in ContactUtilities.PhoneNumberProperties)
			{
				base.AddProperty(contactPropertyInfo.PropertyDefinition);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000099BD File Offset: 0x00007BBD
		public static Dictionary<PropertyDefinition, ThemeFileId> PropertyIconMap
		{
			get
			{
				return ContactsListViewContents.propertyIconMap;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000099C4 File Offset: 0x00007BC4
		public static Dictionary<PropertyDefinition, Strings.IDs> PropertyAltMap
		{
			get
			{
				return ContactsListViewContents.propertyAltMap;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000099CC File Offset: 0x00007BCC
		protected override bool RenderItemRowStyle(TextWriter writer, int itemIndex)
		{
			string itemClass = base.DataSource.GetItemProperty(itemIndex, StoreObjectSchema.ItemClass) as string;
			return ObjectClass.IsDistributionList(itemClass);
		}

		// Token: 0x040000BC RID: 188
		private static Dictionary<PropertyDefinition, ThemeFileId> propertyIconMap = ContactsListViewContents.CreatePropertyIconMap();

		// Token: 0x040000BD RID: 189
		private static Dictionary<PropertyDefinition, Strings.IDs> propertyAltMap = ContactsListViewContents.CreatePropertyAltMap();
	}
}
