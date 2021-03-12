using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A5B RID: 2651
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XsoDictionaryDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x060060C8 RID: 24776 RVA: 0x00198013 File Offset: 0x00196213
		public XsoDictionaryDataProvider(ExchangePrincipal mailboxOwner, string action) : base(mailboxOwner, action)
		{
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x00198041 File Offset: 0x00196241
		public XsoDictionaryDataProvider(ExchangePrincipal mailboxOwner, ISecurityAccessToken userToken, string action) : base(mailboxOwner, userToken, action)
		{
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x00198070 File Offset: 0x00196270
		public XsoDictionaryDataProvider(MailboxSession session) : base(session)
		{
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x0019809D File Offset: 0x0019629D
		internal XsoDictionaryDataProvider()
		{
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x001980CC File Offset: 0x001962CC
		protected XsoDictionaryDataProvider(ExchangePrincipal mailboxOwner, string action, Func<MailboxSession, string, UserConfigurationTypes, bool, IUserConfiguration> getUserConfiguration, Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration> getReadOnlyUserConfiguration = null) : base(mailboxOwner, action)
		{
			this.getUserConfiguration = getUserConfiguration;
			this.getReadOnlyUserConfiguration = (getReadOnlyUserConfiguration ?? ((Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration>)getUserConfiguration));
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x00198120 File Offset: 0x00196320
		protected XsoDictionaryDataProvider(MailboxSession session, Func<MailboxSession, string, UserConfigurationTypes, bool, IUserConfiguration> getUserConfiguration, Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration> getReadOnlyUserConfiguration = null) : base(session)
		{
			this.getUserConfiguration = getUserConfiguration;
			this.getReadOnlyUserConfiguration = (getReadOnlyUserConfiguration ?? ((Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration>)getUserConfiguration));
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x001984B4 File Offset: 0x001966B4
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (filter != null && !(filter is FalseFilter))
			{
				throw new NotSupportedException("filter");
			}
			ADObjectId adUserId = (!base.MailboxSession.MailboxOwner.ObjectId.IsNullOrEmpty()) ? base.MailboxSession.MailboxOwner.ObjectId : null;
			if (rootId != null && rootId is ADObjectId && !ADObjectId.Equals((ADObjectId)rootId, adUserId))
			{
				throw new NotSupportedException("rootId");
			}
			if (!typeof(XsoMailboxConfigurationObject).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			XsoMailboxConfigurationObject configObject = (XsoMailboxConfigurationObject)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			if (adUserId != null)
			{
				configObject.MailboxOwnerId = adUserId;
			}
			HashSet<string> uniqueConfigurationNames = new HashSet<string>(from x in configObject.Schema.AllProperties
			where x is XsoDictionaryPropertyDefinition
			select ((XsoDictionaryPropertyDefinition)x).UserConfigurationName);
			foreach (string userConfigurationName in uniqueConfigurationNames)
			{
				try
				{
					this.LoadUserConfigurationToConfigObject(userConfigurationName, configObject);
				}
				catch (CorruptDataException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "The calendar configuration for {0} is corrupt", base.MailboxSession.MailboxOwner);
					yield break;
				}
				catch (VirusDetectedException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "The calendar configuration for {0} is virus infected.", base.MailboxSession.MailboxOwner);
					yield break;
				}
			}
			yield return (T)((object)configObject);
			yield break;
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x00198514 File Offset: 0x00196714
		protected override void InternalSave(ConfigurableObject instance)
		{
			XsoMailboxConfigurationObject configObject = (XsoMailboxConfigurationObject)instance;
			HashSet<string> hashSet = new HashSet<string>(from x in configObject.Schema.AllProperties
			where x is XsoDictionaryPropertyDefinition && configObject.IsModified((XsoDictionaryPropertyDefinition)x)
			select ((XsoDictionaryPropertyDefinition)x).UserConfigurationName);
			foreach (string userConfigurationName in hashSet)
			{
				this.SaveConfigObjectToUserConfiguration(userConfigurationName, configObject);
			}
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x001985C4 File Offset: 0x001967C4
		private void LoadUserConfigurationToConfigObject(string userConfigurationName, XsoMailboxConfigurationObject configObject)
		{
			using (IReadableUserConfiguration readableUserConfiguration = this.getReadOnlyUserConfiguration(base.MailboxSession, userConfigurationName, UserConfigurationTypes.Dictionary, false))
			{
				if (readableUserConfiguration != null)
				{
					IDictionary dictionary = readableUserConfiguration.GetDictionary();
					foreach (PropertyDefinition propertyDefinition in configObject.Schema.AllProperties)
					{
						XsoDictionaryPropertyDefinition xsoDictionaryPropertyDefinition = propertyDefinition as XsoDictionaryPropertyDefinition;
						if (xsoDictionaryPropertyDefinition != null && !(xsoDictionaryPropertyDefinition.UserConfigurationName != userConfigurationName) && dictionary.Contains(xsoDictionaryPropertyDefinition.Name))
						{
							configObject.propertyBag.SetField(xsoDictionaryPropertyDefinition, StoreValueConverter.ConvertValueFromStore(xsoDictionaryPropertyDefinition, dictionary[xsoDictionaryPropertyDefinition.Name]));
						}
					}
				}
			}
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x00198698 File Offset: 0x00196898
		private void SaveConfigObjectToUserConfiguration(string userConfigurationName, XsoMailboxConfigurationObject configObject)
		{
			bool flag = false;
			int num = 0;
			do
			{
				using (IUserConfiguration userConfiguration = this.getUserConfiguration(base.MailboxSession, userConfigurationName, UserConfigurationTypes.Dictionary, !flag))
				{
					if (userConfiguration != null)
					{
						IDictionary dictionary;
						try
						{
							dictionary = userConfiguration.GetDictionary();
						}
						catch (CorruptDataException)
						{
							ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "The calendar configuration for {0} is corrupt", base.MailboxSession.MailboxOwner);
							dictionary = new ConfigurationDictionary();
						}
						catch (VirusDetectedException)
						{
							ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "The calendar configuration for {0} is virus infected.", base.MailboxSession.MailboxOwner);
							dictionary = new ConfigurationDictionary();
						}
						foreach (PropertyDefinition propertyDefinition in configObject.Schema.AllProperties)
						{
							XsoDictionaryPropertyDefinition xsoDictionaryPropertyDefinition = propertyDefinition as XsoDictionaryPropertyDefinition;
							if (xsoDictionaryPropertyDefinition != null && !xsoDictionaryPropertyDefinition.IsReadOnly && !(xsoDictionaryPropertyDefinition.UserConfigurationName != userConfigurationName) && configObject.IsModified(xsoDictionaryPropertyDefinition))
							{
								object obj = configObject[xsoDictionaryPropertyDefinition];
								if (obj == null || (obj is ICollection && ((ICollection)obj).Count == 0))
								{
									dictionary.Remove(xsoDictionaryPropertyDefinition.Name);
								}
								else
								{
									dictionary[xsoDictionaryPropertyDefinition.Name] = StoreValueConverter.ConvertValueToStore(obj);
								}
							}
						}
						try
						{
							ExTraceGlobals.FaultInjectionTracer.TraceTest(4289080637U);
							userConfiguration.Save();
							break;
						}
						catch (ObjectExistedException)
						{
							if (flag)
							{
								throw;
							}
							flag = true;
						}
						catch (SaveConflictException)
						{
							if (num >= 5)
							{
								throw;
							}
							num++;
							flag = true;
						}
					}
				}
			}
			while (flag);
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x001988B0 File Offset: 0x00196AB0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XsoDictionaryDataProvider>(this);
		}

		// Token: 0x04003703 RID: 14083
		internal const string OwaUserOptionConfigurationName = "OWA.UserOptions";

		// Token: 0x04003704 RID: 14084
		private Func<MailboxSession, string, UserConfigurationTypes, bool, IUserConfiguration> getUserConfiguration = new Func<MailboxSession, string, UserConfigurationTypes, bool, IUserConfiguration>(UserConfigurationHelper.GetMailboxConfiguration);

		// Token: 0x04003705 RID: 14085
		private Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration> getReadOnlyUserConfiguration = new Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration>(UserConfigurationHelper.GetMailboxConfiguration);
	}
}
