using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200013F RID: 319
	public class PerformanceCounterMemoryMappedFile : IDisposable
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x000228A5 File Offset: 0x00020AA5
		public PerformanceCounterMemoryMappedFile() : this(false)
		{
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000228AE File Offset: 0x00020AAE
		public PerformanceCounterMemoryMappedFile(bool writable)
		{
			this.firstCategoryOffset = 4;
			this.Initialize("netfxcustomperfcounters.1.0", writable);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000228C9 File Offset: 0x00020AC9
		public PerformanceCounterMemoryMappedFile(int size, bool writable)
		{
			this.firstCategoryOffset = 4;
			this.Initialize("netfxcustomperfcounters.1.0", size, writable);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000228E5 File Offset: 0x00020AE5
		public PerformanceCounterMemoryMappedFile(string categoryName) : this(categoryName, false)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000228EF File Offset: 0x00020AEF
		public PerformanceCounterMemoryMappedFile(string categoryName, bool writable)
		{
			this.firstCategoryOffset = 8;
			this.Initialize("netfxcustomperfcounters.1.0" + categoryName, writable);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00022910 File Offset: 0x00020B10
		public PerformanceCounterMemoryMappedFile(string categoryName, int size, bool writable)
		{
			this.firstCategoryOffset = 8;
			this.Initialize("netfxcustomperfcounters.1.0" + categoryName, size, writable);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00022932 File Offset: 0x00020B32
		public CategoryEntry FirstCategory
		{
			get
			{
				return this.firstCategory;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002293A File Offset: 0x00020B3A
		public IntPtr Pointer
		{
			get
			{
				return this.perfCounterFileMapping.IntPtr;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00022948 File Offset: 0x00020B48
		public static PerformanceCounterMemoryMappedFile GetDefaultPerformanceCounterSharedMemory()
		{
			PerformanceCounterMemoryMappedFile performanceCounterMemoryMappedFile = null;
			if (!PerformanceCounterMemoryMappedFile.perfCounterSharedMemories.TryGetValue("netfxcustomperfcounters.1.0", out performanceCounterMemoryMappedFile))
			{
				performanceCounterMemoryMappedFile = new PerformanceCounterMemoryMappedFile();
				PerformanceCounterMemoryMappedFile.perfCounterSharedMemories.Add("netfxcustomperfcounters.1.0", performanceCounterMemoryMappedFile);
			}
			return performanceCounterMemoryMappedFile;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00022984 File Offset: 0x00020B84
		public static PerformanceCounterMemoryMappedFile GetPerformanceCounterSharedMemory(string categoryName)
		{
			PerformanceCounterMemoryMappedFile performanceCounterMemoryMappedFile = null;
			if (!PerformanceCounterMemoryMappedFile.perfCounterSharedMemories.TryGetValue(categoryName, out performanceCounterMemoryMappedFile))
			{
				performanceCounterMemoryMappedFile = new PerformanceCounterMemoryMappedFile(categoryName);
				PerformanceCounterMemoryMappedFile.perfCounterSharedMemories.Add(categoryName, performanceCounterMemoryMappedFile);
			}
			return performanceCounterMemoryMappedFile;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000229B8 File Offset: 0x00020BB8
		public static void RefreshAll()
		{
			foreach (PerformanceCounterMemoryMappedFile performanceCounterMemoryMappedFile in PerformanceCounterMemoryMappedFile.perfCounterSharedMemories.Values)
			{
				performanceCounterMemoryMappedFile.Refresh();
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00022A10 File Offset: 0x00020C10
		public static void CloseAll()
		{
			foreach (PerformanceCounterMemoryMappedFile performanceCounterMemoryMappedFile in PerformanceCounterMemoryMappedFile.perfCounterSharedMemories.Values)
			{
				performanceCounterMemoryMappedFile.Close();
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00022A68 File Offset: 0x00020C68
		public void Initialize(string fileMappingName, bool writable)
		{
			fileMappingName = "Global\\" + fileMappingName.ToLowerInvariant();
			this.perfCounterFileMapping = new FileMapping(fileMappingName, writable);
			this.firstCategory = new CategoryEntry(this.perfCounterFileMapping.IntPtr, this.firstCategoryOffset);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00022AA8 File Offset: 0x00020CA8
		public void Initialize(string fileMappingName, int size, bool writable)
		{
			fileMappingName = "Global\\" + fileMappingName.ToLowerInvariant();
			this.perfCounterFileMapping = new FileMapping(fileMappingName, size, writable);
			this.firstCategory = new CategoryEntry(this.perfCounterFileMapping.IntPtr, this.firstCategoryOffset);
			this.firstCategory = new CategoryEntry(this.perfCounterFileMapping.IntPtr, this.firstCategoryOffset);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00022B0D File Offset: 0x00020D0D
		public void Refresh()
		{
			this.firstCategory = new CategoryEntry(this.perfCounterFileMapping.IntPtr, this.firstCategoryOffset);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00022B2B File Offset: 0x00020D2B
		public void Dispose()
		{
			this.perfCounterFileMapping.Dispose();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00022B38 File Offset: 0x00020D38
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00022B40 File Offset: 0x00020D40
		public CategoryEntry FindCategory(string categoryName)
		{
			CategoryEntry next = this.FirstCategory;
			while (next != null && string.Compare(next.Name, categoryName, StringComparison.OrdinalIgnoreCase) != 0)
			{
				next = next.Next;
			}
			return next;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00022B70 File Offset: 0x00020D70
		public void RemoveCategory(string categoryName, Action<CounterEntry, LifetimeEntry, InstanceEntry> logRemoveInstanceEvent)
		{
			CategoryEntry categoryEntry = this.FindCategory(categoryName);
			if (categoryEntry != null)
			{
				for (InstanceEntry instanceEntry = categoryEntry.FirstInstance; instanceEntry != null; instanceEntry = instanceEntry.Next)
				{
					CounterEntry firstCounter = instanceEntry.FirstCounter;
					if (firstCounter != null)
					{
						LifetimeEntry lifetime = firstCounter.Lifetime;
						if (lifetime != null && lifetime.Type == 1)
						{
							instanceEntry.RefCount = 0;
							logRemoveInstanceEvent(firstCounter, lifetime, instanceEntry);
						}
					}
				}
			}
		}

		// Token: 0x04000638 RID: 1592
		public const int DefaultSharedMemorySize = 524288;

		// Token: 0x04000639 RID: 1593
		public const int SeparateSharedMemorySize = 131072;

		// Token: 0x0400063A RID: 1594
		public const int MinSharedMemorySize = 32768;

		// Token: 0x0400063B RID: 1595
		public const int MaxSharedMemorySize = 33554432;

		// Token: 0x0400063C RID: 1596
		private const string DefaultFileMappingName = "netfxcustomperfcounters.1.0";

		// Token: 0x0400063D RID: 1597
		private const string FileMappingPrefix = "netfxcustomperfcounters.1.0";

		// Token: 0x0400063E RID: 1598
		private static Dictionary<string, PerformanceCounterMemoryMappedFile> perfCounterSharedMemories = new Dictionary<string, PerformanceCounterMemoryMappedFile>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400063F RID: 1599
		private FileMapping perfCounterFileMapping;

		// Token: 0x04000640 RID: 1600
		private CategoryEntry firstCategory;

		// Token: 0x04000641 RID: 1601
		private int firstCategoryOffset;
	}
}
