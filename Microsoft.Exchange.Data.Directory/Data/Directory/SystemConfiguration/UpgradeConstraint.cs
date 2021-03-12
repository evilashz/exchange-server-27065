using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005EB RID: 1515
	[Serializable]
	public class UpgradeConstraint : XMLSerializableBase, IComparable<UpgradeConstraint>
	{
		// Token: 0x060047EA RID: 18410 RVA: 0x001093D2 File Offset: 0x001075D2
		public UpgradeConstraint() : this(null, null, DateTime.MinValue)
		{
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x001093E1 File Offset: 0x001075E1
		public UpgradeConstraint(string name, string reason) : this(name, reason, DateTime.MinValue)
		{
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x001093F0 File Offset: 0x001075F0
		public UpgradeConstraint(string name, string reason, DateTime expirationDate)
		{
			this.Name = name;
			this.Reason = reason;
			this.ExpirationDate = expirationDate;
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x0010940D File Offset: 0x0010760D
		// (set) Token: 0x060047EE RID: 18414 RVA: 0x00109415 File Offset: 0x00107615
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x0010941E File Offset: 0x0010761E
		// (set) Token: 0x060047F0 RID: 18416 RVA: 0x00109426 File Offset: 0x00107626
		[XmlAttribute(AttributeName = "Reason")]
		public string Reason { get; set; }

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x0010942F File Offset: 0x0010762F
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x00109437 File Offset: 0x00107637
		[XmlAttribute(AttributeName = "ExpirationDate")]
		public DateTime ExpirationDate { get; set; }

		// Token: 0x060047F3 RID: 18419 RVA: 0x00109440 File Offset: 0x00107640
		int IComparable<UpgradeConstraint>.CompareTo(UpgradeConstraint other)
		{
			if (other == null)
			{
				return 1;
			}
			int num = StringComparer.OrdinalIgnoreCase.Compare(this.Name, other.Name);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.OrdinalIgnoreCase.Compare(this.Reason, other.Reason);
			if (num != 0)
			{
				return num;
			}
			return DateTime.Compare(this.ExpirationDate, other.ExpirationDate);
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0010949B File Offset: 0x0010769B
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.Name, this.Reason, this.ExpirationDate);
		}
	}
}
