using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MobileTransport;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A8B RID: 2699
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class VersionedXmlDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x060062E8 RID: 25320 RVA: 0x001A17AD File Offset: 0x0019F9AD
		public VersionedXmlDataProvider(ExchangePrincipal mailboxOwner, ISecurityAccessToken userToken, string action) : base(mailboxOwner, userToken, action)
		{
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x001A17B8 File Offset: 0x0019F9B8
		public VersionedXmlDataProvider(ExchangePrincipal mailboxOwner, string action) : base(mailboxOwner, action)
		{
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x001A17C2 File Offset: 0x0019F9C2
		public VersionedXmlDataProvider(MailboxSession session) : base(session)
		{
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x001A1BE4 File Offset: 0x0019FDE4
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (filter != null && !(filter is FalseFilter))
			{
				throw new NotSupportedException("filter");
			}
			if (rootId != null && rootId is ADObjectId && !ADObjectId.Equals((ADObjectId)rootId, base.MailboxSession.MailboxOwner.ObjectId))
			{
				throw new NotSupportedException("rootId");
			}
			if (!typeof(VersionedXmlConfigurationObject).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			VersionedXmlConfigurationObject configObject = (VersionedXmlConfigurationObject)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			configObject[VersionedXmlConfigurationObjectSchema.Identity] = base.MailboxSession.MailboxOwner.ObjectId;
			VersionedXmlBase configXml = null;
			using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(base.MailboxSession, configObject.UserConfigurationName, UserConfigurationTypes.XML, false))
			{
				if (mailboxConfiguration != null)
				{
					using (Stream xmlStream = mailboxConfiguration.GetXmlStream())
					{
						try
						{
							configXml = VersionedXmlBase.Deserialize(xmlStream);
						}
						catch (InvalidOperationException ex)
						{
							ExTraceGlobals.XsoTracer.TraceDebug<string>((long)this.GetHashCode(), "Deserialize TextMessagingSettings failed: {0}", ex.ToString());
						}
					}
				}
			}
			if (configXml != null)
			{
				if (configObject.RawVersionedXmlPropertyDefinition.Type != configXml.GetType())
				{
					throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
				}
				configObject[configObject.RawVersionedXmlPropertyDefinition] = configXml;
				TextMessagingAccount textMessagingAccount = configObject as TextMessagingAccount;
				if (textMessagingAccount != null)
				{
					textMessagingAccount.NotificationPreferredCulture = base.MailboxSession.PreferedCulture;
					if (textMessagingAccount.CountryRegionId != null)
					{
						if (string.Equals(textMessagingAccount.CountryRegionId.TwoLetterISORegionName, "US", StringComparison.OrdinalIgnoreCase))
						{
							textMessagingAccount.NotificationPreferredCulture = new CultureInfo("en-US");
						}
						else if (string.Equals(textMessagingAccount.CountryRegionId.TwoLetterISORegionName, "CA", StringComparison.OrdinalIgnoreCase))
						{
							if (textMessagingAccount.NotificationPreferredCulture != null && string.Equals(textMessagingAccount.NotificationPreferredCulture.TwoLetterISOLanguageName, "fr", StringComparison.OrdinalIgnoreCase))
							{
								textMessagingAccount.NotificationPreferredCulture = new CultureInfo("fr-CA");
							}
							else
							{
								textMessagingAccount.NotificationPreferredCulture = new CultureInfo("en-CA");
							}
						}
					}
				}
				ValidationError[] array = configObject.Validate();
				if (array != null && 0 < array.Length)
				{
					ExTraceGlobals.XsoTracer.TraceDebug<string>((long)this.GetHashCode(), "TextMessagingSettings validation failed: {0}", array[0].ToString());
					configObject[configObject.RawVersionedXmlPropertyDefinition] = null;
				}
				configObject.ResetChangeTracking();
			}
			yield return (T)((object)configObject);
			yield break;
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x001A1C10 File Offset: 0x0019FE10
		protected override void InternalSave(ConfigurableObject instance)
		{
			VersionedXmlConfigurationObject versionedXmlConfigurationObject = (VersionedXmlConfigurationObject)instance;
			using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(base.MailboxSession, versionedXmlConfigurationObject.UserConfigurationName, UserConfigurationTypes.XML, true))
			{
				using (Stream xmlStream = mailboxConfiguration.GetXmlStream())
				{
					VersionedXmlBase.Serialize(xmlStream, (VersionedXmlBase)versionedXmlConfigurationObject[versionedXmlConfigurationObject.RawVersionedXmlPropertyDefinition]);
				}
				mailboxConfiguration.Save();
			}
			instance.ResetChangeTracking();
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x001A1C98 File Offset: 0x0019FE98
		protected override void InternalDelete(ConfigurableObject instance)
		{
			VersionedXmlConfigurationObject versionedXmlConfigurationObject = (VersionedXmlConfigurationObject)instance;
			using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(base.MailboxSession, versionedXmlConfigurationObject.UserConfigurationName, UserConfigurationTypes.XML, false))
			{
				if (mailboxConfiguration == null)
				{
					return;
				}
			}
			UserConfigurationHelper.DeleteMailboxConfiguration(base.MailboxSession, versionedXmlConfigurationObject.UserConfigurationName);
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x001A1CF4 File Offset: 0x0019FEF4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VersionedXmlDataProvider>(this);
		}
	}
}
