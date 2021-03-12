using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B02 RID: 2818
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideException : LocalizedException
	{
		// Token: 0x060081B5 RID: 33205 RVA: 0x001A6EDD File Offset: 0x001A50DD
		public SettingOverrideException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x001A6EE6 File Offset: 0x001A50E6
		public SettingOverrideException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x001A6EF0 File Offset: 0x001A50F0
		protected SettingOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x001A6EFA File Offset: 0x001A50FA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
