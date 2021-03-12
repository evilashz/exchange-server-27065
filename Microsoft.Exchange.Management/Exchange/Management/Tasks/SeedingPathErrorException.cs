using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200109B RID: 4251
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedingPathErrorException : LocalizedException
	{
		// Token: 0x0600B206 RID: 45574 RVA: 0x0029942C File Offset: 0x0029762C
		public SeedingPathErrorException(string error) : base(Strings.SeedingPathErrorException(error))
		{
			this.error = error;
		}

		// Token: 0x0600B207 RID: 45575 RVA: 0x00299441 File Offset: 0x00297641
		public SeedingPathErrorException(string error, Exception innerException) : base(Strings.SeedingPathErrorException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600B208 RID: 45576 RVA: 0x00299457 File Offset: 0x00297657
		protected SeedingPathErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B209 RID: 45577 RVA: 0x00299481 File Offset: 0x00297681
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170038B7 RID: 14519
		// (get) Token: 0x0600B20A RID: 45578 RVA: 0x0029949C File Offset: 0x0029769C
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400621D RID: 25117
		private readonly string error;
	}
}
