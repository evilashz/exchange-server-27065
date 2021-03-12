using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000614 RID: 1556
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnalysisGroupKey : IEquatable<AnalysisGroupKey>
	{
		// Token: 0x06003FE8 RID: 16360 RVA: 0x0010A680 File Offset: 0x00108880
		public AnalysisGroupKey(object[] item)
		{
			this.item = item;
			string str = this.Subject + "_#_";
			this.isCalendar = (this.CleanGlobalObjectId != null && this.CleanGlobalObjectId.Length > 0);
			if (this.isCalendar)
			{
				this.key = str + this.CleanGlobalObjectId.ToString();
				return;
			}
			this.key = str + this.ReceivedTime.ToString() + "_#_" + this.ItemClass;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x0010A712 File Offset: 0x00108912
		public override string ToString()
		{
			return this.key;
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x0010A71A File Offset: 0x0010891A
		public string Subject
		{
			get
			{
				return (this.item[0] as string) ?? string.Empty;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x0010A732 File Offset: 0x00108932
		public string ItemClass
		{
			get
			{
				return (this.item[3] as string) ?? string.Empty;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x0010A74A File Offset: 0x0010894A
		public byte[] CleanGlobalObjectId
		{
			get
			{
				return this.item[2] as byte[];
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x0010A759 File Offset: 0x00108959
		public ExDateTime ReceivedTime
		{
			get
			{
				if (!(this.item[1] is ExDateTime))
				{
					return ExDateTime.MinValue;
				}
				return (ExDateTime)this.item[1];
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x0010A780 File Offset: 0x00108980
		public QueryFilter Filter
		{
			get
			{
				QueryFilter result;
				if (this.isCalendar)
				{
					QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.NormalizedSubject, this.Subject);
					result = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.CleanGlobalObjectId, this.CleanGlobalObjectId),
						queryFilter
					});
				}
				else
				{
					QueryFilter queryFilter2 = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.NormalizedSubject, this.Subject),
						new TextFilter(StoreObjectSchema.ItemClass, this.ItemClass, MatchOptions.ExactPhrase, MatchFlags.IgnoreCase)
					});
					result = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ReceivedTime, this.ReceivedTime),
						queryFilter2
					});
				}
				return result;
			}
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x0010A83A File Offset: 0x00108A3A
		public DefaultFolderType FolderToSearch()
		{
			if (this.isCalendar)
			{
				return DefaultFolderType.Calendar;
			}
			return DefaultFolderType.AllItems;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x0010A848 File Offset: 0x00108A48
		public bool Equals(AnalysisGroupKey other)
		{
			if (!this.Subject.Equals(other.Subject))
			{
				return false;
			}
			bool flag = this.CleanGlobalObjectId != null && this.CleanGlobalObjectId.Length > 0;
			bool flag2 = other.CleanGlobalObjectId != null && other.CleanGlobalObjectId.Length > 0;
			if (flag ^ flag2)
			{
				return false;
			}
			if (flag)
			{
				return this.CleanGlobalObjectId.Equals(other.CleanGlobalObjectId);
			}
			return ExDateTime.Compare(this.ReceivedTime, other.ReceivedTime) == 0 && this.ItemClass.Equals(other.ItemClass);
		}

		// Token: 0x0400235A RID: 9050
		private object[] item;

		// Token: 0x0400235B RID: 9051
		private string key;

		// Token: 0x0400235C RID: 9052
		private bool isCalendar;
	}
}
