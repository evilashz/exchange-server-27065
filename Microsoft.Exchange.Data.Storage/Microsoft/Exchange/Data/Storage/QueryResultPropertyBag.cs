using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AFA RID: 2810
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class QueryResultPropertyBag : PropertyBag
	{
		// Token: 0x060065F7 RID: 26103 RVA: 0x001B0F94 File Offset: 0x001AF194
		internal static bool CanBinaryValueBeTruncated(byte[] value)
		{
			return value != null && value.Length == 255;
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x001B0FA5 File Offset: 0x001AF1A5
		internal static bool CanStringValueBeTruncated(string value)
		{
			return value != null && value.Length == 255;
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x001B0FB9 File Offset: 0x001AF1B9
		internal static bool CanValueBeTruncated(object value)
		{
			return QueryResultPropertyBag.CanBinaryValueBeTruncated(value as byte[]) || QueryResultPropertyBag.CanStringValueBeTruncated(value as string);
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x001B0FD8 File Offset: 0x001AF1D8
		public QueryResultPropertyBag(StoreSession session, ICollection<PropertyDefinition> columns)
		{
			this.Context.Session = session;
			if (session != null)
			{
				this.ExTimeZone = session.ExTimeZone;
			}
			this.currentRowValues = null;
			this.returnErrorsOnTruncatedProperties = false;
			this.propertyPositions = QueryResultPropertyBag.CreatePropertyPositionsDictionary(columns);
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x001B102B File Offset: 0x001AF22B
		internal QueryResultPropertyBag(QueryResultPropertyBag queryPropertyBagHeaderInfo) : base(queryPropertyBagHeaderInfo)
		{
			this.timeZone = queryPropertyBagHeaderInfo.timeZone;
			this.currentRowValues = queryPropertyBagHeaderInfo.currentRowValues;
			this.propertyPositions = queryPropertyBagHeaderInfo.propertyPositions;
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x001B1064 File Offset: 0x001AF264
		internal QueryResultPropertyBag(IStorePropertyBag storePropertyBag, ICollection<PropertyDefinition> propertyDefinitions, IList<object> propertyValues)
		{
			QueryResultPropertyBag queryResultPropertyBag = (QueryResultPropertyBag)((PropertyBag.StorePropertyBagAdaptor)storePropertyBag).PropertyBag;
			this.Context.Session = queryResultPropertyBag.Context.Session;
			this.timeZone = queryResultPropertyBag.timeZone;
			List<object> list = new List<object>(queryResultPropertyBag.currentRowValues);
			this.propertyPositions = new Dictionary<StorePropertyDefinition, int>(queryResultPropertyBag.propertyPositions);
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				StorePropertyDefinition key = propertyDefinition as StorePropertyDefinition;
				int index;
				if (!this.propertyPositions.TryGetValue(key, out index))
				{
					this.propertyPositions.Add(key, list.Count);
					list.Add(propertyValues[num]);
				}
				else
				{
					list[index] = propertyValues[num];
				}
				num++;
			}
			this.currentRowValues = list.ToArray();
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x001B1168 File Offset: 0x001AF368
		internal QueryResultPropertyBag(StoreSession session, Dictionary<StorePropertyDefinition, int> propertyPositionsDictionary)
		{
			this.Context.Session = session;
			if (session != null)
			{
				this.ExTimeZone = session.ExTimeZone;
			}
			this.currentRowValues = null;
			this.returnErrorsOnTruncatedProperties = false;
			this.propertyPositions = propertyPositionsDictionary;
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x001B11B8 File Offset: 0x001AF3B8
		internal static Dictionary<StorePropertyDefinition, int> CreatePropertyPositionsDictionary(ICollection<PropertyDefinition> columns)
		{
			Dictionary<StorePropertyDefinition, int> dictionary = new Dictionary<StorePropertyDefinition, int>(columns.Count);
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in columns)
			{
				StorePropertyDefinition propertyDefinition2 = (StorePropertyDefinition)propertyDefinition;
				dictionary.Add(InternalSchema.ToStorePropertyDefinition(propertyDefinition2), num++);
			}
			return dictionary;
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x001B1220 File Offset: 0x001AF420
		internal void SetQueryResultRow(PropValue[] row)
		{
			if (row.Length != this.propertyPositions.Count)
			{
				throw new ArgumentException("An array of values is different in size from an array of columns");
			}
			this.currentRowValues = new object[row.Length];
			foreach (KeyValuePair<StorePropertyDefinition, int> keyValuePair in this.propertyPositions)
			{
				StorePropertyDefinition key = keyValuePair.Key;
				int value = keyValuePair.Value;
				this.currentRowValues[value] = MapiPropertyBag.GetValueFromPropValue(this.Context.Session, this.timeZone, key, row[value]);
			}
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x001B12D0 File Offset: 0x001AF4D0
		public void SetQueryResultRow(object[] row)
		{
			if (row.Length != this.propertyPositions.Count)
			{
				throw new ArgumentException("An array of values is different in size from an array of columns");
			}
			this.currentRowValues = row;
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x06006601 RID: 26113 RVA: 0x001B12F4 File Offset: 0x001AF4F4
		public override bool IsDirty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x001B12F7 File Offset: 0x001AF4F7
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return this.propertyPositions.ContainsKey(propertyDefinition);
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x001B1305 File Offset: 0x001AF505
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			return false;
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x001B1308 File Offset: 0x001AF508
		public override void Load(ICollection<PropertyDefinition> propertyDefinitions)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x001B130F File Offset: 0x001AF50F
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			throw new NotSupportedException(ServerStrings.MapiCannotSetProps);
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x001B1320 File Offset: 0x001AF520
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			int num = 0;
			if (this.propertyPositions.TryGetValue(propertyDefinition, out num))
			{
				object obj = this.currentRowValues[num];
				if (this.returnErrorsOnTruncatedProperties && QueryResultPropertyBag.CanValueBeTruncated(obj))
				{
					obj = new PropertyError(propertyDefinition, PropertyErrorCode.PropertyValueTruncated);
				}
				return obj;
			}
			throw new NotInBagPropertyErrorException(propertyDefinition);
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x001B136C File Offset: 0x001AF56C
		internal object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			object[] array = new object[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				array[num++] = base.TryGetProperty(propertyDefinition);
			}
			return array;
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x001B13CC File Offset: 0x001AF5CC
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			throw new NotSupportedException(ServerStrings.MapiCannotDeleteProperties);
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x001B13DD File Offset: 0x001AF5DD
		// (set) Token: 0x0600660A RID: 26122 RVA: 0x001B13E5 File Offset: 0x001AF5E5
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x0600660B RID: 26123 RVA: 0x001B13EE File Offset: 0x001AF5EE
		// (set) Token: 0x0600660C RID: 26124 RVA: 0x001B13F6 File Offset: 0x001AF5F6
		internal bool ReturnErrorsOnTruncatedProperties
		{
			get
			{
				return this.returnErrorsOnTruncatedProperties;
			}
			set
			{
				this.returnErrorsOnTruncatedProperties = value;
			}
		}

		// Token: 0x04003A00 RID: 14848
		private const int MaxStringLengthFromTable = 255;

		// Token: 0x04003A01 RID: 14849
		internal const int MaxBinaryLengthFromTable = 255;

		// Token: 0x04003A02 RID: 14850
		private object[] currentRowValues;

		// Token: 0x04003A03 RID: 14851
		private readonly Dictionary<StorePropertyDefinition, int> propertyPositions;

		// Token: 0x04003A04 RID: 14852
		private ExTimeZone timeZone = ExTimeZone.UtcTimeZone;

		// Token: 0x04003A05 RID: 14853
		private bool returnErrorsOnTruncatedProperties;
	}
}
