using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200109C RID: 4252
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedingPathWarningException : LocalizedException
	{
		// Token: 0x0600B20B RID: 45579 RVA: 0x002994A4 File Offset: 0x002976A4
		public SeedingPathWarningException(string error) : base(Strings.SeedingPathWarningException(error))
		{
			this.error = error;
		}

		// Token: 0x0600B20C RID: 45580 RVA: 0x002994B9 File Offset: 0x002976B9
		public SeedingPathWarningException(string error, Exception innerException) : base(Strings.SeedingPathWarningException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600B20D RID: 45581 RVA: 0x002994CF File Offset: 0x002976CF
		protected SeedingPathWarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B20E RID: 45582 RVA: 0x002994F9 File Offset: 0x002976F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170038B8 RID: 14520
		// (get) Token: 0x0600B20F RID: 45583 RVA: 0x00299514 File Offset: 0x00297714
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400621E RID: 25118
		private readonly string error;
	}
}
