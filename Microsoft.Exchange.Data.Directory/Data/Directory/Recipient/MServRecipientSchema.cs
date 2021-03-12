using System;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000159 RID: 345
	internal class MServRecipientSchema : ObjectSchema
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x000471D4 File Offset: 0x000453D4
		private static object NameGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetCommonNameFromPuid(puid);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000471F8 File Offset: 0x000453F8
		internal static object AliasGetter(IPropertyBag propertyBag)
		{
			bool flag = false;
			if (MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Exo, out flag) == null)
			{
				return null;
			}
			return MServRecipientSchema.NameGetter(propertyBag);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004721C File Offset: 0x0004541C
		internal static void AliasSetter(object value, IPropertyBag propertyBag)
		{
			if (!string.IsNullOrEmpty((string)value))
			{
				throw new ArgumentOutOfRangeException("Alias");
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00047238 File Offset: 0x00045438
		private static object GuidGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetExchangeGuidFromPuid(puid);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00047264 File Offset: 0x00045464
		private static object NetIdGetter(IPropertyBag propertyBag)
		{
			ulong netID = (ulong)propertyBag[MServRecipientSchema.Puid];
			return new NetID((long)netID);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00047288 File Offset: 0x00045488
		private static object ExchangeGuidGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetExchangeGuidFromPuid(puid);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000472B4 File Offset: 0x000454B4
		private static object SidGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetSecurityIdentifierFromPuid(puid);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000472D8 File Offset: 0x000454D8
		private static object LegacyExchangeDNGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetLegacyExchangeDNFromPuid(puid);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000472FC File Offset: 0x000454FC
		private static object DistinguishedNameGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetDistinguishedNameFromPuid(puid);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00047320 File Offset: 0x00045520
		private static object ObjectIdGetter(IPropertyBag propertyBag)
		{
			ulong puid = (ulong)propertyBag[MServRecipientSchema.Puid];
			return ConsumerIdentityHelper.GetADObjectIdFromPuid(puid);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00047344 File Offset: 0x00045544
		internal static void ObjectIdSetter(object value, IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = value as ADObjectId;
			if (adobjectId == null)
			{
				throw new ArgumentNullException("Id");
			}
			ulong num;
			if (!ConsumerIdentityHelper.TryGetPuidFromGuid(adobjectId.ObjectGuid, out num))
			{
				throw new ArgumentException("Id.ObjectGuid");
			}
			ulong num2;
			if (!ConsumerIdentityHelper.TryGetPuidFromDN(adobjectId.DistinguishedName, out num2))
			{
				throw new ArgumentException("Id.DistinguishedName");
			}
			if (num != num2)
			{
				throw new ArgumentException("Id");
			}
			propertyBag[MServRecipientSchema.Puid] = num2;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000473BC File Offset: 0x000455BC
		private static object DatabaseGetter(IPropertyBag propertyBag)
		{
			bool flag;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Exo, out flag);
			if (record == null)
			{
				return null;
			}
			string exoForestFqdn = record.ExoForestFqdn;
			if (!Fqdn.IsValidFqdn(exoForestFqdn))
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculatePropertyGeneric(MServRecipientSchema.Database.Name), MServRecipientSchema.Database, record));
			}
			Guid guid;
			try
			{
				guid = new Guid(record.ExoDatabaseId);
			}
			catch (Exception ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(MServRecipientSchema.Database.Name, ex.Message), MServRecipientSchema.Database, record), ex);
			}
			return new ADObjectId(guid, exoForestFqdn);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00047458 File Offset: 0x00045658
		internal static void DatabaseSetter(object value, IPropertyBag propertyBag)
		{
			MServPropertyDefinition propertyDefinition;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Exo, true, out propertyDefinition);
			ADObjectId adobjectId = (ADObjectId)value;
			MservRecord value2;
			if (adobjectId == null)
			{
				value2 = null;
			}
			else
			{
				value2 = record.GetUpdatedExoRecord(adobjectId.PartitionFQDN, adobjectId.ObjectGuid.ToString());
			}
			propertyBag[propertyDefinition] = value2;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000474A8 File Offset: 0x000456A8
		private static object IsDeletedGetter(IPropertyBag propertyBag)
		{
			MservRecord mservRecord = (MservRecord)propertyBag[MServRecipientSchema.MservPrimaryRecord];
			MservRecord mservRecord2 = (MservRecord)propertyBag[MServRecipientSchema.MservSecondaryRecord];
			MservRecord mservRecord3 = (MservRecord)propertyBag[MServRecipientSchema.MservSoftDeletedPrimaryRecord];
			return mservRecord == null && mservRecord2 == null && mservRecord3 != null;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00047500 File Offset: 0x00045700
		private static object EmailAddressesGetter(IPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = new ProxyAddressCollection();
			proxyAddressCollection.CopyChangesOnly = true;
			MultiValuedProperty<MservRecord> multiValuedProperty = (MultiValuedProperty<MservRecord>)propertyBag[MServRecipientSchema.MservEmailAddressesRecord];
			foreach (MservRecord mservRecord in multiValuedProperty)
			{
				try
				{
					proxyAddressCollection.Add(new SmtpProxyAddress(mservRecord.Key, true));
				}
				catch (ArgumentException)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculatePropertyGeneric(MServRecipientSchema.EmailAddresses.Name), MServRecipientSchema.EmailAddresses, mservRecord.Key));
				}
			}
			return proxyAddressCollection;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000475AC File Offset: 0x000457AC
		internal static void EmailAddressesSetter(object value, IPropertyBag propertyBag)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)value;
			if (proxyAddressCollection == null || proxyAddressCollection.WasCleared)
			{
				throw new MservOperationException(DirectoryStrings.NoResetOrAssignedMvp);
			}
			string puidKey = MServRecipientSchema.GetPuidKey(propertyBag);
			MultiValuedProperty<MservRecord> multiValuedProperty = (MultiValuedProperty<MservRecord>)propertyBag[MServRecipientSchema.MservEmailAddressesRecord];
			foreach (ProxyAddress proxyAddress in proxyAddressCollection.Added)
			{
				bool flag = true;
				MservRecord mservRecord = new MservRecord(proxyAddress.AddressString, 0, null, puidKey, 0);
				foreach (MservRecord record in multiValuedProperty.Added)
				{
					if (mservRecord.SameRecord(record))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					multiValuedProperty.Add(mservRecord);
				}
			}
			foreach (ProxyAddress proxyAddress2 in proxyAddressCollection.Removed)
			{
				foreach (MservRecord mservRecord2 in multiValuedProperty.ToArray())
				{
					if (proxyAddress2.AddressString.Equals(mservRecord2.Key, StringComparison.OrdinalIgnoreCase))
					{
						multiValuedProperty.Remove(mservRecord2);
						break;
					}
				}
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000476D8 File Offset: 0x000458D8
		private static object SatchmoClusterIpGetter(IPropertyBag propertyBag)
		{
			bool flag;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, out flag);
			if (record == null || record.HotmailClusterIp == null)
			{
				return null;
			}
			IPAddress result;
			if (!IPAddress.TryParse(record.HotmailClusterIp, out result))
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculatePropertyGeneric(MServRecipientSchema.SatchmoClusterIp.Name), MServRecipientSchema.SatchmoClusterIp, record));
			}
			return result;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0004772C File Offset: 0x0004592C
		internal static void SatchmoClusterIpSetter(object value, IPropertyBag propertyBag)
		{
			MServPropertyDefinition propertyDefinition;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, true, out propertyDefinition);
			MservRecord value2;
			if (record.HotmailDGroupId == null && value == null)
			{
				value2 = null;
			}
			else
			{
				value2 = record.GetUpdatedHotmailRecord((value == null) ? null : value.ToString(), record.HotmailDGroupId);
			}
			propertyBag[propertyDefinition] = value2;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00047774 File Offset: 0x00045974
		private static object SatchmoDGroupGetter(IPropertyBag propertyBag)
		{
			bool flag;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, out flag);
			if (record == null)
			{
				return string.Empty;
			}
			return record.HotmailDGroupId;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0004779C File Offset: 0x0004599C
		internal static void SatchmoDGroupSetter(object value, IPropertyBag propertyBag)
		{
			MServPropertyDefinition propertyDefinition;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, true, out propertyDefinition);
			MservRecord value2;
			if (record.HotmailClusterIp == null && value == null)
			{
				value2 = null;
			}
			else
			{
				value2 = record.GetUpdatedHotmailRecord(record.HotmailClusterIp, (string)value);
			}
			propertyBag[propertyDefinition] = value2;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000477E0 File Offset: 0x000459E0
		private static object PrimaryMailboxSourceGetter(IPropertyBag propertyBag)
		{
			bool flag;
			MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Exo, out flag);
			if (flag)
			{
				return PrimaryMailboxSourceType.Exo;
			}
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, out flag);
			if (!flag)
			{
				return PrimaryMailboxSourceType.None;
			}
			if (record.IsXmr)
			{
				return PrimaryMailboxSourceType.Exo;
			}
			return PrimaryMailboxSourceType.Hotmail;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0004782C File Offset: 0x00045A2C
		internal static void PrimaryMailboxSourceSetter(object value, IPropertyBag propertyBag)
		{
			bool flag;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Exo, out flag);
			bool flag2;
			MservRecord record2 = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, out flag2);
			if (record != null)
			{
				bool isEmpty = record.IsEmpty;
			}
			if (record2 != null)
			{
				bool isEmpty2 = record2.IsEmpty;
			}
			switch ((PrimaryMailboxSourceType)value)
			{
			case PrimaryMailboxSourceType.None:
				if ((flag && !record.IsEmpty) || (flag2 && !record2.IsEmpty))
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.MailboxPropertiesMustBeClearedFirst(flag ? record : record2), MServRecipientSchema.MservPrimaryRecord, flag ? record : record2));
				}
				return;
			case PrimaryMailboxSourceType.Hotmail:
				if (flag2)
				{
					return;
				}
				if (flag)
				{
					propertyBag[MServRecipientSchema.MservPrimaryRecord] = record.GetUpdatedRecord(7);
					propertyBag[MServRecipientSchema.MservSecondaryRecord] = record2.GetUpdatedRecord(0);
					return;
				}
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotMakePrimary(record2, "Exo"), MServRecipientSchema.MservPrimaryRecord, record2));
			case PrimaryMailboxSourceType.Exo:
				if (flag)
				{
					return;
				}
				if (flag2)
				{
					propertyBag[MServRecipientSchema.MservPrimaryRecord] = record2.GetUpdatedRecord(7);
					propertyBag[MServRecipientSchema.MservSecondaryRecord] = record.GetUpdatedRecord(0);
					return;
				}
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotMakePrimary(record, "Hotmail"), MServRecipientSchema.MservPrimaryRecord, record));
			default:
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExArgumentOutOfRangeException("PrimaryMailboxSource", value), MServRecipientSchema.PrimaryMailboxSource, value));
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00047970 File Offset: 0x00045B70
		private static object RecipientDisplayTypeGetter(IPropertyBag propertyBag)
		{
			bool flag;
			MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Exo, out flag);
			if (flag)
			{
				return Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.MailboxUser;
			}
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, MservValueFormat.Hotmail, out flag);
			if (flag && record.IsXmr)
			{
				return Microsoft.Exchange.Data.Directory.Recipient.RecipientDisplayType.MailboxUser;
			}
			return null;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00047A00 File Offset: 0x00045C00
		private static GetterDelegate MservFlagGetterDelegate(byte mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag propertyBag)
			{
				MservRecord mservRecord = (MservRecord)propertyBag[MServRecipientSchema.MservPrimaryRecord];
				if (mservRecord == null)
				{
					return propertyDefinition.DefaultValue;
				}
				bool value = 0 != (mask & mservRecord.Flags);
				return BoxedConstants.GetBool(value);
			};
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00047A68 File Offset: 0x00045C68
		private static SetterDelegate MservFlagSetterDelegate(byte mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				MServRecipientSchema.UpdateRecordFlag((bool)value, mask, propertyBag, MServRecipientSchema.MservPrimaryRecord);
				MServRecipientSchema.UpdateRecordFlag((bool)value, mask, propertyBag, MServRecipientSchema.MservSecondaryRecord);
			};
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00047A90 File Offset: 0x00045C90
		private static void UpdateRecordFlag(bool value, byte mask, IPropertyBag propertyBag, MServPropertyDefinition propertyDefinition)
		{
			MservRecord mservRecord = (MservRecord)propertyBag[propertyDefinition];
			if (mservRecord != null)
			{
				MservRecord updatedRecordFlag = mservRecord.GetUpdatedRecordFlag(value, mask);
				propertyBag[propertyDefinition] = updatedRecordFlag;
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00047AC0 File Offset: 0x00045CC0
		private static MservRecord GetRecord(IPropertyBag propertyBag, MservValueFormat format, out bool isPrimary)
		{
			MServPropertyDefinition mservPropertyDefinition;
			MservRecord record = MServRecipientSchema.GetRecord(propertyBag, format, false, out mservPropertyDefinition);
			isPrimary = (mservPropertyDefinition == MServRecipientSchema.MservPrimaryRecord);
			return record;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00047AE4 File Offset: 0x00045CE4
		private static MservRecord GetRecord(IPropertyBag propertyBag, MservValueFormat format, bool createIfMissing, out MServPropertyDefinition recordPropDef)
		{
			MservRecord mservRecord = (MservRecord)propertyBag[MServRecipientSchema.MservPrimaryRecord];
			MservRecord mservRecord2 = (MservRecord)propertyBag[MServRecipientSchema.MservSecondaryRecord];
			if (mservRecord != null && mservRecord.ValueFormat == format)
			{
				recordPropDef = MServRecipientSchema.MservPrimaryRecord;
				return mservRecord;
			}
			if (mservRecord2 != null && mservRecord2.ValueFormat == format)
			{
				recordPropDef = MServRecipientSchema.MservSecondaryRecord;
				return mservRecord2;
			}
			if (!createIfMissing)
			{
				recordPropDef = null;
				return null;
			}
			bool flag = mservRecord == null;
			if (mservRecord != null && mservRecord2 != null)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CrossRecordMismatch(mservRecord, mservRecord2), MServRecipientSchema.MservPrimaryRecord, mservRecord));
			}
			string puidKey = MServRecipientSchema.GetPuidKey(propertyBag);
			byte resourceId = flag ? 0 : 7;
			recordPropDef = (flag ? MServRecipientSchema.MservPrimaryRecord : MServRecipientSchema.MservSecondaryRecord);
			byte flags = 0;
			if (!flag)
			{
				flags = mservRecord.Flags;
			}
			MservRecord mservRecord3 = new MservRecord(puidKey, resourceId, null, null, flags);
			propertyBag[recordPropDef] = mservRecord3;
			return mservRecord3;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00047BB4 File Offset: 0x00045DB4
		private static string GetPuidKey(IPropertyBag propertyBag)
		{
			object obj = propertyBag[MServRecipientSchema.Puid];
			if (obj == null)
			{
				throw new ArgumentNullException("Puid");
			}
			return MservRecord.KeyFromPuid((ulong)obj);
		}

		// Token: 0x04000886 RID: 2182
		public static readonly MServPropertyDefinition MservPrimaryRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservPrimaryRecord", PropertyDefinitionFlags.None);

		// Token: 0x04000887 RID: 2183
		public static readonly MServPropertyDefinition MservSoftDeletedPrimaryRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservSoftDeletedPrimaryRecord", PropertyDefinitionFlags.None);

		// Token: 0x04000888 RID: 2184
		public static readonly MServPropertyDefinition MservCalendarRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservCalendarRecord", PropertyDefinitionFlags.None);

		// Token: 0x04000889 RID: 2185
		public static readonly MServPropertyDefinition PasRecord = MServPropertyDefinition.RawRecordPropertyDefinition("PasRecord", PropertyDefinitionFlags.None);

		// Token: 0x0400088A RID: 2186
		public static readonly MServPropertyDefinition MservSecondaryRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservSecondaryRecord", PropertyDefinitionFlags.None);

		// Token: 0x0400088B RID: 2187
		public static readonly MServPropertyDefinition MservSoftDeletedCalendarRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservSoftDeletedCalendarRecord", PropertyDefinitionFlags.None);

		// Token: 0x0400088C RID: 2188
		public static readonly MServPropertyDefinition MservAggregatedGuidsRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservAggregatedGuidsRecord", PropertyDefinitionFlags.None);

		// Token: 0x0400088D RID: 2189
		public static readonly MServPropertyDefinition MservEmailAddressesRecord = MServPropertyDefinition.RawRecordPropertyDefinition("MservEmailAddressesRecord", PropertyDefinitionFlags.MultiValued);

		// Token: 0x0400088E RID: 2190
		public static readonly MServPropertyDefinition Puid = new MServPropertyDefinition("Puid", typeof(ulong), PropertyDefinitionFlags.TaskPopulated, 0UL, SimpleProviderPropertyDefinition.None, null, null);

		// Token: 0x0400088F RID: 2191
		public static readonly MServPropertyDefinition Id = new MServPropertyDefinition("Id", typeof(ADObjectId), PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.ObjectIdGetter), new SetterDelegate(MServRecipientSchema.ObjectIdSetter));

		// Token: 0x04000890 RID: 2192
		public static readonly MServPropertyDefinition Name = new MServPropertyDefinition("Name", typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.NameGetter), null);

		// Token: 0x04000891 RID: 2193
		public static readonly MServPropertyDefinition CommonName = new MServPropertyDefinition("CommonName", typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.NameGetter), null);

		// Token: 0x04000892 RID: 2194
		public static readonly MServPropertyDefinition DisplayName = new MServPropertyDefinition("DisplayName", typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.NameGetter), null);

		// Token: 0x04000893 RID: 2195
		public static readonly MServPropertyDefinition Alias = new MServPropertyDefinition("Alias", typeof(string), PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.AliasGetter), new SetterDelegate(MServRecipientSchema.AliasSetter));

		// Token: 0x04000894 RID: 2196
		public static readonly MServPropertyDefinition CanonicalName = new MServPropertyDefinition("CanonicalName", typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.NameGetter), null);

		// Token: 0x04000895 RID: 2197
		public static readonly MServPropertyDefinition DistinguishedName = new MServPropertyDefinition("DistinguishedName", typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.DistinguishedNameGetter), null);

		// Token: 0x04000896 RID: 2198
		public static readonly MServPropertyDefinition Guid = new MServPropertyDefinition("Guid", typeof(Guid), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, System.Guid.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.GuidGetter), null);

		// Token: 0x04000897 RID: 2199
		public static readonly MServPropertyDefinition ExchangeObjectId = new MServPropertyDefinition("ExchangeObjectId", typeof(Guid), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, System.Guid.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.GuidGetter), null);

		// Token: 0x04000898 RID: 2200
		public static readonly MServPropertyDefinition CorrelationId = new MServPropertyDefinition("CorrelationId", typeof(Guid), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, System.Guid.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.GuidGetter), null);

		// Token: 0x04000899 RID: 2201
		public static readonly MServPropertyDefinition NetID = new MServPropertyDefinition("NetID", typeof(NetID), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.NetIdGetter), null);

		// Token: 0x0400089A RID: 2202
		public static readonly MServPropertyDefinition ExchangeGuid = new MServPropertyDefinition("ExchangeGuid", typeof(Guid), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, System.Guid.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.ExchangeGuidGetter), null);

		// Token: 0x0400089B RID: 2203
		public static readonly MServPropertyDefinition Sid = new MServPropertyDefinition("Sid", typeof(SecurityIdentifier), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.SidGetter), null);

		// Token: 0x0400089C RID: 2204
		public static readonly MServPropertyDefinition LegacyExchangeDN = new MServPropertyDefinition("LegacyExchangeDN", typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.Puid
		}, new GetterDelegate(MServRecipientSchema.LegacyExchangeDNGetter), null);

		// Token: 0x0400089D RID: 2205
		public static readonly MServPropertyDefinition Database = new MServPropertyDefinition("Database", typeof(ADObjectId), PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, new GetterDelegate(MServRecipientSchema.DatabaseGetter), new SetterDelegate(MServRecipientSchema.DatabaseSetter));

		// Token: 0x0400089E RID: 2206
		public static readonly MServPropertyDefinition IsDeleted = new MServPropertyDefinition("IsDeleted", typeof(bool), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, false, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservSoftDeletedPrimaryRecord,
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, new GetterDelegate(MServRecipientSchema.IsDeletedGetter), null);

		// Token: 0x0400089F RID: 2207
		public static readonly MServPropertyDefinition EmailAddresses = new MServPropertyDefinition("EmailAddresses", typeof(ProxyAddress), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservEmailAddressesRecord
		}, new GetterDelegate(MServRecipientSchema.EmailAddressesGetter), new SetterDelegate(MServRecipientSchema.EmailAddressesSetter));

		// Token: 0x040008A0 RID: 2208
		public static readonly MServPropertyDefinition SatchmoClusterIp = new MServPropertyDefinition("SatchmoClusterIp", typeof(IPAddress), PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, new GetterDelegate(MServRecipientSchema.SatchmoClusterIpGetter), new SetterDelegate(MServRecipientSchema.SatchmoClusterIpSetter));

		// Token: 0x040008A1 RID: 2209
		public static readonly MServPropertyDefinition SatchmoDGroup = new MServPropertyDefinition("SatchmoDGroup", typeof(string), PropertyDefinitionFlags.Calculated, string.Empty, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, new GetterDelegate(MServRecipientSchema.SatchmoDGroupGetter), new SetterDelegate(MServRecipientSchema.SatchmoDGroupSetter));

		// Token: 0x040008A2 RID: 2210
		public static readonly MServPropertyDefinition PrimaryMailboxSource = new MServPropertyDefinition("PrimaryMailboxSource", typeof(PrimaryMailboxSourceType), PropertyDefinitionFlags.Calculated, PrimaryMailboxSourceType.None, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, new GetterDelegate(MServRecipientSchema.PrimaryMailboxSourceGetter), new SetterDelegate(MServRecipientSchema.PrimaryMailboxSourceSetter));

		// Token: 0x040008A3 RID: 2211
		public static readonly MServPropertyDefinition FblEnabled = new MServPropertyDefinition("FblEnabled", typeof(bool), PropertyDefinitionFlags.Calculated, false, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, MServRecipientSchema.MservFlagGetterDelegate(4, MServRecipientSchema.FblEnabled), MServRecipientSchema.MservFlagSetterDelegate(4, MServRecipientSchema.FblEnabled));

		// Token: 0x040008A4 RID: 2212
		public static readonly MServPropertyDefinition RecipientDisplayType = new MServPropertyDefinition("RecipientDisplayType", typeof(RecipientDisplayType?), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, new MServPropertyDefinition[]
		{
			MServRecipientSchema.MservPrimaryRecord,
			MServRecipientSchema.MservSecondaryRecord
		}, new GetterDelegate(MServRecipientSchema.RecipientDisplayTypeGetter), null);
	}
}
