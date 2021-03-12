using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000014 RID: 20
	internal abstract class MdbOneToManyPropertyMapping : MdbPropertyMapping
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003F9F File Offset: 0x0000219F
		public MdbOneToManyPropertyMapping(PropertyDefinition propertyDefinition, StorePropertyDefinition[] storePropertyDefinitions, bool isReadOnly, bool isStreamable) : base(propertyDefinition, storePropertyDefinitions)
		{
			this.isReadOnly = isReadOnly;
			this.isStreamable = isStreamable;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003FB8 File Offset: 0x000021B8
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003FC0 File Offset: 0x000021C0
		public override bool IsStreamable
		{
			get
			{
				return this.isStreamable;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003FC8 File Offset: 0x000021C8
		public override object GetPropertyValue(IDictionary<StorePropertyDefinition, object> dictionary)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003FCF File Offset: 0x000021CF
		public override object GetPropertyValue(IItem item, IMdbPropertyMappingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003FD6 File Offset: 0x000021D6
		public override void SetPropertyValue(IDictionary<StorePropertyDefinition, object> dictionary, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003FDD File Offset: 0x000021DD
		public override void SetPropertyValue(IItem item, object value, IMdbPropertyMappingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000041 RID: 65
		private readonly bool isReadOnly;

		// Token: 0x04000042 RID: 66
		private readonly bool isStreamable;
	}
}
