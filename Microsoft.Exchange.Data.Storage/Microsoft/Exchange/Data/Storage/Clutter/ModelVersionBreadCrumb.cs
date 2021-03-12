using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x0200043E RID: 1086
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ModelVersionBreadCrumb
	{
		// Token: 0x0600307C RID: 12412 RVA: 0x000C72AE File Offset: 0x000C54AE
		public ModelVersionBreadCrumb(byte[] bytes)
		{
			ArgumentValidator.ThrowIfNull("bytes", bytes);
			this.versionsReady = new SortedSet<short>(ReverseComparer<short>.Instance);
			this.versionsNotReady = new SortedSet<short>(ReverseComparer<short>.Instance);
			this.Deserialize(bytes);
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x000C72E8 File Offset: 0x000C54E8
		public static byte MaxCapacity
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000C72EF File Offset: 0x000C54EF
		private byte TotalCrumbCount
		{
			get
			{
				return (byte)(this.versionsReady.Count + this.versionsNotReady.Count);
			}
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000C730C File Offset: 0x000C550C
		public byte GetCrumbCount(ModelVersionBreadCrumb.VersionType versionType)
		{
			SortedSet<short> versionSet = this.GetVersionSet(versionType);
			return (byte)versionSet.Count;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000C7328 File Offset: 0x000C5528
		public short GetLatest(ModelVersionBreadCrumb.VersionType versionType)
		{
			SortedSet<short> versionSet = this.GetVersionSet(versionType);
			if (versionSet.Count == 0)
			{
				throw new InvalidOperationException("Bread crumb doesn't have any versions of the specified type");
			}
			return versionSet.First<short>();
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000C7358 File Offset: 0x000C5558
		public bool Contains(short modelVersion, ModelVersionBreadCrumb.VersionType versionType)
		{
			SortedSet<short> versionSet = this.GetVersionSet(versionType);
			return versionSet.Contains(modelVersion);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000C7374 File Offset: 0x000C5574
		public bool Add(short modelVersion, ModelVersionBreadCrumb.VersionType versionType)
		{
			if (this.TotalCrumbCount + 1 > ModelVersionBreadCrumb.MaxCapacity)
			{
				throw new InvalidOperationException("Cannot add anymore crumbs. Model Bread Crumb reached max capacity");
			}
			if (modelVersion < 0)
			{
				throw new ArgumentException("Only non-negative versions can be added to the crumb");
			}
			SortedSet<short> versionSet = this.GetVersionSet(versionType);
			return versionSet.Add(modelVersion);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000C73BC File Offset: 0x000C55BC
		public bool Remove(short modelVersion, ModelVersionBreadCrumb.VersionType versionType)
		{
			SortedSet<short> versionSet = this.GetVersionSet(versionType);
			return versionSet.Remove(modelVersion);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000C73D8 File Offset: 0x000C55D8
		public byte[] Serialize()
		{
			if (this.TotalCrumbCount == 0)
			{
				return Array<byte>.Empty;
			}
			byte[] array = this.InitializeCrumbByteArray((int)this.TotalCrumbCount);
			int num = 2;
			foreach (short value in this.MergeVersionSets())
			{
				BitConverter.GetBytes(value).CopyTo(array, num);
				num += 2;
			}
			return array;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000C7450 File Offset: 0x000C5650
		public IList<short> Prune(int maxNumber, short versionToKeep, ModelVersionBreadCrumb.VersionType versionType)
		{
			if (!this.Contains(versionToKeep, versionType))
			{
				throw new InvalidOperationException("The given input version is not contained in the crumb");
			}
			IList<short> list = new List<short>();
			SortedSet<short> versionSet = this.GetVersionSet(versionType);
			if (versionSet.Count > maxNumber)
			{
				int num = 0;
				bool flag = false;
				foreach (short num2 in versionSet)
				{
					num++;
					bool flag2 = false;
					if (num > maxNumber)
					{
						flag2 = true;
					}
					else if (num == maxNumber && !flag)
					{
						flag2 = true;
					}
					else
					{
						flag = (flag ? flag : (num2 == versionToKeep));
					}
					if (flag2 && num2 != versionToKeep)
					{
						list.Add(num2);
					}
				}
			}
			foreach (short item in list)
			{
				versionSet.Remove(item);
			}
			return list;
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000C7544 File Offset: 0x000C5744
		public override string ToString()
		{
			if (this.TotalCrumbCount != 0)
			{
				return string.Join<short>(",", this.MergeVersionSets());
			}
			return "<Empty>";
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000C7564 File Offset: 0x000C5764
		public IEnumerable<short> GetVersions(ModelVersionBreadCrumb.VersionType versionType)
		{
			return this.GetVersionSet(versionType).AsEnumerable<short>();
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000C7574 File Offset: 0x000C5774
		private SortedSet<short> GetVersionSet(ModelVersionBreadCrumb.VersionType versionType)
		{
			switch (versionType)
			{
			case ModelVersionBreadCrumb.VersionType.Ready:
				return this.versionsReady;
			case ModelVersionBreadCrumb.VersionType.NotReady:
				return this.versionsNotReady;
			default:
				throw new ArgumentException("Invalid version type");
			}
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000C75B0 File Offset: 0x000C57B0
		private IEnumerable<short> MergeVersionSets()
		{
			return this.versionsReady.Union(from version in this.versionsNotReady
			select -version);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000C75F4 File Offset: 0x000C57F4
		private void Deserialize(byte[] bytes)
		{
			if (bytes.Length > 2)
			{
				byte b = bytes[1];
				int num = (int)(2 + 2 * b);
				if (num == bytes.Length)
				{
					for (int i = 2; i < bytes.Length; i += 2)
					{
						short num2 = BitConverter.ToInt16(bytes, i);
						if (num2 < 0)
						{
							this.Add(-num2, ModelVersionBreadCrumb.VersionType.NotReady);
						}
						else
						{
							this.Add(num2, ModelVersionBreadCrumb.VersionType.Ready);
						}
						if (this.TotalCrumbCount == ModelVersionBreadCrumb.MaxCapacity)
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000C7658 File Offset: 0x000C5858
		private byte[] InitializeCrumbByteArray(int count)
		{
			int num = 2 + 2 * count;
			byte[] array = new byte[num];
			array[0] = 0;
			array[1] = (byte)count;
			return array;
		}

		// Token: 0x04001A5C RID: 6748
		private const byte SerializationVersion = 0;

		// Token: 0x04001A5D RID: 6749
		private SortedSet<short> versionsReady;

		// Token: 0x04001A5E RID: 6750
		private SortedSet<short> versionsNotReady;

		// Token: 0x0200043F RID: 1087
		public enum VersionType
		{
			// Token: 0x04001A61 RID: 6753
			Ready,
			// Token: 0x04001A62 RID: 6754
			NotReady
		}
	}
}
