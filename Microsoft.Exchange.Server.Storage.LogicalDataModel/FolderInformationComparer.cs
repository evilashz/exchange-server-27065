using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200004F RID: 79
	public class FolderInformationComparer : IComparer<IFolderInformation>
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00042D98 File Offset: 0x00040F98
		internal FolderInformationComparer(CompareInfo compareInfo)
		{
			this.compareInfo = compareInfo;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00042DA7 File Offset: 0x00040FA7
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.compareInfo;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00042DB0 File Offset: 0x00040FB0
		public int Compare(IFolderInformation x, IFolderInformation y)
		{
			int num = this.compareInfo.Compare(x.DisplayName, y.DisplayName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
			if (num != 0)
			{
				return num;
			}
			return x.Fid.CompareTo(y.Fid);
		}

		// Token: 0x040003B3 RID: 947
		private CompareInfo compareInfo;
	}
}
