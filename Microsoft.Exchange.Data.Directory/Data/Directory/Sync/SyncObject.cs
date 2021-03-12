using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000816 RID: 2070
	internal abstract class SyncObject : IPropertyBag, IReadOnlyPropertyBag, ICloneable
	{
		// Token: 0x06006644 RID: 26180 RVA: 0x001697E2 File Offset: 0x001679E2
		public SyncObject(SyncDirection syncDirection)
		{
			this.propertyBag = new ADPropertyBag();
			this.propertyBag.SetObjectVersion(ExchangeObjectVersion.Exchange2010);
			this.SyncDirection = syncDirection;
		}

		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06006645 RID: 26181
		public abstract SyncObjectSchema Schema { get; }

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x0016980C File Offset: 0x00167A0C
		// (set) Token: 0x06006647 RID: 26183 RVA: 0x0016981E File Offset: 0x00167A1E
		public bool IsDeleted
		{
			get
			{
				return (bool)this[SyncObjectSchema.Deleted];
			}
			set
			{
				this[SyncObjectSchema.Deleted] = value;
			}
		}

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x06006648 RID: 26184 RVA: 0x00169831 File Offset: 0x00167A31
		public string ContextId
		{
			get
			{
				return (string)this[SyncObjectSchema.ContextId];
			}
		}

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x06006649 RID: 26185 RVA: 0x00169843 File Offset: 0x00167A43
		public bool HasBaseProperties
		{
			get
			{
				return this.hasBaseProperties;
			}
		}

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x0016984B File Offset: 0x00167A4B
		public bool HasLinkedProperties
		{
			get
			{
				return this.hasLinkedProperties;
			}
		}

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x0600664B RID: 26187 RVA: 0x00169853 File Offset: 0x00167A53
		// (set) Token: 0x0600664C RID: 26188 RVA: 0x0016985B File Offset: 0x00167A5B
		public Guid BatchId { get; private set; }

		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x0600664D RID: 26189 RVA: 0x00169864 File Offset: 0x00167A64
		public string ObjectId
		{
			get
			{
				return (string)this[SyncObjectSchema.ObjectId];
			}
		}

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x0600664E RID: 26190 RVA: 0x00169876 File Offset: 0x00167A76
		public ADObjectId Id
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.Id];
			}
		}

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x0600664F RID: 26191 RVA: 0x00169888 File Offset: 0x00167A88
		// (set) Token: 0x06006650 RID: 26192 RVA: 0x0016989A File Offset: 0x00167A9A
		public bool All
		{
			get
			{
				return (bool)this[SyncObjectSchema.All];
			}
			private set
			{
				this[SyncObjectSchema.All] = value;
			}
		}

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x06006651 RID: 26193 RVA: 0x001698AD File Offset: 0x00167AAD
		// (set) Token: 0x06006652 RID: 26194 RVA: 0x001698BF File Offset: 0x00167ABF
		public MultiValuedProperty<ValidationError> PropertyValidationErrors
		{
			get
			{
				return (MultiValuedProperty<ValidationError>)this[SyncObjectSchema.PropertyValidationErrors];
			}
			private set
			{
				this[SyncObjectSchema.PropertyValidationErrors] = value;
			}
		}

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x06006653 RID: 26195 RVA: 0x001698CD File Offset: 0x00167ACD
		// (set) Token: 0x06006654 RID: 26196 RVA: 0x001698D5 File Offset: 0x00167AD5
		public SyncState SyncState { get; set; }

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x06006655 RID: 26197 RVA: 0x001698DE File Offset: 0x00167ADE
		// (set) Token: 0x06006656 RID: 26198 RVA: 0x001698E6 File Offset: 0x00167AE6
		public SyncDirection SyncDirection { get; private set; }

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06006657 RID: 26199 RVA: 0x001698EF File Offset: 0x00167AEF
		public ServiceInstanceId FaultInServiceInstance
		{
			get
			{
				return (ServiceInstanceId)this[SyncObjectSchema.FaultInServiceInstance];
			}
		}

		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06006658 RID: 26200
		internal abstract DirectoryObjectClass ObjectClass { get; }

		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x06006659 RID: 26201 RVA: 0x00169904 File Offset: 0x00167B04
		private static XmlReaderSettings SyncObjectXmlSettings
		{
			get
			{
				if (SyncObject.syncObjectXmlSettings == null)
				{
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
					xmlReaderSettings.ValidationType = ValidationType.Schema;
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "Annotations.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "DirectoryChange.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "DirectorySync.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "DirectorySync2.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "DirectorySyncMetadata.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "Serialization.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "Serialization.Arrays.xsd"));
					xmlSchemaSet.Add(SyncObject.LoadSchema(executingAssembly, "System.xsd"));
					xmlReaderSettings.Schemas.Add(xmlSchemaSet);
					xmlReaderSettings.Schemas.Compile();
					SyncObject.syncObjectXmlSettings = xmlReaderSettings;
				}
				return SyncObject.syncObjectXmlSettings;
			}
		}

		// Token: 0x17002424 RID: 9252
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				SyncPropertyDefinition syncPropertyDefinition = propertyDefinition as SyncPropertyDefinition;
				if (syncPropertyDefinition == null || !syncPropertyDefinition.IsForwardSync || (syncPropertyDefinition.Flags & SyncPropertyDefinitionFlags.Ignore) != (SyncPropertyDefinitionFlags)0)
				{
					return this.propertyBag[propertyDefinition];
				}
				if (this.IsPropertyPresent(syncPropertyDefinition))
				{
					return SyncPropertyFactory.Create(syncPropertyDefinition.Type, this.propertyBag[syncPropertyDefinition], syncPropertyDefinition.IsMultivalued);
				}
				return SyncPropertyFactory.CreateDefault(syncPropertyDefinition.Type, syncPropertyDefinition.IsMultivalued);
			}
			set
			{
				ISyncProperty syncProperty = value as ISyncProperty;
				this.propertyBag[propertyDefinition] = ((syncProperty == null) ? value : syncProperty.GetValue());
			}
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x00169A88 File Offset: 0x00167C88
		public static SyncObject Create(DirectoryObject directoryObject, IList<DirectoryLink> directoryLinks, Guid batchId)
		{
			bool flag = directoryObject != null;
			bool flag2 = directoryLinks != null && directoryLinks.Count != 0;
			if (!flag && !flag2)
			{
				throw new ArgumentException("directoryObject");
			}
			DirectoryObjectClass objectClass;
			if (directoryObject != null)
			{
				objectClass = SyncObject.GetObjectClass(directoryObject);
			}
			else
			{
				objectClass = directoryLinks[0].GetSourceClass();
			}
			SyncObject syncObject = SyncObject.CreateBlankObjectByClass(objectClass, SyncDirection.Forward);
			syncObject.BatchId = batchId;
			if (directoryObject != null)
			{
				syncObject.PopulatePropertyBag<string>(SyncObjectSchema.ContextId, directoryObject.ContextId);
				syncObject.PopulatePropertyBag<bool>(SyncObjectSchema.Deleted, directoryObject.Deleted);
				syncObject.PopulatePropertyBag<string>(SyncObjectSchema.ObjectId, directoryObject.ObjectId);
				syncObject.PopulatePropertyBag<bool>(SyncObjectSchema.All, directoryObject.All);
				SyncObject.FwdSyncDataConverter fwdSyncDataConverter = new SyncObject.FwdSyncDataConverter(new Action<SyncPropertyDefinition, DirectoryProperty>(syncObject.PopulatePropertyBag), syncObject.propertyBag);
				directoryObject.ForEachProperty(fwdSyncDataConverter);
				flag = (fwdSyncDataConverter.BasePropertiesModified || directoryObject.Deleted);
			}
			else
			{
				syncObject.PopulatePropertyBag<string>(SyncObjectSchema.ContextId, directoryLinks[0].ContextId);
				syncObject.PopulatePropertyBag<bool>(SyncObjectSchema.Deleted, directoryLinks[0].Deleted);
				syncObject.PopulatePropertyBag<string>(SyncObjectSchema.ObjectId, directoryLinks[0].SourceId);
			}
			syncObject.hasBaseProperties = flag;
			syncObject.hasLinkedProperties = flag2;
			if (directoryLinks != null && directoryLinks.Count > 0)
			{
				foreach (DirectoryLink directoryLink in directoryLinks)
				{
					syncObject.AddLinkValue(directoryLink.GetType().Name, directoryLink.Deleted, directoryLink.GetTargetClass(), directoryLink.TargetId);
				}
			}
			return syncObject;
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x00169C2C File Offset: 0x00167E2C
		public static DirectoryObjectClass GetObjectClass(DirectoryObject directoryObject)
		{
			if (directoryObject is User)
			{
				return DirectoryObjectClass.User;
			}
			if (directoryObject is Group)
			{
				return DirectoryObjectClass.Group;
			}
			if (directoryObject is Contact)
			{
				return DirectoryObjectClass.Contact;
			}
			if (directoryObject is Company)
			{
				return DirectoryObjectClass.Company;
			}
			if (directoryObject is ForeignPrincipal)
			{
				return DirectoryObjectClass.ForeignPrincipal;
			}
			if (directoryObject is SubscribedPlan)
			{
				return DirectoryObjectClass.SubscribedPlan;
			}
			if (directoryObject is Account)
			{
				return DirectoryObjectClass.Account;
			}
			throw new NotSupportedException(string.Format("Not supported object type {0}.", directoryObject.GetType().Name));
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x00169C9A File Offset: 0x00167E9A
		public virtual bool IsValid(bool isFullSyncObject)
		{
			return true;
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x00169CA0 File Offset: 0x00167EA0
		public List<DirectoryLink> GetDirectoryLinks()
		{
			List<DirectoryLink> list = new List<DirectoryLink>();
			IDictionary<SyncPropertyDefinition, object> changedProperties = this.GetChangedProperties(SyncSchema.Instance.AllBackSyncLinkedProperties);
			foreach (SyncPropertyDefinition syncPropertyDefinition in changedProperties.Keys)
			{
				if (!(syncPropertyDefinition.ExternalType == typeof(object)))
				{
					MultiValuedProperty<SyncLink> multiValuedProperty = null;
					if (syncPropertyDefinition.IsMultivalued)
					{
						multiValuedProperty = (MultiValuedProperty<SyncLink>)changedProperties[syncPropertyDefinition];
					}
					else if (changedProperties[syncPropertyDefinition] != null)
					{
						multiValuedProperty = new MultiValuedProperty<SyncLink>((SyncLink)changedProperties[syncPropertyDefinition]);
					}
					if (multiValuedProperty != null)
					{
						ICollection<DirectoryLink> directoryLinks = SyncObject.GetDirectoryLinks(this, multiValuedProperty, syncPropertyDefinition);
						list.AddRange(directoryLinks);
					}
				}
			}
			return list;
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x00169D64 File Offset: 0x00167F64
		public void RemoveProperty(SyncPropertyDefinition propertyDefinition)
		{
			this.propertyBag.Remove(propertyDefinition);
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x00169D72 File Offset: 0x00167F72
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x00169D79 File Offset: 0x00167F79
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			this.propertyBag.SetProperties(propertyDefinitionArray, propertyValuesArray);
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x00169D88 File Offset: 0x00167F88
		public void PopulatePropertyBag(SyncPropertyDefinition propertyDefinition, DirectoryProperty values)
		{
			if (!propertyDefinition.IsForwardSync)
			{
				return;
			}
			if (values != null)
			{
				List<ValidationError> list = new List<ValidationError>();
				object obj = SyncValueConvertor.GetValuesFromDirectoryProperty(propertyDefinition, values, list);
				foreach (ValidationError item in list)
				{
					this.PropertyValidationErrors.Add(item);
				}
				if (propertyDefinition.Equals(SyncRecipientSchema.EmailAddresses) && obj != null)
				{
					ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)obj;
					ProxyAddressCollection proxyAddressCollection2 = new ProxyAddressCollection();
					foreach (ProxyAddress proxyAddress in proxyAddressCollection)
					{
						if (!proxyAddressCollection2.Contains(proxyAddress))
						{
							if (!(proxyAddress is InvalidProxyAddress))
							{
								proxyAddressCollection2.Add(proxyAddress);
							}
							else
							{
								Globals.LogEvent(DirectoryEventLogConstants.Tuple_SyncObjectInvalidProxyAddressStripped, this.ObjectId, new object[]
								{
									proxyAddress.ToString(),
									this.ObjectId,
									this.ContextId
								});
							}
						}
					}
					obj = proxyAddressCollection2;
				}
				if (propertyDefinition.Equals(SyncUserSchema.WindowsLiveID) && obj != null)
				{
					string text = obj.ToString();
					string text2 = this.ObjectId.Replace("-", string.Empty);
					if (text.StartsWith(text2, StringComparison.OrdinalIgnoreCase))
					{
						obj = new SmtpAddress(text.Substring(text2.Length));
					}
				}
				if (obj != null || list.Count == 0)
				{
					this.propertyBag[propertyDefinition] = obj;
				}
			}
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x00169F20 File Offset: 0x00168120
		public void PopulatePropertyBag<T>(SyncPropertyDefinition propertyDefinition, T value)
		{
			this.propertyBag[propertyDefinition] = value;
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x00169F34 File Offset: 0x00168134
		public void PopulateForwardSyncDataFromPropertyBag(ADRawEntry adRawEntry)
		{
			foreach (ADPropertyDefinition propertyDefinition in SyncObject.ForwardSyncProperties)
			{
				this[propertyDefinition] = adRawEntry[propertyDefinition];
			}
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x00169F88 File Offset: 0x00168188
		public virtual object Clone()
		{
			SyncObject syncObject = SyncObject.CreateBlankObjectByClass(this.ObjectClass, this.SyncDirection);
			syncObject.propertyBag = (ADPropertyBag)this.propertyBag.Clone();
			syncObject.hasBaseProperties = this.hasBaseProperties;
			syncObject.hasLinkedProperties = this.hasLinkedProperties;
			return syncObject;
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x00169FD6 File Offset: 0x001681D6
		public virtual void CopyForwardChangeFrom(SyncObject sourceObject)
		{
			this.CopyChangeFrom(sourceObject, null);
			this.All = (this.All || sourceObject.All);
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x0016A038 File Offset: 0x00168238
		public void CopyChangeFrom(SyncObject sourceObject, SyncPropertyDefinition[] properties)
		{
			IDictionary<SyncPropertyDefinition, object> changedProperties = sourceObject.GetChangedProperties();
			using (IEnumerator<KeyValuePair<SyncPropertyDefinition, object>> enumerator = changedProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<SyncPropertyDefinition, object> property = enumerator.Current;
					if (properties != null && properties.Length != 0)
					{
						if (!Array.Exists<SyncPropertyDefinition>(properties, delegate(SyncPropertyDefinition x)
						{
							KeyValuePair<SyncPropertyDefinition, object> property7 = property;
							return x.Equals(property7.Key);
						}))
						{
							continue;
						}
					}
					KeyValuePair<SyncPropertyDefinition, object> property8 = property;
					if (!property8.Key.IsCalculated)
					{
						goto IL_127;
					}
					KeyValuePair<SyncPropertyDefinition, object> property2 = property;
					if (property2.Key.IsReadOnly)
					{
						KeyValuePair<SyncPropertyDefinition, object> property3 = property;
						using (ReadOnlyCollection<ProviderPropertyDefinition>.Enumerator enumerator2 = property3.Key.SupportingProperties.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ProviderPropertyDefinition providerPropertyDefinition = enumerator2.Current;
								SyncPropertyDefinition key = providerPropertyDefinition as SyncPropertyDefinition;
								if (key != null && properties != null && properties.Length != 0 && !Array.Exists<SyncPropertyDefinition>(properties, (SyncPropertyDefinition x) => x.Equals(key)) && changedProperties.ContainsKey(key))
								{
									this[key] = changedProperties[key];
								}
							}
							goto IL_14D;
						}
						goto IL_127;
					}
					goto IL_127;
					IL_14D:
					KeyValuePair<SyncPropertyDefinition, object> property4 = property;
					if (property4.Key.IsSyncLink)
					{
						this.hasLinkedProperties = true;
						continue;
					}
					this.hasBaseProperties = true;
					continue;
					IL_127:
					KeyValuePair<SyncPropertyDefinition, object> property5 = property;
					PropertyDefinition key2 = property5.Key;
					KeyValuePair<SyncPropertyDefinition, object> property6 = property;
					this[key2] = property6.Value;
					goto IL_14D;
				}
			}
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x0016A208 File Offset: 0x00168408
		public IDictionary<SyncPropertyDefinition, object> GetChangedProperties()
		{
			ICollection<SyncPropertyDefinition> properties;
			switch (this.SyncDirection)
			{
			case SyncDirection.Forward:
				properties = this.Schema.AllForwardSyncProperties;
				break;
			case SyncDirection.Back:
				properties = this.Schema.AllBackSyncProperties;
				break;
			default:
				throw new ArgumentOutOfRangeException("syncDirection");
			}
			return this.GetChangedProperties(properties);
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x0016A25C File Offset: 0x0016845C
		internal static SyncObject Create(ADPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass];
			DirectoryObjectClass directoryObjectClass;
			SyncObject syncObject;
			if (multiValuedProperty.Contains(ADOrganizationalUnit.MostDerivedClass))
			{
				directoryObjectClass = DirectoryObjectClass.Company;
				syncObject = new SyncCompany(SyncDirection.Back);
			}
			else
			{
				directoryObjectClass = SyncRecipient.GetRecipientType(propertyBag);
				DirectoryObjectClass directoryObjectClass2 = directoryObjectClass;
				if (directoryObjectClass2 != DirectoryObjectClass.Contact)
				{
					if (directoryObjectClass2 != DirectoryObjectClass.Group)
					{
						if (directoryObjectClass2 != DirectoryObjectClass.User)
						{
							throw new InvalidOperationException("Unexpected object type");
						}
						syncObject = new SyncUser(SyncDirection.Back);
					}
					else
					{
						syncObject = new SyncGroup(SyncDirection.Back);
					}
				}
				else
				{
					syncObject = new SyncContact(SyncDirection.Back);
				}
			}
			syncObject.propertyBag = propertyBag;
			DirectoryObject directoryObject = syncObject.CreateDirectoryObject();
			SyncObject.BasePropertyModificationChecker basePropertyModificationChecker = new SyncObject.BasePropertyModificationChecker(directoryObjectClass, propertyBag);
			directoryObject.ForEachProperty(basePropertyModificationChecker);
			syncObject.hasBaseProperties = basePropertyModificationChecker.BasePropertiesModified;
			syncObject.hasLinkedProperties = (syncObject.GetChangedProperties(SyncSchema.Instance.AllBackSyncLinkedProperties).Count > 0);
			syncObject.hasLinkedProperties |= (syncObject.GetChangedProperties(SyncSchema.Instance.AllBackSyncShadowLinkedProperties).Count > 0);
			return syncObject;
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x0016A342 File Offset: 0x00168542
		internal static string SerializeResponse(object response)
		{
			return SyncObject.SerializeResponse(response, false);
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x0016A34C File Offset: 0x0016854C
		internal static string SerializeResponse(object response, bool validateWithSchema)
		{
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add("timest", "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01");
			XmlSerializer xmlSerializer = new XmlSerializer(response.GetType(), "http://schemas.microsoft.com/online/directoryservices/sync/2008/11");
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = true,
				IndentChars = "  "
			}))
			{
				xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
				xmlSerializer.Serialize(xmlWriter, response, xmlSerializerNamespaces);
			}
			stringBuilder.Replace("_X", "&#x005f;&#x0058;");
			string text = stringBuilder.ToString();
			if (validateWithSchema)
			{
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(text), SyncObject.SyncObjectXmlSettings))
				{
					while (xmlReader.Read())
					{
					}
				}
			}
			return text;
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x0016A43C File Offset: 0x0016863C
		internal static GetChangesResponse CreateGetChangesResponse(IEnumerable<SyncObject> objects, bool moreData, byte[] cookie, ServiceInstanceId currentServiceInstanceId)
		{
			DirectoryChanges directoryChanges = new DirectoryChanges();
			List<DirectoryObject> list = new List<DirectoryObject>();
			List<DirectoryLink> list2 = new List<DirectoryLink>();
			SyncObject.AddObjectAndLinksWithChanges(objects, list, list2, currentServiceInstanceId);
			directoryChanges.Links = list2.ToArray();
			directoryChanges.Objects = list.ToArray();
			directoryChanges.Contexts = new DirectoryContext[0];
			directoryChanges.More = moreData;
			directoryChanges.NextCookie = cookie;
			return new GetChangesResponse(directoryChanges);
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x0016A49C File Offset: 0x0016869C
		internal static GetDirectoryObjectsResponse CreateGetDirectoryObjectsResponse(IEnumerable<SyncObject> objects, bool moreData, byte[] pageToken, DirectoryObjectError[] errors, ServiceInstanceId currentServiceInstanceId)
		{
			DirectoryObjectsAndLinks directoryObjectsAndLinks = new DirectoryObjectsAndLinks();
			List<DirectoryObject> list = new List<DirectoryObject>();
			List<DirectoryLink> list2 = new List<DirectoryLink>();
			SyncObject.AddObjectAndLinksWithChanges(objects, list, list2, currentServiceInstanceId);
			SyncObject.RemoveDuplicates(list, list2);
			directoryObjectsAndLinks.Links = list2.ToArray();
			directoryObjectsAndLinks.Objects = list.ToArray();
			directoryObjectsAndLinks.More = moreData;
			directoryObjectsAndLinks.NextPageToken = pageToken;
			directoryObjectsAndLinks.Errors = (errors ?? new DirectoryObjectError[0]);
			return new GetDirectoryObjectsResponse(directoryObjectsAndLinks);
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x0016A50C File Offset: 0x0016870C
		internal bool IsPropertyPresent(ADPropertyDefinition property)
		{
			if (this.propertyBag.Contains(property))
			{
				return true;
			}
			if (property.IsCalculated)
			{
				foreach (ProviderPropertyDefinition providerPropertyDefinition in property.SupportingProperties)
				{
					ADPropertyDefinition key = (ADPropertyDefinition)providerPropertyDefinition;
					if (this.propertyBag.Contains(key))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x0016A58C File Offset: 0x0016878C
		internal DirectoryObject ToDirectoryObject()
		{
			DirectoryObject directoryObject = this.CreateDirectoryObject();
			directoryObject.ContextId = this.ContextId;
			directoryObject.ObjectId = this.ObjectId;
			directoryObject.Deleted = this.IsDeleted;
			if (!this.IsDeleted)
			{
				directoryObject.ForEachProperty(new SyncObject.BackSyncDataConverter(this.ObjectClass, this.propertyBag));
			}
			return directoryObject;
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x0016A5E4 File Offset: 0x001687E4
		protected static SyncObject CreateBlankObjectByClass(DirectoryObjectClass objectClass, SyncDirection syncDirection)
		{
			switch (objectClass)
			{
			case DirectoryObjectClass.Account:
				return new SyncAccount(syncDirection);
			case DirectoryObjectClass.Company:
				return new SyncCompany(syncDirection);
			case DirectoryObjectClass.Contact:
				return new SyncContact(syncDirection);
			case DirectoryObjectClass.ForeignPrincipal:
				return new SyncForeignPrincipal(syncDirection);
			case DirectoryObjectClass.Group:
				return new SyncGroup(syncDirection);
			case DirectoryObjectClass.SubscribedPlan:
				return new SyncSubscribedPlan(syncDirection);
			case DirectoryObjectClass.User:
				return new SyncUser(syncDirection);
			}
			throw new NotImplementedException(objectClass.ToString());
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x0016A676 File Offset: 0x00168876
		[Conditional("DEBUG")]
		protected static void AssertGetterIsInvokedOnlyIfPropertyIsPresent(IPropertyBag propertyBag, ADPropertyDefinition propertyDefinition)
		{
			if (!(propertyBag is ADPropertyBag))
			{
				throw new DataValidationException(new PropertyValidationError(new LocalizedString("Unknown type"), propertyDefinition, propertyBag));
			}
		}

		// Token: 0x06006673 RID: 26227
		protected abstract DirectoryObject CreateDirectoryObject();

		// Token: 0x06006674 RID: 26228 RVA: 0x0016A698 File Offset: 0x00168898
		private static void AddObjectAndLinksWithChanges(IEnumerable<SyncObject> objects, List<DirectoryObject> directoryObjects, List<DirectoryLink> directoryLinks, ServiceInstanceId currentServiceInstanceId)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (SyncObject syncObject in objects)
			{
				DirectoryObject directoryObject = null;
				if (syncObject.FaultInServiceInstance != null)
				{
					DirectoryObject directoryObject2 = syncObject.CreateDirectoryObject();
					if (directoryObject2 is IValidationErrorSupport)
					{
						directoryObject2.ContextId = syncObject.ContextId;
						directoryObject2.ObjectId = syncObject.ObjectId;
						((IValidationErrorSupport)directoryObject2).ValidationError = SyncObject.CreateFaultinValidationError(currentServiceInstanceId, syncObject.FaultInServiceInstance);
						directoryObject = directoryObject2;
						if (!hashSet.Contains(directoryObject2.ObjectId))
						{
							directoryObjects.Add(directoryObject);
							hashSet.Add(directoryObject2.ObjectId);
						}
					}
				}
				if (directoryObject == null)
				{
					if (syncObject.hasBaseProperties || syncObject.IsDeleted)
					{
						directoryObjects.Add(syncObject.ToDirectoryObject());
					}
					if (syncObject.HasLinkedProperties)
					{
						directoryLinks.AddRange(SyncObject.GetDirectoryLinks(syncObject));
					}
				}
			}
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x0016A7A8 File Offset: 0x001689A8
		private static void RemoveDuplicates(List<DirectoryObject> directoryObjects, List<DirectoryLink> directoryLinks)
		{
			if (directoryObjects.Count == 0 || !directoryObjects[directoryObjects.Count - 1].Deleted)
			{
				return;
			}
			HashSet<string> deletedIds = new HashSet<string>();
			int num = 0;
			for (int i = directoryObjects.Count - 1; i >= 0; i--)
			{
				if (directoryObjects[i].Deleted)
				{
					deletedIds.Add(directoryObjects[i].ObjectId);
				}
				else if (deletedIds.Contains(directoryObjects[i].ObjectId))
				{
					directoryObjects.RemoveAt(i);
					num++;
				}
			}
			int num2 = directoryLinks.RemoveAll((DirectoryLink dirLink) => deletedIds.Contains(dirLink.SourceId));
			if (num > 0 || num2 > 0)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<int, int, int>(0L, "SyncObject.RemoveDuplicates: - Removed {0} duplicate objects and {1} links from the response after analyzing {2} deleted objects", num, num2, deletedIds.Count);
			}
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x0016A880 File Offset: 0x00168A80
		private static IEnumerable<DirectoryLink> GetDirectoryLinks(SyncObject syncObject)
		{
			List<DirectoryLink> result = new List<DirectoryLink>();
			SyncObject.AddNonShadowLinks(syncObject, result);
			SyncObject.AddShadowLinks(syncObject, result);
			return result;
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x0016A8A4 File Offset: 0x00168AA4
		private static void AddShadowLinks(SyncObject syncObject, List<DirectoryLink> result)
		{
			if (syncObject.ObjectClass == DirectoryObjectClass.User)
			{
				IDictionary<SyncPropertyDefinition, object> changedProperties = syncObject.GetChangedProperties(SyncSchema.Instance.AllBackSyncShadowLinkedProperties);
				foreach (SyncPropertyDefinition syncPropertyDefinition in changedProperties.Keys)
				{
					MultiValuedProperty<SyncLink> multiValuedProperty = (MultiValuedProperty<SyncLink>)syncObject.propertyBag[syncPropertyDefinition];
					if (multiValuedProperty != null)
					{
						ICollection<DirectoryLink> directoryLinks = SyncObject.GetDirectoryLinks(syncObject, multiValuedProperty, syncPropertyDefinition);
						if (directoryLinks.Count > 0)
						{
							DateTime timestamp = (DateTime)(syncObject[ADRecipientSchema.LastExchangeChangedTime] ?? DateTime.MinValue);
							foreach (DirectoryLink directoryLink in directoryLinks)
							{
								Manager manager = (Manager)directoryLink;
								manager.Timestamp = timestamp;
								manager.TimestampSpecified = true;
							}
						}
						result.AddRange(directoryLinks);
					}
				}
			}
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x0016A9B4 File Offset: 0x00168BB4
		private static void AddNonShadowLinks(SyncObject syncObject, List<DirectoryLink> result)
		{
			IDictionary<SyncPropertyDefinition, object> changedProperties = syncObject.GetChangedProperties(SyncSchema.Instance.AllBackSyncLinkedProperties);
			foreach (SyncPropertyDefinition syncPropertyDefinition in changedProperties.Keys)
			{
				if (!(syncPropertyDefinition.ExternalType == typeof(object)) && !SyncObject.IsMultimasteredLink(syncObject, syncPropertyDefinition) && ((SyncConfiguration.EnableSyncingBackCloudLinks() && syncPropertyDefinition.IsCloud) || !SyncObject.IsDirsyncedObject(syncObject.propertyBag)))
				{
					MultiValuedProperty<SyncLink> multiValuedProperty = (MultiValuedProperty<SyncLink>)syncObject.propertyBag[syncPropertyDefinition];
					if (multiValuedProperty != null)
					{
						ICollection<DirectoryLink> directoryLinks = SyncObject.GetDirectoryLinks(syncObject, multiValuedProperty, syncPropertyDefinition);
						result.AddRange(directoryLinks);
					}
				}
			}
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x0016AA78 File Offset: 0x00168C78
		private static bool IsDirsyncedObject(ADPropertyBag propertyBag)
		{
			return SyncObject.IsBackSyncRecipientDirSynced(propertyBag) && SyncObject.IsBackSyncOrganizationDirSyncRunning(propertyBag);
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x0016AA8C File Offset: 0x00168C8C
		private static bool IsBackSyncRecipientDirSynced(ADPropertyBag propertyBag)
		{
			bool isDirSynced = (bool)propertyBag[ADRecipientSchema.IsDirSynced];
			return ADObject.IsRecipientDirSynced(isDirSynced);
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x0016AAB0 File Offset: 0x00168CB0
		private static bool IsBackSyncOrganizationDirSyncRunning(ADPropertyBag propertyBag)
		{
			bool isDirSyncRunning = (bool)propertyBag[OrganizationSchema.IsDirSyncRunning];
			MultiValuedProperty<string> dirSyncStatus = (MultiValuedProperty<string>)propertyBag[OrganizationSchema.DirSyncStatus];
			return ExchangeConfigurationUnit.IsOrganizationDirSyncRunning(isDirSyncRunning, dirSyncStatus, new List<DirSyncState>
			{
				DirSyncState.Disabled,
				DirSyncState.PendingEnabled
			});
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x0016AAFB File Offset: 0x00168CFB
		private static bool IsMultimasteredLink(SyncObject syncObject, SyncPropertyDefinition propertyDefinition)
		{
			return propertyDefinition == SyncUserSchema.Manager && syncObject.ObjectClass == DirectoryObjectClass.User;
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x0016AB14 File Offset: 0x00168D14
		private static ICollection<DirectoryLink> GetDirectoryLinks(SyncObject syncObject, IEnumerable<SyncLink> links, SyncPropertyDefinition propertyDefinition)
		{
			List<DirectoryLink> list = new List<DirectoryLink>();
			foreach (SyncLink syncLink in links)
			{
				DirectoryLink directoryLink = (DirectoryLink)Activator.CreateInstance(propertyDefinition.ExternalType);
				directoryLink.Deleted = (syncLink.State == LinkState.Removed);
				directoryLink.ContextId = syncObject.ContextId;
				directoryLink.SourceId = syncObject.ObjectId;
				directoryLink.TargetId = syncLink.TargetId;
				directoryLink.SetSourceClass(syncObject.ObjectClass);
				directoryLink.SetTargetClass(syncLink.TargetObjectClass);
				list.Add(directoryLink);
			}
			return list;
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0016ABC0 File Offset: 0x00168DC0
		private static XmlSchema LoadSchema(Assembly assembly, string schemaName)
		{
			XmlSchema result;
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(schemaName))
			{
				result = SafeXmlSchema.Read(manifestResourceStream, null);
			}
			return result;
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x0016ABFC File Offset: 0x00168DFC
		private static DirectoryPropertyXmlValidationError CreateFaultinValidationError(ServiceInstanceId sourceServiceInstanceId, ServiceInstanceId targetServiceInstanceId)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Migrated");
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("ServiceInstance");
			xmlAttribute.Value = targetServiceInstanceId.InstanceId;
			xmlElement.Attributes.Append(xmlAttribute);
			XmlValueValidationError xmlValueValidationError = new XmlValueValidationError();
			xmlValueValidationError.ErrorInfo = new ValidationErrorValue();
			xmlValueValidationError.ErrorInfo.ServiceInstance = sourceServiceInstanceId.InstanceId;
			xmlValueValidationError.ErrorInfo.Resolved = false;
			xmlValueValidationError.ErrorInfo.Timestamp = DateTime.UtcNow;
			xmlValueValidationError.ErrorInfo.ErrorDetail = xmlElement;
			return new DirectoryPropertyXmlValidationError
			{
				Value = new XmlValueValidationError[]
				{
					xmlValueValidationError
				}
			};
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x0016ACAC File Offset: 0x00168EAC
		private IDictionary<SyncPropertyDefinition, object> GetChangedProperties(ICollection<SyncPropertyDefinition> properties)
		{
			Dictionary<SyncPropertyDefinition, object> dictionary = new Dictionary<SyncPropertyDefinition, object>(properties.Count);
			foreach (SyncPropertyDefinition syncPropertyDefinition in properties)
			{
				if (this.IsPropertyPresent(syncPropertyDefinition))
				{
					dictionary[syncPropertyDefinition] = this.propertyBag[syncPropertyDefinition];
				}
			}
			return dictionary;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x0016AD18 File Offset: 0x00168F18
		private void AddLinkValue(string linkAttributeName, bool isDeleted, DirectoryObjectClass targetClass, string targetId)
		{
			SyncPropertyDefinition syncPropertyDefinition;
			if (!SyncSchema.Instance.TryGetLinkedPropertyDefinitionByMsoPropertyName(linkAttributeName, out syncPropertyDefinition))
			{
				return;
			}
			SyncLink syncLink = new SyncLink(targetId, targetClass, isDeleted ? LinkState.Removed : LinkState.Added);
			if (syncPropertyDefinition.IsMultivalued)
			{
				MultiValuedProperty<SyncLink> multiValuedProperty = (MultiValuedProperty<SyncLink>)this.propertyBag[syncPropertyDefinition];
				multiValuedProperty.TryAdd(syncLink);
				return;
			}
			SyncLink syncLink2 = (SyncLink)this.propertyBag[syncPropertyDefinition];
			if (syncLink2 == null || syncLink2.State == LinkState.Removed)
			{
				this[syncPropertyDefinition] = syncLink;
			}
		}

		// Token: 0x040043A5 RID: 17317
		internal const string MSOTimestampSchemaName = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01";

		// Token: 0x040043A6 RID: 17318
		private const string MSOSchemaName = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11";

		// Token: 0x040043A7 RID: 17319
		private const string XmlSchemaInstancePrefix = "xsi";

		// Token: 0x040043A8 RID: 17320
		private const string XmlSchemaInstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040043A9 RID: 17321
		public static readonly ICollection<ADPropertyDefinition> ForwardSyncProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.RecipientTypeDetails
		};

		// Token: 0x040043AA RID: 17322
		public static readonly ICollection<ADPropertyDefinition> BackSyncProperties = new ADPropertyDefinition[]
		{
			SyncObjectSchema.ObjectId,
			ADRecipientSchema.ExternalDirectoryObjectId,
			ADObjectSchema.ObjectClass,
			ADRecipientSchema.RecipientTypeDetails,
			ADRecipientSchema.AttributeMetadata,
			ADRecipientSchema.LastExchangeChangedTime,
			ADRecipientSchema.IsDirSynced,
			ADRecipientSchema.DirSyncAuthorityMetadata,
			ADRecipientSchema.ExcludedFromBackSync,
			ADObjectSchema.WhenChangedUTC,
			ADRecipientSchema.UsnChanged,
			ADRecipientSchema.ConfigurationXML,
			ExtendedOrganizationalUnitSchema.DirSyncStatusAck
		};

		// Token: 0x040043AB RID: 17323
		public static readonly ICollection<ADPropertyDefinition> FullSyncLinkPageBackSyncProperties = new ADPropertyDefinition[]
		{
			SyncObjectSchema.ObjectId,
			ADRecipientSchema.ExternalDirectoryObjectId,
			ADObjectSchema.ObjectClass,
			ADRecipientSchema.RecipientTypeDetails
		};

		// Token: 0x040043AC RID: 17324
		private static XmlReaderSettings syncObjectXmlSettings;

		// Token: 0x040043AD RID: 17325
		private ADPropertyBag propertyBag;

		// Token: 0x040043AE RID: 17326
		private bool hasBaseProperties;

		// Token: 0x040043AF RID: 17327
		private bool hasLinkedProperties;

		// Token: 0x02000818 RID: 2072
		private abstract class BackSyncDataProcessor : IPropertyProcessor
		{
			// Token: 0x06006684 RID: 26244 RVA: 0x0016AE61 File Offset: 0x00169061
			protected BackSyncDataProcessor(DirectoryObjectClass objectClass, ADPropertyBag propertyBag)
			{
				this.PropertyBag = propertyBag;
				this.isObjectDirsynced = SyncObject.IsDirsyncedObject(propertyBag);
				this.BacksyncShadowProperties = (objectClass == DirectoryObjectClass.User);
			}

			// Token: 0x17002425 RID: 9253
			// (get) Token: 0x06006685 RID: 26245 RVA: 0x0016AE87 File Offset: 0x00169087
			// (set) Token: 0x06006686 RID: 26246 RVA: 0x0016AE8F File Offset: 0x0016908F
			private protected ADPropertyBag PropertyBag { protected get; private set; }

			// Token: 0x17002426 RID: 9254
			// (get) Token: 0x06006687 RID: 26247 RVA: 0x0016AE98 File Offset: 0x00169098
			// (set) Token: 0x06006688 RID: 26248 RVA: 0x0016AEA0 File Offset: 0x001690A0
			private protected bool BacksyncShadowProperties { protected get; private set; }

			// Token: 0x06006689 RID: 26249
			public abstract void Process<T>(SyncPropertyDefinition propertyDefinition, ref T values) where T : DirectoryProperty, new();

			// Token: 0x0600668A RID: 26250 RVA: 0x0016AEA9 File Offset: 0x001690A9
			protected bool ShouldIgnoreProperty(SyncPropertyDefinition propertyDefinition)
			{
				return !propertyDefinition.IsBackSync || (!propertyDefinition.IsCloud && !this.HasShadow(propertyDefinition) && this.isObjectDirsynced);
			}

			// Token: 0x0600668B RID: 26251 RVA: 0x0016AED1 File Offset: 0x001690D1
			protected bool HasShadow(SyncPropertyDefinition definition)
			{
				return this.BacksyncShadowProperties && definition.ShadowProperty != null;
			}

			// Token: 0x040043B3 RID: 17331
			private readonly bool isObjectDirsynced;
		}

		// Token: 0x02000819 RID: 2073
		private class BasePropertyModificationChecker : SyncObject.BackSyncDataProcessor
		{
			// Token: 0x0600668C RID: 26252 RVA: 0x0016AEE9 File Offset: 0x001690E9
			public BasePropertyModificationChecker(DirectoryObjectClass objectClass, ADPropertyBag propertyBag) : base(objectClass, propertyBag)
			{
			}

			// Token: 0x17002427 RID: 9255
			// (get) Token: 0x0600668D RID: 26253 RVA: 0x0016AEF3 File Offset: 0x001690F3
			// (set) Token: 0x0600668E RID: 26254 RVA: 0x0016AEFB File Offset: 0x001690FB
			public bool BasePropertiesModified { get; private set; }

			// Token: 0x0600668F RID: 26255 RVA: 0x0016AF04 File Offset: 0x00169104
			public override void Process<T>(SyncPropertyDefinition propertyDefinition, ref T values)
			{
				if (base.ShouldIgnoreProperty(propertyDefinition))
				{
					return;
				}
				SyncPropertyDefinition property = propertyDefinition;
				if (base.HasShadow(propertyDefinition))
				{
					property = (SyncPropertyDefinition)propertyDefinition.ShadowProperty;
				}
				this.BasePropertiesModified |= ADDirSyncHelper.ContainsProperty(base.PropertyBag, property);
			}
		}

		// Token: 0x0200081A RID: 2074
		private class FwdSyncDataConverter : IPropertyProcessor
		{
			// Token: 0x06006690 RID: 26256 RVA: 0x0016AF4B File Offset: 0x0016914B
			public FwdSyncDataConverter(Action<SyncPropertyDefinition, DirectoryProperty> populate, PropertyBag propertyBag)
			{
				this.populate = populate;
				this.propertyBag = propertyBag;
			}

			// Token: 0x17002428 RID: 9256
			// (get) Token: 0x06006691 RID: 26257 RVA: 0x0016AF61 File Offset: 0x00169161
			// (set) Token: 0x06006692 RID: 26258 RVA: 0x0016AF69 File Offset: 0x00169169
			public bool BasePropertiesModified { get; private set; }

			// Token: 0x06006693 RID: 26259 RVA: 0x0016AF72 File Offset: 0x00169172
			public void Process<T>(SyncPropertyDefinition propertyDefinition, ref T values) where T : DirectoryProperty, new()
			{
				this.populate(propertyDefinition, values);
				this.BasePropertiesModified |= ADDirSyncHelper.ContainsProperty(this.propertyBag, propertyDefinition);
			}

			// Token: 0x040043B7 RID: 17335
			private readonly Action<SyncPropertyDefinition, DirectoryProperty> populate;

			// Token: 0x040043B8 RID: 17336
			private readonly PropertyBag propertyBag;
		}

		// Token: 0x0200081B RID: 2075
		private class BackSyncDataConverter : SyncObject.BackSyncDataProcessor
		{
			// Token: 0x06006694 RID: 26260 RVA: 0x0016AFC4 File Offset: 0x001691C4
			public BackSyncDataConverter(DirectoryObjectClass objectClass, ADPropertyBag propertyBag) : base(objectClass, propertyBag)
			{
				if (base.BacksyncShadowProperties)
				{
					this.timeStampDictionary = new Dictionary<string, DateTime>(StringComparer.InvariantCultureIgnoreCase);
					MultiValuedProperty<AttributeMetadata> multiValuedProperty = (MultiValuedProperty<AttributeMetadata>)propertyBag[ADRecipientSchema.AttributeMetadata];
					UserConfigXML userConfigXML = (UserConfigXML)propertyBag[ADRecipientSchema.ConfigurationXML];
					DateTime t = DateTime.MinValue;
					if (userConfigXML != null)
					{
						t = userConfigXML.RelocationLastWriteTime;
					}
					using (MultiValuedProperty<AttributeMetadata>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AttributeMetadata attributeMetadata = enumerator.Current;
							SyncPropertyDefinition syncPropertyDefinition = SyncSchema.Instance.GetADPropDefByLdapDisplayName(attributeMetadata.AttributeName) as SyncPropertyDefinition;
							if (syncPropertyDefinition != null && syncPropertyDefinition.IsShadow)
							{
								this.timeStampDictionary[attributeMetadata.AttributeName] = attributeMetadata.LastWriteTime;
								if (t > attributeMetadata.LastWriteTime && userConfigXML != null && userConfigXML.RelocationShadowPropMetaData != null)
								{
									PropertyMetaData propertyMetaData = userConfigXML.RelocationShadowPropMetaData.FirstOrDefault((PropertyMetaData m) => m.AttributeName == attributeMetadata.AttributeName);
									if (propertyMetaData != null)
									{
										this.timeStampDictionary[attributeMetadata.AttributeName] = propertyMetaData.LastWriteTime;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06006695 RID: 26261 RVA: 0x0016B12C File Offset: 0x0016932C
			public override void Process<T>(SyncPropertyDefinition propertyDefinition, ref T values)
			{
				if (base.ShouldIgnoreProperty(propertyDefinition))
				{
					return;
				}
				SyncPropertyDefinition syncPropertyDefinition = propertyDefinition;
				if (base.HasShadow(propertyDefinition))
				{
					base.PropertyBag.SetField(SyncRecipientSchema.UseShadow, true);
					syncPropertyDefinition = (SyncPropertyDefinition)propertyDefinition.ShadowProperty;
				}
				if (typeof(T) != syncPropertyDefinition.ExternalType)
				{
					return;
				}
				if (!ADDirSyncHelper.ContainsProperty(base.PropertyBag, syncPropertyDefinition))
				{
					values = default(T);
					return;
				}
				if (values == null)
				{
					values = Activator.CreateInstance<T>();
				}
				object value = base.PropertyBag[syncPropertyDefinition];
				if ((syncPropertyDefinition == SyncUserSchema.WindowsLiveID || syncPropertyDefinition == SyncUserSchema.WindowsLiveID.ShadowProperty) && (int)base.PropertyBag[SyncUserSchema.RecipientSoftDeletedStatus] != 0)
				{
					string text = (string)base.PropertyBag[SyncObjectSchema.ObjectId];
					text = text.Replace("-", string.Empty);
					if (!string.IsNullOrEmpty(text))
					{
						value = new SmtpAddress(text + base.PropertyBag[syncPropertyDefinition].ToString());
					}
				}
				IList list = SyncValueConvertor.GetValuesForDirectoryProperty(syncPropertyDefinition, value);
				if (SyncObject.BackSyncDataConverter.IsNullableType(syncPropertyDefinition.Type))
				{
					IList list2 = new ArrayList(list.Count);
					foreach (object obj in list)
					{
						if (obj != null)
						{
							list2.Add(obj);
						}
					}
					list = list2;
				}
				values.SetValues(list);
				if (this.timeStampDictionary != null && syncPropertyDefinition.IsShadow)
				{
					string ldapDisplayName = syncPropertyDefinition.LdapDisplayName;
					if (syncPropertyDefinition.IsCalculated)
					{
						ldapDisplayName = ((ADPropertyDefinition)syncPropertyDefinition.SupportingProperties[0]).LdapDisplayName;
					}
					if (this.timeStampDictionary.ContainsKey(ldapDisplayName))
					{
						values.Timestamp = this.timeStampDictionary[ldapDisplayName];
						values.TimestampSpecified = true;
					}
				}
			}

			// Token: 0x06006696 RID: 26262 RVA: 0x0016B334 File Offset: 0x00169534
			private static bool IsNullableType(Type type)
			{
				return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
			}

			// Token: 0x040043BA RID: 17338
			private readonly Dictionary<string, DateTime> timeStampDictionary;
		}
	}
}
