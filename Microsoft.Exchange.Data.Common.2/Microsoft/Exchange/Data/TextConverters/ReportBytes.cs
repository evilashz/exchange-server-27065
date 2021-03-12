using System;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000182 RID: 386
	internal class ReportBytes : IReportBytes
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x00079F02 File Offset: 0x00078102
		internal ReportBytes() : this(0, 0)
		{
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00079F0C File Offset: 0x0007810C
		internal ReportBytes(int expansionSizeLimit, int expansionSizeMultiple)
		{
			if (expansionSizeLimit > 0 && expansionSizeMultiple > 0)
			{
				this.expansionSizeLimit = expansionSizeLimit;
				this.expansionSizeMultiple = expansionSizeMultiple;
			}
			else
			{
				if (!ReportBytes.isReadFromConfiguration)
				{
					CtsConfigurationSetting simpleConfigurationSetting = ApplicationServices.GetSimpleConfigurationSetting("TextConverters", "ExpansionSizeLimit");
					if (simpleConfigurationSetting != null)
					{
						ReportBytes.configExpansionSizeLimit = ApplicationServices.ParseIntegerSetting(simpleConfigurationSetting, 524288, 1, true);
					}
					else
					{
						ReportBytes.configExpansionSizeLimit = 524288;
					}
					simpleConfigurationSetting = ApplicationServices.GetSimpleConfigurationSetting("TextConverters", "ExpansionSizeMultiple");
					if (simpleConfigurationSetting != null)
					{
						ReportBytes.configExpansionSizeMultiple = ApplicationServices.ParseIntegerSetting(simpleConfigurationSetting, 10, 5, false);
					}
					else
					{
						ReportBytes.configExpansionSizeMultiple = 10;
					}
					ReportBytes.isReadFromConfiguration = true;
				}
				this.expansionSizeLimit = ReportBytes.configExpansionSizeLimit;
				this.expansionSizeMultiple = ReportBytes.configExpansionSizeMultiple;
			}
			this.bytesRead = 0L;
			this.bytesWritten = 0L;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00079FC6 File Offset: 0x000781C6
		public void ReportBytesRead(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.bytesRead += (long)count;
			this.CheckBytes();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00079FEC File Offset: 0x000781EC
		public void ReportBytesWritten(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.bytesWritten += (long)count;
			this.CheckBytes();
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0007A014 File Offset: 0x00078214
		private void CheckBytes()
		{
			if (this.bytesRead == 0L)
			{
				return;
			}
			if (this.bytesRead < 0L)
			{
				throw new InvalidOperationException("ReportBytes.bytesRead < 0");
			}
			if (this.bytesWritten < 0L)
			{
				throw new InvalidOperationException("ReportBytes.bytesWritten < 0");
			}
			if (this.bytesWritten > (long)this.expansionSizeLimit && this.bytesWritten / this.bytesRead > (long)this.expansionSizeMultiple)
			{
				throw new TextConvertersException(TextConvertersStrings.DocumentGrowingExcessively(this.expansionSizeMultiple));
			}
		}

		// Token: 0x04001154 RID: 4436
		private const int DefaultExpansionSizeLimit = 524288;

		// Token: 0x04001155 RID: 4437
		private const int DefaultExpansionSizeMultiple = 10;

		// Token: 0x04001156 RID: 4438
		private static bool isReadFromConfiguration = false;

		// Token: 0x04001157 RID: 4439
		private static int configExpansionSizeLimit = -1;

		// Token: 0x04001158 RID: 4440
		private static int configExpansionSizeMultiple = -1;

		// Token: 0x04001159 RID: 4441
		private int expansionSizeLimit;

		// Token: 0x0400115A RID: 4442
		private int expansionSizeMultiple;

		// Token: 0x0400115B RID: 4443
		private long bytesRead;

		// Token: 0x0400115C RID: 4444
		private long bytesWritten;
	}
}
