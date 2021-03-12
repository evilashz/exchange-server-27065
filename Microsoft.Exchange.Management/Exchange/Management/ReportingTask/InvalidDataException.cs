using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001164 RID: 4452
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDataException : ReportingException
	{
		// Token: 0x0600B5E0 RID: 46560 RVA: 0x0029EF48 File Offset: 0x0029D148
		public InvalidDataException(string error) : base(Strings.InvalidDataException(error))
		{
			this.error = error;
		}

		// Token: 0x0600B5E1 RID: 46561 RVA: 0x0029EF5D File Offset: 0x0029D15D
		public InvalidDataException(string error, Exception innerException) : base(Strings.InvalidDataException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600B5E2 RID: 46562 RVA: 0x0029EF73 File Offset: 0x0029D173
		protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B5E3 RID: 46563 RVA: 0x0029EF9D File Offset: 0x0029D19D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700396D RID: 14701
		// (get) Token: 0x0600B5E4 RID: 46564 RVA: 0x0029EFB8 File Offset: 0x0029D1B8
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040062D3 RID: 25299
		private readonly string error;
	}
}
