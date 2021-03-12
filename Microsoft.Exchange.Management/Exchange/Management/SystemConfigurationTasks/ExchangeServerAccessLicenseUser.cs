using System;
using System.Text;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009AB RID: 2475
	[Serializable]
	public sealed class ExchangeServerAccessLicenseUser : ExchangeServerAccessLicenseBase
	{
		// Token: 0x0600585E RID: 22622 RVA: 0x001708E6 File Offset: 0x0016EAE6
		public ExchangeServerAccessLicenseUser(string licenseName, string name) : base(licenseName)
		{
			this.Name = name;
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x0600585F RID: 22623 RVA: 0x001708F6 File Offset: 0x0016EAF6
		// (set) Token: 0x06005860 RID: 22624 RVA: 0x001708FE File Offset: 0x0016EAFE
		public string Name { get; private set; }

		// Token: 0x06005861 RID: 22625 RVA: 0x00170908 File Offset: 0x0016EB08
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LicenseName: ");
			stringBuilder.AppendLine(base.LicenseName);
			stringBuilder.Append("Name: ");
			stringBuilder.AppendLine(this.Name);
			return stringBuilder.ToString();
		}
	}
}
