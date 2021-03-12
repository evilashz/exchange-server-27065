using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000154 RID: 340
	internal sealed class CultureProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x0002D8AE File Offset: 0x0002BAAE
		public CultureProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002D8B7 File Offset: 0x0002BAB7
		public static CultureProperty CreateCommand(CommandContext commandContext)
		{
			return new CultureProperty(commandContext);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			Item storeItem = (Item)commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			CultureInfo itemCultureInfo = CultureProperty.GetItemCultureInfo(storeItem);
			if (itemCultureInfo != null && !string.IsNullOrEmpty(itemCultureInfo.Name))
			{
				serviceObject[this.commandContext.PropertyInformation] = itemCultureInfo.Name;
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002D918 File Offset: 0x0002BB18
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			CultureInfo itemCultureInfo = CultureProperty.GetItemCultureInfo(propertyBag, commandSettings.IdAndSession.Session);
			if (itemCultureInfo != null && !string.IsNullOrEmpty(itemCultureInfo.Name))
			{
				serviceObject[this.commandContext.PropertyInformation] = itemCultureInfo.Name;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002D974 File Offset: 0x0002BB74
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			Item item = (Item)commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			this.SetProperty(serviceObject, item);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			this.SetProperty(setPropertyUpdate.ServiceObject, item);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002D9CC File Offset: 0x0002BBCC
		private static CultureInfo GetItemCultureInfo(IDictionary<PropertyDefinition, object> propertyBag, StoreSession session)
		{
			CultureInfo result = null;
			int lcid;
			if (PropertyCommand.TryGetValueFromPropertyBag<int>(propertyBag, CultureProperty.MessageLocaleID, out lcid) && CultureProperty.TryGetCultureFromLcid(lcid, out result))
			{
				return result;
			}
			int codePage;
			if (PropertyCommand.TryGetValueFromPropertyBag<int>(propertyBag, CultureProperty.InternetCPID, out codePage) && CultureProperty.TryGetCultureInfoFromCodepage(codePage, out result))
			{
				return result;
			}
			if (PropertyCommand.TryGetValueFromPropertyBag<int>(propertyBag, ItemSchema.Codepage, out codePage) && CultureProperty.TryGetCultureInfoFromCodepage(codePage, out result))
			{
				return result;
			}
			if (!CultureProperty.TryGetPrimaryMailboxCulture(session as MailboxSession, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002DA40 File Offset: 0x0002BC40
		private static CultureInfo GetItemCultureInfo(Item storeItem)
		{
			CultureInfo result = null;
			object obj = null;
			if (CultureProperty.TryGetProperty(storeItem, CultureProperty.MessageLocaleID, out obj) && CultureProperty.TryGetCultureFromLcid((int)obj, out result))
			{
				return result;
			}
			if (CultureProperty.TryGetProperty(storeItem, CultureProperty.InternetCPID, out obj) && CultureProperty.TryGetCultureInfoFromCodepage((int)obj, out result))
			{
				return result;
			}
			if (CultureProperty.TryGetProperty(storeItem, ItemSchema.Codepage, out obj) && CultureProperty.TryGetCultureInfoFromCodepage((int)obj, out result))
			{
				return result;
			}
			if (!CultureProperty.TryGetPrimaryMailboxCulture(storeItem.Session as MailboxSession, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002DAC8 File Offset: 0x0002BCC8
		private static void SetItemCulture(Item storeItem, CultureInfo cultureInfo)
		{
			storeItem[CultureProperty.MessageLocaleID] = LocaleMap.GetLcidFromCulture(cultureInfo);
			int num = 0;
			if (CultureProperty.TryGetCodePageFromCultureInfo(cultureInfo, out num))
			{
				storeItem[ItemSchema.Codepage] = num;
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002DB08 File Offset: 0x0002BD08
		private static bool TryGetPrimaryMailboxCulture(MailboxSession session, out CultureInfo cultureInfo)
		{
			cultureInfo = null;
			if (session != null && !PropertyCommand.InMemoryProcessOnly && session.Capabilities.CanHaveCulture)
			{
				CultureInfo[] mailboxCultures = session.GetMailboxCultures();
				cultureInfo = ((mailboxCultures.Length > 0) ? mailboxCultures[0] : null);
			}
			return cultureInfo != null;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		private static bool TryGetCultureFromLcid(int lcid, out CultureInfo culture)
		{
			bool result;
			try
			{
				culture = LocaleMap.GetCultureFromLcid(lcid);
				result = true;
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, string>(0L, "[CultureProperty::TryGetCultureInfoFromLcid] ArgumentException encountered when using lcid '{0}'.  Exception message: {1}", lcid, ex.Message);
				culture = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002DB98 File Offset: 0x0002BD98
		private static bool TryGetCultureInfoFromCodepage(int codePage, out CultureInfo cultureInfo)
		{
			bool result;
			try
			{
				Charset charset = Charset.GetCharset(codePage);
				Culture culture = charset.Culture;
				cultureInfo = new CultureInfo(culture.Name);
				result = true;
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, string>(0L, "[CultureProperty::TryGetCultureInfoFromCodepage] ArgumentException encountered when using codepage '{0}'.  Exception message: {1}", codePage, ex.Message);
				cultureInfo = null;
				result = false;
			}
			catch (InvalidCharsetException ex2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, string>(0L, "[CultureProperty::TryGetCultureInfoFromCodepage] InvalidCharsetException encountered when using codepage '{0}'.  Exception message: {1}", codePage, ex2.Message);
				cultureInfo = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002DC28 File Offset: 0x0002BE28
		private static bool TryGetCodePageFromCultureInfo(CultureInfo cultureInfo, out int codePage)
		{
			Culture culture = null;
			if (!Culture.TryGetCulture(cultureInfo.Name, out culture))
			{
				codePage = 0;
				return false;
			}
			codePage = culture.WindowsCharset.CodePage;
			return true;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		private static bool TryGetProperty(StoreObject storeObject, PropertyDefinition propDef, out object value)
		{
			object obj = storeObject.TryGetProperty(propDef);
			if (obj is PropertyError)
			{
				value = null;
				return false;
			}
			value = obj;
			return true;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002DC84 File Offset: 0x0002BE84
		private void SetProperty(ServiceObject serviceObject, Item item)
		{
			string valueOrDefault = serviceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
			CultureInfo cultureInfo = null;
			try
			{
				cultureInfo = new CultureInfo(valueOrDefault);
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, string>(0L, "[CultureProperty::SetProperty] ArgumentException encountered whensetting property to value '{0}'.  Exception message: {1}", valueOrDefault, ex.Message);
				throw new UnsupportedCultureException(ex);
			}
			if (cultureInfo == CultureInfo.InvariantCulture)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "[CultureProperty::SetProperty] Can't set item culture to invariant culture.  Input culture: {0}", valueOrDefault);
				throw new UnsupportedCultureException();
			}
			CultureProperty.SetItemCulture(item, cultureInfo);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002DD08 File Offset: 0x0002BF08
		public void ToXml()
		{
			throw new InvalidOperationException("CultureProperty.ToXml should not be called.");
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002DD14 File Offset: 0x0002BF14
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("CultureProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x0400077E RID: 1918
		public static PropertyTagPropertyDefinition InternetCPID = PropertyTagPropertyDefinition.CreateCustom("PR_INTERNET_CPID", 1071513603U);

		// Token: 0x0400077F RID: 1919
		public static PropertyTagPropertyDefinition MessageLocaleID = PropertyTagPropertyDefinition.CreateCustom("PR_MESSAGE_LOCALE_ID", 1072758787U);
	}
}
