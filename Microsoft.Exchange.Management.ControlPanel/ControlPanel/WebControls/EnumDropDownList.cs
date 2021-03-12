using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005DB RID: 1499
	public class EnumDropDownList : DropDownList
	{
		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x000CC26A File Offset: 0x000CA46A
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x000CC272 File Offset: 0x000CA472
		public string EnumType { get; set; }

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x000CC27B File Offset: 0x000CA47B
		// (set) Token: 0x0600436C RID: 17260 RVA: 0x000CC283 File Offset: 0x000CA483
		public bool LocalizedText
		{
			get
			{
				return this.localizedText;
			}
			set
			{
				this.localizedText = value;
			}
		}

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x000CC28C File Offset: 0x000CA48C
		// (set) Token: 0x0600436E RID: 17262 RVA: 0x000CC294 File Offset: 0x000CA494
		public string AvailabeValues { get; set; }

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x000CC29D File Offset: 0x000CA49D
		// (set) Token: 0x06004370 RID: 17264 RVA: 0x000CC2A5 File Offset: 0x000CA4A5
		public string DefaultValue { get; set; }

		// Token: 0x06004371 RID: 17265 RVA: 0x000CC2C8 File Offset: 0x000CA4C8
		protected override void OnLoad(EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.EnumType))
			{
				Type type = Type.GetType(this.EnumType);
				Enum.GetUnderlyingType(type);
				foreach (object obj in Enum.GetValues(type))
				{
					string value = obj.ToString();
					if (string.IsNullOrEmpty(this.AvailabeValues) || this.AvailabeValues.Split(new char[]
					{
						','
					}).Any((string each) => each.Equals(value, StringComparison.InvariantCultureIgnoreCase)))
					{
						string value2 = this.localizedText ? LocalizedDescriptionAttribute.FromEnum(type, obj) : obj.ToString();
						ListItem item = new ListItem(RtlUtil.ConvertToDecodedBidiString(value2, RtlUtil.IsRtl), value);
						this.Items.Add(item);
						if (this.DefaultValue != null && value == this.DefaultValue)
						{
							this.SelectedValue = value;
						}
					}
				}
			}
			base.OnLoad(e);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000CC404 File Offset: 0x000CA604
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("role", "combobox");
			base.AddAttributesToRender(writer);
		}

		// Token: 0x04002D9D RID: 11677
		private bool localizedText = true;
	}
}
