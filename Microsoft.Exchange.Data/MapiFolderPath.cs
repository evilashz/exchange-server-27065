using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	public sealed class MapiFolderPath : IEnumerable<string>, IEnumerable, IEquatable<MapiFolderPath>
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00025ABA File Offset: 0x00023CBA
		public IEnumerator<string> GetEnumerator()
		{
			return new MapiFolderPath.LevelHierarchyEnumerator(this);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00025AC7 File Offset: 0x00023CC7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00025AD0 File Offset: 0x00023CD0
		public bool Equals(MapiFolderPath other)
		{
			if (object.ReferenceEquals(null, other) || (this.isNonIpmPath ^ other.isNonIpmPath) || this.levelHierarchy.Length != other.levelHierarchy.Length)
			{
				return false;
			}
			if (this.levelHierarchy == other.levelHierarchy)
			{
				return true;
			}
			int num = 0;
			while (this.levelHierarchy.Length > num)
			{
				if (!this.levelHierarchy[num].Equals(other.levelHierarchy[num], StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00025B47 File Offset: 0x00023D47
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapiFolderPath);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00025B58 File Offset: 0x00023D58
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(this.isNonIpmPath.GetHashCode());
				foreach (string text in this)
				{
					this.hashCode = (this.hashCode << 13 | (int)((uint)this.hashCode.Value >> 19));
					this.hashCode ^= text.ToUpperInvariant().GetHashCode();
				}
			}
			return this.hashCode.Value;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00025C7C File Offset: 0x00023E7C
		public override string ToString()
		{
			if (this.literalRepresentation == null)
			{
				char value = '\\';
				foreach (string text in this)
				{
					if (-1 != text.IndexOf('\\'))
					{
						value = '￾';
						break;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (this.isNonIpmPath)
				{
					stringBuilder.Append(value);
					stringBuilder.Append("NON_IPM_SUBTREE");
				}
				foreach (string value2 in this)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(value2);
				}
				this.literalRepresentation = ((stringBuilder.Length == 0) ? MapiFolderPath.IpmSubtreeRoot.ToString() : stringBuilder.ToString());
			}
			return this.literalRepresentation;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00025D70 File Offset: 0x00023F70
		public static MapiFolderPath Parse(string input)
		{
			return new MapiFolderPath(input);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00025D78 File Offset: 0x00023F78
		public static bool operator ==(MapiFolderPath operand1, MapiFolderPath operand2)
		{
			return object.Equals(operand1, operand2);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00025D81 File Offset: 0x00023F81
		public static bool operator !=(MapiFolderPath operand1, MapiFolderPath operand2)
		{
			return !object.Equals(operand1, operand2);
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00025D8D File Offset: 0x00023F8D
		public bool IsIpmPath
		{
			get
			{
				return !this.isNonIpmPath;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00025D98 File Offset: 0x00023F98
		public bool IsNonIpmPath
		{
			get
			{
				return this.isNonIpmPath;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00025DA0 File Offset: 0x00023FA0
		public bool IsSubtreeRoot
		{
			get
			{
				return 0 == this.Depth;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00025DAB File Offset: 0x00023FAB
		public int Depth
		{
			get
			{
				return this.levelHierarchy.Length;
			}
		}

		// Token: 0x170003DF RID: 991
		public string this[int level]
		{
			get
			{
				if (0 > level || this.Depth <= level)
				{
					throw new ArgumentException("level");
				}
				return this.levelHierarchy[level];
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00025DD7 File Offset: 0x00023FD7
		public string Name
		{
			get
			{
				if (this.IsSubtreeRoot)
				{
					return string.Empty;
				}
				return this[this.Depth - 1];
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00025DF5 File Offset: 0x00023FF5
		public MapiFolderPath Parent
		{
			get
			{
				if (this.IsSubtreeRoot)
				{
					return null;
				}
				if (null == this.parent)
				{
					this.parent = new MapiFolderPath(this, 1, false);
				}
				return this.parent;
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00025E23 File Offset: 0x00024023
		public static MapiFolderPath GenerateFolderPath(MapiFolderPath parentFolerPath, string subFolderName)
		{
			return MapiFolderPath.GenerateFolderPath(parentFolerPath, subFolderName, true);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00025E30 File Offset: 0x00024030
		public static MapiFolderPath GenerateFolderPath(MapiFolderPath parentFolerPath, string subFolderName, bool autoDetectRoot)
		{
			MapiFolderPath mapiFolderPath = parentFolerPath ?? MapiFolderPath.IpmSubtreeRoot;
			if (!string.IsNullOrEmpty(subFolderName))
			{
				return new MapiFolderPath(mapiFolderPath, subFolderName, autoDetectRoot);
			}
			return mapiFolderPath;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00025E5A File Offset: 0x0002405A
		public static MapiFolderPath GenerateFolderPath(string parentFolderName, MapiFolderPath subFolderPath)
		{
			return MapiFolderPath.GenerateFolderPath(parentFolderName, subFolderPath, true);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00025E64 File Offset: 0x00024064
		public static MapiFolderPath GenerateFolderPath(string parentFolderName, MapiFolderPath subFolderPath, bool autoDetectRoot)
		{
			MapiFolderPath mapiFolderPath = subFolderPath ?? MapiFolderPath.IpmSubtreeRoot;
			if (!string.IsNullOrEmpty(parentFolderName))
			{
				return new MapiFolderPath(parentFolderName, mapiFolderPath, autoDetectRoot);
			}
			return mapiFolderPath;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00025E90 File Offset: 0x00024090
		public MapiFolderPath(string folderPath) : this(folderPath, string.IsNullOrEmpty(folderPath) ? '￾' : folderPath[0], null)
		{
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00025EC3 File Offset: 0x000240C3
		public MapiFolderPath(string folderPath, bool nonIpmSubtree) : this(folderPath, string.IsNullOrEmpty(folderPath) ? '￾' : folderPath[0], new bool?(nonIpmSubtree))
		{
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00025EE8 File Offset: 0x000240E8
		public MapiFolderPath(string folderPath, char folderSeperator) : this(folderPath, folderSeperator, null)
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00025F06 File Offset: 0x00024106
		public MapiFolderPath(string folderPath, char folderSeperator, bool nonIpmSubtree) : this(folderPath, folderSeperator, new bool?(nonIpmSubtree))
		{
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00025F18 File Offset: 0x00024118
		private MapiFolderPath(string folderPath, char folderSeperator, bool? nonIpmSubtree)
		{
			if ('\\' != folderSeperator && '￾' != folderSeperator)
			{
				throw new FormatException(DataStrings.ExceptionFormatInvalid(folderPath));
			}
			if (string.IsNullOrEmpty(folderPath))
			{
				throw new FormatException(DataStrings.ExceptionFormatInvalid(folderPath));
			}
			if (folderSeperator != folderPath[0])
			{
				throw new FormatException(DataStrings.ExceptionFormatInvalid(folderPath));
			}
			List<string> list = new List<string>();
			int num = 0;
			int num2 = 0;
			while (folderPath.Length > num2)
			{
				if (folderSeperator == folderPath[num2])
				{
					list.Add(folderPath.Substring(num, num2 - num));
					num = 1 + num2;
				}
				if (folderPath.Length - 1 == num2)
				{
					list.Add((num <= num2) ? folderPath.Substring(num, folderPath.Length - num) : string.Empty);
				}
				num2++;
			}
			string[] array = list.ToArray();
			int num3 = 1;
			int num4 = array.Length - 1;
			if (folderSeperator == folderPath[folderPath.Length - 1])
			{
				num4--;
			}
			if (nonIpmSubtree == null && 0 < num4 && "NON_IPM_SUBTREE".Equals(array[num3], StringComparison.OrdinalIgnoreCase))
			{
				num4--;
				num3++;
			}
			int num5 = num3;
			while (num3 + num4 > num5)
			{
				if (string.IsNullOrEmpty(array[num5]))
				{
					throw new FormatException(DataStrings.ExceptionFormatInvalid(folderPath));
				}
				num5++;
			}
			string[] destinationArray = new string[num4];
			Array.Copy(array, num3, destinationArray, 0, num4);
			if (nonIpmSubtree != null && nonIpmSubtree == false && '\\' == folderSeperator)
			{
				this.literalRepresentation = folderPath;
			}
			this.levelHierarchy = destinationArray;
			this.isNonIpmPath = (nonIpmSubtree ?? (2 == num3));
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000260D4 File Offset: 0x000242D4
		private MapiFolderPath(MapiFolderPath parentFolderPath, string subFolderName, bool autoDetectRoot)
		{
			if (null == parentFolderPath)
			{
				throw new ArgumentNullException("parentFolderPath");
			}
			if (string.IsNullOrEmpty(subFolderName))
			{
				throw new ArgumentNullException("subFolderName");
			}
			if (autoDetectRoot && parentFolderPath.IsSubtreeRoot)
			{
				if (string.Equals("IPM_SUBTREE", subFolderName))
				{
					this.isNonIpmPath = false;
					this.levelHierarchy = parentFolderPath.levelHierarchy;
					return;
				}
				if (string.Equals("NON_IPM_SUBTREE", subFolderName))
				{
					this.isNonIpmPath = true;
					this.levelHierarchy = parentFolderPath.levelHierarchy;
					return;
				}
			}
			this.isNonIpmPath = parentFolderPath.isNonIpmPath;
			this.levelHierarchy = new string[1 + parentFolderPath.levelHierarchy.Length];
			Array.Copy(parentFolderPath.levelHierarchy, this.levelHierarchy, parentFolderPath.levelHierarchy.Length);
			this.levelHierarchy[this.levelHierarchy.Length - 1] = subFolderName;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000261A8 File Offset: 0x000243A8
		private MapiFolderPath(string parentFolderName, MapiFolderPath subFolderPath, bool autoDetectRoot)
		{
			if (string.IsNullOrEmpty(parentFolderName))
			{
				throw new ArgumentNullException("parentFolderName");
			}
			if (null == subFolderPath)
			{
				throw new ArgumentNullException("subFolderPath");
			}
			this.isNonIpmPath = subFolderPath.isNonIpmPath;
			string text = parentFolderName;
			if (autoDetectRoot)
			{
				if ("IPM_SUBTREE".Equals(parentFolderName, StringComparison.OrdinalIgnoreCase))
				{
					this.isNonIpmPath = false;
					if (!subFolderPath.isNonIpmPath)
					{
						this.levelHierarchy = subFolderPath.levelHierarchy;
						return;
					}
					text = "NON_IPM_SUBTREE";
				}
				else if ("NON_IPM_SUBTREE".Equals(parentFolderName, StringComparison.OrdinalIgnoreCase))
				{
					this.isNonIpmPath = true;
					if (!subFolderPath.isNonIpmPath)
					{
						this.levelHierarchy = subFolderPath.levelHierarchy;
						return;
					}
					text = "NON_IPM_SUBTREE";
				}
			}
			this.levelHierarchy = new string[1 + subFolderPath.levelHierarchy.Length];
			Array.Copy(subFolderPath.levelHierarchy, 0, this.levelHierarchy, 1, subFolderPath.levelHierarchy.Length);
			this.levelHierarchy[0] = text;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00026290 File Offset: 0x00024490
		private MapiFolderPath(MapiFolderPath source, int ascendedLevel, bool stopAscendingIfRoot)
		{
			if (0 > ascendedLevel)
			{
				throw new ArgumentException("ascendedLevel");
			}
			if (source.levelHierarchy.Length < ascendedLevel)
			{
				if (!stopAscendingIfRoot)
				{
					throw new ArgumentOutOfRangeException("ascendedLevel");
				}
				ascendedLevel = source.levelHierarchy.Length;
			}
			if (source.IsSubtreeRoot)
			{
				this.isNonIpmPath = source.isNonIpmPath;
				this.levelHierarchy = source.levelHierarchy;
				return;
			}
			this.isNonIpmPath = source.isNonIpmPath;
			this.levelHierarchy = new string[source.levelHierarchy.Length - ascendedLevel];
			Array.Copy(source.levelHierarchy, this.levelHierarchy, this.levelHierarchy.Length);
		}

		// Token: 0x0400075A RID: 1882
		public const char DefaultFolderSeparator = '\\';

		// Token: 0x0400075B RID: 1883
		public const char MapiDotNetFolderSeparator = '￾';

		// Token: 0x0400075C RID: 1884
		public const string IpmSubtreeName = "IPM_SUBTREE";

		// Token: 0x0400075D RID: 1885
		public const string NonIpmSubtreeName = "NON_IPM_SUBTREE";

		// Token: 0x0400075E RID: 1886
		public static readonly MapiFolderPath IpmSubtreeRoot = new MapiFolderPath("\\", false);

		// Token: 0x0400075F RID: 1887
		public static readonly MapiFolderPath NonIpmSubtreeRoot = new MapiFolderPath("\\", true);

		// Token: 0x04000760 RID: 1888
		private readonly string[] levelHierarchy;

		// Token: 0x04000761 RID: 1889
		private readonly bool isNonIpmPath;

		// Token: 0x04000762 RID: 1890
		private string literalRepresentation;

		// Token: 0x04000763 RID: 1891
		private int? hashCode;

		// Token: 0x04000764 RID: 1892
		private MapiFolderPath parent;

		// Token: 0x02000171 RID: 369
		[Serializable]
		private struct LevelHierarchyEnumerator : IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00026354 File Offset: 0x00024554
			public string Current
			{
				get
				{
					if (0 > this.currentIndex || this.count <= this.currentIndex)
					{
						throw new InvalidOperationException(DataStrings.ExceptionCurrentIndexOutOfRange(this.currentIndex.ToString(), 0.ToString(), this.count.ToString()));
					}
					return this.levelHierarchy[this.currentIndex];
				}
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x000263B7 File Offset: 0x000245B7
			public void Dispose()
			{
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000263B9 File Offset: 0x000245B9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000C51 RID: 3153 RVA: 0x000263C4 File Offset: 0x000245C4
			public bool MoveNext()
			{
				return this.count > ++this.currentIndex;
			}

			// Token: 0x06000C52 RID: 3154 RVA: 0x000263EA File Offset: 0x000245EA
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x000263F3 File Offset: 0x000245F3
			public LevelHierarchyEnumerator(MapiFolderPath folderPath)
			{
				this.currentIndex = -1;
				this.levelHierarchy = folderPath.levelHierarchy;
				this.count = folderPath.levelHierarchy.Length;
			}

			// Token: 0x04000765 RID: 1893
			private int currentIndex;

			// Token: 0x04000766 RID: 1894
			private readonly int count;

			// Token: 0x04000767 RID: 1895
			private readonly string[] levelHierarchy;
		}
	}
}
