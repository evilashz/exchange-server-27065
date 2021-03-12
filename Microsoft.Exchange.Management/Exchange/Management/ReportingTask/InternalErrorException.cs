using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001161 RID: 4449
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InternalErrorException : ReportingException
	{
		// Token: 0x0600B5D2 RID: 46546 RVA: 0x0029EE31 File Offset: 0x0029D031
		public InternalErrorException(int number) : base(Strings.InternalErrorException(number))
		{
			this.number = number;
		}

		// Token: 0x0600B5D3 RID: 46547 RVA: 0x0029EE46 File Offset: 0x0029D046
		public InternalErrorException(int number, Exception innerException) : base(Strings.InternalErrorException(number), innerException)
		{
			this.number = number;
		}

		// Token: 0x0600B5D4 RID: 46548 RVA: 0x0029EE5C File Offset: 0x0029D05C
		protected InternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.number = (int)info.GetValue("number", typeof(int));
		}

		// Token: 0x0600B5D5 RID: 46549 RVA: 0x0029EE86 File Offset: 0x0029D086
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("number", this.number);
		}

		// Token: 0x1700396B RID: 14699
		// (get) Token: 0x0600B5D6 RID: 46550 RVA: 0x0029EEA1 File Offset: 0x0029D0A1
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x040062D1 RID: 25297
		private readonly int number;
	}
}
