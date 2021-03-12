using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002A7 RID: 679
	internal class FormatStore
	{
		// Token: 0x06001B2F RID: 6959 RVA: 0x000D2308 File Offset: 0x000D0508
		public FormatStore()
		{
			this.Nodes = new FormatStore.NodeStore(this);
			this.Styles = new FormatStore.StyleStore(this, FormatStoreData.GlobalStyles);
			this.Strings = new FormatStore.StringValueStore(FormatStoreData.GlobalStringValues);
			this.MultiValues = new FormatStore.MultiValueStore(this, FormatStoreData.GlobalMultiValues);
			this.Text = new FormatStore.TextStore();
			this.Initialize();
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000D236A File Offset: 0x000D056A
		public FormatNode RootNode
		{
			get
			{
				return new FormatNode(this, 1);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000D2373 File Offset: 0x000D0573
		public uint CurrentTextPosition
		{
			get
			{
				return this.Text.CurrentPosition;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000D2380 File Offset: 0x000D0580
		public void Initialize()
		{
			this.Nodes.Initialize();
			this.Styles.Initialize(FormatStoreData.GlobalStyles);
			this.Strings.Initialize(FormatStoreData.GlobalStringValues);
			this.MultiValues.Initialize(FormatStoreData.GlobalMultiValues);
			this.Text.Initialize();
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000D23D4 File Offset: 0x000D05D4
		public void ReleaseValue(PropertyValue value)
		{
			if (value.IsString)
			{
				this.GetStringValue(value).Release();
				return;
			}
			if (value.IsMultiValue)
			{
				this.GetMultiValue(value).Release();
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000D2414 File Offset: 0x000D0614
		public void AddRefValue(PropertyValue value)
		{
			if (value.IsString)
			{
				this.GetStringValue(value).AddRef();
				return;
			}
			if (value.IsMultiValue)
			{
				this.GetMultiValue(value).AddRef();
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000D2452 File Offset: 0x000D0652
		public FormatNode GetNode(int nodeHandle)
		{
			return new FormatNode(this, nodeHandle);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000D245B File Offset: 0x000D065B
		public FormatNode AllocateNode(FormatContainerType type)
		{
			return new FormatNode(this.Nodes, this.Nodes.Allocate(type, this.CurrentTextPosition));
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000D247A File Offset: 0x000D067A
		public FormatNode AllocateNode(FormatContainerType type, uint beginTextPosition)
		{
			return new FormatNode(this.Nodes, this.Nodes.Allocate(type, beginTextPosition));
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000D2494 File Offset: 0x000D0694
		public void FreeNode(FormatNode node)
		{
			this.Nodes.Free(node.Handle);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000D24A8 File Offset: 0x000D06A8
		public FormatStyle AllocateStyle(bool isStatic)
		{
			return new FormatStyle(this, this.Styles.Allocate(isStatic));
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000D24BC File Offset: 0x000D06BC
		public FormatStyle GetStyle(int styleHandle)
		{
			return new FormatStyle(this, styleHandle);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000D24C5 File Offset: 0x000D06C5
		public void FreeStyle(FormatStyle style)
		{
			this.Styles.Free(style.Handle);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000D24D9 File Offset: 0x000D06D9
		public StringValue AllocateStringValue(bool isStatic)
		{
			return new StringValue(this, this.Strings.Allocate(isStatic));
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000D24F0 File Offset: 0x000D06F0
		public StringValue AllocateStringValue(bool isStatic, string value)
		{
			StringValue result = this.AllocateStringValue(isStatic);
			result.SetString(value);
			return result;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000D250E File Offset: 0x000D070E
		public StringValue GetStringValue(PropertyValue propertyValue)
		{
			return new StringValue(this, propertyValue.StringHandle);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000D251D File Offset: 0x000D071D
		public void FreeStringValue(StringValue str)
		{
			this.Strings.Free(str.Handle);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000D2531 File Offset: 0x000D0731
		public MultiValue AllocateMultiValue(bool isStatic)
		{
			return new MultiValue(this, this.MultiValues.Allocate(isStatic));
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000D2545 File Offset: 0x000D0745
		public MultiValue GetMultiValue(PropertyValue propertyValue)
		{
			return new MultiValue(this, propertyValue.MultiValueHandle);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000D2554 File Offset: 0x000D0754
		public void FreeMultiValue(MultiValue multi)
		{
			this.MultiValues.Free(multi.Handle);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000D2568 File Offset: 0x000D0768
		public void InitializeCodepageDetector()
		{
			this.Text.InitializeCodepageDetector();
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000D2575 File Offset: 0x000D0775
		public int GetBestWindowsCodePage()
		{
			return this.Text.GetBestWindowsCodePage();
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000D2582 File Offset: 0x000D0782
		public int GetBestWindowsCodePage(int preferredCodePage)
		{
			return this.Text.GetBestWindowsCodePage(preferredCodePage);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000D2590 File Offset: 0x000D0790
		public void SetTextBoundary()
		{
			this.Text.DoNotMergeNextRun();
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000D259D File Offset: 0x000D079D
		public void AddBlockBoundary()
		{
			if (this.Text.LastRunType != TextRunType.BlockBoundary)
			{
				this.Text.AddSimpleRun(TextRunType.BlockBoundary, 1);
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000D25C2 File Offset: 0x000D07C2
		public void AddMarkupText(char[] textBuffer, int offset, int count)
		{
			this.Text.AddText(TextRunType.Markup, textBuffer, offset, count);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000D25D7 File Offset: 0x000D07D7
		public void AddText(char[] textBuffer, int offset, int count)
		{
			this.Text.AddText(TextRunType.NonSpace, textBuffer, offset, count);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000D25EC File Offset: 0x000D07EC
		public void AddInlineObject()
		{
			this.Text.AddSimpleRun(TextRunType.FirstShort, 1);
			this.Text.DoNotMergeNextRun();
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000D260A File Offset: 0x000D080A
		public void AddSpace(int count)
		{
			this.Text.AddSimpleRun(TextRunType.Space, count);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000D261D File Offset: 0x000D081D
		public void AddLineBreak(int count)
		{
			this.Text.AddSimpleRun(TextRunType.NewLine, count);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000D2630 File Offset: 0x000D0830
		public void AddNbsp(int count)
		{
			this.Text.AddSimpleRun(TextRunType.NbSp, count);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000D2643 File Offset: 0x000D0843
		public void AddTabulation(int count)
		{
			this.Text.AddSimpleRun(TextRunType.Tabulation, count);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000D2656 File Offset: 0x000D0856
		internal TextRun GetTextRun(uint position)
		{
			if (position < this.CurrentTextPosition)
			{
				return this.GetTextRunReally(position);
			}
			return TextRun.Invalid;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000D266E File Offset: 0x000D086E
		internal TextRun GetTextRunReally(uint position)
		{
			return new TextRun(this.Text, position);
		}

		// Token: 0x040020B1 RID: 8369
		internal FormatStore.NodeStore Nodes;

		// Token: 0x040020B2 RID: 8370
		internal FormatStore.StyleStore Styles;

		// Token: 0x040020B3 RID: 8371
		internal FormatStore.StringValueStore Strings;

		// Token: 0x040020B4 RID: 8372
		internal FormatStore.MultiValueStore MultiValues;

		// Token: 0x040020B5 RID: 8373
		internal FormatStore.TextStore Text;

		// Token: 0x020002A8 RID: 680
		[Flags]
		internal enum NodeFlags : byte
		{
			// Token: 0x040020B7 RID: 8375
			OnRightEdge = 1,
			// Token: 0x040020B8 RID: 8376
			OnLeftEdge = 2,
			// Token: 0x040020B9 RID: 8377
			CanFlush = 4,
			// Token: 0x040020BA RID: 8378
			OutOfOrder = 8,
			// Token: 0x040020BB RID: 8379
			Visited = 16
		}

		// Token: 0x020002A9 RID: 681
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct NodeEntry
		{
			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000D267C File Offset: 0x000D087C
			// (set) Token: 0x06001B52 RID: 6994 RVA: 0x000D2684 File Offset: 0x000D0884
			internal int NextFree
			{
				get
				{
					return this.NextSibling;
				}
				set
				{
					this.NextSibling = value;
				}
			}

			// Token: 0x06001B53 RID: 6995 RVA: 0x000D268D File Offset: 0x000D088D
			public void Clean()
			{
				this = default(FormatStore.NodeEntry);
			}

			// Token: 0x06001B54 RID: 6996 RVA: 0x000D2698 File Offset: 0x000D0898
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this.Type.ToString(),
					" (",
					this.Parent.ToString("X"),
					", ",
					this.LastChild.ToString("X"),
					", ",
					this.NextSibling.ToString("X"),
					") ",
					this.BeginTextPosition.ToString("X"),
					" - ",
					this.EndTextPosition.ToString("X")
				});
			}

			// Token: 0x040020BC RID: 8380
			internal FormatContainerType Type;

			// Token: 0x040020BD RID: 8381
			internal FormatStore.NodeFlags NodeFlags;

			// Token: 0x040020BE RID: 8382
			internal TextMapping TextMapping;

			// Token: 0x040020BF RID: 8383
			internal int Parent;

			// Token: 0x040020C0 RID: 8384
			internal int LastChild;

			// Token: 0x040020C1 RID: 8385
			internal int NextSibling;

			// Token: 0x040020C2 RID: 8386
			internal uint BeginTextPosition;

			// Token: 0x040020C3 RID: 8387
			internal uint EndTextPosition;

			// Token: 0x040020C4 RID: 8388
			internal int InheritanceMaskIndex;

			// Token: 0x040020C5 RID: 8389
			internal FlagProperties FlagProperties;

			// Token: 0x040020C6 RID: 8390
			internal PropertyBitMask PropertyMask;

			// Token: 0x040020C7 RID: 8391
			internal Property[] Properties;
		}

		// Token: 0x020002AA RID: 682
		internal struct StyleEntry
		{
			// Token: 0x06001B55 RID: 6997 RVA: 0x000D274F File Offset: 0x000D094F
			public StyleEntry(FlagProperties flagProperties, PropertyBitMask propertyMask, Property[] propertyList)
			{
				this.RefCount = int.MaxValue;
				this.FlagProperties = flagProperties;
				this.PropertyMask = propertyMask;
				this.PropertyList = propertyList;
			}

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000D2771 File Offset: 0x000D0971
			// (set) Token: 0x06001B57 RID: 6999 RVA: 0x000D277E File Offset: 0x000D097E
			internal int NextFree
			{
				get
				{
					return this.FlagProperties.IntegerBag;
				}
				set
				{
					this.FlagProperties.IntegerBag = value;
				}
			}

			// Token: 0x06001B58 RID: 7000 RVA: 0x000D278C File Offset: 0x000D098C
			public void Clean()
			{
				this = default(FormatStore.StyleEntry);
			}

			// Token: 0x040020C8 RID: 8392
			internal int RefCount;

			// Token: 0x040020C9 RID: 8393
			internal FlagProperties FlagProperties;

			// Token: 0x040020CA RID: 8394
			internal PropertyBitMask PropertyMask;

			// Token: 0x040020CB RID: 8395
			internal Property[] PropertyList;
		}

		// Token: 0x020002AB RID: 683
		internal struct MultiValueEntry
		{
			// Token: 0x06001B59 RID: 7001 RVA: 0x000D2795 File Offset: 0x000D0995
			public MultiValueEntry(PropertyValue[] values)
			{
				this.RefCount = int.MaxValue;
				this.Values = values;
				this.NextFree = 0;
			}

			// Token: 0x06001B5A RID: 7002 RVA: 0x000D27B0 File Offset: 0x000D09B0
			public void Clean()
			{
				this = default(FormatStore.MultiValueEntry);
			}

			// Token: 0x040020CC RID: 8396
			internal int RefCount;

			// Token: 0x040020CD RID: 8397
			internal int NextFree;

			// Token: 0x040020CE RID: 8398
			internal PropertyValue[] Values;
		}

		// Token: 0x020002AC RID: 684
		internal struct StringValueEntry
		{
			// Token: 0x06001B5B RID: 7003 RVA: 0x000D27B9 File Offset: 0x000D09B9
			public StringValueEntry(string str)
			{
				this.RefCount = int.MaxValue;
				this.Str = str;
				this.NextFree = 0;
			}

			// Token: 0x06001B5C RID: 7004 RVA: 0x000D27D4 File Offset: 0x000D09D4
			public void Clean()
			{
				this = default(FormatStore.StringValueEntry);
			}

			// Token: 0x040020CF RID: 8399
			internal int RefCount;

			// Token: 0x040020D0 RID: 8400
			internal int NextFree;

			// Token: 0x040020D1 RID: 8401
			internal string Str;
		}

		// Token: 0x020002AD RID: 685
		internal struct InheritaceMask
		{
			// Token: 0x06001B5D RID: 7005 RVA: 0x000D27DD File Offset: 0x000D09DD
			public InheritaceMask(FlagProperties flagProperties, PropertyBitMask propertyMask)
			{
				this.FlagProperties = flagProperties;
				this.PropertyMask = propertyMask;
			}

			// Token: 0x040020D2 RID: 8402
			internal FlagProperties FlagProperties;

			// Token: 0x040020D3 RID: 8403
			internal PropertyBitMask PropertyMask;
		}

		// Token: 0x020002AE RID: 686
		internal class NodeStore
		{
			// Token: 0x06001B5E RID: 7006 RVA: 0x000D27ED File Offset: 0x000D09ED
			public NodeStore(FormatStore store)
			{
				this.store = store;
				this.planes = new FormatStore.NodeEntry[32][];
				this.planes[0] = new FormatStore.NodeEntry[16];
				this.freeListHead = 0;
				this.top = 0;
			}

			// Token: 0x06001B5F RID: 7007 RVA: 0x000D2826 File Offset: 0x000D0A26
			public FormatStore.NodeEntry[] Plane(int handle)
			{
				return this.planes[handle / 1024];
			}

			// Token: 0x06001B60 RID: 7008 RVA: 0x000D2836 File Offset: 0x000D0A36
			public int Index(int handle)
			{
				return handle % 1024;
			}

			// Token: 0x06001B61 RID: 7009 RVA: 0x000D2840 File Offset: 0x000D0A40
			public void Initialize()
			{
				this.freeListHead = -1;
				this.top = 1;
				this.planes[0][1].Type = FormatContainerType.Root;
				this.planes[0][1].NodeFlags = (FormatStore.NodeFlags.OnRightEdge | FormatStore.NodeFlags.CanFlush);
				this.planes[0][1].TextMapping = TextMapping.Unicode;
				this.planes[0][1].Parent = 0;
				this.planes[0][1].NextSibling = 1;
				this.planes[0][1].LastChild = 0;
				this.planes[0][1].BeginTextPosition = 0U;
				this.planes[0][1].EndTextPosition = uint.MaxValue;
				this.planes[0][1].FlagProperties = default(FlagProperties);
				this.planes[0][1].PropertyMask = default(PropertyBitMask);
				this.planes[0][1].Properties = null;
				this.top++;
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x000D2954 File Offset: 0x000D0B54
			public int Allocate(FormatContainerType type, uint currentTextPosition)
			{
				int num = this.freeListHead;
				int num2;
				FormatStore.NodeEntry[] array;
				if (num != -1)
				{
					num2 = num % 1024;
					array = this.planes[num / 1024];
					this.freeListHead = array[num2].NextFree;
				}
				else
				{
					num = this.top++;
					num2 = num % 1024;
					int num3 = num / 1024;
					if (num2 == 0)
					{
						if (num3 == 1024)
						{
							throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
						}
						if (num3 == this.planes.Length)
						{
							int num4 = Math.Min(this.planes.Length * 2, 1024);
							FormatStore.NodeEntry[][] destinationArray = new FormatStore.NodeEntry[num4][];
							Array.Copy(this.planes, 0, destinationArray, 0, this.planes.Length);
							this.planes = destinationArray;
						}
						if (this.planes[num3] == null)
						{
							this.planes[num3] = new FormatStore.NodeEntry[1024];
						}
					}
					else if (num3 == 0 && num2 == this.planes[num3].Length)
					{
						int num5 = Math.Min(this.planes[0].Length * 2, 1024);
						FormatStore.NodeEntry[] array2 = new FormatStore.NodeEntry[num5];
						Array.Copy(this.planes[0], 0, array2, 0, this.planes[0].Length);
						this.planes[0] = array2;
					}
					array = this.planes[num3];
				}
				array[num2].Type = type;
				array[num2].NodeFlags = (FormatStore.NodeFlags)0;
				array[num2].TextMapping = TextMapping.Unicode;
				array[num2].Parent = 0;
				array[num2].LastChild = 0;
				array[num2].NextSibling = num;
				array[num2].BeginTextPosition = currentTextPosition;
				array[num2].EndTextPosition = uint.MaxValue;
				array[num2].FlagProperties.ClearAll();
				array[num2].PropertyMask.ClearAll();
				array[num2].Properties = null;
				return num;
			}

			// Token: 0x06001B63 RID: 7011 RVA: 0x000D2B34 File Offset: 0x000D0D34
			public void Free(int handle)
			{
				int num = handle % 1024;
				FormatStore.NodeEntry[] array = this.planes[handle / 1024];
				if (array[num].Properties != null)
				{
					for (int i = 0; i < array[num].Properties.Length; i++)
					{
						if (array[num].Properties[i].Value.IsRefCountedHandle)
						{
							this.store.ReleaseValue(array[num].Properties[i].Value);
						}
					}
				}
				array[num].NextFree = this.freeListHead;
				this.freeListHead = handle;
			}

			// Token: 0x06001B64 RID: 7012 RVA: 0x000D2BDC File Offset: 0x000D0DDC
			public long DumpStat(TextWriter dumpWriter)
			{
				int num = (this.top < 1024) ? 1 : ((this.top % 1024 == 0) ? (this.top / 1024) : (this.top / 1024 + 1));
				long num2 = (long)((num == 1) ? this.planes[0].Length : (num * 1024));
				long num3 = (long)(12 + this.planes.Length * 4 + 12 * num) + num2 * (long)Marshal.SizeOf(typeof(FormatStore.NodeEntry));
				long num4 = 0L;
				long num5 = 0L;
				long num6 = 0L;
				long num7 = 0L;
				for (int i = 0; i < this.top; i++)
				{
					int num8 = i % 1024;
					FormatStore.NodeEntry[] array = this.planes[i / 1024];
					if (array[num8].Type != FormatContainerType.Null)
					{
						num4 += 1L;
						if (array[num8].Properties != null)
						{
							num3 += (long)(12 + array[num8].Properties.Length * Marshal.SizeOf(typeof(Property)));
							num5 += 1L;
							num6 += (long)array[num8].Properties.Length;
							if ((long)array[num8].Properties.Length > num7)
							{
								num7 = (long)array[num8].Properties.Length;
							}
						}
					}
				}
				long num9 = (num5 == 0L) ? 0L : ((num6 + num5 - 1L) / num5);
				if (dumpWriter != null)
				{
					dumpWriter.WriteLine("Nodes alloc: {0}", num2);
					dumpWriter.WriteLine("Nodes used: {0}", num4);
					dumpWriter.WriteLine("Nodes proplists: {0}", num5);
					if (num5 != 0L)
					{
						dumpWriter.WriteLine("Nodes props: {0}", num6);
						dumpWriter.WriteLine("Nodes average proplist: {0}", num9);
						dumpWriter.WriteLine("Nodes max proplist: {0}", num7);
					}
					dumpWriter.WriteLine("Nodes bytes: {0}", num3);
				}
				return num3;
			}

			// Token: 0x040020D4 RID: 8404
			internal const int MaxElementsPerPlane = 1024;

			// Token: 0x040020D5 RID: 8405
			internal const int MaxPlanes = 1024;

			// Token: 0x040020D6 RID: 8406
			internal const int InitialPlanes = 32;

			// Token: 0x040020D7 RID: 8407
			internal const int InitialElements = 16;

			// Token: 0x040020D8 RID: 8408
			private FormatStore store;

			// Token: 0x040020D9 RID: 8409
			private FormatStore.NodeEntry[][] planes;

			// Token: 0x040020DA RID: 8410
			private int freeListHead;

			// Token: 0x040020DB RID: 8411
			private int top;
		}

		// Token: 0x020002AF RID: 687
		internal class StyleStore
		{
			// Token: 0x06001B65 RID: 7013 RVA: 0x000D2DD4 File Offset: 0x000D0FD4
			public StyleStore(FormatStore store, FormatStore.StyleEntry[] globalStyles)
			{
				this.store = store;
				this.planes = new FormatStore.StyleEntry[16][];
				this.planes[0] = new FormatStore.StyleEntry[Math.Max(32, globalStyles.Length + 1)];
				this.freeListHead = 0;
				this.top = 0;
				if (globalStyles != null && globalStyles.Length != 0)
				{
					Array.Copy(globalStyles, 0, this.planes[0], 0, globalStyles.Length);
				}
			}

			// Token: 0x06001B66 RID: 7014 RVA: 0x000D2E3D File Offset: 0x000D103D
			public FormatStore.StyleEntry[] Plane(int handle)
			{
				return this.planes[handle / 2048];
			}

			// Token: 0x06001B67 RID: 7015 RVA: 0x000D2E4D File Offset: 0x000D104D
			public int Index(int handle)
			{
				return handle % 2048;
			}

			// Token: 0x06001B68 RID: 7016 RVA: 0x000D2E56 File Offset: 0x000D1056
			public void Initialize(FormatStore.StyleEntry[] globalStyles)
			{
				this.freeListHead = -1;
				if (globalStyles != null && globalStyles.Length != 0)
				{
					this.top = globalStyles.Length;
					return;
				}
				this.top = 1;
			}

			// Token: 0x06001B69 RID: 7017 RVA: 0x000D2E78 File Offset: 0x000D1078
			public int Allocate(bool isStatic)
			{
				int num = this.freeListHead;
				int num2;
				FormatStore.StyleEntry[] array;
				if (num != -1)
				{
					num2 = num % 2048;
					array = this.planes[num / 2048];
					this.freeListHead = array[num2].NextFree;
				}
				else
				{
					num = this.top++;
					num2 = num % 2048;
					int num3 = num / 2048;
					if (num2 == 0)
					{
						if (num3 == 512)
						{
							throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
						}
						if (num3 == this.planes.Length)
						{
							int num4 = Math.Min(this.planes.Length * 2, 512);
							FormatStore.StyleEntry[][] destinationArray = new FormatStore.StyleEntry[num4][];
							Array.Copy(this.planes, 0, destinationArray, 0, this.planes.Length);
							this.planes = destinationArray;
						}
						if (this.planes[num3] == null)
						{
							this.planes[num3] = new FormatStore.StyleEntry[2048];
						}
					}
					else if (num3 == 0 && num2 == this.planes[num3].Length)
					{
						int num5 = Math.Min(this.planes[0].Length * 2, 2048);
						FormatStore.StyleEntry[] array2 = new FormatStore.StyleEntry[num5];
						Array.Copy(this.planes[0], 0, array2, 0, this.planes[0].Length);
						this.planes[0] = array2;
					}
					array = this.planes[num3];
				}
				array[num2].PropertyList = null;
				array[num2].RefCount = (isStatic ? int.MaxValue : 1);
				array[num2].FlagProperties.ClearAll();
				array[num2].PropertyMask.ClearAll();
				return num;
			}

			// Token: 0x06001B6A RID: 7018 RVA: 0x000D3008 File Offset: 0x000D1208
			public void Free(int handle)
			{
				int num = handle % 2048;
				FormatStore.StyleEntry[] array = this.planes[handle / 2048];
				if (array[num].PropertyList != null)
				{
					for (int i = 0; i < array[num].PropertyList.Length; i++)
					{
						if (array[num].PropertyList[i].Value.IsRefCountedHandle)
						{
							this.store.ReleaseValue(array[num].PropertyList[i].Value);
						}
					}
				}
				array[num].NextFree = this.freeListHead;
				this.freeListHead = handle;
			}

			// Token: 0x06001B6B RID: 7019 RVA: 0x000D30B0 File Offset: 0x000D12B0
			public long DumpStat(TextWriter dumpWriter)
			{
				int num = (this.top < 2048) ? 1 : ((this.top % 2048 == 0) ? (this.top / 2048) : (this.top / 2048 + 1));
				long num2 = (long)((num == 1) ? this.planes[0].Length : (num * 2048));
				long num3 = (long)(12 + this.planes.Length * 4 + 12 * num) + num2 * (long)Marshal.SizeOf(typeof(FormatStore.StyleEntry));
				long num4 = 0L;
				long num5 = 0L;
				long num6 = 0L;
				long num7 = 0L;
				for (int i = 0; i < this.top; i++)
				{
					int num8 = i % 2048;
					FormatStore.StyleEntry[] array = this.planes[i / 2048];
					if (array[num8].RefCount != 0)
					{
						num4 += 1L;
						if (array[num8].PropertyList != null)
						{
							num3 += (long)(12 + array[num8].PropertyList.Length * Marshal.SizeOf(typeof(Property)));
							num5 += 1L;
							num6 += (long)array[num8].PropertyList.Length;
							if ((long)array[num8].PropertyList.Length > num7)
							{
								num7 = (long)array[num8].PropertyList.Length;
							}
						}
					}
				}
				long num9 = (num5 == 0L) ? 0L : ((num6 + num5 - 1L) / num5);
				if (dumpWriter != null)
				{
					dumpWriter.WriteLine("Styles alloc: {0}", num2);
					dumpWriter.WriteLine("Styles used: {0}", num4);
					dumpWriter.WriteLine("Styles non-null prop lists: {0}", num5);
					if (num5 != 0L)
					{
						dumpWriter.WriteLine("Styles total prop lists length: {0}", num6);
						dumpWriter.WriteLine("Styles average prop list length: {0}", num9);
						dumpWriter.WriteLine("Styles max prop list length: {0}", num7);
					}
					dumpWriter.WriteLine("Styles bytes: {0}", num3);
				}
				return num3;
			}

			// Token: 0x040020DC RID: 8412
			internal const int MaxElementsPerPlane = 2048;

			// Token: 0x040020DD RID: 8413
			internal const int MaxPlanes = 512;

			// Token: 0x040020DE RID: 8414
			internal const int InitialPlanes = 16;

			// Token: 0x040020DF RID: 8415
			internal const int InitialElements = 32;

			// Token: 0x040020E0 RID: 8416
			private FormatStore store;

			// Token: 0x040020E1 RID: 8417
			private FormatStore.StyleEntry[][] planes;

			// Token: 0x040020E2 RID: 8418
			private int freeListHead;

			// Token: 0x040020E3 RID: 8419
			private int top;
		}

		// Token: 0x020002B0 RID: 688
		internal class StringValueStore
		{
			// Token: 0x06001B6C RID: 7020 RVA: 0x000D32A8 File Offset: 0x000D14A8
			public StringValueStore(FormatStore.StringValueEntry[] globalStrings)
			{
				this.planes = new FormatStore.StringValueEntry[16][];
				this.planes[0] = new FormatStore.StringValueEntry[Math.Max(16, globalStrings.Length + 1)];
				this.freeListHead = 0;
				this.top = 0;
				if (globalStrings != null && globalStrings.Length != 0)
				{
					Array.Copy(globalStrings, 0, this.planes[0], 0, globalStrings.Length);
				}
			}

			// Token: 0x06001B6D RID: 7021 RVA: 0x000D330A File Offset: 0x000D150A
			public FormatStore.StringValueEntry[] Plane(int handle)
			{
				return this.planes[handle / 4096];
			}

			// Token: 0x06001B6E RID: 7022 RVA: 0x000D331A File Offset: 0x000D151A
			public int Index(int handle)
			{
				return handle % 4096;
			}

			// Token: 0x06001B6F RID: 7023 RVA: 0x000D3323 File Offset: 0x000D1523
			public void Initialize(FormatStore.StringValueEntry[] globalStrings)
			{
				this.freeListHead = -1;
				if (globalStrings != null && globalStrings.Length != 0)
				{
					this.top = globalStrings.Length;
					return;
				}
				this.top = 1;
			}

			// Token: 0x06001B70 RID: 7024 RVA: 0x000D3348 File Offset: 0x000D1548
			public int Allocate(bool isStatic)
			{
				int num = this.freeListHead;
				int num2;
				FormatStore.StringValueEntry[] array;
				if (num != -1)
				{
					num2 = num % 4096;
					array = this.planes[num / 4096];
					this.freeListHead = array[num2].NextFree;
				}
				else
				{
					num = this.top++;
					num2 = num % 4096;
					int num3 = num / 4096;
					if (num2 == 0)
					{
						if (num3 == 256)
						{
							throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
						}
						if (num3 == this.planes.Length)
						{
							int num4 = Math.Min(this.planes.Length * 2, 256);
							FormatStore.StringValueEntry[][] destinationArray = new FormatStore.StringValueEntry[num4][];
							Array.Copy(this.planes, 0, destinationArray, 0, this.planes.Length);
							this.planes = destinationArray;
						}
						if (this.planes[num3] == null)
						{
							this.planes[num3] = new FormatStore.StringValueEntry[4096];
						}
					}
					else if (num3 == 0 && num2 == this.planes[num3].Length)
					{
						int num5 = Math.Min(this.planes[0].Length * 2, 4096);
						FormatStore.StringValueEntry[] array2 = new FormatStore.StringValueEntry[num5];
						Array.Copy(this.planes[0], 0, array2, 0, this.planes[0].Length);
						this.planes[0] = array2;
					}
					array = this.planes[num3];
				}
				array[num2].Str = null;
				array[num2].RefCount = (isStatic ? int.MaxValue : 1);
				array[num2].NextFree = -1;
				return num;
			}

			// Token: 0x06001B71 RID: 7025 RVA: 0x000D34C0 File Offset: 0x000D16C0
			public void Free(int handle)
			{
				int num = handle % 4096;
				FormatStore.StringValueEntry[] array = this.planes[handle / 4096];
				array[num].NextFree = this.freeListHead;
				this.freeListHead = handle;
			}

			// Token: 0x06001B72 RID: 7026 RVA: 0x000D3500 File Offset: 0x000D1700
			public long DumpStat(TextWriter dumpWriter)
			{
				int num = (this.top < 4096) ? 1 : ((this.top % 4096 == 0) ? (this.top / 4096) : (this.top / 4096 + 1));
				long num2 = (long)((num == 1) ? this.planes[0].Length : (num * 4096));
				long num3 = (long)(12 + this.planes.Length * 4 + 12 * num) + num2 * (long)Marshal.SizeOf(typeof(FormatStore.StringValueEntry));
				long num4 = 0L;
				long num5 = 0L;
				long num6 = 0L;
				long num7 = 0L;
				for (int i = 0; i < this.top; i++)
				{
					int num8 = i % 4096;
					FormatStore.StringValueEntry[] array = this.planes[i / 4096];
					if (array[num8].RefCount != 0)
					{
						num4 += 1L;
						if (array[num8].Str != null)
						{
							num3 += (long)(12 + array[num8].Str.Length * 2);
							num5 += 1L;
							num6 += (long)array[num8].Str.Length;
							if ((long)array[num8].Str.Length > num7)
							{
								num7 = (long)array[num8].Str.Length;
							}
						}
					}
				}
				long num9 = (num5 == 0L) ? 0L : ((num6 + num5 - 1L) / num5);
				if (dumpWriter != null)
				{
					dumpWriter.WriteLine("StringValues alloc: {0}", num2);
					dumpWriter.WriteLine("StringValues used: {0}", num4);
					dumpWriter.WriteLine("StringValues non-null strings: {0}", num5);
					if (num5 != 0L)
					{
						dumpWriter.WriteLine("StringValues total string length: {0}", num6);
						dumpWriter.WriteLine("StringValues average string length: {0}", num9);
						dumpWriter.WriteLine("StringValues max string length: {0}", num7);
					}
					dumpWriter.WriteLine("StringValues bytes: {0}", num3);
				}
				return num3;
			}

			// Token: 0x040020E4 RID: 8420
			internal const int MaxElementsPerPlane = 4096;

			// Token: 0x040020E5 RID: 8421
			internal const int MaxPlanes = 256;

			// Token: 0x040020E6 RID: 8422
			internal const int InitialPlanes = 16;

			// Token: 0x040020E7 RID: 8423
			internal const int InitialElements = 16;

			// Token: 0x040020E8 RID: 8424
			private FormatStore.StringValueEntry[][] planes;

			// Token: 0x040020E9 RID: 8425
			private int freeListHead;

			// Token: 0x040020EA RID: 8426
			private int top;
		}

		// Token: 0x020002B1 RID: 689
		internal class MultiValueStore
		{
			// Token: 0x06001B73 RID: 7027 RVA: 0x000D36F4 File Offset: 0x000D18F4
			public MultiValueStore(FormatStore store, FormatStore.MultiValueEntry[] globaMultiValues)
			{
				this.store = store;
				this.planes = new FormatStore.MultiValueEntry[16][];
				this.planes[0] = new FormatStore.MultiValueEntry[Math.Max(16, globaMultiValues.Length + 1)];
				this.freeListHead = 0;
				this.top = 0;
				if (globaMultiValues != null && globaMultiValues.Length != 0)
				{
					Array.Copy(globaMultiValues, 0, this.planes[0], 0, globaMultiValues.Length);
				}
			}

			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000D375D File Offset: 0x000D195D
			public FormatStore Store
			{
				get
				{
					return this.store;
				}
			}

			// Token: 0x06001B75 RID: 7029 RVA: 0x000D3765 File Offset: 0x000D1965
			public FormatStore.MultiValueEntry[] Plane(int handle)
			{
				return this.planes[handle / 4096];
			}

			// Token: 0x06001B76 RID: 7030 RVA: 0x000D3775 File Offset: 0x000D1975
			public int Index(int handle)
			{
				return handle % 4096;
			}

			// Token: 0x06001B77 RID: 7031 RVA: 0x000D377E File Offset: 0x000D197E
			public void Initialize(FormatStore.MultiValueEntry[] globaMultiValues)
			{
				this.freeListHead = -1;
				if (globaMultiValues != null && globaMultiValues.Length != 0)
				{
					this.top = globaMultiValues.Length;
					return;
				}
				this.top = 1;
			}

			// Token: 0x06001B78 RID: 7032 RVA: 0x000D37A0 File Offset: 0x000D19A0
			public int Allocate(bool isStatic)
			{
				int num = this.freeListHead;
				int num2;
				FormatStore.MultiValueEntry[] array;
				if (num != -1)
				{
					num2 = num % 4096;
					array = this.planes[num / 4096];
					this.freeListHead = array[num2].NextFree;
				}
				else
				{
					num = this.top++;
					num2 = num % 4096;
					int num3 = num / 4096;
					if (num2 == 0)
					{
						if (num3 == 256)
						{
							throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
						}
						if (num3 == this.planes.Length)
						{
							int num4 = Math.Min(this.planes.Length * 2, 256);
							FormatStore.MultiValueEntry[][] destinationArray = new FormatStore.MultiValueEntry[num4][];
							Array.Copy(this.planes, 0, destinationArray, 0, this.planes.Length);
							this.planes = destinationArray;
						}
						if (this.planes[num3] == null)
						{
							this.planes[num3] = new FormatStore.MultiValueEntry[4096];
						}
					}
					else if (num3 == 0 && num2 == this.planes[num3].Length)
					{
						int num5 = Math.Min(this.planes[0].Length * 2, 4096);
						FormatStore.MultiValueEntry[] array2 = new FormatStore.MultiValueEntry[num5];
						Array.Copy(this.planes[0], 0, array2, 0, this.planes[0].Length);
						this.planes[0] = array2;
					}
					array = this.planes[num3];
				}
				array[num2].Values = null;
				array[num2].RefCount = (isStatic ? int.MaxValue : 1);
				array[num2].NextFree = -1;
				return num;
			}

			// Token: 0x06001B79 RID: 7033 RVA: 0x000D3918 File Offset: 0x000D1B18
			public void Free(int handle)
			{
				int num = handle % 4096;
				FormatStore.MultiValueEntry[] array = this.planes[handle / 4096];
				if (array[num].Values != null)
				{
					for (int i = 0; i < array[num].Values.Length; i++)
					{
						if (array[num].Values[i].IsRefCountedHandle)
						{
							this.store.ReleaseValue(array[num].Values[i]);
						}
					}
				}
				array[num].NextFree = this.freeListHead;
				this.freeListHead = handle;
			}

			// Token: 0x06001B7A RID: 7034 RVA: 0x000D39B8 File Offset: 0x000D1BB8
			public long DumpStat(TextWriter dumpWriter)
			{
				int num = (this.top < 4096) ? 1 : ((this.top % 4096 == 0) ? (this.top / 4096) : (this.top / 4096 + 1));
				long num2 = (long)((num == 1) ? this.planes[0].Length : (num * 4096));
				long num3 = (long)(12 + this.planes.Length * 4 + 12 * num) + num2 * (long)Marshal.SizeOf(typeof(FormatStore.MultiValueEntry));
				long num4 = 0L;
				long num5 = 0L;
				long num6 = 0L;
				long num7 = 0L;
				for (int i = 0; i < this.top; i++)
				{
					int num8 = i % 4096;
					FormatStore.MultiValueEntry[] array = this.planes[i / 4096];
					if (array[num8].RefCount != 0)
					{
						num4 += 1L;
						if (array[num8].Values != null)
						{
							num3 += (long)(12 + array[num8].Values.Length * Marshal.SizeOf(typeof(PropertyValue)));
							num5 += 1L;
							num6 += (long)array[num8].Values.Length;
							if ((long)array[num8].Values.Length > num7)
							{
								num7 = (long)array[num8].Values.Length;
							}
						}
					}
				}
				long num9 = (num5 == 0L) ? 0L : ((num6 + num5 - 1L) / num5);
				if (dumpWriter != null)
				{
					dumpWriter.WriteLine("MultiValues alloc: {0}", num2);
					dumpWriter.WriteLine("MultiValues used: {0}", num4);
					dumpWriter.WriteLine("MultiValues non-null value lists: {0}", num5);
					if (num5 != 0L)
					{
						dumpWriter.WriteLine("MultiValues total value lists length: {0}", num6);
						dumpWriter.WriteLine("MultiValues average value list length: {0}", num9);
						dumpWriter.WriteLine("MultiValues max value list length: {0}", num7);
					}
					dumpWriter.WriteLine("MultiValues bytes: {0}", num3);
				}
				return num3;
			}

			// Token: 0x040020EB RID: 8427
			internal const int MaxElementsPerPlane = 4096;

			// Token: 0x040020EC RID: 8428
			internal const int MaxPlanes = 256;

			// Token: 0x040020ED RID: 8429
			internal const int InitialPlanes = 16;

			// Token: 0x040020EE RID: 8430
			internal const int InitialElements = 16;

			// Token: 0x040020EF RID: 8431
			private FormatStore store;

			// Token: 0x040020F0 RID: 8432
			private FormatStore.MultiValueEntry[][] planes;

			// Token: 0x040020F1 RID: 8433
			private int freeListHead;

			// Token: 0x040020F2 RID: 8434
			private int top;
		}

		// Token: 0x020002B2 RID: 690
		internal class TextStore
		{
			// Token: 0x06001B7B RID: 7035 RVA: 0x000D3BAE File Offset: 0x000D1DAE
			public TextStore()
			{
				this.planes = new char[16][];
				this.planes[0] = new char[1024];
			}

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000D3BD5 File Offset: 0x000D1DD5
			public uint CurrentPosition
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000D3BDD File Offset: 0x000D1DDD
			public TextRunType LastRunType
			{
				get
				{
					return this.lastRunType;
				}
			}

			// Token: 0x06001B7E RID: 7038 RVA: 0x000D3BE5 File Offset: 0x000D1DE5
			public char[] Plane(uint position)
			{
				return this.planes[(int)((UIntPtr)(position >> 15))];
			}

			// Token: 0x06001B7F RID: 7039 RVA: 0x000D3BF3 File Offset: 0x000D1DF3
			public int Index(uint position)
			{
				return (int)(position & 32767U);
			}

			// Token: 0x06001B80 RID: 7040 RVA: 0x000D3BFC File Offset: 0x000D1DFC
			public char Pick(uint position)
			{
				return this.planes[(int)((UIntPtr)(position >> 15))][(int)((UIntPtr)(position & 32767U))];
			}

			// Token: 0x06001B81 RID: 7041 RVA: 0x000D3C13 File Offset: 0x000D1E13
			public void Initialize()
			{
				this.position = 0U;
				this.lastRunType = TextRunType.Invalid;
				this.lastRunPosition = 0U;
				if (this.detector != null)
				{
					this.detector.Reset();
				}
			}

			// Token: 0x06001B82 RID: 7042 RVA: 0x000D3C3D File Offset: 0x000D1E3D
			public void InitializeCodepageDetector()
			{
				if (this.detector == null)
				{
					this.detector = new OutboundCodePageDetector();
				}
			}

			// Token: 0x06001B83 RID: 7043 RVA: 0x000D3C52 File Offset: 0x000D1E52
			public int GetBestWindowsCodePage()
			{
				return this.detector.GetBestWindowsCodePage();
			}

			// Token: 0x06001B84 RID: 7044 RVA: 0x000D3C5F File Offset: 0x000D1E5F
			public int GetBestWindowsCodePage(int preferredCodePage)
			{
				return this.detector.GetBestWindowsCodePage(preferredCodePage);
			}

			// Token: 0x06001B85 RID: 7045 RVA: 0x000D3C70 File Offset: 0x000D1E70
			public void AddText(TextRunType runType, char[] textBuffer, int offset, int count)
			{
				if (this.detector != null && runType != TextRunType.Markup)
				{
					this.detector.AddText(textBuffer, offset, count);
				}
				int num = (int)(this.position & 32767U);
				int num2 = (int)(this.position >> 15);
				if (this.lastRunType == runType && num != 0)
				{
					char[] array = this.planes[num2];
					int num3 = (int)(this.lastRunPosition & 32767U);
					int num4 = Math.Min(Math.Min(count, 4095 - FormatStore.TextStore.LengthFromRunHeader(array[num3])), 32768 - num);
					if (num4 != 0)
					{
						if (num2 == 0 && num + num4 > array.Length)
						{
							int effectiveLengthForFirstPlane = this.GetEffectiveLengthForFirstPlane(Math.Max(this.planes[0].Length * 2, num + num4));
							char[] array2 = new char[effectiveLengthForFirstPlane];
							Buffer.BlockCopy(this.planes[0], 0, array2, 0, (int)(this.position * 2U));
							array = (this.planes[0] = array2);
						}
						array[num3] = this.MakeTextRunHeader(runType, num4 + FormatStore.TextStore.LengthFromRunHeader(array[num3]));
						Buffer.BlockCopy(textBuffer, offset * 2, array, num * 2, num4 * 2);
						offset += num4;
						count -= num4;
						this.position += (uint)num4;
					}
				}
				while (count != 0)
				{
					num = (int)(this.position & 32767U);
					num2 = (int)(this.position >> 15);
					if (32768 - num < 21)
					{
						this.planes[num2][num] = this.MakeTextRunHeader(TextRunType.Invalid, 32768 - num - 1);
						this.position += (uint)(32768 - num);
					}
					else
					{
						int num5 = Math.Min(Math.Min(count, 4095), 32768 - num - 1);
						if (num2 == 0 && num + num5 + 1 > this.planes[0].Length)
						{
							int effectiveLengthForFirstPlane2 = this.GetEffectiveLengthForFirstPlane(Math.Max(this.planes[0].Length * 2, num + num5 + 1));
							char[] array3 = new char[effectiveLengthForFirstPlane2];
							Buffer.BlockCopy(this.planes[0], 0, array3, 0, (int)(this.position * 2U));
							this.planes[0] = array3;
						}
						else if (num == 0)
						{
							if (num2 == this.planes.Length)
							{
								if (num2 == 640)
								{
									throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
								}
								int num6 = Math.Min(this.planes.Length * 2, 640);
								char[][] destinationArray = new char[num6][];
								Array.Copy(this.planes, 0, destinationArray, 0, this.planes.Length);
								this.planes = destinationArray;
							}
							if (this.planes[num2] == null)
							{
								this.planes[num2] = new char[32768];
							}
						}
						this.lastRunType = runType;
						this.lastRunPosition = this.position;
						this.planes[num2][num] = this.MakeTextRunHeader(runType, num5);
						Buffer.BlockCopy(textBuffer, offset * 2, this.planes[num2], (num + 1) * 2, num5 * 2);
						offset += num5;
						count -= num5;
						this.position += (uint)(num5 + 1);
					}
				}
			}

			// Token: 0x06001B86 RID: 7046 RVA: 0x000D3F58 File Offset: 0x000D2158
			public void AddSimpleRun(TextRunType runType, int count)
			{
				if (this.lastRunType == runType)
				{
					char[] array = this.planes[(int)((UIntPtr)(this.lastRunPosition >> 15))];
					int num = (int)(this.lastRunPosition & 32767U);
					int num2 = Math.Min(count, 4095 - FormatStore.TextStore.LengthFromRunHeader(array[num]));
					if (num2 != 0)
					{
						array[num] = this.MakeTextRunHeader(runType, num2 + FormatStore.TextStore.LengthFromRunHeader(array[num]));
						count -= num2;
					}
				}
				if (count != 0)
				{
					int num3 = (int)(this.position & 32767U);
					int num4 = (int)(this.position >> 15);
					if (num3 == 0)
					{
						if (num4 == this.planes.Length)
						{
							if (num4 == 640)
							{
								throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
							}
							int num5 = Math.Min(this.planes.Length * 2, 640);
							char[][] destinationArray = new char[num5][];
							Array.Copy(this.planes, 0, destinationArray, 0, this.planes.Length);
							this.planes = destinationArray;
						}
						if (this.planes[num4] == null)
						{
							this.planes[num4] = new char[32768];
						}
					}
					else if (num4 == 0 && (ulong)(this.position + 1U) > (ulong)((long)this.planes[0].Length))
					{
						int effectiveLengthForFirstPlane = this.GetEffectiveLengthForFirstPlane(this.planes[0].Length * 2);
						char[] array2 = new char[effectiveLengthForFirstPlane];
						Buffer.BlockCopy(this.planes[0], 0, array2, 0, (int)(this.position * 2U));
						this.planes[0] = array2;
					}
					this.lastRunType = runType;
					this.lastRunPosition = this.position;
					this.planes[num4][num3] = this.MakeTextRunHeader(runType, count);
					this.position += 1U;
				}
			}

			// Token: 0x06001B87 RID: 7047 RVA: 0x000D40EC File Offset: 0x000D22EC
			public void ConvertToInvalid(uint startPosition)
			{
				char[] array = this.Plane(startPosition);
				int num = this.Index(startPosition);
				int num2 = (array[num] >= '\u3000') ? 1 : (FormatStore.TextStore.LengthFromRunHeader(array[num]) + 1);
				array[num] = this.MakeTextRunHeader(TextRunType.Invalid, num2 - 1);
			}

			// Token: 0x06001B88 RID: 7048 RVA: 0x000D4130 File Offset: 0x000D2330
			public void ConvertToInvalid(uint startPosition, int countToConvert)
			{
				char[] array = this.Plane(startPosition);
				int num = this.Index(startPosition);
				int num2 = FormatStore.TextStore.LengthFromRunHeader(array[num]);
				int num3 = num2 - countToConvert;
				int num4 = num2 + 1 - (num3 + 1);
				array[num] = this.MakeTextRunHeader(TextRunType.Invalid, num4 - 1);
				array[num + num4] = this.MakeTextRunHeader(TextRunType.NonSpace, num3);
			}

			// Token: 0x06001B89 RID: 7049 RVA: 0x000D4184 File Offset: 0x000D2384
			public void ConvertShortRun(uint startPosition, TextRunType type, int newEffectiveLength)
			{
				char[] array = this.Plane(startPosition);
				int num = this.Index(startPosition);
				array[num] = this.MakeTextRunHeader(type, newEffectiveLength);
			}

			// Token: 0x06001B8A RID: 7050 RVA: 0x000D41AC File Offset: 0x000D23AC
			public void DoNotMergeNextRun()
			{
				if (this.lastRunType != TextRunType.BlockBoundary)
				{
					this.lastRunType = TextRunType.Invalid;
				}
			}

			// Token: 0x06001B8B RID: 7051 RVA: 0x000D41C4 File Offset: 0x000D23C4
			public long DumpStat(TextWriter dumpWriter)
			{
				int num = (int)(this.position + 32768U - 1U >> 15);
				if (num == 0)
				{
					num = 1;
				}
				long num2 = (long)((num == 1) ? this.planes[0].Length : (num * 32768));
				long num3 = (long)((ulong)this.position);
				long num4 = (long)(12 + this.planes.Length * 4 + num * 12) + num2 * 2L;
				if (dumpWriter != null)
				{
					dumpWriter.WriteLine("Text alloc: {0}", num2);
					dumpWriter.WriteLine("Text used: {0}", num3);
					dumpWriter.WriteLine("Text bytes: {0}", num4);
				}
				return num4;
			}

			// Token: 0x06001B8C RID: 7052 RVA: 0x000D4259 File Offset: 0x000D2459
			internal static TextRunType TypeFromRunHeader(char runHeader)
			{
				return (TextRunType)(runHeader & '');
			}

			// Token: 0x06001B8D RID: 7053 RVA: 0x000D4263 File Offset: 0x000D2463
			internal static int LengthFromRunHeader(char runHeader)
			{
				return (int)(runHeader & '࿿');
			}

			// Token: 0x06001B8E RID: 7054 RVA: 0x000D426C File Offset: 0x000D246C
			internal char MakeTextRunHeader(TextRunType runType, int length)
			{
				return (char)((int)runType | length);
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x000D4274 File Offset: 0x000D2474
			private int GetEffectiveLengthForFirstPlane(int requestedLength)
			{
				int num = Math.Min(requestedLength, 32768);
				if (num == 32768)
				{
					return num;
				}
				if (32768 - num <= 21)
				{
					num = 32768;
				}
				return num;
			}

			// Token: 0x040020F3 RID: 8435
			internal const int LogMaxCharactersPerPlane = 15;

			// Token: 0x040020F4 RID: 8436
			internal const int MaxCharactersPerPlane = 32768;

			// Token: 0x040020F5 RID: 8437
			internal const int MaxPlanes = 640;

			// Token: 0x040020F6 RID: 8438
			internal const int InitialPlanes = 16;

			// Token: 0x040020F7 RID: 8439
			internal const int InitialCharacters = 1024;

			// Token: 0x040020F8 RID: 8440
			internal const int MaxRunEffectivelength = 4095;

			// Token: 0x040020F9 RID: 8441
			internal const int PaddingBufferLength = 21;

			// Token: 0x040020FA RID: 8442
			private OutboundCodePageDetector detector;

			// Token: 0x040020FB RID: 8443
			private char[][] planes;

			// Token: 0x040020FC RID: 8444
			private uint position;

			// Token: 0x040020FD RID: 8445
			private TextRunType lastRunType;

			// Token: 0x040020FE RID: 8446
			private uint lastRunPosition;
		}
	}
}
