using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E99 RID: 3737
	internal static class EwsIdConverter
	{
		// Token: 0x0600614B RID: 24907 RVA: 0x0012F65D File Offset: 0x0012D85D
		public static string EwsIdToODataId(string ewsId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ewsId", ewsId);
			return ewsId.Replace('/', '-').Replace('+', '_');
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x0012F67D File Offset: 0x0012D87D
		public static string ODataIdToEwsId(string odataId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("odataId", odataId);
			return odataId.Replace('-', '/').Replace('_', '+');
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x0012F6A0 File Offset: 0x0012D8A0
		public static BaseFolderId CreateFolderIdFromEwsId(string id)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			DistinguishedFolderIdName id2;
			BaseFolderId result;
			if (Enum.TryParse<DistinguishedFolderIdName>(id, true, out id2))
			{
				result = new DistinguishedFolderId
				{
					Id = id2
				};
			}
			else if (string.Equals(EwsIdConverter.RootFolderName, id, StringComparison.OrdinalIgnoreCase))
			{
				result = new DistinguishedFolderId
				{
					Id = DistinguishedFolderIdName.msgfolderroot
				};
			}
			else
			{
				result = new FolderId
				{
					Id = id
				};
			}
			return result;
		}

		// Token: 0x040034A9 RID: 13481
		public static string RootFolderName = "RootFolder";
	}
}
