using System;
using System.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200013E RID: 318
	public class RecipientTypeFilter<T> : IEnumerableFilter<T> where T : IConfigurable, new()
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x00024318 File Offset: 0x00022518
		private RecipientTypeFilter(RecipientType[] recipientTypes)
		{
			if (recipientTypes == null)
			{
				throw new ArgumentNullException("recipientTypes");
			}
			if (recipientTypes.Length == 0)
			{
				throw new ArgumentException("RecipientTypes parameter should never be empty", "recipientTypes");
			}
			this.recipientTypes = new RecipientType[recipientTypes.Length];
			recipientTypes.CopyTo(this.recipientTypes, 0);
			Array.Sort<RecipientType>(this.recipientTypes);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00024374 File Offset: 0x00022574
		public static RecipientTypeFilter<T> GetRecipientTypeFilter(RecipientType[] recipientTypes)
		{
			return new RecipientTypeFilter<T>(recipientTypes);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002437C File Offset: 0x0002257C
		public bool AcceptElement(T element)
		{
			if (element == null)
			{
				return false;
			}
			IList list = this.recipientTypes;
			ADRecipient adrecipient = element as ADRecipient;
			if (adrecipient != null)
			{
				return list.Contains(adrecipient.RecipientType);
			}
			ReducedRecipient reducedRecipient = element as ReducedRecipient;
			return reducedRecipient != null && list.Contains(reducedRecipient.RecipientType);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000243E0 File Offset: 0x000225E0
		public override bool Equals(object obj)
		{
			RecipientTypeFilter<T> recipientTypeFilter = obj as RecipientTypeFilter<T>;
			if (recipientTypeFilter == null)
			{
				return false;
			}
			if (this.recipientTypes.Length != recipientTypeFilter.recipientTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < this.recipientTypes.Length; i++)
			{
				if (this.recipientTypes[i] != recipientTypeFilter.recipientTypes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00024438 File Offset: 0x00022638
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = typeof(RecipientTypeFilter<T>).GetHashCode();
				foreach (RecipientType recipientType in this.recipientTypes)
				{
					this.hashCode = (int)(this.hashCode + recipientType);
				}
			}
			return this.hashCode;
		}

		// Token: 0x0400029E RID: 670
		private int hashCode;

		// Token: 0x0400029F RID: 671
		private RecipientType[] recipientTypes;
	}
}
