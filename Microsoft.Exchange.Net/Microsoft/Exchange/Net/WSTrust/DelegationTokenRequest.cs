using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B5C RID: 2908
	[Serializable]
	internal sealed class DelegationTokenRequest
	{
		// Token: 0x06003E63 RID: 15971 RVA: 0x000A2D4C File Offset: 0x000A0F4C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append("FederatedIdentity=");
			stringBuilder.AppendLine(this.FederatedIdentity.ToString());
			stringBuilder.Append("EmailAddress=");
			stringBuilder.AppendLine(this.EmailAddress.ToString());
			if (!string.IsNullOrEmpty(this.Policy))
			{
				stringBuilder.Append("Policy=");
				stringBuilder.AppendLine(this.Policy);
			}
			stringBuilder.Append("Target=");
			stringBuilder.AppendLine(this.Target.ToString());
			stringBuilder.Append("Offer=");
			stringBuilder.AppendLine(this.Offer.ToString());
			if (this.EmailAddresses != null && this.EmailAddresses.Count != 0)
			{
				stringBuilder.Append("EmailAddresses=");
				bool flag = true;
				foreach (string value in this.EmailAddresses)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(value);
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400365A RID: 13914
		public FederatedIdentity FederatedIdentity;

		// Token: 0x0400365B RID: 13915
		public string EmailAddress;

		// Token: 0x0400365C RID: 13916
		public string Policy;

		// Token: 0x0400365D RID: 13917
		public TokenTarget Target;

		// Token: 0x0400365E RID: 13918
		public Offer Offer;

		// Token: 0x0400365F RID: 13919
		public List<string> EmailAddresses;
	}
}
