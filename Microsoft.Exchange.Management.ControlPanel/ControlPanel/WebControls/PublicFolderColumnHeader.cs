using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000312 RID: 786
	public class PublicFolderColumnHeader : ColumnHeader
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x0008E558 File Offset: 0x0008C758
		public PublicFolderColumnHeader()
		{
			this.AllowHTML = true;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0008E568 File Offset: 0x0008C768
		public override string ToJavaScript()
		{
			return string.Format("new PublicFolderColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",{8})", new object[]
			{
				base.Name,
				base.SortExpression,
				base.FormatString,
				base.TextAlign.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(base.EmptyText),
				base.Text,
				this.Width.Value,
				this.Width.Type.ToJavaScript(),
				base.Features
			});
		}
	}
}
