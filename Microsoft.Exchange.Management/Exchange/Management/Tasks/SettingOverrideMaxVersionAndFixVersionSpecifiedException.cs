using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001197 RID: 4503
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideMaxVersionAndFixVersionSpecifiedException : SettingOverrideException
	{
		// Token: 0x0600B6EA RID: 46826 RVA: 0x002A0B2C File Offset: 0x0029ED2C
		public SettingOverrideMaxVersionAndFixVersionSpecifiedException() : base(Strings.ErrorMaxVersionAndFixVersionSpecified)
		{
		}

		// Token: 0x0600B6EB RID: 46827 RVA: 0x002A0B39 File Offset: 0x0029ED39
		public SettingOverrideMaxVersionAndFixVersionSpecifiedException(Exception innerException) : base(Strings.ErrorMaxVersionAndFixVersionSpecified, innerException)
		{
		}

		// Token: 0x0600B6EC RID: 46828 RVA: 0x002A0B47 File Offset: 0x0029ED47
		protected SettingOverrideMaxVersionAndFixVersionSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B6ED RID: 46829 RVA: 0x002A0B51 File Offset: 0x0029ED51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
