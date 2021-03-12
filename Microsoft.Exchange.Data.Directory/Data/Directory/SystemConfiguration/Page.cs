using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public sealed class Page : INotifyPropertyChanged
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600231F RID: 8991 RVA: 0x00098D70 File Offset: 0x00096F70
		// (remove) Token: 0x06002320 RID: 8992 RVA: 0x00098DA8 File Offset: 0x00096FA8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06002321 RID: 8993 RVA: 0x00098DDD File Offset: 0x00096FDD
		private void NotifyPropertyChanged(string info)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00098DF9 File Offset: 0x00096FF9
		// (set) Token: 0x06002323 RID: 8995 RVA: 0x00098E01 File Offset: 0x00097001
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != this.text)
				{
					DetailsTemplateControl.ValidateText(value, DetailsTemplateControl.TextLengths.Page);
					this.text = value;
					this.NotifyPropertyChanged("Text");
				}
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00098E2B File Offset: 0x0009702B
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x00098E33 File Offset: 0x00097033
		public int HelpContext
		{
			get
			{
				return this.helpContext;
			}
			set
			{
				DetailsTemplateControl.ValidateRange(value, 0, Page.maxHelpContextLength);
				this.helpContext = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x00098E48 File Offset: 0x00097048
		public ICollection<DetailsTemplateControl> Controls
		{
			get
			{
				return this.controls;
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00098E6E File Offset: 0x0009706E
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x040015C7 RID: 5575
		private static int maxHelpContextLength = 99999;

		// Token: 0x040015C8 RID: 5576
		private int helpContext;

		// Token: 0x040015C9 RID: 5577
		private string text = string.Empty;

		// Token: 0x040015CA RID: 5578
		private Collection<DetailsTemplateControl> controls = new Collection<DetailsTemplateControl>();
	}
}
