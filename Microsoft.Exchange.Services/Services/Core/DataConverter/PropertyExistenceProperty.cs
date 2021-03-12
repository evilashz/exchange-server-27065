using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200016E RID: 366
	internal sealed class PropertyExistenceProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00032BDA File Offset: 0x00030DDA
		public PropertyExistenceProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00032BE3 File Offset: 0x00030DE3
		public static PropertyExistenceProperty CreateCommand(CommandContext commandContext)
		{
			return new PropertyExistenceProperty(commandContext);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00032BEB File Offset: 0x00030DEB
		public void ToXml()
		{
			throw new InvalidOperationException("PropertyExistenceProperty.ToXml should not be called.");
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00032C10 File Offset: 0x00030E10
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			serviceObject.PropertyBag[propertyInformation] = PropertyExistenceProperty.GetValue((PropertyDefinition propDef) => storeObject.GetValueOrDefault<bool>(propDef, false));
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00032C80 File Offset: 0x00030E80
		internal static PropertyExistenceType GetValueFromStorePropertyBag(IStorePropertyBag storePropertyBag)
		{
			return PropertyExistenceProperty.GetValue((PropertyDefinition propDef) => storePropertyBag.GetValueOrDefault<bool>(propDef, false));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00032CAC File Offset: 0x00030EAC
		private static PropertyExistenceType GetValue(Func<PropertyDefinition, bool> getterFunc)
		{
			PropertyExistenceType propertyExistenceType = new PropertyExistenceType();
			bool flag = false;
			foreach (KeyValuePair<PropertyDefinition, Action<PropertyExistenceType>> keyValuePair in PropertyExistenceProperty.propSetterMap)
			{
				if (getterFunc(keyValuePair.Key))
				{
					keyValuePair.Value(propertyExistenceType);
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return propertyExistenceType;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00032D80 File Offset: 0x00030F80
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyExistenceProperty()
		{
			Dictionary<PropertyDefinition, Action<PropertyExistenceType>> dictionary = new Dictionary<PropertyDefinition, Action<PropertyExistenceType>>();
			dictionary.Add(ItemSchema.ExtractedMeetingsExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedMeetings = true;
			});
			dictionary.Add(ItemSchema.ExtractedTasksExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedTasks = true;
			});
			dictionary.Add(ItemSchema.ExtractedAddressesExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedAddresses = true;
			});
			dictionary.Add(ItemSchema.ExtractedKeywordsExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedKeywords = true;
			});
			dictionary.Add(ItemSchema.ExtractedUrlsExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedUrls = true;
			});
			dictionary.Add(ItemSchema.ExtractedPhonesExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedPhones = true;
			});
			dictionary.Add(ItemSchema.ExtractedEmailsExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedEmails = true;
			});
			dictionary.Add(ItemSchema.ExtractedContactsExists, delegate(PropertyExistenceType p)
			{
				p.ExtractedContacts = true;
			});
			dictionary.Add(MessageItemSchema.ReplyToNamesExists, delegate(PropertyExistenceType p)
			{
				p.ReplyToNames = true;
			});
			dictionary.Add(MessageItemSchema.ReplyToBlobExists, delegate(PropertyExistenceType p)
			{
				p.ReplyToBlob = true;
			});
			PropertyExistenceProperty.propSetterMap = dictionary;
		}

		// Token: 0x040007C1 RID: 1985
		private static Dictionary<PropertyDefinition, Action<PropertyExistenceType>> propSetterMap;
	}
}
