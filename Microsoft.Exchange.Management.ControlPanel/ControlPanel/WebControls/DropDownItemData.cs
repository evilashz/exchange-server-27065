using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200024D RID: 589
	[DataContract]
	public class DropDownItemData : BaseRow
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x0007FCCE File Offset: 0x0007DECE
		public DropDownItemData(string text, string value)
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0007FCE4 File Offset: 0x0007DEE4
		protected DropDownItemData() : base(null, null)
		{
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0007FCEE File Offset: 0x0007DEEE
		protected DropDownItemData(ADObject adObject) : base(adObject)
		{
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0007FCF7 File Offset: 0x0007DEF7
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x0007FCFF File Offset: 0x0007DEFF
		[DataMember]
		public string Text { get; set; }

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x0007FD08 File Offset: 0x0007DF08
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x0007FD10 File Offset: 0x0007DF10
		[DataMember]
		public string Value { get; set; }

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x0007FD19 File Offset: 0x0007DF19
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x0007FD21 File Offset: 0x0007DF21
		[DataMember]
		public bool Selected { get; set; }
	}
}
