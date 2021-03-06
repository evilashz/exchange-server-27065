using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000056 RID: 86
	internal class ADObjectSchema : ObjectSchema
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x000181F1 File Offset: 0x000163F1
		public ADObjectSchema()
		{
			this.InitializeADObjectSchemaProperties();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001820A File Offset: 0x0001640A
		public string[] LdapAttributes
		{
			get
			{
				return this.ldapAttributes;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00018212 File Offset: 0x00016412
		public ReadOnlyCollection<ADPropertyDefinition> AllADObjectLinkProperties
		{
			get
			{
				return this.allADObjectLinkProperties;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001821A File Offset: 0x0001641A
		public bool HasAutogeneratedConstraints
		{
			get
			{
				return this.hasAutogeneratedConstraints;
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00018224 File Offset: 0x00016424
		internal static string[] ADPropertyCollectionToLdapAttributes(IEnumerable<PropertyDefinition> schema, int id)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			List<string> list = new List<string>();
			foreach (PropertyDefinition propertyDefinition in schema)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				string ldapDisplayName = adpropertyDefinition.LdapDisplayName;
				ADObjectSchema.InternalAddSoftLinkAttribute(adpropertyDefinition, list, id);
				if (!string.IsNullOrEmpty(ldapDisplayName) && !adpropertyDefinition.IsCalculated && !list.Contains(ldapDisplayName))
				{
					ExTraceGlobals.ADPropertyRequestTracer.TraceDebug<string>((long)id, "ADObjectSchema::ADPropertyCollectionToLdapAttributes: requesting {0}", ldapDisplayName);
					list.Add(ldapDisplayName);
				}
				if (adpropertyDefinition.IsCalculated)
				{
					string name = adpropertyDefinition.Name;
					foreach (ProviderPropertyDefinition providerPropertyDefinition in adpropertyDefinition.SupportingProperties)
					{
						ADPropertyDefinition adpropertyDefinition2 = (ADPropertyDefinition)providerPropertyDefinition;
						if (!adpropertyDefinition2.IsTaskPopulated)
						{
							ldapDisplayName = adpropertyDefinition2.LdapDisplayName;
							if (!list.Contains(ldapDisplayName))
							{
								ExTraceGlobals.ADPropertyRequestTracer.TraceDebug<string, string>((long)id, "ADObjectSchema::ADPropertyCollectionToLdapAttributes: requesting supporting {0} for {1}", ldapDisplayName, name);
								list.Add(ldapDisplayName);
							}
						}
					}
				}
			}
			list.Add("lastKnownParent");
			list.Add("isDeleted");
			return list.ToArray();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001836C File Offset: 0x0001656C
		internal void InitializeAutogeneratedConstraints()
		{
			if (!this.HasAutogeneratedConstraints)
			{
				lock (this.constraintsLock)
				{
					if (!this.HasAutogeneratedConstraints)
					{
						this.hasAutogeneratedConstraints = this.InternalUpdateProperties();
					}
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000183C4 File Offset: 0x000165C4
		protected void InitializeADObjectSchemaProperties()
		{
			this.AddShadowPropertiesToAllPropertiesList();
			this.InitializeAllPropertiesDictionary();
			this.InitializeFilterOnlyPropertiesDictionary();
			this.ldapAttributes = ADObjectSchema.ADPropertyCollectionToLdapAttributes(base.AllProperties, this.GetHashCode());
			this.InitializeADObjectLinkProperties();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000183F8 File Offset: 0x000165F8
		private void AddShadowPropertiesToAllPropertiesList()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in base.AllProperties)
			{
				hashSet.TryAdd(propertyDefinition);
				ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
				if (adpropertyDefinition != null && adpropertyDefinition.ShadowProperty != null)
				{
					hashSet.TryAdd(adpropertyDefinition.ShadowProperty);
				}
			}
			base.AllProperties = new ReadOnlyCollection<PropertyDefinition>(hashSet.ToArray());
			base.InitializePropertyCollections();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00018488 File Offset: 0x00016688
		private void InitializeAllPropertiesDictionary()
		{
			this.allPropertiesDictionary = new Dictionary<string, ADPropertyDefinition>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyDefinition propertyDefinition in base.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
				if (adpropertyDefinition != null)
				{
					this.AddPropertyToDictionary(this.allPropertiesDictionary, adpropertyDefinition);
				}
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000184FC File Offset: 0x000166FC
		private void InitializeFilterOnlyPropertiesDictionary()
		{
			this.allFilterOnlyPropertiesDictionary = new Dictionary<string, ADPropertyDefinition>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyDefinition propertyDefinition in base.AllFilterOnlyProperties)
			{
				ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
				if (adpropertyDefinition != null && !string.IsNullOrEmpty(adpropertyDefinition.LdapDisplayName))
				{
					this.allFilterOnlyPropertiesDictionary.Add(adpropertyDefinition.LdapDisplayName, adpropertyDefinition);
				}
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00018584 File Offset: 0x00016784
		private void InitializeADObjectLinkProperties()
		{
			List<ADPropertyDefinition> list = new List<ADPropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in base.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
				if (adpropertyDefinition.Name != "HomeMTA" && adpropertyDefinition != null && adpropertyDefinition.Type == typeof(ADObjectId))
				{
					list.Add(adpropertyDefinition);
				}
			}
			this.allADObjectLinkProperties = new ReadOnlyCollection<ADPropertyDefinition>(list.ToArray());
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00018624 File Offset: 0x00016824
		private void AddPropertyToDictionary(Dictionary<string, ADPropertyDefinition> allPropertiesDictionary, ADPropertyDefinition adPropertyDefinition)
		{
			if (string.IsNullOrEmpty(adPropertyDefinition.LdapDisplayName) || adPropertyDefinition.IsCalculated)
			{
				if (adPropertyDefinition.IsCalculated && adPropertyDefinition.SupportingProperties != null)
				{
					foreach (ProviderPropertyDefinition providerPropertyDefinition in adPropertyDefinition.SupportingProperties)
					{
						ADPropertyDefinition adPropertyDefinition2 = (ADPropertyDefinition)providerPropertyDefinition;
						this.AddPropertyToDictionary(allPropertiesDictionary, adPropertyDefinition2);
					}
				}
				return;
			}
			if (!allPropertiesDictionary.ContainsKey(adPropertyDefinition.LdapDisplayName))
			{
				allPropertiesDictionary.Add(adPropertyDefinition.LdapDisplayName, adPropertyDefinition);
				return;
			}
			ADPropertyDefinition adpropertyDefinition = allPropertiesDictionary[adPropertyDefinition.LdapDisplayName];
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000186CC File Offset: 0x000168CC
		internal ADPropertyDefinition GetADPropDefByLdapDisplayName(string name)
		{
			ADPropertyDefinition result;
			if (this.allPropertiesDictionary.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000186EC File Offset: 0x000168EC
		internal ADPropertyDefinition GetFilterOnlyADPropDefByLdapDisplayName(string name)
		{
			ADPropertyDefinition result;
			if (this.allFilterOnlyPropertiesDictionary.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001870C File Offset: 0x0001690C
		private static void InternalAddSoftLinkAttribute(ADPropertyDefinition propertyDefinition, List<string> results, int id)
		{
			if (ADGlobalConfigSettings.SoftLinkEnabled && propertyDefinition.IsSoftLinkAttribute)
			{
				string ldapDisplayName = propertyDefinition.SoftLinkShadowProperty.LdapDisplayName;
				ExTraceGlobals.ADPropertyRequestTracer.TraceDebug<string>((long)id, "ADObjectSchema::ADPropertyCollectionToLdapAttributes: requesting {0}", ldapDisplayName);
				results.Add(ldapDisplayName);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001874D File Offset: 0x0001694D
		[Conditional("DEBUG")]
		private void InternalVerifyNoDuplicateProperties(ADPropertyDefinition adPropertyDefinition, ADPropertyDefinition dictionaryPropDef)
		{
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001874F File Offset: 0x0001694F
		private bool InternalUpdateProperties()
		{
			return ADSchemaDataProvider.Instance.UpdateProperties(base.AllProperties);
		}

		// Token: 0x0400017B RID: 379
		private Dictionary<string, ADPropertyDefinition> allPropertiesDictionary;

		// Token: 0x0400017C RID: 380
		private Dictionary<string, ADPropertyDefinition> allFilterOnlyPropertiesDictionary;

		// Token: 0x0400017D RID: 381
		private string[] ldapAttributes;

		// Token: 0x0400017E RID: 382
		private ReadOnlyCollection<ADPropertyDefinition> allADObjectLinkProperties;

		// Token: 0x0400017F RID: 383
		private bool hasAutogeneratedConstraints;

		// Token: 0x04000180 RID: 384
		private object constraintsLock = new object();

		// Token: 0x04000181 RID: 385
		public static readonly ADPropertyDefinition Id = new ADPropertyDefinition("Id", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "distinguishedName", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.Id, null);

		// Token: 0x04000182 RID: 386
		public static readonly ADPropertyDefinition ExchangeVersion = new ADPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), "msExchVersion", ADPropertyDefinitionFlags.DoNotProvisionalClone, ExchangeObjectVersion.Exchange2003, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000183 RID: 387
		public static readonly ADPropertyDefinition RawName = new ADPropertyDefinition("RawName", ExchangeObjectVersion.Exchange2003, typeof(string), "name", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000184 RID: 388
		public static readonly ADPropertyDefinition ObjectCategory = new ADPropertyDefinition("ObjectCategory", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "objectCategory", ADPropertyDefinitionFlags.WriteOnce | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000185 RID: 389
		public static readonly ADPropertyDefinition ObjectClass = new ADPropertyDefinition("ObjectClass", ExchangeObjectVersion.Exchange2003, typeof(string), "objectClass", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000186 RID: 390
		public static readonly ADPropertyDefinition ObjectState = new ADPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2003, typeof(ObjectState), null, ADPropertyDefinitionFlags.TaskPopulated, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000187 RID: 391
		public static readonly ADPropertyDefinition OriginatingServer = new ADPropertyDefinition("OriginatingServer", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000188 RID: 392
		internal static readonly ADPropertyDefinition RawCanonicalName = new ADPropertyDefinition("RawCanonicalName", ExchangeObjectVersion.Exchange2003, typeof(string), "canonicalName", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000189 RID: 393
		public static readonly ADPropertyDefinition WhenCreatedRaw = new ADPropertyDefinition("WhenCreatedRaw", ExchangeObjectVersion.Exchange2003, typeof(string), "whenCreated", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400018A RID: 394
		public static readonly ADPropertyDefinition WhenChangedRaw = new ADPropertyDefinition("WhenChangedRaw", ExchangeObjectVersion.Exchange2003, typeof(string), "whenChanged", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400018B RID: 395
		public static readonly ADPropertyDefinition WhenCreated = new ADPropertyDefinition("WhenCreated", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "whenCreated", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.WhenCreatedRaw
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.WhenCreatedGetter), null, null, null);

		// Token: 0x0400018C RID: 396
		public static readonly ADPropertyDefinition WhenCreatedUTC = new ADPropertyDefinition("WhenCreatedUTC", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "whenCreated", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.WhenCreatedRaw
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.WhenCreatedUTCGetter), null, null, null);

		// Token: 0x0400018D RID: 397
		public static readonly ADPropertyDefinition WhenChanged = new ADPropertyDefinition("WhenChanged", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "whenChanged", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.WhenChangedRaw
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.WhenChangedGetter), null, null, null);

		// Token: 0x0400018E RID: 398
		public static readonly ADPropertyDefinition WhenChangedUTC = new ADPropertyDefinition("WhenChangedUTC", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "whenChanged", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.WhenChangedRaw
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.WhenChangedUTCGetter), null, null, null);

		// Token: 0x0400018F RID: 399
		public static readonly ADPropertyDefinition DistinguishedName = new ADPropertyDefinition("DistinguishedName", ExchangeObjectVersion.Exchange2003, typeof(string), "distinguishedName", ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.ForestSpecific, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, int.MaxValue)
		}, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.DistinguishedNameGetter), new SetterDelegate(ADObject.DistinguishedNameSetter), MServRecipientSchema.DistinguishedName, null);

		// Token: 0x04000190 RID: 400
		public static readonly ADPropertyDefinition Guid = new ADPropertyDefinition("Guid", ExchangeObjectVersion.Exchange2003, typeof(Guid), "objectGuid", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.GuidGetter), null, MServRecipientSchema.Guid, null);

		// Token: 0x04000191 RID: 401
		public static readonly ADPropertyDefinition NTSecurityDescriptor = new ADPropertyDefinition("NTSecurityDescriptor", ExchangeObjectVersion.Exchange2003, typeof(SecurityDescriptor), "ntSecurityDescriptor", ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.ForestSpecific, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000192 RID: 402
		public static readonly ADPropertyDefinition CanonicalName = new ADPropertyDefinition("CanonicalName", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawCanonicalName
		}, null, new GetterDelegate(ADObject.CanonicalNameGetter), null, MServRecipientSchema.CanonicalName, null);

		// Token: 0x04000193 RID: 403
		public static readonly ADPropertyDefinition Name = new ADPropertyDefinition("Name", ExchangeObjectVersion.Exchange2003, typeof(string), "name", ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new ADObjectNameStringLengthConstraint(1, 64),
			new ContainingNonWhitespaceConstraint(),
			new ADObjectNameCharacterConstraint(new char[]
			{
				'\0',
				'\n'
			})
		}, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawName
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(ADObject.NameGetter), new SetterDelegate(ADObject.NameSetter), MServRecipientSchema.Name, null);

		// Token: 0x04000194 RID: 404
		public static readonly ADPropertyDefinition OriginalPrimarySmtpAddress = new ADPropertyDefinition("OriginalPrimarySmtpAddress", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), null, ADPropertyDefinitionFlags.TaskPopulated, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000195 RID: 405
		public static readonly ADPropertyDefinition OriginalWindowsEmailAddress = new ADPropertyDefinition("OriginalWindowsEmailAddress", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), null, ADPropertyDefinitionFlags.TaskPopulated, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000196 RID: 406
		public static readonly ADPropertyDefinition OrganizationalUnitRoot = new ADPropertyDefinition("OrganizationalUnitRoot", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchOURoot", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000197 RID: 407
		public static readonly ADPropertyDefinition ConfigurationUnit = new ADPropertyDefinition("ConfigurationUnit", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchCU", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000198 RID: 408
		public static readonly ADPropertyDefinition OrganizationId = new ADPropertyDefinition("OrganizationId", ExchangeObjectVersion.Exchange2003, typeof(OrganizationId), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.OrganizationalUnitRoot,
			ADObjectSchema.ConfigurationUnit
		}, null, new GetterDelegate(ADObject.OrganizationIdGetter), new SetterDelegate(ADObject.OrganizationIdSetter), null, null);

		// Token: 0x04000199 RID: 409
		internal static readonly ADPropertyDefinition SharedConfiguration = new ADPropertyDefinition("SharedConfiguration", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.TaskPopulated | ADPropertyDefinitionFlags.ValidateInSharedConfig, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400019A RID: 410
		public static readonly ADPropertyDefinition ReplicationSignature = new ADPropertyDefinition("ReplicationSignature", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "ReplicationSignature", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400019B RID: 411
		public static readonly ADPropertyDefinition CorrelationIdRaw = new ADPropertyDefinition("CorrelationIdRaw", ExchangeObjectVersion.Exchange2003, typeof(Guid), "msExchCorrelationId", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400019C RID: 412
		public static readonly ADPropertyDefinition CorrelationId = new ADPropertyDefinition("CorrelationId", ExchangeObjectVersion.Exchange2003, typeof(Guid), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.CorrelationIdRaw,
			ADObjectSchema.Id
		}, new CustomFilterBuilderDelegate(ADObject.CorrelationIdFilterBuilder), new GetterDelegate(ADObject.CorrelationIdGetter), new SetterDelegate(ADObject.CorrelationIdSetter), MServRecipientSchema.CorrelationId, null);

		// Token: 0x0400019D RID: 413
		public static readonly ADPropertyDefinition ExchangeObjectIdRaw = new ADPropertyDefinition("ExchangeObjectIdRaw", ExchangeObjectVersion.Exchange2003, typeof(Guid), "msExchObjectID", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400019E RID: 414
		public static readonly ADPropertyDefinition ExchangeObjectId = new ADPropertyDefinition("ExchangeObjectId", ExchangeObjectVersion.Exchange2003, typeof(Guid), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ExchangeObjectIdRaw,
			ADObjectSchema.Id
		}, new CustomFilterBuilderDelegate(ADObject.ExchangeObjectIdFilterBuilder), new GetterDelegate(ADObject.ExchangeObjectIdGetter), new SetterDelegate(ADObject.ExchangeObjectIdSetter), MServRecipientSchema.ExchangeObjectId, null);

		// Token: 0x0400019F RID: 415
		internal static readonly ADPropertyDefinition WhenReadUTC = new ADPropertyDefinition("WhenReadUTC", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040001A0 RID: 416
		internal static readonly ADPropertyDefinition IsCached = new ADPropertyDefinition("IsCached", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040001A1 RID: 417
		internal static readonly ADPropertyDefinition DirectoryBackendType = new ADPropertyDefinition("DirectoryBackendType", ExchangeObjectVersion.Exchange2003, typeof(DirectoryBackendType), null, ADPropertyDefinitionFlags.TaskPopulated, Microsoft.Exchange.Data.Directory.DirectoryBackendType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}
