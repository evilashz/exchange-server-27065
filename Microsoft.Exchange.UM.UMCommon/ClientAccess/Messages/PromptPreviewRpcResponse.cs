using System;
using System.IO;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000137 RID: 311
	[Serializable]
	public class PromptPreviewRpcResponse : ResponseBase
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x0002656C File Offset: 0x0002476C
		public PromptPreviewRpcResponse(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			using (FileStream fileStream = fileInfo.OpenRead())
			{
				byte[] array = new byte[fileStream.Length];
				int i = (int)fileStream.Length;
				int num = 0;
				while (i > 0)
				{
					int num2 = fileStream.Read(array, num, i);
					if (num2 == 0)
					{
						break;
					}
					num += num2;
					i -= num2;
				}
				this.AudioData = array;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000265EC File Offset: 0x000247EC
		public PromptPreviewRpcResponse()
		{
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x000265F4 File Offset: 0x000247F4
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x000265FC File Offset: 0x000247FC
		public byte[] AudioData { get; private set; }
	}
}
