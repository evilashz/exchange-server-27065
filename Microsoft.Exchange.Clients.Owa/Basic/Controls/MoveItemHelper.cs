using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200006D RID: 109
	public static class MoveItemHelper
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0001AECC File Offset: 0x000190CC
		public static ApplicationElement GetApplicationElementFromStoreType(string type)
		{
			if (ObjectClass.IsGenericFolder(type))
			{
				return ApplicationElement.Folder;
			}
			return ApplicationElement.Item;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001AEDC File Offset: 0x000190DC
		public static NavigationModule GetNavigationModuleFromStoreType(string type)
		{
			StoreObjectType objectType = ObjectClass.GetObjectType(type);
			if (objectType == StoreObjectType.Contact || objectType == StoreObjectType.DistributionList || objectType == StoreObjectType.ContactsFolder)
			{
				return NavigationModule.Contacts;
			}
			if (objectType == StoreObjectType.CalendarFolder || objectType == StoreObjectType.CalendarItem)
			{
				return NavigationModule.Calendar;
			}
			return NavigationModule.Mail;
		}
	}
}
