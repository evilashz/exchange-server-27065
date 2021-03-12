using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000599 RID: 1433
	[XmlType]
	[DataContract]
	public abstract class ServiceObject
	{
		// Token: 0x06002872 RID: 10354 RVA: 0x000AB8A2 File Offset: 0x000A9AA2
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000AB8AA File Offset: 0x000A9AAA
		public ServiceObject()
		{
			this.Init();
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000AB8B8 File Offset: 0x000A9AB8
		internal ServiceObject(PropertyBag propertyBag)
		{
			this.propertyBag = propertyBag;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000AB8C7 File Offset: 0x000A9AC7
		private void Init()
		{
			this.propertyBag = new PropertyBag();
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x000AB8D4 File Offset: 0x000A9AD4
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x000AB8DC File Offset: 0x000A9ADC
		[IgnoreDataMember]
		[XmlIgnore]
		internal PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
			set
			{
				this.propertyBag = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x000AB8E5 File Offset: 0x000A9AE5
		internal List<PropertyInformation> LoadedProperties
		{
			get
			{
				return this.PropertyBag.LoadedProperties;
			}
		}

		// Token: 0x17000707 RID: 1799
		internal virtual object this[PropertyInformation property]
		{
			get
			{
				return this.PropertyBag[property];
			}
			set
			{
				this.PropertyBag[property] = value;
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000AB90F File Offset: 0x000A9B0F
		internal bool IsSet(PropertyInformation propertyInfo)
		{
			return this.PropertyBag.Contains(propertyInfo);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000AB91D File Offset: 0x000A9B1D
		internal void Clear()
		{
			this.PropertyBag.Clear();
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000AB92A File Offset: 0x000A9B2A
		internal bool Remove(PropertyInformation propertyInfo)
		{
			return this.PropertyBag.Remove(propertyInfo);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000AB938 File Offset: 0x000A9B38
		internal T GetValueOrDefault<T>(PropertyInformation propertyInfo)
		{
			return this.PropertyBag.GetValueOrDefault<T>(propertyInfo);
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600287F RID: 10367
		internal abstract StoreObjectType StoreObjectType { get; }

		// Token: 0x06002880 RID: 10368
		internal abstract void AddExtendedPropertyValue(ExtendedPropertyType extendedProperty);

		// Token: 0x040019CA RID: 6602
		private PropertyBag propertyBag;
	}
}
