using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000694 RID: 1684
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MruDictionarySerializer<TKey, TValue>
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x000390C0 File Offset: 0x000372C0
		public MruDictionarySerializer(string filePath, string fileName, string[] columnNames, MruDictionarySerializerRead<TKey, TValue> callbackReadValues, MruDictionarySerializerWrite<TKey, TValue> callbackWriteValues, IMruDictionaryPerfCounters perfCounters)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("fileName");
			}
			if (columnNames == null || columnNames.Length == 0)
			{
				throw new ArgumentNullException("columnNames");
			}
			if (callbackReadValues == null)
			{
				throw new ArgumentNullException("functionReadValues");
			}
			if (callbackWriteValues == null)
			{
				throw new ArgumentNullException("functionWriteValues");
			}
			this.filePath = filePath;
			this.fileFullName = Path.Combine(this.filePath, fileName);
			this.columnNames = columnNames;
			this.csvTable = this.CreateCsvTable();
			LogSchema schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), fileName, this.columnNames);
			this.headerFormatter = new LogHeaderFormatter(schema);
			this.rowFormatter = new LogRowFormatter(schema);
			this.writeValuesCallback = callbackWriteValues;
			this.readValuesCallback = callbackReadValues;
			this.perfCounters = (perfCounters ?? NoopMruDictionaryPerfCounters.Instance);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000391B8 File Offset: 0x000373B8
		public bool TryWriteToDisk(MruDictionary<TKey, TValue> mruDictionary)
		{
			if (mruDictionary == null)
			{
				return false;
			}
			bool result;
			try
			{
				lock (mruDictionary.SyncRoot)
				{
					using (FileStream fileStream = File.Create(this.CreateDirAndGetFileName(), 4096, FileOptions.SequentialScan))
					{
						if (fileStream == null)
						{
							return false;
						}
						this.headerFormatter.Write(fileStream, DateTime.UtcNow);
						foreach (KeyValuePair<TKey, TValue> keyValuePair in mruDictionary)
						{
							string[] array;
							if (this.writeValuesCallback(keyValuePair.Key, keyValuePair.Value, out array) && array != null)
							{
								for (int i = 0; i < array.Length; i++)
								{
									this.rowFormatter[i] = (string.IsNullOrEmpty(array[i]) ? "-NA-" : array[i]);
								}
								this.rowFormatter.Write(fileStream);
							}
						}
						fileStream.Flush();
					}
				}
				this.perfCounters.FileWrite(1);
				result = true;
			}
			catch (IOException arg)
			{
				MruDictionarySerializer<TKey, TValue>.Tracer.TraceError<IOException>(0L, "TryWriteToDisk() failed with IOException - {0}.", arg);
				result = false;
			}
			catch (UnauthorizedAccessException arg2)
			{
				MruDictionarySerializer<TKey, TValue>.Tracer.TraceError<UnauthorizedAccessException>(0L, "TryWriteToDisk() failed with UnauthorizedAccessException - {0}.", arg2);
				result = false;
			}
			catch (SecurityException arg3)
			{
				MruDictionarySerializer<TKey, TValue>.Tracer.TraceError<SecurityException>(0L, "TryWriteToDisk() failed with SecurityException - {0}.", arg3);
				result = false;
			}
			return result;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00039360 File Offset: 0x00037560
		public bool TryReadFromDisk(MruDictionary<TKey, TValue> mruDictionary)
		{
			if (mruDictionary == null)
			{
				return false;
			}
			bool result;
			try
			{
				using (FileStream fileStream = CsvFieldCache.OpenLogFile(this.CreateDirAndGetFileName()))
				{
					if (fileStream == null)
					{
						return false;
					}
					CsvFieldCache csvFieldCache = new CsvFieldCache(this.csvTable, fileStream, 4096);
					string[] array = new string[this.columnNames.Length];
					while (csvFieldCache.MoveNext(false))
					{
						bool flag = true;
						for (int i = 0; i < this.columnNames.Length; i++)
						{
							if (!MruDictionarySerializer<TKey, TValue>.TryGetString(csvFieldCache, i, out array[i]))
							{
								flag = false;
								break;
							}
						}
						TKey key;
						TValue value;
						if (flag && this.readValuesCallback(array, out key, out value))
						{
							mruDictionary.Add(key, value);
						}
					}
				}
				this.perfCounters.FileRead(1);
				result = true;
			}
			catch (IOException arg)
			{
				MruDictionarySerializer<TKey, TValue>.Tracer.TraceError<IOException>(0L, "TryReadFromDisk() failed with IOException - {0}.", arg);
				result = false;
			}
			catch (UnauthorizedAccessException arg2)
			{
				MruDictionarySerializer<TKey, TValue>.Tracer.TraceError<UnauthorizedAccessException>(0L, "TryReadFromDisk() failed with UnauthorizedAccessException - {0}.", arg2);
				result = false;
			}
			catch (SecurityException arg3)
			{
				MruDictionarySerializer<TKey, TValue>.Tracer.TraceError<SecurityException>(0L, "TryReadFromDisk() failed with SecurityException - {0}.", arg3);
				result = false;
			}
			return result;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000394AC File Offset: 0x000376AC
		private string CreateDirAndGetFileName()
		{
			if (!Directory.Exists(this.filePath))
			{
				Directory.CreateDirectory(this.filePath);
			}
			return this.fileFullName;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000394D0 File Offset: 0x000376D0
		private CsvTable CreateCsvTable()
		{
			CsvField[] array = new CsvField[this.columnNames.Length];
			for (int i = 0; i < this.columnNames.Length; i++)
			{
				array[i] = new CsvField(this.columnNames[i], typeof(string));
			}
			return new CsvTable(array);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0003951E File Offset: 0x0003771E
		private static bool TryGetString(CsvFieldCache cursor, int field, out string value)
		{
			value = (cursor.GetField(field) as string);
			if (!string.IsNullOrEmpty(value))
			{
				value = ((value == "-NA-") ? null : value);
				return true;
			}
			return false;
		}

		// Token: 0x04001E5E RID: 7774
		private const int CsvBufferSize = 4096;

		// Token: 0x04001E5F RID: 7775
		private const string PlaceHolderForEmptyString = "-NA-";

		// Token: 0x04001E60 RID: 7776
		private readonly string[] columnNames;

		// Token: 0x04001E61 RID: 7777
		private readonly CsvTable csvTable;

		// Token: 0x04001E62 RID: 7778
		private readonly MruDictionarySerializerWrite<TKey, TValue> writeValuesCallback;

		// Token: 0x04001E63 RID: 7779
		private readonly MruDictionarySerializerRead<TKey, TValue> readValuesCallback;

		// Token: 0x04001E64 RID: 7780
		private readonly string filePath;

		// Token: 0x04001E65 RID: 7781
		private readonly string fileFullName;

		// Token: 0x04001E66 RID: 7782
		private readonly LogHeaderFormatter headerFormatter;

		// Token: 0x04001E67 RID: 7783
		private readonly LogRowFormatter rowFormatter;

		// Token: 0x04001E68 RID: 7784
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04001E69 RID: 7785
		private IMruDictionaryPerfCounters perfCounters;
	}
}
