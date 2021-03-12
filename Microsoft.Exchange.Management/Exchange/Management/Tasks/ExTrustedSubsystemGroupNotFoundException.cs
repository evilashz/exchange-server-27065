using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E07 RID: 3591
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExTrustedSubsystemGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A51E RID: 42270 RVA: 0x00285706 File Offset: 0x00283906
		public ExTrustedSubsystemGroupNotFoundException(Guid guid) : base(Strings.ExTrustedSubsystemGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600A51F RID: 42271 RVA: 0x0028571B File Offset: 0x0028391B
		public ExTrustedSubsystemGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExTrustedSubsystemGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600A520 RID: 42272 RVA: 0x00285731 File Offset: 0x00283931
		protected ExTrustedSubsystemGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A521 RID: 42273 RVA: 0x0028575B File Offset: 0x0028395B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700361F RID: 13855
		// (get) Token: 0x0600A522 RID: 42274 RVA: 0x0028577B File Offset: 0x0028397B
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F85 RID: 24453
		private readonly Guid guid;
	}
}
