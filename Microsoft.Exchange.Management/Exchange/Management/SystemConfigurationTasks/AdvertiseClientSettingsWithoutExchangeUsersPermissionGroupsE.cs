using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F80 RID: 3968
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdvertiseClientSettingsWithoutExchangeUsersPermissionGroupsException : LocalizedException
	{
		// Token: 0x0600AC61 RID: 44129 RVA: 0x00290326 File Offset: 0x0028E526
		public AdvertiseClientSettingsWithoutExchangeUsersPermissionGroupsException() : base(Strings.AdvertiseClientSettingsWithoutExchangeUsersPermissionGroups)
		{
		}

		// Token: 0x0600AC62 RID: 44130 RVA: 0x00290333 File Offset: 0x0028E533
		public AdvertiseClientSettingsWithoutExchangeUsersPermissionGroupsException(Exception innerException) : base(Strings.AdvertiseClientSettingsWithoutExchangeUsersPermissionGroups, innerException)
		{
		}

		// Token: 0x0600AC63 RID: 44131 RVA: 0x00290341 File Offset: 0x0028E541
		protected AdvertiseClientSettingsWithoutExchangeUsersPermissionGroupsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC64 RID: 44132 RVA: 0x0029034B File Offset: 0x0028E54B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
