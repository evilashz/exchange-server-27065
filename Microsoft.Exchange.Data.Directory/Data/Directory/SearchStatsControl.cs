using System;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200018A RID: 394
	[Serializable]
	internal class SearchStatsControl : DirectoryControl
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x000501B8 File Offset: 0x0004E3B8
		public SearchStatsControl() : base(SearchStatsControl.SearchStatOid, null, false, true)
		{
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000501C8 File Offset: 0x0004E3C8
		private SearchStatsControl(byte[] value) : base(SearchStatsControl.SearchStatOid, value, false, true)
		{
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000501D8 File Offset: 0x0004E3D8
		public static SearchStatsControl FindSearchStatsControl(DirectoryResponse response)
		{
			return SearchStatsControl.FindSearchStatsControl(response.Controls);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000501E8 File Offset: 0x0004E3E8
		public static SearchStatsControl FindSearchStatsControl(DirectoryRequest request)
		{
			DirectoryControl[] array = new DirectoryControl[request.Controls.Count];
			request.Controls.CopyTo(array, 0);
			return SearchStatsControl.FindSearchStatsControl(array);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0005021C File Offset: 0x0004E41C
		private static SearchStatsControl FindSearchStatsControl(DirectoryControl[] controls)
		{
			foreach (DirectoryControl directoryControl in controls)
			{
				if (directoryControl.Type.Equals(SearchStatsControl.SearchStatOid, StringComparison.OrdinalIgnoreCase))
				{
					return new SearchStatsControl(directoryControl.GetValue());
				}
			}
			return null;
		}

		// Token: 0x04000972 RID: 2418
		private static readonly string SearchStatOid = "1.2.840.113556.1.4.970";
	}
}
