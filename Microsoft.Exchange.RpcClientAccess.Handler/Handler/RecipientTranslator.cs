using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000017 RID: 23
	internal class RecipientTranslator
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000070A0 File Offset: 0x000052A0
		public RecipientTranslator(ICoreItem coreItem, PropertyTag[] extraUnicodePropertyTags, Encoding string8Encoding)
		{
			this.coreItem = coreItem;
			this.extraUnicodePropertyTags = extraUnicodePropertyTags;
			this.string8Encoding = string8Encoding;
			this.extraUnicodePropertyDefinitions = MEDSPropertyTranslator.GetPropertyDefinitionsIgnoreTypeChecking(coreItem.Session, coreItem.PropertyBag, extraUnicodePropertyTags);
			this.extraPropertyDefinitions = this.GetExtraPropertyDefinitions();
			bool useUnicodeType = true;
			this.extraPropertyTags = ((this.extraPropertyDefinitions.Length > 0) ? MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<NativeStorePropertyDefinition>(coreItem.Session, this.extraPropertyDefinitions, useUnicodeType).ToArray<PropertyTag>() : Array<PropertyTag>.Empty);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007120 File Offset: 0x00005320
		private static RecipientAddress GetRecipientAddress(ICorePropertyBag propertyBag)
		{
			RecipientAddress recipientAddress = null;
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(RecipientSchema.EmailAddrType);
			RecipientAddressType recipientAddressType = RecipientTranslator.GetRecipientAddressType(valueOrDefault);
			RecipientAddressType recipientAddressType2 = recipientAddressType;
			switch (recipientAddressType2)
			{
			case RecipientAddressType.None:
			case RecipientAddressType.MicrosoftMail:
			case RecipientAddressType.Smtp:
			case RecipientAddressType.Fax:
			case RecipientAddressType.ProfessionalOfficeSystem:
				recipientAddress = new RecipientAddress.EmptyRecipientAddress(recipientAddressType);
				break;
			case RecipientAddressType.Exchange:
			{
				RecipientDisplayType recipientDisplayType = (RecipientDisplayType)propertyBag.GetValueOrDefault<int>(RecipientSchema.DisplayType);
				string empty = string.Empty;
				string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(RecipientSchema.EmailAddress);
				if (valueOrDefault2 != null)
				{
					recipientAddress = new RecipientAddress.ExchangeRecipientAddress(recipientAddressType, recipientDisplayType, empty, valueOrDefault2);
				}
				break;
			}
			case RecipientAddressType.MapiPrivateDistributionList:
			case RecipientAddressType.DosPrivateDistributionList:
			{
				byte[] valueOrDefault3 = propertyBag.GetValueOrDefault<byte[]>(RecipientSchema.EntryId, Array<byte>.Empty);
				byte[] valueOrDefault4 = propertyBag.GetValueOrDefault<byte[]>(RecipientSchema.SearchKey, Array<byte>.Empty);
				recipientAddress = new RecipientAddress.DistributionListRecipientAddress(recipientAddressType, valueOrDefault3, valueOrDefault4);
				break;
			}
			default:
				if (recipientAddressType2 == RecipientAddressType.Other)
				{
					recipientAddress = new RecipientAddress.OtherRecipientAddress(recipientAddressType, valueOrDefault);
				}
				break;
			}
			if (recipientAddress == null)
			{
				recipientAddress = new RecipientAddress.EmptyRecipientAddress(RecipientAddressType.None);
			}
			return recipientAddress;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000071F8 File Offset: 0x000053F8
		private static RecipientAddressType GetRecipientAddressType(string addressType)
		{
			switch (addressType)
			{
			case "MAPIPDL":
				return RecipientAddressType.MapiPrivateDistributionList;
			case "DOSPDL":
				return RecipientAddressType.DosPrivateDistributionList;
			case "EX":
				return RecipientAddressType.Exchange;
			case "MS":
				return RecipientAddressType.MicrosoftMail;
			case "SMTP":
				return RecipientAddressType.Smtp;
			case "FAX":
				return RecipientAddressType.Fax;
			case "PROFS":
				return RecipientAddressType.ProfessionalOfficeSystem;
			case null:
				break;
			default:
				return RecipientAddressType.Other;
				break;
			}
			return RecipientAddressType.None;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000072C4 File Offset: 0x000054C4
		private static RecipientFlags GetRecipientFlags(ICorePropertyBag propertyBag)
		{
			RecipientFlags recipientFlags = RecipientFlags.None;
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(RecipientSchema.EmailDisplayName);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(RecipientSchema.TransmittableDisplayName);
			if (propertyBag.GetValueOrDefault<bool>(RecipientSchema.Responsibility))
			{
				recipientFlags |= RecipientFlags.Responsibility;
			}
			if (!propertyBag.GetValueOrDefault<bool>(RecipientSchema.SendRichInfo))
			{
				recipientFlags |= RecipientFlags.SendNoRichInformation;
			}
			if (valueOrDefault == valueOrDefault2)
			{
				recipientFlags |= RecipientFlags.TransmitSameAsDisplayName;
			}
			return recipientFlags;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007327 File Offset: 0x00005527
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000732F File Offset: 0x0000552F
		public PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
			set
			{
				Util.ThrowOnNullArgument(value, "value");
				this.VerifyNewExtraPropertyTags(value);
				if (value.Length > this.extraPropertyTags.Length)
				{
					this.extraPropertyDefinitions = this.GetPropertyDefinitions(value);
				}
				this.extraPropertyTags = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00007364 File Offset: 0x00005564
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000736C File Offset: 0x0000556C
		public PropertyTag[] ExtraUnicodePropertyTags
		{
			get
			{
				return this.extraUnicodePropertyTags;
			}
			set
			{
				this.extraUnicodePropertyTags = value;
				this.extraUnicodePropertyDefinitions = MEDSPropertyTranslator.GetPropertyDefinitionsIgnoreTypeChecking(this.coreItem.Session, this.coreItem.PropertyBag, this.extraUnicodePropertyTags);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007558 File Offset: 0x00005758
		public IEnumerable<RecipientRow> GetRecipientRows(int startRowId)
		{
			foreach (CoreRecipient recipient in this.coreItem.Recipients)
			{
				if (recipient.RowId >= startRowId)
				{
					yield return this.GetRecipientRow(recipient);
				}
			}
			yield break;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000757C File Offset: 0x0000577C
		public void SetRecipientRow(RecipientRow recipientRow)
		{
			int recipientRowId = (int)recipientRow.RecipientRowId;
			if (recipientRow.IsEmpty)
			{
				this.coreItem.Recipients.Remove(recipientRowId);
				return;
			}
			if (!this.TryAddCoreRecipient(recipientRowId, this.TranslateRecipientRow(recipientRow)))
			{
				throw RecipientTranslator.InvalidRecipient(recipientRow, "Cannot persist a recipient that lacks key properties", new object[0]);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000075CC File Offset: 0x000057CC
		public bool TryStubRecipient(RecipientRow recipientRow)
		{
			List<KeyValuePair<PropertyDefinition, object>> properties;
			return RecipientTranslator.TryCreateRecipientStub(recipientRow, out properties) && this.TryAddCoreRecipient((int)recipientRow.RecipientRowId, properties);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000075F2 File Offset: 0x000057F2
		public void RemoveAllRecipients()
		{
			this.coreItem.Recipients.Clear();
			this.extraPropertyTags = Array<PropertyTag>.Empty;
			this.extraPropertyDefinitions = Array<NativeStorePropertyDefinition>.Empty;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000761A File Offset: 0x0000581A
		private IStorageObjectProperties StorageObjectProperties
		{
			get
			{
				return new CoreObjectProperties(this.coreItem.PropertyBag);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000762C File Offset: 0x0000582C
		private static bool TryCreateRecipientStub(RecipientRow invalidRecipientRow, out List<KeyValuePair<PropertyDefinition, object>> properties)
		{
			if (!string.IsNullOrEmpty(invalidRecipientRow.DisplayName) || !string.IsNullOrEmpty(invalidRecipientRow.EmailAddress))
			{
				properties = new List<KeyValuePair<PropertyDefinition, object>>
				{
					new KeyValuePair<PropertyDefinition, object>(RecipientSchema.RecipientType, (RecipientItemType)(EnumValidator.IsValidValue<RecipientType>(invalidRecipientRow.RecipientType) ? invalidRecipientRow.RecipientType : RecipientType.To)),
					new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailDisplayName, (!string.IsNullOrEmpty(invalidRecipientRow.DisplayName)) ? invalidRecipientRow.DisplayName : invalidRecipientRow.EmailAddress),
					new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailAddress, (!string.IsNullOrEmpty(invalidRecipientRow.EmailAddress)) ? invalidRecipientRow.EmailAddress : invalidRecipientRow.DisplayName),
					new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailAddrType, "INVALID")
				};
				if ((ushort)(invalidRecipientRow.Flags & RecipientFlags.Responsibility) == 128)
				{
					properties.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.Responsibility, true));
				}
				return true;
			}
			properties = null;
			return false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007728 File Offset: 0x00005928
		private static Exception InvalidRecipient(RecipientRow invalidRecipientRow, string errorMessage, params object[] args)
		{
			List<KeyValuePair<PropertyDefinition, object>> list;
			string str = RecipientTranslator.TryCreateRecipientStub(invalidRecipientRow, out list) ? ". Invalid recipient will be replaced with a record requiring further resolution." : ". Invalid recipient will be skipped.";
			return new RecipientTranslationException(string.Format(errorMessage, args) + str);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007760 File Offset: 0x00005960
		private static RecipientType PackRecipientType(int mapiRecipientType)
		{
			if ((mapiRecipientType & 268435440) == 0)
			{
				RecipientType recipientType = (RecipientType)(mapiRecipientType >> 24 | (mapiRecipientType & 15));
				if (EnumValidator.IsValidValue<RecipientType>(recipientType))
				{
					return recipientType;
				}
			}
			return RecipientType.To;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000778C File Offset: 0x0000598C
		private static int UnpackRecipientType(RecipientType recipientType)
		{
			if (EnumValidator.IsValidValue<RecipientType>(recipientType))
			{
				return (int)((int)(recipientType & (RecipientType)240) << 24 | (recipientType & (RecipientType)15));
			}
			return 1;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000077A8 File Offset: 0x000059A8
		private NativeStorePropertyDefinition[] GetExtraPropertyDefinitions()
		{
			HashSet<NativeStorePropertyDefinition> hashSet = new HashSet<NativeStorePropertyDefinition>();
			foreach (CoreRecipient coreRecipient in this.coreItem.Recipients)
			{
				hashSet.UnionWith(coreRecipient.PropertyBag.AllFoundProperties.Cast<NativeStorePropertyDefinition>());
			}
			hashSet.ExceptWith(MEDSPropertyTranslator.GetPropertyDefinitionsIgnoreTypeChecking(this.coreItem.Session, this.coreItem.PropertyBag, PropertyTag.StandardRecipientPropertyTags));
			return hashSet.ToArray<NativeStorePropertyDefinition>();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000783C File Offset: 0x00005A3C
		private PropertyValue[] GetPropertyValues(CoreRecipient recipient, PropertyDefinition[] propertyDefinitions, PropertyTag[] propertyTags)
		{
			ICorePropertyBag propertyBag = recipient.PropertyBag;
			PropertyValue[] array = new PropertyValue[propertyTags.Length];
			for (int i = 0; i < propertyTags.Length; i++)
			{
				bool useUnicodeForRestrictions = true;
				array[i] = MEDSPropertyTranslator.TranslatePropertyValue(this.coreItem.Session, propertyTags[i], propertyBag.TryGetProperty(propertyDefinitions[i]), useUnicodeForRestrictions);
			}
			return array;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000789C File Offset: 0x00005A9C
		private RecipientRow GetRecipientRow(CoreRecipient recipient)
		{
			ICorePropertyBag propertyBag = recipient.PropertyBag;
			uint rowId = (uint)recipient.RowId;
			RecipientType recipientType = RecipientTranslator.PackRecipientType(propertyBag.GetValueOrDefault<int>(RecipientSchema.RecipientType));
			RecipientAddress recipientAddress = RecipientTranslator.GetRecipientAddress(propertyBag);
			RecipientFlags recipientFlags = RecipientTranslator.GetRecipientFlags(propertyBag);
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(RecipientSchema.EmailDisplayName);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(RecipientSchema.DisplayName7Bit);
			string emailAddress = null;
			string transmittableDisplayName = null;
			if (!(recipientAddress is RecipientAddress.ExchangeRecipientAddress))
			{
				emailAddress = propertyBag.GetValueOrDefault<string>(RecipientSchema.EmailAddress);
			}
			if ((ushort)(recipientFlags & RecipientFlags.TransmitSameAsDisplayName) != 64)
			{
				transmittableDisplayName = propertyBag.GetValueOrDefault<string>(RecipientSchema.TransmittableDisplayName);
			}
			return new RecipientRow(rowId, recipientType, this.string8Encoding, recipientAddress, recipientFlags, true, emailAddress, valueOrDefault, valueOrDefault2, transmittableDisplayName, this.GetPropertyValues(recipient, this.extraPropertyDefinitions, this.extraPropertyTags), this.GetPropertyValues(recipient, this.extraUnicodePropertyDefinitions, this.extraUnicodePropertyTags));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007B78 File Offset: 0x00005D78
		private IEnumerable<KeyValuePair<PropertyDefinition, object>> TranslateExtraProperties(PropertyValue[] propertyValues, NativeStorePropertyDefinition[] propertyDefinitions)
		{
			Util.ThrowOnNullArgument(propertyValues, "propertyValues");
			Util.ThrowOnNullArgument(propertyDefinitions, "propertyDefinitions");
			PropertyConverter.Recipient.ConvertPropertyValuesFromClient(this.coreItem.Session, this.StorageObjectProperties, propertyValues);
			ushort index = 0;
			while ((int)index < propertyValues.Length)
			{
				NativeStorePropertyDefinition propertyDefinition = propertyDefinitions[(int)index];
				if (propertyDefinition != null && !propertyValues[(int)index].IsNotFound)
				{
					object value = MEDSPropertyTranslator.TranslatePropertyValue(this.coreItem.Session, propertyValues[(int)index]);
					if (propertyDefinition.Validate((this.coreItem.Session == null) ? null : this.coreItem.Session.OperationContext, value).Length == 0)
					{
						yield return new KeyValuePair<PropertyDefinition, object>(propertyDefinition, value);
					}
				}
				index += 1;
			}
			yield break;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007BA4 File Offset: 0x00005DA4
		private NativeStorePropertyDefinition[] GetPropertyDefinitions(PropertyValue[] propertyValues)
		{
			Util.ThrowOnNullArgument(propertyValues, "propertyValues");
			PropertyTag[] array = new PropertyTag[propertyValues.Length];
			for (int i = 0; i < propertyValues.Length; i++)
			{
				array[i] = propertyValues[i].PropertyTag;
			}
			return this.GetPropertyDefinitions(array);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007BF4 File Offset: 0x00005DF4
		private NativeStorePropertyDefinition[] GetPropertyDefinitions(PropertyTag[] propertyTags)
		{
			Util.ThrowOnNullArgument(propertyTags, "propertyTags");
			NativeStorePropertyDefinition[] result;
			MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(this.coreItem.Session, this.coreItem.PropertyBag, propertyTags, out result);
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007C2C File Offset: 0x00005E2C
		private void VerifyNewExtraPropertyTags(PropertyTag[] propertyTags)
		{
			if (propertyTags.Length < this.ExtraPropertyTags.Length)
			{
				throw new RopExecutionException(string.Format("The client supplied less Extra property tags, than was set by the last OpenMessage/ReloadCachedInfo/FlushRecipients operation: {0}, expected at least {1}", propertyTags.Length, this.ExtraPropertyTags.Length), ErrorCode.RpcFormat);
			}
			for (int i = 0; i < this.ExtraPropertyTags.Length; i++)
			{
				if (propertyTags[i] != this.ExtraPropertyTags[i])
				{
					throw new RopExecutionException(string.Format("Invalid Extra PropertyTag at position {0}: {1}. Expected: {2}", i, propertyTags[i], this.ExtraPropertyTags[i]), ErrorCode.RpcFormat);
				}
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007CEC File Offset: 0x00005EEC
		private List<KeyValuePair<PropertyDefinition, object>> TranslateRecipientRow(RecipientRow recipientRow)
		{
			List<KeyValuePair<PropertyDefinition, object>> list = new List<KeyValuePair<PropertyDefinition, object>>();
			list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.RecipientType, RecipientTranslator.UnpackRecipientType(recipientRow.RecipientType)));
			list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.SendRichInfo, (ushort)(recipientRow.Flags & RecipientFlags.SendNoRichInformation) != 256));
			if (recipientRow.DisplayName != null)
			{
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailDisplayName, recipientRow.DisplayName));
				if ((ushort)(recipientRow.Flags & RecipientFlags.TransmitSameAsDisplayName) == 64)
				{
					list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.TransmittableDisplayName, recipientRow.DisplayName));
				}
			}
			if (recipientRow.SimpleDisplayName != null)
			{
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.DisplayName7Bit, recipientRow.SimpleDisplayName));
			}
			if (recipientRow.TransmittableDisplayName != null)
			{
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.TransmittableDisplayName, recipientRow.TransmittableDisplayName));
			}
			RecipientAddress.ExchangeRecipientAddress exchangeRecipientAddress = recipientRow.RecipientAddress as RecipientAddress.ExchangeRecipientAddress;
			RecipientAddress.DistributionListRecipientAddress distributionListRecipientAddress = recipientRow.RecipientAddress as RecipientAddress.DistributionListRecipientAddress;
			RecipientAddress.OtherRecipientAddress otherRecipientAddress = recipientRow.RecipientAddress as RecipientAddress.OtherRecipientAddress;
			RecipientAddress.EmptyRecipientAddress emptyRecipientAddress = recipientRow.RecipientAddress as RecipientAddress.EmptyRecipientAddress;
			string text = null;
			if (exchangeRecipientAddress != null)
			{
				text = "EX";
				RecipientDisplayType recipientDisplayType = exchangeRecipientAddress.RecipientDisplayType;
				switch (recipientDisplayType)
				{
				case RecipientDisplayType.DistributionList:
				case RecipientDisplayType.Agent:
					break;
				case RecipientDisplayType.Forum:
					goto IL_150;
				default:
					if (recipientDisplayType != RecipientDisplayType.RemoteMailUser)
					{
						goto IL_150;
					}
					break;
				}
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.DisplayType, (int)exchangeRecipientAddress.RecipientDisplayType));
				goto IL_166;
				IL_150:
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.DisplayType, 0));
				IL_166:
				string value;
				if (!exchangeRecipientAddress.TryGetFullAddress(string.Empty, out value))
				{
					throw RecipientTranslator.InvalidRecipient(recipientRow, "Client access misconfiguration. The client should be restarted and/or reconfigured. Details: the client is using stale DNPrefix data, possibly obtained by communicating to a different server.", new object[0]);
				}
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailAddress, value));
			}
			else if (distributionListRecipientAddress != null)
			{
				text = ((distributionListRecipientAddress.RecipientAddressType == RecipientAddressType.MapiPrivateDistributionList) ? "MAPIPDL" : "DOSPDL");
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EntryId, distributionListRecipientAddress.EntryId));
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.SearchKey, distributionListRecipientAddress.SearchKey));
			}
			else if (otherRecipientAddress != null)
			{
				if (string.IsNullOrEmpty(otherRecipientAddress.AddressType) || string.IsNullOrEmpty(recipientRow.EmailAddress))
				{
					throw RecipientTranslator.InvalidRecipient(recipientRow, "Recipient of type=other should have both address type ({0}) and email address ({1})", new object[]
					{
						otherRecipientAddress.AddressType,
						recipientRow.EmailAddress
					});
				}
				text = otherRecipientAddress.AddressType;
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailAddress, recipientRow.EmailAddress));
			}
			else
			{
				if (emptyRecipientAddress == null)
				{
					throw new ArgumentException(string.Format("Recipient data of type {0} is not supported", (recipientRow.RecipientAddress != null) ? recipientRow.RecipientAddress.GetType().FullName : null));
				}
				if (string.IsNullOrEmpty(recipientRow.EmailAddress) != (emptyRecipientAddress.RecipientAddressType == RecipientAddressType.None))
				{
					throw RecipientTranslator.InvalidRecipient(recipientRow, "Recipients of type=empty must have an empty address ({0}) IFF the address type ({1}) is None", new object[]
					{
						recipientRow.EmailAddress,
						emptyRecipientAddress.RecipientAddressType
					});
				}
				switch (emptyRecipientAddress.RecipientAddressType)
				{
				case RecipientAddressType.None:
					goto IL_315;
				case RecipientAddressType.MicrosoftMail:
					text = "MS";
					goto IL_315;
				case RecipientAddressType.Smtp:
					text = "SMTP";
					goto IL_315;
				case RecipientAddressType.Fax:
					text = "FAX";
					goto IL_315;
				case RecipientAddressType.ProfessionalOfficeSystem:
					text = "PROFS";
					goto IL_315;
				}
				throw RecipientTranslator.InvalidRecipient(recipientRow, "Address type \"{0}\" is not supported for recipients of type=empty", new object[]
				{
					emptyRecipientAddress.RecipientAddressType
				});
				IL_315:
				if (!string.IsNullOrEmpty(recipientRow.EmailAddress))
				{
					list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailAddress, recipientRow.EmailAddress));
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.EmailAddrType, text));
			}
			bool flag = (ushort)(recipientRow.Flags & RecipientFlags.Responsibility) == 128;
			if (!flag)
			{
				flag = this.coreItem.Session.SupportedRoutingTypes.Contains(text);
			}
			if (flag)
			{
				list.Add(new KeyValuePair<PropertyDefinition, object>(RecipientSchema.Responsibility, flag));
			}
			list.AddRange(this.TranslateExtraProperties(recipientRow.ExtraPropertyValues, this.extraPropertyDefinitions));
			list.AddRange(this.TranslateExtraProperties(recipientRow.ExtraUnicodePropertyValues, this.GetPropertyDefinitions(recipientRow.ExtraUnicodePropertyValues)));
			return list;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008100 File Offset: 0x00006300
		private bool TryAddCoreRecipient(int rowId, List<KeyValuePair<PropertyDefinition, object>> properties)
		{
			CoreRecipient coreRecipient = this.coreItem.Recipients.CreateOrReplace(rowId);
			ICorePropertyBag propertyBag = coreRecipient.PropertyBag;
			bool flag = false;
			try
			{
				foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in properties)
				{
					propertyBag[keyValuePair.Key] = keyValuePair.Value;
				}
				flag = coreRecipient.TryValidateRecipient();
			}
			catch (CorruptDataException innerException)
			{
				throw new RecipientTranslationException("One or more recipient properties failed validation", innerException);
			}
			finally
			{
				if (!flag)
				{
					this.coreItem.Recipients.Remove(rowId);
				}
			}
			return flag;
		}

		// Token: 0x0400005F RID: 95
		private const string RecipientStubAddressType = "INVALID";

		// Token: 0x04000060 RID: 96
		private readonly ICoreItem coreItem;

		// Token: 0x04000061 RID: 97
		private readonly Encoding string8Encoding;

		// Token: 0x04000062 RID: 98
		private PropertyTag[] extraPropertyTags;

		// Token: 0x04000063 RID: 99
		private PropertyTag[] extraUnicodePropertyTags;

		// Token: 0x04000064 RID: 100
		private NativeStorePropertyDefinition[] extraPropertyDefinitions;

		// Token: 0x04000065 RID: 101
		private NativeStorePropertyDefinition[] extraUnicodePropertyDefinitions;

		// Token: 0x02000018 RID: 24
		private static class AddressTypes
		{
			// Token: 0x04000066 RID: 102
			public const string DOSPDL = "DOSPDL";

			// Token: 0x04000067 RID: 103
			public const string EX = "EX";

			// Token: 0x04000068 RID: 104
			public const string FAX = "FAX";

			// Token: 0x04000069 RID: 105
			public const string MAPIPDL = "MAPIPDL";

			// Token: 0x0400006A RID: 106
			public const string MS = "MS";

			// Token: 0x0400006B RID: 107
			public const string PROFS = "PROFS";

			// Token: 0x0400006C RID: 108
			public const string SMTP = "SMTP";
		}
	}
}
