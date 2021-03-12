using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200117C RID: 4476
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToRunGlobalMonitoringOverrideException : LocalizedException
	{
		// Token: 0x0600B654 RID: 46676 RVA: 0x0029F999 File Offset: 0x0029DB99
		public FailedToRunGlobalMonitoringOverrideException(string container) : base(Strings.FailedToRunGlobalMonitoringOverride(container))
		{
			this.container = container;
		}

		// Token: 0x0600B655 RID: 46677 RVA: 0x0029F9AE File Offset: 0x0029DBAE
		public FailedToRunGlobalMonitoringOverrideException(string container, Exception innerException) : base(Strings.FailedToRunGlobalMonitoringOverride(container), innerException)
		{
			this.container = container;
		}

		// Token: 0x0600B656 RID: 46678 RVA: 0x0029F9C4 File Offset: 0x0029DBC4
		protected FailedToRunGlobalMonitoringOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.container = (string)info.GetValue("container", typeof(string));
		}

		// Token: 0x0600B657 RID: 46679 RVA: 0x0029F9EE File Offset: 0x0029DBEE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("container", this.container);
		}

		// Token: 0x17003981 RID: 14721
		// (get) Token: 0x0600B658 RID: 46680 RVA: 0x0029FA09 File Offset: 0x0029DC09
		public string Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x040062E7 RID: 25319
		private readonly string container;
	}
}
