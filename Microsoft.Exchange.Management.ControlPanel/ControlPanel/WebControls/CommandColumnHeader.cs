using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000576 RID: 1398
	public class CommandColumnHeader : ColumnHeader
	{
		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x000C647B File Offset: 0x000C467B
		// (set) Token: 0x06004114 RID: 16660 RVA: 0x000C647E File Offset: 0x000C467E
		[DefaultValue(true)]
		public override bool AllowHTML
		{
			get
			{
				return true;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x000C6485 File Offset: 0x000C4685
		// (set) Token: 0x06004116 RID: 16662 RVA: 0x000C648D File Offset: 0x000C468D
		[DefaultValue(null)]
		public string ButtonCssClass { get; set; }

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x000C6496 File Offset: 0x000C4696
		// (set) Token: 0x06004118 RID: 16664 RVA: 0x000C649E File Offset: 0x000C469E
		[DefaultValue(null)]
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Commands { get; set; }

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x000C64A7 File Offset: 0x000C46A7
		// (set) Token: 0x0600411A RID: 16666 RVA: 0x000C64AF File Offset: 0x000C46AF
		[DefaultValue(false)]
		public bool UseCheckBox { get; set; }

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000C64B8 File Offset: 0x000C46B8
		// (set) Token: 0x0600411C RID: 16668 RVA: 0x000C64C0 File Offset: 0x000C46C0
		[DefaultValue(false)]
		public bool UseCommandText { get; set; }

		// Token: 0x0600411D RID: 16669 RVA: 0x000C64CC File Offset: 0x000C46CC
		public override string ToJavaScript()
		{
			if (this.Commands.IsNullOrEmpty())
			{
				throw new Exception("At least one command must be set to Commands property of CommandColumnHeader.");
			}
			if (this.UseCheckBox && this.Commands.Length != 2)
			{
				throw new Exception("Two commands must be set to Commands property of CommandColumnHeader when UseCheckBox is true.");
			}
			if (this.UseCheckBox && string.IsNullOrEmpty(base.Name))
			{
				throw new Exception("The column must be bound to a Boolean property (set Name property) if UseCheckBox is true.");
			}
			if (!this.UseCheckBox && !this.UseCommandText && !string.IsNullOrEmpty(base.Name) && this.Commands.Length != 1)
			{
				throw new Exception("Only one command can be set to Commands property of CommandColumnHeader if the column is data bound and UseCheckBox is false.");
			}
			return string.Format("new CommandColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6},{7},{8},\"{9}\",{10},\"{11}\",{12})", new object[]
			{
				base.Name,
				base.SortExpression,
				base.FormatString,
				base.TextAlign.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(base.EmptyText),
				this.ButtonCssClass,
				this.Commands.ToJsonString(null),
				this.UseCommandText.ToJavaScript(),
				this.UseCheckBox.ToJavaScript(),
				base.Text,
				this.Width.Value,
				this.Width.Type.ToJavaScript(),
				base.Features
			});
		}
	}
}
