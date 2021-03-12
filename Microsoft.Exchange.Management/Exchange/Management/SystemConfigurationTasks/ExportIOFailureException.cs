using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F5F RID: 3935
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExportIOFailureException : LocalizedException
	{
		// Token: 0x0600ABD0 RID: 43984 RVA: 0x0028F957 File Offset: 0x0028DB57
		public ExportIOFailureException(string err) : base(Strings.ExportIOFailure(err))
		{
			this.err = err;
		}

		// Token: 0x0600ABD1 RID: 43985 RVA: 0x0028F96C File Offset: 0x0028DB6C
		public ExportIOFailureException(string err, Exception innerException) : base(Strings.ExportIOFailure(err), innerException)
		{
			this.err = err;
		}

		// Token: 0x0600ABD2 RID: 43986 RVA: 0x0028F982 File Offset: 0x0028DB82
		protected ExportIOFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x0600ABD3 RID: 43987 RVA: 0x0028F9AC File Offset: 0x0028DBAC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("err", this.err);
		}

		// Token: 0x17003771 RID: 14193
		// (get) Token: 0x0600ABD4 RID: 43988 RVA: 0x0028F9C7 File Offset: 0x0028DBC7
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x040060D7 RID: 24791
		private readonly string err;
	}
}
