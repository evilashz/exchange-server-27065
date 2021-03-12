using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000518 RID: 1304
	[XmlType(TypeName = "AddressListSequence")]
	public class AddressListSequence : XMLSerializableBase
	{
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x000DEA2F File Offset: 0x000DCC2F
		// (set) Token: 0x060039AC RID: 14764 RVA: 0x000DEA37 File Offset: 0x000DCC37
		[XmlAttribute(AttributeName = "AddressListId")]
		public string AddressListId
		{
			get
			{
				return this.addressListId;
			}
			set
			{
				this.addressListId = value;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000DEA40 File Offset: 0x000DCC40
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000DEA48 File Offset: 0x000DCC48
		[XmlAttribute(AttributeName = "Sequence")]
		public uint Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000DEA51 File Offset: 0x000DCC51
		public override int GetHashCode()
		{
			return (string.IsNullOrEmpty(this.addressListId) ? 0 : this.addressListId.GetHashCode()) ^ (int)this.sequence;
		}

		// Token: 0x0400276B RID: 10091
		private string addressListId;

		// Token: 0x0400276C RID: 10092
		private uint sequence;
	}
}
