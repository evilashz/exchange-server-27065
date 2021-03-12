using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProtocolConfigurationMissingException : DataSourceOperationException
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000388E File Offset: 0x00001A8E
		public ProtocolConfigurationMissingException(string lastServer, string settingsType) : base(Strings.ErrorProtocolConfigurationMissing(lastServer, settingsType))
		{
			this.lastServer = lastServer;
			this.settingsType = settingsType;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038AB File Offset: 0x00001AAB
		public ProtocolConfigurationMissingException(string lastServer, string settingsType, Exception innerException) : base(Strings.ErrorProtocolConfigurationMissing(lastServer, settingsType), innerException)
		{
			this.lastServer = lastServer;
			this.settingsType = settingsType;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000038CC File Offset: 0x00001ACC
		protected ProtocolConfigurationMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lastServer = (string)info.GetValue("lastServer", typeof(string));
			this.settingsType = (string)info.GetValue("settingsType", typeof(string));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003921 File Offset: 0x00001B21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lastServer", this.lastServer);
			info.AddValue("settingsType", this.settingsType);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000394D File Offset: 0x00001B4D
		public string LastServer
		{
			get
			{
				return this.lastServer;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003955 File Offset: 0x00001B55
		public string SettingsType
		{
			get
			{
				return this.settingsType;
			}
		}

		// Token: 0x0400006E RID: 110
		private readonly string lastServer;

		// Token: 0x0400006F RID: 111
		private readonly string settingsType;
	}
}
