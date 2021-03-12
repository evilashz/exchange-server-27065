using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200012D RID: 301
	internal sealed class InfobarRenderingHelper
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000454FC File Offset: 0x000436FC
		public string FileNameStringForLevelOneAndBlock
		{
			get
			{
				return this.fileNameStringForLevelOneAndBlock;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00045504 File Offset: 0x00043704
		public bool HasLevelOneAndBlock
		{
			get
			{
				return this.hasLevelOneAndBlock;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0004550C File Offset: 0x0004370C
		public bool HasWebReadyFirst
		{
			get
			{
				return this.hasWebReadyFirst;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00045514 File Offset: 0x00043714
		public bool HasLevelOne
		{
			get
			{
				return this.hasLevelOne;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0004551C File Offset: 0x0004371C
		public bool HasLevelTwo
		{
			get
			{
				return this.hasLevelTwo;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00045524 File Offset: 0x00043724
		public bool HasLevelThree
		{
			get
			{
				return this.hasLevelThree;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0004552C File Offset: 0x0004372C
		public InfobarRenderingHelper(ArrayList attachmentList)
		{
			this.CreateAttachmentInfobarHelper(attachmentList);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00045548 File Offset: 0x00043748
		private void CreateAttachmentInfobarHelper(ArrayList attachmentList)
		{
			if (attachmentList == null || attachmentList.Count <= 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			UserContext userContext = UserContextManager.GetUserContext();
			AttachmentPolicy attachmentPolicy = userContext.AttachmentPolicy;
			foreach (object obj in attachmentList)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
				bool flag = AttachmentUtility.IsWebReadyDocument(attachmentWellInfo.FileExtension, attachmentWellInfo.MimeType);
				if (flag && (attachmentWellInfo.AttachmentLevel == AttachmentPolicy.Level.Block || attachmentPolicy.ForceWebReadyDocumentViewingFirst))
				{
					this.hasWebReadyFirst = true;
				}
				if (attachmentWellInfo.AttachmentLevel == AttachmentPolicy.Level.Block)
				{
					this.hasLevelOne = true;
					if (!flag)
					{
						this.hasLevelOneAndBlock = true;
						num++;
						if (num == 16)
						{
							stringBuilder.Append(",...");
						}
						else if (num <= 15)
						{
							if (num != 1)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append(attachmentWellInfo.AttachmentName);
						}
					}
				}
				else if (attachmentWellInfo.AttachmentLevel == AttachmentPolicy.Level.ForceSave)
				{
					this.hasLevelTwo = true;
				}
				else if (attachmentWellInfo.AttachmentLevel == AttachmentPolicy.Level.Allow)
				{
					this.hasLevelThree = true;
				}
			}
			if (stringBuilder.Length > 0)
			{
				this.fileNameStringForLevelOneAndBlock = stringBuilder.ToString();
			}
		}

		// Token: 0x04000761 RID: 1889
		private string fileNameStringForLevelOneAndBlock = string.Empty;

		// Token: 0x04000762 RID: 1890
		private bool hasLevelOneAndBlock;

		// Token: 0x04000763 RID: 1891
		private bool hasWebReadyFirst;

		// Token: 0x04000764 RID: 1892
		private bool hasLevelOne;

		// Token: 0x04000765 RID: 1893
		private bool hasLevelTwo;

		// Token: 0x04000766 RID: 1894
		private bool hasLevelThree;
	}
}
