using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache
{
	// Token: 0x020001EB RID: 491
	internal class RecipientInfoCacheIntProperty : RecipientInfoCacheProperty, IIntegerProperty, IProperty
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x0007077C File Offset: 0x0006E97C
		public RecipientInfoCacheIntProperty(RecipientInfoCacheEntryElements element)
		{
			if (element != RecipientInfoCacheEntryElements.WeightedRank)
			{
				throw new ArgumentException("The element " + element + " is not an int type!");
			}
			base.State = PropertyState.Modified;
			this.element = element;
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x000707C0 File Offset: 0x0006E9C0
		public int IntegerData
		{
			get
			{
				int num = -1;
				RecipientInfoCacheEntryElements recipientInfoCacheEntryElements = this.element;
				if (recipientInfoCacheEntryElements == RecipientInfoCacheEntryElements.WeightedRank)
				{
					num = (int)((this.entry.DateTimeTicks & 18014398501093376L) >> 23);
				}
				if (num == -1)
				{
					throw new ArgumentException("The element " + this.element + " is not an int type!");
				}
				return num;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00070819 File Offset: 0x0006EA19
		public override void Bind(RecipientInfoCacheEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("Entry is null!");
			}
			this.entry = entry;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00070830 File Offset: 0x0006EA30
		public override void CopyFrom(IProperty srcProperty)
		{
			throw new NotImplementedException("Can't set any Int value! Element: " + this.element);
		}

		// Token: 0x04000C09 RID: 3081
		private const long BitMask = 18014398501093376L;

		// Token: 0x04000C0A RID: 3082
		private RecipientInfoCacheEntryElements element;

		// Token: 0x04000C0B RID: 3083
		private RecipientInfoCacheEntry entry;
	}
}
