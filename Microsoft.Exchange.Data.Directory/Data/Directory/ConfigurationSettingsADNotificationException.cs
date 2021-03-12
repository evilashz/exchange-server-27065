using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF1 RID: 2801
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsADNotificationException : ConfigurationSettingsException
	{
		// Token: 0x0600815D RID: 33117 RVA: 0x001A65DB File Offset: 0x001A47DB
		public ConfigurationSettingsADNotificationException() : base(DirectoryStrings.ConfigurationSettingsADNotificationError)
		{
		}

		// Token: 0x0600815E RID: 33118 RVA: 0x001A65E8 File Offset: 0x001A47E8
		public ConfigurationSettingsADNotificationException(Exception innerException) : base(DirectoryStrings.ConfigurationSettingsADNotificationError, innerException)
		{
		}

		// Token: 0x0600815F RID: 33119 RVA: 0x001A65F6 File Offset: 0x001A47F6
		protected ConfigurationSettingsADNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x001A6600 File Offset: 0x001A4800
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
