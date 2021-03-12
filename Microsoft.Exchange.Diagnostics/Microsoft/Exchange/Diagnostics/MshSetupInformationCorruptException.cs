using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000417 RID: 1047
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MshSetupInformationCorruptException : LocalizedException
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x0005F82C File Offset: 0x0005DA2C
		public MshSetupInformationCorruptException(string keyPath) : base(DiagnosticsResources.ExceptionMshSetupInformationCorrupt(keyPath))
		{
			this.keyPath = keyPath;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005F841 File Offset: 0x0005DA41
		public MshSetupInformationCorruptException(string keyPath, Exception innerException) : base(DiagnosticsResources.ExceptionMshSetupInformationCorrupt(keyPath), innerException)
		{
			this.keyPath = keyPath;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0005F857 File Offset: 0x0005DA57
		protected MshSetupInformationCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyPath = (string)info.GetValue("keyPath", typeof(string));
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005F881 File Offset: 0x0005DA81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyPath", this.keyPath);
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0005F89C File Offset: 0x0005DA9C
		public string KeyPath
		{
			get
			{
				return this.keyPath;
			}
		}

		// Token: 0x04001DEB RID: 7659
		private readonly string keyPath;
	}
}
