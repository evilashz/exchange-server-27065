using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public abstract class ADDirSyncResult : ADRawEntry
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x00057F1A File Offset: 0x0005611A
		internal ADDirSyncResult(ADPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00057F23 File Offset: 0x00056123
		protected ADDirSyncResult()
		{
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00057F2B File Offset: 0x0005612B
		public bool IsDeleted
		{
			get
			{
				return (bool)this.propertyBag[ADDirSyncResultSchema.IsDeleted];
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00057F42 File Offset: 0x00056142
		public bool IsNew
		{
			get
			{
				return this.Contains(ADDirSyncResultSchema.WhenCreated);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00057F4F File Offset: 0x0005614F
		public bool IsRenamed
		{
			get
			{
				return this.Contains(ADDirSyncResultSchema.Name) && !this.IsNew;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00057F69 File Offset: 0x00056169
		public override ObjectId Identity
		{
			get
			{
				return (ADObjectId)this.propertyBag[ADDirSyncResultSchema.Id];
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00057F80 File Offset: 0x00056180
		public string DistinguishedName
		{
			get
			{
				ADObjectId adobjectId = (ADObjectId)this.Identity;
				if (adobjectId != null)
				{
					return adobjectId.DistinguishedName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00057FA8 File Offset: 0x000561A8
		internal override ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000308 RID: 776
		internal override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.GetPropertyValue<object>(propertyDefinition);
			}
			set
			{
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00057FBE File Offset: 0x000561BE
		internal ADDirSyncProperty<T> GetPropertyValue<T>(PropertyDefinition property)
		{
			if (this.Contains((ProviderPropertyDefinition)property))
			{
				return new ADDirSyncProperty<T>((T)((object)this.propertyBag[property]));
			}
			return ADDirSyncProperty<T>.NoChange;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00057FEC File Offset: 0x000561EC
		internal IDictionary<ADPropertyDefinition, object> GetChangedProperties()
		{
			ArrayList arrayList = new ArrayList(this.propertyBag.Keys);
			arrayList.Remove(ADDirSyncResultSchema.Id);
			arrayList.Remove(ADObjectSchema.ObjectState);
			return this.GetChangedProperties(arrayList);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00058027 File Offset: 0x00056227
		internal IDictionary<ADPropertyDefinition, object> GetChangedProperties(ICollection properties)
		{
			return ADDirSyncHelper.GetChangedProperties(properties, this.propertyBag);
		}

		// Token: 0x06001232 RID: 4658
		internal abstract ADDirSyncResult CreateInstance(PropertyBag propertyBag);

		// Token: 0x06001233 RID: 4659 RVA: 0x00058035 File Offset: 0x00056235
		internal override bool SkipFullPropertyValidation(ProviderPropertyDefinition propertyDefinition)
		{
			return true;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00058038 File Offset: 0x00056238
		protected override void ValidateRead(List<ValidationError> errors)
		{
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0005803A File Offset: 0x0005623A
		protected override void ValidateWrite(List<ValidationError> errors)
		{
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0005803C File Offset: 0x0005623C
		private bool Contains(ProviderPropertyDefinition property)
		{
			return ADDirSyncHelper.ContainsProperty(this.propertyBag, property);
		}
	}
}
