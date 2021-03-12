using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000457 RID: 1111
	internal class TraceLoggingEventTypes
	{
		// Token: 0x06003618 RID: 13848 RVA: 0x000CFFC9 File Offset: 0x000CE1C9
		internal TraceLoggingEventTypes(string name, EventTags tags, params Type[] types) : this(tags, name, TraceLoggingEventTypes.MakeArray(types))
		{
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000CFFD9 File Offset: 0x000CE1D9
		internal TraceLoggingEventTypes(string name, EventTags tags, params TraceLoggingTypeInfo[] typeInfos) : this(tags, name, TraceLoggingEventTypes.MakeArray(typeInfos))
		{
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000CFFEC File Offset: 0x000CE1EC
		internal TraceLoggingEventTypes(string name, EventTags tags, ParameterInfo[] paramInfos)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.typeInfos = this.MakeArray(paramInfos);
			this.name = name;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			for (int i = 0; i < this.typeInfos.Length; i++)
			{
				TraceLoggingTypeInfo traceLoggingTypeInfo = this.typeInfos[i];
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				string fieldName = paramInfos[i].Name;
				if (Statics.ShouldOverrideFieldName(fieldName))
				{
					fieldName = traceLoggingTypeInfo.Name;
				}
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, fieldName, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000D00E4 File Offset: 0x000CE2E4
		private TraceLoggingEventTypes(EventTags tags, string defaultName, TraceLoggingTypeInfo[] typeInfos)
		{
			if (defaultName == null)
			{
				throw new ArgumentNullException("defaultName");
			}
			this.typeInfos = typeInfos;
			this.name = defaultName;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			foreach (TraceLoggingTypeInfo traceLoggingTypeInfo in typeInfos)
			{
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, null, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000D01B5 File Offset: 0x000CE3B5
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000D01BD File Offset: 0x000CE3BD
		internal EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000D01C5 File Offset: 0x000CE3C5
		internal EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000D01CD File Offset: 0x000CE3CD
		internal EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000D01D5 File Offset: 0x000CE3D5
		internal EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000D01E0 File Offset: 0x000CE3E0
		internal NameInfo GetNameInfo(string name, EventTags tags)
		{
			NameInfo nameInfo = this.nameInfos.TryGet(new KeyValuePair<string, EventTags>(name, tags));
			if (nameInfo == null)
			{
				nameInfo = this.nameInfos.GetOrAdd(new NameInfo(name, tags, this.typeMetadata.Length));
			}
			return nameInfo;
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000D0220 File Offset: 0x000CE420
		private TraceLoggingTypeInfo[] MakeArray(ParameterInfo[] paramInfos)
		{
			if (paramInfos == null)
			{
				throw new ArgumentNullException("paramInfos");
			}
			List<Type> recursionCheck = new List<Type>(paramInfos.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[paramInfos.Length];
			for (int i = 0; i < paramInfos.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(paramInfos[i].ParameterType, recursionCheck);
			}
			return array;
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000D0270 File Offset: 0x000CE470
		private static TraceLoggingTypeInfo[] MakeArray(Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			List<Type> recursionCheck = new List<Type>(types.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(types[i], recursionCheck);
			}
			return array;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000D02B8 File Offset: 0x000CE4B8
		private static TraceLoggingTypeInfo[] MakeArray(TraceLoggingTypeInfo[] typeInfos)
		{
			if (typeInfos == null)
			{
				throw new ArgumentNullException("typeInfos");
			}
			return (TraceLoggingTypeInfo[])typeInfos.Clone();
		}

		// Token: 0x040017DB RID: 6107
		internal readonly TraceLoggingTypeInfo[] typeInfos;

		// Token: 0x040017DC RID: 6108
		internal readonly string name;

		// Token: 0x040017DD RID: 6109
		internal readonly EventTags tags;

		// Token: 0x040017DE RID: 6110
		internal readonly byte level;

		// Token: 0x040017DF RID: 6111
		internal readonly byte opcode;

		// Token: 0x040017E0 RID: 6112
		internal readonly EventKeywords keywords;

		// Token: 0x040017E1 RID: 6113
		internal readonly byte[] typeMetadata;

		// Token: 0x040017E2 RID: 6114
		internal readonly int scratchSize;

		// Token: 0x040017E3 RID: 6115
		internal readonly int dataCount;

		// Token: 0x040017E4 RID: 6116
		internal readonly int pinCount;

		// Token: 0x040017E5 RID: 6117
		private ConcurrentSet<KeyValuePair<string, EventTags>, NameInfo> nameInfos;
	}
}
