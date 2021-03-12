using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x0200091E RID: 2334
	public class NspiState
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x0007B1F2 File Offset: 0x000793F2
		public NspiState()
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x0007B1FC File Offset: 0x000793FC
		public NspiState(int sortType, int containerId, int currentRecord, int delta, int position, int totalRecords, int codePage, int templateLocale, int sortLocale)
		{
			this.sortType = sortType;
			this.containerId = containerId;
			this.currentRecord = currentRecord;
			this.delta = delta;
			this.position = position;
			this.totalRecords = totalRecords;
			this.codePage = codePage;
			this.templateLocale = templateLocale;
			this.sortLocale = sortLocale;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0007B254 File Offset: 0x00079454
		public NspiState(IntPtr src)
		{
			this.MarshalFromNative(src);
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x0007B263 File Offset: 0x00079463
		// (set) Token: 0x060031F3 RID: 12787 RVA: 0x0007B26B File Offset: 0x0007946B
		public SortIndex SortIndex
		{
			get
			{
				return (SortIndex)this.sortType;
			}
			set
			{
				this.sortType = (int)value;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x0007B274 File Offset: 0x00079474
		// (set) Token: 0x060031F5 RID: 12789 RVA: 0x0007B27C File Offset: 0x0007947C
		public int SortType
		{
			get
			{
				return this.sortType;
			}
			set
			{
				this.sortType = value;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x0007B285 File Offset: 0x00079485
		// (set) Token: 0x060031F7 RID: 12791 RVA: 0x0007B28D File Offset: 0x0007948D
		public int ContainerId
		{
			get
			{
				return this.containerId;
			}
			set
			{
				this.containerId = value;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x0007B296 File Offset: 0x00079496
		// (set) Token: 0x060031F9 RID: 12793 RVA: 0x0007B29E File Offset: 0x0007949E
		public int CurrentRecord
		{
			get
			{
				return this.currentRecord;
			}
			set
			{
				this.currentRecord = value;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x0007B2A7 File Offset: 0x000794A7
		// (set) Token: 0x060031FB RID: 12795 RVA: 0x0007B2AF File Offset: 0x000794AF
		public int Delta
		{
			get
			{
				return this.delta;
			}
			set
			{
				this.delta = value;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x0007B2B8 File Offset: 0x000794B8
		// (set) Token: 0x060031FD RID: 12797 RVA: 0x0007B2C0 File Offset: 0x000794C0
		public int Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x0007B2C9 File Offset: 0x000794C9
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x0007B2D1 File Offset: 0x000794D1
		public int TotalRecords
		{
			get
			{
				return this.totalRecords;
			}
			set
			{
				this.totalRecords = value;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x0007B2DA File Offset: 0x000794DA
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x0007B2E2 File Offset: 0x000794E2
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
			set
			{
				this.codePage = value;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x0007B2EB File Offset: 0x000794EB
		// (set) Token: 0x06003203 RID: 12803 RVA: 0x0007B2F3 File Offset: 0x000794F3
		public int TemplateLocale
		{
			get
			{
				return this.templateLocale;
			}
			set
			{
				this.templateLocale = value;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x0007B2FC File Offset: 0x000794FC
		// (set) Token: 0x06003205 RID: 12805 RVA: 0x0007B304 File Offset: 0x00079504
		public int SortLocale
		{
			get
			{
				return this.sortLocale;
			}
			set
			{
				this.sortLocale = value;
			}
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x0007B30D File Offset: 0x0007950D
		internal NspiState Clone()
		{
			return (NspiState)base.MemberwiseClone();
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x0007B31A File Offset: 0x0007951A
		internal int GetBytesToMarshal()
		{
			return 36;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x0007B320 File Offset: 0x00079520
		internal void MarshalToNative(IntPtr dst)
		{
			Marshal.WriteInt32(dst, 0, this.sortType);
			Marshal.WriteInt32(dst, 4, this.containerId);
			Marshal.WriteInt32(dst, 8, this.currentRecord);
			Marshal.WriteInt32(dst, 12, this.delta);
			Marshal.WriteInt32(dst, 16, this.position);
			Marshal.WriteInt32(dst, 20, this.totalRecords);
			Marshal.WriteInt32(dst, 24, this.codePage);
			Marshal.WriteInt32(dst, 28, this.templateLocale);
			Marshal.WriteInt32(dst, 32, this.sortLocale);
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x0007B3A8 File Offset: 0x000795A8
		internal void MarshalFromNative(IntPtr src)
		{
			this.sortType = Marshal.ReadInt32(src, 0);
			this.containerId = Marshal.ReadInt32(src, 4);
			this.currentRecord = Marshal.ReadInt32(src, 8);
			this.delta = Marshal.ReadInt32(src, 12);
			this.position = Marshal.ReadInt32(src, 16);
			this.totalRecords = Marshal.ReadInt32(src, 20);
			this.codePage = Marshal.ReadInt32(src, 24);
			this.templateLocale = Marshal.ReadInt32(src, 28);
			this.sortLocale = Marshal.ReadInt32(src, 32);
		}

		// Token: 0x04002B77 RID: 11127
		internal const int SizeOf = 36;

		// Token: 0x04002B78 RID: 11128
		private int sortType;

		// Token: 0x04002B79 RID: 11129
		private int containerId;

		// Token: 0x04002B7A RID: 11130
		private int currentRecord;

		// Token: 0x04002B7B RID: 11131
		private int delta;

		// Token: 0x04002B7C RID: 11132
		private int position;

		// Token: 0x04002B7D RID: 11133
		private int totalRecords;

		// Token: 0x04002B7E RID: 11134
		private int codePage;

		// Token: 0x04002B7F RID: 11135
		private int templateLocale;

		// Token: 0x04002B80 RID: 11136
		private int sortLocale;
	}
}
