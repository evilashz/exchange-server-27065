using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200037B RID: 891
	internal sealed class FileAsDropDownList : DropDownList
	{
		// Token: 0x06002143 RID: 8515 RVA: 0x000BF824 File Offset: 0x000BDA24
		public FileAsDropDownList(string id, FileAsMapping fileAsMapping)
		{
			int num = (int)fileAsMapping;
			base..ctor(id, num.ToString(), null);
			this.fileAsMapping = fileAsMapping;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000BF849 File Offset: 0x000BDA49
		protected override void RenderSelectedValue(TextWriter writer)
		{
			Utilities.HtmlEncode(ContactUtilities.GetFileAsString(this.fileAsMapping), writer);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000BF85C File Offset: 0x000BDA5C
		protected override DropDownListItem[] CreateListItems()
		{
			DropDownListItem[] array = new DropDownListItem[FileAsDropDownList.fileAsMappingList.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DropDownListItem((int)FileAsDropDownList.fileAsMappingList[i], ContactUtilities.GetFileAsString(FileAsDropDownList.fileAsMappingList[i]), false);
			}
			return array;
		}

		// Token: 0x040017B9 RID: 6073
		private static readonly FileAsMapping[] fileAsMappingList = new FileAsMapping[]
		{
			FileAsMapping.LastCommaFirst,
			FileAsMapping.FirstSpaceLast,
			FileAsMapping.Company,
			FileAsMapping.LastCommaFirstCompany,
			FileAsMapping.CompanyLastCommaFirst,
			FileAsMapping.LastFirst,
			FileAsMapping.LastFirstCompany,
			FileAsMapping.CompanyLastFirst,
			FileAsMapping.LastFirstSuffix,
			FileAsMapping.LastSpaceFirstCompany,
			FileAsMapping.CompanyLastSpaceFirst,
			FileAsMapping.LastSpaceFirst
		};

		// Token: 0x040017BA RID: 6074
		private FileAsMapping fileAsMapping;
	}
}
