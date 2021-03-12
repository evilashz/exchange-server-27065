using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200007A RID: 122
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADServerSettingsChangedException : ADTransientException
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001FDCB File Offset: 0x0001DFCB
		internal ADServerSettingsChangedException(LocalizedString message, ADServerSettings serverSettings) : base(message)
		{
			if (serverSettings == null)
			{
				throw new ArgumentNullException("serverSettings");
			}
			this.serverSettings = serverSettings;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001FDE9 File Offset: 0x0001DFE9
		public ADServerSettingsChangedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001FDF3 File Offset: 0x0001DFF3
		protected ADServerSettingsChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverSettings = (ADServerSettings)info.GetValue("ServerSettings", typeof(ADServerSettings));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001FE1D File Offset: 0x0001E01D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ServerSettings", this.serverSettings);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001FE38 File Offset: 0x0001E038
		internal ADServerSettings ServerSettings
		{
			get
			{
				return this.serverSettings;
			}
		}

		// Token: 0x04000264 RID: 612
		private const string ServerSettingsLabel = "ServerSettings";

		// Token: 0x04000265 RID: 613
		private ADServerSettings serverSettings;
	}
}
