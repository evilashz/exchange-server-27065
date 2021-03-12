using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x0200115F RID: 4447
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConnectionFailedException : ReportingException
	{
		// Token: 0x0600B5C8 RID: 46536 RVA: 0x0029ED41 File Offset: 0x0029CF41
		public ConnectionFailedException(int number) : base(Strings.ConnectionFailedException(number))
		{
			this.number = number;
		}

		// Token: 0x0600B5C9 RID: 46537 RVA: 0x0029ED56 File Offset: 0x0029CF56
		public ConnectionFailedException(int number, Exception innerException) : base(Strings.ConnectionFailedException(number), innerException)
		{
			this.number = number;
		}

		// Token: 0x0600B5CA RID: 46538 RVA: 0x0029ED6C File Offset: 0x0029CF6C
		protected ConnectionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.number = (int)info.GetValue("number", typeof(int));
		}

		// Token: 0x0600B5CB RID: 46539 RVA: 0x0029ED96 File Offset: 0x0029CF96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("number", this.number);
		}

		// Token: 0x17003969 RID: 14697
		// (get) Token: 0x0600B5CC RID: 46540 RVA: 0x0029EDB1 File Offset: 0x0029CFB1
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x040062CF RID: 25295
		private readonly int number;
	}
}
