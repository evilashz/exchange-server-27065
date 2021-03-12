using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C8B RID: 3211
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NamedPropMap
	{
		// Token: 0x0600705C RID: 28764 RVA: 0x001F1B38 File Offset: 0x001EFD38
		internal NamedPropMap(Action<int> sizeChangedDelegate)
		{
			this.sizeChangedDelegate = sizeChangedDelegate;
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x001F1B90 File Offset: 0x001EFD90
		internal static MiddleTierStoragePerformanceCountersInstance GetPerfCounters()
		{
			if (NamedPropMap.middleTierStoragePerformanceCountersInstanceName.Length == 0)
			{
				Process currentProcess = Process.GetCurrentProcess();
				NamedPropMap.middleTierStoragePerformanceCountersInstanceName = string.Format("{0} - {1}", currentProcess.ProcessName, currentProcess.Id);
			}
			return MiddleTierStoragePerformanceCounters.GetInstance(NamedPropMap.middleTierStoragePerformanceCountersInstanceName);
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x001F1BDC File Offset: 0x001EFDDC
		internal bool TryGetNamedPropFromPropId(ushort propId, out NamedProp namedProp)
		{
			bool result;
			try
			{
				this.lockObject.EnterReadLock();
				result = this.propIdToNamedPropMap.TryGetValue(propId, out namedProp);
			}
			finally
			{
				try
				{
					this.lockObject.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x001F1C34 File Offset: 0x001EFE34
		internal bool TryGetPropIdFromNamedProp(NamedProp namedProp, out ushort propId)
		{
			bool result;
			try
			{
				this.lockObject.EnterReadLock();
				result = this.namedPropToPropIdMap.TryGetValue(namedProp, out propId);
			}
			finally
			{
				try
				{
					this.lockObject.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06007060 RID: 28768 RVA: 0x001F1C8C File Offset: 0x001EFE8C
		internal int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x001F1C94 File Offset: 0x001EFE94
		internal int UnregisterSizeChangedDelegate()
		{
			int result;
			lock (this.sizeChangedDelegateLock)
			{
				this.sizeChangedDelegate = null;
				result = this.size;
			}
			return result;
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x001F1CE0 File Offset: 0x001EFEE0
		internal void AddMapping(bool addUnresolvedProperties, ICollection<ushort> propIds, ICollection<NamedProp> namedProps)
		{
			ICollection<NamedProp> collection = NamedPropertyDefinition.NamedPropertyKey.AddNamedProps(namedProps);
			try
			{
				this.lockObject.EnterWriteLock();
				IEnumerator<NamedProp> enumerator = collection.GetEnumerator();
				IEnumerator<ushort> enumerator2 = propIds.GetEnumerator();
				while (enumerator2.MoveNext() && enumerator.MoveNext())
				{
					NamedProp namedProp = enumerator.Current;
					if (namedProp != null)
					{
						ushort num = enumerator2.Current;
						if ((addUnresolvedProperties || num != 0) && !this.namedPropToPropIdMap.ContainsKey(namedProp))
						{
							int num2 = 0;
							this.namedPropToPropIdMap[namedProp] = num;
							this.perfCounters.NamedPropertyCacheEntries.Increment();
							num2++;
							if (num != 0)
							{
								this.propIdToNamedPropMap[num] = namedProp;
								this.perfCounters.NamedPropertyCacheEntries.Increment();
								num2++;
							}
							lock (this.sizeChangedDelegateLock)
							{
								if (this.sizeChangedDelegate != null)
								{
									this.sizeChangedDelegate(num2);
								}
								this.size += num2;
							}
						}
					}
				}
			}
			finally
			{
				try
				{
					this.lockObject.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x04004D83 RID: 19843
		internal const ushort PropIdUnresolved = 0;

		// Token: 0x04004D84 RID: 19844
		private const int InitialMapSize = 100;

		// Token: 0x04004D85 RID: 19845
		private readonly MiddleTierStoragePerformanceCountersInstance perfCounters = NamedPropMap.GetPerfCounters();

		// Token: 0x04004D86 RID: 19846
		private static string middleTierStoragePerformanceCountersInstanceName = string.Empty;

		// Token: 0x04004D87 RID: 19847
		private Dictionary<NamedProp, ushort> namedPropToPropIdMap = new Dictionary<NamedProp, ushort>(100);

		// Token: 0x04004D88 RID: 19848
		private Dictionary<ushort, NamedProp> propIdToNamedPropMap = new Dictionary<ushort, NamedProp>(100);

		// Token: 0x04004D89 RID: 19849
		private int size;

		// Token: 0x04004D8A RID: 19850
		private Action<int> sizeChangedDelegate;

		// Token: 0x04004D8B RID: 19851
		private object sizeChangedDelegateLock = new object();

		// Token: 0x04004D8C RID: 19852
		private ReaderWriterLockSlim lockObject = new ReaderWriterLockSlim();
	}
}
