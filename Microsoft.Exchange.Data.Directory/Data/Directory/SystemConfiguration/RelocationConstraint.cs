using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000560 RID: 1376
	[Serializable]
	public class RelocationConstraint : XMLSerializableBase, IComparable<RelocationConstraint>
	{
		// Token: 0x06003DDC RID: 15836 RVA: 0x000EB83C File Offset: 0x000E9A3C
		public RelocationConstraint() : this(null, DateTime.MinValue)
		{
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000EB84A File Offset: 0x000E9A4A
		public RelocationConstraint(RelocationConstraintType constraintType, DateTime expirationDate) : this(constraintType.ToString(), expirationDate)
		{
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000EB85E File Offset: 0x000E9A5E
		public RelocationConstraint(string name, DateTime expirationDate)
		{
			this.Name = name;
			this.ExpirationDate = expirationDate;
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000EB874 File Offset: 0x000E9A74
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x000EB87C File Offset: 0x000E9A7C
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x000EB885 File Offset: 0x000E9A85
		// (set) Token: 0x06003DE2 RID: 15842 RVA: 0x000EB88D File Offset: 0x000E9A8D
		[XmlAttribute(AttributeName = "ExpirationDate")]
		public DateTime ExpirationDate { get; set; }

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000EB898 File Offset: 0x000E9A98
		int IComparable<RelocationConstraint>.CompareTo(RelocationConstraint other)
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
			return DateTime.Compare(this.ExpirationDate, other.ExpirationDate);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000EB8D7 File Offset: 0x000E9AD7
		public override string ToString()
		{
			return string.Format("{0}: Expires {1}", this.Name, this.ExpirationDate);
		}
	}
}
