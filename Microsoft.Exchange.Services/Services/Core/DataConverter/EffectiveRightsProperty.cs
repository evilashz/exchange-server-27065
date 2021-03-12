using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000104 RID: 260
	internal sealed class EffectiveRightsProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x000245B2 File Offset: 0x000227B2
		public EffectiveRightsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000245BB File Offset: 0x000227BB
		public static EffectiveRightsProperty CreateCommand(CommandContext commandContext)
		{
			return new EffectiveRightsProperty(commandContext);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000245C4 File Offset: 0x000227C4
		public static EffectiveRightsType GetFromEffectiveRights(EffectiveRights effectiveRights, IStoreSession session)
		{
			bool? viewPrivateItems = EffectiveRightsProperty.GetViewPrivateItems(session);
			EffectiveRightsType effectiveRightsType = new EffectiveRightsType();
			effectiveRightsType.CreateAssociated = EffectiveRightsProperty.IsSet(effectiveRights, EffectiveRights.CreateAssociated);
			effectiveRightsType.CreateContents = EffectiveRightsProperty.IsSet(effectiveRights, EffectiveRights.CreateContents);
			effectiveRightsType.CreateHierarchy = EffectiveRightsProperty.IsSet(effectiveRights, EffectiveRights.CreateHierarchy);
			effectiveRightsType.Delete = EffectiveRightsProperty.IsSet(effectiveRights, EffectiveRights.Delete);
			effectiveRightsType.Modify = EffectiveRightsProperty.IsSet(effectiveRights, EffectiveRights.Modify);
			effectiveRightsType.Read = EffectiveRightsProperty.IsSet(effectiveRights, EffectiveRights.Read);
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1) && viewPrivateItems != null)
			{
				effectiveRightsType.ViewPrivateItems = new bool?(viewPrivateItems.Value);
			}
			return effectiveRightsType;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002465B File Offset: 0x0002285B
		public void ToXml()
		{
			throw new InvalidOperationException("EffectiveRightsProperty.ToXml should not be called.");
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00024667 File Offset: 0x00022867
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("EffectiveRightsProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00024674 File Offset: 0x00022874
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			EffectiveRights effectiveRights;
			try
			{
				effectiveRights = (EffectiveRights)storeObject[this.propertyDefinitions[0]];
			}
			catch (PropertyErrorException)
			{
				effectiveRights = EffectiveRights.None;
			}
			this.RenderEffectiveRights(serviceObject, effectiveRights, storeObject.Session);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000246D0 File Offset: 0x000228D0
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			int effectiveRights;
			if (PropertyCommand.TryGetValueFromPropertyBag<int>(propertyBag, this.propertyDefinitions[0], out effectiveRights))
			{
				this.RenderEffectiveRights(serviceObject, (EffectiveRights)effectiveRights, commandSettings.IdAndSession.Session);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00024717 File Offset: 0x00022917
		private static bool IsSet(EffectiveRights effectiveRights, EffectiveRights flag)
		{
			return flag == (effectiveRights & flag);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00024720 File Offset: 0x00022920
		private static bool? GetViewPrivateItems(IStoreSession storeSession)
		{
			if (storeSession is MailboxSession)
			{
				MailboxSession mailboxSession = (MailboxSession)storeSession;
				bool privateItemsFilterDisabled = mailboxSession.PrivateItemsFilterDisabled;
				bool value;
				try
				{
					if (privateItemsFilterDisabled)
					{
						mailboxSession.EnablePrivateItemsFilter();
					}
					value = !mailboxSession.FilterPrivateItems;
				}
				finally
				{
					if (privateItemsFilterDisabled)
					{
						mailboxSession.DisablePrivateItemsFilter();
					}
				}
				return new bool?(value);
			}
			return null;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00024784 File Offset: 0x00022984
		private void RenderEffectiveRights(ServiceObject serviceObject, EffectiveRights effectiveRights, StoreSession session)
		{
			serviceObject[this.commandContext.PropertyInformation] = EffectiveRightsProperty.GetFromEffectiveRights(effectiveRights, session);
		}
	}
}
