using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BF5 RID: 3061
	internal class UpdatableHelpVersion
	{
		// Token: 0x06007374 RID: 29556 RVA: 0x001D5A38 File Offset: 0x001D3C38
		internal UpdatableHelpVersion(string version)
		{
			this.NormalizedVersionNumber = string.Empty;
			this.ElementIntValues = new int[4];
			string[] array = new string[4];
			string[] array2 = version.Split(new char[]
			{
				'.'
			});
			if (array2.Length == 4)
			{
				for (int i = 0; i < 4; i++)
				{
					string s = array2[i];
					if (!int.TryParse(s, out this.ElementIntValues[i]) || this.ElementIntValues[i] >= UpdatableHelpVersion.ElementUpperBound[i])
					{
						throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInvalidVersionNumberErrorID, UpdatableHelpStrings.UpdateInvalidVersionNumber(new LocalizedString(version)), ErrorCategory.InvalidData, null, null);
					}
					array[i] = this.ElementIntValues[i].ToString().PadLeft(UpdatableHelpVersion.ElementDigits[i], '0');
				}
				this.NormalizedVersionNumber = string.Join(".", array);
				return;
			}
			throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInvalidVersionNumberErrorID, UpdatableHelpStrings.UpdateInvalidVersionNumber(new LocalizedString(version)), ErrorCategory.InvalidData, null, null);
		}

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06007375 RID: 29557 RVA: 0x001D5B2F File Offset: 0x001D3D2F
		internal int MajorVersion
		{
			get
			{
				return this.ElementIntValues[0];
			}
		}

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x06007376 RID: 29558 RVA: 0x001D5B39 File Offset: 0x001D3D39
		internal int MinorVersion
		{
			get
			{
				return this.ElementIntValues[1];
			}
		}

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x06007377 RID: 29559 RVA: 0x001D5B43 File Offset: 0x001D3D43
		internal int BuildNumber
		{
			get
			{
				return this.ElementIntValues[2];
			}
		}

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x06007378 RID: 29560 RVA: 0x001D5B4D File Offset: 0x001D3D4D
		internal int DotBuildNumber
		{
			get
			{
				return this.ElementIntValues[3];
			}
		}

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x06007379 RID: 29561 RVA: 0x001D5B57 File Offset: 0x001D3D57
		// (set) Token: 0x0600737A RID: 29562 RVA: 0x001D5B5F File Offset: 0x001D3D5F
		internal string NormalizedVersionNumber { get; private set; }

		// Token: 0x0600737B RID: 29563 RVA: 0x001D5B68 File Offset: 0x001D3D68
		internal string NormalizedVersionNumberWithRevision(int revision)
		{
			return this.NormalizedVersionNumber + "." + revision.ToString();
		}

		// Token: 0x04003A99 RID: 15001
		internal const int NumVersionElements = 4;

		// Token: 0x04003A9A RID: 15002
		internal static int[] ElementUpperBound = new int[]
		{
			100,
			100,
			10000,
			1000
		};

		// Token: 0x04003A9B RID: 15003
		internal static int[] ElementDigits = new int[]
		{
			2,
			2,
			4,
			3
		};

		// Token: 0x04003A9C RID: 15004
		internal int[] ElementIntValues;
	}
}
