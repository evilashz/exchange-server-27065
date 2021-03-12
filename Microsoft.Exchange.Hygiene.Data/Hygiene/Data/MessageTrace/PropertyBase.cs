using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200014C RID: 332
	[DebuggerDisplay("Name = {PropertyName}; Namespace = {Namespace}; Id = {PropertyId}")]
	internal abstract class PropertyBase : MessageTraceEntityBase
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x000277BE File Offset: 0x000259BE
		public PropertyBase(string nameSpace, string name, object value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000277E6 File Offset: 0x000259E6
		public PropertyBase(string nameSpace, string name, int? value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002780E File Offset: 0x00025A0E
		public PropertyBase(string nameSpace, string name, string value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00027836 File Offset: 0x00025A36
		public PropertyBase(string nameSpace, string name, DateTime? value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002785E File Offset: 0x00025A5E
		public PropertyBase(string nameSpace, string name, decimal? value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00027886 File Offset: 0x00025A86
		public PropertyBase(string nameSpace, string name, BlobType value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000278AE File Offset: 0x00025AAE
		public PropertyBase(string nameSpace, string name, Guid value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000278D6 File Offset: 0x00025AD6
		public PropertyBase(string nameSpace, string name, long? value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(value);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000278FE File Offset: 0x00025AFE
		public PropertyBase(string nameSpace, string name, bool value)
		{
			this.PropertyId = Guid.NewGuid();
			this.Namespace = nameSpace;
			this.PropertyName = name;
			this.SetProperty(new bool?(value));
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002792B File Offset: 0x00025B2B
		protected PropertyBase()
		{
			this.PropertyId = Guid.NewGuid();
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002793E File Offset: 0x00025B3E
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00027950 File Offset: 0x00025B50
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00027963 File Offset: 0x00025B63
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00027975 File Offset: 0x00025B75
		public Guid PropertyId
		{
			get
			{
				return (Guid)this[PropertyBase.PropertyIdProperty];
			}
			set
			{
				this[PropertyBase.PropertyIdProperty] = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00027988 File Offset: 0x00025B88
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x0002799A File Offset: 0x00025B9A
		public Guid ParentId
		{
			get
			{
				return (Guid)this[PropertyBase.ParentIdProperty];
			}
			set
			{
				this[PropertyBase.ParentIdProperty] = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x000279AD File Offset: 0x00025BAD
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x000279BF File Offset: 0x00025BBF
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.ExMessageIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x000279D2 File Offset: 0x00025BD2
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x000279E4 File Offset: 0x00025BE4
		public string Namespace
		{
			get
			{
				return (string)this[PropertyBase.NamespaceProperty];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Namespace cannot be set to null value.");
				}
				this[PropertyBase.NamespaceProperty] = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00027A00 File Offset: 0x00025C00
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00027A12 File Offset: 0x00025C12
		public string PropertyName
		{
			get
			{
				return (string)this[PropertyBase.PropertyNameProperty];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Name cannot be null");
				}
				this[PropertyBase.PropertyNameProperty] = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00027A2E File Offset: 0x00025C2E
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00027A40 File Offset: 0x00025C40
		public int PropertyIndex
		{
			get
			{
				return (int)this[PropertyBase.PropertyIndexProperty];
			}
			set
			{
				this[PropertyBase.PropertyIndexProperty] = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00027A53 File Offset: 0x00025C53
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00027A65 File Offset: 0x00025C65
		public Guid PropertyValueGuid
		{
			get
			{
				return (Guid)this[PropertyBase.PropertyValueGuidProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00027A6E File Offset: 0x00025C6E
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00027A80 File Offset: 0x00025C80
		public int? PropertyValueInteger
		{
			get
			{
				return (int?)this[PropertyBase.PropertyValueIntegerProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00027A89 File Offset: 0x00025C89
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00027A9B File Offset: 0x00025C9B
		public long? PropertyValueLong
		{
			get
			{
				return (long?)this[PropertyBase.PropertyValueLongProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00027AA4 File Offset: 0x00025CA4
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x00027AB6 File Offset: 0x00025CB6
		public string PropertyValueString
		{
			get
			{
				return (string)this[PropertyBase.PropertyValueStringProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00027ABF File Offset: 0x00025CBF
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00027AD1 File Offset: 0x00025CD1
		public DateTime? PropertyValueDatetime
		{
			get
			{
				return (DateTime?)this[PropertyBase.PropertyValueDatetimeProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00027ADA File Offset: 0x00025CDA
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x00027AEC File Offset: 0x00025CEC
		public bool? PropertyValueBit
		{
			get
			{
				return (bool?)this[PropertyBase.PropertyValueBitProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00027AF5 File Offset: 0x00025CF5
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00027B07 File Offset: 0x00025D07
		public decimal? PropertyValueDecimal
		{
			get
			{
				return (decimal?)this[PropertyBase.PropertyValueDecimalProperty];
			}
			set
			{
				this.SetProperty(value);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00027B10 File Offset: 0x00025D10
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00027B27 File Offset: 0x00025D27
		public BlobType PropertyValueBlob
		{
			get
			{
				return new BlobType((string)this[PropertyBase.PropertyValueBlobProperty]);
			}
			set
			{
				this[PropertyBase.PropertyValueBlobProperty] = value.Value;
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00027B3B File Offset: 0x00025D3B
		private void SetProperty(Guid value)
		{
			this[PropertyBase.PropertyValueGuidProperty] = value;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00027B50 File Offset: 0x00025D50
		private void SetProperty(object value)
		{
			if (value != null)
			{
				Type type = value.GetType();
				if (type == typeof(BlobType))
				{
					this[PropertyBase.PropertyValueBlobProperty] = ((BlobType)value).Value;
					return;
				}
				if (type == typeof(bool))
				{
					this[PropertyBase.PropertyValueBitProperty] = (bool)value;
					return;
				}
				if (type == typeof(DateTime))
				{
					this[PropertyBase.PropertyValueDatetimeProperty] = (DateTime?)value;
					return;
				}
				if (type == typeof(decimal))
				{
					this[PropertyBase.PropertyValueDecimalProperty] = (decimal?)value;
					return;
				}
				if (type == typeof(float))
				{
					this[PropertyBase.PropertyValueDecimalProperty] = new decimal?((decimal)((float)value));
					return;
				}
				if (type == typeof(Guid))
				{
					this[PropertyBase.PropertyValueGuidProperty] = (Guid)value;
					return;
				}
				if (type == typeof(int))
				{
					this[PropertyBase.PropertyValueIntegerProperty] = (int?)value;
					return;
				}
				if (type == typeof(long))
				{
					this[PropertyBase.PropertyValueLongProperty] = (long?)value;
					return;
				}
				if (!(type == typeof(string)))
				{
					throw new NotSupportedException(string.Format("PropertyValueType {0} is not supported in PropertyBase", type));
				}
				string text = (string)value;
				if (!string.IsNullOrEmpty(text))
				{
					if (text.Length > 320)
					{
						this[PropertyBase.PropertyValueBlobProperty] = text;
						return;
					}
					this[PropertyBase.PropertyValueStringProperty] = text;
					return;
				}
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00027D19 File Offset: 0x00025F19
		private void SetProperty(int? value)
		{
			this[PropertyBase.PropertyValueIntegerProperty] = value;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00027D2C File Offset: 0x00025F2C
		private void SetProperty(string value)
		{
			this[PropertyBase.PropertyValueStringProperty] = value;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00027D3A File Offset: 0x00025F3A
		private void SetProperty(long? value)
		{
			this[PropertyBase.PropertyValueLongProperty] = value;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00027D4D File Offset: 0x00025F4D
		private void SetProperty(DateTime? value)
		{
			this[PropertyBase.PropertyValueDatetimeProperty] = value;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00027D60 File Offset: 0x00025F60
		private void SetProperty(bool? value)
		{
			this[PropertyBase.PropertyValueBitProperty] = value;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00027D73 File Offset: 0x00025F73
		private void SetProperty(decimal? value)
		{
			this[PropertyBase.PropertyValueDecimalProperty] = value;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00027D86 File Offset: 0x00025F86
		private void SetProperty(BlobType value)
		{
			this[PropertyBase.PropertyValueBlobProperty] = value.Value;
		}

		// Token: 0x04000659 RID: 1625
		public const int MaxNumOfBytesInDBExtendedPropertyStringType = 320;

		// Token: 0x0400065A RID: 1626
		internal static readonly HygienePropertyDefinition[] BaseProperties = new HygienePropertyDefinition[]
		{
			PropertyBase.PropertyIdProperty,
			PropertyBase.ParentIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			PropertyBase.NamespaceProperty,
			PropertyBase.PropertyNameProperty,
			PropertyBase.PropertyIndexProperty,
			PropertyBase.PropertyValueGuidProperty,
			PropertyBase.PropertyValueIntegerProperty,
			PropertyBase.PropertyValueLongProperty,
			PropertyBase.PropertyValueStringProperty,
			PropertyBase.PropertyValueDatetimeProperty,
			PropertyBase.PropertyValueBitProperty,
			PropertyBase.PropertyValueDecimalProperty,
			PropertyBase.PropertyValueBlobProperty,
			PropertyBase.EventHashKeyProperty,
			PropertyBase.EmailHashKeyProperty,
			PropertyBase.ParentObjectIdProperty,
			PropertyBase.RefObjectIdProperty,
			PropertyBase.RefNameProperty,
			PropertyBase.PropIdProperty
		};

		// Token: 0x0400065B RID: 1627
		internal static readonly HygienePropertyDefinition PropertyIdProperty = new HygienePropertyDefinition("PropertyId", typeof(Guid));

		// Token: 0x0400065C RID: 1628
		internal static readonly HygienePropertyDefinition ParentIdProperty = new HygienePropertyDefinition("ParentId", typeof(Guid));

		// Token: 0x0400065D RID: 1629
		internal static readonly HygienePropertyDefinition NamespaceProperty = new HygienePropertyDefinition("Namespace", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400065E RID: 1630
		internal static readonly HygienePropertyDefinition EventHashKeyProperty = CommonMessageTraceSchema.EventHashKeyProperty;

		// Token: 0x0400065F RID: 1631
		internal static readonly HygienePropertyDefinition EmailHashKeyProperty = CommonMessageTraceSchema.EmailHashKeyProperty;

		// Token: 0x04000660 RID: 1632
		internal static readonly HygienePropertyDefinition ParentObjectIdProperty = new HygienePropertyDefinition("ParentObjectId", typeof(Guid?));

		// Token: 0x04000661 RID: 1633
		internal static readonly HygienePropertyDefinition RefObjectIdProperty = new HygienePropertyDefinition("RefObjectId", typeof(Guid?));

		// Token: 0x04000662 RID: 1634
		internal static readonly HygienePropertyDefinition RefNameProperty = new HygienePropertyDefinition("RefName", typeof(string));

		// Token: 0x04000663 RID: 1635
		internal static readonly HygienePropertyDefinition PropIdProperty = new HygienePropertyDefinition("PropId", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000664 RID: 1636
		internal static readonly HygienePropertyDefinition PropertyNameProperty = new HygienePropertyDefinition("PropertyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000665 RID: 1637
		internal static readonly HygienePropertyDefinition PropertyIndexProperty = new HygienePropertyDefinition("PropertyIndex", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000666 RID: 1638
		internal static readonly HygienePropertyDefinition PropertyValueGuidProperty = new HygienePropertyDefinition("PropertyValueGuid", typeof(Guid?));

		// Token: 0x04000667 RID: 1639
		internal static readonly HygienePropertyDefinition PropertyValueIntegerProperty = new HygienePropertyDefinition("PropertyValueInteger", typeof(int?));

		// Token: 0x04000668 RID: 1640
		internal static readonly HygienePropertyDefinition PropertyValueLongProperty = new HygienePropertyDefinition("PropertyValueLong", typeof(long?));

		// Token: 0x04000669 RID: 1641
		internal static readonly HygienePropertyDefinition PropertyValueStringProperty = new HygienePropertyDefinition("PropertyValueString", typeof(string));

		// Token: 0x0400066A RID: 1642
		internal static readonly HygienePropertyDefinition PropertyValueDatetimeProperty = new HygienePropertyDefinition("PropertyValueDatetime", typeof(DateTime?));

		// Token: 0x0400066B RID: 1643
		internal static readonly HygienePropertyDefinition PropertyValueBitProperty = new HygienePropertyDefinition("PropertyValueBit", typeof(bool?));

		// Token: 0x0400066C RID: 1644
		internal static readonly HygienePropertyDefinition PropertyValueDecimalProperty = new HygienePropertyDefinition("PropertyValueDecimal", typeof(decimal?));

		// Token: 0x0400066D RID: 1645
		internal static readonly HygienePropertyDefinition PropertyValueBlobProperty = new HygienePropertyDefinition("PropertyValueBlob", typeof(string));
	}
}
