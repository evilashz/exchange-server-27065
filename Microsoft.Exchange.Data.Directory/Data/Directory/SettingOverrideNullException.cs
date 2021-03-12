using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B03 RID: 2819
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideNullException : SettingOverrideException
	{
		// Token: 0x060081B9 RID: 33209 RVA: 0x001A6F04 File Offset: 0x001A5104
		public SettingOverrideNullException() : base(DirectoryStrings.ErrorSettingOverrideNull)
		{
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x001A6F11 File Offset: 0x001A5111
		public SettingOverrideNullException(Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideNull, innerException)
		{
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x001A6F1F File Offset: 0x001A511F
		protected SettingOverrideNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060081BC RID: 33212 RVA: 0x001A6F29 File Offset: 0x001A5129
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
