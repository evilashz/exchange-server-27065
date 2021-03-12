using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.PST;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000075 RID: 117
	internal sealed class PstTargetFolderProvider : TargetFolderProvider<uint, IFolder, IPST>
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		protected override void InitializeTargetFolderHierarchy()
		{
			base.FolderMapping.Add("", 32802U);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001CAC3 File Offset: 0x0001ACC3
		protected override IFolder GetFolder(IPST targetSession, uint folderId)
		{
			return targetSession.ReadFolder(folderId);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001CACC File Offset: 0x0001ACCC
		protected override string GenerateTopLevelFolderName(bool isArchive)
		{
			if (!isArchive)
			{
				return "Primary Mailbox";
			}
			return "Archive Mailbox";
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001CADC File Offset: 0x0001ACDC
		protected override IFolder CreateFolder(IPST targetSession, IFolder parentFolder, string folderName)
		{
			List<uint> subFolderIds = parentFolder.SubFolderIds;
			if (subFolderIds != null)
			{
				foreach (uint num in subFolderIds)
				{
					IFolder folder = targetSession.ReadFolder(num);
					Dictionary<ushort, IProperty> properties = folder.PropertyBag.Properties;
					IProperty property = null;
					if (properties.TryGetValue(PropertyTag.DisplayName.Id, out property))
					{
						IPropertyReader propertyReader = property.OpenStreamReader();
						string @string = Encoding.Unicode.GetString(propertyReader.Read());
						if (@string == folderName)
						{
							return folder;
						}
						propertyReader.Close();
					}
				}
			}
			IFolder folder2 = parentFolder.AddFolder();
			IProperty property2 = folder2.PropertyBag.AddProperty(PropertyTag.DisplayName.Value);
			IPropertyWriter propertyWriter = property2.OpenStreamWriter();
			propertyWriter.Write(Encoding.Unicode.GetBytes(folderName));
			propertyWriter.Close();
			folder2.Save();
			return folder2;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		protected override uint GetFolderId(IFolder folder)
		{
			return folder.Id;
		}

		// Token: 0x040002CD RID: 717
		private const int PstIpmRootFolderId = 32802;
	}
}
