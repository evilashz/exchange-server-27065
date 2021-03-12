using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	internal class NsResourceRecord : ConfigurablePropertyBag
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00024D68 File Offset: 0x00022F68
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.ResourceRecordId);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00024D75 File Offset: 0x00022F75
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00024D87 File Offset: 0x00022F87
		public Guid ResourceRecordId
		{
			get
			{
				return (Guid)this[DomainSchema.ResourceRecordId];
			}
			set
			{
				this[DomainSchema.ResourceRecordId] = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00024D9A File Offset: 0x00022F9A
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00024DAC File Offset: 0x00022FAC
		public string NameServer
		{
			get
			{
				return (string)this[DomainSchema.NameServer];
			}
			set
			{
				this[DomainSchema.NameServer] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00024DBF File Offset: 0x00022FBF
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00024DD1 File Offset: 0x00022FD1
		public string DomainName
		{
			get
			{
				return (string)this[DomainSchema.DomainName];
			}
			set
			{
				this[DomainSchema.DomainName] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00024DE4 File Offset: 0x00022FE4
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x00024DF6 File Offset: 0x00022FF6
		public string IpAddress
		{
			get
			{
				return (string)this[DomainSchema.IpAddress];
			}
			set
			{
				this[DomainSchema.IpAddress] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00024E09 File Offset: 0x00023009
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return NsResourceRecord.propertydefinitions;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00024E1B File Offset: 0x0002301B
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F2 RID: 1522
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.ResourceRecordId,
			DomainSchema.NameServer,
			DomainSchema.DomainName,
			DomainSchema.IpAddress
		};
	}
}
