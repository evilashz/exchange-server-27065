using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DE2 RID: 3554
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetEcpVersionException : DataSourceOperationException
	{
		// Token: 0x0600A458 RID: 42072 RVA: 0x00284101 File Offset: 0x00282301
		public GetEcpVersionException(string ecpDllPath) : base(Strings.GetEcpVersionException(ecpDllPath))
		{
			this.ecpDllPath = ecpDllPath;
		}

		// Token: 0x0600A459 RID: 42073 RVA: 0x00284116 File Offset: 0x00282316
		public GetEcpVersionException(string ecpDllPath, Exception innerException) : base(Strings.GetEcpVersionException(ecpDllPath), innerException)
		{
			this.ecpDllPath = ecpDllPath;
		}

		// Token: 0x0600A45A RID: 42074 RVA: 0x0028412C File Offset: 0x0028232C
		protected GetEcpVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ecpDllPath = (string)info.GetValue("ecpDllPath", typeof(string));
		}

		// Token: 0x0600A45B RID: 42075 RVA: 0x00284156 File Offset: 0x00282356
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ecpDllPath", this.ecpDllPath);
		}

		// Token: 0x170035ED RID: 13805
		// (get) Token: 0x0600A45C RID: 42076 RVA: 0x00284171 File Offset: 0x00282371
		public string EcpDllPath
		{
			get
			{
				return this.ecpDllPath;
			}
		}

		// Token: 0x04005F53 RID: 24403
		private readonly string ecpDllPath;
	}
}
