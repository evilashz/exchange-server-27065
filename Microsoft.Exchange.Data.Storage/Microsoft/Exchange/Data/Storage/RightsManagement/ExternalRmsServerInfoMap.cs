using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B5B RID: 2907
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExternalRmsServerInfoMap
	{
		// Token: 0x06006959 RID: 26969 RVA: 0x001C46B8 File Offset: 0x001C28B8
		public ExternalRmsServerInfoMap(string path, int maxCount, IMruDictionaryPerfCounters perfCounters)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			string uniqueFileNameForProcess = RmsClientManagerUtils.GetUniqueFileNameForProcess("ExternalRmsServerInfo_1.dat");
			this.dictionary = new MruDictionary<Uri, ExternalRMSServerInfo>(maxCount, new ExternalRmsServerInfoMap.UriComparer(), perfCounters);
			this.serializer = new MruDictionarySerializer<Uri, ExternalRMSServerInfo>(path, uniqueFileNameForProcess, ExternalRMSServerInfo.ColumnNames, new MruDictionarySerializerRead<Uri, ExternalRMSServerInfo>(ExternalRmsServerInfoMap.TryReadValues), new MruDictionarySerializerWrite<Uri, ExternalRMSServerInfo>(ExternalRmsServerInfoMap.TryWriteValues), perfCounters);
			if (!this.serializer.TryReadFromDisk(this.dictionary))
			{
				ExternalRmsServerInfoMap.Tracer.TraceError<string>(0L, "External Rms Server Info Map failed to read map-file ({0}).", uniqueFileNameForProcess);
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x0600695A RID: 26970 RVA: 0x001C474B File Offset: 0x001C294B
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x001C4758 File Offset: 0x001C2958
		public bool TryGet(Uri key, out ExternalRMSServerInfo entry)
		{
			if (key == null)
			{
				entry = null;
				return false;
			}
			if (!this.dictionary.TryGetValue(key, out entry))
			{
				return false;
			}
			if (entry.ExpiryTime != DateTime.MaxValue && entry.ExpiryTime < DateTime.UtcNow)
			{
				ExternalRmsServerInfoMap.Tracer.TraceDebug<Uri>(0L, "External Rms Server Info Map removed expired negative entry for key ({0}).", key);
				this.Remove(key);
				entry = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x001C47C9 File Offset: 0x001C29C9
		public void Add(ExternalRMSServerInfo entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.dictionary.Add(entry.KeyUri, entry);
			this.serializer.TryWriteToDisk(this.dictionary);
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x001C47FD File Offset: 0x001C29FD
		public void Remove(Uri key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.dictionary.Remove(key))
			{
				this.serializer.TryWriteToDisk(this.dictionary);
			}
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x001C4833 File Offset: 0x001C2A33
		internal static bool TryReadValues(string[] values, out Uri key, out ExternalRMSServerInfo value)
		{
			if (ExternalRMSServerInfo.TryParse(values, out value))
			{
				key = value.KeyUri;
				return true;
			}
			key = null;
			return false;
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x001C484D File Offset: 0x001C2A4D
		internal static bool TryWriteValues(Uri key, ExternalRMSServerInfo value, out string[] values)
		{
			if (key == null || value == null)
			{
				ExternalRmsServerInfoMap.Tracer.TraceDebug(0L, "External Rms Server Info Map failed to write values.");
				values = null;
				return false;
			}
			values = value.ToStringArray();
			return true;
		}

		// Token: 0x04003BFF RID: 15359
		private const string MapFileSuffix = "ExternalRmsServerInfo_1.dat";

		// Token: 0x04003C00 RID: 15360
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04003C01 RID: 15361
		private MruDictionary<Uri, ExternalRMSServerInfo> dictionary;

		// Token: 0x04003C02 RID: 15362
		private MruDictionarySerializer<Uri, ExternalRMSServerInfo> serializer;

		// Token: 0x02000B5C RID: 2908
		internal sealed class UriComparer : IComparer<Uri>
		{
			// Token: 0x06006961 RID: 26977 RVA: 0x001C4886 File Offset: 0x001C2A86
			int IComparer<Uri>.Compare(Uri x, Uri y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return Uri.Compare(x, y, UriComponents.HostAndPort, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
