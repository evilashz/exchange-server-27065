using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000459 RID: 1113
	internal abstract class TraceLoggingTypeInfo
	{
		// Token: 0x06003636 RID: 13878 RVA: 0x000D069E File Offset: 0x000CE89E
		internal TraceLoggingTypeInfo(Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			this.name = dataType.Name;
			this.dataType = dataType;
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000D06DC File Offset: 0x000CE8DC
		internal TraceLoggingTypeInfo(Type dataType, string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			if (name == null)
			{
				throw new ArgumentNullException("eventName");
			}
			Statics.CheckName(name);
			this.name = name;
			this.keywords = keywords;
			this.level = level;
			this.opcode = opcode;
			this.tags = tags;
			this.dataType = dataType;
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000D0752 File Offset: 0x000CE952
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000D075A File Offset: 0x000CE95A
		public EventLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x000D0762 File Offset: 0x000CE962
		public EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000D076A File Offset: 0x000CE96A
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000D0772 File Offset: 0x000CE972
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000D077A File Offset: 0x000CE97A
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x0600363E RID: 13886
		public abstract void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format);

		// Token: 0x0600363F RID: 13887
		public abstract void WriteObjectData(TraceLoggingDataCollector collector, object value);

		// Token: 0x06003640 RID: 13888 RVA: 0x000D0782 File Offset: 0x000CE982
		public virtual object GetData(object value)
		{
			return value;
		}

		// Token: 0x040017EA RID: 6122
		private readonly string name;

		// Token: 0x040017EB RID: 6123
		private readonly EventKeywords keywords;

		// Token: 0x040017EC RID: 6124
		private readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x040017ED RID: 6125
		private readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x040017EE RID: 6126
		private readonly EventTags tags;

		// Token: 0x040017EF RID: 6127
		private readonly Type dataType;
	}
}
