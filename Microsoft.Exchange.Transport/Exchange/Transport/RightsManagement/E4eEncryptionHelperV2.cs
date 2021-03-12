using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003DB RID: 987
	internal class E4eEncryptionHelperV2 : E4eEncryptionHelper
	{
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000B4E12 File Offset: 0x000B3012
		internal new static E4eEncryptionHelperV2 Instance
		{
			get
			{
				if (E4eEncryptionHelperV2.instance == null)
				{
					E4eEncryptionHelperV2.instance = new E4eEncryptionHelperV2();
				}
				return E4eEncryptionHelperV2.instance;
			}
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000B4E2A File Offset: 0x000B302A
		internal virtual int Version()
		{
			return 2;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000B4E30 File Offset: 0x000B3030
		internal override void AppendVersionInfo(StringBuilder html, string messageId)
		{
			html.Append(string.Format("<input type='hidden' name='{0}' value='{1}' />", "version", this.Version()));
			E4eLog.Instance.LogInfo(messageId, "[E][form].Version: {0}", new object[]
			{
				this.Version()
			});
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000B4E84 File Offset: 0x000B3084
		internal override byte[] SignText(RSACryptoServiceProvider csp, byte[] data)
		{
			byte[] result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] rgbHash = sha256Cng.ComputeHash(data);
				result = csp.SignHash(rgbHash, CryptoConfig.MapNameToOID("SHA256"));
			}
			return result;
		}

		// Token: 0x0400166C RID: 5740
		private static E4eEncryptionHelperV2 instance;
	}
}
