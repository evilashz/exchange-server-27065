using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Search.Mdb;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000002 RID: 2
	internal class CatalogItemStatistics
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal CatalogItemStatistics(List<MdbInfo> mdbInfoList)
		{
			foreach (MdbInfo mdbInfo in mdbInfoList)
			{
				if (mdbInfo.MountedOnLocalServer)
				{
					this.ActiveItems += mdbInfo.NumberOfItems;
					if (mdbInfo.IsInstantSearchEnabled)
					{
						this.ActiveItemsInstantSearchOn += mdbInfo.NumberOfItems;
					}
					else
					{
						this.ActiveItemsInstantSearchOff += mdbInfo.NumberOfItems;
					}
					if (mdbInfo.IsRefinersEnabled)
					{
						this.ActiveItemsRefinersOn += mdbInfo.NumberOfItems;
					}
					else
					{
						this.ActiveItemsRefinersOff += mdbInfo.NumberOfItems;
					}
				}
				else
				{
					this.PassiveItems += mdbInfo.NumberOfItems;
					if (mdbInfo.IsCatalogSuspended)
					{
						this.PassiveItemsCatalogSuspendedOn += mdbInfo.NumberOfItems;
					}
					else
					{
						this.PassiveItemsCatalogSuspendedOff += mdbInfo.NumberOfItems;
					}
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000021EC File Offset: 0x000003EC
		public long ActiveItems { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021F5 File Offset: 0x000003F5
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000021FD File Offset: 0x000003FD
		public long PassiveItems { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002206 File Offset: 0x00000406
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000220E File Offset: 0x0000040E
		public long ActiveItemsInstantSearchOn { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002217 File Offset: 0x00000417
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000221F File Offset: 0x0000041F
		public long ActiveItemsInstantSearchOff { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002228 File Offset: 0x00000428
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002230 File Offset: 0x00000430
		public long ActiveItemsRefinersOn { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002239 File Offset: 0x00000439
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002241 File Offset: 0x00000441
		public long ActiveItemsRefinersOff { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000224A File Offset: 0x0000044A
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002252 File Offset: 0x00000452
		public long PassiveItemsCatalogSuspendedOn { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000225B File Offset: 0x0000045B
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002263 File Offset: 0x00000463
		public long PassiveItemsCatalogSuspendedOff { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x0000226C File Offset: 0x0000046C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"CatalogItemStatistics =  ActiveItems:",
				this.ActiveItems,
				", PassiveItems:",
				this.PassiveItems,
				", ActiveItemsInstantSearchOn:",
				this.ActiveItemsInstantSearchOn,
				", ActiveItemsInstantSearchOff:",
				this.ActiveItemsInstantSearchOff,
				", ActiveItemsRefinersOn:",
				this.ActiveItemsRefinersOn,
				", ActiveItemsRefinersOff:",
				this.ActiveItemsRefinersOff,
				", PassiveItemsCatalogSuspendedOn:",
				this.PassiveItemsCatalogSuspendedOn,
				", PassiveItemsCatalogSuspendedOff:",
				this.PassiveItemsCatalogSuspendedOff
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002340 File Offset: 0x00000540
		internal static string GenerateFeatureStateLoggingInfo(List<MdbInfo> mdbInfoList)
		{
			if (mdbInfoList.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MdbInfo mdbInfo in mdbInfoList)
			{
				stringBuilder.AppendFormat("{0},{1},{2},{3},{4},{5};", new object[]
				{
					mdbInfo.Name,
					mdbInfo.NumberOfItems,
					mdbInfo.MountedOnLocalServer ? "1" : "0",
					mdbInfo.IsInstantSearchEnabled ? "1" : "0",
					mdbInfo.IsRefinersEnabled ? "1" : "0",
					mdbInfo.IsCatalogSuspended ? "1" : "0"
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000001 RID: 1
		private const string CatalogItemStatisticsLoggingFormat = "{0},{1},{2},{3},{4},{5};";
	}
}
