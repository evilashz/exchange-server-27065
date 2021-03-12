using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF0 RID: 2800
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsADConfigDriverException : ConfigurationSettingsException
	{
		// Token: 0x06008159 RID: 33113 RVA: 0x001A65AC File Offset: 0x001A47AC
		public ConfigurationSettingsADConfigDriverException() : base(DirectoryStrings.ConfigurationSettingsADConfigDriverError)
		{
		}

		// Token: 0x0600815A RID: 33114 RVA: 0x001A65B9 File Offset: 0x001A47B9
		public ConfigurationSettingsADConfigDriverException(Exception innerException) : base(DirectoryStrings.ConfigurationSettingsADConfigDriverError, innerException)
		{
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x001A65C7 File Offset: 0x001A47C7
		protected ConfigurationSettingsADConfigDriverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x001A65D1 File Offset: 0x001A47D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
