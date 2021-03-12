using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public sealed class FolderSizeRec : XMLSerializableBase
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x00013E40 File Offset: 0x00012040
		public FolderSizeRec()
		{
			this.Source = new FolderSizeRec.CountAndSize();
			this.SourceFAI = new FolderSizeRec.CountAndSize();
			this.Target = new FolderSizeRec.CountAndSize();
			this.TargetFAI = new FolderSizeRec.CountAndSize();
			this.Corrupt = new FolderSizeRec.CountAndSize();
			this.Large = new FolderSizeRec.CountAndSize();
			this.Skipped = new FolderSizeRec.CountAndSize();
			this.Missing = new FolderSizeRec.CountAndSize();
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00013EAB File Offset: 0x000120AB
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00013EB3 File Offset: 0x000120B3
		[XmlElement(ElementName = "FolderPath")]
		public string FolderPath { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00013EBC File Offset: 0x000120BC
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00013EC4 File Offset: 0x000120C4
		[XmlElement(ElementName = "FolderID")]
		public byte[] FolderID { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00013ECD File Offset: 0x000120CD
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00013ED5 File Offset: 0x000120D5
		[XmlElement(ElementName = "ParentID")]
		public byte[] ParentID { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00013EDE File Offset: 0x000120DE
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00013EE6 File Offset: 0x000120E6
		[XmlIgnore]
		public WellKnownFolderType WKFType { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00013EEF File Offset: 0x000120EF
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00013EF7 File Offset: 0x000120F7
		[XmlElement(ElementName = "WKFType")]
		public int WKFTypeInt
		{
			get
			{
				return (int)this.WKFType;
			}
			set
			{
				this.WKFType = (WellKnownFolderType)value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00013F00 File Offset: 0x00012100
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00013F08 File Offset: 0x00012108
		[XmlElement(ElementName = "Source")]
		public FolderSizeRec.CountAndSize Source { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00013F11 File Offset: 0x00012111
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00013F19 File Offset: 0x00012119
		[XmlElement(ElementName = "SourceFAI")]
		public FolderSizeRec.CountAndSize SourceFAI { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00013F22 File Offset: 0x00012122
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00013F2A File Offset: 0x0001212A
		[XmlElement(ElementName = "Target")]
		public FolderSizeRec.CountAndSize Target { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00013F33 File Offset: 0x00012133
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00013F3B File Offset: 0x0001213B
		[XmlElement(ElementName = "TargetFAI")]
		public FolderSizeRec.CountAndSize TargetFAI { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00013F44 File Offset: 0x00012144
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00013F4C File Offset: 0x0001214C
		[XmlElement(ElementName = "Corrupt")]
		public FolderSizeRec.CountAndSize Corrupt { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00013F55 File Offset: 0x00012155
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00013F5D File Offset: 0x0001215D
		[XmlElement(ElementName = "Large")]
		public FolderSizeRec.CountAndSize Large { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00013F66 File Offset: 0x00012166
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00013F6E File Offset: 0x0001216E
		[XmlElement(ElementName = "Skipped")]
		public FolderSizeRec.CountAndSize Skipped { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00013F77 File Offset: 0x00012177
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00013F7F File Offset: 0x0001217F
		[XmlElement(ElementName = "Missing")]
		public FolderSizeRec.CountAndSize Missing { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00013F88 File Offset: 0x00012188
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00013F90 File Offset: 0x00012190
		[XmlElement(ElementName = "MissingItems")]
		public List<BadMessageRec> MissingItems { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00013F99 File Offset: 0x00012199
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00013FA1 File Offset: 0x000121A1
		[XmlElement(ElementName = "MailboxGuid")]
		public Guid MailboxGuid { get; set; }

		// Token: 0x060009D6 RID: 2518 RVA: 0x00013FAC File Offset: 0x000121AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}: Source: {1}, Target {2}", this.FolderPath, this.Source.ToString(), this.Target.ToString());
			if (!this.SourceFAI.IsEmpty)
			{
				stringBuilder.AppendFormat(", SourceFAI: {0}, TargetFAI: {1}", this.SourceFAI.ToString(), this.TargetFAI.ToString());
			}
			if (!this.Corrupt.IsEmpty)
			{
				stringBuilder.AppendFormat(", Corrupt: {0}", this.Corrupt.ToString());
			}
			if (!this.Large.IsEmpty)
			{
				stringBuilder.AppendFormat(", Large: {0}", this.Large.ToString());
			}
			if (!this.Skipped.IsEmpty)
			{
				stringBuilder.AppendFormat(", Skipped: {0}", this.Skipped.ToString());
			}
			if (!this.Missing.IsEmpty)
			{
				stringBuilder.AppendFormat(", Missing: {0}", this.Missing.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0200011B RID: 283
		[Serializable]
		public sealed class CountAndSize : XMLSerializableBase
		{
			// Token: 0x1700031B RID: 795
			// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000140AC File Offset: 0x000122AC
			// (set) Token: 0x060009D8 RID: 2520 RVA: 0x000140B4 File Offset: 0x000122B4
			[XmlAttribute(AttributeName = "C")]
			public int Count { get; set; }

			// Token: 0x1700031C RID: 796
			// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000140BD File Offset: 0x000122BD
			// (set) Token: 0x060009DA RID: 2522 RVA: 0x000140C5 File Offset: 0x000122C5
			[XmlAttribute(AttributeName = "S")]
			public ulong Size { get; set; }

			// Token: 0x1700031D RID: 797
			// (get) Token: 0x060009DB RID: 2523 RVA: 0x000140CE File Offset: 0x000122CE
			internal bool IsEmpty
			{
				get
				{
					return this.Count == 0;
				}
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x000140DC File Offset: 0x000122DC
			public override string ToString()
			{
				return string.Format("{0} [{1}]", this.Count, new ByteQuantifiedSize(this.Size).ToString());
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x00014117 File Offset: 0x00012317
			internal void Add(MessageRec msgRec)
			{
				this.Count++;
				this.Size += (ulong)((long)msgRec.MessageSize);
			}
		}
	}
}
