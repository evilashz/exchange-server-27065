using System;
using System.ComponentModel;
using System.Drawing;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000602 RID: 1538
	public class PopupCommand : NavigateCommand
	{
		// Token: 0x060044DC RID: 17628 RVA: 0x000CFDEC File Offset: 0x000CDFEC
		static PopupCommand()
		{
			PopupCommand.DefaultPopupSize = new Size(510, 564);
			PopupCommand.DefaultBookmarkedPopupSize = new Size(PopupCommand.DefaultBookmarkedPopupWidth, PopupCommand.DefaultBookmarkedPopupHeight);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x000CFE5D File Offset: 0x000CE05D
		public PopupCommand() : this(null, CommandSprite.SpriteId.NONE)
		{
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x000CFE67 File Offset: 0x000CE067
		public PopupCommand(string text, CommandSprite.SpriteId imageID) : base(text, imageID)
		{
			this.TargetFrame = "_blank";
			this.HasBookmark = true;
		}

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000CFE9C File Offset: 0x000CE09C
		// (set) Token: 0x060044E0 RID: 17632 RVA: 0x000CFECA File Offset: 0x000CE0CA
		[TypeConverter(typeof(SizeConverter))]
		public virtual Size DialogSize
		{
			get
			{
				if (this.dialogSize != null)
				{
					return this.dialogSize.Value;
				}
				if (!this.HasBookmark)
				{
					return PopupCommand.DefaultPopupSize;
				}
				return PopupCommand.DefaultBookmarkedPopupSize;
			}
			set
			{
				this.dialogSize = new Size?(value);
			}
		}

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000CFED8 File Offset: 0x000CE0D8
		// (set) Token: 0x060044E2 RID: 17634 RVA: 0x000CFEE0 File Offset: 0x000CE0E0
		[DefaultValue(typeof(Point), "-1, -1")]
		[TypeConverter(typeof(PointConverter))]
		public virtual Point Position
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

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000CFEE9 File Offset: 0x000CE0E9
		// (set) Token: 0x060044E4 RID: 17636 RVA: 0x000CFEF1 File Offset: 0x000CE0F1
		[DefaultValue(true)]
		public virtual bool Resizable { get; set; }

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x000CFEFA File Offset: 0x000CE0FA
		// (set) Token: 0x060044E6 RID: 17638 RVA: 0x000CFF02 File Offset: 0x000CE102
		[DefaultValue(false)]
		public virtual bool UseDefaultWindow { get; set; }

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x000CFF0B File Offset: 0x000CE10B
		// (set) Token: 0x060044E8 RID: 17640 RVA: 0x000CFF13 File Offset: 0x000CE113
		[DefaultValue(false)]
		public virtual bool ShowAddressBar { get; set; }

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x000CFF1C File Offset: 0x000CE11C
		// (set) Token: 0x060044EA RID: 17642 RVA: 0x000CFF24 File Offset: 0x000CE124
		[DefaultValue(false)]
		public virtual bool ShowMenuBar { get; set; }

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x000CFF2D File Offset: 0x000CE12D
		// (set) Token: 0x060044EC RID: 17644 RVA: 0x000CFF35 File Offset: 0x000CE135
		[DefaultValue(true)]
		public virtual bool ShowStatusBar { get; set; }

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x000CFF3E File Offset: 0x000CE13E
		// (set) Token: 0x060044EE RID: 17646 RVA: 0x000CFF46 File Offset: 0x000CE146
		[DefaultValue(false)]
		public virtual bool ShowToolBar { get; set; }

		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x000CFF4F File Offset: 0x000CE14F
		// (set) Token: 0x060044F0 RID: 17648 RVA: 0x000CFF57 File Offset: 0x000CE157
		[DefaultValue(false)]
		public virtual bool SingleInstance { get; set; }

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x000CFF60 File Offset: 0x000CE160
		// (set) Token: 0x060044F2 RID: 17650 RVA: 0x000CFF68 File Offset: 0x000CE168
		public bool HasBookmark
		{
			get
			{
				return this.hasBookmark;
			}
			set
			{
				this.hasBookmark = value;
			}
		}

		// Token: 0x04002E22 RID: 11810
		internal const int DefaultPopupWidth = 510;

		// Token: 0x04002E23 RID: 11811
		internal const int DefaultPopupHeight = 564;

		// Token: 0x04002E24 RID: 11812
		internal static readonly int DefaultBookmarkedPopupWidth = 710;

		// Token: 0x04002E25 RID: 11813
		internal static readonly int DefaultBookmarkedPopupHeight = 564;

		// Token: 0x04002E26 RID: 11814
		internal static readonly Size DefaultPopupSize = new Size(510, 564);

		// Token: 0x04002E27 RID: 11815
		internal static readonly Size DefaultBookmarkedPopupSize = new Size(PopupCommand.DefaultBookmarkedPopupWidth, PopupCommand.DefaultBookmarkedPopupHeight);

		// Token: 0x04002E28 RID: 11816
		private Size? dialogSize = null;

		// Token: 0x04002E29 RID: 11817
		private bool hasBookmark;

		// Token: 0x04002E2A RID: 11818
		private Point position = new Point(-1, -1);
	}
}
