using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200013E RID: 318
	internal class CompleteNameProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x0002A442 File Offset: 0x00028642
		private CompleteNameProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002A44C File Offset: 0x0002864C
		private void SetCompleteNameProperty(ServiceObject serviceObject, string displayNamePrefix, string givenName, string middleName, string surname, string generation, string initials, string displayName, string nickname, string yomiFirstName, string yomiLastName)
		{
			bool flag = string.IsNullOrEmpty(displayNamePrefix);
			flag &= string.IsNullOrEmpty(givenName);
			flag &= string.IsNullOrEmpty(middleName);
			flag &= string.IsNullOrEmpty(surname);
			flag &= string.IsNullOrEmpty(generation);
			flag &= string.IsNullOrEmpty(initials);
			flag &= string.IsNullOrEmpty(displayName);
			flag &= string.IsNullOrEmpty(nickname);
			flag &= string.IsNullOrEmpty(yomiFirstName);
			if (!(flag & string.IsNullOrEmpty(yomiLastName)))
			{
				serviceObject[this.commandContext.PropertyInformation] = new CompleteNameType
				{
					Title = displayNamePrefix,
					FirstName = givenName,
					MiddleName = middleName,
					LastName = surname,
					Suffix = generation,
					Initials = initials,
					FullName = displayName,
					Nickname = nickname,
					YomiFirstName = yomiFirstName,
					YomiLastName = yomiLastName
				};
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002A522 File Offset: 0x00028722
		public static CompleteNameProperty CreateCommand(CommandContext commandContext)
		{
			return new CompleteNameProperty(commandContext);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002A52A File Offset: 0x0002872A
		public void ToXml()
		{
			throw new NotImplementedException("CompleteNameProperty.ToXml should not be called");
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002A538 File Offset: 0x00028738
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			Contact storeObject = (Contact)commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			string displayNamePrefix = null;
			string givenName = null;
			string middleName = null;
			string surname = null;
			string generation = null;
			string initials = null;
			string displayName = null;
			string nickname = null;
			string yomiFirstName = null;
			string yomiLastName = null;
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.DisplayNamePrefix))
			{
				displayNamePrefix = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.DisplayNamePrefix);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.GivenName))
			{
				givenName = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.GivenName);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.MiddleName))
			{
				middleName = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.MiddleName);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.Surname))
			{
				surname = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.Surname);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.Generation))
			{
				generation = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.Generation);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.Initials))
			{
				initials = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.Initials);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, StoreObjectSchema.DisplayName))
			{
				displayName = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, StoreObjectSchema.DisplayName);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.Nickname))
			{
				nickname = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.Nickname);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.YomiFirstName))
			{
				yomiFirstName = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.YomiFirstName);
			}
			if (PropertyCommand.StorePropertyExists(storeObject, ContactSchema.YomiLastName))
			{
				yomiLastName = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ContactSchema.YomiLastName);
			}
			this.SetCompleteNameProperty(serviceObject, displayNamePrefix, givenName, middleName, surname, generation, initials, displayName, nickname, yomiFirstName, yomiLastName);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002A6CB File Offset: 0x000288CB
		public void ToXmlForPropertyBag()
		{
			throw new NotImplementedException("CompleteNameProperty.ToXmlForPropertyBag should not be called");
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002A6D8 File Offset: 0x000288D8
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			string displayNamePrefix = null;
			string givenName = null;
			string middleName = null;
			string surname = null;
			string generation = null;
			string initials = null;
			string displayName = null;
			string nickname = null;
			string yomiFirstName = null;
			string yomiLastName = null;
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.DisplayNamePrefix, out displayNamePrefix);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.GivenName, out givenName);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.MiddleName, out middleName);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.Surname, out surname);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.Generation, out generation);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.Initials, out initials);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, StoreObjectSchema.DisplayName, out displayName);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.Nickname, out nickname);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.YomiFirstName, out yomiFirstName);
			PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ContactSchema.YomiLastName, out yomiLastName);
			this.SetCompleteNameProperty(serviceObject, displayNamePrefix, givenName, middleName, surname, generation, initials, displayName, nickname, yomiFirstName, yomiLastName);
		}
	}
}
