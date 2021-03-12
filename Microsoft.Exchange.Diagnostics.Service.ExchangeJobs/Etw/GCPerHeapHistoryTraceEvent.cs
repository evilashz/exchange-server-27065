using System;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw
{
	// Token: 0x02000022 RID: 34
	public sealed class GCPerHeapHistoryTraceEvent : TraceEvent
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00005E1C File Offset: 0x0000401C
		internal unsafe GCPerHeapHistoryTraceEvent(Guid providerGuid, string providerName, EtwTraceNativeComponents.EVENT_RECORD* rawData) : base(providerGuid, providerName, rawData)
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005E27 File Offset: 0x00004027
		public int MemoryPressure
		{
			get
			{
				if (this.V4_0)
				{
					return base.GetInt32At(this.SizeOfGenData * 5);
				}
				return base.GetInt32At(this.SizeOfGenData * 5 + 8);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005E50 File Offset: 0x00004050
		public int GenCondemnedReasons
		{
			get
			{
				if (this.V4_0)
				{
					return base.GetInt32At(this.SizeOfGenData * 5 + 16);
				}
				return base.GetInt32At(this.SizeOfGenData * 5);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005E7A File Offset: 0x0000407A
		public int GenCondemnedReasonsEx
		{
			get
			{
				return base.GetInt32At(this.SizeOfGenData * 5 + 4);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00005E8C File Offset: 0x0000408C
		public int HeapIndex
		{
			get
			{
				return base.GetInt32At(base.EventDataLength - 5);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005E9C File Offset: 0x0000409C
		public int ClrInstanceID
		{
			get
			{
				return base.GetByteAt(base.EventDataLength - 1);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00005EAC File Offset: 0x000040AC
		public int EntriesInGenData
		{
			get
			{
				if (this.V4_5_Beta)
				{
					return 8;
				}
				return 10;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005EBA File Offset: 0x000040BA
		public int SizeOfGenData
		{
			get
			{
				return base.HostSizePtr(this.EntriesInGenData);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005EC8 File Offset: 0x000040C8
		public int TotalSizeOfGenData
		{
			get
			{
				return this.SizeOfGenData * 5;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005ED4 File Offset: 0x000040D4
		public bool V4_0
		{
			get
			{
				if (base.Version == 0)
				{
					int num = base.HostSizePtr(10) * 5 + 25;
					return base.EventDataLength == num || base.EventDataLength == num - 4;
				}
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005F10 File Offset: 0x00004110
		public bool V4_5_Beta
		{
			get
			{
				if (base.Version == 0)
				{
					int num = base.HostSizePtr(8) * 5 + 25;
					return base.EventDataLength == num;
				}
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00005F3D File Offset: 0x0000413D
		public bool V4_5
		{
			get
			{
				return base.Version == 2;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005F48 File Offset: 0x00004148
		public bool VersionRecognized
		{
			get
			{
				return this.V4_0 || this.V4_5_Beta || this.V4_5;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005F62 File Offset: 0x00004162
		public GCPerHeapHistoryTraceEvent.GCPerHeapHistoryGenData GenData(Gens genNumber)
		{
			return new GCPerHeapHistoryTraceEvent.GCPerHeapHistoryGenData(this, this.SizeOfGenData * (int)genNumber);
		}

		// Token: 0x04000104 RID: 260
		private const int MaxxGenData = 5;

		// Token: 0x02000023 RID: 35
		public sealed class GCPerHeapHistoryGenData
		{
			// Token: 0x060000A0 RID: 160 RVA: 0x00005F72 File Offset: 0x00004172
			internal GCPerHeapHistoryGenData(GCPerHeapHistoryTraceEvent container, int offset)
			{
				this.container = container;
				this.startOffset = offset;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005F88 File Offset: 0x00004188
			public long SizeBefore
			{
				get
				{
					return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(0));
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005FA8 File Offset: 0x000041A8
			public long SizeAfter
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(3));
					}
					return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(1));
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005FFF File Offset: 0x000041FF
			public long ObjSpaceBefore
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.SizeBefore - this.FreeListSpaceBefore - this.FreeObjSpaceBefore;
					}
					return -1L;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00006025 File Offset: 0x00004225
			public long Fragmentation
			{
				get
				{
					if (this.container.V4_0)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(4));
					}
					return this.FreeListSpaceAfter + this.FreeObjSpaceAfter;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006060 File Offset: 0x00004260
			public long ObjSizeAfter
			{
				get
				{
					return this.SizeAfter - this.Fragmentation;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000606F File Offset: 0x0000426F
			public long FreeListSpaceBefore
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(1));
					}
					return -1L;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000609F File Offset: 0x0000429F
			public long FreeObjSpaceBefore
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(2));
					}
					return -1L;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x000060D0 File Offset: 0x000042D0
			public long FreeListSpaceAfter
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(4));
					}
					if (this.container.V4_5_Beta)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(2));
					}
					return -1L;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x00006138 File Offset: 0x00004338
			public long FreeObjSpaceAfter
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(5));
					}
					if (this.container.V4_5_Beta)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(3));
					}
					return -1L;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000AA RID: 170 RVA: 0x000061A0 File Offset: 0x000043A0
			public long In
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(6));
					}
					if (this.container.V4_5_Beta)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(4));
					}
					return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(5));
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00006224 File Offset: 0x00004424
			public long Out
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(7));
					}
					if (this.container.V4_5_Beta)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(5));
					}
					return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(6));
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000AC RID: 172 RVA: 0x000062A8 File Offset: 0x000044A8
			public long Budget
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(8));
					}
					if (this.container.V4_5_Beta)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(6));
					}
					return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(7));
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000AD RID: 173 RVA: 0x0000632C File Offset: 0x0000452C
			public long SurvRate
			{
				get
				{
					if (this.container.V4_5)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(9));
					}
					if (this.container.V4_5_Beta)
					{
						return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(7));
					}
					return this.container.GetIntPtrAt(this.startOffset + this.container.HostSizePtr(8));
				}
			}

			// Token: 0x04000105 RID: 261
			private readonly int startOffset;

			// Token: 0x04000106 RID: 262
			private GCPerHeapHistoryTraceEvent container;
		}
	}
}
