using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps
{
	// Token: 0x0200042C RID: 1068
	public class FailureCategory
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0009AF5E File Offset: 0x0009915E
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x0009AF66 File Offset: 0x00099166
		public string IDRegex
		{
			get
			{
				return this.idRegex;
			}
			set
			{
				this.idRegex = value;
				if (!string.IsNullOrEmpty(this.idRegex))
				{
					this.fcReg = new Regex(this.idRegex);
				}
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0009AF8D File Offset: 0x0009918D
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x0009AF95 File Offset: 0x00099195
		public string Description { get; set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0009AF9E File Offset: 0x0009919E
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0009AFA6 File Offset: 0x000991A6
		[XmlArray]
		[XmlArrayItem("Instance")]
		public FailureInstance[] Instances { get; set; }

		// Token: 0x06001B88 RID: 7048 RVA: 0x0009AFAF File Offset: 0x000991AF
		public bool IsMatch(string errorMessage)
		{
			return this.fcReg != null && this.fcReg.IsMatch(errorMessage);
		}

		// Token: 0x040012D2 RID: 4818
		private Regex fcReg;

		// Token: 0x040012D3 RID: 4819
		private string idRegex;
	}
}
