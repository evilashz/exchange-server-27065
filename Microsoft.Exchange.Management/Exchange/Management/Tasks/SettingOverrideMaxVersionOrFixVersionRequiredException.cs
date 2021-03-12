using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001196 RID: 4502
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideMaxVersionOrFixVersionRequiredException : SettingOverrideException
	{
		// Token: 0x0600B6E6 RID: 46822 RVA: 0x002A0AFD File Offset: 0x0029ECFD
		public SettingOverrideMaxVersionOrFixVersionRequiredException() : base(Strings.ErrorMaxVersionOrFixVersionRequired)
		{
		}

		// Token: 0x0600B6E7 RID: 46823 RVA: 0x002A0B0A File Offset: 0x0029ED0A
		public SettingOverrideMaxVersionOrFixVersionRequiredException(Exception innerException) : base(Strings.ErrorMaxVersionOrFixVersionRequired, innerException)
		{
		}

		// Token: 0x0600B6E8 RID: 46824 RVA: 0x002A0B18 File Offset: 0x0029ED18
		protected SettingOverrideMaxVersionOrFixVersionRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B6E9 RID: 46825 RVA: 0x002A0B22 File Offset: 0x0029ED22
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
