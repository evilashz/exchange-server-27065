using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E2 RID: 226
	[XmlType(TypeName = "CompressedReport")]
	[Serializable]
	public sealed class CompressedReport : XMLSerializableBase
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x00010947 File Offset: 0x0000EB47
		internal CompressedReport()
		{
			this.Entries = new List<ReportEntry>();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001095A File Offset: 0x0000EB5A
		internal CompressedReport(List<ReportEntry> entries)
		{
			this.Entries = (entries ?? new List<ReportEntry>());
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00010972 File Offset: 0x0000EB72
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0001097A File Offset: 0x0000EB7A
		[XmlIgnore]
		public List<ReportEntry> Entries { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00010983 File Offset: 0x0000EB83
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00010997 File Offset: 0x0000EB97
		[XmlElement(ElementName = "CompressedEntries")]
		public byte[] CompressedEntries
		{
			get
			{
				return CommonUtils.PackString(XMLSerializableBase.Serialize(this.Entries, false), true);
			}
			set
			{
				this.Entries = (XMLSerializableBase.Deserialize<List<ReportEntry>>(CommonUtils.UnpackString(value, true), false) ?? new List<ReportEntry>());
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000109B8 File Offset: 0x0000EBB8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ReportEntry reportEntry in this.Entries)
			{
				stringBuilder.AppendLine(reportEntry.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
