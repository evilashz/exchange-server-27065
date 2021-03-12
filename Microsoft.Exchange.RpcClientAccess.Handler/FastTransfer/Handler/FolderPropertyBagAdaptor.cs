using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000067 RID: 103
	internal class FolderPropertyBagAdaptor : CorePropertyBagAdaptor
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0001F19C File Offset: 0x0001D39C
		public FolderPropertyBagAdaptor(ICorePropertyBag corePropertyBag, ICoreObject propertyMappingReference, Encoding string8Encoding, bool wantUnicode, bool isUpload, bool shouldInterceptPropertyChanges) : base(new CoreObjectProperties(corePropertyBag), corePropertyBag, propertyMappingReference, ClientSideProperties.FolderInstance, PropertyConverter.Folder, DownloadBodyOption.AllBodyProperties, string8Encoding, wantUnicode, isUpload, false)
		{
			this.shouldInterceptPropertyChanges = shouldInterceptPropertyChanges;
			if (this.shouldInterceptPropertyChanges)
			{
				this.interceptedPropertyBag = new MemoryPropertyBag(base.Session);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		public override void SetProperty(PropertyValue propertyValue)
		{
			if (base.IsMoveUser && propertyValue.PropertyTag == PropertyTag.PackedNamedProps)
			{
				MDBEFCollection mdbefcollection = MDBEFCollection.CreateFrom(propertyValue.Value as byte[], base.String8Encoding);
				using (IEnumerator<AnnotatedPropertyValue> enumerator = mdbefcollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AnnotatedPropertyValue annotatedPropertyValue = enumerator.Current;
						PropertyTag propertyTag = annotatedPropertyValue.PropertyTag;
						base.Session.TryResolveFromNamedProperty(annotatedPropertyValue.NamedProperty, ref propertyTag);
						base.SetProperty(new PropertyValue(propertyTag, annotatedPropertyValue.PropertyValue.Value));
					}
					goto IL_9A;
				}
			}
			base.SetProperty(propertyValue);
			IL_9A:
			if (this.shouldInterceptPropertyChanges && !this.IgnoreUploadProperty(propertyValue.PropertyTag))
			{
				this.interceptedPropertyBag.SetProperty(propertyValue);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public override System.IO.Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			if (this.IgnoreUploadProperty(property))
			{
				return null;
			}
			return new MemoryPropertyBag.WriteMemoryStream(this, property, dataSizeEstimate);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001F2DD File Offset: 0x0001D4DD
		public IEnumerable<AnnotatedPropertyValue> GetInterceptedProperties()
		{
			if (!this.shouldInterceptPropertyChanges)
			{
				return null;
			}
			return this.interceptedPropertyBag.GetAnnotatedProperties();
		}

		// Token: 0x0400017E RID: 382
		private readonly bool shouldInterceptPropertyChanges;

		// Token: 0x0400017F RID: 383
		private MemoryPropertyBag interceptedPropertyBag;
	}
}
