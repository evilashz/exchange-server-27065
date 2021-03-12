using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200083A RID: 2106
	internal static class SyncValueConvertor
	{
		// Token: 0x06006895 RID: 26773 RVA: 0x001704E4 File Offset: 0x0016E6E4
		public static object GetValuesFromDirectoryProperty(SyncPropertyDefinition property, DirectoryProperty originalValues, List<ValidationError> errors)
		{
			if (originalValues == null)
			{
				return null;
			}
			IList values = originalValues.GetValues();
			if (values == null || values.Count == 0 || values == DirectoryProperty.EmptyValues)
			{
				return null;
			}
			if (property.IsMultivalued)
			{
				IList values2 = new ArrayList(values.Count);
				SyncValueConvertor.ConvertMultipleValues(property, values, ref values2, errors);
				MultiValuedPropertyBase multiValuedPropertyBase = ADValueConvertor.CreateGenericMultiValuedProperty(property, property.IsReadOnly, values2, null, null);
				multiValuedPropertyBase.IsCompletelyRead = true;
				return multiValuedPropertyBase;
			}
			ValidationError validationError = null;
			object result = SyncValueConvertor.ConvertSingleValue(property, values[0], out validationError);
			if (validationError != null)
			{
				errors.Add(validationError);
			}
			return result;
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x00170574 File Offset: 0x0016E774
		public static IList GetValuesForDirectoryProperty(SyncPropertyDefinition property, object value)
		{
			ArrayList result = new ArrayList();
			if (property.IsMultivalued)
			{
				MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)value;
				using (IEnumerator enumerator = ((IEnumerable)multiValuedPropertyBase).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object value2 = enumerator.Current;
						SyncValueConvertor.AddValueToResultCollection(property, result, value2);
					}
					return result;
				}
			}
			SyncValueConvertor.AddValueToResultCollection(property, result, value);
			return result;
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x001705E8 File Offset: 0x0016E7E8
		private static void AddValueToResultCollection(SyncPropertyDefinition property, ArrayList result, object value)
		{
			object obj = SyncValueConvertor.ConvertSingleValueForDirectoryProperty(property, value);
			if (obj != SyncValueConvertor.IgnoreValue)
			{
				result.Add(obj);
			}
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x00170610 File Offset: 0x0016E810
		private static void ConvertMultipleValues(SyncPropertyDefinition property, IList values, ref IList convertedValues, List<ValidationError> errors)
		{
			foreach (object value in values)
			{
				ValidationError validationError = null;
				object obj = SyncValueConvertor.ConvertSingleValue(property, value, out validationError);
				if (obj != null)
				{
					convertedValues.Add(obj);
				}
				if (validationError != null)
				{
					errors.Add(validationError);
				}
			}
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x00170680 File Offset: 0x0016E880
		private static object ConvertSingleValue(SyncPropertyDefinition property, object value, out ValidationError error)
		{
			error = null;
			if (value is XmlValueAssignedPlan)
			{
				return ((XmlValueAssignedPlan)value).Plan;
			}
			if (value is XmlValueCompanyVerifiedDomain)
			{
				return ((XmlValueCompanyVerifiedDomain)value).Domain;
			}
			if (value is XmlValueDirSyncStatus)
			{
				return SyncValueConvertor.ConvertDirSyncStatusToString(((XmlValueDirSyncStatus)value).DirSyncStatus);
			}
			if (value is AttributeSet)
			{
				return SyncValueConvertor.ConvertAttributeSetToString((AttributeSet)value);
			}
			if (value is XmlValueValidationError)
			{
				return ((XmlValueValidationError)value).ErrorInfo;
			}
			if (value is XmlValueLicenseUnitsDetail)
			{
				return SyncValueConvertor.ConvertXmlValueLicenseUnitsDetailToString((XmlValueLicenseUnitsDetail)value);
			}
			if (property == SyncSubscribedPlanSchema.Capability)
			{
				return ((XmlElement)value).OuterXml;
			}
			if (property == SyncCompanySchema.CompanyPartnership)
			{
				return SyncValueConvertor.ConvertCompanyPartnershipToString((XmlValueCompanyPartnership)value);
			}
			if (property == SyncRecipientSchema.SipAddresses)
			{
				string text = (string)value;
				if (!string.IsNullOrEmpty(text) && !text.StartsWith(ProxyAddressPrefix.SIP.ToString() + ':', StringComparison.OrdinalIgnoreCase))
				{
					value = ProxyAddressPrefix.SIP.ToString() + ':' + text;
				}
			}
			else
			{
				if (property.Type == typeof(RemoteRecipientType))
				{
					return (RemoteRecipientType)value;
				}
				if (property.Type == typeof(RecipientTypeDetails))
				{
					return (RecipientTypeDetails)value;
				}
				if (property.Type == typeof(MailboxAuditOperations))
				{
					return (MailboxAuditOperations)value;
				}
				if (property.Type == typeof(ElcMailboxFlags))
				{
					return (ElcMailboxFlags)value;
				}
				if (property.Type == typeof(DateTime?))
				{
					return (DateTime?)value;
				}
				if (property.Type == typeof(DateTime))
				{
					return (DateTime)value;
				}
				if (property.Type == typeof(EnhancedTimeSpan))
				{
					return TimeSpan.FromSeconds((double)((int)value));
				}
				if (value is XmlValueRightsManagementTenantConfiguration)
				{
					return ((XmlValueRightsManagementTenantConfiguration)value).RightsManagementTenantConfiguration;
				}
				if (value is XmlValueRightsManagementTenantKey)
				{
					return ((XmlValueRightsManagementTenantKey)value).RightsManagementTenantKey;
				}
				if (value is XmlValueServiceInfo)
				{
					return ((XmlValueServiceInfo)value).Info;
				}
				if (value is int || value is bool)
				{
					return value;
				}
				if (value is DirectoryReferenceAddressList)
				{
					DirectoryReferenceAddressList directoryReferenceAddressList = (DirectoryReferenceAddressList)value;
					if (!directoryReferenceAddressList.TargetDeleted)
					{
						return new PropertyReference(directoryReferenceAddressList.Value, directoryReferenceAddressList.TargetClass, directoryReferenceAddressList.TargetDeleted);
					}
					return null;
				}
				else if (value is DirectoryReferenceUserAndServicePrincipal)
				{
					DirectoryReferenceUserAndServicePrincipal directoryReferenceUserAndServicePrincipal = (DirectoryReferenceUserAndServicePrincipal)value;
					return directoryReferenceUserAndServicePrincipal.Value;
				}
			}
			bool noConversionRequired = property.Type == typeof(byte[]) || property.Type == typeof(string);
			return ADValueConvertor.ConvertFromADAndValidateSingleValue(value, property, noConversionRequired, out error);
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x00170960 File Offset: 0x0016EB60
		private static object ConvertSingleValueForDirectoryProperty(SyncPropertyDefinition property, object value)
		{
			if (object.Equals(property.DefaultValue, value) && !property.PersistDefaultValue)
			{
				return SyncValueConvertor.IgnoreValue;
			}
			if (value == null)
			{
				return null;
			}
			if (typeof(DirectoryPropertyString).IsAssignableFrom(property.ExternalType) || property.ExternalType == typeof(string))
			{
				return ADValueConvertor.ConvertValueToString(value, property.FormatProvider);
			}
			if (typeof(DirectoryPropertyGuid).IsAssignableFrom(property.ExternalType))
			{
				return ADValueConvertor.ConvertValueToString(value, property.FormatProvider);
			}
			if (typeof(DirectoryPropertyBinary).IsAssignableFrom(property.ExternalType))
			{
				return ADValueConvertor.ConvertValueToBinary(value, property.FormatProvider);
			}
			if (typeof(DirectoryPropertyBoolean).IsAssignableFrom(property.ExternalType))
			{
				return bool.Parse(ADValueConvertor.ConvertValueToString(value, property.FormatProvider));
			}
			if (typeof(DirectoryPropertyInt32).IsAssignableFrom(property.ExternalType))
			{
				return int.Parse(ADValueConvertor.ConvertValueToString(value, property.FormatProvider));
			}
			if (typeof(DirectoryPropertyDateTime).IsAssignableFrom(property.ExternalType))
			{
				Type type = value.GetType();
				if (type.Equals(typeof(DateTime)))
				{
					return ((DateTime)value).ToUniversalTime();
				}
				return DateTime.Parse(ADValueConvertor.ConvertValueToString(value, property.FormatProvider), property.FormatProvider);
			}
			else
			{
				if (typeof(DirectoryPropertyXmlServiceOriginatedResource) == property.ExternalType && value is Capability)
				{
					Capability capability = (Capability)value;
					if (CapabilityHelper.IsAllowedSKUCapability(capability))
					{
						Guid skucapabilityGuid = CapabilityHelper.GetSKUCapabilityGuid(capability);
						if (skucapabilityGuid != Guid.Empty)
						{
							return new XmlValueServiceOriginatedResource
							{
								Resource = new ServiceOriginatedResourceValue
								{
									ServicePlanId = skucapabilityGuid.ToString("D"),
									Capability = capability.ToString()
								}
							};
						}
					}
					return SyncValueConvertor.IgnoreValue;
				}
				if (typeof(DirectoryPropertyXmlDirSyncStatus).IsAssignableFrom(property.ExternalType))
				{
					string stringValue = (string)value;
					return new XmlValueDirSyncStatus
					{
						DirSyncStatus = SyncValueConvertor.ConvertStringToDirSyncStatus(stringValue)
					};
				}
				if (typeof(DirectoryPropertyReferenceAddressList).IsAssignableFrom(property.ExternalType) && value is PropertyReference)
				{
					PropertyReference propertyReference = (PropertyReference)value;
					return new DirectoryReferenceAddressList
					{
						TargetClass = propertyReference.TargetObjectClass,
						Value = propertyReference.TargetId,
						TargetDeleted = propertyReference.TargetDeleted
					};
				}
				throw new NotSupportedException("Conversion for external type " + property.ExternalType.Name);
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x00170C00 File Offset: 0x0016EE00
		public static DirSyncStatusValue ConvertStringToDirSyncStatus(string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				ExTraceGlobals.ActiveDirectoryTracer.TraceError(0L, "<SyncValueConvertor::ConvertStringToDirSyncStatus> NULL or empty string passed in");
				return null;
			}
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToDirSyncStatus> stringValue = \"{0}\"", stringValue);
			string[] array = stringValue.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 4)
			{
				ExTraceGlobals.ActiveDirectoryTracer.TraceError<string>(0L, "<SyncValueConvertor::ConvertStringToDirSyncStatus> Failed to parse \"{0}\"", stringValue);
				return null;
			}
			DirSyncStatusValue dirSyncStatusValue = new DirSyncStatusValue();
			dirSyncStatusValue.State = (DirSyncState)Enum.Parse(typeof(DirSyncState), array[0]);
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToDirSyncStatus> dirSyncStatusValue.State = {0}", dirSyncStatusValue.State.ToString());
			dirSyncStatusValue.AttributeSetName = array[1];
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToDirSyncStatus> dirSyncStatusValue.AttributeSetName = \"{0}\"", dirSyncStatusValue.AttributeSetName);
			dirSyncStatusValue.Version = array[2];
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToDirSyncStatus> dirSyncStatusValue.Version = \"{0}\"", dirSyncStatusValue.Version);
			return dirSyncStatusValue;
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x00170CF0 File Offset: 0x0016EEF0
		public static string ConvertDirSyncStatusToString(DirSyncStatusValue dirSyncStatusValue)
		{
			if (dirSyncStatusValue == null)
			{
				ExTraceGlobals.ActiveDirectoryTracer.TraceError(0L, "<SyncValueConvertor::ConvertDirSyncStatusToString> NULL DirSyncStatusValue passed in");
				return string.Empty;
			}
			string text = string.Format("{0},{1},{2},{3}", new object[]
			{
				dirSyncStatusValue.State.ToString(),
				dirSyncStatusValue.AttributeSetName,
				dirSyncStatusValue.Version,
				0
			});
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertDirSyncStatusToString> return \"{0}\"", text);
			return text;
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x00170D6C File Offset: 0x0016EF6C
		public static AttributeSet ConvertStringToAttributeSet(string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				ExTraceGlobals.ActiveDirectoryTracer.TraceError(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> NULL or empty string passed in");
				return null;
			}
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> stringValue = \"{0}\"", stringValue);
			string[] array = stringValue.Split(new char[]
			{
				','
			});
			if (array.Length != 5)
			{
				ExTraceGlobals.ActiveDirectoryTracer.TraceError<string>(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> Failed to parse \"{0}\"", stringValue);
				return null;
			}
			AttributeSet attributeSet = new AttributeSet();
			attributeSet.ExchangeMastered = bool.Parse(array[0]);
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> attributeSet.ExchangeMastered = {0}", attributeSet.ExchangeMastered.ToString());
			attributeSet.Name = array[1];
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> attributeSet.Name = {0}", attributeSet.Name);
			attributeSet.Version = array[2];
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> attributeSet.Version = {0}", attributeSet.Version);
			attributeSet.LastVersionSeized = (string.IsNullOrEmpty(array[3]) ? string.Empty : array[3]);
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertStringToAttributeSet> attributeSet.LastVersionSeized = {0}", attributeSet.LastVersionSeized);
			return attributeSet;
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x00170E7C File Offset: 0x0016F07C
		public static string ConvertAttributeSetToString(AttributeSet attributeSet)
		{
			if (attributeSet == null)
			{
				ExTraceGlobals.ActiveDirectoryTracer.TraceError(0L, "<SyncValueConvertor::ConvertAttributeSetToString> NULL AttributeSet passed in");
				return string.Empty;
			}
			string text = string.Format("{0},{1},{2},{3},{4}", new object[]
			{
				attributeSet.ExchangeMastered,
				attributeSet.Name,
				attributeSet.Version,
				string.IsNullOrEmpty(attributeSet.LastVersionSeized) ? string.Empty : attributeSet.LastVersionSeized,
				0
			});
			ExTraceGlobals.ActiveDirectoryTracer.TraceDebug<string>(0L, "<SyncValueConvertor::ConvertAttributeSetToString> return \"{0}\"", text);
			return text;
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x00170F0F File Offset: 0x0016F10F
		public static string ConvertXmlValueLicenseUnitsDetailToString(XmlValueLicenseUnitsDetail licenseDetail)
		{
			return SyncValueConvertor.ConvertToBasicXml<LicenseUnitsDetailValue>(licenseDetail.LicenseUnitsDetail);
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x00170F1C File Offset: 0x0016F11C
		public static string ConvertCompanyPartnershipToString(XmlValueCompanyPartnership companyPartnership)
		{
			return SyncValueConvertor.ConvertToBasicXml<PartnershipValue[]>(companyPartnership.Partnerships);
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x00170F2C File Offset: 0x0016F12C
		public static bool TryParseReleaseTrackFromServiceInfo(MultiValuedProperty<ServiceInfoValue> serviceInfoValues, out string releaseTrack)
		{
			bool result = false;
			releaseTrack = null;
			if (serviceInfoValues == null)
			{
				return false;
			}
			foreach (ServiceInfoValue serviceInfoValue in serviceInfoValues)
			{
				if (!string.IsNullOrEmpty(serviceInfoValue.ServiceInstance) && serviceInfoValue.ServiceInstance.StartsWith("ChangeManagement/", true, null))
				{
					XmlElement[] any = serviceInfoValue.Any;
					int i = 0;
					while (i < any.Length)
					{
						XmlElement xmlElement = any[i];
						XmlNodeList elementsByTagName = xmlElement.GetElementsByTagName("ReleaseTrack");
						if (elementsByTagName.Count > 0)
						{
							result = true;
							if (!string.IsNullOrWhiteSpace(elementsByTagName[0].InnerText))
							{
								releaseTrack = elementsByTagName[0].InnerText;
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x00171004 File Offset: 0x0016F204
		private static string ConvertToBasicXml<T>(T targetObject)
		{
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlSerializerNamespaces.Add(string.Empty, string.Empty);
			xmlWriterSettings.OmitXmlDeclaration = true;
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					xmlSerializer.Serialize(xmlWriter, targetObject, xmlSerializerNamespaces);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x040044D2 RID: 17618
		private const char DirSyncStatusPartDelimitor = ',';

		// Token: 0x040044D3 RID: 17619
		private static readonly object IgnoreValue = new object();
	}
}
