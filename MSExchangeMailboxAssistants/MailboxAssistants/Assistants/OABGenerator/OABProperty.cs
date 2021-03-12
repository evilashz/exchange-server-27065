using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.AddressBook.Nspi;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.OAB;
using Microsoft.Exchange.Security.Cryptography;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001EE RID: 494
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OABProperty
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x00070834 File Offset: 0x0006EA34
		public static OABProperty[] CreateOABPropertyList(Dictionary<PropTag, OABPropertyFlags> rawPropertyList)
		{
			OABProperty.Tracer.TraceFunction(0L, "OABProperty.CreateOABPropertyList");
			if (rawPropertyList.ContainsKey((PropTag)2363293707U))
			{
				OABProperty.Tracer.TraceDebug(0L, "HAB is enabled");
				if (!rawPropertyList.ContainsKey((PropTag)2148077598U))
				{
					rawPropertyList[(PropTag)2148077598U] = OABPropertyFlags.Truncated;
				}
				if (!rawPropertyList.ContainsKey((PropTag)2148012062U))
				{
					rawPropertyList[(PropTag)2148012062U] = OABPropertyFlags.Truncated;
				}
			}
			rawPropertyList.Remove(OABFilePropTags.TruncatedProperties);
			List<OABProperty> list = new List<OABProperty>(rawPropertyList.Count + 1);
			foreach (KeyValuePair<PropTag, OABPropertyFlags> keyValuePair in rawPropertyList)
			{
				PropTag key = keyValuePair.Key;
				OABPropertyFlags value = keyValuePair.Value;
				OABProperty.Tracer.TraceDebug<PropTag>(0L, "Adding PropTag {0}...", key);
				if (key == (PropTag)2355953922U)
				{
					OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating ObjectGuidOABProperty for PropTag {0}...", key);
					list.Add(new OABProperty.ObjectGuidOABProperty(key, value));
				}
				else if ((value & OABPropertyFlags.Truncated) != OABPropertyFlags.None)
				{
					if (key == (PropTag)2148077598U || key == (PropTag)2148012062U)
					{
						OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating HABGroupMemberOABProperty for PropTag {0}...", key);
						list.Add(new OABProperty.HABGroupMemberOABProperty(key, value));
					}
					else
					{
						PropertyDefinition propertyDefinition = NspiPropMapper.GetPropertyDefinition(key);
						if (propertyDefinition != null && propertyDefinition.Type == typeof(ADObjectId))
						{
							OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating TruncatedLinkOABProperty for PropTag {0}...", key);
							list.Add(new OABProperty.TruncatedLinkOABProperty(key, value));
						}
						else
						{
							OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating TruncatedOABProperty for PropTag {0}...", key);
							list.Add(new OABProperty.TruncatedOABProperty(key, value));
						}
					}
				}
				else if (key == (PropTag)2355761410U)
				{
					OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating X509CertificateProperty for PropTag {0}...", key);
					list.Add(new OABProperty.X509CertificateProperty(key, value));
				}
				else if (key.IsMultiValued())
				{
					OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating MultivaluedProperty for PropTag {0}...", key);
					list.Add(new OABProperty.MultivaluedProperty(key, value));
				}
				else
				{
					OABProperty.Tracer.TraceDebug<PropTag>(0L, "Instantiating OABProperty for PropTag {0}...", key);
					list.Add(new OABProperty(key, value));
				}
			}
			list.Add(new OABProperty.TruncatedPropertiesOABProperty(OABFilePropTags.TruncatedProperties, OABPropertyFlags.None));
			return list.ToArray();
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00070A84 File Offset: 0x0006EC84
		protected OABProperty(PropTag propTag, OABPropertyFlags propFlags)
		{
			this.propTag = propTag;
			this.propFlags = propFlags;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00070A9A File Offset: 0x0006EC9A
		public virtual PropTag PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00070AA2 File Offset: 0x0006ECA2
		public virtual OABPropertyFlags PropFlags
		{
			get
			{
				return this.propFlags;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00070AAC File Offset: 0x0006ECAC
		public virtual OABPropertyDescriptor PropertyDescriptor
		{
			get
			{
				if (this.oabPropertyDescriptor == null)
				{
					this.oabPropertyDescriptor = new OABPropertyDescriptor
					{
						PropTag = this.propTag,
						PropFlags = this.propFlags
					};
				}
				return this.oabPropertyDescriptor;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00070AEC File Offset: 0x0006ECEC
		public virtual void PreResolveLinks(ADRawEntry adRawEntry)
		{
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00070AF0 File Offset: 0x0006ECF0
		public virtual void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
		{
			PropValue propValue = propRow.Properties[index];
			if (propValue.IsError())
			{
				return;
			}
			if (propValue.PropType == PropType.String)
			{
				string value = this.ValidString((string)propValue.Value, adRawEntry.Id.ToString(), propValue.PropTag.ToString());
				oabFileRecord.PropertyValues[index] = new OABPropertyValue
				{
					PropTag = this.propTag,
					Value = value
				};
				return;
			}
			oabFileRecord.PropertyValues[index] = new OABPropertyValue
			{
				PropTag = this.propTag,
				Value = propValue.Value
			};
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00070B9C File Offset: 0x0006ED9C
		private string ValidString(string valueString, string id, string prop)
		{
			int num = valueString.IndexOf('\0');
			if (num >= 0)
			{
				OABLogger.LogRecord(TraceType.DebugTrace, "OABProperty.AddPropertyValue: Object {0}. Property {1} has non-terminating NULL char. Value {2} discarded.", new object[]
				{
					id,
					prop,
					valueString
				});
				return OABProperty.ReplacementString;
			}
			return valueString;
		}

		// Token: 0x04000BB6 RID: 2998
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000BB7 RID: 2999
		private static readonly string ReplacementString = "Corrupted string discarded";

		// Token: 0x04000BB8 RID: 3000
		private PropTag propTag;

		// Token: 0x04000BB9 RID: 3001
		private OABPropertyFlags propFlags;

		// Token: 0x04000BBA RID: 3002
		private OABPropertyDescriptor oabPropertyDescriptor;

		// Token: 0x020001EF RID: 495
		private sealed class ObjectGuidOABProperty : OABProperty
		{
			// Token: 0x06001354 RID: 4948 RVA: 0x00070BF1 File Offset: 0x0006EDF1
			public ObjectGuidOABProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x06001355 RID: 4949 RVA: 0x00070BFB File Offset: 0x0006EDFB
			public override PropTag PropTag
			{
				get
				{
					return PropTag.ExchangeObjectId;
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06001356 RID: 4950 RVA: 0x00070C04 File Offset: 0x0006EE04
			public override OABPropertyDescriptor PropertyDescriptor
			{
				get
				{
					return new OABPropertyDescriptor
					{
						PropTag = (PropTag)2355953922U,
						PropFlags = this.propFlags
					};
				}
			}

			// Token: 0x06001357 RID: 4951 RVA: 0x00070C30 File Offset: 0x0006EE30
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
				PropValue propValue = propRow.Properties[index];
				if (propValue.IsError())
				{
					return;
				}
				oabFileRecord.PropertyValues[index] = new OABPropertyValue
				{
					PropTag = (PropTag)2355953922U,
					Value = propValue.Value
				};
			}
		}

		// Token: 0x020001F0 RID: 496
		private sealed class TruncatedPropertiesOABProperty : OABProperty
		{
			// Token: 0x06001358 RID: 4952 RVA: 0x00070C7D File Offset: 0x0006EE7D
			public TruncatedPropertiesOABProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x00070C87 File Offset: 0x0006EE87
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
			}
		}

		// Token: 0x020001F1 RID: 497
		private class TruncatedOABProperty : OABProperty
		{
			// Token: 0x0600135A RID: 4954 RVA: 0x00070C89 File Offset: 0x0006EE89
			public TruncatedOABProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
			}

			// Token: 0x0600135B RID: 4955 RVA: 0x00070C94 File Offset: 0x0006EE94
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
				PropValue propValue = propRow.Properties[index];
				if (propValue.IsError() || propValue.Value == null)
				{
					return;
				}
				this.AddPropTagToTruncatedPropertiesProperty(oabFileRecord);
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x00070CCC File Offset: 0x0006EECC
			protected void AddPropTagToTruncatedPropertiesProperty(OABFileRecord oabFileRecord)
			{
				List<int> list;
				if (oabFileRecord.PropertyValues[oabFileRecord.PropertyValues.Length - 1] == null || oabFileRecord.PropertyValues[oabFileRecord.PropertyValues.Length - 1].Value == null || ((int[])oabFileRecord.PropertyValues[oabFileRecord.PropertyValues.Length - 1].Value).Length == 0)
				{
					list = new List<int>();
				}
				else
				{
					list = new List<int>(oabFileRecord.PropertyValues[oabFileRecord.PropertyValues.Length - 1].Value as int[]);
				}
				list.Add((int)this.propTag);
				oabFileRecord.PropertyValues[oabFileRecord.PropertyValues.Length - 1] = new OABPropertyValue
				{
					PropTag = OABFilePropTags.TruncatedProperties,
					Value = list.ToArray()
				};
			}
		}

		// Token: 0x020001F2 RID: 498
		private class MultivaluedProperty : OABProperty
		{
			// Token: 0x0600135D RID: 4957 RVA: 0x00070D87 File Offset: 0x0006EF87
			public MultivaluedProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
				this.propertyDefinition = NspiPropMapper.GetPropertyDefinition(this.propTag);
			}

			// Token: 0x0600135E RID: 4958 RVA: 0x00070DE0 File Offset: 0x0006EFE0
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
				PropValue propValue = propRow.Properties[index];
				if (propValue.IsError())
				{
					return;
				}
				try
				{
					if (propValue.PropType == PropType.StringArray)
					{
						string[] stringArray = propValue.GetStringArray();
						if (stringArray != null)
						{
							string[] value = (from s in stringArray
							select this.ValidString(s, adRawEntry.Id.ToString(), propValue.PropTag.ToString()) into v
							orderby v
							select v).ToArray<string>();
							oabFileRecord.PropertyValues[index] = new OABPropertyValue
							{
								PropTag = this.propTag,
								Value = value
							};
							return;
						}
					}
				}
				catch (Exception ex)
				{
					OABLogger.LogRecord(TraceType.DebugTrace, "MultivaluedProperty.AddPropertyValue: Informational only: Unable to validate strings in object {0}, property {1}. Reason: {2}", new object[]
					{
						adRawEntry.Id.ToString(),
						propValue.PropTag.ToString(),
						ex.Message
					});
				}
				object obj = this.SortValues(propValue);
				if (obj != null)
				{
					oabFileRecord.PropertyValues[index] = new OABPropertyValue
					{
						PropTag = this.propTag,
						Value = obj
					};
				}
			}

			// Token: 0x0600135F RID: 4959 RVA: 0x00070F58 File Offset: 0x0006F158
			protected object SortValues(PropValue propValue)
			{
				object value;
				try
				{
					PropType propType = propValue.PropType;
					if (propType <= PropType.LongArray)
					{
						switch (propType)
						{
						case PropType.ShortArray:
						{
							short[] shortArray = propValue.GetShortArray();
							if (shortArray != null)
							{
								Array.Sort<short>(shortArray);
								return shortArray;
							}
							return propValue.Value;
						}
						case PropType.IntArray:
						{
							int[] intArray = propValue.GetIntArray();
							if (intArray != null)
							{
								Array.Sort<int>(intArray);
								return intArray;
							}
							return propValue.Value;
						}
						case PropType.FloatArray:
						{
							float[] floatArray = propValue.GetFloatArray();
							if (floatArray != null)
							{
								Array.Sort<float>(floatArray);
								return floatArray;
							}
							return propValue.Value;
						}
						case PropType.DoubleArray:
						{
							double[] doubleArray = propValue.GetDoubleArray();
							if (doubleArray != null)
							{
								Array.Sort<double>(doubleArray);
								return doubleArray;
							}
							return propValue.Value;
						}
						default:
							if (propType == PropType.LongArray)
							{
								long[] longArray = propValue.GetLongArray();
								if (longArray != null)
								{
									Array.Sort<long>(longArray);
									return longArray;
								}
								return propValue.Value;
							}
							break;
						}
					}
					else
					{
						switch (propType)
						{
						case PropType.AnsiStringArray:
						case PropType.StringArray:
						{
							string[] stringArray = propValue.GetStringArray();
							if (stringArray != null)
							{
								Array.Sort<string>(stringArray);
								return stringArray;
							}
							return propValue.Value;
						}
						default:
							if (propType != PropType.GuidArray)
							{
								if (propType == PropType.BinaryArray)
								{
									byte[][] bytesArray = propValue.GetBytesArray();
									if (bytesArray != null)
									{
										Array.Sort<byte[]>(bytesArray, ArrayComparer<byte>.Comparer);
										return bytesArray;
									}
									return propValue.Value;
								}
							}
							else
							{
								Guid[] guidArray = propValue.GetGuidArray();
								if (guidArray != null)
								{
									Array.Sort<Guid>(guidArray);
									return guidArray;
								}
								return propValue.Value;
							}
							break;
						}
					}
					OABLogger.LogRecord(TraceType.ErrorTrace, "MultivaluedProperty.SortValues: don't know how to deal with type {0}", new object[]
					{
						propValue.PropType
					});
					value = propValue.Value;
				}
				catch (InvalidCastException)
				{
					OABLogger.LogRecord(TraceType.ErrorTrace, "MultivaluedProperty.SortValues: InvalidCastException while handling property {0} of stated type {1}", new object[]
					{
						this.propertyDefinition.Name,
						propValue.PropType
					});
					value = propValue.Value;
				}
				return value;
			}

			// Token: 0x04000BBB RID: 3003
			protected readonly PropertyDefinition propertyDefinition;
		}

		// Token: 0x020001F3 RID: 499
		private class X509CertificateProperty : OABProperty.MultivaluedProperty
		{
			// Token: 0x06001361 RID: 4961 RVA: 0x000711AC File Offset: 0x0006F3AC
			public X509CertificateProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
			}

			// Token: 0x06001362 RID: 4962 RVA: 0x000711B8 File Offset: 0x0006F3B8
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
				PropValue propValue = propRow.Properties[index];
				if (propValue.IsError())
				{
					return;
				}
				object obj = base.SortValues(propValue);
				if (obj == null || !(obj is byte[][]))
				{
					return;
				}
				byte[][] array = this.FilterX509Certificates((byte[][])obj);
				if (array != null)
				{
					oabFileRecord.PropertyValues[index] = new OABPropertyValue
					{
						PropTag = this.propTag,
						Value = array
					};
				}
			}

			// Token: 0x06001363 RID: 4963 RVA: 0x00071224 File Offset: 0x0006F424
			private byte[][] FilterX509Certificates(byte[][] rawCertificates)
			{
				List<byte[]> list = new List<byte[]>();
				foreach (byte[] array in rawCertificates)
				{
					try
					{
						X509Certificate2 x509Certificate = new X509Certificate2(array);
						if (!(x509Certificate.NotBefore > DateTime.UtcNow) && !(x509Certificate.NotAfter < DateTime.UtcNow))
						{
							X509ExtensionCollection extensions = x509Certificate.Extensions;
							bool flag = false;
							bool flag2 = false;
							bool flag3 = false;
							bool flag4 = false;
							bool flag5 = false;
							foreach (X509Extension x509Extension in extensions)
							{
								if (x509Extension is X509KeyUsageExtension)
								{
									flag = true;
									X509KeyUsageExtension x509KeyUsageExtension = x509Extension as X509KeyUsageExtension;
									if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) != X509KeyUsageFlags.None || (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) != X509KeyUsageFlags.None)
									{
										flag3 = true;
									}
									if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyEncipherment) != X509KeyUsageFlags.None || (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyAgreement) != X509KeyUsageFlags.None)
									{
										flag4 = true;
									}
								}
								else if (x509Extension is X509EnhancedKeyUsageExtension)
								{
									flag2 = true;
									X509EnhancedKeyUsageExtension x509EnhancedKeyUsageExtension = x509Extension as X509EnhancedKeyUsageExtension;
									foreach (Oid oid in x509EnhancedKeyUsageExtension.EnhancedKeyUsages)
									{
										if (string.Equals(oid.Value, WellKnownOid.EmailProtection.Value, StringComparison.OrdinalIgnoreCase) || string.Equals(oid.Value, WellKnownOid.AnyExtendedKeyUsage.Value, StringComparison.OrdinalIgnoreCase))
										{
											flag5 = true;
											break;
										}
									}
								}
							}
							if (!flag)
							{
								flag3 = true;
								flag4 = true;
							}
							if (!flag2)
							{
								flag5 = true;
							}
							if ((flag3 || flag4) && flag5)
							{
								list.Add(array);
							}
						}
					}
					catch (CryptographicException)
					{
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
				return list.ToArray();
			}
		}

		// Token: 0x020001F4 RID: 500
		private class TruncatedLinkOABProperty : OABProperty.TruncatedOABProperty
		{
			// Token: 0x06001364 RID: 4964 RVA: 0x000713D8 File Offset: 0x0006F5D8
			public TruncatedLinkOABProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
				this.linkProperty = NspiPropMapper.GetPropertyDefinition(this.propTag);
				string text = this.linkProperty.Name + "-Placeholder";
				this.placeholderProperty = new ADPropertyDefinition(text, ExchangeObjectVersion.Exchange2003, typeof(bool), text, ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
			}

			// Token: 0x06001365 RID: 4965 RVA: 0x00071443 File Offset: 0x0006F643
			public override void PreResolveLinks(ADRawEntry adRawEntry)
			{
				this.RememberIfLinkHasValue(adRawEntry);
			}

			// Token: 0x06001366 RID: 4966 RVA: 0x0007144C File Offset: 0x0006F64C
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
				if ((bool)adRawEntry[this.placeholderProperty])
				{
					base.AddPropTagToTruncatedPropertiesProperty(oabFileRecord);
				}
			}

			// Token: 0x06001367 RID: 4967 RVA: 0x00071468 File Offset: 0x0006F668
			protected void RememberIfLinkHasValue(ADRawEntry adRawEntry)
			{
				bool flag = false;
				if ((((ADPropertyDefinition)this.linkProperty).Flags & ADPropertyDefinitionFlags.MultiValued) != ADPropertyDefinitionFlags.None)
				{
					MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)adRawEntry[this.linkProperty];
					if (multiValuedProperty != null && multiValuedProperty.Count > 0)
					{
						flag = true;
					}
				}
				else
				{
					ADObjectId adobjectId = (ADObjectId)adRawEntry[this.linkProperty];
					if (adobjectId != null)
					{
						flag = true;
					}
				}
				if (flag)
				{
					adRawEntry.SetIsReadOnly(false);
					adRawEntry[this.placeholderProperty] = true;
					adRawEntry[this.linkProperty] = null;
				}
			}

			// Token: 0x04000BBD RID: 3005
			private const string PlaceholderPropertySuffix = "-Placeholder";

			// Token: 0x04000BBE RID: 3006
			protected readonly PropertyDefinition linkProperty;

			// Token: 0x04000BBF RID: 3007
			protected readonly PropertyDefinition placeholderProperty;
		}

		// Token: 0x020001F5 RID: 501
		private sealed class HABGroupMemberOABProperty : OABProperty.TruncatedLinkOABProperty
		{
			// Token: 0x06001368 RID: 4968 RVA: 0x000714ED File Offset: 0x0006F6ED
			public HABGroupMemberOABProperty(PropTag propTag, OABPropertyFlags propFlags) : base(propTag, propFlags)
			{
				this.isOrganizationalProperty = NspiPropMapper.GetPropertyDefinition((PropTag)2363293707U);
			}

			// Token: 0x06001369 RID: 4969 RVA: 0x00071507 File Offset: 0x0006F707
			public override void PreResolveLinks(ADRawEntry adRawEntry)
			{
				if (!(bool)adRawEntry[this.isOrganizationalProperty])
				{
					base.RememberIfLinkHasValue(adRawEntry);
				}
			}

			// Token: 0x0600136A RID: 4970 RVA: 0x00071524 File Offset: 0x0006F724
			public override void AddPropertyValue(ADRawEntry adRawEntry, PropRow propRow, OABFileRecord oabFileRecord, int index)
			{
				if (!(bool)adRawEntry[this.isOrganizationalProperty])
				{
					if ((bool)adRawEntry[this.placeholderProperty])
					{
						base.AddPropTagToTruncatedPropertiesProperty(oabFileRecord);
					}
					return;
				}
				PropValue propValue = propRow.Properties[index];
				if (propValue.IsError())
				{
					return;
				}
				oabFileRecord.PropertyValues[index] = new OABPropertyValue
				{
					PropTag = this.propTag,
					Value = propValue.Value
				};
			}

			// Token: 0x04000BC0 RID: 3008
			private PropertyDefinition isOrganizationalProperty;
		}
	}
}
