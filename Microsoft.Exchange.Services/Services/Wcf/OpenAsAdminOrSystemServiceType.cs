using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC2 RID: 3522
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot(ElementName = "OpenAsAdminOrSystemService", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class OpenAsAdminOrSystemServiceType
	{
		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06005997 RID: 22935 RVA: 0x0011824C File Offset: 0x0011644C
		// (set) Token: 0x06005998 RID: 22936 RVA: 0x00118254 File Offset: 0x00116454
		[XmlElement]
		public ConnectingSIDType ConnectingSID
		{
			get
			{
				return this.connectingSIDField;
			}
			set
			{
				this.connectingSIDField = value;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x0011825D File Offset: 0x0011645D
		// (set) Token: 0x0600599A RID: 22938 RVA: 0x00118265 File Offset: 0x00116465
		[XmlAttribute]
		public SpecialLogonType LogonType
		{
			get
			{
				return this.logonTypeField;
			}
			set
			{
				this.logonTypeField = value;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x0600599B RID: 22939 RVA: 0x0011826E File Offset: 0x0011646E
		// (set) Token: 0x0600599C RID: 22940 RVA: 0x00118276 File Offset: 0x00116476
		[XmlAttribute]
		public int BudgetType
		{
			get
			{
				return this.budgetTypeField;
			}
			set
			{
				this.budgetTypeField = value;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x0011827F File Offset: 0x0011647F
		// (set) Token: 0x0600599E RID: 22942 RVA: 0x00118287 File Offset: 0x00116487
		[XmlIgnore]
		public bool BudgetTypeSpecified
		{
			get
			{
				return this.budgetTypeSpecifiedField;
			}
			set
			{
				this.budgetTypeSpecifiedField = value;
			}
		}

		// Token: 0x0400319B RID: 12699
		private int budgetTypeField;

		// Token: 0x0400319C RID: 12700
		private bool budgetTypeSpecifiedField;

		// Token: 0x0400319D RID: 12701
		private SpecialLogonType logonTypeField;

		// Token: 0x0400319E RID: 12702
		private ConnectingSIDType connectingSIDField;
	}
}
