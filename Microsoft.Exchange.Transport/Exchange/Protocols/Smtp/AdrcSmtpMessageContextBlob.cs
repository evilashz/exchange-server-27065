using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F3 RID: 1011
	internal class AdrcSmtpMessageContextBlob : SmtpMessageContextBlob
	{
		// Token: 0x06002E38 RID: 11832 RVA: 0x000B9ED0 File Offset: 0x000B80D0
		public static bool IsSupportedVersion(string ehloAdvertisement, bool isSmtpReceive, out Version version)
		{
			version = null;
			if (ehloAdvertisement.Equals(AdrcSmtpMessageContextBlob.VersionString, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			Match match = AdrcSmtpMessageContextBlob.VersionRegex.Match(ehloAdvertisement);
			if (!match.Success)
			{
				return false;
			}
			version = new Version(int.Parse(match.Groups["Major"].Value), int.Parse(match.Groups["Minor"].Value), int.Parse(match.Groups["Build"].Value), int.Parse(match.Groups["Revision"].Value));
			if (AdrcSmtpMessageContextBlob.Version.Major == version.Major)
			{
				if (AdrcSmtpMessageContextBlob.Version.Minor == version.Minor)
				{
					return true;
				}
				if (AdrcSmtpMessageContextBlob.Version.Minor > version.Minor && !isSmtpReceive)
				{
					return true;
				}
				if (AdrcSmtpMessageContextBlob.Version.Minor < version.Minor && isSmtpReceive)
				{
					return true;
				}
			}
			version = null;
			return false;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000B9FD1 File Offset: 0x000B81D1
		public AdrcSmtpMessageContextBlob(bool acceptFromSmptIn, bool sendToSmtpOut, ProcessTransportRole role) : base(acceptFromSmptIn, sendToSmtpOut, role)
		{
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000B9FDC File Offset: 0x000B81DC
		public override string Name
		{
			get
			{
				return AdrcSmtpMessageContextBlob.VersionString;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000B9FE3 File Offset: 0x000B81E3
		public override ByteQuantifiedSize MaxBlobSize
		{
			get
			{
				return Components.TransportAppConfig.MessageContextBlobConfiguration.AdrcCacheMaxBlobSize;
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000B9FF4 File Offset: 0x000B81F4
		public override bool IsAdvertised(IEhloOptions ehloOptions)
		{
			return ehloOptions.XAdrc;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000B9FFC File Offset: 0x000B81FC
		public override void DeserializeBlob(Stream stream, ISmtpInSession smtpInSession, long blobSize)
		{
			ArgumentValidator.ThrowIfNull("stream", stream);
			ArgumentValidator.ThrowIfNull("smtpInSession", smtpInSession);
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DeserializeAdrcBlob);
			this.DeserializeBlobInternal(stream, smtpInSession.TransportMailItem, smtpInSession.LogSession, smtpInSession.Tracer);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000BA038 File Offset: 0x000B8238
		public override void DeserializeBlob(Stream stream, SmtpInSessionState sessionState, long blobSize)
		{
			ArgumentValidator.ThrowIfNull("stream", stream);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			this.DeserializeBlobInternal(stream, sessionState.TransportMailItem, sessionState.ProtocolLogSession, sessionState.Tracer);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000BA06C File Offset: 0x000B826C
		public override Stream SerializeBlob(SmtpOutSession smtpOutSession)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)smtpOutSession.GetHashCode(), "Serializing ADRecipient Cache");
			smtpOutSession.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SerializeAdrcPropertiesBlob);
			IADRecipientCache<TransportMiniRecipient> adrecipientCache = this.GetADRecipientCache(smtpOutSession);
			return this.Serialize(adrecipientCache, smtpOutSession, this.GetMailItemRecipients(smtpOutSession));
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000BA0AE File Offset: 0x000B82AE
		public override bool VerifyPermission(Permission permission)
		{
			return SmtpInSessionUtils.HasSMTPAcceptXMessageContextADRecipientCachePermission(permission);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000BA0B8 File Offset: 0x000B82B8
		public Stream Serialize(IADRecipientCache<TransportMiniRecipient> adRecipientCache, SmtpOutSession smtpOutSession, List<MailRecipient> mailRecipients)
		{
			MultiByteArrayMemoryStream multiByteArrayMemoryStream = new MultiByteArrayMemoryStream();
			HashSet<TransportMiniRecipient> hashSet;
			List<ProxyAddress> list;
			AdrcSmtpMessageContextBlob.GetCacheEntriesToSerialize(adRecipientCache, mailRecipients, smtpOutSession.ShouldReduceRecipientCacheForTransmission, out hashSet, out list);
			byte[] array = new byte[1024];
			int count = list.Count;
			base.SerializeValue(multiByteArrayMemoryStream, ref array, hashSet.Count);
			base.SerializeValue(multiByteArrayMemoryStream, ref array, count);
			this.SerializeOrganizationId(multiByteArrayMemoryStream, ref array, adRecipientCache.OrganizationId);
			multiByteArrayMemoryStream.Write(AdrcSmtpMessageContextBlob.recipientBlobSeparator, 0, AdrcSmtpMessageContextBlob.recipientBlobSeparator.Length);
			foreach (TransportMiniRecipient miniRecipient in hashSet)
			{
				this.Serialize(miniRecipient, ref array, multiByteArrayMemoryStream, smtpOutSession);
				multiByteArrayMemoryStream.Write(AdrcSmtpMessageContextBlob.recipientBlobSeparator, 0, AdrcSmtpMessageContextBlob.recipientBlobSeparator.Length);
			}
			foreach (ProxyAddress value in list)
			{
				base.SerializeValue(multiByteArrayMemoryStream, ref array, value);
				base.SerializeValue(multiByteArrayMemoryStream, ref array, AdrcSmtpMessageContextBlob.RecipientValidationError.RecipientNotFound);
			}
			if (smtpOutSession.LogSession != null)
			{
				smtpOutSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Transfering {0} resolved and {1} unresolved recipient(s)", new object[]
				{
					hashSet.Count,
					count
				});
			}
			SystemProbeHelper.SmtpSendTracer.TracePass<int, int>(smtpOutSession.RoutedMailItem, 0L, "Transfering {0} resolved and {1} unresolved recipient(s)", hashSet.Count, count);
			return multiByteArrayMemoryStream;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000BA244 File Offset: 0x000B8444
		private static OrganizationId GetOrganizationId(TransportPropertyStreamReader reader)
		{
			ADObjectId value = (ADObjectId)reader.ReadValue();
			ADObjectId value2 = (ADObjectId)reader.ReadValue();
			ADPropertyBag adpropertyBag = new ADPropertyBag();
			adpropertyBag[ADObjectSchema.ConfigurationUnit] = value;
			adpropertyBag[ADObjectSchema.OrganizationalUnitRoot] = value2;
			return (OrganizationId)ADObject.OrganizationIdGetter(adpropertyBag);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000BA294 File Offset: 0x000B8494
		private static void DeserializeNonNativeMvps(KeyValuePair<string, object> item, ADPropertyDefinition propertyDefinition, TransportMiniRecipient miniRecipient)
		{
			if (item.Value.GetType() == typeof(List<byte[]>))
			{
				Exception innerException = null;
				List<object> list = new List<object>();
				foreach (byte[] originalValue in ((List<byte[]>)item.Value))
				{
					innerException = null;
					object item2;
					if (!ADValueConvertor.TryConvertValueFromBinary(originalValue, propertyDefinition.Type, null, out item2, out innerException))
					{
						throw new FormatException(string.Format("Encountered error while deserializing property {0} with type {1}", propertyDefinition.Name, propertyDefinition.Type), innerException);
					}
					list.Add(item2);
				}
				miniRecipient.propertyBag.SetField(propertyDefinition, miniRecipient.propertyBag.CreateMultiValuedProperty(propertyDefinition, false, list, null, null));
				return;
			}
			if (item.Value.GetType() == typeof(List<string>))
			{
				Exception innerException2 = null;
				List<object> list2 = new List<object>();
				foreach (string originalValue2 in ((List<string>)item.Value))
				{
					innerException2 = null;
					object item3;
					if (!ADValueConvertor.TryConvertValueFromString(originalValue2, propertyDefinition.Type, null, out item3, out innerException2))
					{
						throw new FormatException(string.Format("Encountered error while deserializing property {0} with type {1}", propertyDefinition.Name, propertyDefinition.Type), innerException2);
					}
					list2.Add(item3);
				}
				miniRecipient.propertyBag.SetField(propertyDefinition, miniRecipient.propertyBag.CreateMultiValuedProperty(propertyDefinition, false, list2, null, null));
				return;
			}
			throw new InvalidOperationException("Unexpected type " + item.Value.GetType().ToString());
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000BA468 File Offset: 0x000B8668
		protected virtual void AddItemsToCache(List<TransportMiniRecipient> resolvedRecipients, List<ProxyAddress> notResolvedRecipients, OrganizationId organizationId, TransportMailItem transportMailItem, IProtocolLogSession protocolLogSession)
		{
			if (transportMailItem.ADRecipientCache == null)
			{
				transportMailItem.ADRecipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, resolvedRecipients.Count + notResolvedRecipients.Count, organizationId);
			}
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = transportMailItem.ADRecipientCache;
			foreach (TransportMiniRecipient transportMiniRecipient in resolvedRecipients)
			{
				foreach (ProxyAddress proxyAddress in transportMiniRecipient.EmailAddresses)
				{
					if (!adrecipientCache.ContainsKey(proxyAddress))
					{
						adrecipientCache.AddCacheEntry(proxyAddress, new Result<TransportMiniRecipient>(transportMiniRecipient, null));
					}
				}
			}
			foreach (ProxyAddress proxyAddress2 in notResolvedRecipients)
			{
				adrecipientCache.AddCacheEntry(proxyAddress2, new Result<TransportMiniRecipient>(null, ProviderError.NotFound));
			}
			protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Transferred {0} resolved and {1} unresolved recipient(s)", new object[]
			{
				resolvedRecipients.Count,
				notResolvedRecipients.Count
			});
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000BA5B4 File Offset: 0x000B87B4
		private static void CheckSeparator(Stream stream, byte[] separator, int i)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			if (separator[0] != array[0] || separator[1] != array[1])
			{
				throw new FormatException("Encountered error while deserializing recipient at position " + i);
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000BA5F8 File Offset: 0x000B87F8
		private static void PopulateCalculatedPropertiesAndSetDefaultValue(TransportMiniRecipient miniRecipient, HashSet<PropertyDefinition> propertiesDeserialized)
		{
			foreach (PropertyDefinition propertyDefinition in TransportMiniRecipientSchema.Schema.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (!AdrcSmtpMessageContextBlob.GetShadowProperties().Contains(adpropertyDefinition))
				{
					if (adpropertyDefinition.IsCalculated)
					{
						if (miniRecipient.HasSupportingProperties(adpropertyDefinition))
						{
							object obj = miniRecipient.propertyBag[adpropertyDefinition];
						}
					}
					else if (!propertiesDeserialized.Contains(adpropertyDefinition))
					{
						if (adpropertyDefinition.IsMultivalued)
						{
							object obj2 = miniRecipient.propertyBag[adpropertyDefinition];
						}
						else
						{
							miniRecipient.propertyBag.SetField(adpropertyDefinition, null);
						}
					}
				}
			}
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000BA6A8 File Offset: 0x000B88A8
		private static void GetCacheEntriesToSerialize(IADRecipientCache<TransportMiniRecipient> adRecipientCache, List<MailRecipient> mailRecipients, bool shouldReduceRecipientCacheForTransmission, out HashSet<TransportMiniRecipient> resolvedRecipientsToTransfer, out List<ProxyAddress> notResolvedRecipients)
		{
			resolvedRecipientsToTransfer = new HashSet<TransportMiniRecipient>();
			notResolvedRecipients = new List<ProxyAddress>();
			HashSet<ProxyAddress> hashSet = null;
			if (shouldReduceRecipientCacheForTransmission)
			{
				hashSet = new HashSet<ProxyAddress>();
				foreach (MailRecipient mailRecipient in mailRecipients)
				{
					hashSet.Add(new SmtpProxyAddress((string)mailRecipient.Email, false));
				}
			}
			foreach (ProxyAddress proxyAddress in adRecipientCache.ClonedKeys)
			{
				Result<TransportMiniRecipient> result;
				if (adRecipientCache.TryGetValue(proxyAddress, out result))
				{
					if (result.Data != null)
					{
						if (!shouldReduceRecipientCacheForTransmission || result.Data.IsSenderOrP2RecipientEntry || hashSet.Contains(proxyAddress))
						{
							resolvedRecipientsToTransfer.Add(result.Data);
						}
					}
					else if (result.Error == ProviderError.NotFound)
					{
						notResolvedRecipients.Add(proxyAddress);
					}
				}
			}
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000BA7B4 File Offset: 0x000B89B4
		private static HashSet<PropertyDefinition> GetShadowProperties()
		{
			if (AdrcSmtpMessageContextBlob.shadowProperties == null)
			{
				HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
				foreach (PropertyDefinition propertyDefinition in TransportMiniRecipientSchema.Schema.AllProperties)
				{
					ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
					if (adpropertyDefinition.ShadowProperty != null)
					{
						hashSet.Add(adpropertyDefinition.ShadowProperty);
					}
				}
				AdrcSmtpMessageContextBlob.shadowProperties = hashSet;
			}
			return AdrcSmtpMessageContextBlob.shadowProperties;
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000BA838 File Offset: 0x000B8A38
		private static bool TryConvertMvpToStringList(IEnumerable valueToSerialize, out List<string> stringList, out Exception error)
		{
			stringList = new List<string>();
			error = null;
			foreach (object originalValue in valueToSerialize)
			{
				string item;
				if (!ADValueConvertor.TryConvertValueToString(originalValue, null, out item, out error))
				{
					stringList = null;
					return false;
				}
				stringList.Add(item);
			}
			return true;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000BA8B0 File Offset: 0x000B8AB0
		private static bool TryConvertMvpToByteArrayList(IEnumerable valueToSerialize, out List<byte[]> byteArrayList, out Exception error)
		{
			byteArrayList = new List<byte[]>();
			error = null;
			foreach (object originalValue in valueToSerialize)
			{
				byte[] item;
				if (!ADValueConvertor.TryConvertValueToBinary(originalValue, null, out item, out error))
				{
					byteArrayList = null;
					return false;
				}
				byteArrayList.Add(item);
			}
			return true;
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000BA928 File Offset: 0x000B8B28
		private static Dictionary<string, ADPropertyDefinition> GetNameToPropertyMapping()
		{
			Dictionary<string, ADPropertyDefinition> dictionary = new Dictionary<string, ADPropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in TransportMiniRecipientSchema.Schema.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				TransportHelpers.AttemptAddToDictionary<string, ADPropertyDefinition>(dictionary, adpropertyDefinition.Name, adpropertyDefinition, null);
			}
			HashSet<ADPropertyDefinition> hashSet = new HashSet<ADPropertyDefinition>();
			foreach (ProviderPropertyDefinition providerPropertyDefinition in dictionary.Values)
			{
				if (providerPropertyDefinition.SupportingProperties != null && providerPropertyDefinition.SupportingProperties.Count != 0)
				{
					foreach (ProviderPropertyDefinition providerPropertyDefinition2 in providerPropertyDefinition.SupportingProperties)
					{
						ADPropertyDefinition adpropertyDefinition2 = (ADPropertyDefinition)providerPropertyDefinition2;
						if (!dictionary.ContainsKey(adpropertyDefinition2.Name))
						{
							hashSet.Add(adpropertyDefinition2);
						}
					}
				}
			}
			foreach (ADPropertyDefinition adpropertyDefinition3 in hashSet)
			{
				TransportHelpers.AttemptAddToDictionary<string, ADPropertyDefinition>(dictionary, adpropertyDefinition3.Name, adpropertyDefinition3, null);
			}
			return dictionary;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000BAA90 File Offset: 0x000B8C90
		private void DeserializeBlobInternal(Stream stream, TransportMailItem transportMailItem, IProtocolLogSession protocolLogSession, ITracer tracer)
		{
			TransportPropertyStreamReader transportPropertyStreamReader = new TransportPropertyStreamReader(stream);
			try
			{
				int num = base.ReadTypeAndValidate<int>(transportPropertyStreamReader);
				int num2 = base.ReadTypeAndValidate<int>(transportPropertyStreamReader);
				OrganizationId organizationId = AdrcSmtpMessageContextBlob.GetOrganizationId(transportPropertyStreamReader);
				AdrcSmtpMessageContextBlob.CheckSeparator(stream, AdrcSmtpMessageContextBlob.recipientBlobSeparator, -1);
				List<TransportMiniRecipient> list = new List<TransportMiniRecipient>(num);
				List<ProxyAddress> list2 = new List<ProxyAddress>(num2);
				for (int i = 0; i < num; i++)
				{
					int num3 = base.ReadTypeAndValidate<int>(transportPropertyStreamReader);
					long num4 = stream.Position + (long)num3;
					TransportMiniRecipient transportMiniRecipient = new TransportMiniRecipient();
					HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
					KeyValuePair<string, object> item;
					while (stream.Position < num4 && transportPropertyStreamReader.Read(out item))
					{
						if (string.IsNullOrEmpty(item.Key))
						{
							throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Encountered an unexpected error while deserializing entry at position {0} (Out of {1} expected).", new object[]
							{
								i + 1,
								num
							}));
						}
						ADPropertyDefinition adpropertyDefinition;
						if (!AdrcSmtpMessageContextBlob.nameToPropertyDefinitionMapping.TryGetValue(item.Key, out adpropertyDefinition))
						{
							tracer.TraceDebug<string>(0L, "Key {0} is not supported", item.Key);
						}
						else if (!adpropertyDefinition.IsFilterOnly)
						{
							object value;
							Exception ex;
							if (item.Value == null)
							{
								transportMiniRecipient.propertyBag.SetField(adpropertyDefinition, item.Value);
							}
							else if (base.IsNativelySupported(adpropertyDefinition))
							{
								if (adpropertyDefinition.IsMultivalued)
								{
									transportMiniRecipient.propertyBag.SetField(adpropertyDefinition, transportMiniRecipient.propertyBag.CreateMultiValuedProperty(adpropertyDefinition, false, (ICollection)item.Value, null, null));
								}
								else
								{
									transportMiniRecipient.propertyBag.SetField(adpropertyDefinition, item.Value);
								}
							}
							else if (adpropertyDefinition.IsMultivalued)
							{
								AdrcSmtpMessageContextBlob.DeserializeNonNativeMvps(item, adpropertyDefinition, transportMiniRecipient);
							}
							else if (item.Value.GetType() == typeof(byte[]) && ADValueConvertor.TryConvertValueFromBinary((byte[])item.Value, adpropertyDefinition.Type, null, out value, out ex))
							{
								transportMiniRecipient.propertyBag.SetField(adpropertyDefinition, value);
							}
							else
							{
								if (!(item.Value is string) || !ADValueConvertor.TryConvertValueFromString((string)item.Value, adpropertyDefinition.Type, null, out value, out ex))
								{
									throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Do not know how to convert property with type {0}", new object[]
									{
										item.Value.GetType()
									}));
								}
								transportMiniRecipient.propertyBag.SetField(adpropertyDefinition, value);
							}
							hashSet.Add(adpropertyDefinition);
						}
					}
					list.Add(transportMiniRecipient);
					AdrcSmtpMessageContextBlob.PopulateCalculatedPropertiesAndSetDefaultValue(transportMiniRecipient, hashSet);
					AdrcSmtpMessageContextBlob.CheckSeparator(stream, AdrcSmtpMessageContextBlob.recipientBlobSeparator, i);
				}
				for (int j = 0; j < num2; j++)
				{
					list2.Add((ProxyAddress)transportPropertyStreamReader.ReadValue());
					transportPropertyStreamReader.ReadValue();
				}
				this.AddItemsToCache(list, list2, organizationId, transportMailItem, protocolLogSession);
				SystemProbeHelper.SmtpReceiveTracer.TracePass<OrganizationId, List<TransportMiniRecipient>, List<ProxyAddress>>(transportMailItem, 0L, "Received ADRecipientCache information. OrgId={0}; Resolved={1}; UnResolved={2}", organizationId, list, list2);
			}
			catch (DataValidationException innerException)
			{
				throw new FormatException("AD Property validation failed", innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new FormatException("Encountered error while deserializing ADRC blob", innerException2);
			}
			catch (TransientException innerException3)
			{
				throw new FormatException("Encountered AD transient exception", innerException3);
			}
			catch (DataSourceOperationException innerException4)
			{
				throw new FormatException("Encountered AD permanent exception", innerException4);
			}
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000BAE2C File Offset: 0x000B902C
		private void SerializeOrganizationId(Stream ms, ref byte[] buffer, OrganizationId organizationId)
		{
			base.SerializeValue(ms, ref buffer, organizationId.ConfigurationUnit);
			base.SerializeValue(ms, ref buffer, organizationId.OrganizationalUnit);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000BAE4C File Offset: 0x000B904C
		private void Serialize(TransportMiniRecipient miniRecipient, ref byte[] buffer, Stream ms, SmtpOutSession smtpOutSession)
		{
			ArgumentValidator.ThrowIfNull("miniRecipient", miniRecipient);
			ArgumentValidator.ThrowIfNull("buffer", buffer);
			ArgumentValidator.ThrowIfNull("ms", ms);
			bool flag = false;
			long position = ms.Position;
			try
			{
				this.SerializeHelper(miniRecipient, ref buffer, ms, out flag, false);
			}
			catch (InvalidOperationException)
			{
				if (flag)
				{
					ExWatson.AddExtraData(this.GetExtraDataForCrashReport(smtpOutSession, miniRecipient));
					throw;
				}
				ms.Seek(position, SeekOrigin.Begin);
				this.SerializeHelper(miniRecipient, ref buffer, ms, out flag, true);
			}
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000BAED0 File Offset: 0x000B90D0
		protected virtual void SerializeHelper(TransportMiniRecipient miniRecipient, ref byte[] buffer, Stream ms, out bool customException, bool useClone = false)
		{
			ArgumentValidator.ThrowIfNull("miniRecipient", miniRecipient);
			ArgumentValidator.ThrowIfNull("buffer", buffer);
			ArgumentValidator.ThrowIfNull("ms", ms);
			long position = ms.Position;
			int num = TransportPropertyStreamWriter.SizeOf(StreamPropertyType.Int32, 0);
			customException = false;
			ms.Seek((long)num, SeekOrigin.Current);
			lock (miniRecipient)
			{
				PropertyBag transportMiniRecipientPropertyBag = this.GetTransportMiniRecipientPropertyBag(miniRecipient, useClone);
				foreach (KeyValuePair<ProviderPropertyDefinition, object> entry in transportMiniRecipientPropertyBag)
				{
					ExTraceGlobals.SmtpSendTracer.TraceFunction<bool, string, Type>(0L, "{0} {1} {2}", entry.Key.IsCalculated, entry.Key.Name, entry.Key.Type);
					if (!entry.Key.IsCalculated)
					{
						object valueToSerialize = this.GetValueToSerialize(entry, out customException);
						TypedValue typedValue = new TypedValue(StreamPropertyType.String, entry.Key.Name);
						StreamPropertyType propetyTypeForValue = base.GetPropetyTypeForValue(valueToSerialize);
						int num2 = TransportPropertyStreamWriter.SizeOf(typedValue.Type, typedValue.Value);
						num2 += TransportPropertyStreamWriter.SizeOf(propetyTypeForValue, valueToSerialize);
						buffer = base.IncreaseBufferIfSmall(num2, buffer);
						int count = 0;
						TransportPropertyStreamWriter.Serialize(typedValue.Type, typedValue.Value, buffer, ref count);
						TransportPropertyStreamWriter.Serialize(propetyTypeForValue, valueToSerialize, buffer, ref count);
						ms.Write(buffer, 0, count);
					}
				}
			}
			long position2 = ms.Position;
			ms.Seek(position, SeekOrigin.Begin);
			base.SerializeValue(ms, ref buffer, (int)(position2 - position - (long)num));
			ms.Seek(position2, SeekOrigin.Begin);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000BB0B0 File Offset: 0x000B92B0
		protected PropertyBag GetTransportMiniRecipientPropertyBag(TransportMiniRecipient transportMiniRecipient, bool useClone = false)
		{
			ArgumentValidator.ThrowIfNull("transportMiniRecipient", transportMiniRecipient);
			if (useClone)
			{
				return (PropertyBag)transportMiniRecipient.propertyBag.Clone();
			}
			return transportMiniRecipient.propertyBag;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000BB0D8 File Offset: 0x000B92D8
		protected virtual object GetValueToSerialize(KeyValuePair<ProviderPropertyDefinition, object> entry, out bool customException)
		{
			ArgumentValidator.ThrowIfNull("entry", entry);
			customException = false;
			object obj = entry.Value;
			byte[] array;
			Exception innerException2;
			if (entry.Value == null || base.IsNativelySupported((ADPropertyDefinition)entry.Key))
			{
				obj = entry.Value;
			}
			else if (entry.Key.IsMultivalued)
			{
				Exception innerException = null;
				IEnumerable enumerable = obj as IEnumerable;
				List<byte[]> list;
				if (enumerable != null && AdrcSmtpMessageContextBlob.TryConvertMvpToByteArrayList(enumerable, out list, out innerException))
				{
					obj = list;
				}
				else
				{
					List<string> list2;
					if (enumerable == null || !AdrcSmtpMessageContextBlob.TryConvertMvpToStringList(enumerable, out list2, out innerException))
					{
						customException = true;
						throw new InvalidOperationException(string.Format("Serialization is not supported for Multivalued property {0} with type {1}", entry.Key.Name, entry.Key.Type), innerException);
					}
					obj = list2;
				}
			}
			else if (ADValueConvertor.TryConvertValueToBinary(obj, null, out array, out innerException2))
			{
				obj = array;
			}
			else
			{
				string text;
				if (!ADValueConvertor.TryConvertValueToString(obj, null, out text, out innerException2))
				{
					customException = true;
					throw new InvalidOperationException(string.Format("Serialization is not supported for property {0} with type {1}", entry.Key.Name, entry.Key.Type), innerException2);
				}
				obj = text;
			}
			return obj;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000BB1F4 File Offset: 0x000B93F4
		private string GetExtraDataForCrashReport(SmtpOutSession smtpOutSession, TransportMiniRecipient miniRecipient)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (smtpOutSession != null && smtpOutSession.RoutedMailItem != null)
			{
				if (!string.IsNullOrEmpty(smtpOutSession.RoutedMailItem.InternetMessageId))
				{
					stringBuilder.AppendLine("Internet Message ID of Crashing message: " + smtpOutSession.RoutedMailItem.InternetMessageId);
				}
				stringBuilder.AppendLine("Network Message ID of Crashing message: " + smtpOutSession.RoutedMailItem.NetworkMessageId);
			}
			if (miniRecipient != null && miniRecipient.propertyBag != null)
			{
				stringBuilder.AppendLine("Transport Mini Recipient Property Bag Count: " + miniRecipient.propertyBag.Count);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000BB293 File Offset: 0x000B9493
		protected virtual IADRecipientCache<TransportMiniRecipient> GetADRecipientCache(SmtpOutSession smtpOutSession)
		{
			return smtpOutSession.RoutedMailItem.ADRecipientCache;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000BB2A0 File Offset: 0x000B94A0
		protected virtual List<MailRecipient> GetMailItemRecipients(SmtpOutSession smtpOutSession)
		{
			return smtpOutSession.RecipientCorrelator.Recipients;
		}

		// Token: 0x040016DD RID: 5853
		public static readonly Version Version = new Version(2, 1, 0, 0);

		// Token: 0x040016DE RID: 5854
		public static readonly string ADRCBlobName = "ADRC";

		// Token: 0x040016DF RID: 5855
		private static readonly Regex VersionRegex = new Regex("^ADRC-(?<Major>\\d{1,7})\\.(?<Minor>\\d{1,7})\\.(?<Build>\\d{1,7})\\.(?<Revision>\\d{1,7})$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x040016E0 RID: 5856
		public static readonly string VersionString = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
		{
			AdrcSmtpMessageContextBlob.ADRCBlobName,
			AdrcSmtpMessageContextBlob.Version
		});

		// Token: 0x040016E1 RID: 5857
		public static readonly string VersionStringWithSpace = string.Format(CultureInfo.InvariantCulture, " {0}", new object[]
		{
			AdrcSmtpMessageContextBlob.VersionString
		});

		// Token: 0x040016E2 RID: 5858
		private static readonly byte[] recipientBlobSeparator = new byte[]
		{
			235,
			83
		};

		// Token: 0x040016E3 RID: 5859
		private static readonly Dictionary<string, ADPropertyDefinition> nameToPropertyDefinitionMapping = AdrcSmtpMessageContextBlob.GetNameToPropertyMapping();

		// Token: 0x040016E4 RID: 5860
		private static HashSet<PropertyDefinition> shadowProperties;

		// Token: 0x020003F4 RID: 1012
		private static class RecipientValidationError
		{
			// Token: 0x040016E5 RID: 5861
			public static readonly int Unknown = 0;

			// Token: 0x040016E6 RID: 5862
			public static readonly int RecipientNotFound = 1;
		}
	}
}
