using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000352 RID: 850
	internal class LinkClickedSignalStats
	{
		// Token: 0x06001BB7 RID: 7095 RVA: 0x0006AA00 File Offset: 0x00068C00
		public LinkClickedSignalStats()
		{
			this.linkHash = "";
			this.userHash = "";
			this.nrRecipients = 0;
			this.nrDLs = 0;
			this.nrOpenDLs = 0;
			this.nrGroups = 0;
			this.nrOpenGroups = 0;
			this.isInternalLink = false;
			this.isSPURLValid = true;
			this.isValidSignal = false;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0006AA64 File Offset: 0x00068C64
		internal string GetLinkClickedSignalStatsLogString()
		{
			return string.Format("LinkClickedSignalStats:{0};{1};{2};{3};{4};{5};{6};{7};{8},{9}", new object[]
			{
				this.linkHash,
				this.userHash,
				this.nrRecipients.ToString(),
				this.nrDLs.ToString(),
				this.nrOpenDLs.ToString(),
				this.nrGroups.ToString(),
				this.nrOpenGroups.ToString(),
				this.isInternalLink.ToString(),
				this.isSPURLValid.ToString(),
				this.isValidSignal.ToString()
			});
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0006AB0C File Offset: 0x00068D0C
		internal static string GenerateObfuscatingHash(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			string result;
			using (SHA256 sha = new SHA256Cng())
			{
				byte[] value = sha.ComputeHash(bytes);
				result = BitConverter.ToString(value).Replace("-", string.Empty);
			}
			return result;
		}

		// Token: 0x04000FB2 RID: 4018
		internal string linkHash;

		// Token: 0x04000FB3 RID: 4019
		internal string userHash;

		// Token: 0x04000FB4 RID: 4020
		internal int nrRecipients;

		// Token: 0x04000FB5 RID: 4021
		internal int nrDLs;

		// Token: 0x04000FB6 RID: 4022
		internal int nrOpenDLs;

		// Token: 0x04000FB7 RID: 4023
		internal int nrGroups;

		// Token: 0x04000FB8 RID: 4024
		internal int nrOpenGroups;

		// Token: 0x04000FB9 RID: 4025
		internal bool isInternalLink;

		// Token: 0x04000FBA RID: 4026
		internal bool isSPURLValid;

		// Token: 0x04000FBB RID: 4027
		internal bool isValidSignal;
	}
}
