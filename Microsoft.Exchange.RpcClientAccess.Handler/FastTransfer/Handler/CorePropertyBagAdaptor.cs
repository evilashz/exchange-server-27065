using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200005D RID: 93
	internal class CorePropertyBagAdaptor : IPropertyBag
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		public CorePropertyBagAdaptor(CoreObjectProperties coreObjectProperties, ICorePropertyBag corePropertyBag, ICoreObject propertyMappingReference, ClientSideProperties clientSideProperties, PropertyConverter propertyConverter, DownloadBodyOption downloadBodyOption, Encoding string8Encoding, bool wantUnicode, bool isUpload, bool isFastTransferCopyProperties)
		{
			this.corePropertyBag = corePropertyBag;
			this.propertyMappingReference = propertyMappingReference;
			this.session = new SessionAdaptor(propertyMappingReference.Session);
			this.clientSideProperties = clientSideProperties;
			this.string8Encoding = string8Encoding;
			this.wantUnicode = wantUnicode;
			this.fixupMapping = ((isUpload || isFastTransferCopyProperties) ? FixupMapping.FastTransferCopyProperties : FixupMapping.FastTransfer);
			this.excludeList = CorePropertyBagAdaptor.CalculateBodyPropertiesExcludeList(corePropertyBag, downloadBodyOption, !this.IsMoveUser && !isUpload && !isFastTransferCopyProperties);
			this.coreObjectProperties = coreObjectProperties;
			this.propertyConverter = propertyConverter;
			if (this.BodyHelper != null)
			{
				this.conversionList = CorePropertyBagAdaptor.allBodyIncludes;
				return;
			}
			this.conversionList = Array<PropertyTag>.Empty;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001D27C File Offset: 0x0001B47C
		private BodyHelper BodyHelper
		{
			get
			{
				BestBodyCoreObjectProperties bestBodyCoreObjectProperties = this.coreObjectProperties as BestBodyCoreObjectProperties;
				if (bestBodyCoreObjectProperties != null)
				{
					return bestBodyCoreObjectProperties.BodyHelper;
				}
				return null;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		public ISession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		public AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag)
		{
			if (this.BodyHelper != null)
			{
				this.BodyHelper.InitiatePropertyEvaluation();
			}
			PropertyDefinition propertyDefinition = this.GetPropertyDefinition(propertyTag);
			return this.GetAnnotatedProperty(propertyDefinition, propertyTag);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001D590 File Offset: 0x0001B790
		public IEnumerable<AnnotatedPropertyValue> GetAnnotatedProperties()
		{
			bool useUnicodeType = true;
			ICollection<PropertyDefinition> propertyDefinitions = this.coreObjectProperties.AllFoundProperties.Union(this.GetPropertyDefinitions(this.conversionList));
			ICollection<PropertyTag> propertyTags = MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<PropertyDefinition>(this.propertyMappingReference.Session, propertyDefinitions, useUnicodeType);
			if (this.BodyHelper != null)
			{
				this.BodyHelper.InitiatePropertyEvaluation();
			}
			using (IEnumerator<PropertyTag> propertyTagEnumerator = propertyTags.GetEnumerator())
			{
				foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
				{
					propertyTagEnumerator.MoveNext();
					PropertyTag propertyTag = propertyTagEnumerator.Current;
					if (this.IncludeDownloadProperty(propertyTag))
					{
						yield return this.GetAnnotatedProperty(propertyDefinition, propertyTag);
					}
				}
			}
			yield break;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		public virtual void SetProperty(PropertyValue propertyValue)
		{
			if (propertyValue.IsError)
			{
				throw new RopExecutionException(string.Format("Can't set errors for properties; propertyValue={0}.", propertyValue), (ErrorCode)2147746050U);
			}
			if (this.IgnoreUploadProperty(propertyValue.PropertyTag))
			{
				return;
			}
			PropertyConverter.ConvertFromExportToOurServerId(this.propertyMappingReference.Session, ref propertyValue);
			this.propertyConverter.ConvertPropertyValueFromClient(this.propertyMappingReference.Session, this.coreObjectProperties, ref propertyValue);
			EntryIdConverter.ConvertFromClient(this.string8Encoding, ref propertyValue);
			PropertyDefinition propertyDefinition;
			try
			{
				propertyDefinition = this.GetPropertyDefinition(propertyValue.PropertyTag);
			}
			catch (RopExecutionException)
			{
				if (this.propertyMappingReference.Session != null && this.propertyMappingReference.Session.OperationContext.IsMoveUser && propertyValue.PropertyTag.IsNamedProperty)
				{
					return;
				}
				throw;
			}
			this.coreObjectProperties.SetProperty(propertyDefinition, MEDSPropertyTranslator.TranslatePropertyValue(this.propertyMappingReference.Session, propertyValue));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
		public void Delete(PropertyTag property)
		{
			if (this.IgnoreUploadProperty(property))
			{
				return;
			}
			this.coreObjectProperties.DeleteProperty(this.GetPropertyDefinition(property));
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
		public System.IO.Stream GetPropertyStream(PropertyTag property)
		{
			if (this.IgnoreDownloadProperty(property))
			{
				return null;
			}
			System.IO.Stream result;
			try
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					System.IO.Stream stream = null;
					if (this.BodyHelper != null && property.IsBodyProperty())
					{
						stream = this.BodyHelper.GetConversionStream(property);
						if (stream != null)
						{
							disposeGuard.Add<System.IO.Stream>(stream);
						}
					}
					if (stream == null)
					{
						stream = this.coreObjectProperties.OpenStream(this.GetPropertyDefinition(property), PropertyOpenMode.ReadOnly);
						disposeGuard.Add<System.IO.Stream>(stream);
						long length = stream.Length;
					}
					disposeGuard.Success();
					result = stream;
				}
			}
			catch (ObjectNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001D778 File Offset: 0x0001B978
		public virtual System.IO.Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			if (this.IgnoreUploadProperty(property))
			{
				return null;
			}
			if (this.propertyConverter.ClientPropertyTagsThatRequireConversion.Contains(property) || EntryIdConverter.NeedsConversion(property))
			{
				return new MemoryPropertyBag.WriteMemoryStream(this, property, dataSizeEstimate);
			}
			return this.coreObjectProperties.OpenStream(this.GetPropertyDefinition(property), PropertyOpenMode.Create);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		private static PropertyId[] CalculateBodyPropertiesExcludeList(ICorePropertyBag propertyBag, DownloadBodyOption downloadBodyOption, bool excludePreview)
		{
			EnumValidator.AssertValid<DownloadBodyOption>(downloadBodyOption);
			switch (downloadBodyOption)
			{
			case DownloadBodyOption.RtfOnly:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.rtfCompressedExcludes;
				}
				return CorePropertyBagAdaptor.rtfCompressedExcludesWithPreview;
			case DownloadBodyOption.BestBodyOnly:
				return CorePropertyBagAdaptor.CalculateBestBodyPropertyExcludeList(propertyBag, excludePreview);
			case DownloadBodyOption.AllBodyProperties:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.defaultExcludes;
				}
				return CorePropertyBagAdaptor.defaultExcludesWithPreview;
			default:
				throw new ArgumentOutOfRangeException("downloadBodyOption");
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001D824 File Offset: 0x0001BA24
		private static PropertyId[] CalculateBestBodyPropertyExcludeList(ICorePropertyBag propertyBag, bool excludePreview)
		{
			switch (propertyBag.GetValueOrDefault<NativeBodyInfo>(CoreItemSchema.NativeBodyInfo))
			{
			case NativeBodyInfo.PlainTextBody:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.plainTextExcludes;
				}
				return CorePropertyBagAdaptor.plainTextExcludesWithPreview;
			case NativeBodyInfo.RtfCompressedBody:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.rtfCompressedExcludes;
				}
				return CorePropertyBagAdaptor.rtfCompressedExcludesWithPreview;
			case NativeBodyInfo.HtmlBody:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.htmlExcludes;
				}
				return CorePropertyBagAdaptor.htmlExcludesWithPreview;
			case NativeBodyInfo.ClearSignedBody:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.allBodyExcludes;
				}
				return CorePropertyBagAdaptor.allBodyExcludesWithPreview;
			default:
				if (!excludePreview)
				{
					return CorePropertyBagAdaptor.defaultExcludes;
				}
				return CorePropertyBagAdaptor.defaultExcludesWithPreview;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		private PropertyDefinition GetPropertyDefinition(PropertyTag property)
		{
			return this.GetPropertyDefinitions(new PropertyTag[]
			{
				property
			})[0];
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001D8CE File Offset: 0x0001BACE
		private PropertyDefinition[] GetPropertyDefinitions(PropertyTag[] properties)
		{
			return MEDSPropertyTranslator.GetPropertyDefinitionsIgnoreTypeChecking(this.propertyMappingReference.Session, this.propertyMappingReference.PropertyBag, properties);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		private AnnotatedPropertyValue GetAnnotatedProperty(PropertyDefinition propertyDefinition, PropertyTag propertyTag)
		{
			PropertyValue propertyValue;
			if (!IdConverters.GetClientId(this.propertyMappingReference.Session, this.corePropertyBag, propertyTag, out propertyValue))
			{
				object xsoPropertyValue = this.coreObjectProperties.TryGetProperty(propertyDefinition);
				Feature.Stubbed(212759, "Support String8 for Restrictions in FastTransfer");
				bool useUnicodeForRestrictions = true;
				propertyValue = MEDSPropertyTranslator.TranslatePropertyValue(this.propertyMappingReference.Session, propertyTag, xsoPropertyValue, useUnicodeForRestrictions);
				if (this.BodyHelper != null && BodyHelper.IsFixupNeeded(propertyValue.PropertyTag))
				{
					this.BodyHelper.FixupProperty(ref propertyValue, this.fixupMapping);
				}
				this.propertyConverter.ConvertPropertyValueToClient(this.propertyMappingReference.Session, this.coreObjectProperties, ref propertyValue, null);
				PropertyConverter.ConvertFromOurToExportServerId(this.propertyMappingReference.Session, ref propertyValue);
				EntryIdConverter.ConvertToClient(this.wantUnicode, this.string8Encoding, ref propertyValue);
				if (!propertyValue.IsError)
				{
					propertyTag = propertyValue.PropertyTag;
				}
			}
			NamedProperty namedProperty = null;
			if (propertyTag.IsNamedProperty)
			{
				NamedPropertyDefinition namedPropertyDefinition = (NamedPropertyDefinition)propertyDefinition;
				NamedPropertyDefinition.NamedPropertyKey key = namedPropertyDefinition.GetKey();
				namedProperty = key.ToNamedProperty();
			}
			return new AnnotatedPropertyValue(propertyTag, propertyValue, namedProperty);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001DA00 File Offset: 0x0001BC00
		private bool IncludeDownloadProperty(PropertyTag propertyTag)
		{
			if (this.excludeList != null)
			{
				foreach (PropertyId propertyId in this.excludeList)
				{
					if (propertyTag.PropertyId == propertyId)
					{
						return false;
					}
				}
			}
			return !this.IgnoreDownloadProperty(propertyTag);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001DA48 File Offset: 0x0001BC48
		protected bool IsMoveUser
		{
			get
			{
				return this.propertyMappingReference != null && this.propertyMappingReference.Session != null && this.propertyMappingReference.Session.IsMoveUser;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001DA71 File Offset: 0x0001BC71
		protected Encoding String8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001DA79 File Offset: 0x0001BC79
		protected virtual bool IgnoreDownloadProperty(PropertyTag property)
		{
			return !this.clientSideProperties.ShouldBeReturnedIfRequested(property.PropertyId);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001DA90 File Offset: 0x0001BC90
		protected virtual bool IgnoreUploadProperty(PropertyTag property)
		{
			return !this.clientSideProperties.ShouldBeReturnedIfRequested(property.PropertyId);
		}

		// Token: 0x0400014A RID: 330
		private static readonly PropertyId[] defaultExcludes = new PropertyId[]
		{
			PropertyId.NativeBodyInfo
		};

		// Token: 0x0400014B RID: 331
		private static readonly PropertyId[] plainTextExcludes = new PropertyId[]
		{
			PropertyId.NativeBodyInfo,
			PropertyId.RtfCompressed,
			PropertyId.Html
		};

		// Token: 0x0400014C RID: 332
		private static readonly PropertyId[] rtfCompressedExcludes = new PropertyId[]
		{
			PropertyId.NativeBodyInfo,
			PropertyId.Body,
			PropertyId.Html
		};

		// Token: 0x0400014D RID: 333
		private static readonly PropertyId[] htmlExcludes = new PropertyId[]
		{
			PropertyId.NativeBodyInfo,
			PropertyId.Body,
			PropertyId.RtfCompressed
		};

		// Token: 0x0400014E RID: 334
		private static readonly PropertyId[] allBodyExcludes = new PropertyId[]
		{
			PropertyId.NativeBodyInfo,
			PropertyId.Body,
			PropertyId.RtfCompressed,
			PropertyId.Html
		};

		// Token: 0x0400014F RID: 335
		private static readonly PropertyId[] previewExclude = new PropertyId[]
		{
			PropertyId.Preview
		};

		// Token: 0x04000150 RID: 336
		private static readonly PropertyId[] defaultExcludesWithPreview = CorePropertyBagAdaptor.defaultExcludes.Concat(CorePropertyBagAdaptor.previewExclude).ToArray<PropertyId>();

		// Token: 0x04000151 RID: 337
		private static readonly PropertyId[] plainTextExcludesWithPreview = CorePropertyBagAdaptor.plainTextExcludes.Concat(CorePropertyBagAdaptor.previewExclude).ToArray<PropertyId>();

		// Token: 0x04000152 RID: 338
		private static readonly PropertyId[] rtfCompressedExcludesWithPreview = CorePropertyBagAdaptor.rtfCompressedExcludes.Concat(CorePropertyBagAdaptor.previewExclude).ToArray<PropertyId>();

		// Token: 0x04000153 RID: 339
		private static readonly PropertyId[] htmlExcludesWithPreview = CorePropertyBagAdaptor.htmlExcludes.Concat(CorePropertyBagAdaptor.previewExclude).ToArray<PropertyId>();

		// Token: 0x04000154 RID: 340
		private static readonly PropertyId[] allBodyExcludesWithPreview = CorePropertyBagAdaptor.allBodyExcludes.Concat(CorePropertyBagAdaptor.previewExclude).ToArray<PropertyId>();

		// Token: 0x04000155 RID: 341
		private static readonly PropertyTag[] allBodyIncludes = new PropertyTag[]
		{
			PropertyTag.Body,
			PropertyTag.Html,
			PropertyTag.RtfCompressed,
			PropertyTag.RtfInSync
		};

		// Token: 0x04000156 RID: 342
		private static readonly PropertyTag[] rtfOnlyIncludes = new PropertyTag[]
		{
			PropertyTag.RtfCompressed,
			PropertyTag.RtfInSync
		};

		// Token: 0x04000157 RID: 343
		private readonly ICorePropertyBag corePropertyBag;

		// Token: 0x04000158 RID: 344
		private readonly ICoreObject propertyMappingReference;

		// Token: 0x04000159 RID: 345
		private readonly ISession session;

		// Token: 0x0400015A RID: 346
		private readonly PropertyId[] excludeList;

		// Token: 0x0400015B RID: 347
		private readonly PropertyTag[] conversionList;

		// Token: 0x0400015C RID: 348
		private readonly ClientSideProperties clientSideProperties;

		// Token: 0x0400015D RID: 349
		private readonly PropertyConverter propertyConverter;

		// Token: 0x0400015E RID: 350
		private readonly Encoding string8Encoding;

		// Token: 0x0400015F RID: 351
		private readonly bool wantUnicode;

		// Token: 0x04000160 RID: 352
		private readonly FixupMapping fixupMapping;

		// Token: 0x04000161 RID: 353
		private readonly CoreObjectProperties coreObjectProperties;
	}
}
