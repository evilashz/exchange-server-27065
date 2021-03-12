using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B53 RID: 2899
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RmsLicenseStoreInfoMap
	{
		// Token: 0x0600691F RID: 26911 RVA: 0x001C2D9C File Offset: 0x001C0F9C
		public RmsLicenseStoreInfoMap(string path, int maxCount, IMruDictionaryPerfCounters perfCounters)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			this.path = path;
			string uniqueFileNameForProcess = RmsClientManagerUtils.GetUniqueFileNameForProcess("RmsLicenseStoreInfoMap_2.dat");
			this.dictionary = new MruDictionary<MultiValueKey, RmsLicenseStoreInfo>(maxCount, new RmsLicenseStoreInfoMap.MultiValueKeyComparer(), perfCounters);
			this.dictionary.OnRemoved += this.MruDictionaryOnRemoved;
			this.dictionary.OnReplaced += this.MruDictionaryOnReplaced;
			this.serializer = new MruDictionarySerializer<MultiValueKey, RmsLicenseStoreInfo>(path, uniqueFileNameForProcess, RmsLicenseStoreInfo.ColumnNames, new MruDictionarySerializerRead<MultiValueKey, RmsLicenseStoreInfo>(RmsLicenseStoreInfoMap.TryReadValues), new MruDictionarySerializerWrite<MultiValueKey, RmsLicenseStoreInfo>(RmsLicenseStoreInfoMap.TryWriteValues), perfCounters);
			if (!this.serializer.TryReadFromDisk(this.dictionary))
			{
				RmsLicenseStoreInfoMap.Tracer.TraceError<string>(0L, "Rms License Store Info Map failed to read map-file ({0}).", uniqueFileNameForProcess);
			}
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x06006920 RID: 26912 RVA: 0x001C2E64 File Offset: 0x001C1064
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x001C2E74 File Offset: 0x001C1074
		public bool TryGet(MultiValueKey key, out RmsLicenseStoreInfo entry)
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
			DateTime utcNow = DateTime.UtcNow;
			if (entry.RacExpire < utcNow || entry.ClcExpire < utcNow)
			{
				RmsLicenseStoreInfoMap.Tracer.TraceDebug<MultiValueKey>(0L, "Rms License Store Info Map removed expired entry for key ({0}).", key);
				this.Remove(key);
				entry = null;
				return false;
			}
			return true;
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x001C2EE0 File Offset: 0x001C10E0
		public void Add(RmsLicenseStoreInfo entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			MultiValueKey key = new MultiValueKey(new object[]
			{
				entry.TenantId,
				entry.Url
			});
			this.dictionary.Add(key, entry);
			this.serializer.TryWriteToDisk(this.dictionary);
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x001C2F3F File Offset: 0x001C113F
		public void Remove(MultiValueKey key)
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

		// Token: 0x06006924 RID: 26916 RVA: 0x001C2F70 File Offset: 0x001C1170
		private static bool TryReadValues(string[] values, out MultiValueKey key, out RmsLicenseStoreInfo value)
		{
			if (RmsLicenseStoreInfo.TryParse(values, out value))
			{
				key = new MultiValueKey(new object[]
				{
					value.TenantId,
					value.Url
				});
				return true;
			}
			key = null;
			return false;
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x001C2FB4 File Offset: 0x001C11B4
		private static bool TryWriteValues(MultiValueKey key, RmsLicenseStoreInfo value, out string[] values)
		{
			if (key == null || value == null)
			{
				RmsLicenseStoreInfoMap.Tracer.TraceDebug(0L, "Rms License Store Info Map failed to write values.");
				values = null;
				return false;
			}
			values = value.ToStringArray();
			return true;
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x001C2FDC File Offset: 0x001C11DC
		private void MruDictionaryOnRemoved(object sender, MruDictionaryElementRemovedEventArgs<MultiValueKey, RmsLicenseStoreInfo> e)
		{
			if (e.KeyValuePair.Value != null)
			{
				this.DeleteFile(e.KeyValuePair.Value.RacFileName);
				this.DeleteFile(e.KeyValuePair.Value.ClcFileName);
			}
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x001C3030 File Offset: 0x001C1230
		private void MruDictionaryOnReplaced(object sender, MruDictionaryElementReplacedEventArgs<MultiValueKey, RmsLicenseStoreInfo> e)
		{
			if (e.OldKeyValuePair.Value != null && e.NewKeyValuePair.Value != null)
			{
				if (!string.Equals(e.OldKeyValuePair.Value.RacFileName, e.NewKeyValuePair.Value.RacFileName, StringComparison.OrdinalIgnoreCase))
				{
					this.DeleteFile(e.OldKeyValuePair.Value.RacFileName);
				}
				if (!string.Equals(e.OldKeyValuePair.Value.ClcFileName, e.NewKeyValuePair.Value.ClcFileName, StringComparison.OrdinalIgnoreCase))
				{
					this.DeleteFile(e.OldKeyValuePair.Value.ClcFileName);
				}
			}
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x001C30F8 File Offset: 0x001C12F8
		private bool DeleteFile(string file)
		{
			try
			{
				if (!string.IsNullOrEmpty(file))
				{
					File.Delete(Path.Combine(this.path, file));
					RmsLicenseStoreInfoMap.Tracer.TraceDebug<string>(0L, "Rms License Store Info Map deleted RAC-CLC file ({0}).", file);
					return true;
				}
			}
			catch (IOException arg)
			{
				RmsLicenseStoreInfoMap.Tracer.TraceError<string, IOException>(0L, "Rms License Store Info Map failed to delete RAC-CLC file ({0}). IOException - {1}", file, arg);
			}
			catch (UnauthorizedAccessException arg2)
			{
				RmsLicenseStoreInfoMap.Tracer.TraceError<string, UnauthorizedAccessException>(0L, "Rms License Store Info Map failed to delete RAC-CLC file ({0}). UnauthorizedAccessException - {1}", file, arg2);
			}
			catch (SecurityException arg3)
			{
				RmsLicenseStoreInfoMap.Tracer.TraceError<string, SecurityException>(0L, "Rms License Store Info Map failed to delete RAC-CLC file ({0}). SecurityException - {1}", file, arg3);
			}
			return false;
		}

		// Token: 0x04003BD2 RID: 15314
		private const string MapFileSuffix = "RmsLicenseStoreInfoMap_2.dat";

		// Token: 0x04003BD3 RID: 15315
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04003BD4 RID: 15316
		private MruDictionary<MultiValueKey, RmsLicenseStoreInfo> dictionary;

		// Token: 0x04003BD5 RID: 15317
		private MruDictionarySerializer<MultiValueKey, RmsLicenseStoreInfo> serializer;

		// Token: 0x04003BD6 RID: 15318
		private string path;

		// Token: 0x02000B54 RID: 2900
		private sealed class MultiValueKeyComparer : IComparer<MultiValueKey>
		{
			// Token: 0x0600692A RID: 26922 RVA: 0x001C31B0 File Offset: 0x001C13B0
			public int Compare(MultiValueKey x, MultiValueKey y)
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
				Guid guid = (Guid)x.GetKey(0);
				Guid value = (Guid)y.GetKey(0);
				int num = guid.CompareTo(value);
				if (num == 0)
				{
					Uri uri = (Uri)x.GetKey(1);
					Uri uri2 = (Uri)y.GetKey(1);
					return Uri.Compare(uri, uri2, UriComponents.HostAndPort, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase);
				}
				return num;
			}
		}
	}
}
