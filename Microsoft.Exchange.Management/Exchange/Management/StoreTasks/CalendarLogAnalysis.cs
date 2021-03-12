using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000783 RID: 1923
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarLogAnalysis : IComparable<CalendarLogAnalysis>, IComparer<CalendarLogAnalysis>
	{
		// Token: 0x060043B7 RID: 17335 RVA: 0x00115DF0 File Offset: 0x00113FF0
		private CalendarLogAnalysis()
		{
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x00115E10 File Offset: 0x00114010
		internal CalendarLogAnalysis(CalendarLogId identity, Item item, IEnumerable<PropertyDefinition> loadedProperties)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (loadedProperties == null)
			{
				throw new ArgumentNullException("loadedProperties");
			}
			this.identity = identity;
			this.loadedProperties = loadedProperties;
			this.InitializeProperties(item);
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x00115E6A File Offset: 0x0011406A
		internal ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x00115E72 File Offset: 0x00114072
		internal IEnumerable<AnalysisRule> Alerts
		{
			get
			{
				return this.alerts;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x00115E7C File Offset: 0x0011407C
		internal string LocalLogTime
		{
			get
			{
				return this.OriginalLastModifiedTime.ToString("MMM dd, yyyy HH:mm:ss:ffff");
			}
		}

		// Token: 0x17001494 RID: 5268
		internal string this[PropertyDefinition p]
		{
			get
			{
				if (this.InternalProperties.ContainsKey(p))
				{
					object obj = this.properties[p];
					if (obj != null)
					{
						return this.FormatPropertyValue(p, obj);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x00115ED8 File Offset: 0x001140D8
		private string FormatPropertyValue(PropertyDefinition prop, object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (prop.Type == typeof(ExDateTime))
			{
				if (value is ExDateTime)
				{
					return ((ExDateTime)value).ToString("yyyy MM dd HH mm ss ffff");
				}
				return ((DateTime)value).ToString("yyyy MM dd HH mm ss ffff");
			}
			else
			{
				if (prop.Type == typeof(byte[]))
				{
					byte[] bytes = (byte[])value;
					return bytes.To64BitString();
				}
				return value.ToString();
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x00115F60 File Offset: 0x00114160
		internal bool HasAlerts
		{
			get
			{
				return this.Alerts.Count<AnalysisRule>() > 0;
			}
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x00115F70 File Offset: 0x00114170
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x00115F78 File Offset: 0x00114178
		internal ExDateTime OriginalLastModifiedTime { get; private set; }

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x00115F81 File Offset: 0x00114181
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x00115F89 File Offset: 0x00114189
		internal int ItemVersion { get; private set; }

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x00115F92 File Offset: 0x00114192
		internal Dictionary<PropertyDefinition, object> InternalProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x00115F9A File Offset: 0x0011419A
		internal CalendarLogId InternalIdentity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x00115FA2 File Offset: 0x001141A2
		internal void AddAlert(AnalysisRule rule)
		{
			if (!this.alerts.Contains(rule))
			{
				this.alerts.Add(rule.Clone());
			}
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x00115FC4 File Offset: 0x001141C4
		private void InitializeProperties(Item item)
		{
			this.OriginalLastModifiedTime = item.GetProperty(CalendarItemBaseSchema.OriginalLastModifiedTime);
			this.ItemVersion = item.GetProperty(CalendarItemBaseSchema.ItemVersion);
			if (item.StoreObjectId == null)
			{
				this.properties.Add(ItemSchema.Id, item.GetProperty(ItemSchema.Id));
			}
			else
			{
				this.properties.Add(ItemSchema.Id, item.StoreObjectId.ToBase64String());
			}
			foreach (PropertyDefinition propertyDefinition in this.loadedProperties)
			{
				if (propertyDefinition != ItemSchema.Id)
				{
					object obj = item.TryGetProperty(propertyDefinition);
					if (!(obj is PropertyError))
					{
						this.properties.Add(propertyDefinition, obj);
					}
				}
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x00116094 File Offset: 0x00114294
		public static IEnumerable<PropertyDefinition> GetDisplayProperties(IEnumerable<CalendarLogAnalysis> logs)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			foreach (CalendarLogAnalysis calendarLogAnalysis in logs)
			{
				foreach (AnalysisRule analysisRule in calendarLogAnalysis.Alerts)
				{
					list.AddRange(analysisRule.RequiredProperties.Except(list));
				}
			}
			return list;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x00116128 File Offset: 0x00114328
		public static IComparer<CalendarLogAnalysis> GetComparer()
		{
			return new CalendarLogAnalysis();
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x0011612F File Offset: 0x0011432F
		public int CompareTo(CalendarLogAnalysis other)
		{
			return this.Compare(this, other);
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x0011613C File Offset: 0x0011433C
		public int Compare(CalendarLogAnalysis c0, CalendarLogAnalysis c1)
		{
			int num = c0.ItemVersion.CompareTo(c1.ItemVersion);
			if (num == 0)
			{
				num = c0.OriginalLastModifiedTime.CompareTo(c1.OriginalLastModifiedTime);
			}
			return num;
		}

		// Token: 0x04002A1E RID: 10782
		private const string SortableDateFormat = "yyyy MM dd HH mm ss ffff";

		// Token: 0x04002A1F RID: 10783
		private const string DisplayDateFormat = "MMM dd, yyyy HH:mm:ss:ffff";

		// Token: 0x04002A20 RID: 10784
		private CalendarLogId identity;

		// Token: 0x04002A21 RID: 10785
		private List<AnalysisRule> alerts = new List<AnalysisRule>();

		// Token: 0x04002A22 RID: 10786
		private Dictionary<PropertyDefinition, object> properties = new Dictionary<PropertyDefinition, object>();

		// Token: 0x04002A23 RID: 10787
		private IEnumerable<PropertyDefinition> loadedProperties;
	}
}
