using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E3 RID: 227
	[DataContract]
	public abstract class SearchTextFilter : ResultSizeFilter
	{
		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0005B300 File Offset: 0x00059500
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0005B308 File Offset: 0x00059508
		[DataMember]
		public string SearchText
		{
			get
			{
				return this.searchText;
			}
			set
			{
				this.searchText = value;
			}
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0005B311 File Offset: 0x00059511
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.UpdateFilterProperty();
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0005B31C File Offset: 0x0005951C
		protected string TranslateBasicFilter()
		{
			string text = (this.FilterType == SearchTextFilterType.StartsWith) ? "{0} -like '{1}*'" : ((this.FilterType == SearchTextFilterType.Contains) ? "{0} -like '*{1}*'" : "{0} -eq '{1}'");
			string text2 = null;
			if (!string.IsNullOrEmpty(this.searchText))
			{
				text2 = this.searchText;
				text2 = text2.Replace("'", "''");
				text2 = text2.TrimEnd(new char[]
				{
					'*'
				});
				if (this.FilterType == SearchTextFilterType.Contains)
				{
					text2 = text2.TrimStart(new char[]
					{
						'*'
					});
				}
				if (text2.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder((text2.Length + text.Length + this.FilterableProperties[0].Length) * this.FilterableProperties.Length + 10);
					stringBuilder.Append(string.Format(text, this.FilterableProperties[0], text2));
					for (int i = 1; i < this.FilterableProperties.Length; i++)
					{
						stringBuilder.Append(" -or ");
						stringBuilder.Append(string.Format(text, this.FilterableProperties[i], text2));
					}
					text2 = stringBuilder.ToString();
				}
			}
			return text2;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0005B438 File Offset: 0x00059638
		protected virtual void UpdateFilterProperty()
		{
			base["Filter"] = this.TranslateBasicFilter();
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06001E26 RID: 7718
		protected abstract string[] FilterableProperties { get; }

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x0005B44B File Offset: 0x0005964B
		protected virtual SearchTextFilterType FilterType
		{
			get
			{
				return SearchTextFilterType.StartsWith;
			}
		}

		// Token: 0x04001C01 RID: 7169
		protected const string StartsWithFilter = "{0} -like '{1}*'";

		// Token: 0x04001C02 RID: 7170
		protected const string ContainsFilter = "{0} -like '*{1}*'";

		// Token: 0x04001C03 RID: 7171
		protected const string EqualsFilter = "{0} -eq '{1}'";

		// Token: 0x04001C04 RID: 7172
		public new const string RbacParameters = "?ResultSize&Filter";

		// Token: 0x04001C05 RID: 7173
		private string searchText;
	}
}
