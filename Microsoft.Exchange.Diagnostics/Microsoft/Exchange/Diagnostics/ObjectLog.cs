using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001B0 RID: 432
	internal class ObjectLog<T>
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0002C14C File Offset: 0x0002A34C
		public ObjectLog(ObjectLogSchema schema, ObjectLogConfiguration configuration)
		{
			this.configuration = configuration;
			this.schemaEntries = ObjectLog<T>.GetSchemaEntries(schema);
			this.logSchema = ObjectLog<T>.GetLogSchema(schema, this.schemaEntries);
			this.log = new Log(configuration.FilenamePrefix, new LogHeaderFormatter(this.logSchema, LogHeaderCsvOption.CsvStrict), configuration.LogComponentName, true);
			this.log.Configure(configuration.LoggingFolder, configuration.MaxLogAge, configuration.MaxLogDirSize, configuration.MaxLogFileSize, configuration.BufferLength, configuration.StreamFlushInterval, configuration.Note, configuration.FlushToDisk);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		public static List<IObjectLogPropertyDefinition<T>> GetSchemaEntries(ObjectLogSchema schema)
		{
			List<IObjectLogPropertyDefinition<T>> list = new List<IObjectLogPropertyDefinition<T>>();
			FieldInfo[] fields = schema.GetType().GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			foreach (FieldInfo fieldInfo in fields)
			{
				object value = fieldInfo.GetValue(null);
				ObjectLogSchema objectLogSchema = value as ObjectLogSchema;
				if (objectLogSchema != null)
				{
					list.AddRange(ObjectLog<T>.GetSchemaEntries(objectLogSchema));
				}
				else
				{
					IObjectLogPropertyDefinition<T> objectLogPropertyDefinition = value as IObjectLogPropertyDefinition<T>;
					if (objectLogPropertyDefinition != null)
					{
						if (schema.ExcludedProperties == null || !schema.ExcludedProperties.Contains(objectLogPropertyDefinition.FieldName))
						{
							list.Add(objectLogPropertyDefinition);
						}
					}
					else
					{
						IEnumerable<IObjectLogPropertyDefinition<T>> enumerable = value as IEnumerable<IObjectLogPropertyDefinition<T>>;
						if (enumerable != null)
						{
							foreach (IObjectLogPropertyDefinition<T> objectLogPropertyDefinition2 in enumerable)
							{
								if (schema.ExcludedProperties == null || !schema.ExcludedProperties.Contains(objectLogPropertyDefinition2.FieldName))
								{
									list.Add(objectLogPropertyDefinition2);
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002C2EC File Offset: 0x0002A4EC
		public static LogSchema GetLogSchema(ObjectLogSchema schema)
		{
			List<IObjectLogPropertyDefinition<T>> list = ObjectLog<T>.GetSchemaEntries(schema);
			return ObjectLog<T>.GetLogSchema(schema, list);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002C308 File Offset: 0x0002A508
		public void LogObject(T objectToLog)
		{
			if (!this.configuration.IsEnabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			int num = 1;
			foreach (IObjectLogPropertyDefinition<T> objectLogPropertyDefinition in this.schemaEntries)
			{
				logRowFormatter[num++] = objectLogPropertyDefinition.GetValue(objectToLog);
			}
			this.log.Append(logRowFormatter, 0);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002C390 File Offset: 0x0002A590
		public void CloseLog()
		{
			this.log.Close();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002C39D File Offset: 0x0002A59D
		public void Flush()
		{
			this.log.Flush();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002C3AC File Offset: 0x0002A5AC
		private static LogSchema GetLogSchema(ObjectLogSchema schema, List<IObjectLogPropertyDefinition<T>> schemaEntries)
		{
			List<string> list = new List<string>();
			list.Add("Time");
			foreach (IObjectLogPropertyDefinition<T> objectLogPropertyDefinition in schemaEntries)
			{
				list.Add(objectLogPropertyDefinition.FieldName);
			}
			return new LogSchema(schema.Software, schema.Version, schema.LogType, list.ToArray());
		}

		// Token: 0x040008BF RID: 2239
		private readonly LogSchema logSchema;

		// Token: 0x040008C0 RID: 2240
		private readonly Log log;

		// Token: 0x040008C1 RID: 2241
		private readonly List<IObjectLogPropertyDefinition<T>> schemaEntries;

		// Token: 0x040008C2 RID: 2242
		private readonly ObjectLogConfiguration configuration;
	}
}
