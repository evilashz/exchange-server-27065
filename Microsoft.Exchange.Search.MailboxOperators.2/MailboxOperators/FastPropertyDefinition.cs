using System;
using Microsoft.Ceres.SearchCore.Admin.Config;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200000F RID: 15
	internal class FastPropertyDefinition : IRetrieverPropertyDefinition
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00005CCD File Offset: 0x00003ECD
		internal FastPropertyDefinition(FastIndexSystemField indexSystemField, PropertyDefinitionAttribute attributes, StorePropertyDefinition sourcePropertyDefinition, Action<RetrieverProducer, object> setAction) : this(indexSystemField, attributes, sourcePropertyDefinition, null, setAction)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005CDB File Offset: 0x00003EDB
		internal FastPropertyDefinition(FastIndexSystemField indexSystemField, PropertyDefinitionAttribute attributes, Func<Item, object> getAction, Action<RetrieverProducer, object> setAction) : this(indexSystemField, attributes, null, getAction, setAction)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005CEC File Offset: 0x00003EEC
		internal FastPropertyDefinition(FastIndexSystemField indexSystemField, PropertyDefinitionAttribute attributes, StorePropertyDefinition sourcePropertyDefinition, Func<Item, object> getAction, Action<RetrieverProducer, object> setAction) : this(indexSystemField, attributes, (sourcePropertyDefinition == null) ? new StorePropertyDefinition[0] : new StorePropertyDefinition[]
		{
			sourcePropertyDefinition
		}, getAction, setAction)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005D1C File Offset: 0x00003F1C
		internal FastPropertyDefinition(FastIndexSystemField indexSystemField, PropertyDefinitionAttribute attributes, StorePropertyDefinition[] sourcePropertyDefinitions, Func<Item, object> getAction, Action<RetrieverProducer, object> setAction) : this(indexSystemField, attributes, sourcePropertyDefinitions, null, getAction, setAction)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005D2C File Offset: 0x00003F2C
		internal FastPropertyDefinition(FastIndexSystemField indexSystemField, PropertyDefinitionAttribute attributes, StorePropertyDefinition[] sourcePropertyDefinitions, Func<Item, RetrieverProducer, bool> predicate, Func<Item, object> getAction, Action<RetrieverProducer, object> setAction) : this(indexSystemField.Name, indexSystemField.Definition.Type, attributes, sourcePropertyDefinitions, predicate, getAction, setAction)
		{
			this.FieldDefinition = indexSystemField;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005D54 File Offset: 0x00003F54
		internal FastPropertyDefinition(string name, IndexSystemFieldType type, PropertyDefinitionAttribute attributes, StorePropertyDefinition[] sourcePropertyDefinitions, Func<Item, RetrieverProducer, bool> predicate, Func<Item, object> getAction, Action<RetrieverProducer, object> setAction)
		{
			Util.ThrowOnNullArgument(setAction, "setAction");
			this.predicate = predicate;
			this.setAction = setAction;
			this.getAction = getAction;
			this.SourcePropertyDefinitions = (sourcePropertyDefinitions ?? new StorePropertyDefinition[0]);
			this.Name = name;
			this.Type = type;
			this.Attributes = attributes;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005DB2 File Offset: 0x00003FB2
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005DBA File Offset: 0x00003FBA
		public string Name { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005DC3 File Offset: 0x00003FC3
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005DCB File Offset: 0x00003FCB
		public PropertyDefinitionAttribute Attributes { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005DD4 File Offset: 0x00003FD4
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005DDC File Offset: 0x00003FDC
		public FastIndexSystemField FieldDefinition { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005DE5 File Offset: 0x00003FE5
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005DED File Offset: 0x00003FED
		public IndexSystemFieldType Type { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005DF6 File Offset: 0x00003FF6
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005DFE File Offset: 0x00003FFE
		public StorePropertyDefinition[] SourcePropertyDefinitions { get; private set; }

		// Token: 0x060000CF RID: 207 RVA: 0x00005E07 File Offset: 0x00004007
		internal bool ShouldProcess(Item item, RetrieverProducer producer)
		{
			return this.predicate == null || this.predicate(item, producer);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005E20 File Offset: 0x00004020
		internal void SetValue(RetrieverProducer producer, object value)
		{
			this.setAction(producer, value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005E30 File Offset: 0x00004030
		internal object GetValue(Item item)
		{
			if (this.getAction == null)
			{
				object obj = item.TryGetProperty(this.SourcePropertyDefinitions[0]);
				if (PropertyError.IsPropertyError(obj))
				{
					return null;
				}
				return obj;
			}
			else
			{
				object obj2 = this.getAction(item);
				if (PropertyError.IsPropertyError(obj2))
				{
					return null;
				}
				return obj2;
			}
		}

		// Token: 0x040000CB RID: 203
		private Func<Item, RetrieverProducer, bool> predicate;

		// Token: 0x040000CC RID: 204
		private Func<Item, object> getAction;

		// Token: 0x040000CD RID: 205
		private Action<RetrieverProducer, object> setAction;
	}
}
