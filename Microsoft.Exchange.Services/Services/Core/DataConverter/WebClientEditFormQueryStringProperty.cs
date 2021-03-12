using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000175 RID: 373
	internal sealed class WebClientEditFormQueryStringProperty : WebClientQueryStringPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x00033FDB File Offset: 0x000321DB
		private WebClientEditFormQueryStringProperty(CommandContext commandContext) : base(commandContext)
		{
			this.isDraftProperty = this.propertyDefinitions[3];
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00033FF2 File Offset: 0x000321F2
		public static WebClientEditFormQueryStringProperty CreateCommand(CommandContext commandContext)
		{
			return new WebClientEditFormQueryStringProperty(commandContext);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00033FFC File Offset: 0x000321FC
		protected override bool ValidateProperty(StoreObject storeObject, string className, bool isPublic)
		{
			bool isDraft = (bool)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.isDraftProperty);
			return this.IsEditable(className, isDraft, isPublic);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00034024 File Offset: 0x00032224
		protected override bool ValidatePropertyFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, string className, bool isPublic)
		{
			bool isDraft;
			return PropertyCommand.TryGetValueFromPropertyBag<bool>(propertyBag, this.isDraftProperty, out isDraft) && this.IsEditable(className, isDraft, isPublic);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0003404C File Offset: 0x0003224C
		protected override string GetOwaViewModel(string className)
		{
			if (className != null)
			{
				if (className == "IPM.Appointment")
				{
					return WebClientEditFormQueryStringProperty.EditCalendarViewModel;
				}
				if (className == "IPM.Task")
				{
					return WebClientQueryStringPropertyBase.ReadTaskViewModel;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003408C File Offset: 0x0003228C
		private bool IsEditable(string className, bool isDraft, bool isPublic)
		{
			return (!isPublic || !(className == "IPM.Note")) && (isDraft || (className != null && (className == "IPM.Appointment" || className == "IPM.Task")));
		}

		// Token: 0x040007D1 RID: 2001
		private const int IsDraftPropertyIndex = 3;

		// Token: 0x040007D2 RID: 2002
		private PropertyDefinition isDraftProperty;

		// Token: 0x040007D3 RID: 2003
		private static readonly string EditCalendarViewModel = "ComposeCalendarItemViewModelFactory";
	}
}
