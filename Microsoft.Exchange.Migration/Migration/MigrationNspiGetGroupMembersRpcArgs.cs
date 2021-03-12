using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E5 RID: 229
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiGetGroupMembersRpcArgs : MigrationNspiRpcArgs
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00033FE6 File Offset: 0x000321E6
		public MigrationNspiGetGroupMembersRpcArgs(ExchangeOutlookAnywhereEndpoint endpoint, string groupSmtpAddress) : base(endpoint, MigrationProxyRpcType.GetGroupMembers)
		{
			this.GroupSmtpAddress = groupSmtpAddress;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00033FF7 File Offset: 0x000321F7
		public MigrationNspiGetGroupMembersRpcArgs(byte[] requestBlob) : base(requestBlob, MigrationProxyRpcType.GetGroupMembers)
		{
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00034001 File Offset: 0x00032201
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0003400E File Offset: 0x0003220E
		public string GroupSmtpAddress
		{
			get
			{
				return base.GetProperty<string>(2416508959U);
			}
			set
			{
				base.SetPropertyAsString(2416508959U, value);
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0003401C File Offset: 0x0003221C
		public override bool Validate(out string errorMsg)
		{
			if (!base.Validate(out errorMsg))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.GroupSmtpAddress))
			{
				errorMsg = "Group Smtp Address cannot be null or empty.";
				return false;
			}
			errorMsg = null;
			return true;
		}
	}
}
