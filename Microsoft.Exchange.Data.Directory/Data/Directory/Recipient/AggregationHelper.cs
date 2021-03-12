using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000213 RID: 531
	internal static class AggregationHelper
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00075BAC File Offset: 0x00073DAC
		internal static bool IsMailboxRole
		{
			get
			{
				if (AggregationHelper.isMailboxRole == null)
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole"))
					{
						AggregationHelper.isMailboxRole = new bool?(registryKey != null);
					}
				}
				return AggregationHelper.isMailboxRole.Value;
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00075C0C File Offset: 0x00073E0C
		public static void FilterPropertyDefinitionsByBackendSource(IEnumerable<PropertyDefinition> properties, MbxReadMode mbxReadMode, out List<ADPropertyDefinition> adProps, out List<MServPropertyDefinition> mservProps, out List<MbxPropertyDefinition> mbxProps)
		{
			adProps = new List<ADPropertyDefinition>();
			mservProps = new List<MServPropertyDefinition>();
			mbxProps = new List<MbxPropertyDefinition>();
			bool flag = !ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("ConsumerMbxLookupDisabled");
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
				if (propertyDefinition != null)
				{
					if (adpropertyDefinition.MServPropertyDefinition != null)
					{
						mservProps.Add((MServPropertyDefinition)adpropertyDefinition.MServPropertyDefinition);
					}
					if (adpropertyDefinition.MbxPropertyDefinition != null && mbxReadMode != MbxReadMode.NoMbxRead && flag)
					{
						mbxProps.Add((MbxPropertyDefinition)adpropertyDefinition.MbxPropertyDefinition);
					}
					adProps.Add(adpropertyDefinition);
				}
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00075CC0 File Offset: 0x00073EC0
		[Conditional("DEBUG")]
		private static void CheckPropertyDefinitionsForConsistency(ADPropertyDefinition adProp, SimpleProviderPropertyDefinition propertyDefinition, bool checkReadonly = false)
		{
			if (adProp == null)
			{
				throw new ArgumentNullException("adProp");
			}
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (adProp.Name != propertyDefinition.Name)
			{
				throw new ArgumentException(string.Format("Underlying property definition for ADPropertyDefinition {0} has non-matching name: {1}", adProp.Name, propertyDefinition.Name));
			}
			if (adProp.IsMultivalued != propertyDefinition.IsMultivalued)
			{
				throw new ArgumentException(string.Format("ADPropertyDefinition {0} and underlying property definition must have the same IsMultivalued value", adProp.Name));
			}
			if (adProp.Type != propertyDefinition.Type && adProp.Type != Nullable.GetUnderlyingType(propertyDefinition.Type))
			{
				throw new ArgumentException(string.Format("ADPropertyDefinition {0}: underlying property definition must have either the same Type, or Nullable version of it", adProp.Name));
			}
			if (checkReadonly && adProp.IsReadOnly)
			{
				throw new ArgumentException(string.Format("ADPropertyDefinition {0} has underlying property definition but is read-only", adProp.Name));
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00075DA0 File Offset: 0x00073FA0
		public static ADRawEntry PerformMservLookupByPuid(ulong puid, bool isReadOnly, List<MServPropertyDefinition> properties)
		{
			new ADPropertyBag();
			ADRawEntry result;
			using (MservRecipientSession mservRecipientSession = new MservRecipientSession(isReadOnly))
			{
				result = mservRecipientSession.FindADRawEntryByPuid(puid, properties);
			}
			return result;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00075DE0 File Offset: 0x00073FE0
		public static ADRawEntry PerformMbxLookupByPuid(ADObjectId resultId, Guid mdbGuid, bool isReadOnly, List<MbxPropertyDefinition> properties)
		{
			ulong puid;
			if (ConsumerIdentityHelper.TryGetPuidFromGuid(resultId.ObjectGuid, out puid))
			{
				return MbxRecipientSession.FindADRawEntryByPuid(puid, mdbGuid, isReadOnly, properties);
			}
			throw new ArgumentException("resultId");
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00075E10 File Offset: 0x00074010
		public static ADRawEntry PerformMservLookupByMemberName(SmtpAddress memberName, bool isReadOnly, List<MServPropertyDefinition> properties)
		{
			if (!properties.Contains(MServRecipientSchema.NetID))
			{
				properties.Add(MServRecipientSchema.NetID);
			}
			new ADPropertyBag();
			ADRawEntry result;
			using (MservRecipientSession mservRecipientSession = new MservRecipientSession(isReadOnly))
			{
				result = mservRecipientSession.FindADRawEntryByEmailAddress(memberName.ToString(), properties);
			}
			return result;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00075E74 File Offset: 0x00074074
		public static void PerformMservModification(ADPropertyBag mservPropertyBag)
		{
			using (MservRecipientSession mservRecipientSession = new MservRecipientSession(false))
			{
				ADRawEntry instanceToSave = new ADRawEntry(mservPropertyBag);
				mservRecipientSession.Save(instanceToSave);
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00075EB4 File Offset: 0x000740B4
		public static void PerformMbxModification(Guid mdbGuid, Guid mbxGuid, ADPropertyBag properties, bool isNew)
		{
			if (isNew)
			{
				MbxRecipientSession.CreateUserInformationRecord(mdbGuid, mbxGuid, properties);
				return;
			}
			try
			{
				MbxRecipientSession.UpdateUserInformationRecord(mdbGuid, mbxGuid, properties, null);
			}
			catch (ADDriverStoreAccessPermanentException ex)
			{
				if (ex.InnerException == null || !(ex.InnerException is MapiExceptionUserInformationNotFound))
				{
					throw;
				}
				MbxRecipientSession.CreateUserInformationRecord(mdbGuid, mbxGuid, properties);
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00075F0C File Offset: 0x0007410C
		public static ADRawEntry PerformADLookup(ADObjectId identity, List<ADPropertyDefinition> properties)
		{
			ADUser aduser = (ADUser)TemplateTenantConfiguration.GetLocalTempateUser().Clone();
			ADPropertyBag adpropertyBag = new ADPropertyBag();
			foreach (ADPropertyDefinition adpropertyDefinition in properties)
			{
				adpropertyBag.SetField(adpropertyDefinition, aduser[adpropertyDefinition]);
				if (adpropertyDefinition.IsCalculated)
				{
					foreach (ProviderPropertyDefinition providerPropertyDefinition in adpropertyDefinition.SupportingProperties)
					{
						ADPropertyDefinition adpropertyDefinition2 = (ADPropertyDefinition)providerPropertyDefinition;
						adpropertyBag.SetField(adpropertyDefinition2, aduser[adpropertyDefinition2]);
					}
				}
			}
			ADRawEntry adrawEntry = new ADRawEntry(adpropertyBag);
			adrawEntry.SetId(identity);
			adrawEntry.ValidateRead();
			adrawEntry.ResetChangeTracking(true);
			return adrawEntry;
		}

		// Token: 0x04000BE1 RID: 3041
		private static bool? isMailboxRole = null;
	}
}
