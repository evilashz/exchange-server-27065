using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BF6 RID: 3062
	internal class UpdatableHelpVersionRange
	{
		// Token: 0x0600737D RID: 29565 RVA: 0x001D5BD8 File Offset: 0x001D3DD8
		internal UpdatableHelpVersionRange(string versionRange, int revision, string culturesUpdated, string cabinetUrl)
		{
			string[] array = versionRange.Split(new char[]
			{
				'-'
			});
			if (array == null || array.Length < 1 || array.Length > 2)
			{
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInvalidVersionNumberErrorID, UpdatableHelpStrings.UpdateInvalidVersionNumber(new LocalizedString(versionRange)), ErrorCategory.InvalidData, null, null);
			}
			this.StartVersion = new UpdatableHelpVersion(array[0].Trim());
			this.EndVersion = ((array.Length == 2) ? new UpdatableHelpVersion(array[1].Trim()) : this.StartVersion);
			if (this.CompareVersions(this.StartVersion, this.EndVersion) > 0)
			{
				UpdatableHelpVersion startVersion = this.StartVersion;
				this.StartVersion = this.EndVersion;
				this.EndVersion = startVersion;
			}
			this.HelpRevision = revision;
			this.CabinetUrl = cabinetUrl;
			string[] array2 = culturesUpdated.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			List<string> list = new List<string>();
			if (array2 != null)
			{
				foreach (string text in array2)
				{
					list.Add(text.Trim().ToLower());
				}
			}
			this.CulturesAffected = list.ToArray();
		}

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x001D5CFD File Offset: 0x001D3EFD
		// (set) Token: 0x0600737F RID: 29567 RVA: 0x001D5D05 File Offset: 0x001D3F05
		internal UpdatableHelpVersion StartVersion { get; private set; }

		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x001D5D0E File Offset: 0x001D3F0E
		// (set) Token: 0x06007381 RID: 29569 RVA: 0x001D5D16 File Offset: 0x001D3F16
		internal UpdatableHelpVersion EndVersion { get; private set; }

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x001D5D1F File Offset: 0x001D3F1F
		// (set) Token: 0x06007383 RID: 29571 RVA: 0x001D5D27 File Offset: 0x001D3F27
		internal int HelpRevision { get; private set; }

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x001D5D30 File Offset: 0x001D3F30
		// (set) Token: 0x06007385 RID: 29573 RVA: 0x001D5D38 File Offset: 0x001D3F38
		internal string[] CulturesAffected { get; private set; }

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x001D5D41 File Offset: 0x001D3F41
		// (set) Token: 0x06007387 RID: 29575 RVA: 0x001D5D49 File Offset: 0x001D3F49
		internal string CabinetUrl { get; private set; }

		// Token: 0x06007388 RID: 29576 RVA: 0x001D5D52 File Offset: 0x001D3F52
		internal bool IsInRangeAndNewerThan(UpdatableHelpVersion currentVersion, int newestRevisionFound)
		{
			return this.HelpRevision > newestRevisionFound && this.CompareVersions(currentVersion, this.StartVersion) >= 0 && this.CompareVersions(currentVersion, this.EndVersion) <= 0;
		}

		// Token: 0x06007389 RID: 29577 RVA: 0x001D5D80 File Offset: 0x001D3F80
		private int CompareVersions(UpdatableHelpVersion v1, UpdatableHelpVersion v2)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				num = v1.ElementIntValues[i] - v2.ElementIntValues[i];
				if (num != 0)
				{
					break;
				}
			}
			return num;
		}
	}
}
