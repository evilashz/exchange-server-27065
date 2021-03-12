using System;
using System.Security;
using System.Text;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x0200019A RID: 410
	internal class Office365ConnectionSettings : ProtocolSpecificConnectionSettings
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x0003F629 File Offset: 0x0003D829
		public Office365ConnectionSettings() : base(ConnectionSettingsType.Office365)
		{
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003F633 File Offset: 0x0003D833
		public Office365ConnectionSettings(MiniRecipient adUser) : base(ConnectionSettingsType.Office365)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser", "The adUser cannot be null.");
			}
			this.AdUser = adUser;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003F657 File Offset: 0x0003D857
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0003F65F File Offset: 0x0003D85F
		public MiniRecipient AdUser { get; private set; }

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003F668 File Offset: 0x0003D868
		public bool IsSameAccount(Office365ConnectionSettings otherConnectionSettings)
		{
			return otherConnectionSettings != null && otherConnectionSettings.AdUser != null && this.AdUser != null && this.AdUser.DistinguishedName.Equals(otherConnectionSettings.AdUser.DistinguishedName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003F69C File Offset: 0x0003D89C
		public override string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Get {0}", base.ToMultiLineString(lineSeparator));
			return stringBuilder.ToString();
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
		protected override OperationStatusCode TestUserCanLogonWithCurrentSettings(SmtpAddress email, string userName, SecureString password)
		{
			return OperationStatusCode.Success;
		}
	}
}
