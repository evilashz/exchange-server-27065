using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000529 RID: 1321
	public static class OrganizationRelationshipHelper
	{
		// Token: 0x06003AE2 RID: 15074 RVA: 0x000E1068 File Offset: 0x000DF268
		internal static GetterDelegate GetOrganizationRelationshipState(string sharedResource, ProviderPropertyDefinition federationEnabledActions)
		{
			return delegate(IPropertyBag properties)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)properties[federationEnabledActions];
				return multiValuedProperty.Contains(sharedResource);
			};
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000E10FC File Offset: 0x000DF2FC
		internal static SetterDelegate SetOrganizationRelationshipState(string sharedResource, ProviderPropertyDefinition federationEnabledActions)
		{
			return delegate(object isEnabledObject, IPropertyBag properties)
			{
				bool flag = (bool)isEnabledObject;
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)properties[federationEnabledActions];
				bool flag2 = multiValuedProperty.Contains(sharedResource);
				if (flag && !flag2)
				{
					multiValuedProperty.Add(sharedResource);
					return;
				}
				if (!flag && flag2)
				{
					multiValuedProperty.Remove(sharedResource);
				}
			};
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000E1129 File Offset: 0x000DF329
		internal static object GetFreeBusyAccessLevel(IPropertyBag properties)
		{
			return OrganizationRelationshipHelper.GetAccessLevel<FreeBusyAccessLevel>(properties, "MSExchange.SharingCalendarFreeBusyLevel:", FreeBusyAccessLevel.None);
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000E1137 File Offset: 0x000DF337
		internal static void SetFreeBusyAccessLevel(object value, IPropertyBag properties)
		{
			OrganizationRelationshipHelper.SetAccessLevel<FreeBusyAccessLevel>((FreeBusyAccessLevel)value, properties, "MSExchange.SharingCalendarFreeBusyLevel:");
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x000E114A File Offset: 0x000DF34A
		internal static object GetFreeBusyAccessScope(IPropertyBag properties)
		{
			return OrganizationRelationshipHelper.GetAccessScope(properties, "MSExchange.SharingCalendarFreeBusyLevel:", OrganizationRelationshipNonAdProperties.FreeBusyAccessScopeCache);
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x000E115C File Offset: 0x000DF35C
		internal static void SetFreeBusyAccessScope(object value, IPropertyBag properties)
		{
			OrganizationRelationshipHelper.SetAccessScope<FreeBusyAccessLevel>(value as ADObjectId, properties, "MSExchange.SharingCalendarFreeBusyLevel:", FreeBusyAccessLevel.None);
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000E1170 File Offset: 0x000DF370
		internal static object GetMailTipsAccessLevel(IPropertyBag properties)
		{
			return OrganizationRelationshipHelper.GetAccessLevel<MailTipsAccessLevel>(properties, "MSExchange.MailTipsAccessLevel:", MailTipsAccessLevel.None);
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000E117E File Offset: 0x000DF37E
		internal static void SetMailTipsAccessLevel(object value, IPropertyBag properties)
		{
			OrganizationRelationshipHelper.SetAccessLevel<MailTipsAccessLevel>((MailTipsAccessLevel)value, properties, "MSExchange.MailTipsAccessLevel:");
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000E1191 File Offset: 0x000DF391
		internal static object GetMailTipsAccessScope(IPropertyBag properties)
		{
			return OrganizationRelationshipHelper.GetAccessScope(properties, "MSExchange.MailTipsAccessLevel:", OrganizationRelationshipNonAdProperties.MailTipsAccessScopeScopeCache);
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000E11A3 File Offset: 0x000DF3A3
		internal static void SetMailTipsAccessScope(object value, IPropertyBag properties)
		{
			OrganizationRelationshipHelper.SetAccessScope<MailTipsAccessLevel>(value as ADObjectId, properties, "MSExchange.MailTipsAccessLevel:", MailTipsAccessLevel.None);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000E11B8 File Offset: 0x000DF3B8
		private static object GetAccessLevel<T>(IPropertyBag properties, string actionPrefix, T defaultLevel)
		{
			MultiValuedProperty<string> actions = (MultiValuedProperty<string>)properties[OrganizationRelationshipSchema.FederationEnabledActions];
			string action = OrganizationRelationshipHelper.GetAction(actions, actionPrefix);
			string levelElement = OrganizationRelationshipHelper.GetLevelElement(action);
			if (levelElement == null)
			{
				return defaultLevel;
			}
			object result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), levelElement, true));
			}
			catch (ArgumentNullException)
			{
				result = defaultLevel;
			}
			catch (ArgumentException)
			{
				result = defaultLevel;
			}
			return result;
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000E1240 File Offset: 0x000DF440
		private static void SetAccessLevel<T>(T accessLevel, IPropertyBag properties, string prefix)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)properties[OrganizationRelationshipSchema.FederationEnabledActions];
			string andRemoveAction = OrganizationRelationshipHelper.GetAndRemoveAction(multiValuedProperty, prefix);
			string targetElement = OrganizationRelationshipHelper.GetTargetElement(andRemoveAction);
			multiValuedProperty.Add(OrganizationRelationshipHelper.GenerateAction(prefix, accessLevel.ToString(), targetElement));
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000E1288 File Offset: 0x000DF488
		private static object GetAccessScope(IPropertyBag properties, string prefix, ADPropertyDefinition cacheDefinition)
		{
			MultiValuedProperty<string> actions = (MultiValuedProperty<string>)properties[OrganizationRelationshipSchema.FederationEnabledActions];
			string action = OrganizationRelationshipHelper.GetAction(actions, prefix);
			string targetElement = OrganizationRelationshipHelper.GetTargetElement(action);
			if (targetElement == null)
			{
				return null;
			}
			Guid guid;
			try
			{
				guid = new Guid(targetElement);
			}
			catch (FormatException)
			{
				return null;
			}
			catch (OverflowException)
			{
				return null;
			}
			ADObjectId adobjectId = (ADObjectId)properties[cacheDefinition];
			if (adobjectId != null && adobjectId.ObjectGuid == guid)
			{
				return adobjectId;
			}
			return new ADObjectId(guid);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000E1318 File Offset: 0x000DF518
		private static void SetAccessScope<T>(ADObjectId objectId, IPropertyBag properties, string prefix, T defaultLevel)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)properties[OrganizationRelationshipSchema.FederationEnabledActions];
			string target = (objectId != null) ? objectId.ObjectGuid.ToString() : null;
			string andRemoveAction = OrganizationRelationshipHelper.GetAndRemoveAction(multiValuedProperty, prefix);
			string text = OrganizationRelationshipHelper.GetLevelElement(andRemoveAction);
			if (text == null)
			{
				text = defaultLevel.ToString();
			}
			multiValuedProperty.Add(OrganizationRelationshipHelper.GenerateAction(prefix, text, target));
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000E1380 File Offset: 0x000DF580
		private static string GetLevelElement(string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				return null;
			}
			int num = action.IndexOf(':');
			if (num == -1)
			{
				return null;
			}
			int num2 = action.IndexOf(':', num + 1);
			if (num2 == -1)
			{
				num2 = action.Length;
			}
			return action.Substring(num + 1, num2 - num - 1);
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000E13CC File Offset: 0x000DF5CC
		private static string GetTargetElement(string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				return null;
			}
			int num = action.IndexOf(':');
			if (num == -1)
			{
				return null;
			}
			int num2 = action.IndexOf(':', num + 1);
			if (num2 == -1)
			{
				return null;
			}
			return action.Substring(num2 + 1, action.Length - num2 - 1);
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000E1417 File Offset: 0x000DF617
		private static string GenerateAction(string prefix, string level, string target)
		{
			if (target == null)
			{
				return prefix + level;
			}
			return prefix + level + ":" + target;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000E1434 File Offset: 0x000DF634
		private static string GetAction(MultiValuedProperty<string> actions, string prefix)
		{
			foreach (string text in actions)
			{
				if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000E148C File Offset: 0x000DF68C
		private static string GetAndRemoveAction(MultiValuedProperty<string> actions, string prefix)
		{
			string action = OrganizationRelationshipHelper.GetAction(actions, prefix);
			if (action != null)
			{
				actions.Remove(action);
			}
			return action;
		}

		// Token: 0x040027E6 RID: 10214
		private const string AvailabilityLevelWithSeparator = "MSExchange.SharingCalendarFreeBusyLevel:";

		// Token: 0x040027E7 RID: 10215
		private const string MailTipsAccessLevelWithSeparator = "MSExchange.MailTipsAccessLevel:";
	}
}
