using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class VDirConfigurationMissingException : DataSourceOperationException
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003775 File Offset: 0x00001975
		public VDirConfigurationMissingException(string lastServer, string urlType, string missingServiceType) : base(Strings.ErrorVDirConfigurationMissing(lastServer, urlType, missingServiceType))
		{
			this.lastServer = lastServer;
			this.urlType = urlType;
			this.missingServiceType = missingServiceType;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000379A File Offset: 0x0000199A
		public VDirConfigurationMissingException(string lastServer, string urlType, string missingServiceType, Exception innerException) : base(Strings.ErrorVDirConfigurationMissing(lastServer, urlType, missingServiceType), innerException)
		{
			this.lastServer = lastServer;
			this.urlType = urlType;
			this.missingServiceType = missingServiceType;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000037C4 File Offset: 0x000019C4
		protected VDirConfigurationMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lastServer = (string)info.GetValue("lastServer", typeof(string));
			this.urlType = (string)info.GetValue("urlType", typeof(string));
			this.missingServiceType = (string)info.GetValue("missingServiceType", typeof(string));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003839 File Offset: 0x00001A39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lastServer", this.lastServer);
			info.AddValue("urlType", this.urlType);
			info.AddValue("missingServiceType", this.missingServiceType);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003876 File Offset: 0x00001A76
		public string LastServer
		{
			get
			{
				return this.lastServer;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000387E File Offset: 0x00001A7E
		public string UrlType
		{
			get
			{
				return this.urlType;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003886 File Offset: 0x00001A86
		public string MissingServiceType
		{
			get
			{
				return this.missingServiceType;
			}
		}

		// Token: 0x0400006B RID: 107
		private readonly string lastServer;

		// Token: 0x0400006C RID: 108
		private readonly string urlType;

		// Token: 0x0400006D RID: 109
		private readonly string missingServiceType;
	}
}
