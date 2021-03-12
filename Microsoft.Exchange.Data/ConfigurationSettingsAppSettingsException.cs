using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000ED RID: 237
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsAppSettingsException : ConfigurationSettingsException
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0001B385 File Offset: 0x00019585
		public ConfigurationSettingsAppSettingsException() : base(DataStrings.ConfigurationSettingsAppSettingsError)
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001B392 File Offset: 0x00019592
		public ConfigurationSettingsAppSettingsException(Exception innerException) : base(DataStrings.ConfigurationSettingsAppSettingsError, innerException)
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B3A0 File Offset: 0x000195A0
		protected ConfigurationSettingsAppSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B3AA File Offset: 0x000195AA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
