using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DEA RID: 3562
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ElcUserConfigurationException : LocalizedException
	{
		// Token: 0x0600A486 RID: 42118 RVA: 0x002846AE File Offset: 0x002828AE
		public ElcUserConfigurationException(string reason) : base(Strings.ElcUserConfigurationException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600A487 RID: 42119 RVA: 0x002846C3 File Offset: 0x002828C3
		public ElcUserConfigurationException(string reason, Exception innerException) : base(Strings.ElcUserConfigurationException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600A488 RID: 42120 RVA: 0x002846D9 File Offset: 0x002828D9
		protected ElcUserConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600A489 RID: 42121 RVA: 0x00284703 File Offset: 0x00282903
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170035FB RID: 13819
		// (get) Token: 0x0600A48A RID: 42122 RVA: 0x0028471E File Offset: 0x0028291E
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04005F61 RID: 24417
		private readonly string reason;
	}
}
