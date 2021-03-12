using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.AddressBook.Nspi;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.OAB;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001F6 RID: 502
	internal sealed class PropertyManager : DisposeTrackableBase
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x000715A8 File Offset: 0x0006F7A8
		public PropertyManager(OfflineAddressBook offlineAddressBook, SecurityIdentifier userSid, string userDomain, bool habEnabled)
		{
			PropertyManager.Tracer.TraceFunction((long)this.GetHashCode(), "PropertyManager.PropertyManager");
			Dictionary<PropTag, OABPropertyFlags> dictionary = new Dictionary<PropTag, OABPropertyFlags>();
			PropertyManager.AddProperties("RdnProperties", PropertyManager.RdnProperties, OABPropertyFlags.RDN, dictionary);
			PropertyManager.AddProperties("OfflineAddressBookSchema.ANRProperties", PropertyManager.GetPropTagArray(offlineAddressBook[OfflineAddressBookSchema.ANRProperties]), OABPropertyFlags.ANR, dictionary);
			PropertyManager.AddProperties("OfflineAddressBookSchema.DetailsProperties", PropertyManager.GetPropTagArray(offlineAddressBook[OfflineAddressBookSchema.DetailsProperties]), OABPropertyFlags.None, dictionary);
			if (habEnabled)
			{
				PropertyManager.AddProperties("HABProperties", PropertyManager.habProperties, OABPropertyFlags.None, dictionary);
			}
			PropertyManager.AddProperties("OfflineAddressBookSchema.TruncatedProperties", PropertyManager.GetPropTagArray(offlineAddressBook[OfflineAddressBookSchema.TruncatedProperties]), OABPropertyFlags.Truncated, dictionary);
			PropertyManager.AddProperties("RequiredProperties", PropertyManager.RequiredProperties, OABPropertyFlags.None, dictionary);
			foreach (PropTag key in PropertyManager.V4ANRPropTags)
			{
				OABPropertyFlags oabpropertyFlags;
				if (dictionary.TryGetValue(key, out oabpropertyFlags))
				{
					dictionary[key] = (oabpropertyFlags | OABPropertyFlags.ANR);
				}
			}
			PropertyManager.InitializeNspiPropMapper();
			this.oabProperties = OABProperty.CreateOABPropertyList(dictionary);
			SecurityAccessToken securityAccessToken = new SecurityAccessToken
			{
				UserSid = userSid.ToString()
			};
			ClientSecurityContext clientSecurityContext = new ClientSecurityContext(securityAccessToken);
			this.nspiContext = new NspiContext(clientSecurityContext, userDomain, string.Empty, string.Empty, string.Empty, default(Guid));
			if (!this.nspiContext.TryAcquireBudget())
			{
				PropertyManager.Tracer.TraceError((long)this.GetHashCode(), "NspiContext.TryAcquireBudget failed");
			}
			NspiState state = new NspiState
			{
				CodePage = Encoding.UTF8.CodePage
			};
			this.nspiContext.Bind(NspiBindFlags.None, state);
			this.nspiPropMapper = new NspiPropMapper(this.nspiContext, Array.ConvertAll<OABProperty, PropTag>(this.oabProperties, (OABProperty property) => property.PropTag), Encoding.UTF8.CodePage, NspiPropMapperFlags.IncludeHiddenFromAddressListsEnabled);
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x000717A0 File Offset: 0x0006F9A0
		public OABProperty[] OABProperties
		{
			get
			{
				return this.oabProperties;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x000717A8 File Offset: 0x0006F9A8
		public OABPropertyDescriptor[] HeaderProperties
		{
			get
			{
				return PropertyManager.headerProperties;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000717AF File Offset: 0x0006F9AF
		public ADPropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return this.nspiPropMapper.PropertyDefinitions;
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000717BC File Offset: 0x0006F9BC
		public PropRow GetProps(ADRawEntry adRawEntry)
		{
			return this.nspiPropMapper.GetProps(adRawEntry);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000717CC File Offset: 0x0006F9CC
		public void ResolveLinks(ADRawEntry[] adRawEntries)
		{
			foreach (ADRawEntry adRawEntry in adRawEntries)
			{
				foreach (OABProperty oabproperty in this.oabProperties)
				{
					oabproperty.PreResolveLinks(adRawEntry);
				}
			}
			this.nspiPropMapper.ResolveLinks(adRawEntries, false);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00071822 File Offset: 0x0006FA22
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.nspiContext.Dispose();
			}
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00071832 File Offset: 0x0006FA32
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PropertyManager>(this);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0007183C File Offset: 0x0006FA3C
		private static void AddProperties(string description, IEnumerable<PropTag> propertiesToAdd, OABPropertyFlags flags, Dictionary<PropTag, OABPropertyFlags> properties)
		{
			PropertyManager.Tracer.TraceDebug<string>(0L, "PropertyManager.AddProperties: Adding {0}", description);
			foreach (PropTag propTag in propertiesToAdd)
			{
				PropertyManager.AddProperty(propTag, flags, properties);
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00071898 File Offset: 0x0006FA98
		private static void AddProperty(PropTag propTag, OABPropertyFlags flags, Dictionary<PropTag, OABPropertyFlags> properties)
		{
			OABPropertyFlags oabpropertyFlags;
			if (properties.TryGetValue(propTag, out oabpropertyFlags))
			{
				PropertyManager.Tracer.TraceDebug<PropTag, OABPropertyFlags, OABPropertyFlags>(0L, "PropertyManager.AddProperty: merging property={0} existing flags {1} with {2}", propTag, oabpropertyFlags, flags);
				flags |= oabpropertyFlags;
			}
			else
			{
				PropertyManager.Tracer.TraceDebug<PropTag, OABPropertyFlags>(0L, "PropertyManager.AddProperty: adding property={0}, flags={1}", propTag, flags);
			}
			properties[propTag] = flags;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000718EC File Offset: 0x0006FAEC
		private static PropTag[] GetPropTagArray(object collectionObject)
		{
			MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)collectionObject;
			return Array.ConvertAll<int, PropTag>(multiValuedProperty.ToArray(), (int propTag) => (PropTag)propTag);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00071928 File Offset: 0x0006FB28
		private static void InitializeNspiPropMapper()
		{
			if (!PropertyManager.nspiPropMapperInitialized)
			{
				lock (PropertyManager.nspiPropMapperInitializeLocker)
				{
					if (!PropertyManager.nspiPropMapperInitialized)
					{
						PropertyManager.Tracer.TraceDebug(0L, "PropertyManager.InitializeNspiPropMapper: initializing NspiPropMapper");
						NspiPropMapper.Initialize();
						PropertyManager.nspiPropMapperInitialized = true;
						PropertyManager.Tracer.TraceDebug(0L, "PropertyManager.InitializeNspiPropMapper: NspiPropMapper initialized successfully");
					}
				}
			}
		}

		// Token: 0x04000BC1 RID: 3009
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000BC2 RID: 3010
		private static readonly PropTag[] RequiredProperties = new PropTag[]
		{
			(PropTag)2355953922U,
			PropTag.DisplayName,
			PropTag.DisplayType,
			PropTag.ObjectType
		};

		// Token: 0x04000BC3 RID: 3011
		private static readonly PropTag[] RdnProperties = new PropTag[]
		{
			PropTag.EmailAddressAnsi,
			PropTag.SmtpAddress
		};

		// Token: 0x04000BC4 RID: 3012
		public static readonly HashSet<PropTag> V4ANRPropTags = new HashSet<PropTag>
		{
			PropTag.DisplayName,
			(PropTag)2358378527U,
			PropTag.Account,
			PropTag.Surname,
			(PropTag)2358181919U,
			PropTag.GivenName,
			(PropTag)2358116383U,
			(PropTag)2148470815U,
			PropTag.OfficeLocation
		};

		// Token: 0x04000BC5 RID: 3013
		private static readonly OABPropertyDescriptor[] headerProperties = new OABPropertyDescriptor[]
		{
			new OABPropertyDescriptor
			{
				PropTag = OABFilePropTags.OabName,
				PropFlags = OABPropertyFlags.None
			},
			new OABPropertyDescriptor
			{
				PropTag = OABFilePropTags.OabDN,
				PropFlags = OABPropertyFlags.None
			},
			new OABPropertyDescriptor
			{
				PropTag = OABFilePropTags.OabSequence,
				PropFlags = OABPropertyFlags.None
			},
			new OABPropertyDescriptor
			{
				PropTag = OABFilePropTags.OabContainerGuid,
				PropFlags = OABPropertyFlags.None
			},
			new OABPropertyDescriptor
			{
				PropTag = OABFilePropTags.OabHABRootDepartmentLink,
				PropFlags = OABPropertyFlags.None
			}
		};

		// Token: 0x04000BC6 RID: 3014
		private static readonly PropTag[] habProperties = new PropTag[]
		{
			(PropTag)2363293707U,
			(PropTag)2359296003U
		};

		// Token: 0x04000BC7 RID: 3015
		private static readonly object nspiPropMapperInitializeLocker = new object();

		// Token: 0x04000BC8 RID: 3016
		private static bool nspiPropMapperInitialized;

		// Token: 0x04000BC9 RID: 3017
		private readonly NspiContext nspiContext;

		// Token: 0x04000BCA RID: 3018
		private readonly OABProperty[] oabProperties;

		// Token: 0x04000BCB RID: 3019
		private readonly NspiPropMapper nspiPropMapper;
	}
}
