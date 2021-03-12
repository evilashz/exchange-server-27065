using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200006D RID: 109
	internal class ConfigurationSettingBatch<T> : ConfigurablePropertyBag where T : IConfigurable, IPropertyBag
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public ConfigurationSettingBatch(ADObjectId organizationalUnitRoot)
		{
			if (organizationalUnitRoot == null)
			{
				throw new ArgumentNullException("organizationalUnitRoot");
			}
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.Batch = new MultiValuedProperty<T>();
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000CA17 File Offset: 0x0000AC17
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000CA29 File Offset: 0x0000AC29
		private ADObjectId OrganizationalUnitRoot
		{
			get
			{
				return (ADObjectId)this[ConfigurationSettingBatchSchema.OrganizationalUnitRootProp];
			}
			set
			{
				this[ConfigurationSettingBatchSchema.OrganizationalUnitRootProp] = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000CA37 File Offset: 0x0000AC37
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000CA49 File Offset: 0x0000AC49
		private MultiValuedProperty<T> Batch
		{
			get
			{
				return (MultiValuedProperty<T>)this[ConfigurationSettingBatchSchema.BatchProp];
			}
			set
			{
				this[ConfigurationSettingBatchSchema.BatchProp] = value;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000CA58 File Offset: 0x0000AC58
		public void Add(T configurableObject)
		{
			if (configurableObject == null)
			{
				throw new ArgumentNullException("configurableObject");
			}
			object obj = configurableObject[ConfigurationSettingBatchSchema.OrganizationalUnitRootProp];
			if (obj != null && ((obj is Guid && (Guid)obj != this.OrganizationalUnitRoot.ObjectGuid) || (obj is ADObjectId && ((ADObjectId)obj).ObjectGuid != this.OrganizationalUnitRoot.ObjectGuid)))
			{
				throw new InvalidOperationException("Attempt to add object with mismatching tenant id to an existing batch.");
			}
			this.Batch.Add(configurableObject);
		}
	}
}
