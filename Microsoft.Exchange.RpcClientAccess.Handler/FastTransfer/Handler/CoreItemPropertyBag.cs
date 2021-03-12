using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200005F RID: 95
	internal sealed class CoreItemPropertyBag : IPropertyBag
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		public CoreItemPropertyBag(IPropertyBag propertyBag, bool sendEntryId)
		{
			this.propertyBag = propertyBag;
			this.addedProperties = (sendEntryId ? new PropertyTag[]
			{
				PropertyTag.OriginalMessageEntryId
			} : Array<PropertyTag>.Empty);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag)
		{
			if (propertyTag == PropertyTag.OriginalMessageEntryId)
			{
				return new AnnotatedPropertyValue(propertyTag, this.GetAnnotatedProperty(PropertyTag.EntryId).PropertyValue.CloneAs(propertyTag.PropertyId), null);
			}
			return this.propertyBag.GetAnnotatedProperty(propertyTag);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001DE00 File Offset: 0x0001C000
		public IEnumerable<AnnotatedPropertyValue> GetAnnotatedProperties()
		{
			return this.addedProperties.Select(new Func<PropertyTag, AnnotatedPropertyValue>(this.GetAnnotatedProperty)).Concat(this.propertyBag.GetAnnotatedProperties().Where(new Func<AnnotatedPropertyValue, bool>(this.NotAddedProperty))).Where(new Func<AnnotatedPropertyValue, bool>(this.IncludePropertyOnDownload));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001DE56 File Offset: 0x0001C056
		public void SetProperty(PropertyValue propertyValue)
		{
			if (this.IncludePropertyOnUpload(propertyValue.PropertyTag))
			{
				this.propertyBag.SetProperty(propertyValue);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001DE73 File Offset: 0x0001C073
		public void Delete(PropertyTag property)
		{
			if (this.IncludePropertyOnUpload(property))
			{
				this.propertyBag.Delete(property);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001DE8A File Offset: 0x0001C08A
		public Stream GetPropertyStream(PropertyTag property)
		{
			if (!this.IncludePropertyOnDownload(property) || !this.NotAddedProperty(property))
			{
				return null;
			}
			return this.propertyBag.GetPropertyStream(property);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		public Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			if (!this.IncludePropertyOnUpload(property))
			{
				return null;
			}
			return this.propertyBag.SetPropertyStream(property, dataSizeEstimate);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001DEC6 File Offset: 0x0001C0C6
		public ISession Session
		{
			get
			{
				return this.propertyBag.Session;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001DED3 File Offset: 0x0001C0D3
		private bool IncludePropertyOnDownload(AnnotatedPropertyValue propertyValue)
		{
			return this.IncludePropertyOnDownload(propertyValue.PropertyTag);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		private bool IncludePropertyOnDownload(PropertyTag property)
		{
			return property.PropertyId != PropertyTag.EntryId.PropertyId;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001DF0A File Offset: 0x0001C10A
		private bool IncludePropertyOnUpload(PropertyTag property)
		{
			return !CoreItemPropertyBag.StoreComputedMessageProperties.Contains(property, PropertyTag.PropertyIdComparer);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001DF20 File Offset: 0x0001C120
		private bool NotAddedProperty(PropertyTag propertyTag)
		{
			foreach (PropertyTag propertyTag2 in this.addedProperties)
			{
				if (propertyTag2.PropertyId == propertyTag.PropertyId)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001DF66 File Offset: 0x0001C166
		private bool NotAddedProperty(AnnotatedPropertyValue propertyValue)
		{
			return this.NotAddedProperty(propertyValue.PropertyTag);
		}

		// Token: 0x04000163 RID: 355
		private static readonly PropertyTag[] StoreComputedMessageProperties = new PropertyTag[]
		{
			PropertyTag.RuleFolderEntryId
		};

		// Token: 0x04000164 RID: 356
		private readonly PropertyTag[] addedProperties;

		// Token: 0x04000165 RID: 357
		private readonly IPropertyBag propertyBag;
	}
}
